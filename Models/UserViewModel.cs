using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class UserViewModel
    {
        public ActionMessagePerformed ActionMessagePerformed { get; set; }

        public CUSTOMER customer { get; set; }
        public CustomerPaymentInfo PaymentInfo { get; set; }
        public List<STATE> StateList { get; set; }
        public List<LGA> LGAList { get; set; }
        public List<METER_REPAYMENT_PLAN> MeterRepaymentList { get; set; }
        public List<PaymentSchedule> PaymentScheduleList { get; set; }
    }
}