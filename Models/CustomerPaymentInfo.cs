using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

    public class CustomerPaymentInfo
    {
        [Key]
        public string TransactionID { get; set; }
        public string CustomerName { get; set; }
        public string AlternateCustReference { get; set; }
        public string PaymentLogId { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public string TerminalID { get; set; }
        public string ChannelName { get; set; }
        public string Location { get; set; }
        public string InstitutionId { get; set; }
        public string PaymentCurrency { get; set; }
        public string DepositSlipNumber { get; set; }
        public string DepositorName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string BankCode { get; set; }
        public string CollectionsAccount { get; set; }
        public string ReceiptNo { get; set; }
        public string OtherCustomerInfo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string InstitutionName { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string IsReversal { get; set; }
        public string SettlementDate { get; set; }
        public string Teller { get; set; }
        public string CustomerReference { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime? BRCDate { get; set; }
        public string ResponseMessage { get; set; }
        public string CustomerEmail { get; set; }
        public string Token { get; set; }
        public string Units { get; set; }
        public string Tarriff { get; set; }
        public string Complaints { get; set; }
        public string merchantID { get; set; }
        public string Hash { get; set; }
        public string CardType { get; set; }
        public string CardBrand { get; set; }
        public string TransactionProcessDate { get; set; }
        public string TokenDate { get; set; }
        public string AccountType { get; set; }
        public string BRC_ID { get; set; }
        public string BRCPaymentStatus { get; set; }
        public string ArrearsPaymentStatus { get; set; }
        public string BRCApprovedAmount { get; set; }
        public string BRCApprovalCS { get; set; }
        public string BRCApprovalCSComment { get; set; }

        public string BRCApprovalCSM { get; set; }

        public string BRCApprovalCSMComment { get; set; }

        public string MAPApplicationStatus { get; set; }

        public string BRCApprovalIBCHead { get; set; }
        public string BRCStatus { get; set; } 
        public string BRCApprovalIBCHeadComment { get; set; } 
        public string ArrearsPercent { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string MeterPhase { get; set; }
        public string BRCApprovalCSMAmount { get; set; }
        public string BRCApprovalCSAmount { get; set; }
        public string BRCApprovalIBCAmount { get; set; } 
        public string BRCApprovedBy { get; set; }
        /////////////////////////////////////////////////////////////
        public string BRC_Arrears { get; set; }
        public string MAPPaymentStatus { get; set; }
        public string MAPCustomerName { get; set; }
         
        /////////////////////////////////////////////////////////////////////////////// 
        public string IDcardNo { get; set; } 
        public string MAPPlan { get; set; } 
        //New Fields
        public string MAPVendor { get; set; }
        public DateTime? DateCaptured { get; set; }
        public string CapturedBy { get; set; }
        public string InstalledBy { get; set; }
        public string MeterInstalaltionComment { get; set; }
        public string InstalledMeterNo { get; set; }
        public string PoleNo { get; set; }
        public string MeterSeal2 { get; set; }
        public string MeterSeal1 { get; set; }


        public string CapturedByName { get; set; }

     //   public string IdentityCardNumber { get; set; }

        public string IBC_OLD { get; set; }

        public string BSC_OLD { get; set; }

        public string CIN { get; set; }

        public string DTR_NAME { get; set; }

        public string MAPAmount { get; set; }

        public DateTime? BRCApprovalIBCDate { get; set; }

        public string AmountToPayMSC { get; set; }

        public string AmountToPayUpfront { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string InstallerId { get; set; }

        public string ContractorName { get; set; }

        public string ContactorId { get; set; }

        public string filePaths { get; set; }

        public string UpgradeStatus { get; set; }

        public string UpgradeAmount { get; set; }

        public DateTime? UpgradeDate { get; set; }

        public string UpgradePaymentStatus { get; set; }

        public string MAPPlanTotalAmount { get; set; }

        public string MAPPlanId { get; set; }

        public DateTime? MAPApplicationDate { get; set; }
    } 
}