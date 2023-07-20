using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHEDServe.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Configuration;


namespace PHEDServe.Controllers
{
    
    public class MAPRegisterController1 : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        decimal ThreePhaseAmount = 72085.68M;
            decimal SinglePhaseAmount = 39765.86M;

        //
        // GET: /MAPRegister/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckApplicationStatus()
        {
          
            return View();
        }
        public ActionResult UploadPayment()
        {
          
            return View();
        }

        public ActionResult ApplicationSuccess(string TicketID)
        {
          
            return View();
        }
        public ActionResult SuccessPage()
        {
          
            return View();
        }
        public ActionResult CheckStatus()
        { 
            return View();
        }

        public ActionResult BRC()
        {
            return View();
        }


        [HttpGet]
        public JsonResult CreateCustomer()
        {
            UserViewModel myUserViewModel = new UserViewModel();

            myUserViewModel.customer = new CUSTOMER();
            myUserViewModel.StateList = db.States.ToList();
            myUserViewModel.LGAList =   db.LGAs.ToList();

            var result = JsonConvert.SerializeObject(myUserViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public async Task<JsonResult> UploadAgency(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            var DocumentId = Guid.NewGuid().ToString();
            UserViewModel viewModel = new UserViewModel();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];

                //var StudentCode = System.Web.HttpContext.Current.Request.Files["StudentCode"];
                //var DatePaid = System.Web.HttpContext.Current.Request.Files["DatePaid"];
                //var BankName = System.Web.HttpContext.Current.Request.Files["BankName"];
                //var TellerNo = System.Web.HttpContext.Current.Request.Files["TellerNo"];
                //var AmountPaid = System.Web.HttpContext.Current.Request.Files["AmountPaid"];
                //var ScheduleCode = System.Web.HttpContext.Current.Request.Files["ScheduleCode"];

                var DocumentFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                var DocumentName = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                var _TellerNo = System.Web.HttpContext.Current.Request.Params["TellerNo"];
                var _DocumentSize = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                var _DatePaid = System.Web.HttpContext.Current.Request.Params["DatePaid"];
                var _TicketId = System.Web.HttpContext.Current.Request.Params["TicketId"];
                var _BankName = System.Web.HttpContext.Current.Request.Params["BankName"];
                var _AmountPaid = System.Web.HttpContext.Current.Request.Params["AmountPaid"];
                 
                //insert into the Payment File for Upload
                 
                var _backbone = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == _TicketId);
                 
                if (_backbone == null)
                {
                    //return Error
                    var result = JsonConvert.SerializeObject(viewModel); 
                    var jsonResult = Json(new { result = result, error = "The Ticket Id you're quoting is wrong. Please input the Correct Ticket Id to Proceed. Please try again" }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {


                    //Check the amount that  the GUY paid

                    if (_backbone.MeterPhase.ToString().Trim() == "THREE PHASE" && Convert.ToDecimal(_AmountPaid) < ThreePhaseAmount)
                    {
                        //return Error
                        var result = JsonConvert.SerializeObject(viewModel);
                        var jsonResult = Json(new { result = result, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Three Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }

                    if (_backbone.MeterPhase.ToString().Trim() == "SINGLE PHASE" && Convert.ToDecimal(_AmountPaid) < SinglePhaseAmount)
                    {
                        //return Error
                        var result = JsonConvert.SerializeObject(viewModel);
                        var jsonResult = Json(new { result = result, error = "Hey! It seems the Amount you paid is Less than the Actual amount for a Single Phase Meter. You cannot Upload this. Please Pay the Actual amount before Uploading. thank you." }, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }





                    MAPPayment backbone = new MAPPayment();
                    backbone.DatePaid = _DatePaid.ToString();
                    backbone.DocumentPath = "/Documents/" + DocumentName;
                    backbone.TellerNo = _TellerNo.ToString();
                    backbone.Amount = _AmountPaid.ToString();
                    backbone.ApprovalStatus = "NOTAPPROVED";
                    backbone.BSC = _backbone.BSC;
                    backbone.IBC = _backbone.IBC;
                    backbone.AccountNo = _backbone.CustomerReference;
                    backbone.CustomerName = _backbone.CustomerName;
                    backbone.PaymentMode = "BANK";
                    backbone.ReceiptNo = _TellerNo.ToString();
                    backbone.PaymentStatus = "PAID";
                    backbone.PaymentFor = "METER";
                    backbone.Phase = _backbone.MeterPhase;
                    backbone.TransRef = _TellerNo.ToString();
                    backbone.TicketId = _TicketId.ToString();
                    backbone.PaymentId = Guid.NewGuid().ToString();
                    db.MAPPayments.Add(backbone);
                    db.SaveChanges();
                }

                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);

                httpPostedFile.SaveAs(fileSavePath);
            }
             
            var _result = JsonConvert.SerializeObject(viewModel); 
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }
      
     
        [HttpGet]
        public ActionResult Success(string hash, string hash_type, string merchant_id, string status_code, string status_msg, string transaction_id)
        {
            hash = Request.QueryString["hash"];

            hash_type = Request.QueryString["hash-type"];

            merchant_id = Request.QueryString["merchant-id"];

            status_code = Request.QueryString["status-code"];

            status_msg = Request.QueryString["status-msg"];

            transaction_id = Request.QueryString["transaction-id"];

            string PaymentRef = Request.QueryString["payment-ref"];

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
            CustomerPaymentInfo CustomerDetails2 = db.CustomerPaymentInfos.Find(transaction_id);
            ViewBag.CustomerDetails2 = transaction_id;
            return View("SuccessPage");
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
        public JsonResult ShowSuccessDetails(string TicketIDHidden)
        {

            UserViewModel myUserViewModel = new UserViewModel();
            var Application = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketIDHidden);

            myUserViewModel.PaymentInfo = Application;

            var result = JsonConvert.SerializeObject(myUserViewModel);
            return Json(result, JsonRequestBehavior.AllowGet); 
        }



        [HttpGet]
        public JsonResult UpdatePercentage(string Percentage,string TicketId)
        { 
            UserViewModel myUserViewModel = new UserViewModel();
            var Application = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);
            if(Application != null)
            { 
                //update the Data 
                Application.ArrearsPercent = Percentage;
                Application.MAPApplicationStatus = "ABOUTTOAPPLY";
                db.Entry(Application).State = EntityState.Modified; 
                db.SaveChanges(); 
            }
             
            var result = JsonConvert.SerializeObject(myUserViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult UpdateApplicationStatus(string Status, string TicketId)
        {
            UserViewModel myUserViewModel = new UserViewModel();
            ActionMessagePerformed Message = new ActionMessagePerformed();

            var _Status = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            if (_Status != null)
            { 
                _Status.MAPApplicationStatus = Status;
                db.Entry(_Status).State = EntityState.Modified;
                db.SaveChanges();
            }
            var result = JsonConvert.SerializeObject(myUserViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplyForMeter(string CustomerDetails, string TicketId)
        {
            UserViewModel myUserViewModel = new UserViewModel();
            ActionMessagePerformed Message = new ActionMessagePerformed();
            var Customer = JsonConvert.DeserializeObject<CUSTOMER>(CustomerDetails);
            try
            {
                var Cust = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

                if (Cust != null)
                {
                    //Update with Application Status
                  
                    Cust.MeterPhase = Customer.MeterType;

                    if (Customer.ModeOfPayment == "MSC")
                    {
                        Cust.MAPApplicationStatus = "APPROVED FOR INSTALLATION";
                    }

                    else if (Customer.ModeOfPayment == "UPFRONT")
                    {
                        Cust.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                    }

                    Cust.IDcardNo = Customer.IDcardNo.Trim();
                   //  Cust.IdentityCardNumber = Customer.IDcardNo.Trim();
                    Cust.MAPPlan = Customer.ModeOfPayment;//.ModePaymentValue;
                    db.Entry(Cust).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //Send them an Email and SMS
                string PhoneNumber = "";
                string SMSMessage = ""; 
                string EmailId = "";
                string MailMessage = "";
                string ApplicantName = "";
                Customer.RegistrationDate = DateTime.Now;
                this.db.CUSTOMERS.Add(Customer);
                this.db.SaveChanges();

                string smsMessage = "Dear " + ApplicantName + ", Your application for a Meter was successful, your Ticket Number is " + TicketId + ". Kindly Quote this Number in all Transactions with PHED";

               // SendSMS(smsMessage, PhoneNumber);

               // SendEmail(EmailId, MailMessage, TicketId, PhoneNumber, ApplicantName);
                string AccountNo = TicketId;
                string Status = "";
                string MeterType = ""; 
                string MeterCost = "";


                 

                Message.message = "Your Application for the Smart Meter Was Successful";
                Message.title = "Successful";
                Message.type = "success";
                Message.PaymentType = Customer.ModeOfPayment;
            }

            catch (Exception e)
            {
                Message.message = "Your Application for the Smart Meter Was Unsuccessful!";
                Message.title = "Something went wrong";
                Message.type = "error";
            }

            myUserViewModel.ActionMessagePerformed = Message;
            myUserViewModel.customer = Customer;
            var result = JsonConvert.SerializeObject(myUserViewModel);
            return Json( new {result = result , ModeofPayment = Customer.ModeOfPayment}, JsonRequestBehavior.AllowGet);
        }

        private void SendEmail(string EmailId, string MailMessage, string TicketId, string PhoneNumber, string ApplicantName)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
            mail.Subject = "Meter Asset Provider Information From PHED";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.Bcc.Add("payments@phed.com.ng");
            mail.To.Add(EmailId);
            string RecipientType = "";

            string SMTPMailServer = "mail.phed.com.ng";

            SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
            MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
            //  string htmlMsgBody = this.EmailTextBox.Text;
            string htmlMsgBody = "<html><head></head>";
            htmlMsgBody = htmlMsgBody + "<body>";

            htmlMsgBody = htmlMsgBody + "<P>" + "Dear "  + ApplicantName + "," + "</P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your taking the time to complete the request for a meter." + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Please visit our website www.phed.com.ng/Map to know the status of your application." + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + ApplicantName + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Date: " + DateTime.Now.AddHours(8) + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + TicketId + " </P>";

            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + "Thank you,";
            htmlMsgBody = htmlMsgBody + " <P> " + "PHED  Metering Team" + " </P> ";
            htmlMsgBody = htmlMsgBody + "<br><br>";
            mail.Body = htmlMsgBody;

            //-Merchant’s Name
            //-Merchant’s Url
            //-Description of the Service/Goods


            MailSMTPserver.Send(mail);
        }

        private static void SendSMS(string smsMessage, String managersPhoneNose)
        {
            string smsapikey = ConfigurationManager.AppSettings["SMS_APIKEY"];
            try
            {
                // SMSLIVE247
                WebClient client = new WebClient();
                // Add a user agent header in case the requested URI contains a query.
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.QueryString.Add("cmd", "sendmsg");
                client.QueryString.Add("sessionid", smsapikey);
                client.QueryString.Add("message", smsMessage);
                client.QueryString.Add("sender", "PHED");
                client.QueryString.Add("sendto", managersPhoneNose);
                client.QueryString.Add("msgtype", "0");
                string baseurl = "http://www.smslive247.com/http/index.aspx";
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                //Response.Redirect("report");

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


        [HttpGet]
        public async Task<JsonResult> x(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";
             
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                              "\"CustomerNumber\":\"" + AccountNo + "\"," +
                              "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                                "\"Mailid\":\"" + EmailAddress + "\"," +
                              "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //perform the hash here before going to the Payment Page

            string MerchantID = "00004";

            string CallbackURL = "http://phedpayments.nepamsonline.com/PhedPay/SuccessQR";


            //GENERATE YOUTUBE-LIKE KEYS AND ID
            //StringBuilder builder = new StringBuilder();
            //Enumerable
            //   .Range(65, 26)
            //    .Select(e => ((char)e).ToString())
            //    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            //    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            //    .OrderBy(e => Guid.NewGuid())
            //    .Take(11)
            //    .ToList().ForEach(e => builder.Append(e));

            //LETS GENERATE A RANDOM NUMBER BASED ON OUR GENERATOR FOR THIS TRANSACTION




            string trans_id = RandomPassword.Generate(10).ToString();
            string CustomerEmail = EmailAddress.Trim();
            string Currency = "NGN";
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string ProductDescription = "Bill";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

         


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                if (result == "Customer Not Found")
                {
                    var jsonResulti = Json(new { result = result, HashCode = "0", amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResulti;
                }

                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace(@"\", string.Empty);
                result = result.Replace(@"\\", string.Empty);
                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);

                hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL
         + "&currency=" +
         Currency + "&customer-email=" + CustomerEmail +
         "&merchant-id=" + MerchantID + "&product-desc="
         + ProductDescription +
         "&product-id=" + ProductId + "&public-key=" +
         PublicKey + "&trans-id=" + trans_id);



                //Save the Customers Details to the Database

                string HashCode = CreateHash(hashString.ToString(), Key);


                AmountPaid = objResponse1[0].ARREAR;

                string ChannelName = "WEB";
                string Location = "WEB";
                string PaymentMethod = "Debit Card";
                string InstitutionId = "PHEDCPP";
                string PaymentCurrency = "NGN";
                string BankName = "WEB Payment";
                string BranchName = "WEB";
                string InstitutionName = "PHEDBillPay";
                string ItemName = "PHED Bill Payment";
                string ItemCode = "PHEDBill";
                string PaymentStatus = "Success";
                string IsReversal = "false";

                CustomerPaymentInfo Info = new CustomerPaymentInfo();
                Info.Amount = AmountPaid;
                Info.ItemAmount = AmountPaid;
                Info.ItemCode = ProductId;
                Info.TransactionID = trans_id;
                Info.PaymentMethod = "WEB";
                Info.CustomerName = objResponse1[0].CONS_NAME;
                Info.CustomerPhoneNumber = PhoneNumber;
                Info.DepositorName = objResponse1[0].CONS_NAME;
                Info.DepositSlipNumber = trans_id;
                Info.InstitutionId = InstitutionId;
                Info.InstitutionName = InstitutionName;
                Info.ItemName = ItemName;
                Info.ChannelName = ChannelName;
                Info.PaymentCurrency = PaymentCurrency;
                Info.IsReversal = IsReversal;
                Info.PaymentStatus = PaymentStatus;
                Info.BankName = BankName;
                Info.BranchName = BranchName;
                Info.Location = Location;
                Info.CustomerAddress = objResponse1[0].ADDRESS;
                Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                Info.AlternateCustReference = objResponse1[0].METER_NO;
                Info.PaymentMethod = PaymentMethod;
                db.CustomerPaymentInfos.Add(Info);
                db.SaveChanges();
                var jsonResult = Json(new { result = result, HashCode = HashCode.ToLower(), amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult; 
                //var Data = new { data = stuff, data2 = otherstuff };
            }
        }

        [HttpGet]
        public async Task<JsonResult> v(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            //check if the Account Number is Existing with MAP

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            //if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            //{

            //    SparkBal = GetSparkBalance(AccountNo);
            //}



            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

            string CallbackURL = "http://map.nepamsonline.com/successPage.aspx";


            //GENERATE YOUTUBE-LIKE KEYS AND ID
            //StringBuilder builder = new StringBuilder();
            //Enumerable
            //   .Range(65, 26)
            //    .Select(e => ((char)e).ToString())
            //    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            //    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            //    .OrderBy(e => Guid.NewGuid())
            //    .Take(11)
            //    .ToList().ForEach(e => builder.Append(e));

            //LETS GENERATE A RANDOM NUMBER BASED ON OUR GENERATOR FOR THIS TRANSACTION


            //check if this GUY Has come Before

            var CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
            string CustEmail = "";
            string CustomerPhone = ""; string AlternateCustReference = "";
            string trans_id = RandomPassword.Generate(10).ToString(); 
            string CustomerEmail = EmailAddress.Trim();
            if (CheckHim.Count > 0)
            {

               AccountNo = CheckHim.FirstOrDefault().CustomerReference;
               CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
               EmailAddress = CustEmail;
               CustomerEmail = CustEmail;
               CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
               AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
               trans_id =  CheckHim.FirstOrDefault().TransactionID;
            }


           using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
           {
               string json = "{\"Username\":\"phed\"," +
                             "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                             "\"CustomerNumber\":\"" + AccountNo + "\"," +
                             "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                               "\"Mailid\":\"" + EmailAddress + "\"," +
                             "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
               streamWriter.Write(json);
               streamWriter.Flush();
               streamWriter.Close();
           }

          
           
            string Currency = "NGN";
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string ProductDescription = "MAP/Arrears Meter Payment";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace(@"\", string.Empty);
                result = result.Replace(@"\\", string.Empty);
                //check if the Customer Exists here

                if (result == "Customer Not Found")
                {
                    var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    return _jsonResult;
                }
                 

                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);

                AmountPaid = objResponse1[0].ARREAR;
                //AmountPaid = "100";
               
                string ChannelName = "WEB";
                string Location = "WEB";
                string PaymentMethod = "Debit Card";
                string InstitutionId = "PHED";
                string PaymentCurrency = "NGN";
                string BankName = "Arrears Payment";
                string BranchName = "ARREARS";
                string InstitutionName = "ARREARSBillClearance";
                string ItemName = "PHED Bill Payment";
                string ItemCode = "PHEDBillARREARS";
                string PaymentStatus = "PENDING";
                string IsReversal = "false";
                string HashCode = "";
                string MeterType = "";
                string MeterCost = "";
                string TransactionReference = "";
                string Status = "";

                if (CheckHim.Count > 0)
                {
                    trans_id = CheckHim.FirstOrDefault().TransactionID;
                    string trans_id2 = RandomPassword.Generate(10);


                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                    "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                    + "&trans-id=" + trans_id2);

                    MAPPayment pr = new MAPPayment();
                    pr.PaymentFor = "BRC";
                    pr.TicketId = trans_id;
                    pr.TransRef = trans_id2;
                    pr.AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                    pr.Amount = AmountPaid;
                    pr.BSC = CheckHim.FirstOrDefault().BSC;
                    pr.IBC = CheckHim.FirstOrDefault().IBC;
                    pr.PaymentMode = "WEB";
                    pr.PaymentStatus = "NOTPAID";
                    pr.Phase = CheckHim.FirstOrDefault().MeterPhase;
                    db.MAPPayments.Add(pr);
                    db.SaveChanges();
                     
                    //Save the Customers Details to the Database

                    HashCode = CreateHash(hashString.ToString(), Key);

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "APPROVED FOR PAYMENT";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ABOUTTOAPPLY")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "ABOUTTOAPPLY";
                    }



                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                    {
                      


                        //Load the Parameters for the Meter Application

                        string TicketID = CheckHim.FirstOrDefault().TransactionID;

                        var Details = db.CUSTOMERS.FirstOrDefault(p => p.TransactionID == TicketID);
                        if (Details != null)
                        {

                            MeterType = Details.MeterType;

                            if (string.IsNullOrEmpty(Details.Email as string))
                            {
                                EmailAddress = "map@phed.com.ng";
                                CustomerEmail = "map@phed.com.ng";
                            }
                            else
                            {

                                EmailAddress = Details.Email;
                                CustomerEmail = Details.Email;
                            }


                            if (MeterType.Trim() == "SINGLE PHASE")
                            {
                                MeterCost = "38,841.7";
                                AmountPaid = "38841.7";

                            }

                            if (MeterType.Trim() == "THREE PHASE")
                            {
                                MeterCost = "70,834.8";
                                AmountPaid = "70834.8";
                            }

                            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                                             "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                                             + "&trans-id=" + trans_id2);
                            HashCode = CreateHash(hashString.ToString(), Key);
                            TransactionReference = trans_id2;

                            MAPPayment p = new MAPPayment();
                            p.PaymentFor = "METER";
                            p.TicketId = CheckHim.FirstOrDefault().TransactionID;
                            p.TransRef = trans_id2;
                            p.AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                            p.Amount = MeterCost;
                            p.BSC = CheckHim.FirstOrDefault().BSC;
                            p.IBC = CheckHim.FirstOrDefault().IBC;
                            p.PaymentMode = "WEB";
                            p.PaymentStatus = "NOTPAID";
                            p.Phase = CheckHim.FirstOrDefault().MeterPhase;
                            db.MAPPayments.Add(p);
                            db.SaveChanges();
                            Status = "PROCEEDTOMAPPAY";
                        }
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "HASARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "HASARREARS";
                    } 
                    
                    
                    
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ARREARSPERCENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "ARREARSPERCENT";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "MAPAPPLIED")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "MAPAPPLIED";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAYARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAYARREARS";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAID FOR METER")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAID FOR METER";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "BRCDONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "BRCDONE";
                    }

                    
                         
                     

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLMENTAL")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        //08036978187
                        Status = "INSTALLMENTAL";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAP PAID
                        //BRC has b  
                        

                        Status = "APPROVED FOR INSTALLATION";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLATION DONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        Status = "INSTALLATION DONE";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "VERIFY")
                    { 
                        Status = "VERIFY";
                    }

                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }

                else
                {
                    //Send and Email and SMS to the Customer

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                    mail.Subject = "Meter Asset Provider Information From PHED";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    mail.Bcc.Add("payments@phed.com.ng");
                    mail.To.Add(CustomerEmail);
                    string RecipientType = "";

                    string SMTPMailServer = "mail.phed.com.ng";

                    SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                    MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                    //  string htmlMsgBody = this.EmailTextBox.Text;
                    string htmlMsgBody = "<html><head></head>";
                    htmlMsgBody = htmlMsgBody + "<body>";
                    //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                    htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + MAPApplicantName + "," + "</P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + MAPApplicantName + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Visit Date: " + DateTime.Now.AddHours(8) + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + trans_id + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + AccountNo + " </P>";
 
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    htmlMsgBody = htmlMsgBody + "Thank you,";
                    htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    mail.Body = htmlMsgBody;

                    //-Merchant’s Name
                    //-Merchant’s Url
                    //-Description of the Service/Goods


                    MailSMTPserver.Send(mail);

                      
                   
                    CustomerPaymentInfo Info = new CustomerPaymentInfo();
                    Info.Amount = AmountPaid;
                    Info.ItemAmount = AmountPaid;
                    Info.ItemCode = ProductId;
                    Info.TransactionID = trans_id;
                    Info.PaymentMethod = "WEB";
                    Info.CustomerName = objResponse1[0].CONS_NAME;
                    string CustomerName = objResponse1[0].CONS_NAME;
                    Info.CustomerEmail = CustomerEmail;
                    Info.CustomerPhoneNumber = PhoneNumber;
                    Info.DepositorName = objResponse1[0].CONS_NAME;
                    Info.DepositSlipNumber = trans_id;
                    Info.InstitutionId = InstitutionId;
                    Info.InstitutionName = InstitutionName;
                    Info.ItemName = ItemName;
                    Info.ChannelName = ChannelName;
                    Info.PaymentCurrency = PaymentCurrency;
                    Info.IsReversal = IsReversal;
                    Info.IBC = objResponse1[0].IBC_NAME;
                    Info.BSC = objResponse1[0].BSC_NAME;
                    Info.PaymentStatus = PaymentStatus;
                    Info.BankName = BankName;
                    Info.BranchName = BranchName;
                    Info.Location = Location;
                    Info.CustomerAddress = objResponse1[0].ADDRESS;
                    Info.CustomerReference = objResponse1[0].CUSTOMER_NO;

                    if (!string.IsNullOrEmpty(objResponse1[0].METER_NO as string))
                    {
                        Info.AlternateCustReference = objResponse1[0].METER_NO;
                    }
                    else
                    {
                        Info.AlternateCustReference = "NOMETER";


                    }

                   
                    string BRC_ID = "MAP" + RandomPassword.Generate(10).ToString();
                    Info.BRC_ID = BRC_ID;
                    Info.Token = "MAP";
                    Info.PaymentMethod = PaymentMethod;
                    Info.MAPApplicationStatus = "VERIFY";
                    Info.MAPPaymentStatus = "NOT PAID";
                    Info.BRCPaymentStatus = "NOT PAID";
                    Info.PaymentStatus = "NOT PAID";
                    Info.MAPCustomerName = MAPApplicantName;
                    string MAPplicant = MAPApplicantName;
                    Info.AccountType = AccountType.ToUpper();
                    Info.TransactionProcessDate = DateTime.Now.ToShortDateString();
                    Info.BRC_Arrears = objResponse1[0].ARREAR;

                    db.CustomerPaymentInfos.Add(Info);
                    db.SaveChanges();

                    Status = "VERIFY";

                    CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
                    string trans_id2 = "";
                    if (CheckHim.Count > 0)
                    { 
                        AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                        CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
                        CustomerEmail = CustEmail;
                        AccountType = CheckHim.FirstOrDefault().AccountType;
                        CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
                        AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
                        trans_id = CheckHim.FirstOrDefault().TransactionID;
                        trans_id2 = RandomPassword.Generate(10).ToString();
                    }





                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                   "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                   + "&trans-id=" + trans_id);
                    HashCode = CreateHash(hashString.ToString(), Key);
                     
                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
                var jsonResult0 = Json(new { AlternateCustReference = AlternateCustReference, CustEmail = CustEmail, CustomerPhone = CustomerPhone, Status = Status, MAPplicant = "", BRC_ID = "", CustomerName = "", result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult0;
            }
        }





        [HttpGet]
        public async Task<JsonResult> w(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            //check if the Account Number is Existing with MAP

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            //if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            //{

            //    SparkBal = GetSparkBalance(AccountNo);
            //}



            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

            string CallbackURL = "http://map.nepamsonline.com/successPage.aspx";


            //GENERATE YOUTUBE-LIKE KEYS AND ID
            //StringBuilder builder = new StringBuilder();
            //Enumerable
            //   .Range(65, 26)
            //    .Select(e => ((char)e).ToString())
            //    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            //    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            //    .OrderBy(e => Guid.NewGuid())
            //    .Take(11)
            //    .ToList().ForEach(e => builder.Append(e));

            //LETS GENERATE A RANDOM NUMBER BASED ON OUR GENERATOR FOR THIS TRANSACTION


            //check if this GUY Has come Before

            var CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
            string CustEmail = "";
            string CustomerPhone = ""; string AlternateCustReference = "";
            string trans_id = RandomPassword.Generate(10).ToString(); 
            string CustomerEmail = EmailAddress.Trim();

            if (CheckHim.Count > 0)
            {
                AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
                EmailAddress = CustEmail;
                CustomerEmail = CustEmail;
                CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
                AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
                trans_id = CheckHim.FirstOrDefault().TransactionID;
                AccountType = CheckHim.FirstOrDefault().AccountType.ToUpper();
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                              "\"CustomerNumber\":\"" + AccountNo + "\"," +
                              "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                                "\"Mailid\":\"" + EmailAddress + "\"," +
                              "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

          
           
            string Currency = "NGN";
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string ProductDescription = "MAP/Arrears Meter Payment";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace(@"\", string.Empty);
                result = result.Replace(@"\\", string.Empty);
                //check if the Customer Exists here

                if (result == "Customer Not Found")
                {
                    var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    return _jsonResult;
                }
                 
                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);

                AmountPaid = objResponse1[0].ARREAR;
                //AmountPaid = "100";
               
                string ChannelName = "WEB";
                string Location = "WEB";
                string PaymentMethod = "Debit Card";
                string InstitutionId = "PHED";
                string PaymentCurrency = "NGN";
                string BankName = "Arrears Payment";
                string BranchName = "ARREARS";
                string InstitutionName = "ARREARSBillClearance";
                string ItemName = "PHED Bill Payment";
                string ItemCode = "PHEDBillARREARS";
                string PaymentStatus = "PENDING";
                string IsReversal = "false";
                string HashCode = "";
                string MeterType = "";
                string MeterCost = "";
                string TransactionReference = "";
                string Status = "";

                if (CheckHim.Count > 0)
                {
                    trans_id = CheckHim.FirstOrDefault().TransactionID;
                    string trans_id2 = RandomPassword.Generate(10);


                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                    "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                    + "&trans-id=" + trans_id2);

                    MAPPayment pr = new MAPPayment();
                    pr.PaymentFor = "BRC";
                    pr.TicketId = trans_id;
                    pr.TransRef = trans_id2;
                    pr.AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                    pr.Amount = AmountPaid;
                    pr.BSC = CheckHim.FirstOrDefault().BSC;
                    pr.IBC = CheckHim.FirstOrDefault().IBC;
                    pr.PaymentMode = "WEB";
                    pr.PaymentStatus = "NOTPAID";
                    pr.Phase = CheckHim.FirstOrDefault().MeterPhase;
                    db.MAPPayments.Add(pr);
                    db.SaveChanges();
                     
                    //Save the Customers Details to the Database

                    HashCode = CreateHash(hashString.ToString(), Key);

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "APPROVED FOR PAYMENT";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ABOUTTOAPPLY")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "ABOUTTOAPPLY";
                    }



                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                    {
                      


                        //Load the Parameters for the Meter Application

                        string TicketID = CheckHim.FirstOrDefault().TransactionID;

                        var Details = db.CUSTOMERS.FirstOrDefault(p => p.TransactionID == TicketID);
                        if (Details != null)
                        {

                            MeterType = Details.MeterType;

                            if (string.IsNullOrEmpty(Details.Email as string))
                            {
                                EmailAddress = "map@phed.com.ng";
                                CustomerEmail = "map@phed.com.ng";
                            }
                            else
                            {

                                EmailAddress = Details.Email;
                                CustomerEmail = Details.Email;
                            }


                            if (MeterType.Trim() == "SINGLE PHASE")
                            {
                                MeterCost = "38,841.7";
                                AmountPaid = "38841.7";

                            }

                            if (MeterType.Trim() == "THREE PHASE")
                            {
                                MeterCost = "70,834.8";
                                AmountPaid = "70834.8";
                            }

                            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                                             "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                                             + "&trans-id=" + trans_id2);
                            HashCode = CreateHash(hashString.ToString(), Key);
                            TransactionReference = trans_id2;

                            MAPPayment p = new MAPPayment();
                            p.PaymentFor = "METER";
                            p.TicketId = CheckHim.FirstOrDefault().TransactionID;
                            p.TransRef = trans_id2;
                            p.AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                            p.Amount = MeterCost;
                            p.BSC = CheckHim.FirstOrDefault().BSC;
                            p.IBC = CheckHim.FirstOrDefault().IBC;
                            p.PaymentMode = "WEB";
                            p.PaymentStatus = "NOTPAID";
                            p.Phase = CheckHim.FirstOrDefault().MeterPhase;
                            db.MAPPayments.Add(p);
                            db.SaveChanges();
                            Status = "PROCEEDTOMAPPAY";
                        }
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "HASARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "HASARREARS";
                    } 
                    
                    
                    
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ARREARSPERCENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "ARREARSPERCENT";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "MAPAPPLIED")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "MAPAPPLIED";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAYARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAYARREARS";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAID FOR METER")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAID FOR METER";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "BRCDONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "BRCDONE";
                    }

                    
                         
                     

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLMENTAL")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        //08036978187
                        Status = "INSTALLMENTAL";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAP PAID
                        //BRC has b  
                        

                        Status = "APPROVED FOR INSTALLATION";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLATION DONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        Status = "INSTALLATION DONE";
                    }
                    
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "MAPPAID")
                    { 
                        Status = "MAPPAID";    
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "VERIFY")
                    { 
                        Status = "VERIFY";
                    }

                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }

                else
                {
                    //Send and Email and SMS to the Customer

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                    mail.Subject = "Meter Asset Provider Information From PHED";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    mail.Bcc.Add("payments@phed.com.ng");
                    mail.To.Add(CustomerEmail);
                    string RecipientType = "";

                    string SMTPMailServer = "mail.phed.com.ng";

                    SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                    MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                    //  string htmlMsgBody = this.EmailTextBox.Text;
                    string htmlMsgBody = "<html><head></head>";
                    htmlMsgBody = htmlMsgBody + "<body>";
                    //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                    htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + MAPApplicantName + "," + "</P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + MAPApplicantName + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Visit Date: " + DateTime.Now.AddHours(8) + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + trans_id + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + AccountNo + " </P>";
 
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    htmlMsgBody = htmlMsgBody + "Thank you,";
                    htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    mail.Body = htmlMsgBody;

                    //-Merchant’s Name
                    //-Merchant’s Url
                    //-Description of the Service/Goods


                    MailSMTPserver.Send(mail);

                      
                   
                    CustomerPaymentInfo Info = new CustomerPaymentInfo();
                    Info.Amount = AmountPaid;
                    Info.ItemAmount = AmountPaid;
                    Info.ItemCode = ProductId;
                    Info.TransactionID = trans_id;
                    Info.PaymentMethod = "WEB";
                    Info.CustomerName = objResponse1[0].CONS_NAME;
                    string CustomerName = objResponse1[0].CONS_NAME;
                    Info.CustomerEmail = CustomerEmail;
                    Info.CustomerPhoneNumber = PhoneNumber;
                    Info.DepositorName = objResponse1[0].CONS_NAME;
                    Info.DepositSlipNumber = trans_id;
                    Info.InstitutionId = InstitutionId;
                    Info.InstitutionName = InstitutionName;
                    Info.ItemName = ItemName;
                    Info.ChannelName = ChannelName;
                    Info.PaymentCurrency = PaymentCurrency;
                    Info.IsReversal = IsReversal;
                    Info.IBC = objResponse1[0].IBC_NAME;
                    Info.BSC = objResponse1[0].BSC_NAME;
                    Info.PaymentStatus = PaymentStatus;
                    Info.BankName = BankName;
                    Info.BranchName = BranchName;
                    Info.Location = Location;
                    Info.CustomerAddress = objResponse1[0].ADDRESS;
                    Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                    Info.AlternateCustReference = objResponse1[0].METER_NO;
                    string BRC_ID = "MAP" + RandomPassword.Generate(10).ToString();
                    Info.BRC_ID = BRC_ID;
                    Info.Token = "MAP";
                    Info.PaymentMethod = PaymentMethod;
                    Info.MAPApplicationStatus = "VERIFY";
                    Info.MAPPaymentStatus = "NOT PAID";
                    Info.BRCPaymentStatus = "NOT PAID";
                    Info.PaymentStatus = "NOT PAID";
                    Info.MAPCustomerName = MAPApplicantName;
                    string MAPplicant = MAPApplicantName;
                    Info.AccountType = AccountType.ToUpper();
                    Info.TransactionProcessDate = DateTime.Now.ToShortDateString();
                    Info.BRC_Arrears = objResponse1[0].ARREAR;

                    db.CustomerPaymentInfos.Add(Info);
                    db.SaveChanges();

                    Status = "VERIFY";

                    CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
                    string trans_id2 = "";
                    if (CheckHim.Count > 0)
                    { 
                        AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                        CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
                        CustomerEmail = CustEmail;
                        AccountType = CheckHim.FirstOrDefault().AccountType;
                        CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
                        AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
                        trans_id = CheckHim.FirstOrDefault().TransactionID;
                        trans_id2 = RandomPassword.Generate(10).ToString();
                    }





                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                   "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                   + "&trans-id=" + trans_id);
                    HashCode = CreateHash(hashString.ToString(), Key);







                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
                var jsonResult0 = Json(new { AlternateCustReference = AlternateCustReference, CustEmail = CustEmail, CustomerPhone = CustomerPhone, Status = Status, MAPplicant = "", BRC_ID = "", CustomerName = "", result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult0;
            }
        }

         

        [HttpGet]
        public async Task<JsonResult> ww(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid, string MAPApplicantName)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            //check if the Account Number is Existing with MAP

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://dlenhanceuat.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            //if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            //{

            //    SparkBal = GetSparkBalance(AccountNo);
            //}



            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

            string CallbackURL = "http://map.nepamsonline.com/successPage.aspx";


            //GENERATE YOUTUBE-LIKE KEYS AND ID
            //StringBuilder builder = new StringBuilder();
            //Enumerable
            //   .Range(65, 26)
            //    .Select(e => ((char)e).ToString())
            //    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            //    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            //    .OrderBy(e => Guid.NewGuid())
            //    .Take(11)
            //    .ToList().ForEach(e => builder.Append(e));

            //LETS GENERATE A RANDOM NUMBER BASED ON OUR GENERATOR FOR THIS TRANSACTION


            //check if this GUY Has come Before

            var CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
            string CustEmail = "";
            string CustomerPhone = ""; string AlternateCustReference = "";
            string trans_id = RandomPassword.Generate(10).ToString();
            if (CheckHim.Count > 0)
            {

               AccountNo = CheckHim.FirstOrDefault().CustomerReference;
               CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
               AccountType = CheckHim.FirstOrDefault().AccountType;
               CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
               AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
               trans_id =  CheckHim.FirstOrDefault().TransactionID;
            }

           


           using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
           {
               string json = "{\"Username\":\"phed\"," +
                             "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                             "\"CustomerNumber\":\"" + AccountNo + "\"," +
                             "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                               "\"Mailid\":\"" + EmailAddress + "\"," +
                             "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
               streamWriter.Write(json);
               streamWriter.Flush();
               streamWriter.Close();
           }

          
            string CustomerEmail = EmailAddress.Trim();
            string Currency = "NGN";
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string ProductDescription = "MAP/Arrears Meter Payment";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace(@"\", string.Empty);
                result = result.Replace(@"\\", string.Empty);
                //check if the Customer Exists here

                if (result == "Customer Not Found")
                {
                    var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    return _jsonResult;
                }
                 
                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);

                AmountPaid = objResponse1[0].ARREAR;
                //AmountPaid = "100";
               
                string ChannelName = "WEB";
                string Location = "WEB";
                string PaymentMethod = "Debit Card";
                string InstitutionId = "PHED";
                string PaymentCurrency = "NGN";
                string BankName = "Arrears Payment";
                string BranchName = "ARREARS";
                string InstitutionName = "ARREARSBillClearance";
                string ItemName = "PHED Bill Payment";
                string ItemCode = "PHEDBillARREARS";
                string PaymentStatus = "PENDING";
                string IsReversal = "false";
                string HashCode = "";

                string MeterType = "";
                string MeterCost = "";
                string TransactionReference = "";
                string Status = "";

                if (CheckHim.Count > 0)
                {
                    trans_id = CheckHim.FirstOrDefault().TransactionID;
                    string trans_id2 = RandomPassword.Generate(10);


                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                    "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                    + "&trans-id=" + trans_id2);

                    MAPPayment pr = new MAPPayment();

                    pr.PaymentFor = "BRC"; 
                    pr.TicketId = trans_id; 
                    pr.TransRef = trans_id2; 

                    pr.AccountNo = CheckHim.FirstOrDefault().CustomerReference; 

                    pr.Amount = AmountPaid; 

                    pr.BSC = CheckHim.FirstOrDefault().BSC;
                    pr.IBC = CheckHim.FirstOrDefault().IBC;
                    pr.PaymentMode = "WEB";
                    pr.PaymentStatus = "NOTPAID";
                    pr.Phase = CheckHim.FirstOrDefault().MeterPhase;
                    db.MAPPayments.Add(pr);
                    db.SaveChanges();
                     
                    //Save the Customers Details to the Database

                    HashCode = CreateHash(hashString.ToString(), Key);

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR PAYMENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        Status = "APPROVED FOR PAYMENT";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ABOUTTOAPPLY")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        
                        Status = "ABOUTTOAPPLY";
                    }



                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PROCEEDTOMAPPAY")
                    {
                      


                        //Load the Parameters for the Meter Application

                        string TicketID = CheckHim.FirstOrDefault().TransactionID;

                        var Details = db.CUSTOMERS.FirstOrDefault(p => p.TransactionID == TicketID);
                        if (Details != null)
                        {

                            MeterType = Details.MeterType;

                            if (string.IsNullOrEmpty(Details.Email as string))
                            {
                                EmailAddress = "map@phed.com.ng";
                                CustomerEmail = "map@phed.com.ng";
                            }
                            else
                            {

                                EmailAddress = Details.Email;
                                CustomerEmail = Details.Email;
                            }


                            if (MeterType.Trim() == "SINGLE PHASE")
                            {
                                MeterCost = "38,841.7";
                                AmountPaid = "38841.7";

                            }

                            if (MeterType.Trim() == "THREE PHASE")
                            {
                                MeterCost = "70,834.8";
                                AmountPaid = "70834.8";
                            }

                            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                                             "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                                             + "&trans-id=" + trans_id2);
                            HashCode = CreateHash(hashString.ToString(), Key);
                            TransactionReference = trans_id2;

                            MAPPayment p = new MAPPayment();
                            p.PaymentFor = "METER";
                            p.TicketId = CheckHim.FirstOrDefault().TransactionID;
                            p.TransRef = trans_id2;
                            p.AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                            p.Amount = MeterCost;
                            p.BSC = CheckHim.FirstOrDefault().BSC;
                            p.IBC = CheckHim.FirstOrDefault().IBC;
                            p.PaymentMode = "WEB";
                            p.PaymentStatus = "NOTPAID";
                            p.Phase = CheckHim.FirstOrDefault().MeterPhase;
                            db.MAPPayments.Add(p);
                            db.SaveChanges();
                            Status = "PROCEEDTOMAPPAY";
                        }
                       
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "HASARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "HASARREARS";
                    } 
                    
                    
                    
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "ARREARSPERCENT")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "ARREARSPERCENT";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "MAPAPPLIED")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "MAPAPPLIED";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAYARREARS")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAYARREARS";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "PAID FOR METER")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "PAID FOR METER";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "GOBRC")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "GOBRC";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "BRCDONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b 
                        Status = "BRCDONE";
                    }

                    
                         
                     

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLMENTAL")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        //08036978187
                        Status = "INSTALLMENTAL";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAP PAID
                        //BRC has b  
                        

                        Status = "APPROVED FOR INSTALLATION";
                    }

                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "INSTALLATION DONE")
                    {
                        //NEW
                        //PAYBRC
                        //PAYMAP
                        //MAPAPPROVED
                        //MAPPAID
                        //BRC has b  
                        Status = "INSTALLATION DONE";
                    }
                    if (CheckHim.FirstOrDefault().MAPApplicationStatus == "VERIFY")
                    { 
                        Status = "VERIFY";
                    }

                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }

                else
                {
                    //Send and Email and SMS to the Customer

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                    mail.Subject = "Meter Asset Provider Information From PHED";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    mail.Bcc.Add("payments@phed.com.ng");
                    mail.To.Add(CustomerEmail);
                    string RecipientType = "";

                    string SMTPMailServer = "mail.phed.com.ng";

                    SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                    MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                    //  string htmlMsgBody = this.EmailTextBox.Text;
                    string htmlMsgBody = "<html><head></head>";
                    htmlMsgBody = htmlMsgBody + "<body>";
                    //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                    htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + MAPApplicantName + "," + "</P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your Interest in Procuring a Meter. Please find below your Ticket ID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + MAPApplicantName + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Visit Date: " + DateTime.Now.AddHours(8) + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + trans_id + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + AccountNo + " </P>";
 
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    htmlMsgBody = htmlMsgBody + "Thank you,";
                    htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    mail.Body = htmlMsgBody;

                    //-Merchant’s Name
                    //-Merchant’s Url
                    //-Description of the Service/Goods


                    MailSMTPserver.Send(mail);

                      
                    hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                    "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                    + "&trans-id=" + trans_id);
                    HashCode = CreateHash(hashString.ToString(), Key);
                    CustomerPaymentInfo Info = new CustomerPaymentInfo();
                    Info.Amount = AmountPaid;
                    Info.ItemAmount = AmountPaid;
                    Info.ItemCode = ProductId;
                    Info.TransactionID = trans_id;
                    Info.PaymentMethod = "WEB";
                    Info.CustomerName = objResponse1[0].CONS_NAME;
                    string CustomerName = objResponse1[0].CONS_NAME;
                    Info.CustomerEmail = CustomerEmail;
                    Info.CustomerPhoneNumber = PhoneNumber;
                    Info.DepositorName = objResponse1[0].CONS_NAME;
                    Info.DepositSlipNumber = trans_id;
                    Info.InstitutionId = InstitutionId;
                    Info.InstitutionName = InstitutionName;
                    Info.ItemName = ItemName;
                    Info.ChannelName = ChannelName;
                    Info.PaymentCurrency = PaymentCurrency;
                    Info.IsReversal = IsReversal;
                    Info.IBC = objResponse1[0].IBC_NAME;
                    Info.BSC = objResponse1[0].BSC_NAME;
                    Info.PaymentStatus = PaymentStatus;
                    Info.BankName = BankName;
                    Info.BranchName = BranchName;
                    Info.Location = Location;
                    Info.CustomerAddress = objResponse1[0].ADDRESS;
                    Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                    Info.AlternateCustReference = objResponse1[0].METER_NO;
                    string BRC_ID = "MAP" + RandomPassword.Generate(10).ToString();
                    Info.BRC_ID = BRC_ID;
                    Info.Token = "MAP";
                    Info.PaymentMethod = PaymentMethod;
                    Info.MAPApplicationStatus = "VERIFY";
                    Info.MAPPaymentStatus = "NOT PAID";
                    Info.BRCPaymentStatus = "NOT PAID";
                    Info.PaymentStatus = "NOT PAID";
                    Info.MAPCustomerName = MAPApplicantName;
                    string MAPplicant = MAPApplicantName;
                    Info.AccountType = AccountType.ToUpper();
                    Info.TransactionProcessDate = DateTime.Now.ToShortDateString();
                    Info.BRC_Arrears = objResponse1[0].ARREAR;

                    db.CustomerPaymentInfos.Add(Info);
                    db.SaveChanges();

                    Status = "VERIFY";
                     
                     CheckHim = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && (p.AlternateCustReference == AccountNo || p.CustomerReference == AccountNo || p.TransactionID == AccountNo)).ToList();
                     string trans_id2 = "";
                    if (CheckHim.Count > 0)
                    {

                        AccountNo = CheckHim.FirstOrDefault().CustomerReference;
                        CustEmail = CheckHim.FirstOrDefault().CustomerEmail;
                        AccountType = CheckHim.FirstOrDefault().AccountType;
                        CustomerPhone = CheckHim.FirstOrDefault().CustomerPhoneNumber;
                        AlternateCustReference = CheckHim.FirstOrDefault().AlternateCustReference;
                        trans_id = CheckHim.FirstOrDefault().TransactionID;
                        trans_id2 = RandomPassword.Generate(10).ToString();
                    }
                     
                    var jsonResult = Json(new { AlternateCustReference = AlternateCustReference, trans_id2 = trans_id2, CustEmail = CustEmail, CustomerPhone = CustomerPhone, MeterType = MeterType, TransactionReference = TransactionReference, MeterCost = MeterCost, Status = Status, MAPplicant = CheckHim.FirstOrDefault().MAPCustomerName, BRC_ID = CheckHim.FirstOrDefault().BRC_ID, CustomerName = CheckHim.FirstOrDefault().CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                     
                }
                var jsonResult0 = Json(new { AlternateCustReference = AlternateCustReference, CustEmail = CustEmail, CustomerPhone = CustomerPhone, Status = Status, MAPplicant = "", BRC_ID = "", CustomerName = "", result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult0;
            }
        }

 
 
 
        public string Hasher(string message, string key)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);

            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            HMACSHA256 HMac256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);
            byte[] Hash256Message = HMac256.ComputeHash(messageBytes);
            //this.hmac1.Text = ByteToString(hashmessage); 
            // byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
            //this.hmac2.Text = ByteToString(hashmessage);

            return ByteToStrings(Hash256Message);
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







	}
}