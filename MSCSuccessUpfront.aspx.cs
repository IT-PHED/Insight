using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHEDServe
{ 
    public partial class MSCSuccessUpfront : System.Web.UI.Page
    {
        protected PaymentConfigurationManager ConfigSource { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public static string _MerchantID = ConfigurationManager.AppSettings["XpressPayMerchantID"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            string _ThreePhaseAmount = ConfigurationManager.AppSettings["ThreePhaseAmount"].ToString();
            string _SinglePhaseAmount = ConfigurationManager.AppSettings["SinglePhaseAmount"].ToString();

            decimal ThreePhaseAmount = Convert.ToDecimal(_ThreePhaseAmount);
            decimal SinglePhaseAmount = Convert.ToDecimal(_SinglePhaseAmount);

            string TicketId = Request.QueryString["TicketId"].ToString();
            string PaymentType = Request.QueryString["Type"].ToString();
            
            //get the Details of the Guy
             
            CustomerPaymentInfo Details = (from p in db.CustomerPaymentInfos
                                           where p.TransactionID == TicketId
                                           select p).SingleOrDefault();
            decimal divide = 0;

            if (Details.MAPPlan.Trim() == "50UPFRONT")
            {
                divide = 2;
            }
            if (Details.MAPPlan.Trim() == "75UPFRONT")
            {
                divide = 1.33333M;
            }
            if (Details.MAPPlan.Trim() == "25UPFRONT")
            {
                divide = 4;
            }
            if (Details.MAPPlan.Trim() == "100UPFRONT")
            {
                divide = 1;
            }
            if (Details.MAPPlan == "0UPFRONT")
            {
                Response.Redirect("~/MSCSuccess.aspx?TicketId=" + TicketId + "&Type=MSC");
            }


            if (Details != null)
            {



                if (PaymentType == "OFFICEREPRINT")
                {

                }

                if (PaymentType == "UPGRADE")
                { 

                    //Get the Upgrade Amount from the 


                    var upgrade = db.MAP_METER_UPGRADEs.FirstOrDefault(p => p.TICKET_ID == TicketId);


                    if(upgrade != null)
                    {

                        //get the New amount to be paid for

                        ThreePhaseAmount = Convert.ToDecimal( upgrade.UPFRONT_AMOUNT);

                    }

                    TicketId45.Value = Details.TransactionID;
                   // MapTicketIdApplied.InnerText = Details.TransactionID;
                    MAPTicketId.InnerText = Details.TransactionID;
                   // MAPApplicantsName.InnerText = Details.MAPCustomerName;
                   // MAPMeterPhase.InnerText = Details.MeterPhase;
                    //ApplicantsName1.InnerText = Details.MAPCustomerName;
                    //MAPMeterType1.InnerText = Details.MeterPhase;
                     
                    //show upfront
                    MapApplicationPayment.Visible = true;
                    MapApplicationSuccess.Visible = false;
                    //Do all the HASHING HERE AND 
                    string trans_id = RandomPassword.Generate(11).ToString();
                    string ProductID = "101";
                     
                    string Amount = "50";
                    //  string Amount = ThreePhaseAmount;
                    string CustomerName = Details.MAPCustomerName;
                    
                      //  Amount = SinglePhaseAmount.ToString();
                        MAPAmount.InnerText = ThreePhaseAmount.ToString();
                   
                    /// Amount = "100";
                    string TransactionId = Details.TransactionID;
                    MAPTransRef.InnerText = trans_id;

                    string CustomerEmail = Details.CustomerEmail;
                    string Currency = "NGN";
                    string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
                    string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
                    string ProductDescription = "MAP Meter Upgrade Payment";
                    string ProductId = "78";
                    string CallbackURL = "map.nepamsonline.com/successPage.aspx";
                    string MerchantID = _MerchantID;

                    StringBuilder hashString = new StringBuilder();

                    hashString.Append("amount=" + Amount + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                        "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                        + "&trans-id=" + trans_id);

                    //Save the Customers Details to the Database 
                    string HashCode = CreateHash(hashString.ToString(), Key);

                    ConfigSource = new PaymentConfigurationManager();
                    ConfigSource.tranx_amt = Amount;
                    ConfigSource.DemoMode = true;
                    ConfigSource.Email = CustomerEmail;
                    ConfigSource.mert_id = MerchantID;
                    ConfigSource.tranx_curr = "NGN";
                    ConfigSource.cust_id = TransactionId;
                    ConfigSource.cust_name = CustomerName;
                    ConfigSource.DemoConfirmationServiceUrl = "";
                    ConfigSource.LiveConfirmationServiceUrl = "";
                    ConfigSource.tranx_id = trans_id;
                    ConfigSource.echo_data = "";
                    ConfigSource.gway_first = "No";
                    ConfigSource.tranx_noti_url = CallbackURL;
                    ConfigSource.tranx_memo = ProductDescription;
                    ConfigSource.gway_name = "webpay";
                    ConfigSource.hash = HashCode;
                    ConfigSource.BankName = "";
                    ConfigSource.ProductId = ProductId;
                    ConfigSource.ItemId = "";
                    ConfigSource.MAC = PublicKey;

                    //save to the MAPPayment Table 
                    MAPPayment pw = new MAPPayment();
                    pw.PaymentFor = "METER UPGRADE";
                    pw.TicketId = TransactionId;
                    pw.TransRef = trans_id;
                    pw.AccountNo = Details.CustomerReference;
                    pw.Amount = Amount;
                    pw.BSC = Details.BSC;
                    pw.IBC = Details.IBC;
                    pw.PaymentMode = "WEB";
                    pw.PaymentStatus = "NOTPAID";
                    pw.Phase = Details.MeterPhase;
                    db.MAPPayments.Add(pw);
                    db.SaveChanges();

                    string BigInfo = "Dear " + CustomerName + ",Your Application for a Meter Upgrade from SINGLE PHASE TO THREE PHASE has been approved for payment. You may proceed to pay.";
                    string SmallInfo = "You will be contacted by the Meter Asset Provider Installation Team, for your Meter Installation according to the MAP installation Schedule, please quote the TicketID in all your transactions with PHED.A copy of the Customer Undertaking form has been sent to your Email.  Kindly click to Download the undertaking Form.";
                    string Status = "UPGRADE";

                    WriteInfoForCustomer(BigInfo, SmallInfo, Status);
                    return;
                }


                TicketId45.Value = Details.TransactionID;
              //  MapTicketIdApplied.InnerText = Details.TransactionID;
                MAPTicketId.InnerText = Details.TransactionID;
                //  MAPApplicantsName.InnerText = Details.MAPCustomerName;
                //  MAPMeterPhase.InnerText = Details.MeterPhase;
                //ApplicantsName1.InnerText = Details.MAPCustomerName;
                //MAPMeterType1.InnerText = Details.MeterPhase;

                if (Details.MAPPlan.Trim() == "UPFRONT")
                {
                    //show upfront
                    MapApplicationPayment.Visible = true;
                    MapApplicationSuccess.Visible = false;
                    //Do all the HASHING HERE AND 
                    string trans_id = RandomPassword.Generate(11).ToString();
                    string ProductID = "101"; 
                    string Amount = ""; 
                    string CustomerName = Details.MAPCustomerName;
                    if (Details.MeterPhase.Trim() == "THREE PHASE")
                    {
                        Amount = ThreePhaseAmount.ToString();
                        MAPAmount.InnerText = ThreePhaseAmount.ToString();
                    }
                    else
                    {
                        Amount = SinglePhaseAmount.ToString();
                        MAPAmount.InnerText = SinglePhaseAmount.ToString();
                    }

                    Amount = "100";
                    string TransactionId = Details.TransactionID;
                    MAPTransRef.InnerText = trans_id;


                    string CustomerEmail = Details.CustomerEmail;
                    string Currency = "NGN";
                    string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
                    string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
                    string ProductDescription = "MAP Meter Payment";
                    string ProductId = "78";
                    string CallbackURL = "map.nepamsonline.com/successPage.aspx";
                    string MerchantID = _MerchantID;

                    StringBuilder hashString = new StringBuilder();

                    hashString.Append("amount=" + Amount + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                        "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                        + "&trans-id=" + trans_id);

                    //Save the Customers Details to the Database

                    string HashCode = CreateHash(hashString.ToString(), Key);

                    ConfigSource = new PaymentConfigurationManager();
                    ConfigSource.tranx_amt = Amount;
                    ConfigSource.DemoMode = true;
                    ConfigSource.Email = CustomerEmail;
                    ConfigSource.mert_id = MerchantID;
                    ConfigSource.tranx_curr = "NGN";
                    ConfigSource.cust_id = TransactionId;
                    ConfigSource.cust_name = CustomerName;
                    ConfigSource.DemoConfirmationServiceUrl = "";
                    ConfigSource.LiveConfirmationServiceUrl = "";
                    ConfigSource.tranx_id = trans_id;
                    ConfigSource.echo_data = "";
                    ConfigSource.gway_first = "No";
                    ConfigSource.tranx_noti_url = CallbackURL;
                    ConfigSource.tranx_memo = ProductDescription;
                    ConfigSource.gway_name = "webpay";
                    ConfigSource.hash = HashCode;
                    ConfigSource.BankName = "";
                    ConfigSource.ProductId = ProductId;
                    ConfigSource.ItemId = "";
                    ConfigSource.MAC = PublicKey;

                    //save to the MAPPayment Table 
                    MAPPayment p = new MAPPayment();
                    p.PaymentFor = "METER";
                    p.TicketId = TransactionId;
                    p.TransRef = trans_id;
                    p.AccountNo = Details.CustomerReference;
                    p.Amount = Amount;
                    p.BSC = Details.BSC;
                    p.IBC = Details.IBC;
                    p.PaymentMode = "WEB";
                    p.PaymentStatus = "NOTPAID";
                    p.Phase = Details.MeterPhase;
                    db.MAPPayments.Add(p);
                    db.SaveChanges();

                    string BigInfo = "Dear " + CustomerName + ",Your Application for a " + Details.MeterPhase + " Meter has been approved for payment. You may proceed to pay.";
                    string SmallInfo = "You will be contacted by the Meter Asset Provider Installation Team, for your Meter Installation according to the MAP installation Schedule, please quote the TicketID in all your transactions with PHED.A copy of the Customer Undertaking form has been sent to your Email.  Kindly click to Download the undertaking Form.";
                    string Status = "UPFRONT";
                  
                    WriteInfoForCustomer(BigInfo,SmallInfo,Status);
                }

                if (Details.MAPPlan == "50UPFRONT" || Details.MAPPlan == "75UPFRONT" || Details.MAPPlan == "25UPFRONT" || Details.MAPPlan == "100UPFRONT")
                {
                    //show upfront
                    MapApplicationPayment.Visible = true;
                    MapApplicationSuccess.Visible = false;
                    //Do all the HASHING HERE AND 
                    string trans_id = RandomPassword.Generate(11).ToString();
                    string ProductID = "101";


                    string Amount = "10";

                    string CustomerName = Details.MAPCustomerName;
                    if (Details.MeterPhase.Trim() == "THREE PHASE")
                    {
                        Amount = (ThreePhaseAmount / divide).ToString();
                        MAPAmount.InnerText = (ThreePhaseAmount / divide).ToString();
                    }
                    else
                    {
                        Amount = (SinglePhaseAmount / divide).ToString();
                        MAPAmount.InnerText = (SinglePhaseAmount / divide).ToString();
                    } 
                    string TransactionId = Details.TransactionID;
                    MAPTransRef.InnerText = trans_id; 
                    string CustomerEmail = Details.CustomerEmail;
                    string Currency = "NGN";
                    string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
                    string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
                    string ProductDescription = "MAP Meter Payment";
                    string ProductId = "78";
                    string CallbackURL = "map.nepamsonline.com/successPage.aspx";
                    string MerchantID = _MerchantID;

                    StringBuilder hashString = new StringBuilder();

                    hashString.Append("amount=" + Amount + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                        "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                        + "&trans-id=" + trans_id);

                    //Save the Customers Details to the Database 
                    string HashCode = CreateHash(hashString.ToString(), Key); 
                    ConfigSource = new PaymentConfigurationManager();
                    ConfigSource.tranx_amt = Amount;
                    ConfigSource.DemoMode = true;
                    ConfigSource.Email = CustomerEmail;
                    ConfigSource.mert_id = MerchantID;
                    ConfigSource.tranx_curr = "NGN";
                    ConfigSource.cust_id = TransactionId;
                    ConfigSource.cust_name = CustomerName;
                    ConfigSource.DemoConfirmationServiceUrl = "";
                    ConfigSource.LiveConfirmationServiceUrl = "";
                    ConfigSource.tranx_id = trans_id;
                    ConfigSource.echo_data = "";
                    ConfigSource.gway_first = "No";
                    ConfigSource.tranx_noti_url = CallbackURL;
                    ConfigSource.tranx_memo = ProductDescription;
                    ConfigSource.gway_name = "webpay";
                    ConfigSource.hash = HashCode;
                    ConfigSource.BankName = "";
                    ConfigSource.ProductId = ProductId;
                    ConfigSource.ItemId = "";
                    ConfigSource.MAC = PublicKey;

                    //save to the MAPPayment Table 
                    MAPPayment p = new MAPPayment();
                    p.PaymentFor = "METER";
                    p.TicketId = TransactionId;
                    p.TransRef = trans_id;
                    p.AccountNo = Details.CustomerReference;
                    p.Amount = Amount;
                    p.BSC = Details.BSC;
                    p.IBC = Details.IBC;
                    p.PaymentMode = "WEB";
                    p.PaymentStatus = "NOTPAID";
                    p.Phase = Details.MeterPhase;
                    db.MAPPayments.Add(p);
                    db.SaveChanges();



                    string BigInfo = "Dear " + CustomerName + ",Your Application for a " + Details.MeterPhase + " Meter has been approved for payment. You may proceed to pay.";
                    string SmallInfo = "You will be contacted by the Meter Asset Provider Installation Team, for your Meter Installation according to the MAP installation Schedule, please quote the TicketID in all your transactions with PHED.A copy of the Customer Undertaking form has been sent to your Email.  Kindly click to Download the undertaking Form.";
                    string Status = "UPFRONT";


                   
                    WriteInfoForCustomer(BigInfo, SmallInfo, Status);
                }
                else
                {
                  
                    Response.Redirect("~/MSCSuccess.aspx?TicketId=" + TicketId + "&Type=MSC");
                     
                }
            }
        }

        private void WriteInfoForCustomer(string BigInfo, string SmallInfo, string Status)
        {
            InnerInfo.InnerText = SmallInfo;
            BiggerInfo.InnerText = BigInfo;
        }


        public string CreateHash(string message, string Key)
        {
            var encoding = new System.Text.ASCIIEncoding();
            //byte[] keyByte = 
            encoding.GetBytes(Key);
            byte[] keyByte = FromHex(Key);
            byte[] messageBytes = encoding.GetBytes(message); using
            (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return ByteToString(hashmessage);
            }
        }


        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", ""); byte[] raw = new byte[hex.Length / 2]; for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format 
            }
            return (sbinary);
        }

        private void BankParameters(string Email, string ItemId, string TransactionId, string Hash, string PaymentURL, string CustomerName,
           string MerchantID, string BankName, string Amount, string ProductId, string TransRef,
           string site_redirect_url, string MAC)
        {
            ConfigSource = new PaymentConfigurationManager();// PaymentConfigurationManager();
            ConfigSource.tranx_amt = Amount;
            ConfigSource.DemoMode = true;
            ConfigSource.mert_id = MerchantID;
            ConfigSource.LiveUrl = PaymentURL;
            ConfigSource.tranx_curr = ApplicationConstants.NairaCode;
            ConfigSource.cust_id = TransactionId;
            ConfigSource.cust_name = CustomerName;
            ConfigSource.DemoConfirmationServiceUrl = "";
            ConfigSource.LiveConfirmationServiceUrl = "";
            ConfigSource.tranx_id = TransRef;
            ConfigSource.echo_data = "";
            ConfigSource.gway_first = "No";
            ConfigSource.tranx_noti_url = site_redirect_url;
            ConfigSource.tranx_memo = "Payment for MAP";
            ConfigSource.gway_name = "webpay";
            ConfigSource.hash = Hash;
            ConfigSource.BankName = BankName;
            ConfigSource.ProductId = ProductId;
            ConfigSource.ItemId = ItemId;
            ConfigSource.MAC = MAC;
        }


    }
}