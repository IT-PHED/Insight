

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 
using System.Data.SqlClient;
using System.Configuration; 
using Newtonsoft.Json;
using PHEDServe.Models;
//using System.Web.Http;
 
namespace ERP.Controllers
{ 
    [Authorize]
    public class StaffRoleController : Controller
    {
        //
        // GET: /StaffRole/

        static string subscriberID = "";
        static string branchCode = "";
        static string loginStaffID = "";


        ApplicationDbContext db = new ApplicationDbContext();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public ActionResult DataCorrection()
        {

            return View();


        }
        public ActionResult AssignRoles()
        {

            return View();


        }

        public ActionResult AssignStaffRoles2()
        {

            return View();


        }
        public ActionResult AssignStaffRoles()
        {

            return View();


        }



        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                var userLoggedIn = context.Users.SingleOrDefault(u => u.UserName == username);

                ViewData["SubscriberID"] = userLoggedIn.SubscriberId;
                ViewBag.SubscriberID = userLoggedIn.SubscriberId;
                subscriberID = userLoggedIn.SubscriberId;
                loginStaffID = userLoggedIn.Id;
                 
            }
 
            return View();
        }

        [HttpGet]
        public JsonResult AllMenuList()
        {


            // viewModel.subGroupList = 
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            var RolesModules = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();
            List<ModulesWithSubVM> Modules = new List<ModulesWithSubVM>();
            ModulesWithSubVM _Modules = new ModulesWithSubVM();
            RoleModule __RoleModule = new RoleModule();
            RoleModuleItem __RoleModuleItems = new RoleModuleItem(); List<RoleModule> _RoleModule = new List<RoleModule>();
            foreach (var Module in RolesModules)
            {
                _Modules = new ModulesWithSubVM();
                __RoleModule = new RoleModule();

                //List of RoleModule


                List<RoleModuleItem> _RoleModuleItem = new List<RoleModuleItem>();
                //get all the Module items Associated with this Particular Module

                var AssociatedModuleItems = db.MenuItemsMains.Where(p => p.ParentMenuItemId == Module.MenuItemId).ToList();

                foreach (var moduleitem in AssociatedModuleItems)
                {
                    __RoleModuleItems = new RoleModuleItem();
                    //add them to the   __RoleModuleItems
                    __RoleModuleItems.ModuleItemId = moduleitem.MenuItemId;
                    __RoleModuleItems.ModuleItemName = moduleitem.MenuText;
                    //                if(Module.MenuText == "Customer_Care")
                    //                {
                    //                    __RoleModuleItems.Status = true;

                    //                }else{

                    //__RoleModuleItems.Status= false;
                    //                }

                    //add the Items to the List for the MOdule Items
                    _RoleModuleItem.Add(__RoleModuleItems);
                }

                //add to the Role Module 
                __RoleModule.ModuleId = Module.MenuItemId;
                __RoleModule.ModuleName = Module.MenuText;
                __RoleModule.RoleModuleItem = _RoleModuleItem;
                _RoleModule.Add(__RoleModule);

                //_Modules.RoleModule = _RoleModule;
                //Modules.Add(_Modules);
            }


            viewModel.AllMenuLists = _RoleModule.OrderBy(p => p.ModuleName).ToList();
            viewModel.StaffAllMenuLists = new List<RoleModule>();

            JsonResult jsonResult = base.Json(JsonConvert.SerializeObject(viewModel), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = new int?(2147483647);
            return jsonResult;
        }
        [HttpGet]
        public JsonResult AllMenuList2()
        {


            // viewModel.subGroupList = 
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            var RolesModules = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();
            List<ModulesWithSubVM> Modules = new List<ModulesWithSubVM>();
            ModulesWithSubVM _Modules = new ModulesWithSubVM();
            RoleModule __RoleModule = new RoleModule();
            RoleModuleItem __RoleModuleItems = new RoleModuleItem(); List<RoleModule> _RoleModule = new List<RoleModule>();



            /// viewModel.AllMenuLists = _RoleModule.OrderBy(p=>p.ModuleName).ToList();
            //  viewModel.StaffAllMenuLists = new List<RoleModule>();

            JsonResult jsonResult = base.Json(JsonConvert.SerializeObject(viewModel), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = new int?(2147483647);
            return jsonResult;
        }

        [HttpGet]
        public JsonResult loadAssignedRoleToStaff(string StaffId)
        {
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);

            // viewModel.usersList = new List<ApplicationUser>();// await context.Users.AsNoTracking().ToListAsync();
            //viewModel.modulesList = await context.MenuItemsMains.AsNoTracking().ToListAsync();
            // var List = db.StaffRoles.Where(p => p.StaffId == StaffId && p.ParentMenuItemId == null).ToList();
            //Iterate through the List and get it to a point where we can have 


            #region OldRoleAssignment
            //List<ModulesWithSubVM> Modules = new List<ModulesWithSubVM>();
            //ModulesWithSubVM _Modules = new ModulesWithSubVM();
            //RoleModule __RoleModule = new RoleModule();
            //RoleModuleItem __RoleModuleItems = new RoleModuleItem();
            //List<RoleModule> _RoleModule = new List<RoleModule>();
            //foreach (var Module in List)
            //{
            //    _Modules = new ModulesWithSubVM();

            //    //List of RoleModule
            //    __RoleModule = new RoleModule();

            //    List<RoleModuleItem> _RoleModuleItem = new List<RoleModuleItem>();
            //    //get all the Module items Associated with this Particular Module

            //    var AssociatedModuleItems = db.StaffRoles.Where(p => p.StaffId == StaffId && p.ParentMenuItemId == Module.MenuItemId).ToList();

            //    foreach (var moduleitem in AssociatedModuleItems)
            //    {
            //        __RoleModuleItems = new RoleModuleItem();
            //        //add them to the   __RoleModuleItems
            //        __RoleModuleItems.ModuleItemId = moduleitem.MenuItemId;
            //        __RoleModuleItems.ModuleItemName = moduleitem.MenuText;
            //        //add the Items to the List for the MOdule Items
            //        _RoleModuleItem.Add(__RoleModuleItems);
            //    }

            //    //add to the Role Module 
            //    __RoleModule.ModuleId = Module.MenuItemId;
            //    __RoleModule.ModuleName = Module.MenuText;
            //    __RoleModule.RoleModuleItem = _RoleModuleItem;
            //    _RoleModule.Add(__RoleModule);

            //    //_Modules.RoleModule = _RoleModule;
            //    //Modules.Add(_Modules);
            //}


            //viewModel.StaffAllMenuLists = _RoleModule.OrderBy(p => p.ModuleName).ToList();

            #endregion

            #region SelfBinding
            var RolesModules = db.MenuItemsMains.Where(p => p.ParentMenuItemId == null).ToList();
            List<ModulesWithSubVM> Modules = new List<ModulesWithSubVM>();
            ModulesWithSubVM _Modules = new ModulesWithSubVM();
            RoleModule __RoleModule = new RoleModule();
            RoleModuleItem __RoleModuleItems = new RoleModuleItem(); List<RoleModule> _RoleModule = new List<RoleModule>();
            foreach (var Module in RolesModules)
            {
                _Modules = new ModulesWithSubVM();
                __RoleModule = new RoleModule();

                //List of RoleModule


                List<RoleModuleItem> _RoleModuleItem = new List<RoleModuleItem>();
                //get all the Module items Associated with this Particular Module

                var AssociatedModuleItems = db.MenuItemsMains.Where(p => p.ParentMenuItemId == Module.MenuItemId).ToList();

                foreach (var moduleitem in AssociatedModuleItems)
                {
                    __RoleModuleItems = new RoleModuleItem();
                    //add them to the   __RoleModuleItems
                    __RoleModuleItems.ModuleItemId = moduleitem.MenuItemId;
                    __RoleModuleItems.ModuleItemName = moduleitem.MenuText;
                    __RoleModuleItems.Status = CheckIfStaffIsAssignedModule(StaffId, moduleitem.MenuItemId);
                    //add the Items to the List for the MOdule Items
                    _RoleModuleItem.Add(__RoleModuleItems);
                }

                //add to the Role Module 
                __RoleModule.ModuleId = Module.MenuItemId;
                __RoleModule.ModuleName = Module.MenuText;
                __RoleModule.RoleModuleItem = _RoleModuleItem;
                _RoleModule.Add(__RoleModule);

                //_Modules.RoleModule = _RoleModule;
                //Modules.Add(_Modules);
            }




            #endregion


            viewModel.AllMenuLists = _RoleModule.OrderBy(p => p.ModuleName).ToList();
            viewModel.StaffAllMenuLists = new List<RoleModule>();
            JsonResult jsonResult = base.Json(JsonConvert.SerializeObject(viewModel), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = new int?(2147483647);
            return jsonResult;

        }

        private bool CheckIfStaffIsAssignedModule(string StaffId, string MenuItemId)
        {

            //check 
            if (db.StaffRoles.FirstOrDefault(p => p.StaffId == StaffId && p.MenuItemId == MenuItemId) != null)
            {
                return true;

            }
            return false;

        }



        [System.Web.Mvc.HttpGet]
        public async Task<JsonResult> ReassignRolesToStaff(string StaffId, string SelectedRoles)
        {
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);

            //get the Comma Separated into an Array of Comma Sepoarated Values

            var RoleList = SelectedRoles.Split(',').ToList();

            StaffRoleAssignment Ass = new StaffRoleAssignment();
            StaffRole staff = new StaffRole();

            //get all roles assigned to Staff Previously

            var AllRoles = db.StaffRoles.Where(p => p.StaffId == StaffId).ToList();


            foreach (var ss in AllRoles)
            {
                //delete 

                var s = db.StaffRoles.Find(ss.RoleId);
                if (s != null)
                {
                    db.StaffRoles.Remove(s);
                    db.SaveChanges();
                }
            }

            foreach (var item in RoleList)
            {
                staff = new StaffRole();
                //check if it is a Module or a ModuleItem
                Ass = CheckIfMenuIsModule(item.Trim());

                if (Ass.Status == "MODULE")
                {
                    //It's a Module

                    staff.ActionName = Ass.ActionName;
                    staff.ControllerName = Ass.ControllerName;
                    staff.icon = Ass.Icon;
                    staff.MenuId = 0;
                    staff.MenuItemId = Ass.MenuItemId;
                    staff.MenuOrder = Convert.ToInt32(Ass.MenuOrder);
                    staff.MenuText = Ass.MenuText;
                    staff.ParentMenuItemId = Ass.ParentMenuItemId;
                    staff.RoleId = Guid.NewGuid().ToString();
                    staff.StaffId = StaffId;
                    db.StaffRoles.Add(staff);
                    db.SaveChanges();
                }
                else
                {
                    //its a Module Item 
                    staff.ActionName = Ass.ActionName;
                    staff.ControllerName = Ass.ControllerName;
                    staff.icon = Ass.Icon;
                    staff.MenuId = 0;
                    staff.MenuItemId = Ass.MenuItemId;
                    staff.MenuOrder = Convert.ToInt32(Ass.MenuOrder);
                    staff.MenuText = Ass.MenuText;
                    staff.ParentMenuItemId = Ass.ParentMenuItemId;
                    staff.RoleId = Guid.NewGuid().ToString();
                    staff.StaffId = StaffId;
                    db.StaffRoles.Add(staff);
                    db.SaveChanges();

                }



            }

            JsonResult jsonResult = base.Json(JsonConvert.SerializeObject(viewModel), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = new int?(2147483647);
            return jsonResult;

        }




   

        [HttpGet]
        public async Task<JsonResult> CorrectStaffData(string staffId, string FirstName, string LastName,
         string UserName, string Email, string Id, string StaffName, string Status, string IMEI1, string IMEI2, string IMEILogin, string AllowBillVerificationChange)
        {

            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);



            //Correct the Identity

            if (Status != "EDIT")
            {
                StaffBasicData users = new StaffBasicData();

                var Check = db.StaffBasicDatas.FirstOrDefault(p => p.Staff_Id == staffId && p.Email == Email);



                if (Check != null)
                {



                    var _result = JsonConvert.SerializeObject(viewModel);
                    var _jsonResult = Json(new { result = _result, error = "This StaffID and Email have been registered Before. Please crosscheck and try again" }, JsonRequestBehavior.AllowGet);
                    _jsonResult.MaxJsonLength = int.MaxValue;
                    return _jsonResult;

                }
                else
                {
                    users.IMEI1 = IMEI1;
                    users.IMEI2 = IMEI2;
                    users.IMEILogin = IMEILogin;


                    users.Surname = LastName;
                    users.Email = Email;
                    users.OtherNames = FirstName;
                    users.Staff_Id = staffId;
                    db.StaffBasicDatas.Add(users);
                    db.SaveChanges();
                }


            }
            else
            {

                var _Identity = db.Users.Find(Id);
                //.Where(p => p.FirstName.ToLower().Contains(searchText.ToLower()) || p.Email.ToLower().Contains(searchText.ToLower()) || p.LastName.ToLower().Contains(searchText.ToLower()) || p.StaffId.ToLower().Contains(searchText.ToLower())).Select(u => new { StaffId = u.StaffId, StaffName = u.StaffName, Email = u.Email, UserName = u.UserName, Id = u.Id, FirstName = u.FirstName, LastName = u.LastName }).ToArray();

                if (_Identity != null)
                {

                    if (AllowBillVerificationChange == "True" || AllowBillVerificationChange == "true")
                    {
                        //Clear the Verification and allow him to Do a New One
                        _Identity.Submission = "RESUBMIT";
                    }

                    _Identity.IMEI1 = IMEI1;
                    _Identity.IMEI2 = IMEI2;
                    _Identity.IMEILogin = IMEILogin;
                    _Identity.LastName = LastName;
                    _Identity.UserName = UserName;
                    _Identity.Email = Email;
                    _Identity.FirstName = FirstName;
                    _Identity.StaffId = staffId;
                    _Identity.StaffName = LastName + " " + FirstName;
                    db.Entry(_Identity).State = EntityState.Modified;
                    db.SaveChanges();
                }


                //Correct the 

                var Gret = db.StaffBasicDatas.FirstOrDefault(p => p.Staff_Id == staffId && p.Email == Email);

                if (Gret != null)
                {
                    Gret.Surname = LastName;
                    Gret.Email = Email;
                    Gret.OtherNames = FirstName;
                    Gret.Staff_Id = staffId;
                    db.Entry(Gret).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            result = JsonConvert.SerializeObject(viewModel);
            var jsonResult = Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }







        private StaffRoleAssignment CheckIfMenuIsModule(string item)
        {

            StaffRoleAssignment ass = new StaffRoleAssignment();

            var check = db.MenuItemsMains.FirstOrDefault(p => p.MenuItemId == item);

            if (check.ParentMenuItemId == null || string.IsNullOrEmpty(check.ParentMenuItemId))
            {
                //it is a Module

                ass.Status = "MODULE";
                ass.Icon = check.icon;
                ass.MenuItemId = check.MenuItemId;
                ass.MenuOrder = check.MenuOrder.ToString();
                ass.MenuText = check.MenuText;
                ass.ActionName = check.ActionName;
                ass.ControllerName = check.ControllerName;
                ass.ParentMenuItemId = check.ParentMenuItemId;
                return ass;
            }
            else
            {
                ass.Status = "SUBMODULE";
                ass.Icon = check.icon;
                ass.MenuItemId = check.MenuItemId;
                ass.MenuOrder = check.MenuOrder.ToString();
                ass.MenuText = check.MenuText;
                ass.ActionName = check.ActionName;
                ass.ControllerName = check.ControllerName;
                ass.ParentMenuItemId = check.ParentMenuItemId;
                return ass;
            }
        }


        [HttpGet]
        public async Task<JsonResult> loadstaffRoleViewModel()
        {


            // viewModel.subGroupList = 
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            using (var context = new ApplicationDbContext())
            {
                viewModel.usersList = new List<ApplicationUser>();// await context.Users.AsNoTracking().ToListAsync();
                viewModel.modulesList = await context.MenuItemsMains.AsNoTracking().ToListAsync();
                viewModel.allStaffRoleList = await context.StaffRoles.AsNoTracking().ToListAsync();

                //viewModel.modulesHeadList = context.MenuItemsMains.Where(n => n.ControllerName == null).ToList();
                //viewModel.ModulesWithSubList = context.MenuItemsMains.ToList();

                var RolesModules = db.Modules.ToList();
                List<ModulesWithSubVM> Modules = new List<ModulesWithSubVM>();
                ModulesWithSubVM _Modules = new ModulesWithSubVM();
                RoleModule __RoleModule = new RoleModule();
                RoleModuleItem __RoleModuleItems = new RoleModuleItem();
                foreach (var Module in RolesModules)
                {
                    _Modules = new ModulesWithSubVM();
                    __RoleModule = new RoleModule();

                    //List of RoleModule

                    List<RoleModule> _RoleModule = new List<RoleModule>();
                    List<RoleModuleItem> _RoleModuleItem = new List<RoleModuleItem>();
                    //get all the Module items Associated with this Particular Module

                    var AssociatedModuleItems = context.ModuleItems.Where(p => p.ModuleId == Module.ModuleId).ToList();

                    foreach (var moduleitem in AssociatedModuleItems)
                    {
                        __RoleModuleItems = new RoleModuleItem();
                        //add them to the   __RoleModuleItems
                        __RoleModuleItems.ModuleItemId = moduleitem.ModuleItemId;
                        __RoleModuleItems.ModuleItemName = moduleitem.ModuleItemName;
                        //add the Items to the List for the MOdule Items
                        _RoleModuleItem.Add(__RoleModuleItems);
                    }

                    //add to the Role Module 
                    __RoleModule.ModuleId = Module.ModuleId;
                    __RoleModule.ModuleName = Module.ModuleId;
                    __RoleModule.RoleModuleItem = _RoleModuleItem;
                    _RoleModule.Add(__RoleModule);

                    _Modules.RoleModule = _RoleModule;
                    Modules.Add(_Modules);
                }


                viewModel.ModulesWithSubList = Modules;
                // viewModel.modulesHeadList = await ModuleActivity("", context);
                //viewModel.modulesActivityList = context.ModuleActivitys.ToList();
                //  viewModel.staffModulesActivityList = await context.StaffModuleActivitys.AsNoTracking().ToListAsync();
                result = JsonConvert.SerializeObject(viewModel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> loadStaffModuleactivity(string staffId)
        {


            // viewModel.subGroupList = 
            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            using (var context = new ApplicationDbContext())
            {
                viewModel.usersList = new List<ApplicationUser>();// await context.Users.AsNoTracking().ToListAsync();
                viewModel.modulesList = await context.MenuItemsMains.AsNoTracking().ToListAsync();
                viewModel.allStaffRoleList = await context.StaffRoles.AsNoTracking().ToListAsync();

                //viewModel.modulesHeadList = context.MenuItemsMains.Where(n => n.ControllerName == null).ToList();


                viewModel.modulesHeadList = await ModuleActivity(staffId, context);
                //viewModel.modulesActivityList = context.ModuleActivitys.ToList();
                viewModel.staffModulesActivityList = await context.StaffModuleActivitys.AsNoTracking().ToListAsync();
                result = JsonConvert.SerializeObject(viewModel);
            }






            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public async Task<List<MenuItemsMain>> ModuleActivity(string staffId, ApplicationDbContext context)
        {


            var moduleMappedActivity = new List<MenuItemsMain>();
            var getAllModuleHeaders = await context.MenuItemsMains.AsNoTracking().Where(n => n.ControllerName == null).ToListAsync();
            MenuItemsMain menuItems = new MenuItemsMain();
            foreach (var menu in getAllModuleHeaders)
            {
                menuItems = new MenuItemsMain();
                if (!string.IsNullOrEmpty(staffId))
                {
                    var checkIfModuleisInStaffRole = await context.StaffRoles.FirstOrDefaultAsync(n => n.MenuItemId == menu.MenuItemId && n.StaffId == staffId);
                    if (checkIfModuleisInStaffRole == null)
                    {
                        continue;
                    }


                    menuItems.ActionName = menu.ActionName;
                    menuItems.ControllerName = menu.ControllerName;
                    menuItems.icon = menu.icon;
                    menuItems.StyleClass = menu.StyleClass;
                    menuItems.LinkUrl = menu.LinkUrl;
                    menuItems.menuCode = menu.menuCode;
                    menuItems.MenuId = menu.MenuId;
                    menuItems.MenuItemId = menu.MenuItemId;
                    menuItems.MenuOrder = menu.MenuOrder;
                    menuItems.MenuText = menu.MenuText;
                    menuItems.ParentMenuItemId = menu.ParentMenuItemId;

                    var modActList = await context.ModuleActivitys.AsNoTracking().Where(n => n.MenuItemId == menu.MenuItemId).ToListAsync();
                    var activityListByStaff = new List<ModuleActivity>();
                    ModuleActivity mainModeAct = new ModuleActivity();

                    foreach (var activity in modActList)
                    {
                        mainModeAct = new ModuleActivity();

                        mainModeAct.ActivityId = activity.ActivityId;
                        mainModeAct.ActivityName = activity.ActivityName;
                        mainModeAct.ActivityDesc = activity.ActivityDesc;
                        mainModeAct.MenuItemId = activity.MenuItemId;

                        string mainActivity = activity.ActivityId.ToString();
                        var checkIfActivityExistForStaff = await context.StaffModuleActivitys.AsNoTracking().FirstOrDefaultAsync(n => n.ActivityId == mainActivity && n.StaffId == staffId);
                        if (checkIfActivityExistForStaff != null)
                        {
                            mainModeAct.ActivityDefaultRights = checkIfActivityExistForStaff.Rights;

                        }
                        else
                        {
                            mainModeAct.ActivityDefaultRights = activity.ActivityDefaultRights;
                        }

                        activityListByStaff.Add(mainModeAct);
                    }


                    menuItems.moduleActivityList = activityListByStaff;
                    moduleMappedActivity.Add(menuItems);
                }
                else
                {



                    menuItems.ActionName = menu.ActionName;
                    menuItems.ControllerName = menu.ControllerName;
                    menuItems.icon = menu.icon;
                    menuItems.StyleClass = menu.StyleClass;
                    menuItems.LinkUrl = menu.LinkUrl;
                    menuItems.menuCode = menu.menuCode;
                    menuItems.MenuId = menu.MenuId;
                    menuItems.MenuItemId = menu.MenuItemId;
                    menuItems.MenuOrder = menu.MenuOrder;
                    menuItems.MenuText = menu.MenuText;
                    menuItems.ParentMenuItemId = menu.ParentMenuItemId;

                    menuItems.moduleActivityList = await context.ModuleActivitys.AsNoTracking().Where(n => n.MenuItemId == menu.MenuItemId).ToListAsync();

                    moduleMappedActivity.Add(menuItems);
                }




            }

            return moduleMappedActivity;


        }













        [HttpPost]
        public async Task<JsonResult> AssignUnAssignRoles(string postedData, string staffId)
        {
            var dataFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(postedData);


            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            using (var context = new ApplicationDbContext())
            {
                var AllStaffAssignRoles = await context.StaffRoles.Where(n => n.StaffId == staffId).ToListAsync();

                foreach (var role in AllStaffAssignRoles)
                {
                    StaffRole staffrole = await context.StaffRoles.FindAsync(role.RoleId);

                    context.StaffRoles.Remove(staffrole);

                }
                context.SaveChanges();

                StaffRole staffroleNew = new StaffRole();
                foreach (var role in dataFromClient)
                {

                    var checkIfParentExist = await context.StaffRoles.FirstOrDefaultAsync(n => n.MenuItemId == role.ParentMenuItemId && n.StaffId == staffId);
                    if (checkIfParentExist == null)
                    {
                        var getParentInMaster = await context.MenuItemsMains.FirstOrDefaultAsync(n => n.MenuItemId == role.ParentMenuItemId);

                        staffroleNew = new StaffRole();

                        staffroleNew.ActionName = getParentInMaster.ActionName;
                        staffroleNew.ControllerName = getParentInMaster.ControllerName;
                        staffroleNew.icon = getParentInMaster.icon;
                        staffroleNew.StyleClass = getParentInMaster.StyleClass;
                        staffroleNew.LinkUrl = getParentInMaster.LinkUrl;
                        staffroleNew.menuCode = getParentInMaster.menuCode;
                        staffroleNew.MenuId = getParentInMaster.MenuId;
                        staffroleNew.MenuItemId = getParentInMaster.MenuItemId;
                        staffroleNew.MenuOrder = getParentInMaster.MenuOrder;
                        staffroleNew.MenuText = getParentInMaster.MenuText;
                        staffroleNew.ParentMenuItemId = getParentInMaster.ParentMenuItemId;
                        staffroleNew.StaffId = staffId;
                        context.StaffRoles.Add(staffroleNew);

                    }

                    staffroleNew = new StaffRole();

                    staffroleNew.ActionName = role.ActionName;
                    staffroleNew.ControllerName = role.ControllerName;
                    staffroleNew.icon = role.icon;
                    staffroleNew.StyleClass = role.StyleClass;
                    staffroleNew.LinkUrl = role.LinkUrl;
                    staffroleNew.menuCode = role.menuCode;
                    staffroleNew.MenuId = role.MenuId;
                    staffroleNew.MenuItemId = role.MenuItemId;
                    staffroleNew.MenuOrder = role.MenuOrder;
                    staffroleNew.MenuText = role.MenuText;
                    staffroleNew.ParentMenuItemId = role.ParentMenuItemId;
                    staffroleNew.StaffId = staffId;
                    context.StaffRoles.Add(staffroleNew);

                    await context.SaveChangesAsync();

                }


                viewModel.usersList = new List<ApplicationUser>();// await context.Users.AsNoTracking().ToListAsync();
                viewModel.modulesList = await context.MenuItemsMains.AsNoTracking().ToListAsync();
                viewModel.allStaffRoleList = await context.StaffRoles.AsNoTracking().ToListAsync();

                //viewModel.modulesHeadList = context.MenuItemsMains.Where(n => n.ControllerName == null).ToList();


                viewModel.modulesHeadList = await ModuleActivity(staffId, context);
                //viewModel.modulesActivityList = context.ModuleActivitys.ToList();
                viewModel.staffModulesActivityList = await context.StaffModuleActivitys.AsNoTracking().ToListAsync();
                result = JsonConvert.SerializeObject(viewModel);

            }






            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddStaffModuleAccess(string postedData, string staffId)
        {
            var dataFromClient = JsonConvert.DeserializeObject<List<MenuItemsMain>>(postedData);

            StaffRoleViewModel viewModel = new StaffRoleViewModel();
            var result = JsonConvert.SerializeObject(viewModel);
            using (var context = new ApplicationDbContext())
            {

                var AllStaffModuleActivity = await context.StaffModuleActivitys.Where(n => n.StaffId == staffId).ToListAsync();

                foreach (var role in AllStaffModuleActivity)
                {
                    StaffModuleActivity staffrole = await context.StaffModuleActivitys.FindAsync(role.RoleActivityId);

                    context.StaffModuleActivitys.Remove(staffrole);

                }

                context.SaveChanges();
                foreach (var module in dataFromClient)
                {
                    var checkIfParentExist = await context.StaffRoles.FirstOrDefaultAsync(n => n.MenuItemId == module.MenuItemId && n.StaffId == staffId);
                    if (checkIfParentExist == null)
                    {
                        continue;

                    }
                    StaffModuleActivity staffActivity = new StaffModuleActivity();
                    foreach (var activity in module.moduleActivityList)
                    {
                        staffActivity = new StaffModuleActivity();

                        staffActivity.RoleActivityId = Guid.NewGuid();
                        staffActivity.ActivityId = activity.ActivityId.ToString();
                        staffActivity.ActivityName = activity.ActivityName;
                        staffActivity.ActivityDesc = activity.ActivityDesc;
                        staffActivity.StaffId = staffId;
                        staffActivity.MenuItemId = activity.MenuItemId;

                        if (activity.AccessRight != null)
                        {
                            staffActivity.Rights = activity.AccessRight.name;
                        }

                        context.StaffModuleActivitys.Add(staffActivity);
                    }
                    await context.SaveChangesAsync();
                }

                viewModel.usersList = new List<ApplicationUser>();// await context.Users.AsNoTracking().ToListAsync();
                viewModel.modulesList = await context.MenuItemsMains.AsNoTracking().ToListAsync();
                viewModel.allStaffRoleList = await context.StaffRoles.AsNoTracking().ToListAsync();

                //viewModel.modulesHeadList = context.MenuItemsMains.Where(n => n.ControllerName == null).ToList();


                viewModel.modulesHeadList = await ModuleActivity(staffId, context);
                //viewModel.modulesActivityList = context.ModuleActivitys.ToList();
                viewModel.staffModulesActivityList = await context.StaffModuleActivitys.AsNoTracking().ToListAsync();
                result = JsonConvert.SerializeObject(viewModel);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveData(string str)
        {
            foreach (string s in str.Split(','))
            {
                if (!string.IsNullOrEmpty(s))
                {

                    string mystring = s;

                    string testedString = mystring;
                    //Perform your opeartion here
                }
            }

            return Json("");
        }


        public ActionResult Checkboxes()
        {
            return View("Index", new string[0]);
        }

        [HttpPost]
        public ActionResult Checkboxes(string[] checkedFiles)
        {
            checkedFiles = checkedFiles ?? new string[0];
            return View("Index", checkedFiles);
        }

        public IEnumerable<StaffRole> GetLoggedInStaffRole(string loginStaffID)
        {


            // int mainAppID = Convert.ToInt32(Session["APPLICATIONID"].ToString());

            // ApplicationDbContext db = new ApplicationDbContext();
            var model = new List<StaffRole>();

            using (var contextStaffRole = new ApplicationDbContext())
            {
                model = contextStaffRole.StaffRoles.Where(m => m.StaffId == loginStaffID).ToList();

                //model = contextStaffRole.StaffRoles.Include("Children").Where(m => m.MenuId == 1 && m.Staff_ID == loginStaffID).ToList();
            }



            //string query = "select * from StaffRole where [Staff_ID] =" + loginStaffID;
            //var result = con.Query<StaffRole>(query);
            // model = result;   


            return model;
        }


        [HttpGet]
        public JsonResult SearchStaff(string searchText)
        {
            db = new ApplicationDbContext();
            var array = db.Users.Where(p => p.FirstName.ToLower().Contains(searchText.ToLower()) || p.Email.ToLower().Contains(searchText.ToLower()) || p.LastName.ToLower().Contains(searchText.ToLower()) || p.StaffId.ToLower().Contains(searchText.ToLower())).Select(u => new { StaffId = u.StaffId, StaffName = u.StaffName, Email = u.Email, UserName = u.UserName, Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, IMEI1 = u.IMEI1, IMEI2 = u.IMEI2, IMEILogin = u.IMEILogin }).ToArray();
            return base.Json(array, 0);
        }
    }
}