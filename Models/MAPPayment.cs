using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MAPPayment
    {
        public MAPPayment()
        {
            this.PaymentId = Guid.NewGuid().ToString();
        }
        public string TicketId { get; set; }
        [Key]
        public string PaymentId { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string Amount { get; set; }
        public string DatePaid { get; set; }
        public string PaymentStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string TransRef { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovalComment { get; set; }
        public string ReceiptNo { get; set; }
        public string CustomerName { get; set; }
        public string Phase { get; set; }
        public string AccountNo { get; set; }
        public string DocumentPath { get; set; }
        public string TellerNo { get; set; }

        public string PaymentFor { get; set; }

        public string CheckStatus = "false";

        public string IBC_OLD { get; set; }

        public string BSC_OLD { get; set; }

        public string CIN { get; set; }

        public string DTR_NAME { get; set; }
    }

    }
