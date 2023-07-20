using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ReferencesViewModel
    {


        public List<ApplicationUser> UserStaffDetailList { get; set; }
        public List<Module> ModuleList { get; set; }

        public List<ModuleItem> ModuleItemList { get; set; }

        public List<MenuItemsMain> MenuItemsMainList { get; set; }


        public List<MenuItemsMain> MenuMainList { get; set; }


    }
}