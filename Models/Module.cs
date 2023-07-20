using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Module
    {
        public Module()
        {
            var ModuleGuid = Guid.NewGuid();
            ModuleId = ModuleGuid.ToString();
        }

        [Key]
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int? ModulePriority { get; set; }
        public string ModuleIcon { get; set; }
        public int Identifier { get; set; }

    }
}