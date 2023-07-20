using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ActionMessagePerformed
    {
        public string message { get; set; }
        public string type { get; set; }
        public string title { get; set; }

        public string PaymentType { get; set; }
    }
}