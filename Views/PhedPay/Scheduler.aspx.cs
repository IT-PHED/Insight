using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhedPay.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;
using System.Threading.Tasks;


namespace PhedPay
{

    public partial class Scheduler : System.Web.UI.Page
    {
        bool invalid = false;

        private static readonly HttpClient client = new HttpClient();
        ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        { 
            var AllPayments = db.CustomerPaymentInfos.Where(p => p.Token == "NOTAVAILABLE").ToList();


            foreach (var item in AllPayments)
            {
            string transaction_id = item.TransactionID; 
            string hash_type = "SHA256";
            string merchant_id = "00024";

            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";

            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";

            //de requery here and send email and other things

            StringBuilder hashString = new StringBuilder();

            hashString.Append("merchant-id=" + merchant_id + "&public-key=" + PublicKey + "&transaction-id=" + transaction_id);

            //Save the Customers Details to the Database

            string HashCode = CreateHash(hashString.ToString(), Key);


            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["transaction-id"] = transaction_id;
                values["merchant-id"] = merchant_id;
                values["public-key"] = PublicKey;
                values["hash"] = HashCode;
                values["hash-type"] = hash_type;

                var response = client.UploadValues("https://xpresspayonline.com:8000/xp-gateway/ws/v2/query", values);

                var responseString = Encoding.Default.GetString(response);

                JObject Op = new JObject();

                if (TryParseJSON(responseString, out Op))
                {
                    //True Can be parsed

                    JObject o = JObject.Parse(responseString);

                    string transactionID = o["transactionID"].ToString();
                    string responseCode = o["responseCode"].ToString();

                    string responseMsg = o["responseMsg"].ToString();

                    string merchantID = o["merchantID"].ToString();
                    string amount = o["amount"].ToString();
                    string amountInNaira = o["amountInNaira"].ToString();
                    string _hash = o["hash"].ToString();
                    string hashType = o["hashType"].ToString();
                    string transactionDate = o["transactionDate"].ToString();
                    string maskedPan = o["maskedPan"].ToString();

                    string type = o["type"].ToString();
                    string brand = o["brand"].ToString();
                    string paymentReference = o["paymentReference"].ToString();
                    string transactionProccessDate = o["transactionProccessDate"].ToString();


                    if (responseCode == "000")
                    {

                        //find the Customer and Update his record
                        db = new ApplicationDbContext();


                        CustomerPaymentInfo CustomerDetails = (from p in db.CustomerPaymentInfos
                                                               where p.TransactionID == transaction_id
                                                               select p).SingleOrDefault();

                        CustomerDetails.merchantID = merchantID;
                        CustomerDetails.Amount = amountInNaira;
                        CustomerDetails.Hash = _hash;
                        CustomerDetails.DatePaid = Convert.ToDateTime(transactionDate);
                        CustomerDetails.CardType = type;
                        CustomerDetails.CardBrand = brand;
                        CustomerDetails.PaymentReference = paymentReference;
                        CustomerDetails.TransactionProcessDate = transactionProccessDate;
                        CustomerDetails.PaymentStatus = "SUCCESS";
                        db.Entry(CustomerDetails).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();



                        //update the Customer Records with the Needed Items 
                        db = new ApplicationDbContext();
                        CustomerPaymentInfo CustomerDetails2 = (from p in db.CustomerPaymentInfos
                                                                where p.TransactionID == transaction_id
                                                                select p).SingleOrDefault();
                        if (CustomerDetails != null)
                        {

                            //check if DLENHANC HAs generated a token with this Transaction ID


                            if (CustomerDetails2.Token == "NOTAVAILABLE")
                            {
                                //gotoDLENHANCEC

                                //check DLENHANCE 

                                var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/Gettransactioninfo");
                                httpWebRequest2.ContentType = "application/json";

                                httpWebRequest2.Method = "POST";

                                using (var streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream()))
                                {
                                    //{"username": "phed",  "apikey": "2E639809-58B0-49E2-99D7-38CB4DF2B5B20" , "TransactionNo": "5754336624" }

                                    string json = "{\"Username\":\"phed\"," +
                                    "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                                    "\"TransactionNo\":\"" + transaction_id + "\"}";
                                    streamWriter.Write(json);
                                    streamWriter.Flush();
                                    streamWriter.Close();
                                }

                                var httpResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse2.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();
                                    result = result.Replace("\r", string.Empty);
                                    result = result.Replace("\n", string.Empty);
                                    result = result.Replace(@"\", string.Empty);
                                    result = result.Replace(@"\\", string.Empty);

                                    //check if the Customer Exists here

                                    if (result.ToUpper().Contains("LOGID NOT FOUND"))
                                    {
                                        //call DL ENhance Direct

                                        //the  
                                        string APIKey = "2E639809-58B0-49E2-99D7-38CB4DF2B5B20";
                                        string Username = "phed";
                                        string CustReference = CustomerDetails2.CustomerReference;
                                        string AlternateCustReference = CustomerDetails2.AlternateCustReference;
                                        string PaymentLogId = transactionID;
                                        string Amount = CustomerDetails2.Amount;
                                        string PaymentMethod = "WEB";
                                        string PaymentReference = paymentReference;
                                        string TerminalID = null;
                                        string ChannelName = "WEB";
                                        string Location = null;
                                        string InstitutionId = "02";
                                        string PaymentCurrency = "NGN";
                                        string DepositSlipNumber = transactionID;
                                        string DepositorName = CustomerDetails2.DepositorName.ToString();
                                        string CustomerPhoneNumber = CustomerDetails2.CustomerPhoneNumber;
                                        string CustomerAddress = CustomerDetails2.CustomerAddress;
                                        string BankCode = "023";
                                        string CollectionsAccount = null;
                                        string ReceiptNo = transactionID;
                                        string OtherCustomerInfo = null;
                                        string CustomerName = CustomerDetails2.CustomerName;
                                        string BankName = "PHED WEB";
                                        string BranchName = "PHED WEB";
                                        string InstitutionName = "PHEDBillPay";
                                        string ItemName = "PHED Bill Payment";
                                        string ItemCode = "01";
                                        string ItemAmount = CustomerDetails2.Amount;
                                        string PaymentStatus = "Success";
                                        string IsReversal = null;
                                        string SettlementDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                        string Teller = null;



                                        var httpWebRequest3 = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/NotifyPayment");
                                        httpWebRequest3.ContentType = "application/json";
                                        httpWebRequest3.Method = "POST";

                                        using (var streamWriter = new StreamWriter(httpWebRequest3.GetRequestStream()))
                                        {
                                            string json = "{\"Username\":\"phed\"," +
                                                          "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                                                          "\"PaymentLogId\":\"" + PaymentLogId + "\"," +
                                                          "\"CustReference\":\"" + CustReference + "\"," +
                                                            "\"AlternateCustReference\":\"" + AlternateCustReference + "\"," +
                                                             "\"Amount\":\"" + Amount + "\"," +
                                                          "\"PaymentMethod\":\"" + PaymentMethod + "\"," +
                                                          "\"PaymentReference\":\"" + PaymentReference + "\"," +
                                                            "\"TerminalID\":\"" + TerminalID + "\"," +
                                                          "\"ChannelName\":\"" + ChannelName + "\"," +
                                                          "\"Location\":\"" + Location + "\"," +
                                                            "\"PaymentDate\":\"" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "\"," +
                                                          "\"InstitutionId\":\"" + null + "\"," +
                                                          "\"InstitutionName\":\"" + null + "\"," +
                                                            "\"BankName\":\"" + BankName + "\"," +
                                                            "\"BranchName\":\"" + BranchName + "\"," +
                                                          "\"CustomerName\":\"" + CustomerName + "\"," +
                                                          "\"OtherCustomerInfo\":\"" + OtherCustomerInfo + "\"," +
                                                            "\"ReceiptNo\":\"" + ReceiptNo + "\"," +
                                                          "\"CollectionsAccount\":\"" + CollectionsAccount + "\"," +
                                                          "\"BankCode\":\"" + BankCode + "\"," +
                                                            "\"CustomerAddress\":\"" + CustomerAddress + "\"," +
                                                          "\"CustomerPhoneNumber\":\"" + CustomerPhoneNumber + "\"," +
                                                          "\"DepositorName\":\"" + DepositorName + "\"," +
                                                            "\"DepositSlipNumber\":\"" + DepositSlipNumber + "\"," +
                                                          "\"PaymentCurrency\":\"" + PaymentCurrency + "\"," +
                                                          "\"ItemName\":\"" + ItemName + "\"," +
                                                            "\"ItemCode\":\"" + ItemCode + "\"," +
                                                          "\"ItemAmount\":\"" + ItemAmount + "\"," +
                                                          "\"PaymentStatus\":\"" + PaymentStatus + "\"," +
                                                            "\"IsReversal\":\"" + IsReversal + "\"," +
                                                            "\"SettlementDate\":\"" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "\"," +
                                                             "\"Teller\":\"" + "PHED WebTeller" + "\"}";
                                            streamWriter.Write(json);
                                            streamWriter.Flush();
                                            streamWriter.Close();
                                        }


                                        var httpResponse3 = (HttpWebResponse)httpWebRequest3.GetResponse();

                                        using (var streamReader3 = new StreamReader(httpResponse3.GetResponseStream()))
                                        {
                                            var result3 = streamReader3.ReadToEnd();
                                            result3 = result3.Replace("\r", string.Empty);
                                            result3 = result3.Replace("\n", string.Empty);
                                            result3 = result3.Replace(@"\", string.Empty);
                                            result3 = result3.Replace(@"\\", string.Empty);

                                            //check if the Customer Exists here

                                            var objResponse1 = JsonConvert.DeserializeObject<List<TokenJSONObject>>(result3);


                                            if (CustomerDetails2.AccountType == "PREPAID")
                                            {
                                                string Token = objResponse1[0].TOKENDESC.ToString();
                                                CustomerDetails2.Token = Token;
                                                CustomerDetails2.Units = objResponse1[0].UNITSACTUAL.ToString();
                                                CustomerDetails2.Tarriff = objResponse1[0].TARIFF.ToString();
                                            }
                                            else
                                            {
                                                CustomerDetails2.Token = "POSTPAID";
                                            }
                                            CustomerDetails2.TokenDate = objResponse1[0].PAYMENTDATETIME.ToString();
                                            CustomerDetails2.ReceiptNo = ReceiptNo;
                                            db.Entry(CustomerDetails2).State = System.Data.Entity.EntityState.Modified;
                                            db.SaveChanges();
                                        }



                                    }
                                    else
                                    {
                                        var objResponse1 = JsonConvert.DeserializeObject<List<TokenJSONObject>>(result);

                                        if (CustomerDetails2.AccountType == "PREPAID")
                                        {
                                            string Token = objResponse1[0].TOKENDESC.ToString();
                                            CustomerDetails2.Token = Token;
                                            CustomerDetails2.Units = objResponse1[0].UNITSACTUAL.ToString();
                                            CustomerDetails2.Tarriff = objResponse1[0].TARIFF.ToString();
                                        }
                                        else
                                        {
                                            CustomerDetails2.Token = "POSTPAID";
                                        }

                                        CustomerDetails2.TokenDate = objResponse1[0].PAYMENTDATETIME.ToString();

                                        db.Entry(CustomerDetails2).State = System.Data.Entity.EntityState.Modified;
                                        db.SaveChanges();
                                    }

                                }


                            }

                        }





                        db = new ApplicationDbContext();
                        CustomerPaymentInfo CustomerDetails3 = (from p in db.CustomerPaymentInfos
                                                                where p.TransactionID == transaction_id
                                                                select p).SingleOrDefault();

                        if (CustomerDetails3.Token != "NOTAVAILABLE" && CustomerDetails3.PaymentStatus == "SUCCESS")
                        {


                            SendEmail(transaction_id);


                        }
                        else
                        {


                        }
                    }
                }



            }
        }
        }

