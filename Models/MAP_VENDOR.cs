

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MAP_VENDOR
    {
        
        public string ProviderName { get; set; }
        [Key]
        public string ProviderId { get; set; }

    }
}
