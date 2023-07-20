using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MenuItem
    {

        public int MenuItemId { get; set; }
        [StringLength(50)]
        public string MenuText { get; set; }
        [StringLength(255)]
        public string LinkUrl { get; set; }
        public int? MenuOrder { get; set; }
        public int? ParentMenuItemId { get; set; }
        public virtual MenuItem Parent { get; set; }
        public virtual ICollection<MenuItem> Children { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string icon { get; set; }
        public string StyleClass { get; set; }

        public int? StaffRole_Role_ID { get; set; }

        public int? MenuItemsMain_MenuItemId { get; set; }
    }
}