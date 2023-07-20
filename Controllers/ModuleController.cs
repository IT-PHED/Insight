using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHEDServe.Models;
using PHEDServe.Models;

namespace ERP.Controllers
{
    public class ModuleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        static string subcriberId = "0";
        static string subscriptionUserId = "0";
        static string loginStaffID = "";
        //
        // GET: /SIMS/Module/
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
                //subcriberId = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;


            }
            return View();
        }



        public ReferencesViewModel populateDetailInfoViewModel(List<MenuItemsMain> ModuleList)
        {

            ReferencesViewModel viewModel = new ReferencesViewModel();

            viewModel.MenuItemsMainList = ModuleList;


            return viewModel;
        }


        [HttpGet]
        public JsonResult loadReferenceViewModel()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            { 
                var userLoggedIn = context.Users.SingleOrDefault(u => u.UserName == username); 
                //ViewData["SubscriberID"] = userLoggedIn.SubscriberId;
                //ViewBag.SubscriberID = userLoggedIn.SubscriberId;
                //subcriberId = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;
            }

            var GetAllDetails = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();

            ReferencesViewModel myModel = populateDetailInfoViewModel(GetAllDetails);

            var result = JsonConvert.SerializeObject(myModel); 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

         
        [HttpPost]
        public JsonResult InsertUpdatedDelete(string addModifiedItems, string deletedItems)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            var addModifiedItemsFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(addModifiedItems);

            var deletedItemsFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(deletedItems);



            foreach (var item in addModifiedItemsFromClient)
            {
                MenuItemsMain Cont = db.MenuItemsMains.FirstOrDefault(m => m.MenuItemId == item.MenuItemId);

                if (Cont == null)
                {


                    //item.SubscriberId = subcriberId;
                    //item.SubscriptionUserId = subscriptionUserId;

                    db.MenuItemsMains.Add(item); 
                }
                else
                {
                    MenuItemsMain myModule = db.MenuItemsMains.Find(item.MenuItemId);

                    myModule.MenuText = item.MenuText;
                    myModule.MenuOrder = item.MenuOrder;
                    myModule.icon = item.icon;
                    db.Entry(myModule).State = EntityState.Modified;
                } 
            }

            foreach (var Deleteditem in deletedItemsFromClient)
            {
                MenuItemsMain myModule = db.MenuItemsMains.Find(Deleteditem.MenuItemId);

                if (myModule == null)
                {
                    continue;
                }
                db.MenuItemsMains.Remove(myModule); 
            }

            db.SaveChanges();
             
            var GetAllDetails = db.MenuItemsMains.ToList();

            ReferencesViewModel myModel = populateDetailInfoViewModel(GetAllDetails);

            var result = JsonConvert.SerializeObject(myModel);
             
            return Json(result, JsonRequestBehavior.AllowGet);
        }


	}
}