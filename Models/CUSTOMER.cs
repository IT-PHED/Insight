
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class CUSTOMER
    {
        public CUSTOMER()
        {
            var key = Guid.NewGuid();
            this.CustomerId = key.ToString();
        }
        [Key]
        public string CustomerId { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerSurname { get; set; }
        //public string CustomerOtherName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AccountNo { get; set; }
        //public string ApplicationId { get; set; }
        //public string MeterTypeId { get; set; }
        //public string Quantity { get; set; }
        public string CustomerAddress { get; set; }
        public string MeterInstallationAddress { get; set; }
        public string TypePremises { get; set; }
        public string PremiseUse { get; set; }
        public string MeterNo { get; set; }

        //public string AccountType { get; set; }
        //public string RegistrationDate { get; set; }

        public string ModeOfPayment { get; set; }
        public string PaymentPlan { get; set; }
        public string Occupation { get; set; }
        public string IDcard { get; set; }
        public string IDcardNo { get; set; }
        //public string IDcardNo { get; set; }
        public string MeterType { get; set; }
        // public string Passport { get; set; }
        public string HouseNo { get; set; }
        public string BusStop { get; set; }
        public string Landmark { get; set; }
        public string LGA { get; set; }
        public string State { get; set; }
        public string TransactionID { get; set; }
        public string Occupant { get; set; }
        public DateTime? RegistrationDate { get; set; } 
        //public string IdentityCardNumber { get; set; }

        public string MAPAmount { get; set; }

        public string MeterAmount { get; set; }
    }
    public class ADVANCECUSTOMER : CUSTOMER
    {
        public DateTime? PaymentDate { get; set; }
        public string MAPMeterNo { get; set; }
        public string MapProvider { get; set; }
        public string PaymentStatus { get; set; }
        public string AmountPaid { get; set; }
        public string InstalledBy { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string ModeOfPayment { get; set; }
        public string PaymentPlan { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string ApprovedStatus { get; set; }

    }
}