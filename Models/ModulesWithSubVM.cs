using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
    //public class ModulesWithSubVM
    //{


    public class ModulesWithSubVM
    {
        public List<RoleModule> RoleModule { get; set; }
    }

        public class RoleModule
        {
            public string ModuleId { get; set; }
            public string ModuleName { get; set; }
            public List<RoleModuleItem> RoleModuleItem { get; set; }
        }

        public class RoleModuleItem
        {
            public string ModuleItemName { get; set; }
            public string ModuleItemId { get; set; }

            public bool Status { get; set; }
        }

    //}
}
