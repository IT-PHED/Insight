using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class AppViewModels
    {
        //public BusinessServiceCenter BSCList { get; set; }
        //public IntegratedServiceCenter IBCList { get; set; }
        //public List<Marketer> MarketerList { get; set; }
        //public List<TransmissionStations> TransmissionStationList { get; set; }
        //public List<TransmissionStation> ModifiedTransmissionStationList { get; set; }
        //public List<Feeders33VM> Feeder33List { get; set; }

        public List<CustomerPaymentInfo> PaymentList { get; set; }
        public CustomerPaymentInfo CustomerDetails { get; set; }
        public CUSTOMER CustomerDetailsFromCustomer { get; set; }
        public KYC KYC { get; set; }
        public List<UploadedFilesVM> UplodedStatusList { get; set; }
        public List<DirectPayments> DirecPaymentList { get; set; }
        public List<Complaint> ComplaintList { get; set; }
        public List<IBC> IBCList { get; set; }
        public List<BSC> BSCList { get; set; }
        public MAPPayment MAPPayment { get; set; }
        public BRCApprovals BRCApprovals { get; set; }
        public List<Models.MAPPayment> MAPPaymentList { get; set; }
        public List<AuditTrail> AuditTrailList { get; set; }
        public AMRReportsModel AMRdata { get; set; }

        public List<MeterList> MeterUploadApprovalList { get; set; }

        public List<DailyMeterReading> DailyReadingData { get; set; }

        //public List<MDCustomerData> MDCustomerDataList { get; set; }

        public MDCustomerData MDCustomerDataLists { get; set; }

        public List<DailyMeterReading> MonthlyUsage { get; set; }

        public List<ApplicationUser> ParentAccountList { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public List<CustomerAccounts> LinkedAccountList { get; set; }

        public string APIStatus { get; set; }

        public Models.ApplicationUser ParentAccountProfile { get; set; }

        public List<ComplaintCategory> ComplaintCategoryList { get; set; }

        public List<ComplaintsSubCategory> ComplaintSubCategoryList { get; set; }

        public List<CustomerTickets> CustomerTickets { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public List<Models.CustomerTickets> TicketList { get; set; }

        public string ClosedTickets { get; set; }

        public string ActiveTickets { get; set; }

        public string CompletedTickets { get; set; }

        public List<DeliveredBills> DelivererdBillsList { get; set; }

        public List<MAP_VENDOR> VendorList { get; set; }

        public List<MAP_CONTRACTOR> ContractorList { get; set; }

        public List<MAP_INSTALLER> InstallersList { get; set; }

        public List<StaffBasicData> UplodedStaffList { get; set; }

        public string TotalNumberOfStaff { get; set; }

        public string TotalNumberOfCompletedStaff { get; set; }

        public string NumberPaidThisMonth { get; set; }

        public string StaffPrepaid { get; set; }

        public string StaffPostpaid { get; set; }

        public string PercentageSuccess { get; set; }

        public string NoOfMDCustomers { get; set; }

        public string NoOfPostpaidCustomers { get; set; }

        public string NoOfMDEmailSent { get; set; }

        public string NoOfPOSTPAIDEmailSent { get; set; }

        public string PercentSuccessMD { get; set; }

        public string PercentSuccessPOSTPAID { get; set; }

        public List<RCDC_DisconnectionList> DisconnectionList { get; set; }

        public Controllers.MAPController.RCDCStatData RCDCStatData { get; set; }

        public List<DOCUMENTS> UploadedDocumentList { get; set; }

        public List<RCDC_LoadApplicances> LossOfRevenueList { get; set; }

        public RCDC_Gang GangModel { get; set; }

        public List<RCDC_Gang> GangList { get; set; }

        public List<RCDCMember> RCDCMemberList { get; set; }

        public RCDCMember GangMember { get; set; }

        public List<RCDC_OnboardCustomers> NewCustomerOnboardList { get; set; }

        public List<RCDC_BillDistribution> BillDistributionList { get; set; }

        public List<RCDC_ReportCategory> RCDC_ReportCategory { get; set; }

        public List<RCDC_ReportSubCategory> RCDC_ReportSubCategory { get; set; }

        public List<RCDC_Ireport> IReportList { get; set; }
    }
}
