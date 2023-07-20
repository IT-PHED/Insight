using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MAP_View
    {
        public string CustomerId { get; set; } public string CustomerOtherName { get; set; } public string CustomerSurname { get; set; } public string Phone { get; set; } public string Email { get; set; } public string AccountNo { get; set; } public string IBC { get; set; }
        public string BSC { get; set; } public string MapProvider { get; set; } public string PaymentStatus { get; set; } public string AmountPaid { get; set; } public string ApplicationId { get; set; } public string MeterTypeId { get; set; } public string Quantity { get; set; }
        public string MeterInstallationAddress { get; set; } public string MeterAddress { get; set; } public string TypePremises { get; set; } public string PremiseUse { get; set; } public string MeterNo { get; set; } public string AccountType { get; set; }
        public string MAPMeterNo { get; set; }
        public string InstalledBy { get; set; }
        public string InstallationDate { get; set; }
        public string RegistrationDate { get; set; } public string PaymentDate { get; set; } public string Occupation { get; set; } public string IDcard { get; set; }
        public string IDcardNo { get; set; }
        public string MeterType { get; set; } public string ModeOfPayment { get; set; } public string PaymentPlan { get; set; } public string Passport { get; set; } public string BusStop { get; set; } public string HouseNo { get; set; }
        public string Landmark { get; set; }
        public string LGA { get; set; } public string State { get; set; } public string ApprovedBy { get; set; } public string DateApproved { get; set; } public string ApprovedStatus { get; set; } public string CustomerTitle { get; set; }
        public string Discriminator { get; set; }
        public string TransactionID { get; set; } public string CustomerName { get; set; } public string AlternateCustReference { get; set; } public string PaymentLogId { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; } public string PaymentReference { get; set; } public string TerminalID { get; set; } public string ChannelName { get; set; }
        public string Location { get; set; }
        public string InstitutionId { get; set; } public string PaymentCurrency { get; set; } public string DepositSlipNumber { get; set; } public string DepositorName { get; set; }

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
      
        public string IsReversal { get; set; }
        public string SettlementDate { get; set; }

        public string Teller { get; set; }
        public string CustomerReference { get; set; }
        public string DatePaid { get; set; }
        public string ResponseMessage { get; set; }
        public string CustomerEmail { get; set; }

        public string Token { get; set; }
        public string Units { get; set; }
        public string Tarriff { get; set; }
        public string merchantID { get; set; }
        public string Hash { get; set; } 
        public string CardType { get; set; }

        public string CardBrand { get; set; }
        public string TransactionProcessDate { get; set; }
        public string TokenDate { get; set; }   
        public string BRCPaymentStatus { get; set; }

        public string ArrearsPaymentStatus { get; set; } 
        public string BRCApprovedAmount { get; set; } 
        public string BRCApprovalCS { get; set; } 
        public string BRCApprovalCSComment { get; set; }

        public string BRC_ID { get; set; }
        public string BRCApprovalCSM { get; set; }
        public string BRCApprovalCSMComment { get; set; }
        public string MAPApplicationStatus { get; set; }

        public string BRCApprovalIBCHead { get; set; }
        public string BRCApprovalIBCHeadComment { get; set; }
        public string ArrearsPercentage { get; set; }
 
 

        public string ArrearsPercent { get; set; }
        public string Complaints { get; set; }
        public string BRC_Arrears { get; set; }
        public string MAPPaymentStatus { get; set; }
        public string BRCApprovalCSMAmount { get; set; }

        public string BRCApprovalCSAmount { get; set; }
        public string BRCApprovalIBCAmount { get; set; }
     

        public string MAPCustomerName { get; set; }
    }
}