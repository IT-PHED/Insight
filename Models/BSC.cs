using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
  
    public class BSC
    {
        [Key]
        public string BSCId { get; set; }
        public string BSCName { get; set; }
        public string IBCId { get; set; }
    }
}