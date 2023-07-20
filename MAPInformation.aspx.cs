using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHEDServe
{
    public partial class MAPInformation : System.Web.UI.Page
    {
        protected PaymentConfigurationManager ConfigSource { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
             
            string PaymentType = Request.QueryString["Type"].ToString();
            string TicketId = Request.QueryString["TicketId"].ToString();
            TicketId45.Value = TicketId;
            //get the Details of the Guy 
            MAPTicketId.InnerText = TicketId;
            //Installed

            if (PaymentType.Trim() == "INSTALLATION DONE")
            {
                MeterInformation.InnerText = "Your meter has  been Installed. ";

                DescriptiveInfo.InnerText = "You can proceed to Vend Electricity. please visit www.phed.com.ng to pay for your Electricity Bills. Thank you.";
            }

            if (PaymentType.Trim() == "APPROVED FOR INSTALLATION")
            {
                MeterInformation.InnerText = "Your meter has  been Approved for Installation. ";

                DescriptiveInfo.InnerText = "You can proceed to Vend Electricity. please visit www.phed.com.ng to pay for your Electricity Bills. Thank you.";

            }

            if (PaymentType.Trim() == "PAID FOR METER")
            {
                MeterInformation.InnerText = "You have paid for a  meter and it will be Installed for you Soon. ";

                DescriptiveInfo.InnerText = "Your Payment has been approved for Installation.Please wait as the MAP Vendor will contact you  to install the Meter.Dont forget to Quote the TicketID in all your transactions with PHED. Thank you.";
            }



            if (PaymentType.Trim() == "PAID FOR METER")
            {
                MeterInformation.InnerText = "You have paid for a  meter and it will be Installed for you Soon. ";

                DescriptiveInfo.InnerText = "Your Payment has been approved for Installation.Please wait as the MAP Vendor will contact you  to install the Meter.Dont forget to Quote the TicketID in all your transactions with PHED. Thank you.";
            }
            if (PaymentType.Trim() == "MAPPAID")
            {
                MeterInformation.InnerText = "You have paid for a  meter and it will be Installed for you Soon. ";

                DescriptiveInfo.InnerText = "Your Payment has been approved for Installation.Please wait as the MAP Vendor will contact you  to install the Meter.Dont forget to Quote the TicketID in all your transactions with PHED. Thank you.";
            }

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