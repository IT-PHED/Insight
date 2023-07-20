using ERDBManager;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class MAPRegisterController : Controller
    {
          ApplicationDbContext db = new ApplicationDbContext();
 

          public static string _ThreePhaseAmount = ConfigurationManager.AppSettings["ThreePhaseAmount"].ToString();
          public static string _SinglePhaseAmount = ConfigurationManager.AppSettings["SinglePhaseAmount"].ToString();

          decimal ThreePhaseAmount = Convert.ToDecimal(_ThreePhaseAmount);
          decimal SinglePhaseAmount = Convert.ToDecimal(_SinglePhaseAmount); 

      
        public MAPRegisterController()
        {
        }

        public ActionResult ApplicationSuccess(string TicketID)
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult ApplyForMeter(string CustomerDetails, string TicketId, string PaymentScheduleName, string PaymentScheduleValue, string TOTAL_AMOUNT)
        {
            decimal threePhaseAmount;
            UserViewModel userViewModel = new UserViewModel();
            ActionMessagePerformed actionMessagePerformed = new ActionMessagePerformed();
            CUSTOMER nullable = JsonConvert.DeserializeObject<CUSTOMER>(CustomerDetails);

            decimal divide = 0;

            if (nullable.ModeOfPayment == "50UPFRONT")
            {
                divide = 2;
            }
            if (nullable.ModeOfPayment == "75UPFRONT")
            {
                divide = 1.33333M;
            }
            if (nullable.ModeOfPayment == "25UPFRONT")
            {
                divide = 4;
            }
            if (nullable.ModeOfPayment == "100UPFRONT")
            {
                divide = 1;
            }
            if (nullable.ModeOfPayment == "0UPFRONT")
            {
                if (nullable.MeterType.Trim() == "SINGLE PHASE")
                {
                    divide = SinglePhaseAmount;
                }
                if (nullable.MeterType.Trim() == "THREE PHASE")
                {
                    divide = ThreePhaseAmount;
                }
            }
            try
            {
                CustomerPaymentInfo meterType = this.db.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
                if (meterType != null)
                {
                    meterType.MeterPhase = nullable.MeterType;
                    meterType.MAPPlanId = PaymentScheduleValue;
                    meterType.MAPPlanTotalAmount = TOTAL_AMOUNT;
                     
                    if (nullable.ModeOfPayment == "MSC")
                    {
                        meterType.MAPApplicationStatus = "APPROVED FOR INSTALLATION";
                        
                        if (nullable.MeterType.Trim() == "THREE PHASE")
                        {
                            meterType.MAPAmount = ThreePhaseAmount.ToString();
                            meterType.AmountToPayMSC = ThreePhaseAmount.ToString();
                            meterType.AmountToPayUpfront = "0";
                            nullable.MeterAmount = ThreePhaseAmount.ToString();
                        }

                        if (nullable.MeterType.Trim() == "SINGLE PHASE")
                        {
                            meterType.MAPAmount = SinglePhaseAmount.ToString();
                            meterType.AmountToPayMSC = SinglePhaseAmount.ToString();
                            meterType.AmountToPayUpfront = "0";
                            nullable.MeterAmount = SinglePhaseAmount.ToString();
                        }
                    }

                 

                    else if (nullable.ModeOfPayment == "UPFRONT")
                    {
                        meterType.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                        if (nullable.MeterType.Trim() == "THREE PHASE")
                        {
                            meterType.MAPAmount = ThreePhaseAmount.ToString();
                            meterType.AmountToPayUpfront = ThreePhaseAmount.ToString();
                            meterType.AmountToPayMSC = "0";
                            nullable.MeterAmount = ThreePhaseAmount.ToString();
                        }


                        if (nullable.MeterType.Trim() == "SINGLE PHASE")
                        {
                            meterType.MAPAmount = SinglePhaseAmount.ToString();
                            meterType.AmountToPayUpfront = SinglePhaseAmount.ToString();
                            meterType.AmountToPayMSC = "0";
                            nullable.MeterAmount = SinglePhaseAmount.ToString();
                        }
                    }
                    else if (nullable.ModeOfPayment == "50UPFRONT" || nullable.ModeOfPayment == "75UPFRONT" || nullable.ModeOfPayment == "25UPFRONT" || nullable.ModeOfPayment == "100UPFRONT"|| nullable.ModeOfPayment == "0UPFRONT")
                    {
                        meterType.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                        if (nullable.MeterType.Trim() == "SINGLE PHASE")
                        {
                            threePhaseAmount = SinglePhaseAmount / divide;
                            meterType.MAPAmount = SinglePhaseAmount.ToString();
                            meterType.AmountToPayMSC = Math.Round(Convert.ToDecimal(SinglePhaseAmount - threePhaseAmount), 2).ToString();
                            meterType.AmountToPayUpfront = Math.Round(Convert.ToDecimal(threePhaseAmount), 2).ToString();
                            nullable.MeterAmount = Math.Round(Convert.ToDecimal(SinglePhaseAmount), 2).ToString();
                        }
                        if (nullable.MeterType.Trim() == "THREE PHASE")
                        {
                            threePhaseAmount = ThreePhaseAmount / divide;
                            meterType.MAPAmount = Math.Round(Convert.ToDecimal(ThreePhaseAmount), 2).ToString();
                            meterType.AmountToPayMSC = Math.Round(Convert.ToDecimal(ThreePhaseAmount - threePhaseAmount), 2).ToString();
                            meterType.AmountToPayUpfront =
                            nullable.MeterAmount = Math.Round(Convert.ToDecimal(ThreePhaseAmount), 2).ToString();
                        }
                    }

                    meterType.IDcardNo = nullable.IDcardNo.Trim();
                    meterType.MAPPlan = nullable.ModeOfPayment;
                    this.db.Entry(meterType).State = EntityState.Modified;
                    this.db.SaveChanges();
                }
                 
                string str = "";
                nullable.RegistrationDate = new DateTime?(DateTime.Now);
                if (nullable.ModeOfPayment == "MSC")
                {
                    meterType.MAPApplicationStatus = "APPROVED FOR INSTALLATION";
                    if (nullable.MeterType.Trim() == "THREE PHASE")
                    {
                        meterType.MAPAmount = ThreePhaseAmount.ToString();
                        meterType.AmountToPayMSC = ThreePhaseAmount.ToString();
                        meterType.AmountToPayUpfront = "0";
                        nullable.MeterAmount = ThreePhaseAmount.ToString();
                    }
                    if (nullable.MeterType.Trim() == "SINGLE PHASE")
                    {
                        meterType.MAPAmount = SinglePhaseAmount.ToString();
                        meterType.AmountToPayMSC = SinglePhaseAmount.ToString();
                        meterType.AmountToPayUpfront = "0";
                        nullable.MeterAmount = SinglePhaseAmount.ToString();
                    }
                }


                else if (nullable.ModeOfPayment == "UPFRONT")
                {
                    meterType.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                    if (nullable.MeterType.Trim() == "THREE PHASE")
                    {
                        meterType.MAPAmount = ThreePhaseAmount.ToString();
                        meterType.AmountToPayUpfront = ThreePhaseAmount.ToString();
                        meterType.AmountToPayMSC = "0";
                        nullable.MeterAmount = ThreePhaseAmount.ToString();
                    }
                    if (nullable.MeterType.Trim() == "SINGLE PHASE")
                    {
                        meterType.MAPAmount = SinglePhaseAmount.ToString();
                        meterType.AmountToPayUpfront = SinglePhaseAmount.ToString();
                        meterType.AmountToPayMSC = "0";
                        nullable.MeterAmount = Math.Round(Convert.ToDecimal(SinglePhaseAmount), 2).ToString();  
                    }
                }


                else if (nullable.ModeOfPayment == "50UPFRONT" || nullable.ModeOfPayment == "75UPFRONT" || nullable.ModeOfPayment == "25UPFRONT" || nullable.ModeOfPayment == "100UPFRONT"|| nullable.ModeOfPayment == "0UPFRONT")
                {
                    meterType.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                    if (nullable.MeterType.Trim() == "SINGLE PHASE")
                    {
                        threePhaseAmount = SinglePhaseAmount / divide;
                        meterType.MAPAmount = SinglePhaseAmount.ToString();
                        meterType.AmountToPayMSC =  (SinglePhaseAmount - threePhaseAmount).ToString();
                        meterType.AmountToPayUpfront = Math.Round(Convert.ToDecimal(threePhaseAmount), 2).ToString(); 
                        nullable.MeterAmount = SinglePhaseAmount.ToString();
                    }
                    if (nullable.MeterType.Trim() == "THREE PHASE")
                    {
                        threePhaseAmount = ThreePhaseAmount / divide;
                        meterType.MAPAmount = ThreePhaseAmount.ToString();
                        meterType.AmountToPayMSC = (ThreePhaseAmount - threePhaseAmount).ToString();
                        meterType.AmountToPayUpfront = Math.Round(Convert.ToDecimal(threePhaseAmount), 2).ToString();  
                        nullable.MeterAmount = ThreePhaseAmount.ToString();
                    }
                }

                else if (nullable.ModeOfPayment == "UPFRONT")
                {
                    meterType.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                    if (nullable.MeterType.Trim() == "SINGLE PHASE")
                    {

                        meterType.MAPAmount = SinglePhaseAmount.ToString();
                        meterType.AmountToPayMSC = "0";
                        meterType.AmountToPayUpfront = SinglePhaseAmount.ToString();
                        nullable.MeterAmount = SinglePhaseAmount.ToString();


                    }
                    if (nullable.MeterType.Trim() == "THREE PHASE")
                    {
                        meterType.MAPAmount = ThreePhaseAmount.ToString();
                        meterType.AmountToPayMSC = "0";
                        meterType.AmountToPayUpfront = ThreePhaseAmount.ToString();
                        nullable.MeterAmount = ThreePhaseAmount.ToString();
                    }
                }

                this.db.CUSTOMERS.Add(nullable);
                this.db.SaveChanges();
                string[] ticketId = new string[] { "Dear ", str, ", Your application for a Meter was successful, your Ticket Number is ", TicketId, ". Kindly Quote this Number in all Transactions with PHED" };
                string.Concat(ticketId);
                actionMessagePerformed.message = "Your Application for the Smart Meter Was Successful";
                actionMessagePerformed.title = "Successful";
                actionMessagePerformed.type = "success";
                actionMessagePerformed.PaymentType = nullable.ModeOfPayment;
            }
            catch (Exception exception)
            {
                actionMessagePerformed.message = "Your Application for the Smart Meter Was Unsuccessful!";
                actionMessagePerformed.title = "Something went wrong";
                actionMessagePerformed.type = "error";
            }
            userViewModel.ActionMessagePerformed = actionMessagePerformed;
            userViewModel.customer = nullable;
            string str1 = JsonConvert.SerializeObject(userViewModel);
            JsonResult jsonResult = base.Json(new { result = str1, ModeofPayment = nullable.ModeOfPayment }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public ActionResult BRC()
        {
            return base.View();
        }
        public ActionResult ChangeMeter()
        {
            Response.Redirect("~/ChangeMeterType.aspx");
            return base.View();
        }


        public static string ByteToString(byte[] buff)
        {
            string str = "";
            for (int i = 0; i < (int)buff.Length; i++)
            {
                str = string.Concat(str, buff[i].ToString("X2"));
            }
            return str;
        }

        public static string ByteToStrings(byte[] buff)
        {
            string str = "";
            for (int i = 0; i < (int)buff.Length; i++)
            {
                str = string.Concat(str, buff[i].ToString("X2"));
            }
            return str;
        }

        public ActionResult CheckApplicationStatus()
        {
            return base.View();
        }

        public ActionResult CheckStatus()
        {
            return base.View();
        }

        [HttpGet]
        public JsonResult CreateCustomer()
        {


            DataSet pDataset = new DataSet();
            /////  PaymentScheduleList  List<MAPPaymentPlans> MAPPay = new List<MAPPaymentPlans>();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString();

            List<MAPPaymentPlans> MAPPay = new List<MAPPaymentPlans>();


            //DBManager dBManager = new DBManager(DataProvider.Oracle)
            //{
            //    ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
            //};

            //dBManager.Open();

            //string str = "select DESCRIPTION,PLAN_ID, TOTAL_AMOUNT from ENSERV.TBL_PAYMENTPLAN";

            //DataSet ds = dBManager.ExecuteDataSet(CommandType.Text, str);
            //dBManager.Close();
            //dBManager.Dispose();

            //MAPPaymentPlans py = new MAPPaymentPlans();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        py = new MAPPaymentPlans(); 
            //        py.PaymentScheduleName = ds.Tables[0].Rows[i]["DESCRIPTION"].ToString();
            //        py.PaymentScheduleValue = ds.Tables[0].Rows[i]["PLAN_ID"].ToString();
            //        py.TOTAL_AMOUNT = ds.Tables[0].Rows[i]["TOTAL_AMOUNT"].ToString();
            //        MAPPay.Add(py);
            //    }
            //}
 

            UserViewModel userViewModel = new UserViewModel()
            {
                customer = new CUSTOMER(),
                StateList = this.db.States.ToList<STATE>(),
                LGAList = this.db.LGAs.ToList<LGA>(),
                MeterRepaymentList = db.METER_REPAYMENT_PLANs.ToList(),
                PaymentScheduleList = db.PaymentSchedules.ToList()
            };


            return base.Json(JsonConvert.SerializeObject(userViewModel), JsonRequestBehavior.AllowGet);
        }

        public string CreateHash(string message, string Key)
        {
            string str;
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            aSCIIEncoding.GetBytes(Key);
            byte[] numArray = MAPRegisterController.FromHex(Key);
            byte[] bytes = aSCIIEncoding.GetBytes(message);
            HMACSHA256 hMACSHA256 = new HMACSHA256(numArray);
            try
            {
                str = MAPRegisterController.ByteToString(hMACSHA256.ComputeHash(bytes));
            }
            finally
            {
                if (hMACSHA256 != null)
                {
                    ((IDisposable)hMACSHA256).Dispose();
                }
            }
            return str;
        }

        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] num = new byte[hex.Length / 2];
            for (int i = 0; i < (int)num.Length; i++)
            {
                num[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return num;
        }

        private string getMapVendor(string Zone)
        { 
            string _SinglePhaseAmount = ConfigurationManager.AppSettings["ARMESE"].ToString();
             
            if (_SinglePhaseAmount.Contains(Zone))
            {
                return "HOLLEY";
            }
            else
            {
                return "ARMESE";
            }

            return "ARMESE";
        }
         

        public string Hasher(string message, string key)
        {
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = aSCIIEncoding.GetBytes(key);
            HMACMD5 hMACMD5 = new HMACMD5(bytes);
            HMACSHA1 hMACSHA1 = new HMACSHA1(bytes);
            HMACSHA256 hMACSHA256 = new HMACSHA256(bytes);
            byte[] numArray = aSCIIEncoding.GetBytes(message);
            hMACMD5.ComputeHash(numArray);
            return MAPRegisterController.ByteToStrings(hMACSHA256.ComputeHash(numArray));
        }


        public ActionResult Index()
        {
            return base.View();
        }


        private void SendEmail(string EmailId, string MailMessage, string TicketId, string PhoneNumber, string ApplicantName)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage()
            {
                From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                Subject = "Meter Asset Provider Information From PHED",
                IsBodyHtml = true,
                Priority = MailPriority.High
            };
            mailMessage.Bcc.Add("payments@phed.com.ng");
            mailMessage.To.Add(EmailId);
            SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
            {
                Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
            };
            string str = string.Concat("<html><head></head>", "<body>");
            str = string.Concat(str, "<P>Dear ", ApplicantName, ",</P>");
            str = string.Concat(str, " <P> Thank you for your taking the time to complete the request for a meter. </P>");
            str = string.Concat(str, " <P> Please visit our website map.nepamsonline.com to know the status of your application. </P>");
            str = string.Concat(str, " <P> Customer's Name: ", ApplicantName, " </P>");
            object[] objArray = new object[] { str, " <P> Date: ", null, null };
            DateTime now = DateTime.Now;
            objArray[2] = now.AddHours(8);
            objArray[3] = " </P>";
            str = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
            str = string.Concat(str, " <P> TicketID: ", TicketId, " </P>");
            str = string.Concat(str, "<br><br>");
            str = string.Concat(str, "Thank you,");
            str = string.Concat(str, " <P> PHED  Metering Team </P> ");
            mailMessage.Body = string.Concat(str, "<br><br>");
            smtpClient.Send(mailMessage);
        }

        private static void SendSMS(string smsMessage, string managersPhoneNose)
        {
            string item = ConfigurationManager.AppSettings["SMS_APIKEY"];
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                webClient.QueryString.Add("cmd", "sendmsg");
                webClient.QueryString.Add("sessionid", item);
                webClient.QueryString.Add("message", smsMessage);
                webClient.QueryString.Add("sender", "PHED");
                webClient.QueryString.Add("sendto", managersPhoneNose);
                webClient.QueryString.Add("msgtype", "0");
                StreamReader streamReader = new StreamReader(webClient.OpenRead("http://www.smslive247.com/http/index.aspx"));
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        [HttpGet]
        public JsonResult ShowSuccessDetails(string TicketIDHidden)
        {
            UserViewModel userViewModel = new UserViewModel();
            CustomerPaymentInfo customerPaymentInfo = this.db.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketIDHidden);
            userViewModel.PaymentInfo = customerPaymentInfo;
            return base.Json(JsonConvert.SerializeObject(userViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Success(string hash, string hash_type, string merchant_id, string status_code, string status_msg, string transaction_id)
        {
            string str;
            object[] transactionId;
            hash = base.Request.QueryString["hash"];
            hash_type = base.Request.QueryString["hash-type"];
            merchant_id = base.Request.QueryString["merchant-id"];
            status_code = base.Request.QueryString["status-code"];
            status_msg = base.Request.QueryString["status-msg"];
            transaction_id = base.Request.QueryString["transaction-id"];
            string item = base.Request.QueryString["payment-ref"];
            string str1 = "1d8c210a3b1a5d32496204618cf5bd5a";
            string str2 = "fbf1f5bbf7d4bfcaead84b46022286e4";
            StringBuilder stringBuilder = new StringBuilder();
            string[] merchantId = new string[] { "merchant-id=", merchant_id, "&public-key=", str1, "&transaction-id=", transaction_id };
            stringBuilder.Append(string.Concat(merchantId));
            string str3 = this.CreateHash(stringBuilder.ToString(), str2);
            WebClient webClient = new WebClient();
            try
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection["transaction-id"] = transaction_id;
                nameValueCollection["merchant-id"] = merchant_id;
                nameValueCollection["public-key"] = str1;
                nameValueCollection["hash"] = str3;
                nameValueCollection["hash-type"] = hash_type;
                byte[] numArray = webClient.UploadValues("https://xpresspayonline.com:8000/xp-gateway/ws/v2/query", nameValueCollection);
                string str4 = Encoding.Default.GetString(numArray);
                JObject jObject = new JObject();
                if (MAPRegisterController.TryParseJSON(str4, out jObject))
                {
                     
                    JObject o = JObject.Parse(str4);

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
                            CustomerPaymentInfo CustomerDetails = db.CustomerPaymentInfos.Find(transactionID);
                            ViewBag.CustomerDetails = CustomerDetails;
                            if (CustomerDetails != null && CustomerDetails.BRCPaymentStatus == "NOT PAID")
                            {

                                string Amounts = "";
                                //customer has not been given the Token Yet. 
                                CustomerDetails.merchantID = merchantID;
                                CustomerDetails.Amount = amountInNaira;
                                CustomerDetails.BRCPaymentStatus = "PAID";
                                CustomerDetails.Hash = _hash;
                                CustomerDetails.DatePaid = Convert.ToDateTime(transactionDate);
                                CustomerDetails.CardType = type;
                                CustomerDetails.CardBrand = brand;
                                CustomerDetails.PaymentReference = paymentReference;
                                CustomerDetails.TransactionProcessDate = transactionProccessDate;
                                CustomerDetails.PaymentStatus = "SUCCESS";
                                db.Entry(CustomerDetails).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                ViewBag.PaymentStatus = "BRCPAID";
                            }
                            else if (CustomerDetails != null && CustomerDetails.BRCPaymentStatus == "PAID")
                            {
                                string Amounts = "";
                                //customer has not been given the Token Yet. 
                                CustomerDetails.merchantID = merchantID;
                                CustomerDetails.Amount = amountInNaira;
                                CustomerDetails.MAPPaymentStatus = "PAID";
                                CustomerDetails.Hash = _hash;
                                CustomerDetails.DatePaid = Convert.ToDateTime(transactionDate);
                                CustomerDetails.CardType = type;
                                CustomerDetails.CardBrand = brand;
                                CustomerDetails.PaymentReference = paymentReference;
                                CustomerDetails.TransactionProcessDate = transactionProccessDate;
                                CustomerDetails.PaymentStatus = "SUCCESS";
                                db.Entry(CustomerDetails).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                ViewBag.PaymentStatus = "MAPPAID";
                            }
                        }
                }
            }
            finally
            {
                if (webClient != null)
                {
                    ((IDisposable)webClient).Dispose();
                }
            }
            DbSet<CustomerPaymentInfo> dbSet = this.db.CustomerPaymentInfos;
            transactionId = new object[] { transaction_id };
            dbSet.Find(transactionId);
            ((dynamic)base.ViewBag).CustomerDetails2 = transaction_id;
            return base.View("SuccessPage");
        }

        public ActionResult SuccessPage()
        {
            return base.View();
        }

        private static bool TryParseJSON(string json, out JObject jObject)
        {
            bool flag;
            try
            {
                jObject = JObject.Parse(json);
                flag = true;
            }
            catch
            {
                jObject = null;
                flag = false;
            }
            return flag;
        }

        [HttpGet]
        public JsonResult UpdateApplicationStatus(string Status, string TicketId)
        {
            UserViewModel userViewModel = new UserViewModel();
            ActionMessagePerformed actionMessagePerformed = new ActionMessagePerformed();
            CustomerPaymentInfo status = this.db.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (status != null)
            {
                status.MAPApplicationStatus = Status;
                this.db.Entry(status).State= EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(JsonConvert.SerializeObject(userViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdatePercentage(string Percentage, string TicketId)
        {
            UserViewModel userViewModel = new UserViewModel();
            CustomerPaymentInfo percentage = this.db.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (percentage != null)
            {
                percentage.ArrearsPercent = Percentage;
                percentage.MAPApplicationStatus = "ABOUTTOAPPLY";
                this.db.Entry(percentage).State  = EntityState.Modified;
               
                this.db.SaveChanges();
            }
            return base.Json(JsonConvert.SerializeObject(userViewModel), JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult SchedulePaymentEvent(string MAPPaymentPlan, string MAPPaymentPhase)
        {
            UserViewModel userViewModel = new UserViewModel();
             List<MAPPaymentPlans> MAPPay = new List<MAPPaymentPlans>();
            string phase = "";
            if (MAPPaymentPhase == "SINGLE_PHASE")
            {
                phase = "SINGLEPHASE";

            }
            else
            {
                phase = "THREEPHASE";

            }

            //get Paymetn Schedule List


            userViewModel.MeterRepaymentList = db.METER_REPAYMENT_PLANs.Where(p => p.Repayment_Plan_Phase == phase && p.Repayment_MAP_Plan == MAPPaymentPlan).ToList();

            return base.Json(JsonConvert.SerializeObject(userViewModel), JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public async Task<JsonResult> UploadAgency(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            string str;
            JsonResult nullable;
            JsonResult jsonResult;
            bool flag;
            bool flag1;
            bool flag2;
            bool flag3;
            string PurposeOfPayment = "";
            Guid.NewGuid().ToString();
            UserViewModel userViewModel = new UserViewModel();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                HttpPostedFile httpPostedFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string item1 = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string str1 = System.Web.HttpContext.Current.Request.Params["TellerNo"];
                string item2 = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                string str2 = System.Web.HttpContext.Current.Request.Params["DatePaid"];
                string item3 = System.Web.HttpContext.Current.Request.Params["TicketId"];
                string str3 = System.Web.HttpContext.Current.Request.Params["BankName"];
                string item4 = System.Web.HttpContext.Current.Request.Params["AmountPaid"];
                PurposeOfPayment  = System.Web.HttpContext.Current.Request.Params["PurposeOfPayment"];
                DbSet<CustomerPaymentInfo> customerPaymentInfos = this.db.CustomerPaymentInfos;

                decimal divide = 0;

           

                CustomerPaymentInfo customerPaymentInfo = customerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == item3);


                if (customerPaymentInfo.MAPPlan == "50UPFRONT")
                {
                    divide = 2;
                }
                if (customerPaymentInfo.MAPPlan == "75UPFRONT")
                {
                    divide = 1.33333M;
                }
                if (customerPaymentInfo.MAPPlan == "25UPFRONT")
                {
                    divide = 4;
                }
                if (customerPaymentInfo.MAPPlan == "100UPFRONT")
                {
                    divide = 1;
                }
                if (customerPaymentInfo.MAPPlan == "0UPFRONT")
                {
                    if (customerPaymentInfo.MeterPhase.Trim() == "SINGLE PHASE")
                    {
                        divide = SinglePhaseAmount;
                    }
                    if (customerPaymentInfo.MeterPhase.Trim() == "THREE PHASE")
                    {
                        divide = ThreePhaseAmount;
                    }
                }

                //check what is being Uploaded











                if (customerPaymentInfo != null)
                {
                    if (PurposeOfPayment == "METER PAYMENT")
                    {
                        if (string.IsNullOrEmpty(customerPaymentInfo.MAPPlan) || string.IsNullOrEmpty(customerPaymentInfo.MAPAmount))
                        {
                            str = JsonConvert.SerializeObject(userViewModel);
                            nullable = base.Json(new { result = str, error = "Hey! It seems you have not applied for a meter. Please apply for a meter before you can proceed with the Payment Upload. Thank you." }, JsonRequestBehavior.AllowGet);
                            nullable.MaxJsonLength = new int?(2147483647);
                            jsonResult = nullable;
                            return jsonResult;

                        }




                        flag = (!(customerPaymentInfo.MeterPhase.ToString().Trim() == "THREE PHASE") || !(customerPaymentInfo.MAPPlan == "50UPFRONT" || customerPaymentInfo.MAPPlan == "75UPFRONT" || customerPaymentInfo.MAPPlan == "25UPFRONT" || customerPaymentInfo.MAPPlan == "100UPFRONT"|| customerPaymentInfo.MAPPlan == "0UPFRONT") ? true : !(Convert.ToDecimal(item4) < (this.ThreePhaseAmount / divide)));
                        if (flag)
                        {
                            flag1 = (!(customerPaymentInfo.MeterPhase.ToString().Trim() == "SINGLE PHASE") || !(customerPaymentInfo.MAPPlan == "50UPFRONT" || customerPaymentInfo.MAPPlan == "75UPFRONT" || customerPaymentInfo.MAPPlan == "25UPFRONT" || customerPaymentInfo.MAPPlan == "100UPFRONT"|| customerPaymentInfo.MAPPlan == "0UPFRONT") ? true : !(Convert.ToDecimal(item4) < (this.SinglePhaseAmount / divide)));
                            if (flag1)
                            {
                                flag2 = (!(customerPaymentInfo.MeterPhase.ToString().Trim() == "THREE PHASE") || !(customerPaymentInfo.MAPPlan.Trim() == "UPFRONT") ? true : !(Convert.ToDecimal(item4) < this.ThreePhaseAmount));
                                if (flag2)
                                {
                                    flag3 = (!(customerPaymentInfo.MeterPhase.ToString().Trim() == "SINGLE PHASE") || !(customerPaymentInfo.MAPPlan.Trim() == "UPFRONT") ? true : !(Convert.ToDecimal(item4) < this.SinglePhaseAmount));
                                    if (flag3)
                                    {
                                        MAPPayment mAPPayment = new MAPPayment()
                                        {
                                            DatePaid = str2.ToString(),
                                            DocumentPath = string.Concat("/Documents/", item1),
                                            TellerNo = str1.ToString(),
                                            Amount = item4.ToString(),
                                            ApprovalStatus = "NOTAPPROVED",
                                            BSC = customerPaymentInfo.BSC,
                                            IBC = customerPaymentInfo.IBC,
                                            AccountNo = customerPaymentInfo.CustomerReference,
                                            CustomerName = customerPaymentInfo.CustomerName,
                                            PaymentMode = "BANK",
                                            ReceiptNo = str1.ToString(),
                                            PaymentStatus = "PAID",
                                            PaymentFor = PurposeOfPayment,
                                            Phase = customerPaymentInfo.MeterPhase,
                                            TransRef = str1.ToString(),
                                            TicketId = item3.ToString(),
                                            PaymentId = Guid.NewGuid().ToString()
                                        };
                                        this.db.MAPPayments.Add(mAPPayment);
                                        this.db.SaveChanges();
                                        string str4 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), item1);
                                        item.SaveAs(str4);
                                    }
                                    else
                                    {
                                        str = JsonConvert.SerializeObject(userViewModel);
                                        nullable = base.Json(new { result = str, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Single Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                                        nullable.MaxJsonLength = new int?(2147483647);
                                        jsonResult = nullable;
                                        return jsonResult;
                                    }
                                }
                                else
                                {
                                    str = JsonConvert.SerializeObject(userViewModel);
                                    nullable = base.Json(new { result = str, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Three Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                                    nullable.MaxJsonLength = new int?(2147483647);
                                    jsonResult = nullable;
                                    return jsonResult;
                                }
                            }
                            else
                            {
                                str = JsonConvert.SerializeObject(userViewModel);
                                nullable = base.Json(new { result = str, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Single Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                                nullable.MaxJsonLength = new int?(2147483647);
                                jsonResult = nullable;
                                return jsonResult;
                            }
                        }
                        else
                        {
                            str = JsonConvert.SerializeObject(userViewModel);
                            nullable = base.Json(new { result = str, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Three Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                            nullable.MaxJsonLength = new int?(2147483647);
                            jsonResult = nullable;
                            return jsonResult;
                        }
                    }

                    if (PurposeOfPayment == "UPGRADE TO 3PHASE")
                    {

                        var check = db.MAP_METER_UPGRADEs.FirstOrDefault(p => p.TICKET_ID == item3);
                        if (check == null)
                        {
                            str = JsonConvert.SerializeObject(userViewModel);
                            nullable = base.Json(new { result = str, error = "Hey! It seems you have not applied for a meter Upgrade. Please apply for a meter upgrede before you can proceed with the Payment Upload. Thank you." }, JsonRequestBehavior.AllowGet);
                            nullable.MaxJsonLength = new int?(2147483647);
                            jsonResult = nullable;
                            return jsonResult;
                        }


                        MAPPayment mAPPayment = new MAPPayment()
                        {
                            DatePaid = str2.ToString(),
                            DocumentPath = string.Concat("/Documents/", item1),
                            TellerNo = str1.ToString(),
                            Amount = item4.ToString(),
                            ApprovalStatus = "NOTAPPROVED",
                            BSC = customerPaymentInfo.BSC,
                            IBC = customerPaymentInfo.IBC,
                            AccountNo = customerPaymentInfo.CustomerReference,
                            CustomerName = customerPaymentInfo.CustomerName,
                            PaymentMode = "BANK",
                            ReceiptNo = str1.ToString(),
                            PaymentStatus = "PAID",
                            PaymentFor = PurposeOfPayment,
                            Phase = customerPaymentInfo.MeterPhase,
                            TransRef = str1.ToString(),
                            TicketId = item3.ToString(),
                            PaymentId = Guid.NewGuid().ToString(),

                        };
                        this.db.MAPPayments.Add(mAPPayment);
                        this.db.SaveChanges();
                        string str4 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), item1);
                        item.SaveAs(str4);


                    }

                    if (PurposeOfPayment == "ARREARS PAYMENT")
                    {

                        MAPPayment mAPPayment = new MAPPayment()
                        {
                            DatePaid = str2.ToString(),
                            DocumentPath = string.Concat("/Documents/", item1),
                            TellerNo = str1.ToString(),
                            Amount = item4.ToString(),
                            ApprovalStatus = "NOTAPPROVED",
                            BSC = customerPaymentInfo.BSC,
                            IBC = customerPaymentInfo.IBC,
                            AccountNo = customerPaymentInfo.CustomerReference,
                            CustomerName = customerPaymentInfo.CustomerName,
                            PaymentMode = "BANK",
                            ReceiptNo = str1.ToString(),
                            PaymentStatus = "PAID",
                            PaymentFor = PurposeOfPayment,
                            Phase = customerPaymentInfo.MeterPhase,
                            TransRef = str1.ToString(),
                            TicketId = item3.ToString(),
                            PaymentId = Guid.NewGuid().ToString(),

                        };
                        this.db.MAPPayments.Add(mAPPayment);
                        this.db.SaveChanges();
                        string str4 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), item1);
                        item.SaveAs(str4);
                    }
                }
                else
                {
                    str = JsonConvert.SerializeObject(userViewModel);
                    nullable = base.Json(new { result = str, error = "The Ticket Id you're quoting is wrong. Please input the Correct Ticket Id to Proceed. Please try again" }, JsonRequestBehavior.AllowGet);
                    nullable.MaxJsonLength = new int?(2147483647);
                    jsonResult = nullable;
                    return jsonResult;
                }
            }
            string str5 = JsonConvert.SerializeObject(userViewModel);
            JsonResult nullable1 = base.Json(new { result = str5, error = "" }, JsonRequestBehavior.AllowGet);
            nullable1.MaxJsonLength = new int?(2147483647);
            jsonResult = nullable1;
            return jsonResult;
        }

        public ActionResult UploadPayment()
        {
            return base.View();
        }

        [HttpGet]
        public async Task<JsonResult> v(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            string str;
            JsonResult jsonResult;
            JsonResult jsonResult1;
            string[] phoneNumber;
            string accountNo = AccountNo;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string str1 = "";
            string str2 = "00024";
            string str3 = "http://map.nepamsonline.com/successPage.aspx";
            DbSet<CustomerPaymentInfo> customerPaymentInfos = this.db.CustomerPaymentInfos;
            List<CustomerPaymentInfo> list = (
                from p in customerPaymentInfos
                where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                select p).ToList<CustomerPaymentInfo>();
            string customerEmail = "";
            string customerPhoneNumber = "";
            string alternateCustReference = "";
            string transactionID = RandomPassword.Generate(10).ToString();
            string email = EmailAddress.Trim();
            string str4 = accountNo;
            string customerReference = "";
            string bC = "";
            string bSC = "";
            if (list.Count > 0)
            {
                accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                customerReference = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                EmailAddress = customerEmail;
                email = customerEmail;
                customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                bC = list.FirstOrDefault<CustomerPaymentInfo>().IBC;
                bSC = list.FirstOrDefault<CustomerPaymentInfo>().BSC;
            }
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                phoneNumber = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", str4, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
                streamWriter.Write(string.Concat(phoneNumber));
                streamWriter.Flush();
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            string str5 = "NGN";
            string str6 = "1d8c210a3b1a5d32496204618cf5bd5a";
            string str7 = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string str8 = "MAP/Arrears Meter Payment";
            string str9 = "78";
            StringBuilder stringBuilder = new StringBuilder();
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string end = streamReader.ReadToEnd();
                end = end.Replace("\r", string.Empty);
                end = end.Replace("\n", string.Empty);
                end = end.Replace("\\", string.Empty);
                end = end.Replace("\\\\", string.Empty);
                if (!(end == "Customer Not Found"))
                {
                    List<DirectJSON> directJSONs = JsonConvert.DeserializeObject<List<DirectJSON>>(end);
                    AmountPaid = directJSONs[0].ARREAR;
                    if (list.Count <= 0)
                    {
                        customerReference = directJSONs[0].CUSTOMER_NO;
                    }
                    string str10 = "WEB";
                    string str11 = "WEB";
                    string str12 = "Debit Card";
                    string str13 = "PHED";
                    string str14 = "NGN";
                    string str15 = "Arrears Payment";
                    string str16 = "ARREARS";
                    string str17 = "ARREARSBillClearance";
                    string str18 = "PHED Bill Payment";
                    string str19 = "PENDING";
                    string str20 = "false";
                    string str21 = "";
                    string meterType = "";
                    string str22 = "";
                    string str23 = "";
                    string str24 = "";
                    DataSet dataSet = new DataSet();
                    DeliveredBills deliveredBill = new DeliveredBills();
                    List<DeliveredBills> deliveredBills = new List<DeliveredBills>();
                    string str25 = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
                    DataSet dataSet1 = new DataSet();
                    string str26 = ""; string DTR = "";

                    str26 = string.Concat("select DTR_Name, Feeder33Name, CIN, Zone from  EnumerationFeederMapping where (AccountNo = '", customerReference, "')");

                    (new SqlDataAdapter(str26, str25)).Fill(dataSet1);
                   
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        bSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                        bC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                        DTR = dataSet1.Tables[0].Rows[0]["DTR_Name"].ToString();
                    }

                    if (list.Count <= 0)
                    {
                        MailMessage mailMessage = new MailMessage()
                        {
                            From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                            Subject = "Meter Asset Provider Information From PHED",
                            IsBodyHtml = true,
                            Priority = MailPriority.High
                        };
                        mailMessage.Bcc.Add("payments@phed.com.ng");
                        mailMessage.To.Add(email);
                        SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                        {
                            Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                        };
                        string str27 = string.Concat("<html><head></head>", "<body>");
                        str27 = string.Concat(str27, "<P>Dear ", MAPApplicantName, ",</P>");
                        str27 = string.Concat(str27, " <P> Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID </P>");
                        str27 = string.Concat(str27, " <P> Customer's Name: ", MAPApplicantName, " </P>");
                        object[] objArray = new object[] { str27, " <P> Visit Date: ", null, null };
                        DateTime now = DateTime.Now;
                        objArray[2] = now.AddHours(8);
                        objArray[3] = " </P>";
                        str27 = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
                        str27 = string.Concat(str27, " <P> TicketID: ", transactionID, " </P>");
                        str27 = string.Concat(str27, " <P> Account No: ", accountNo, " </P>");
                        str27 = string.Concat(str27, "<br><br>");
                        str27 = string.Concat(str27, "Thank you,");
                        str27 = string.Concat(str27, " <P> PHED Team </P> ");
                        mailMessage.Body = string.Concat(str27, "<br><br>");
                        try
                        {
                            smtpClient.Send(mailMessage);
                        }
                        catch (Exception exception)
                        {
                        }
                        CustomerPaymentInfo customerPaymentInfo = new CustomerPaymentInfo()
                        {
                            Amount = AmountPaid,
                            ItemAmount = AmountPaid,
                            ItemCode = str9,
                            TransactionID = transactionID,
                            PaymentMethod = "WEB",
                            CustomerName = directJSONs[0].CONS_NAME
                        };
                        string cONSNAME = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.CustomerEmail = email;
                        customerPaymentInfo.CustomerPhoneNumber = PhoneNumber;
                        customerPaymentInfo.DepositorName = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.DepositSlipNumber = transactionID;
                        customerPaymentInfo.InstitutionId = str13;
                        customerPaymentInfo.InstitutionName = str17;
                        customerPaymentInfo.ItemName = str18;
                        customerPaymentInfo.ChannelName = str10;
                        customerPaymentInfo.PaymentCurrency = str14;
                        customerPaymentInfo.IsReversal = str20;
                        customerPaymentInfo.MAPVendor = this.getMapVendor(bC);

                        if (dataSet1.Tables[0].Rows.Count > 0)
                        {
                            customerPaymentInfo.IBC_OLD = directJSONs[0].IBC_NAME;
                            customerPaymentInfo.BSC_OLD = directJSONs[0].BSC_NAME;
                            customerPaymentInfo.IBC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                            customerPaymentInfo.BSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                            customerPaymentInfo.CIN = dataSet1.Tables[0].Rows[0]["CIN"].ToString();
                            customerPaymentInfo.DTR_NAME = dataSet1.Tables[0].Rows[0]["DTR_Name"].ToString();
                        }
                        customerPaymentInfo.PaymentStatus = str19;
                        customerPaymentInfo.BankName = str15;
                        customerPaymentInfo.BranchName = str16;
                        customerPaymentInfo.Location = str11;
                        customerPaymentInfo.CustomerAddress = directJSONs[0].ADDRESS;
                        customerPaymentInfo.CustomerReference = directJSONs[0].CUSTOMER_NO;
                        if (string.IsNullOrEmpty(directJSONs[0].METER_NO))
                        {
                            customerPaymentInfo.AlternateCustReference = "NOMETER";
                        }
                        else
                        {
                            customerPaymentInfo.AlternateCustReference = directJSONs[0].METER_NO;
                        }
                        string str28 = string.Concat("MAP", RandomPassword.Generate(10).ToString());
                        customerPaymentInfo.BRC_ID = str28;
                        customerPaymentInfo.Token = "MAP";
                        customerPaymentInfo.PaymentMethod = str12;
                        customerPaymentInfo.MAPApplicationStatus = "VERIFY";
                        customerPaymentInfo.MAPPaymentStatus = "NOT PAID";
                        customerPaymentInfo.BRCPaymentStatus = "NOT PAID";
                        customerPaymentInfo.PaymentStatus = "NOT PAID";
                        customerPaymentInfo.MAPCustomerName = MAPApplicantName;
                        customerPaymentInfo.AccountType = AccountType.ToUpper();
                        customerPaymentInfo.TransactionProcessDate = DateTime.Now.ToShortDateString();
                        customerPaymentInfo.BRC_Arrears = directJSONs[0].ARREAR;
                        customerPaymentInfo.DTR_NAME = DTR;
                        this.db.CustomerPaymentInfos.Add(customerPaymentInfo);
                        this.db.SaveChanges();
                        str24 = "VERIFY";
                        DbSet<CustomerPaymentInfo> dbSet = this.db.CustomerPaymentInfos;
                        list = (
                            from p in dbSet
                            where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                            select p).ToList<CustomerPaymentInfo>();
                        str = "";
                        if (list.Count > 0)
                        {
                            accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                            customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                            email = customerEmail;
                            AccountType = list.FirstOrDefault<CustomerPaymentInfo>().AccountType;
                            customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                            alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                            transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            str = RandomPassword.Generate(10).ToString();

                            MAPPayment p = new MAPPayment();
                            p.PaymentFor = "METER";
                            p.TicketId = transactionID;
                            p.TransRef = str;
                            p.AccountNo = accountNo;
                            p.Amount = list.FirstOrDefault().Amount;
                            p.BSC = list.FirstOrDefault().BSC;
                            p.IBC = list.FirstOrDefault().IBC;
                            p.PaymentMode = "WEB";
                            p.PaymentStatus = "NOTPAID";
                            p.Phase = list.FirstOrDefault().MeterPhase;
                            p.IBC_OLD = directJSONs[0].IBC_NAME;
                            p.BSC_OLD = directJSONs[0].BSC_NAME;
                            p.IBC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                            p.BSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                            p.CIN = dataSet1.Tables[0].Rows[0]["CIN"].ToString();
                            p.DTR_NAME = dataSet1.Tables[0].Rows[0]["DTR_Name"].ToString();
                           
                            db.MAPPayments.Add(p);
                            db.SaveChanges();

                        }
                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", str };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        str21 = this.CreateHash(stringBuilder.ToString(), str7);
                        jsonResult = base.Json(new { Zone = bC, Feeder = bSC, DTR = DTR, AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str23, MeterCost = str22, Status = str24, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str21.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str9, trans_id = transactionID, ProductDescription = str8, PublicKey = str6, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                    else
                    {
                        transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                        str = RandomPassword.Generate(10);

                      








                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", str };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        //MAPPayment p = new MAPPayment();
                        //p.PaymentFor = "METER";
                        //p.TicketId = transactionID;
                        //p.TransRef = str;
                        //p.AccountNo = accountNo;
                        //p.Amount = AmountPaid;
                        //p.BSC = list.FirstOrDefault().BSC;
                        //p.IBC = list.FirstOrDefault().IBC;
                        //p.PaymentMode = "WEB";
                        //p.PaymentStatus = "NOTPAID";
                        //p.Phase = list.FirstOrDefault().MeterPhase;
                        //db.MAPPayments.Add(p);
                        //db.SaveChanges();
                        
                        MAPPayment mAPPayment = new MAPPayment()
                        {
                            PaymentFor = "BRC",
                            TicketId = transactionID,
                            TransRef = str,
                            AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                            Amount = AmountPaid
                        };
                        if (dataSet1.Tables[0].Rows.Count > 0)
                        {
                            mAPPayment.IBC_OLD = directJSONs[0].IBC_NAME;
                            mAPPayment.BSC_OLD = directJSONs[0].BSC_NAME;
                            mAPPayment.IBC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                            mAPPayment.BSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                            mAPPayment.CIN = dataSet1.Tables[0].Rows[0]["CIN"].ToString();
                            mAPPayment.DTR_NAME = dataSet1.Tables[0].Rows[0]["DTR_Name"].ToString();
                            bSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                            bC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                        }
                        mAPPayment.PaymentMode = "WEB";
                        mAPPayment.PaymentStatus = "NOTPAID";
                        mAPPayment.Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase;
                        this.db.MAPPayments.Add(mAPPayment);
                        this.db.SaveChanges();
                        str21 = this.CreateHash(stringBuilder.ToString(), str7);
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                        {
                            str24 = "APPROVED FOR PAYMENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ABOUTTOAPPLY")
                        {
                            str24 = "ABOUTTOAPPLY";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                        {
                            string transactionID1 = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            DbSet<CUSTOMER> cUSTOMERS = this.db.CUSTOMERS;
                            CUSTOMER cUSTOMER = cUSTOMERS.FirstOrDefault<CUSTOMER>((CUSTOMER p) => p.TransactionID == transactionID1);
                            if (cUSTOMER != null)
                            {
                                meterType = cUSTOMER.MeterType;
                                if (!string.IsNullOrEmpty(cUSTOMER.Email))
                                {
                                    EmailAddress = cUSTOMER.Email;
                                    email = cUSTOMER.Email;
                                }
                                else
                                {
                                    EmailAddress = "map@phed.com.ng";
                                    email = "map@phed.com.ng";
                                }
                                if (meterType.Trim() == "SINGLE PHASE")
                                {
                                    str22 = this.SinglePhaseAmount.ToString();
                                    AmountPaid = this.SinglePhaseAmount.ToString();
                                }
                                if (meterType.Trim() == "THREE PHASE")
                                {
                                    str22 = this.ThreePhaseAmount.ToString();
                                    AmountPaid = this.ThreePhaseAmount.ToString();
                                }
                                phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", str };
                                stringBuilder.Append(string.Concat(phoneNumber));
                                str21 = this.CreateHash(stringBuilder.ToString(), str7);
                                str23 = str;
                                MAPPayment meterPhase = new MAPPayment()
                                {
                                    PaymentFor = "METER",
                                    TicketId = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID,
                                    TransRef = str,
                                    AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                                    Amount = str22
                                };
                                if (dataSet1.Tables[0].Rows.Count > 0)
                                {
                                    mAPPayment.IBC_OLD = directJSONs[0].IBC_NAME;
                                    mAPPayment.BSC_OLD = directJSONs[0].BSC_NAME;
                                    mAPPayment.IBC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                                    mAPPayment.BSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                                    mAPPayment.CIN = dataSet1.Tables[0].Rows[0]["CIN"].ToString();
                                    mAPPayment.DTR_NAME = dataSet1.Tables[0].Rows[0]["DTR_Name"].ToString();
                                    bSC = dataSet1.Tables[0].Rows[0]["Feeder33Name"].ToString();
                                    bC = dataSet1.Tables[0].Rows[0]["Zone"].ToString();
                                }
                                meterPhase.PaymentMode = "WEB";
                                meterPhase.PaymentStatus = "NOTPAID";
                                meterPhase.Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase;
                                this.db.MAPPayments.Add(meterPhase);
                                this.db.SaveChanges();
                                str24 = "PROCEEDTOMAPPAY";
                            }
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "HASARREARS")
                        {
                            str24 = "HASARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ARREARSPERCENT")
                        {
                            str24 = "ARREARSPERCENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "MAPAPPLIED")
                        {
                            str24 = "MAPAPPLIED";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAYARREARS")
                        {
                            str24 = "PAYARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str24 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAID FOR METER")
                        {
                            str24 = "PAID FOR METER";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str24 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "BRCDONE")
                        {
                            str24 = "BRCDONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLMENTAL")
                        {
                            str24 = "INSTALLMENTAL";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                        {
                            str24 = "APPROVED FOR INSTALLATION";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLATION DONE")
                        {
                            str24 = "INSTALLATION DONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "VERIFY")
                        {
                            str24 = "VERIFY";
                        }
                        jsonResult = base.Json(new { Zone = bC, DTR = DTR, Feeder = bSC, AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str23, MeterCost = str22, Status = str24, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str21.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str9, trans_id = transactionID, ProductDescription = str8, PublicKey = str6, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                }
                else
                {
                    jsonResult1 = base.Json(new { result = end }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            return jsonResult1;
        }

        [HttpGet]
        public async Task<JsonResult> w(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            string str;
            JsonResult jsonResult;
            JsonResult jsonResult1;
            string[] phoneNumber;
            string accountNo = AccountNo;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string str1 = "";
            string str2 = "00024";
            string str3 = "http://map.nepamsonline.com/successPage.aspx";
            DbSet<CustomerPaymentInfo> customerPaymentInfos = this.db.CustomerPaymentInfos;
            List<CustomerPaymentInfo> list = (
                from p in customerPaymentInfos
                where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                select p).ToList<CustomerPaymentInfo>();
            string customerEmail = "";
            string customerPhoneNumber = "";
            string alternateCustReference = "";
            string transactionID = RandomPassword.Generate(10).ToString();
            string email = EmailAddress.Trim();
            string str4 = "";
            if (list.Count > 0)
            {
                accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                EmailAddress = customerEmail;
                email = customerEmail;
                customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                AccountType = list.FirstOrDefault<CustomerPaymentInfo>().AccountType.ToUpper();
                str4 = (!(AccountType == "PREPAID") ? list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference : list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference);
            }
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                phoneNumber = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", str4, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
                streamWriter.Write(string.Concat(phoneNumber));
                streamWriter.Flush();
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            string str5 = "NGN";
            string str6 = "1d8c210a3b1a5d32496204618cf5bd5a";
            string str7 = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string str8 = "MAP/Arrears Meter Payment";
            string str9 = "78";
            StringBuilder stringBuilder = new StringBuilder();
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string end = streamReader.ReadToEnd();
                end = end.Replace("\r", string.Empty);
                end = end.Replace("\n", string.Empty);
                end = end.Replace("\\", string.Empty);
                end = end.Replace("\\\\", string.Empty);
                if (!(end == "Customer Not Found"))
                {
                    List<DirectJSON> directJSONs = JsonConvert.DeserializeObject<List<DirectJSON>>(end);
                    AmountPaid = directJSONs[0].ARREAR;
                    string str10 = "WEB";
                    string str11 = "WEB";
                    string str12 = "Debit Card";
                    string str13 = "PHED";
                    string str14 = "NGN";
                    string str15 = "Arrears Payment";
                    string str16 = "ARREARS";
                    string str17 = "ARREARSBillClearance";
                    string str18 = "PHED Bill Payment";
                    string str19 = "PENDING";
                    string str20 = "false";
                    string str21 = "";
                    string meterType = "";
                    string str22 = "";
                    string str23 = "";
                    string str24 = "";
                    if (list.Count <= 0)
                    {
                        MailMessage mailMessage = new MailMessage()
                        {
                            From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                            Subject = "Meter Asset Provider Information From PHED",
                            IsBodyHtml = true,
                            Priority = MailPriority.High
                        };
                        mailMessage.Bcc.Add("payments@phed.com.ng");
                        mailMessage.To.Add(email);
                        SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                        {
                            Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                        };
                        string str25 = string.Concat("<html><head></head>", "<body>");
                        str25 = string.Concat(str25, "<P>Dear ", MAPApplicantName, ",</P>");
                        str25 = string.Concat(str25, " <P> Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID </P>");
                        str25 = string.Concat(str25, " <P> Customer's Name: ", MAPApplicantName, " </P>");
                        object[] objArray = new object[] { str25, " <P> Visit Date: ", null, null };
                        DateTime now = DateTime.Now;
                        objArray[2] = now.AddHours(8);
                        objArray[3] = " </P>";
                        str25 = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
                        str25 = string.Concat(str25, " <P> TicketID: ", transactionID, " </P>");
                        str25 = string.Concat(str25, " <P> Account No: ", accountNo, " </P>");
                        str25 = string.Concat(str25, "<br><br>");
                        str25 = string.Concat(str25, "Thank you,");
                        str25 = string.Concat(str25, " <P> PHED Team </P> ");
                        mailMessage.Body = string.Concat(str25, "<br><br>");
                        smtpClient.Send(mailMessage);
                        CustomerPaymentInfo customerPaymentInfo = new CustomerPaymentInfo()
                        {
                            Amount = AmountPaid,
                            ItemAmount = AmountPaid,
                            ItemCode = str9,
                            TransactionID = transactionID,
                            PaymentMethod = "WEB",
                            CustomerName = directJSONs[0].CONS_NAME
                        };
                        string cONSNAME = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.CustomerEmail = email;
                        customerPaymentInfo.CustomerPhoneNumber = PhoneNumber;
                        customerPaymentInfo.DepositorName = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.DepositSlipNumber = transactionID;
                        customerPaymentInfo.InstitutionId = str13;
                        customerPaymentInfo.InstitutionName = str17;
                        customerPaymentInfo.ItemName = str18;
                        customerPaymentInfo.ChannelName = str10;
                        customerPaymentInfo.PaymentCurrency = str14;
                        customerPaymentInfo.IsReversal = str20;
                        customerPaymentInfo.IBC = directJSONs[0].IBC_NAME;
                        customerPaymentInfo.BSC = directJSONs[0].BSC_NAME;
                        customerPaymentInfo.PaymentStatus = str19;
                        customerPaymentInfo.BankName = str15;
                        customerPaymentInfo.BranchName = str16;
                        customerPaymentInfo.Location = str11;
                        customerPaymentInfo.CustomerAddress = directJSONs[0].ADDRESS;
                        customerPaymentInfo.CustomerReference = directJSONs[0].CUSTOMER_NO;
                        customerPaymentInfo.AlternateCustReference = directJSONs[0].METER_NO;
                        string str26 = string.Concat("MAP", RandomPassword.Generate(10).ToString());
                        customerPaymentInfo.BRC_ID = str26;
                        customerPaymentInfo.Token = "MAP";
                        customerPaymentInfo.PaymentMethod = str12;
                        customerPaymentInfo.MAPApplicationStatus = "VERIFY";
                        customerPaymentInfo.MAPPaymentStatus = "NOT PAID";
                        customerPaymentInfo.BRCPaymentStatus = "NOT PAID";
                        customerPaymentInfo.PaymentStatus = "NOT PAID";
                        customerPaymentInfo.MAPCustomerName = MAPApplicantName;
                        customerPaymentInfo.AccountType = AccountType.ToUpper();
                        customerPaymentInfo.TransactionProcessDate = DateTime.Now.ToShortDateString();
                        customerPaymentInfo.BRC_Arrears = directJSONs[0].ARREAR;
                        this.db.CustomerPaymentInfos.Add(customerPaymentInfo);
                        this.db.SaveChanges();
                        str24 = "VERIFY";
                        DbSet<CustomerPaymentInfo> dbSet = this.db.CustomerPaymentInfos;
                        list = (
                            from p in dbSet
                            where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                            select p).ToList<CustomerPaymentInfo>();
                        str = "";
                        if (list.Count > 0)
                        {
                            accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                            customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                            email = customerEmail;
                            AccountType = list.FirstOrDefault<CustomerPaymentInfo>().AccountType;
                            customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                            alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                            transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            str = RandomPassword.Generate(10).ToString();
                        }
                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", transactionID };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        str21 = this.CreateHash(stringBuilder.ToString(), str7);
                        jsonResult = base.Json(new { AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str23, MeterCost = str22, Status = str24, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str21.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str9, trans_id = transactionID, ProductDescription = str8, PublicKey = str6, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                    else
                    {
                        transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                        str = RandomPassword.Generate(10);
                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", str };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        MAPPayment mAPPayment = new MAPPayment()
                        {
                            PaymentFor = "BRC",
                            TicketId = transactionID,
                            TransRef = str,
                            AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                            Amount = AmountPaid,
                            BSC = list.FirstOrDefault<CustomerPaymentInfo>().BSC,
                            IBC = list.FirstOrDefault<CustomerPaymentInfo>().IBC,
                            PaymentMode = "WEB",
                            PaymentStatus = "NOTPAID",
                            Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase
                        };
                        this.db.MAPPayments.Add(mAPPayment);
                        this.db.SaveChanges();
                        str21 = this.CreateHash(stringBuilder.ToString(), str7);
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                        {
                            str24 = "APPROVED FOR PAYMENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ABOUTTOAPPLY")
                        {
                            str24 = "ABOUTTOAPPLY";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                        {
                            string transactionID1 = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            DbSet<CUSTOMER> cUSTOMERS = this.db.CUSTOMERS;
                            CUSTOMER cUSTOMER = cUSTOMERS.FirstOrDefault<CUSTOMER>((CUSTOMER p) => p.TransactionID == transactionID1);
                            if (cUSTOMER != null)
                            {
                                meterType = cUSTOMER.MeterType;
                                if (!string.IsNullOrEmpty(cUSTOMER.Email))
                                {
                                    EmailAddress = cUSTOMER.Email;
                                    email = cUSTOMER.Email;
                                }
                                else
                                {
                                    EmailAddress = "map@phed.com.ng";
                                    email = "map@phed.com.ng";
                                }
                                if (meterType.Trim() == "SINGLE PHASE")
                                {
                                    str22 = this.SinglePhaseAmount.ToString();
                                    AmountPaid = this.SinglePhaseAmount.ToString();
                                }
                                if (meterType.Trim() == "THREE PHASE")
                                {
                                    str22 = this.ThreePhaseAmount.ToString();
                                    AmountPaid = this.ThreePhaseAmount.ToString();
                                }
                                phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str5, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str8, "&product-id=", str9, "&public-key=", str6, "&trans-id=", str };
                                stringBuilder.Append(string.Concat(phoneNumber));
                                str21 = this.CreateHash(stringBuilder.ToString(), str7);
                                str23 = str;
                                MAPPayment mAPPayment1 = new MAPPayment()
                                {
                                    PaymentFor = "METER",
                                    TicketId = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID,
                                    TransRef = str,
                                    AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                                    Amount = str22,
                                    BSC = list.FirstOrDefault<CustomerPaymentInfo>().BSC,
                                    IBC = list.FirstOrDefault<CustomerPaymentInfo>().IBC,
                                    PaymentMode = "WEB",
                                    PaymentStatus = "NOTPAID",
                                    Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase
                                };
                                this.db.MAPPayments.Add(mAPPayment1);
                                this.db.SaveChanges();
                                str24 = "PROCEEDTOMAPPAY";
                            }
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "HASARREARS")
                        {
                            str24 = "HASARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ARREARSPERCENT")
                        {
                            str24 = "ARREARSPERCENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "MAPAPPLIED")
                        {
                            str24 = "MAPAPPLIED";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAYARREARS")
                        {
                            str24 = "PAYARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str24 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAID FOR METER")
                        {
                            str24 = "PAID FOR METER";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str24 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "BRCDONE")
                        {
                            str24 = "BRCDONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLMENTAL")
                        {
                            str24 = "INSTALLMENTAL";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                        {
                            str24 = "APPROVED FOR INSTALLATION";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLATION DONE")
                        {
                            str24 = "INSTALLATION DONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "MAPPAID")
                        {
                            str24 = "MAPPAID";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "VERIFY")
                        {
                            str24 = "VERIFY";
                        }
                        jsonResult = base.Json(new { AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str23, MeterCost = str22, Status = str24, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str21.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str9, trans_id = transactionID, ProductDescription = str8, PublicKey = str6, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                }
                else
                {
                    jsonResult1 = base.Json(new { result = end }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            return jsonResult1;
        }

        [HttpGet]
        public async Task<JsonResult> ww(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            string str;
            JsonResult jsonResult;
            JsonResult jsonResult1;
            string[] phoneNumber;
            string accountNo = AccountNo;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://dlenhanceuat.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string str1 = "";
            string str2 = "00024";
            string str3 = "http://map.nepamsonline.com/successPage.aspx";
            DbSet<CustomerPaymentInfo> customerPaymentInfos = this.db.CustomerPaymentInfos;
            List<CustomerPaymentInfo> list = (
                from p in customerPaymentInfos
                where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                select p).ToList<CustomerPaymentInfo>();
            string customerEmail = "";
            string customerPhoneNumber = "";
            string alternateCustReference = "";
            string transactionID = RandomPassword.Generate(10).ToString();
            if (list.Count > 0)
            {
                accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                AccountType = list.FirstOrDefault<CustomerPaymentInfo>().AccountType;
                customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
            }
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                phoneNumber = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\",\"CustomerNumber\":\"", accountNo, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
                streamWriter.Write(string.Concat(phoneNumber));
                streamWriter.Flush();
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            string email = EmailAddress.Trim();
            string str4 = "NGN";
            string str5 = "1d8c210a3b1a5d32496204618cf5bd5a";
            string str6 = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string str7 = "MAP/Arrears Meter Payment";
            string str8 = "78";
            StringBuilder stringBuilder = new StringBuilder();
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string end = streamReader.ReadToEnd();
                end = end.Replace("\r", string.Empty);
                end = end.Replace("\n", string.Empty);
                end = end.Replace("\\", string.Empty);
                end = end.Replace("\\\\", string.Empty);
                if (!(end == "Customer Not Found"))
                {
                    List<DirectJSON> directJSONs = JsonConvert.DeserializeObject<List<DirectJSON>>(end);
                    AmountPaid = directJSONs[0].ARREAR;
                    string str9 = "WEB";
                    string str10 = "WEB";
                    string str11 = "Debit Card";
                    string str12 = "PHED";
                    string str13 = "NGN";
                    string str14 = "Arrears Payment";
                    string str15 = "ARREARS";
                    string str16 = "ARREARSBillClearance";
                    string str17 = "PHED Bill Payment";
                    string str18 = "PENDING";
                    string str19 = "false";
                    string str20 = "";
                    string meterType = "";
                    string str21 = "";
                    string str22 = "";
                    string str23 = "";
                    if (list.Count <= 0)
                    {
                        MailMessage mailMessage = new MailMessage()
                        {
                            From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                            Subject = "Meter Asset Provider Information From PHED",
                            IsBodyHtml = true,
                            Priority = MailPriority.High
                        };
                        mailMessage.Bcc.Add("payments@phed.com.ng");
                        mailMessage.To.Add(email);
                        SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                        {
                            Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                        };
                        string str24 = string.Concat("<html><head></head>", "<body>");
                        str24 = string.Concat(str24, "<P>Dear ", MAPApplicantName, ",</P>");
                        str24 = string.Concat(str24, " <P> Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID </P>");
                        str24 = string.Concat(str24, " <P> Customer's Name: ", MAPApplicantName, " </P>");
                        object[] objArray = new object[] { str24, " <P> Visit Date: ", null, null };
                        DateTime now = DateTime.Now;
                        objArray[2] = now.AddHours(8);
                        objArray[3] = " </P>";
                        str24 = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
                        str24 = string.Concat(str24, " <P> TicketID: ", transactionID, " </P>");
                        str24 = string.Concat(str24, " <P> Account No: ", accountNo, " </P>");
                        str24 = string.Concat(str24, "<br><br>");
                        str24 = string.Concat(str24, "Thank you,");
                        str24 = string.Concat(str24, " <P> PHED Team </P> ");
                        mailMessage.Body = string.Concat(str24, "<br><br>");
                        smtpClient.Send(mailMessage);
                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str4, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str7, "&product-id=", str8, "&public-key=", str5, "&trans-id=", transactionID };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        str20 = this.CreateHash(stringBuilder.ToString(), str6);
                        CustomerPaymentInfo customerPaymentInfo = new CustomerPaymentInfo()
                        {
                            Amount = AmountPaid,
                            ItemAmount = AmountPaid,
                            ItemCode = str8,
                            TransactionID = transactionID,
                            PaymentMethod = "WEB",
                            CustomerName = directJSONs[0].CONS_NAME
                        };
                        string cONSNAME = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.CustomerEmail = email;
                        customerPaymentInfo.CustomerPhoneNumber = PhoneNumber;
                        customerPaymentInfo.DepositorName = directJSONs[0].CONS_NAME;
                        customerPaymentInfo.DepositSlipNumber = transactionID;
                        customerPaymentInfo.InstitutionId = str12;
                        customerPaymentInfo.InstitutionName = str16;
                        customerPaymentInfo.ItemName = str17;
                        customerPaymentInfo.ChannelName = str9;
                        customerPaymentInfo.PaymentCurrency = str13;
                        customerPaymentInfo.IsReversal = str19;
                        customerPaymentInfo.IBC = directJSONs[0].IBC_NAME;
                        customerPaymentInfo.BSC = directJSONs[0].BSC_NAME;
                        customerPaymentInfo.PaymentStatus = str18;
                        customerPaymentInfo.BankName = str14;
                        customerPaymentInfo.BranchName = str15;
                        customerPaymentInfo.Location = str10;
                        customerPaymentInfo.CustomerAddress = directJSONs[0].ADDRESS;
                        customerPaymentInfo.CustomerReference = directJSONs[0].CUSTOMER_NO;
                        customerPaymentInfo.AlternateCustReference = directJSONs[0].METER_NO;
                        string str25 = string.Concat("MAP", RandomPassword.Generate(10).ToString());
                        customerPaymentInfo.BRC_ID = str25;
                        customerPaymentInfo.Token = "MAP";
                        customerPaymentInfo.PaymentMethod = str11;
                        customerPaymentInfo.MAPApplicationStatus = "VERIFY";
                        customerPaymentInfo.MAPPaymentStatus = "NOT PAID";
                        customerPaymentInfo.BRCPaymentStatus = "NOT PAID";
                        customerPaymentInfo.PaymentStatus = "NOT PAID";
                        customerPaymentInfo.MAPCustomerName = MAPApplicantName;
                        customerPaymentInfo.AccountType = AccountType.ToUpper();
                        customerPaymentInfo.TransactionProcessDate = DateTime.Now.ToShortDateString();
                        customerPaymentInfo.BRC_Arrears = directJSONs[0].ARREAR;
                        this.db.CustomerPaymentInfos.Add(customerPaymentInfo);
                        this.db.SaveChanges();
                        str23 = "VERIFY";
                        DbSet<CustomerPaymentInfo> dbSet = this.db.CustomerPaymentInfos;
                        list = (
                            from p in dbSet
                            where (p.Token == "MAP") && ((p.AlternateCustReference == accountNo) || (p.CustomerReference == accountNo) || (p.TransactionID == accountNo))
                            select p).ToList<CustomerPaymentInfo>();
                        str = "";
                        if (list.Count > 0)
                        {
                            accountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference;
                            customerEmail = list.FirstOrDefault<CustomerPaymentInfo>().CustomerEmail;
                            AccountType = list.FirstOrDefault<CustomerPaymentInfo>().AccountType;
                            customerPhoneNumber = list.FirstOrDefault<CustomerPaymentInfo>().CustomerPhoneNumber;
                            alternateCustReference = list.FirstOrDefault<CustomerPaymentInfo>().AlternateCustReference;
                            transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            str = RandomPassword.Generate(10).ToString();
                        }
                        jsonResult = base.Json(new { AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str22, MeterCost = str21, Status = str23, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str20.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str8, trans_id = transactionID, ProductDescription = str7, PublicKey = str5, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                    else
                    {
                        transactionID = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                        str = RandomPassword.Generate(10);
                        phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str4, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str7, "&product-id=", str8, "&public-key=", str5, "&trans-id=", str };
                        stringBuilder.Append(string.Concat(phoneNumber));
                        MAPPayment mAPPayment = new MAPPayment()
                        {
                            PaymentFor = "BRC",
                            TicketId = transactionID,
                            TransRef = str,
                            AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                            Amount = AmountPaid,
                            BSC = list.FirstOrDefault<CustomerPaymentInfo>().BSC,
                            IBC = list.FirstOrDefault<CustomerPaymentInfo>().IBC,
                            PaymentMode = "WEB",
                            PaymentStatus = "NOTPAID",
                            Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase
                        };
                        this.db.MAPPayments.Add(mAPPayment);
                        this.db.SaveChanges();
                        str20 = this.CreateHash(stringBuilder.ToString(), str6);
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                        {
                            str23 = "APPROVED FOR PAYMENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ABOUTTOAPPLY")
                        {
                            str23 = "ABOUTTOAPPLY";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                        {
                            string transactionID1 = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID;
                            DbSet<CUSTOMER> cUSTOMERS = this.db.CUSTOMERS;
                            CUSTOMER cUSTOMER = cUSTOMERS.FirstOrDefault<CUSTOMER>((CUSTOMER p) => p.TransactionID == transactionID1);
                            if (cUSTOMER != null)
                            {
                                meterType = cUSTOMER.MeterType;
                                if (!string.IsNullOrEmpty(cUSTOMER.Email))
                                {
                                    EmailAddress = cUSTOMER.Email;
                                    email = cUSTOMER.Email;
                                }
                                else
                                {
                                    EmailAddress = "map@phed.com.ng";
                                    email = "map@phed.com.ng";
                                }
                                if (meterType.Trim() == "SINGLE PHASE")
                                {
                                    str21 = this.SinglePhaseAmount.ToString();
                                    AmountPaid = this.SinglePhaseAmount.ToString();
                                }
                                if (meterType.Trim() == "THREE PHASE")
                                {
                                    str21 = this.ThreePhaseAmount.ToString();
                                    AmountPaid = this.ThreePhaseAmount.ToString();
                                }
                                phoneNumber = new string[] { "amount=", AmountPaid, "&callback-url=", str3, "&currency=", str4, "&customer-email=", email, "&merchant-id=", str2, "&product-desc=", str7, "&product-id=", str8, "&public-key=", str5, "&trans-id=", str };
                                stringBuilder.Append(string.Concat(phoneNumber));
                                str20 = this.CreateHash(stringBuilder.ToString(), str6);
                                str22 = str;
                                MAPPayment mAPPayment1 = new MAPPayment()
                                {
                                    PaymentFor = "METER",
                                    TicketId = list.FirstOrDefault<CustomerPaymentInfo>().TransactionID,
                                    TransRef = str,
                                    AccountNo = list.FirstOrDefault<CustomerPaymentInfo>().CustomerReference,
                                    Amount = str21,
                                    BSC = list.FirstOrDefault<CustomerPaymentInfo>().BSC,
                                    IBC = list.FirstOrDefault<CustomerPaymentInfo>().IBC,
                                    PaymentMode = "WEB",
                                    PaymentStatus = "NOTPAID",
                                    Phase = list.FirstOrDefault<CustomerPaymentInfo>().MeterPhase
                                };
                                this.db.MAPPayments.Add(mAPPayment1);
                                this.db.SaveChanges();
                                str23 = "PROCEEDTOMAPPAY";
                            }
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "HASARREARS")
                        {
                            str23 = "HASARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "ARREARSPERCENT")
                        {
                            str23 = "ARREARSPERCENT";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "MAPAPPLIED")
                        {
                            str23 = "MAPAPPLIED";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAYARREARS")
                        {
                            str23 = "PAYARREARS";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str23 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "PAID FOR METER")
                        {
                            str23 = "PAID FOR METER";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "GOBRC")
                        {
                            str23 = "GOBRC";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "BRCDONE")
                        {
                            str23 = "BRCDONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLMENTAL")
                        {
                            str23 = "INSTALLMENTAL";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                        {
                            str23 = "APPROVED FOR INSTALLATION";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "INSTALLATION DONE")
                        {
                            str23 = "INSTALLATION DONE";
                        }
                        if (list.FirstOrDefault<CustomerPaymentInfo>().MAPApplicationStatus == "VERIFY")
                        {
                            str23 = "VERIFY";
                        }
                        jsonResult = base.Json(new { AlternateCustReference = alternateCustReference, trans_id2 = str, CustEmail = customerEmail, CustomerPhone = customerPhoneNumber, MeterType = meterType, TransactionReference = str22, MeterCost = str21, Status = str23, MAPplicant = list.FirstOrDefault<CustomerPaymentInfo>().MAPCustomerName, BRC_ID = list.FirstOrDefault<CustomerPaymentInfo>().BRC_ID, CustomerName = list.FirstOrDefault<CustomerPaymentInfo>().CustomerName, result = end, HashCode = str20.ToLower(), SparkBal = str1, amount = AmountPaid, customer_email = EmailAddress, ProductId = str8, trans_id = transactionID, ProductDescription = str7, PublicKey = str5, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                        jsonResult1 = jsonResult;
                    }
                }
                else
                {
                    jsonResult1 = base.Json(new { result = end }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            return jsonResult1;
        }

        [HttpGet]
        public async Task<JsonResult> x(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid)
        {
            JsonResult jsonResult;
            string[] accountNo;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                accountNo = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", AccountNo, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
                streamWriter.Write(string.Concat(accountNo));
                streamWriter.Flush();
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
            string str = "00004";
            string str1 = "http://phedpayments.nepamsonline.com/PhedPay/SuccessQR";
            string str2 = RandomPassword.Generate(10).ToString();
            string str3 = EmailAddress.Trim();
            string str4 = "NGN";
            string str5 = "1d8c210a3b1a5d32496204618cf5bd5a";
            string str6 = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string str7 = "Bill";
            string str8 = "78";
            StringBuilder stringBuilder = new StringBuilder();
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string end = streamReader.ReadToEnd();
                if (!(end == "Customer Not Found"))
                {
                    end = end.Replace("\r", string.Empty);
                    end = end.Replace("\n", string.Empty);
                    end = end.Replace("\\", string.Empty);
                    end = end.Replace("\\\\", string.Empty);
                    List<DirectJSON> directJSONs = JsonConvert.DeserializeObject<List<DirectJSON>>(end);
                    accountNo = new string[] { "amount=", AmountPaid, "&callback-url=", str1, "&currency=", str4, "&customer-email=", str3, "&merchant-id=", str, "&product-desc=", str7, "&product-id=", str8, "&public-key=", str5, "&trans-id=", str2 };
                    stringBuilder.Append(string.Concat(accountNo));
                    string str9 = this.CreateHash(stringBuilder.ToString(), str6);
                    AmountPaid = directJSONs[0].ARREAR;
                    string str10 = "WEB";
                    string str11 = "WEB";
                    string str12 = "Debit Card";
                    string str13 = "PHEDCPP";
                    string str14 = "NGN";
                    string str15 = "WEB Payment";
                    string str16 = "WEB";
                    string str17 = "PHEDBillPay";
                    string str18 = "PHED Bill Payment";
                    string str19 = "Success";
                    string str20 = "false";
                    CustomerPaymentInfo customerPaymentInfo = new CustomerPaymentInfo()
                    {
                        Amount = AmountPaid,
                        ItemAmount = AmountPaid,
                        ItemCode = str8,
                        TransactionID = str2,
                        PaymentMethod = "WEB",
                        CustomerName = directJSONs[0].CONS_NAME,
                        CustomerPhoneNumber = PhoneNumber,
                        DepositorName = directJSONs[0].CONS_NAME,
                        DepositSlipNumber = str2,
                        InstitutionId = str13,
                        InstitutionName = str17,
                        ItemName = str18,
                        ChannelName = str10,
                        PaymentCurrency = str14,
                        IsReversal = str20,
                        PaymentStatus = str19,
                        BankName = str15,
                        BranchName = str16,
                        Location = str11,
                        CustomerAddress = directJSONs[0].ADDRESS,
                        CustomerReference = directJSONs[0].CUSTOMER_NO,
                        AlternateCustReference = directJSONs[0].METER_NO
                    };
                    customerPaymentInfo.PaymentMethod = str12;
                    this.db.CustomerPaymentInfos.Add(customerPaymentInfo);
                    this.db.SaveChanges();
                    JsonResult jsonResult1 = base.Json(new { result = end, HashCode = str9.ToLower(), amount = AmountPaid, customer_email = EmailAddress, ProductId = str8, trans_id = str2, ProductDescription = str7, PublicKey = str5, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                    jsonResult = jsonResult1;
                }
                else
                {
                    JsonResult jsonResult2 = base.Json(new { result = end, HashCode = "0", amount = AmountPaid, customer_email = EmailAddress, ProductId = str8, trans_id = str2, ProductDescription = str7, PublicKey = str5, hashString = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
                    jsonResult = jsonResult2;
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            return jsonResult;
        }
    }
}