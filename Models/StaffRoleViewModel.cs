using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using PHEDServe.Models;

namespace PHEDServe.Models
{
    public class StaffRoleViewModel
    {

        public List<ApplicationUser> usersList { get; set; }
        public List<MenuItemsMain> modulesList { get; set; }
        public List<StaffRole> allStaffRoleList { get; set; }
        public List<MenuItemsMain> modulesHeadList { get; set; }

        public List<ModuleActivity> modulesActivityList { get; set; }
        public List<StaffModuleActivity> staffModulesActivityList { get; set; }

        public List<ModulesWithSubVM> ModulesWithSubList { get; set; }

        public List<ModulesWithSubVM> AllMenuList { get; set; }

        public List<RoleModule> AllMenuLists { get; set; }

        public List<ModulesWithSubVM> StaffModulesWithSubList { get; set; }

        public List<RoleModule> StaffAllMenuLists { get; set; }
    }
}