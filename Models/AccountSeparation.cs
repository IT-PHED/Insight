using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class AccountSeparation
    {
        public string primaryaccount { get; set; } 
        public string requestbyname { get; set; } 
        public string requestbyid { get; set; } 
        public int noofseparation { get; set; } 
        public string requestdate { get; set; } 
        public string staffid { get; set; } 
        public string SeparationAccount { get; set; } 
        public string StaffName { get; set; } 
        public string StaffId { get; set; } 
        public string AccountNo { get; set; }

        public object timestamp { get; set; }

        public object Date { get; set; }

        public object CurrentTime { get; set; }

        public object FeederId { get; set; }
    }

    public class Category
    {
        public string secaccountno { get; set; }
        public string CategoryName { get; set; }
        public string primaryacctno { get; set; }
        public bool success { get; set; }
        public string message { get; internal set; }

        public string AccountNo { get; set; }
    }

    internal class GeneratedCIN
    { 
        public string CIN { get; set; }
    }

  internal class EnumerationDataVerification 
     {
         public string PremiseId { get; set; } 
         public string AccountNo { get; set; }

         public string CustomerName { get; set; }

         public string CustomerPhone { get; set; }

         public string FlatNo { get; set; }

         public string ConnectionType { get; set; }

         public string MeterType { get; set; }

         public string MeterNo { get; set; }

         public string TariffClass { get; set; }



         public string Address { get; set; }

         public string Longitude { get; set; }

         public string Latitude { get; set; }

         public string VerificationStatus { get; set; }
     }

     internal class EnumerationData
     {
         public string PremiseId { get; set; }
         public string NoOfAccounts { get; set; }
         public string CustomerCount { get; set; }

         public string AccountNo { get; set; }

         public string CustomerName { get; set; }

         public string CustomerPhone { get; set; }

         public string FlatNo { get; set; }

         public string ConnectionType { get; set; }

         public string MeterType { get; set; }

         public string MeterNo { get; set; }

         public string TariffClass { get; set; }

         public string SerialNo { get; set; }
     }

     internal class BaseEnumerationData
     {
         public string PremiseId { get; set; }
         public string NoOfAccounts { get; set; }
         public string CustomerCount { get; set; }
     }
   
    
    
    internal class AllPrimaryAccounts
    {
        public string sn { get; set; }
        public string primaryaccount { get; set; }
        public string requestdate { get; set; }
        public string requestbyname { get; set; }

        public string pending { get; internal set; }
    }

    public sealed class AllSubAcct
    {
        public int sn { get; set; }
        public string primaryact { get; set; }
        public string secact { get; internal set; }

        public string Status { get; set; }
    }


    public class tbl_map_accountseparation_secoderyaccounts
    {
        [Key]
        public int sn { get; set; }

        public string primaryaccounts { get; set; }

        public string secondaryaccount { get; set; }

        public string aprovedbyid { get; set; }

        public string approvedbyname { get; set; }

        public DateTime? approveddate { get; set; }

        public DateTime? timestamp { get; set; }

        public string Status { get; set; }

    }

}