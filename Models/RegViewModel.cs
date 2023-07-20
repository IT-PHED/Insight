using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class RegViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public bool isLoggedIn { get; set; }

        public List<MaritalStatusTbl> MaritalStatusTbls { get; set; }
        public List<Sex> SexTbls { get; set; }

        public List<TitleTbl> TitleTbls { get; set; }

        public List<StaffBillPaymentData> StaffBillPaymentDataList { get; set; }

        public List<StaffBasicData> UplodedStaffList { get; set; }

        public string TotalNumberOfStaff { get; set; }

        public string TotalNumberOfCompletedStaff { get; set; }

        public string NumberPaidThisMonth { get; set; }

        public string StaffPrepaid { get; set; }

        public string StaffPostpaid { get; set; }

        public string PercentageSuccess { get; set; }
    }
}