using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
    public class RPDGang
    {
        public bool Status { get; set; }
        public string GangID { get; set; }
        public string GangName { get; set; }
        public string GangLeader { get; set; }
        public string GangLeaderEmail { get; set; }
        public string GangLeaderPhone { get; set; }
        public string Zone { get; set; }
        public string Feeder { get; set; }


        public string FeederName { get; set; }
    }

    public class IreportRCDCModel
    {


        public string StaffId { get; set; }

        public string FeederId { get; set; }

        public string AccountName { get; set; }

        public string Address { get; set; }

        public string DTR_Id { get; set; }

        public string Comments { get; set; }

        public string ReportCategory { get; set; }

        public string ReportSubCategory { get; set; }

        public string IreportersPhoneNo { get; set; }

        public string IReportersEmail { get; set; }

        public string UserId { get; set; }

        public string Zone { get; set; }

        public string CustomerName { get; set; }

        public string IreportersEmail { get; set; }

        public string Status { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string AccountNo { get; set; }

        //public string Latitude { get; set; }

        //public string Longitude { get; set; }
    }

    public class TariffModel
    {
        public string TariffID { get; set; }
        public string TariffCode { get; set; }
        public string Description { get; set; }
        public string TariffRate { get; set; }
    }


    public class NewCustomer
    {

        public string TicketNo { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string HouseNo { get; set; }

        public string StreetName { get; set; }

        public string CommunityName { get; set; }

        public string LandMark { get; set; }

        public string StaffId { get; set; }

        public string LGA { get; set; }

        public string State { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public string CustomerEmail { get; set; }

        public string OfficeEmail { get; set; }

        public string ZipCode { get; set; }

        public string TypeOfPremises { get; set; }

        public string OnboardCategory { get; set; }

        public string UseOfPremises { get; set; }

        public string Status { get; set; }

        public string ParentAccountNo { get; set; }

        public string MeterNo { get; set; }

        public string Occupation { get; set; }

        public string MDA { get; set; }

        public string MeansOfIdentification { get; set; }

        public string CustomerLoad { get; set; }

        public string NearbyAccountNo { get; set; }

        public string TypeOfMeterRequired { get; set; }

        public string FeederName { get; set; }

        public string Zone { get; set; }

        public string FeederId { get; set; }

        public string DTRName { get; set; }

        public string DTRCode { get; set; }

        public string BookCode { get; set; }

        public string UserId { get; set; }

        public string CapturedBy { get; set; }

        public DateTime? DateCaptured { get; set; }

        public string ApprovedBy_Feeder { get; set; }

        public DateTime? DateApproved_Feeder { get; set; }

        public string ApprovedBy_Zone { get; set; }

        public DateTime? DateApproved_Zone { get; set; }

        public string ApprovedBy_IAD { get; set; }

        public DateTime? DateApproved_IAD { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string filePaths { get; set; }

        public string Passport { get; set; }

        public string ApplicantsSignature { get; set; }
        public string DebulkingNumber { get; set; }
    }


    public class StaffSuggestions
    {
        [Key]
        public int RCDC_Ireport_Id { get; set; }
        public DateTime? DateCommented { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }

        public string Comments { get; set; }

        public string ModuleName { get; set; }

        public string StaffPhone { get; set; }
        public string StaffEmail { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        
    }
    public class RCDCModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        public string StaffName { get; set; }

        public string StaffId { get; set; }

        public string GangID { get; set; }

        public string GangName { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public string Message { get; set; }

        public string GangLeader { get; set; }

        public string GangLeaderPhone { get; set; }

        public string GangLeaderEmail { get; set; }

        public string Zone { get; set; }

        public string Feeder { get; set; }
         
        public object Modules { get; set; }

        public string Date { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string AccountNo { get; set; }

        public List<RCDC_Disconnection_Incidence> Incidence { get; set; }

        public string DisconnId { get; set; } public string Comments { get; set; }

        public string AverageBillReading { get; set; }

        public string AccountType { get; set; }

        public string UserId { get; set; }

        public string ReasonForDisconnectionId { get; set; }

        public string FeederName { get; set; }

        public string  FeederId { get; set; }

        public string LoadProfile { get; set; }

        public string DateOfLastPayment { get; set; }

        public string Phase { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string LGA_Id { get; set; }

        public string State_code { get; set; }

        public string ReportCategoryId { get; set; }

        public string State_Id { get; set; }

        public string Settlement_Period { get; set; }

        public string Settlement_Amount { get; set; }

        public string Settlement_Status { get; set; }

        public string  filePaths { get; set; }

        public string Flag { get; set; }
         
        public string ListOfAppliances { get; set; }

        public string Tariff { get; set; }

        public string Availability { get; set; }

        public string Duration { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string TariffRate { get; set; }

        public string StaffPhone { get; set; }

        public string ModuleName { get; set; }

        public string CustomerName { get; set; }

        public string IreportersPhoneNo { get; set; }

        public string IreportersEmail { get; set; }

        public string AccountName { get; set; }

        public string DTR_Id { get; set; }

        public string ReportCategory { get; set; }

        public string ReportSubCategory { get; set; }
        ////////////////////////////////////////////////
        public string TicketNo { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string HouseNo { get; set; }

        public string StreetName { get; set; }

        public string CommunityName { get; set; }

        public string LandMark { get; set; }

       

        public string LGA { get; set; }

        public string State { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        

        public string OfficeEmail { get; set; }

        public string ZipCode { get; set; }

        public string TypeOfPremises { get; set; }

        public string OnboardCategory { get; set; }

        public string UseOfPremises { get; set; }

        

        public string ParentAccountNo { get; set; }

        public string MeterNo { get; set; }

        public string Occupation { get; set; }

        public string MDA { get; set; }

        public string MeansOfIdentification { get; set; }

        public string CustomerLoad { get; set; }

        public string NearbyAccountNo { get; set; }

        public string TypeOfMeterRequired { get; set; }
         
        public string DTRName { get; set; }

        public string DTRCode { get; set; }

        public string BookCode { get; set; }

 

        public string CapturedBy { get; set; }

        public DateTime? DateCaptured { get; set; }

        public string ApprovedBy_Feeder { get; set; }

        public DateTime? DateApproved_Feeder { get; set; }

        public string ApprovedBy_Zone { get; set; }

        public DateTime? DateApproved_Zone { get; set; }

        public string ApprovedBy_IAD { get; set; }

        public DateTime? DateApproved_IAD { get; set; }

 

        public string Passport { get; set; }

        public string ApplicantsSignature { get; set; }
        public string DebulkingNumber { get; set; }

        public string DisconNoticeNo { get; set; }

        public string OldTariff { get; set; }

        public string NewTariff { get; set; }

        public string ReportCategoryName { get; set; }

        public string ReportSubCategoryName { get; set; }

        public string Amount { get; set; }

        public string PaymentTransId { get; set; }

        public string Token { get; set; }

        public string PaymentStatus { get; set; }

        public string IMEI { get; set; }

        public string DeviceIMEI { get; set; }
    }
}
