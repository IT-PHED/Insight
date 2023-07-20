using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
  

    public class STATE
    {
        [Key]
        public string STATE_NAME { get; set; }
        public string STATE_CODE { get; set; }
       
        
    }

     

    public class LGA
    {
        [Key]
        public string LGA_CODE { get; set; }
        public string LGA_NAME { get; set; }
       
        public string STATE_CODE { get; set; }
    }

}