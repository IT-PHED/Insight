using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PHEDServe.Models
{
    public class AccessRight
    {
        [Key]
        public string name { get; set; }
        public string value { get; set; }
    }

   
}