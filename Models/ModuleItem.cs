using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ModuleItem
    {
        public ModuleItem()
        {
            var ModuleItemGuid = Guid.NewGuid();
            ModuleItemId = ModuleItemGuid.ToString();
        }

        [Key]
        public string ModuleItemId { get; set; }
        public string ModuleItemName { get; set; }
        public string ModuleId { get; set; }
        public int? Priority { get; set; }
      //  public string Message { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
        public string Icon { get; set; }
    }
}