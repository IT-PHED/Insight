using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class enumCustomerValidation
    {
        public string Status { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string LandLordName { get; set; }
        public int PPM { get; set; }
        public string CorrectAddress {  get; set; }
        public string BuildType { get; set; }
        public string CustomerClass { get; set; }
        public int Upriser { get; set; }
        public int PoleNo { get; set; }
        public int FeederId { get; set; }
        public int DTRId { get; set; }
        public int userId { get; set; }
        public string remarks { get; set; }
        public string Infraction { get; set; }
        public int NoofRooms { get; set; }
        public int NoofNew { get; set;}
        public string timeStampDate { get; set; }
        public string DateCaptured { get; set;}
        public int noofSeperation { get; set; }
        public string seperationforBilling { get; set; }
        public string Recommendation { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}