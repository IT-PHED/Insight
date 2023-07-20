using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class AMRFeederDataDTO
    {
        public List<AMRFeederData> loadAmrFeederData { get; set; }
    }

    public class AMRFeederData
    {
        public string InjSubname { get; set; }
        public string Feeder11name { get; set; }
        public string Bands { get; set; }
        public string METER_NO { get; set; }
        public string RECORDDATE { get; set; }
        public string I { get; set; }
        public string V { get; set; }
        public string MW { get; set; }
        public string feeder33id { get; set; }
        public string feeder11id { get; set; }
        public string InjSubId { get; set; }
        public string power_factor { get; set; }
        public string Frequency { get; set; }

        public string feeder33name { get; set; }

        public string getdateval { get; set; }

        public string IA { get; set; }

        public string IB { get; set; }

        public string IC { get; set; }

        public string VA { get; set; }

        public string VC { get; set; }

        public string VB { get; set; }

        public string getdateval1 { get; set; }
    }
}