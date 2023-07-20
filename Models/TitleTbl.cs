using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class TitleTbl
    {
         public TitleTbl()
        {
            var TitleTblGuid = Guid.NewGuid();
            TitleId = TitleTblGuid.ToString();

        }
        [Key]
        public string TitleId { get; set; }
        public string TitleName { get; set; }
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
