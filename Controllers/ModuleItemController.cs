using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using PHEDServe.Models;

namespace ERP.Controllers
{
    public class ModuleItemController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string SubscriberID = "0";
        static string loginStaffID = "";
        //
        // GET: /SIMS/ModuleItem/
        public ActionResult Index()
        {
            ViewBag.ModuleName = "Configurations";
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {

                var userLoggedIn = context.Users.SingleOrDefault(u => u.UserName == username);

                //ViewData["SubscriberID"] = userLoggedIn.SubscriberId;
                //ViewBag.SubscriberID = userLoggedIn.SubscriberId;
                //SubscriberID = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;
            }
            //var canAccessModule = db.StaffRoles.AsNoTracking().FirstOrDefault(n => n.ControllerName == "Country" && n.StaffId == loginStaffID);
            //if (canAccessModule == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView();
            //}

            return View();
          
        }


        public ReferencesViewModel populateStudentInfoViewModel(List<MenuItemsMain> ModuleItemList, List<MenuItemsMain> ModuleList)
        { 
            ReferencesViewModel viewModel = new ReferencesViewModel();

            //viewModel.ModuleList = ModuleList;
            viewModel.MenuItemsMainList = ModuleItemList;
            viewModel.MenuMainList = ModuleList;

            return viewModel;
        }


        [HttpGet]
        public JsonResult loadAllReferencesViewModel()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {

                var userLoggedIn = context.Users.SingleOrDefault(u => u.UserName == username);

                //ViewData["SubscriberID"] = userLoggedIn.SubscriberId;
                //ViewBag.SubscriberID = userLoggedIn.SubscriberId;
                //SubscriberID = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;


            }
            //var canAccessModule = db.StaffRoles.AsNoTracking().FirstOrDefault(n => n.ControllerName == "Country" && n.StaffId == loginStaffID);
            //if (canAccessModule == null)
            //{
            //    return Json("", JsonRequestBehavior.AllowGet);
            //}
            var GetAllDetails = db.MenuItemsMains.Where(p=>p.ParentMenuItemId != null).ToList();
            var GetAllCountryDetails = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();

            ReferencesViewModel myModel = populateStudentInfoViewModel(GetAllDetails, GetAllCountryDetails);

            var result = JsonConvert.SerializeObject(myModel);


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InsertUpdatedDelete(string addModifiedItems, string deletedItems)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {

                var userLoggedIn = context.Users.SingleOrDefault(u => u.UserName == username);

                //ViewData["SubscriberID"] = userLoggedIn.SubscriberId;
                //ViewBag.SubscriberID = userLoggedIn.SubscriberId;
                //SubscriberID = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;


            }

            //var canAccessModule = db.StaffRoles.AsNoTracking().FirstOrDefault(n => n.ControllerName == "Country" && n.StaffId == loginStaffID);
            //if (canAccessModule == null)
            //{
            //    return Json("", JsonRequestBehavior.AllowGet);
            //}

            var addModifiedItemsFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(addModifiedItems);

            var deletedItemsFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(deletedItems);
             

            foreach (var item in addModifiedItemsFromClient)
            {
                MenuItemsMain Cont = db.MenuItemsMains.FirstOrDefault(m => m.MenuItemId == item.MenuItemId);

                if (Cont == null)
                { 
                    db.MenuItemsMains.Add(item); 
                }
                else
                {
                    MenuItemsMain myModuleItem = db.MenuItemsMains.Find(item.MenuItemId); 
                    myModuleItem.MenuText = item.MenuText;
                    myModuleItem.ControllerName = item.ControllerName;
                    myModuleItem.ActionName = item.ActionName;
                    myModuleItem.MenuOrder = item.MenuOrder;
                    db.Entry(myModuleItem).State = EntityState.Modified;
                }
            }

            foreach (var Deleteditem in deletedItemsFromClient)
            {
                MenuItemsMain myModuleItem = db.MenuItemsMains.Find(Deleteditem.MenuItemId);

                if (myModuleItem == null)
                {
                    continue;
                }
                db.MenuItemsMains.Remove(myModuleItem); 
            }



            db.SaveChanges();
              
            var GetAllDetails = db.MenuItemsMains.Where(p => p.ParentMenuItemId != null).ToList();
            var GetAllCountryDetails = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();
               
            ReferencesViewModel myModel = populateStudentInfoViewModel(GetAllDetails, GetAllCountryDetails);

            var result = JsonConvert.SerializeObject(myModel);
             
            return Json(result, JsonRequestBehavior.AllowGet);
        }



	}
}