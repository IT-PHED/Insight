using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft;
 

namespace PHEDServe
{
    public partial class successPage : System.Web.UI.Page
    {
        ApplicationDbContext  db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorDiv.Visible = false;
            MapApplicationPayment.Visible = false;


            #region Before Requery

            string hash = Request.QueryString["hash"];
            string hash_type = Request.QueryString["hash-type"];
            string merchant_id = Request.QueryString["merchant-id"];
            string status_code = Request.QueryString["status-code"];
            string status_msg = Request.QueryString["status-msg"];
            string transaction_id = Request.QueryString["transaction-id"];
            string PaymentRef = Request.QueryString["payment-ref"];
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";



            // ?status-code=08&merchant-id=300034&transaction-id=8024065733&hash=c29tZWhhc2hlcw==&hash-type=c29tZWhhc2hlczE=&status-message=Transaction Successful, Approved by Financial Institution&payment-ref=XPS/012007/115007261000000589433
            //use the transaction Id and go to the payments Table and Check What the Guy is Paying for. He can only Pay for
            //Meter
            //BRC
            //Arrears







            //get the Details of the Guy


            #endregion


            //de requery here and send email and other things

            db = new ApplicationDbContext();
          


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

                        var WhatImPayingFor = db.MAPPayments.FirstOrDefault(p => p.TransRef == transaction_id);

                        if (WhatImPayingFor != null)
                        {
                            //There is Something I am paying for

                            var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == WhatImPayingFor.TicketId);

