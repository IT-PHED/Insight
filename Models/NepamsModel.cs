using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

    public class TransmissionStations
    {
        [Key]
        public int StationID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
    }


    public class Transformers
    {
        public string TransformerName { get; set; }
        public int TransformerID { get; set; }
    }
    public class TransmissionStation
    {
        public string TransmissionStationName { get; set; }
        public int TransmissionStationID { get; set; }
    }


    public class Feeders33VM
    {
        public int FeederID { get; set; }
        public int TransmissionStationID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public string RouteLenght { get; set; }
        public string FeederVoltlevel { get; set; }
        public string PanelCTR { get; set; }
        public string MeterNo { get; set; }
        public int? FeederOrder { get; set; }
        public string Reading { get; set; }
    }

    public class Feeders33
    {
        [Key]
        public int FeederID { get; set; }
        public int TransmissionStationID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public string RouteLenght { get; set; }
        public string FeederVoltlevel { get; set; }
        public string PanelCTR { get; set; }
        public string MeterNo { get; set; }
        public int? FeederOrder { get; set; }
        public string Reading { get; set; }
    }

    public class Feeders33ViewModel
    {
        [Key]
        public int FeederID { get; set; }
        public int TransmissionStationID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public string RouteLenght { get; set; }
        public string FeederVoltlevel { get; set; }
        public string PanelCTR { get; set; }
        public string MeterNo { get; set; }
        public int? FeederOrder { get; set; }
        public int? Reading { get; set; }
    }

    public class NationalGridEnergy
    {
        public NationalGridEnergy()
        {
            var AddressBookIdGuid = Guid.NewGuid();
            NationalGridID = AddressBookIdGuid.ToString();

        }


        [Key]
        public string NationalGridID { get; set; }
        public string FeederID { get; set; }
        public string FeederName { get; set; }
        public string FeederVoltLevel { get; set; }
        public string MeterNo { get; set; }
        public string TransmissionStation { get; set; }
        public string TransmissionStationID { get; set; }
        public string TransformerCapacity { get; set; }
        public string PanelCTR { get; set; }
        public DateTime Date { get; set; }
        public string ReadingValue { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

    }

}