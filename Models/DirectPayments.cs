using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{


    public class DirectPayments
    {
        [Key]
        public string PaymentId { get; set; }

        public string PaymentDescription { get; set; } 
        public string Amount { get; set; } 
        public DateTime? Datepaid { get; set; } 
        public string Status { get; set; }
         
        public DateTime? DateClaimed { get; set; }

        public string DateClaimedBy { get; set; }
    }

    public class PaymentDetails
    {
        [Key]
        public string PaymentDetailId { get; set; }

        public string PaymentId { get; set; }
        public string AccountNumber { get; set; }

    }

  public class UploadedFilesVM
    {

        public string PaymentDetailId { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }


        public string DateUploaded { get; set; }

        public string TicketId { get; set; }

        public string CustomerName { get; set; }

        public string MeterNo { get; set; }

        public string MeterVendor { get; set; }

        public string Amount { get; set; }

        public object CustomerAddress { get; set; }

        public object Phase { get; set; }

        public object AccountNo { get; set; }

        public string ContractorName { get; set; }

        public string InstallerName { get; set; }

        public string StaffSurname { get; set; }

        public string OtherNames { get; set; }

        public string StaffId { get; set; }
    }
}