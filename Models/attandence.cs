using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class attandence
    {
        public string StaffId { get; set; }
        public string LogTime { get; set; }
        public string CapturedImagePath { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}