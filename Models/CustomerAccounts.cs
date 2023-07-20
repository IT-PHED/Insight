using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

    public class DTRExecutives
    {

        public string DTRExecutiveName { get; set; }
        public string DTRExecutivePhone { get; set; } 
        public bool Status { get; set; }

        public string DTRExecutiveEmail { get; set; }
    }



    public class CustomerAccounts
    {
        [Key]
        public int SerialNo { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAPIKey { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string MeterNo { get; set; }
        public string StatusType { get; set; }

    }

}