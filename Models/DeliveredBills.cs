using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

    public class DeliveredEbills
    {
        [Key]
        public int SERIAL { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string EMAIL_STATUS { get; set; }
        public string SMS_STATUS { get; set; }
        public string E_MAIL { get; set; }
        public string ACCOUNT_STATUS { get; set; }
        public DateTime? DATE_SENT { get; set; }
        public string MONTH { get; set; }
        public string YEAR { get; set; }
        public string COMMENT { get; set; }
    }


  
    public class DeliveredBills
    {
        public string AccountName
        {
            get;
            set;
        }

        public string AccountNo
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Amount
        {
            get;
            set;
        }

        public string Arrears
        {
            get;
            set;
        }

        public string Current_Amount
        {
            get;
            set;
        }

        public DateTime? DateSent
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Tarrif_Code
        {
            get;
            set;
        }

        public string TotalBill
        {
            get;
            set;
        }

       
    }
}