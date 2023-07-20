using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{ 
    public class KYCReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string SERIAL { get; set; }
        public string CustomerName { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerMiddleName { get; set; }
        public string EmailStatus { get; set; } 
        public string SMSStatus { get; set; }


        public DateTime DateSent { get; set; }
    }


    public class KYCModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string SMS { get; set; }
        public string EmailCheck { get; set; }
        public string HardCopy { get; set; }
        public string Type { get; set; }
        public string BVN { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }

        public string EmailStatus { get; set; }

        public string SMSStatus { get; set; }

        public string BillId { get; set; }

        public string TENANT_PHONE { get; set; }

        public string TENANT_PHONE2 { get; set; }
    }



    public class MAPModel
    {
        public string CurrentCharges { get; set; }

        public string Arrears { get; set; }


        public string AccountNo { get; set; }

        public string MAPVendor { get; set; }

        public string ContractorID { get; set; }

        public string MeterNo { get; set; }

        public string TicketId { get; set; }

        public string SealNo1 { get; set; }

        public string SealNo2 { get; set; }

        public string InstallerId { get; set; }

        public string AmountPaid { get; set; }

        public string MAPType { get; set; }

        public string PoleNo { get; set; }

        public string StaffName { get; set; }

        public string MeterInstallationComment { get; set; }

        public string StaffId { get; set; }

        public string CustomerName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string InstallerName { get; set; }

        public string ContractorName { get; set; }

        public string filePaths { get; set; }

        public string TerminalSeal1 { get; set; }

        public string TerminalSeal2 { get; set; }

        public string UserId { get; set; }

        public string CapturedBy { get; set; }

        public string DTRCode { get; set; }

        public string DTRName { get; set; }

        public string FeederId { get; set; }

        public string FeederName { get; set; }

        public string ParentAccountNo { get; set; }

        public string MeterPhase { get; set; }

        public string Zone { get; set; }

        public string OnboardCategory { get; set; }

        public string Address { get; set; }

        public string AccountName { get; set; }

        public string CIN { get; set; }

        public string BVN { get; set; }

        public string PROG { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string IsReplaced { get; set; }

        public string OldMeterNo { get; set; }

        public string Band { get; set; }

        public string Perimeter { get; set; }

        public string PremiseId { get; set; }

        public string SerialNo { get; set; }

        public string ApprovalStatus { get; set; }

        public string ApprovalDate { get; set; }
    }

    public class EnergyBill
    {
        public string CurrentCharges { get; set; }

        public string Arrears { get; set; }


        public string Status { get; set; }

        public string TotalOutstanding { get; set; }
    }
    public class RCDC_Gang
    {
        public string GangName { get; set; }

        public string Zone { get; set; }

        public string Feeder { get; set; } 
        public string FeederName { get; set; }

        public string TeamLeadName { get; set; }

        public string TeamLeadEmail { get; set; }

        public string TeamLeadPhone { get; set; }

        public string TeamLeadStaff_ID { get; set; }

        public DateTime? DateAdded { get; set; }

        public string AddedBy { get; set; } 
        [Key]
        public string GangID { get; set; }
         
        public string Gang_ID { get; set; }
    }

    public class RCDC_DTR
    {
        [Key]
        public string DTR_Id { get; set; }
        public string FeederId { get; set; }
        public string FeederName { get; set; }
        public string DTRName { get; set; } 
    }

    public class RCDC_ReportCategory
    {
        [Key]
        public string ReportCategoryId { get; set; }
        public string ReportCategoryName { get; set; }
    }


    public class RCDC_OnboardCustomers
    {
        [Key]
        public int RCDC_Onboard_Id { get; set; }

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

        public DateTime? DateReported { get; set; }

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

        public string OldTariff { get; set; }

        public string NewTariff { get; set; }

        public string Address { get; set; }

        public string CustomerName { get; set; }
    }







    public class RCDC_Billinformation
    {

        public string BillId { get; set; }
        public string CustomerName { get; set; }

        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string DTR_Name { get; set; }
        public string DTR_Id { get; set; }
        public string AmountBilled { get; set; }

        public string Zone { get; set; }

        public string FeederName { get; set; }

        public string FeederId { get; set; }
    }
   public class RCDC_ReportSubCategory
    {
        [Key]
        public string ReportSubCategoryId { get; set; }
        public string ReportSubCategoryName { get; set; }
        public string ReportCategoryId { get; set; }
    }



    ////
    public class RCDC_DisconnectionList
    {
        [Key]
        public int SerialNo { get; set; }

        public string DisconID { get; set; }

        public string AccountNo { get; set; }

        public string Address { get; set; }

        public string CIN { get; set; }

        public string DTRCode { get; set; }

        public string FeederName { get; set; }

        public string Zone { get; set; }

        public string DTRExec { get; set; }

        public string Arrears { get; set; }

        public string LastPayDate { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string DisconReason { get; set; }

        public string DisconAmount { get; set; }

        public string DisconHistoryID { get; set; }

        public string DisconBy { get; set; }

        public string SettlementPlan_ID { get; set; }

        public DateTime? DateOfDiscon { get; set; }

        public DateTime? DatePaid { get; set; }

        public string AmountPaid { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public DateTime? DateGenerated { get; set; }

        public string GeneratedBy { get; set; }

        public string DisconStatus { get; set; }

        public string Gang_ID { get; set; }

        public string AccountType { get; set; }

        public string AccountName { get; set; }

        public string AvgConsumption { get; set; }

        public string DTR_Exec_Email { get; set; }

        public string DTR_Exec_Name { get; set; }

        public string DTR_Exec_Phone { get; set; }

        public string DTR_Name { get; set; }

        public string FeederId { get; set; }
        
        /////////////////////
        public string Phase { get; set; } 
        public string DTR_Id { get; set; } 
        public string Tariff { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string Settlement_Period { get; set; }

        public string Settlement_Amount { get; set; }

        public string Settlement_Status { get; set; }

        public string Settlement_Agreement { get; set; }

        public string ReconnectedBy { get; set; }

        public DateTime? DateReconnected { get; set; }

        public string RPDApproval { get; set; }

        public string IADApproval { get; set; }

        public string RPDCalculatedLoad { get; set; }

        public string RPDLossOfRevenueAvailabilty { get; set; }

        public int? RPDLossOfRevenueInfractionDuration { get; set; }

        public string RPDLossOfRevenueAmount { get; set; }

        public string RPDApprovalComments { get; set; }
        public string IADApprovalComments { get; set; }

        public DateTime? RPDApprovalDate { get; set; } 
        
        public DateTime? IADApprovalDate { get; set; }

        public string RPDApprovedBy { get; set; } public string IADApprovedBy { get; set; }

        public string TariffAmount { get; set; }

        public string ConsumerStatus { get; set; }

        public string DisconNoticeNo { get; set; }

        public string MeterNo { get; set; }

        public string Band { get; set; }
    }

    [Table("tbl_bd_update_new_dtrdetails", Schema = "ebuka")]
    public class tbl_bd_update_new_dtrdetails
    {

        [Key]
        public int sn { get; set; }

        public string dtrname { get; set; }

        public string feeder33name { get; set; }

        public string feeder11name { get; set; }

        public string staffid { get; set; }

        public string accountno { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }

        public DateTime? datecaptured { get; set; }

        public string capacity { get; set; }

        public string dtrid { get; set; }

        public string feeder11id { get; set; }

        public string feeder33id { get; set; }

        public string dtrexec { get; set; }

        public string zone { get; set; }

        public string updatedby { get; set; }

        public DateTime? dateupdated { get; set; }

        public string NewDtrName { get; set; }

        public string CurrentStatus { get; set; }

        public string EnuemeratedPOP { get; set; }

        public string CurrentPOP { get; set; }

    }


    //
    public class RCDCMember
    {
        [Key]
        public string MemberID { get; set; }
         
        public virtual RCDC_Gang Gang { get; set; }

        public string GangID { get; set; }
        
        public string Gang_ID { get; set; }

        public string StaffName { get; set; }

        public string StaffID { get; set; }

        public string StaffPhone { get; set; }

        public string StaffEmail { get; set; }

        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }

        public string AddedBy { get; set; }

    }
    public class RCDC_BillDistribution
    {
        [Key]
        public int RCDC_Ireport_Id { get; set; }
    }




    
    public class RCDC_AppVersion
    {
        [Key]
        public int RCDC_Ireport_Id { get; set; }

        public string Current_Version { get; set; }

        public string UserMessage { get; set; }

    }


    public class DOCUMENTS
    { 
        public string SENDER_ID { get; set; }

        public string DOCUMENT_NAME { get; set; }
        [Key]
        public string DOCUMENT_CODE { get; set; }
        public string DOCUMENT_EXTENSION { get; set; }

        public string DOCUMENT_PATH { get; set; }

        public DateTime? DATE_UPLOADED { get; set; }

        

        public string COMMENTS { get; set; }

        public string STATUS { get; set; }

       

        public string YOUTUBE { get; set; }
        public string REFERENCE_CODE { get; set; }

        public string DocumentDescription { get; set; }

        public string Size { get; set; }
    }

    public class RCDCReconnectionFee
    {
        [Key]
        public string ReconFeeID { get; set; }

        public string ReconFeeName { get; set; }

        public string Phase { get; set; }

        public string Amount { get; set; }

        public string Status { get; set; }

        public string DateAdded { get; set; }

        public string AddedBy { get; set; }


        public string AccountType { get; set; }
    }
    ///
    public class RCDCDTR_Exec
    {
        [Key]
        public string DTRExec_ID { get; set; }

        public string ExecName { get; set; }

        public string PhoneNo { get; set; }

        public string EmailID { get; set; }

        public string Zone { get; set; }

        public string FeederName { get; set; }

        public string Status { get; set; }

        public DateTime? DateAdded { get; set; }

        public string AddedBy { get; set; }

    }
    
     

    public class RCDC_LoadApplicances
    {
      

        public string AccountNo { get; set; }

        public string DisconId { get; set; }

        public string ApplianceName { get; set; }

        public string Qty { get; set; }

        public string TotalWattage { get; set; }
        
        [Key]
        public string LoadApplianceId { get; set; }

        public string ApplianceId { get; set; }
    }

    public class RCDCPaymentMaster
    {
        [Key]
        public string DisconID { get; set; }

        public string MasterID { get; set; }

        public string Date { get; set; }

        public string AccountNo { get; set; }

        public string Amount { get; set; }

        public DateTime? DateAdded { get; set; }

        public string AddedBy { get; set; }

    }

    //
    public class RCDCPaymentHistory
    {
        [Key]
        public string HistoryID { get; set; }

        public string AccountNo { get; set; } 
        public string IncidenceID { get; set; }

        public string Amount { get; set; }

        public string Description { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateAdded { get; set; }

        public string AddedBy { get; set; }

    }
   public class PaymentReceiptNumbers
    {
        public string RECEIPTNO { get; set; }
        public string DATETIME { get; set; } 
    }

    public class ProvisionalOutstanding
    {
        public string INCIDENCE { get; set; }
        public string PRI_OUT_CRE_COM { get; set; }

        public string Amount { get; set; }

        public string Inc_Date { get; set; }
    }

    public class KYCModels
    {
        public string AccountNo { get; set; }
        public bool Status { get; set; }
       
    }
    public class RCDCCustomerPayments
    {
        public string PaymentID { get; set; }
        public Double AmountPaid { get; set; }


        public DateTime? DatePaid { get; set; }
        public string PaymentDescription { get; set; }

        public string Token { get; set; }

        public string TarriffIndex { get; set; }

        public string PaymentChannel { get; set; }
    }

    public class RCDCCustomerIncidences
    {
        [Key]
        public string IncidenceID { get; set; }
        public Double AmountBilled { get; set; }
        public DateTime? DatePaid { get; set; }
        public string Status { get; set; }
        public string DisconID { get; set; } 
        public string AccountNo { get; set; }
        public string IncidenceDescription { get; set; }
    }



    public class RCDC_Incidence
    {   
        [Key]
        public string IncidenceId { get; set; }
        public string IncidenceName { get; set; } 
        public string IncidencePurposeGroup { get; set; }

        public string IncidenceDescription { get; set; }

        public string Priority { get; set; }

    }
       public class RCDC_Incidence_VM
    {   
        
        public string IncidenceId { get; set; }
        public string IncidenceName { get; set; }  
    }


       public class RCDC_Incidence_For_Reasons
       {
           [Key]
           public int IncidenceReasonId { get; set; }
           public string ReasonForDisconnectionId { get; set; }

           public virtual RCDC_Incidence Incidence { get; set; }
           public string IncidenceId { get; set; }
           public string PercentageToPay { get; set; } 
       }


  public class RCDC_Reasons_For_Disconnection
    {
      [Key]
        public string ReasonForDisconnectionId { get; set; }
        public string ReasonName { get; set; }  
    }





  public class RCDC_Ireport
    { 

        [Key]
        public int RCDC_Ireport_Id { get; set; } 
        public string Zone { get; set; }

        public string Feeder_Id { get; set; }

        public string AccountName { get; set; }

        public string Address { get; set; }

        public string DTR_Id { get; set; }

        public string Comments { get; set; }

        public string StaffId { get; set; }

        public string ReportCategory { get; set; }

        public string ReportSubCategory { get; set; }

 
      public string PhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime DateReported { get; set; }

        public string AccountNo { get; set; }

        public string CustomerName { get; set; }

        public string IreportersPhone { get; set; }

        public string Status { get; set; }

        public string IreportersEmail { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string filePaths { get; set; }

        public string UserId { get; set; }

        public string ReportCategoryName { get; set; }

        public string ReportSubCategoryName { get; set; } 
      public string CapturedBy { get; set; }
    }




  public class RCDC_Disconnection_Incidence
  {
      public string IncidenceId { get; set; }
      public string IncidenceAmount { get; set; }
      public string Percentpayment { get; set; }
      public bool CalculateAmount { get; set; }
      public int? DurationInDays { get; set; }
  }
    

    public class RCDC_Disconnection_Incidence_History
    {

        [Key]
        public string IncidenceDefaultId { get; set; }
        public string IncidenceId { get; set; }
        public string IncidenceAmount { get; set; }
        public string Percentpayment { get; set; }
        public string CalculateAmount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? DurationInDays { get; set; }
        public decimal? UnitChargePerday { get; set; }
        public string DisconnId { get; set; }
        public string Status { get; set; }
        public DateTime? DateDisconnected { get; set; }

        public string IncidenceName { get; set; }

        public string Settlement { get; set; }

        public string SettlementAmount { get; set; }
    }

    public class RCDC_SettlementPlan_Settings
    {
        [Key]
        public string Settlement_Plan_Name { get; set; }
        public string Settlement_Plan_Amount { get; set; }
    }


    public class RCDC_Settlement_History
    {

        [Key]
        public string Settlement_Id { get; set; }

        public string Settlement_Name { get; set; }

        public string Settlement_Month { get; set; }

        public string Settlement_Year { get; set; }

        public string Payment_Status { get; set; }

        public string Discon_Id { get; set; }

        public string Settlement_Amount { get; set; }

    }





    public class RCDC_Settlement_Duration
    {
        [Key]
        public string Settlement_Duration_Id { get; set; }
        public string Settlement_Duration_Name { get; set; }
    }

    public class RCDC_Spot_Billing
    {
        [Key]
        public string BillingId { get; set; }
        public string DisconnId { get; set; }
        public DateTime? BillingDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string NoofDaysBilled { get; set; }
        public string AvgConsumption { get; set; }
        public string BilledQty { get; set; }
        public string Status { get; set; }
    }



    public class RCDCCustomer
    {
        
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string CIN { get; set; }
        public string DTR_Name { get; set; }
        public string Zone { get; set; }
        public string Feeder { get; set; }
        public string DTR_Exec_Phone { get; set; }
        public string DTR_Exec_Name { get; set; }
        public string DTR_Exec_Email { get; set; }
        public string Avg_Consumption { get; set; }
        public string Arrears { get; set; }
        public DateTime LastDatePaid { get; set; } 
        public string DisconnectionStatus { get; set; } 
        public string AccountNo { get; set; }
        public List<RCDCCustomerPayments> PaymentHistory { get; set; }
        public List<RCDC_Spot_Billing> BillingHistory { get; set; }
        public List<RCDC_Disconnection_Incidence_History> IncidenceHistory { get; set; }


        public List<ProvisionalOutstanding> ProvisionalOutstanding { get; set; }

        public List<PaymentReceiptNumbers> ReceiptNoList { get; set; }
    }
}