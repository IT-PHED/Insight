using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MaritalStatusTbl
    {
        public MaritalStatusTbl()
        {
            var MaritalStatusIdGuid = Guid.NewGuid();
            MaritalStatusId = MaritalStatusIdGuid.ToString();

        }
        [Key]
        public string MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }
        public string BranchCode { get; set; }
        public string SubscriberId { get; set; }
        public string AdminId { get; set; }
        public string SubAdminId { get; set; }
        public string SecondSubAdminId { get; set; }
        public string ThirdSubAdminId { get; set; }
        public string FourthSubAdminId { get; set; }
        public string FifthSubAdminId { get; set; }
        public string SixthSubAdminId { get; set; }
    }
}
