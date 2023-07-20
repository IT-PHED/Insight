using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class IBC
    {
        [Key]
        public string IBCId { get; set; }
        public string IBCName { get; set; }
    }
}