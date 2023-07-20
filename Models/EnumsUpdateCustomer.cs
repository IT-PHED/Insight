using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class EnumsUpdateCustomer
    {
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string GSM { get; set; }
        public string AccountType { get; set; }
        public string MaxDemand { get; set; }
        public string Tariff { get; set; }
        public string MeterNo { get; set; }
        public string TransformerId { get; set; }
        public string Upriser { get; set; }
        public string ServiceLTPole { get; set; }
        public string ServiceWire { get; set; }
        public string CustomerSn { get; set; }
        public float MeteringLat { get; set; }
        public float MeteringLon { get; set; }
        public string CapturedBy { get; set; }
    }
}