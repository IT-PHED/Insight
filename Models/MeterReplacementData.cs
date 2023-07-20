using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{


    public class MscPlan
    {
        public int checkMap { get; set; }
        public string planID { get; set; }
        public string cosumerNo { get; set; }
        public string meterNo { get; set; }
        public string comDate { get; set; }
        public string planDesc { get; set; }
        public string principalAmount { get; set; }
        public string interestRate { get; set; }
        public string noOfInstallment { get; set; }
        public string interestAmount { get; set; }
        public string totalAmount { get; set; }
        public string installmentAmount { get; set; }
        public string unm { get; set; }
    }
        public class MeterReplacementData
        {
            public string ticketnumber { get; set; }
            public string phase { get; set; }
            public string devicebrand { get; set; }
            public string manufacturername { get; set; }
            public string devicebrandtype { get; set; }
            public string meterdeviceserialnumber { get; set; }
            public string ctr { get; set; }
            public string ptr { get; set; }
            //public string meterowner{ get; set; }
            public string meterinstalleddate { get; set; }
            public string migir { get; set; }
            public string createdby { get; set; }
            public string createddatetime { get; set; }
            public string meterdigits { get; set; }
            public string metersealnumber1 { get; set; }
            public string meterseal_no2 { get; set; }
            public string terminalsealno1 { get; set; }
            public string terminalsealno2 { get; set; }
            public string meteris { get; set; }
            public string consumerno { get; set; }
            public string oldkwh { get; set; }
            public string meterstatus { get; set; }
            public string oldmeterstatus { get; set; }
            public string orderno { get; set; }
            public string orderdate { get; set; }
            public string meterreplacement { get; set; }
            //public string meterrentflag{ get; set; }
            public string remaininginstallmentsofmr { get; set; }
            public string totalinstallmentsofmr { get; set; }
            public string mfnum { get; set; }
            public string oldmetermanufacturer { get; set; }
            public string oldmeterinstallationdate { get; set; }
            public string oldmeteris { get; set; }
            //public string oldmeterowner{ get; set; }
            public string oldmeterno { get; set; }
            public string oldmeterdigit { get; set; }
            public string oldmetertype { get; set; }
            public string reason { get; set; }
            public string oldmtrownership { get; set; }
            public string mtrownership { get; set; }
        }
    }
