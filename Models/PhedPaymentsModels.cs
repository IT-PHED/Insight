using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{



    public class PaymentAdmin_VW
    {

        [Key]
        public string DistrictName { get; set; }
        public string ServiceCenterName { get; set; }
        public string MDOrNonMD { get; set; }
        public string BookNo { get; set; }
        public string Marketer { get; set; }
        public string CustomerReference { get; set; }
        public string Amount { get; set; }
        public string AlternateCustReference { get; set; }


        public string PaymentReference { get; set; }
        public string PaymentMethod { get; set; }

        public string ChannelName { get; set; }
        public string DatePaid { get; set; }

        public string ResponseMessage { get; set; }
        public string PaymentStatus { get; set; }


        public string DistrictID { get; set; }
        public string ServiceCenterID { get; set; }
        public string ZoneName { get; set; }
        public string StaffID { get; set; }
        public string Token { get; set; }
        public string Units { get; set; }
        public string CustomerName { get; set; }
        public string PaymentLogId { get; set; }

        public string OtherCustomerInfo { get; set; }


    }









    public class DistributionGroup
    {
        [Key]
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }

    public class IntegratedServiceCenter
    {
        [Key]
        public string IBCID { get; set; }
        public string IBCName { get; set; }
        public string GroupID { get; set; }
    }

   

   
    

    public class BusinessServiceCenter
    {
        [Key]
        public string BSCID { get; set; }
        public string BSCName { get; set; }
    }

    public class Marketer
    {
        [Key]
        public string MarketerID { get; set; }
        public string MarketerName { get; set; }
    }

    public class BookInfo
    {
        public long BookNo { get; set; }

        public string ZoneName { get; set; }

        public string DistrictName { get; set; }

        public int DistrictID { get; set; }

        public string ServiceCenterName { get; set; }

        public int ServiceCenterID { get; set; }

        public string MDOrNonMD { get; set; }

        public int? StaffID { get; set; }

        public string Marketer { get; set; }

    }

}