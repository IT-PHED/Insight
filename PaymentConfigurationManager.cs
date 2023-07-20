using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe
{
  
        public class PaymentConfigurationManager
        {
            public string tranx_amt { get; set; }

            public bool DemoMode { get; set; }

            public string mert_id { get; set; }

            public string DemoUrl { get; set; }

            public string LiveUrl { get; set; }

            public string cust_id { get; set; }

            public string DemoConfirmationServiceUrl { get; set; }

            public string LiveConfirmationServiceUrl { get; set; }

            public string tranx_curr { get; set; }

            public string cust_name { get; set; }

            public string tranx_id { get; set; }

            public string echo_data { get; set; }

            public string gway_first { get; set; }

            public string tranx_noti_url { get; set; }

            public string tranx_memo { get; set; }

            public string gway_name { get; set; }

            public string hash { get; set; }

            public string BankName { get; set; }

            public string ProductId { get; set; }

            public string ItemId { get; set; }

            public object Email { get; set; }

            public object ProductDescription { get; set; }

            public object PublicKey { get; set; }

            public string MAC { get; set; }
        }

        public class ApplicationConstants
        {

            public const string NairaCode = "566";
            public const string DollarCode = "840";
            public const string PoundCode = "211";
            public const string Francs = "002";

        }
   
}