using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Module_Tbl
    {
        public Module_Tbl()
        {
            var NewGuid = Guid.NewGuid();
            Module_ID = NewGuid.ToString();
        }

       
        [Key]
        public string Module_ID { get; set; }
        [Display (Name = "Module Name")]
        public string Module_Name { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
         [Display(Name = "Module Menu Order")]
        public int Module_Menu_Order { get; set; }

         public string menuCode { get; set; }

         public string Left_bg_Class { get; set; }

         //public int ApplicationID { get; set; }

         
    }
}