using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class RCDC_Faulty_Transformer
    {
        [Key]
        public int RCDC_FaultyTransformer_Id { get; set; }
        public string Zone { get; set; }

        public string Feeder_Id { get; set; }

        public string AccountName { get; set; }

        public string DTR_Address { get; set; }

        public string DTR_Id { get; set; }

        public string Comments { get; set; }

        public string StaffId { get; set; }

        public string ReportCategory { get; set; }

        public string ReportSubCategory { get; set; }


        public string DTR_Load { get; set; }

        public DateTime DateReported { get; set; }

        public string AccountNo { get; set; }

        public string CustomerName { get; set; }

        public string IreportersPhone { get; set; }

        public string IreportersEmail { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string filePaths { get; set; }

        public string UserId { get; set; }

        public string ReportCategoryName { get; set; }

        public string ReportSubCategoryName { get; set; }
        public string CapturedBy { get; set; }
    }
}