
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class METER_TYPE
    {
        [Key]
        public string MeterTypeId { get; set; }

        public string TypeName { get; set; }

    }
}