                            if (Details != null)
                            {

                                if (WhatImPayingFor.PaymentFor == "METER PAYMENT" || WhatImPayingFor.PaymentFor == "METER")
                                {
                                    //change his status to paid for the Meter and Let him have his receipt and expect a Printout

                                    //Post into DLEnhance

                                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/ApproveCustomerPayments");
                                    httpWebRequest.ContentType = "application/json";
                                    httpWebRequest.Method = "POST";

                                    string _DatePaid = DateTime.Now.ToString("dd-MM-yyyy");
                                    string Phase = "";
                                    if (WhatImPayingFor.Phase.Trim() == "THREE PHASE")
                                    {
                                        Phase = "3";
                                    }

                                    if (WhatImPayingFor.Phase.Trim() == "SINGLE PHASE")
                                    {
                                        Phase = "4";
                                    }

                                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                    {
                                        string json = "{\"Username\":\"phed\"," +
                                        "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                                        "\"DatePaid\":\"" + _DatePaid + "\"," +
                                        "\"Phase\":\"" + Phase + "\"," +
                                        "\"AccountNo\":\"" + WhatImPayingFor.AccountNo + "\"," +
                                        "\"AmountPaid\":\"" + amount + "\"," +
                                        "\"ReceiptNo\":\"" + WhatImPayingFor.TransRef + "\"," +
                                        "\"TicketNo\":\"" + WhatImPayingFor.TicketId + "\"}";
                                        streamWriter.Write(json);
                                        streamWriter.Flush();
                                        streamWriter.Close();
                                    }

                                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                    {
                                        var result = streamReader.ReadToEnd();
                                        result = result.Replace("\r", string.Empty);
                                        result = result.Replace("\n", string.Empty);
                                        result = result.Replace(@"\", string.Empty);
                                        result = result.Replace(@"\\", string.Empty);

                                        //check if the Customer Exists here

                                        if (result.Contains("Successful"))
                                        {
                                            CustomerPaymentInfo pf = db.CustomerPaymentInfos.Find(WhatImPayingFor.TicketId);
                                            if (pf != null)
                                            { 
                                                // MAPMeterType1.InnerHtml = pf.MeterPhase;
                                                ApplicantsName1.InnerHtml = pf.MAPCustomerName;
                                                TicketId45.Value = pf.TransactionID;
                                                 
                                                pf.DatePaid = DateTime.Now;
                                                pf.MAPPaymentStatus = "PAID";
                                                pf.MAPApplicationStatus = "APPROVED FOR INSTALLATION";

                                                db.Entry(pf).State = System.Data.Entity.EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            MAPPayment pays = db.MAPPayments.Find(transaction_id);
                                            if (pays != null)
                                            {
                                                MAPAmount.InnerHtml = pays.Amount;
                                                MAPTransRef.InnerHtml = pays.TransRef;

                                                pays.PaymentStatus = "PAID";
                                                pays.ApprovalStatus = "APPROVED";
                                                pays.ApprovedDate = DateTime.Now;
                                                pays.ApprovedBy = "WEB";
                                                pays.DatePaid = DateTime.Now.ToShortDateString();
                                                db.Entry(pays).State = System.Data.Entity.EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                        }

                                    }

                                    ErrorDiv.Visible = false;
                                    MapApplicationPayment.Visible = true;

                                }

                                if (WhatImPayingFor.PaymentFor == "METER UPGRADE")
                                {
                                    //change his status to paid for the Meter and Let him have his receipt and expect a Printout

                                    //Post into DLEnhance

                                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/ApproveCustomerPayments");
                                    httpWebRequest.ContentType = "application/json";
                                    httpWebRequest.Method = "POST";

                                    string _DatePaid = DateTime.Now.ToString("dd-MM-yyyy");
                                    string Phase = "";
                                    if (WhatImPayingFor.Phase.Trim() == "THREE PHASE")
                                    {
                                        Phase = "3";
                                    }

                                    if (WhatImPayingFor.Phase.Trim() == "SINGLE PHASE")
                                    {
                                        Phase = "4";
                                    }

                                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                    {
                                        string json = "{\"Username\":\"phed\"," +
                                        "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                                        "\"DatePaid\":\"" + _DatePaid + "\"," +
                                        "\"Phase\":\"" + Phase + "\"," +
                                        "\"AccountNo\":\"" + WhatImPayingFor.AccountNo + "\"," +
                                        "\"AmountPaid\":\"" + amount + "\"," +
                                        "\"ReceiptNo\":\"" + WhatImPayingFor.TransRef + "\"," +
                                        "\"TicketNo\":\"" + WhatImPayingFor.TicketId + "\"}";
                                        streamWriter.Write(json);
                                        streamWriter.Flush();
                                        streamWriter.Close();
                                    }

                                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                    {
                                        var result = streamReader.ReadToEnd();
                                        result = result.Replace("\r", string.Empty);
                                        result = result.Replace("\n", string.Empty);
                                        result = result.Replace(@"\", string.Empty);
                                        result = result.Replace(@"\\", string.Empty);

                                        //check if the Customer Exists here

                                        if (result.Contains("Successful"))
                                        {
                                            CustomerPaymentInfo pf = db.CustomerPaymentInfos.Find(WhatImPayingFor.TicketId);
                                            if (pf != null)
                                            {
                                                // MAPMeterType1.InnerHtml = pf.MeterPhase;
                                                ApplicantsName1.InnerHtml = pf.MAPCustomerName;
                                                TicketId45.Value = pf.TransactionID; 
                                                pf.DatePaid = DateTime.Now;
                                                pf.MAPPaymentStatus = "PAID";
                                                pf.MAPApplicationStatus = "APPROVED FOR INSTALLATION"; 
                                                pf.UpgradePaymentStatus = "PAID";
                                      
                                                db.Entry(pf).State = System.Data.Entity.EntityState.Modified;
                                                db.SaveChanges();
                                            } 
                                            MAPPayment pays = db.MAPPayments.Find(transaction_id);
                                            if (pays != null)
                                            {
                                                MAPAmount.InnerHtml = pays.Amount;
                                                MAPTransRef.InnerHtml = pays.TransRef; 
                                                pays.PaymentStatus = "PAID";
                                                pays.ApprovalStatus = "APPROVED";
                                                pays.ApprovedDate = DateTime.Now;
                                                pays.ApprovedBy = "WEB";
                                                pays.DatePaid = DateTime.Now.ToShortDateString();
                                                db.Entry(pays).State = System.Data.Entity.EntityState.Modified;
                                                db.SaveChanges();
                                            } 
                                        } 
                                    }

                                    ErrorDiv.Visible = false;
                                    MapApplicationPayment.Visible = true;

                                }

                                if (WhatImPayingFor.PaymentFor == "BRC")
                                {
                                    //Post his Payment into Notify payment for his Arrears
                                    //var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == WhatImPayingFor.TicketId);

                                    CustomerPaymentInfo CustomerDetails1 = (from p in db.CustomerPaymentInfos where p.TransactionID == WhatImPayingFor.TicketId select p).SingleOrDefault();
                                    string Amounts = "";
                                    //customer has not been given the Token Yet.
                                    CustomerDetails1.merchantID = merchantID;
                                    CustomerDetails1.Amount = amountInNaira;
                                    CustomerDetails1.Hash = _hash;
                                    CustomerDetails1.DatePaid = Convert.ToDateTime(transactionDate);
                                    CustomerDetails1.CardType = type;
                                    CustomerDetails1.CardBrand = brand;
                                    CustomerDetails1.PaymentReference = paymentReference;
                                    CustomerDetails1.TransactionProcessDate = transactionProccessDate;
                                    CustomerDetails1.PaymentStatus = "SUCCESS";
                                    db.Entry(CustomerDetails1).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();



                                    MAPPayment pays = db.MAPPayments.Find(transaction_id);
                                    if (pays != null)
                                    {
                                        MAPAmount.InnerHtml = pays.Amount;
                                        MAPTransRef.InnerHtml = pays.TransRef;
                                        ApplicantsName1.InnerHtml = CustomerDetails1.MAPCustomerName;
                                        TicketId45.Value = CustomerDetails1.TransactionID;
                                         
                                    }

                                     
                                    //update the Customer Records with the Needed Items 
                                    db = new ApplicationDbContext();
                                    db = new ApplicationDbContext();
                                    CustomerPaymentInfo CustomerDetails2 = (from p in db.CustomerPaymentInfos
                                                                            where p.TransactionID == WhatImPayingFor.TicketId
                                                                            select p).SingleOrDefault();
                                    if (CustomerDetails2 != null)
                                    {
                                        //gotoDLENHANCEC

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

                                        //Requery DLENHANCE

                                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/NotifyPayment");
                                        httpWebRequest.ContentType = "application/json";
                                        httpWebRequest.Method = "POST";

                                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
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


                                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                        using (var streamReader2 = new StreamReader(httpResponse.GetResponseStream()))
                                        {
                                            var result2 = streamReader2.ReadToEnd();
                                            result2 = result2.Replace("\r", string.Empty);
                                            result2 = result2.Replace("\n", string.Empty);
                                            result2 = result2.Replace(@"\", string.Empty);
                                            result2 = result2.Replace(@"\\", string.Empty);

                                            //check if the Customer Exists here

                                            CustomerPaymentInfo pf = db.CustomerPaymentInfos.Find(WhatImPayingFor.TicketId);
                                            if (pf != null)
                                            {

                                                // MAPMeterType1.InnerHtml = pf.MeterPhase;
                                                ApplicantsName1.InnerHtml = pf.MAPCustomerName;
                                                TicketId45.Value = pf.TransactionID;



                                                pf.DatePaid = DateTime.Now;
                                                pf.MAPPaymentStatus = "PAID";
                                                pf.MAPApplicationStatus = "ABOUTTOAPPLY";

                                                db.Entry(pf).State = System.Data.Entity.EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            MAPPayment payss = db.MAPPayments.Find(transaction_id);
                                            payss.PaymentStatus = "PAID";
                                            payss.ApprovalStatus = "APPROVED";
                                            payss.ApprovedDate = DateTime.Now;
                                            payss.ApprovedBy = "WEB";
                                            payss.DatePaid = DateTime.Now.ToShortDateString();
                                            db.Entry(payss).State = System.Data.Entity.EntityState.Modified;
                                            db.SaveChanges();

                                            //var objResponse1 = JsonConvert.DeserializeObject<List<TokenJSONObject>>(result);

                                            db.Entry(CustomerDetails2).State = System.Data.Entity.EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                    }


                                }
                            }
                        }
                    }
                    else
                    {

                        Span1.InnerHtml = "Esteemed Customer";
                        ErrorDiv.Visible = false;
                        MapApplicationPayment.Visible = true;
                        ErrorDiv.Visible = true;
                        MapApplicationPayment.Visible = false;
                    }
                }
                else
                {
                    //Tell him his payment was not Successful. Thanks
                    ErrorDiv.Visible = true;
                    MapApplicationPayment.Visible = false;
                }
            }
        }

        private bool TryParseJSON(string json, out JObject jObject)
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
     
    }
}