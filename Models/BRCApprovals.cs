    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    namespace PHEDServe.Models
    {

        public class BRCApprovals
        {
            public string TicketId { get; set; }
             
            public string BRCAmount { get; set; }

            public string ApprovalComment { get; set; }

            public DateTime? Date { get; set; }

            public string PostedBy { get; set; }

            public string StaffName { get; set; } 

            public string ApprovalStatus { get; set; }

            public string BRCStatus { get; set; }

            public string ApprovedAmount { get; set; }
            [Key]
            public int SerialNo { get; set; }

        }
    }


