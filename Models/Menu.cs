using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }

    public class StaffRoleAssignment
    {
        public string MenuItemId { get; set; }
        public string MenuText { get; set; }
        public string MenuOrder { get; set; }
        public string ParentMenuItemId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; } 
    }
}