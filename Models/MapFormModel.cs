using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MapForm
    {


        public string ApplicationID { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string HouseNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string BusStop { get; set; }
        public string LandMark { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string OtherPremise { get; set; }
        public string UseOfPremise { get; set; }
        public string ExistingPole { get; set; }
        public string ConditionOfPole { get; set; }
        public string ConditionOfCircuit { get; set; }
        public string ExistingInternalWiringCondition { get; set; }
        public string ExistingOutsideWiringCondition { get; set; }
        public string MeterType { get; set; }
        public string SeperationNeeded { get; set; }
        public string HouseOk { get; set; }
        public string NetworkOk { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StaffID { get; set; }
        public string CapturedBy { get; set; }
        public string Appliances { get; set; }
        public string Zone { get; set; }
        public DateTime? CapturedDate { get; set; }
    }


}