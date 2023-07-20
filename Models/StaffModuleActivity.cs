using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class StaffModuleActivity
    {
        [Key]
        public Guid RoleActivityId { get; set; }

        public string ActivityId { get; set; }

        public string ActivityName { get; set; }

        public string Rights { get; set; }

        public string StaffId { get; set; }

        public string AssignedBy { get; set; }
        public string ActivityDesc { get; set; }
        public string MenuItemId { get; set; }
        
    }
}