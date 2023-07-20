using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MAP_METER_UPGRADE
    {
        [Key]
        public int SN { get; set; }

        public string MAP_CUSTOMER_NAME { get; set; }

        public string OLD_MAP_PLAN { get; set; }

        public string OLD_MAP_AMOUNT { get; set; }

        public string OLD_MAP_PHASE { get; set; }

        public string NEW_MAP_AMOUNT { get; set; }

        public string NEW_MAP_PLAN { get; set; }

        public string NEW_MAP_PHASE { get; set; }

        public DateTime? DATE_APPLIED { get; set; }

        public string ACCOUNT_NO { get; set; }

        public string TICKET_ID { get; set; }

        public string UPFRONT_AMOUNT { get; set; }

        public string MSC_AMOUNT { get; set; }

        public string PAYMENT_STATUS { get; set; }

    }

}