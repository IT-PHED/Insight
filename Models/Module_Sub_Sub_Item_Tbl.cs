using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Module_Sub_Sub_Item_Tbl
    {

        public Module_Sub_Sub_Item_Tbl()
        {
            var NewGuid = Guid.NewGuid();
            Module_Sub_Sub_Item_ID = NewGuid.ToString();
        }

       
     
      
        [Key]
        public string Module_Sub_Sub_Item_ID { get; set; }
        [Display(Name = "Module Sub Sub Item")]
        public string Module_Sub_Sub_Item_Name { get; set; }
          [Display(Name = "Icon")]
        public string Module_Sub_Sub_Icon { get; set; }
          [Display(Name = "Class")]
        public string Module_Sub_Sub_Class { get; set; }
          [Display(Name = "Controller Name")]
        public string Module_Sub_Sub_Controller_Name { get; set; }
          [Display(Name = "Action Name")]
        public string Module_Sub_Sub_Action_Name { get; set; }
        public int Module_Sub_Item_ID { get; set; }
        public Module_Sub_Item_Tbl Module_Sub_Item_Tbl { get; set; }
         [Display(Name = "Menu Order")]
        public int Module_Sub_Sub_Menu_Order { get; set; }
         public string menuCode { get; set; }

         public string Left_bg_Class { get; set; }

         //public int ApplicationID { get; set; }
         //public string AreaName { get; set; }
    }
}