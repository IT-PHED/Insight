using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class LastTransaction
    {


        public string LastAmount { get; set; }

        public DateTime? LastDatePaid { get; set; }

        public string Arrears { get; set; }

        public string AccountType { get; set; }
    }
    

    public class StaffBillPaymentData
    {
        [Key]
        public int SerialNo { get; set; }

        public string Staff_Id { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public DateTime? DatePaid { get; set; }

        public decimal? AmountPaid { get; set; }

        public decimal? Arrears { get; set; }


        public string Comments { get; set; }

        public DateTime? DateGenerated { get; set; }

        public string AccountNo { get; set; }
        public string AccountType { get; set; }
    }

    public class StaffPaymentDetails
    {


        public decimal? AmountPaid { get; set; }
        public DateTime? DatePaid { get; set; } 
        public string Status { get; set; }

        public string AccountType { get; set; }

        public string Arrears { get; set; }

        public string AccountNo { get; set; }
    }
    public class ZoneFeederMapping
    {


        public string Zone { get; set; }   
        public string Feeder { get; set; }



        public string CIN { get; set; }

        public string DTR_Name { get; set; }
    }
}