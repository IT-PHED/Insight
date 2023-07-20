using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class StaffRole
    {
        public StaffRole()
        {
            var newGuid = Guid.NewGuid();
            RoleId = newGuid.ToString();
        }

        [Key]
       
        public string RoleId { get; set; }
        public string StaffId { get; set; }
        public string MenuItemId { get; set; }
        //[StringLength(50)]
        public virtual MenuItemsMain MenuItemsMain { get; set; }
        public string MenuText { get; set; }
        [StringLength(255)]
        public string LinkUrl { get; set; }
        public int? MenuOrder { get; set; }
        public string ParentMenuItemId { get; set; }
        //public virtual MenuItem Parent { get; set; }
        //public virtual ICollection<MenuItem> Children { get; set; }
        public int MenuId { get; set; }
        //public virtual Menu Menu { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string icon { get; set; }

        public string StyleClass { get; set; }
        public string menuCode { get; set; }

        public string Left_bg_Class { get; set; }

        public string AreaName { get; set; }
    }
}