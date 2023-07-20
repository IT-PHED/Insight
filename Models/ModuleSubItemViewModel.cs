using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ModuleSubItemViewModel
    {
        [Key]
        public int Module_Sub_Item_ID { get; set; }
       
        public string Module_Sub_Item_Name { get; set; }
        public string Module_Sub_Icon { get; set; }
        public string Module_Sub_Class { get; set; }
        public string Module_Sub_Controller_Name { get; set; }
        public string Module_Sub_Action_Name { get; set; }
        public int Module_ID { get; set; }
        public Module_Tbl Module_Tbl { get; set; }
        public int Module_Sub_Menu_Order { get; set; }

        public string Left_bg_Class { get; set; }

        public int ApplicationID { get; set; }
    }
}