        public static string ByteToStrings(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
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

            private static bool TryParseJSON(string json, out JObject jObject)
        {
            try
            {
                jObject = JObject.Parse(json);
                return true;
            }
            catch
            {
                jObject = null;
                return false;
            }
        }


            [HttpGet]
            public void SendEmail(string id)
            {
                db = new ApplicationDbContext();

                CustomerPaymentInfo CustomerDetails2 =  db.CustomerPaymentInfos.Find(id);

                string Amounts = "";

                if (CustomerDetails2 != null)
                {
                    #region SendEmail
                    if (CustomerDetails2.Token != "NOTAVAILABLE" && CustomerDetails2.AccountType == "PREPAID" && CustomerDetails2.PaymentStatus == "SUCCESS")
                    {
                        //Send Email again

                        if (CustomerDetails2 != null)
                        {
                            if (CustomerDetails2.CustomerEmail != "")
                            {
                                //SEND EMAIL TO THE CUSTOMER
                                MailMessage mail = new MailMessage();

                                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                                mail.Subject = "Payment Information From PHED";
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.High;
                                mail.Bcc.Add("payments@phed.com.ng");
                                mail.To.Add(CustomerDetails2.CustomerEmail);
                                string RecipientType = "";

                                string SMTPMailServer = "mail.phed.com.ng";

                                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                                //  string htmlMsgBody = this.EmailTextBox.Text;
                                string htmlMsgBody = "<html><head></head>";
                                htmlMsgBody = htmlMsgBody + "<body>";

                                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";

                                htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + CustomerDetails2.CustomerName + "</P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for Paying your energy bills. Please find below your payment details" + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + CustomerDetails2.CustomerName + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Date: " + CustomerDetails2.TokenDate + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Type: " + CustomerDetails2.CardType + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Brand: " + CustomerDetails2.CardBrand + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Amount: ₦" + CustomerDetails2.Amount + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Token Key: " + CustomerDetails2.Token + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Units: " + CustomerDetails2.Units + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Currency: NAIRA " + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Merchant Name: Port-Harcourt Electricity Distribution Company" + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Merchant URL: www.phed.com.ng" + " </P>";

                                htmlMsgBody = htmlMsgBody + "<br><br>";

                                htmlMsgBody = htmlMsgBody + "Thank you,";

                                htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";

                                htmlMsgBody = htmlMsgBody + "<br><br>";
                                mail.Body = htmlMsgBody;

                                //-Merchant’s Name
                                //-Merchant’s Url
                                //-Description of the Service/Goods

                                MailSMTPserver.Send(mail);

                            }
                        }
                    }

                    else if (CustomerDetails2.AccountType == "POSTPAID" && CustomerDetails2.PaymentStatus == "SUCCESS")
                    {
                        if (CustomerDetails2 != null)
                        {


                            if (CustomerDetails2.CustomerEmail != "")
                            {
                                //SEND EMAIL TO THE CUSTOMER
                                MailMessage mail = new MailMessage();

                                mail.From = new MailAddress("Payments@phed.com.ng", "Port-harcourt Electricity Distribution Company");
                                mail.Subject = "Payment Information From PHED";
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.High;
                                mail.Bcc.Add("payments@phed.com.ng");
                                mail.To.Add(CustomerDetails2.CustomerEmail);
                                string RecipientType = "";

                                string SMTPMailServer = "mail.phed.com.ng";

                                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                                //  string htmlMsgBody = this.EmailTextBox.Text;
                                string htmlMsgBody = "<html><head></head>";
                                htmlMsgBody = htmlMsgBody + "<body>";

                                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";

                                htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + CustomerDetails2.CustomerName + "</P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for Paying your energy bills. Please find below your payment details" + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + CustomerDetails2.CustomerName + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Date: " + CustomerDetails2.TokenDate + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Type: " + CustomerDetails2.CardType + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Brand: " + CustomerDetails2.CardBrand + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Amount: ₦" + CustomerDetails2.Amount + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Currency: NAIRA " + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Merchant Name: Port-Harcourt Electricity Distribution Company" + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Merchant URL: www.phed.com.ng" + " </P>";

                                htmlMsgBody = htmlMsgBody + "<br><br>";
                                htmlMsgBody = htmlMsgBody + "Thank you,";
                                htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
                                htmlMsgBody = htmlMsgBody + "<br><br>";
                                mail.Body = htmlMsgBody;

                                //-Merchant’s Name
                                //-Merchant’s Url
                                //-Description of the Service/Goods

                                MailSMTPserver.Send(mail);
                            }
                        }
                    }

                    #endregion
                }
            }
    }
}