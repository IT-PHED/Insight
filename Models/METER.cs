
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class METER
    {
        [Key]
        public string MeterId { get; set; }

        public string MeterNumber { get; set; }

        public string AccountNumber { get; set; }

        public DateTime? DateInstalled { get; set; }

        public string InstalledBy { get; set; }

        public string Type { get; set; }

        public string Phase { get; set; }

        public string BatchId { get; set; }

        public string BatchName { get; set; }

    }
}
