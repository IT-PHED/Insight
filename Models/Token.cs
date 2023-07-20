using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Token
    {
        public string ConsumerNo { get; set; }
        public string amount { get; set; }
        public string tokenDec { get; set; }
        public string LSTSESSION { get; set; }
        public string idRecord { get; set; }
        public string tariff { get; set; }
        public string subclass { get; set; }
        public string description { get; set; }
        public string vendTimeUnix { get; set; }
        public string unitsActual { get; set; }
        public string unitName { get; set; }
        public string valueActual { get; set; }
        public string tokenHex { get; set; }
        public string meterno { get; set; }
        public string sbmno { get; set; }
        public int iserror { get; set; }
        public string errormessage { get; set; }

        public string txCredit { get; set; }

        public string token { get; set; }


    }
}