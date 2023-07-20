using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{ 
        public class AuditTrail
        {
            [Key]
            public string StatusId { get; set; }
            public string StaffId { get; set; }
            public string ActivityName { get; set; }
            public DateTime DateTime { get; set; }
            public string InvoiceID { get; set; }

            public string ActivityType { get; set; }

            public string StaffName { get; set; }
        }
    
}