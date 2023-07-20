using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class AMRFeederDataFakerDT0
    {
        public List<AMRFeederDataFaker> loadAmrFeederDataFaker { get; set; }
    }


    public class AMRFeederDataFaker
    {
        public string InjSubname { get; set; }
        public string Feeder11name { get; set; }
        public string Bands { get; set; }
        public string METER_NO { get; set; }
        public DateTime RECORDDATE { get; set; }
        public float I { get; set; }
        public float V { get; set; }
        public float MW { get; set; }
        public int feeder33id { get; set; }
        public int feeder11id { get; set; }
        public int InjSubId { get; set; }
        public float power_factor { get; set; }
        public int Frequency { get; set; }
    }
}