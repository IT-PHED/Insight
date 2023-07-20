using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MassMeterCapture
    {
        [Key]
        public string MeterCaptureId { get; set; }

        public string StaffName { get; set; }

        public string StaffId { get; set; }

        public DateTime? DateCaptured { get; set; }

        public string MeterNo { get; set; }

        public string Address { get; set; }

        public string AccountNo { get; set; }

        public string AccountName { get; set; }

        public string Zone { get; set; }

        public string FeederId { get; set; }

        public string FeederName { get; set; }

        public string DTRName { get; set; }

        public string CIN { get; set; }

        public string DTRId { get; set; }

        public string SealNo1 { get; set; }

        public string SealNo2 { get; set; }

        public string InstallerName { get; set; }

        public string ContractorName { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string MeterPhase { get; set; }

        public string FilePath { get; set; }


        public string MAPVendor { get; set; }

        //public string BVN { get; set; }

        //public string PROG { get; set; }

        public string BVN { get; set; }

        public string PROG { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string OldMeterNo { get; set; }

        public string IsReplaced { get; set; }

        public string Band { get; set; }
    }

}