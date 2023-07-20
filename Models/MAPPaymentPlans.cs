using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

   public class MAPPaymentPlans
    {
        public string PaymentScheduleName { get; set; }

        public string PaymentScheduleValue { get; set; }

        public string TOTAL_AMOUNT { get; set; }
    }

   public class PaymentSchedule
   {
    
       [Key]
       public string PaymentScheduleValue { get; set; }

       public string TOTAL_AMOUNT { get; set; } 
       public string PaymentScheduleName { get; set; }
   }


 


}