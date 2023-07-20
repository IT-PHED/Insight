using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class METER_REPAYMENT_PLAN
    {
        [Key]
        public int SN { get; set; }
        public string Repayment_Plan_Name { get; set; }

        public string Repayment_Plan_Amount { get; set; }

        public string Repayment_Plan_Phase { get; set; }

        public string Repayment_MAP_Plan { get; set; }

    }
}