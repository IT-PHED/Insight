using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace PHEDServe.Controllers
{ 
    public class PaymentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        bool invalid = false;

        private static readonly HttpClient client = new HttpClient();
        //
        // GET: /Payments/
        public ActionResult Index()
        {
            //var AllPayments = db
            return View();
        }
         
        public ActionResult ClaimPayments()
        {
            //var AllPayments = db
            return View();
        }

        public ActionResult UploadBankStatement()
        {
            //var AllPayments = db
            return View();
        }
        
        //Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped 
        
        public ActionResult CustomerCare()
        { 
            //var AllPayments = db
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> UploadBulkMeter(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var DocumentId = Guid.NewGuid().ToString();
            UploadedFilesVM ErrorLog = new UploadedFilesVM();
            List<UploadedFilesVM> ErrorLogList = new List<UploadedFilesVM>();
            AppViewModels viewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];

                //var StudentCode = System.Web.HttpContext.Current.Request.Files["StudentCode"];
                //var DatePaid = System.Web.HttpContext.Current.Request.Files["DatePaid"];
                //var BankName = System.Web.HttpContext.Current.Request.Files["BankName"];
                //var TellerNo = System.Web.HttpContext.Current.Request.Files["TellerNo"];
                //var AmountPaid = System.Web.HttpContext.Current.Request.Files["AmountPaid"];
                //var ScheduleCode = System.Web.HttpContext.Current.Request.Files["ScheduleCode"];


                //var DocumentFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                //var _StudentCode = System.Web.HttpContext.Current.Request.Params["StudentCode"];
                var DocumentName = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                var Status = System.Web.HttpContext.Current.Request.Params["Status"];
                var MeterVendor = System.Web.HttpContext.Current.Request.Params["MAPVendor"];

                //var _TellerNo = System.Web.HttpContext.Current.Request.Params["TellerNo"];
                //var _DocumentSize = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                //var _DatePaid = System.Web.HttpContext.Current.Request.Params["DatePaid"];
                //var _ScheduleCode = System.Web.HttpContext.Current.Request.Params["ScheduleCode"];
                //var _BankName = System.Web.HttpContext.Current.Request.Params["BankName"];
                //var _AmountPaid = System.Web.HttpContext.Current.Request.Params["AmountPaid"];

                //string ScheduleCode = _ScheduleCode.ToString();
                //string StudentCode = _StudentCode.ToString();


                //DirectPayments backbone = db.ACCOUNT_FEES_BACKBONE_MASTERS.FirstOrDefault(p => p.SCHEDULE_CODE == ScheduleCode && p.STUDENT_CODE == StudentCode);


                //if (backbone == null)
                //{
                //    //return Error
                //    var result = JsonConvert.SerializeObject(viewModel);

                //    var jsonResult = Json(new { result = result, error = "The Invoice you're trying to pay for does not Exist. Please try again" }, JsonRequestBehavior.AllowGet);
                //    jsonResult.MaxJsonLength = int.MaxValue;
                //    return jsonResult;
                //}
                //else
                //{
                //    backbone.DATE_PAID = Convert.ToDateTime(_DatePaid.ToString());
                //    backbone.DOCUMENT_PATH = "/Documents/" + DocumentName;
                //    backbone.TELLER_NO = _TellerNo.ToString();
                //    backbone.AMOUNT_PAID = _AmountPaid.ToString();
                //    backbone.STATUS = "PENDING";
                //    backbone.APPROVED_STATUS = "NOT APPROVED";
                //    backbone.PAYMENT_METHOD = "BANK";

                //    db.Entry(backbone).State = EntityState.Modified;
                //    db.SaveChanges();

                //}

                //   LoanApplicationId = System.Web.HttpContext.Current.Request.Params["LoanApplicationId"];



                // var fileSavePath = Server.MapPath("~/Uploads/"+DocumentName);
                //  var fileSavePath = Path.Combine(Server.MapPath("~/Documents"), DocumentName);


                //httpPostedFile.SaveAs(fileSavePath);


                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);
                // var fileSavePath = "http://map.nepamsonline.com/Documents/" + DocumentName;

                Console.Write(fileSavePath.ToString());
                Console.WriteLine(fileSavePath.ToString());
                httpPostedFile.SaveAs(fileSavePath);

                DataSet ds = new DataSet();

                //string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileSavePath + ";Extended Properties=Excel 12.0;";
                string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileSavePath + ";Extended Properties=Excel 12.0;";

                using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                    {
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        string query = "SELECT * FROM [" + sheetName + "]";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);

                        //DataSet ds = new DataSet();
                        adapter.Fill(ds, "Items");

                        if (ds.Tables.Count == 0)
                        {
                            // return View("Index");
                        }

                        if (ds.Tables.Count > 0)
                        {
                            //TaxAgency agency = new TaxAgency();
                            //DirectPayments d = new DirectPayments();
                            MeterList d = new MeterList();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ErrorLog = new UploadedFilesVM();
                                //string POSTINGDATE = ds.Tables[0].Rows[i]["POSTINGDATE"].ToString();
                                //string MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                //string Status = ds.Tables[0].Rows[i]["Status"].ToString();
                                string MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();

                                d = new MeterList();
                                d.MeterNo = MeterNo;
                                d.MAPVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                d.InstallationStatus = Status;

                                db.MeterLists.Add(d);
                                db.SaveChanges();

                                ErrorLog.MeterNo = MeterNo;
                                ErrorLog.MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                ErrorLog.Status = "FILE UPLOADED";
                                ErrorLog.DateUploaded = MeterVendor;
                                ErrorLogList.Add(ErrorLog);
                            }

                        }
                    }
                }
            }

            // viewModel.TaxPayersList = db.TaxPayerss.Where(p => (p.TaxPayerName == "")).ToList();
            viewModel.UplodedStatusList = ErrorLogList;
            //viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOT CLAIMED").ToList();
            var _result = JsonConvert.SerializeObject(viewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }









        [HttpPost]

        public async Task<JsonResult> UploadAgency(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            var DocumentId = Guid.NewGuid().ToString();
            UploadedFilesVM ErrorLog = new UploadedFilesVM();
            List<UploadedFilesVM> ErrorLogList = new List<UploadedFilesVM>();
            AppViewModels viewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];

                //var StudentCode = System.Web.HttpContext.Current.Request.Files["StudentCode"];
                //var DatePaid = System.Web.HttpContext.Current.Request.Files["DatePaid"];
                //var BankName = System.Web.HttpContext.Current.Request.Files["BankName"];
                //var TellerNo = System.Web.HttpContext.Current.Request.Files["TellerNo"];
                //var AmountPaid = System.Web.HttpContext.Current.Request.Files["AmountPaid"];
                //var ScheduleCode = System.Web.HttpContext.Current.Request.Files["ScheduleCode"];


                //var DocumentFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                //var _StudentCode = System.Web.HttpContext.Current.Request.Params["StudentCode"];
                var DocumentName = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                //var _TellerNo = System.Web.HttpContext.Current.Request.Params["TellerNo"];
                //var _DocumentSize = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                //var _DatePaid = System.Web.HttpContext.Current.Request.Params["DatePaid"];
                //var _ScheduleCode = System.Web.HttpContext.Current.Request.Params["ScheduleCode"];
                //var _BankName = System.Web.HttpContext.Current.Request.Params["BankName"];
                //var _AmountPaid = System.Web.HttpContext.Current.Request.Params["AmountPaid"];

                //string ScheduleCode = _ScheduleCode.ToString();
                //string StudentCode = _StudentCode.ToString();


                //DirectPayments backbone = db.ACCOUNT_FEES_BACKBONE_MASTERS.FirstOrDefault(p => p.SCHEDULE_CODE == ScheduleCode && p.STUDENT_CODE == StudentCode);


                //if (backbone == null)
                //{
                //    //return Error
                //    var result = JsonConvert.SerializeObject(viewModel);

                //    var jsonResult = Json(new { result = result, error = "The Invoice you're trying to pay for does not Exist. Please try again" }, JsonRequestBehavior.AllowGet);
                //    jsonResult.MaxJsonLength = int.MaxValue;
                //    return jsonResult;
                //}
                //else
                //{
                //    backbone.DATE_PAID = Convert.ToDateTime(_DatePaid.ToString());
                //    backbone.DOCUMENT_PATH = "/Documents/" + DocumentName;
                //    backbone.TELLER_NO = _TellerNo.ToString();
                //    backbone.AMOUNT_PAID = _AmountPaid.ToString();
                //    backbone.STATUS = "PENDING";
                //    backbone.APPROVED_STATUS = "NOT APPROVED";
                //    backbone.PAYMENT_METHOD = "BANK";

                //    db.Entry(backbone).State = EntityState.Modified;
                //    db.SaveChanges();

                //}

                //   LoanApplicationId = System.Web.HttpContext.Current.Request.Params["LoanApplicationId"];



                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);


                httpPostedFile.SaveAs(fileSavePath);


                DataSet ds = new DataSet();

                string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileSavePath + ";Extended Properties=Excel 12.0;";

                using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                    {
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        string query = "SELECT * FROM [" + sheetName + "]";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);

                        //DataSet ds = new DataSet();
                        adapter.Fill(ds, "Items");

                        if (ds.Tables.Count == 0)
                        {
                            // return View("Index");
                        }

                        if (ds.Tables.Count > 0)
                        {
                            //TaxAgency agency = new TaxAgency();
                            DirectPayments d = new DirectPayments();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ErrorLog = new UploadedFilesVM();
                                string POSTINGDATE = ds.Tables[0].Rows[i]["POSTINGDATE"].ToString();
                                string TRANSACTIONREFERENCE = ds.Tables[0].Rows[i]["TRANSACTIONREFERENCE"].ToString();
                                string AMOUNT = ds.Tables[0].Rows[i]["AMOUNT"].ToString();
                                string TYPE = ds.Tables[0].Rows[i]["TYPE"].ToString();

                                DateTime x = Convert.ToDateTime(POSTINGDATE);

                                if (TYPE == "C")
                                {
                                    //its a Credit


                                    //check if the Transaction has been done Before.


                                    var Check = db.DirectPaymentss.Where(p => p.Datepaid == x && p.PaymentDescription.Trim() == TRANSACTIONREFERENCE).ToList();


                                    if (Check.Count > 0)
                                    {
                                        //the payment is There before
                                        ErrorLog.Description = TRANSACTIONREFERENCE;
                                        ErrorLog.Status = "DUPLICATE UPLOAD";
                                        ErrorLog.DateUploaded = POSTINGDATE;

                                    }
                                    else
                                    {
                                        d = new DirectPayments();
                                        d.PaymentDescription = TRANSACTIONREFERENCE;
                                        d.PaymentId = Guid.NewGuid().ToString();
                                        d.Status = "NOTCLAIMED";
                                        d.Datepaid = x;
                                        d.Amount = AMOUNT;
                                        db.DirectPaymentss.Add(d);
                                        db.SaveChanges();

                                        ErrorLog.Description = TRANSACTIONREFERENCE;
                                        ErrorLog.Status = "FILE UPLOADED";
                                        ErrorLog.DateUploaded = POSTINGDATE;
                                    }

                                }
                                // agency = new TaxAgency();
                            } 
                            //Add the log to the lists

                            ErrorLogList.Add(ErrorLog);
                            //    //Now we can insert this data to database... 
                            //    string AgencyID = RandomPassword.Generate(5); 
                            //    string AgencyName = ds.Tables[0].Rows[i]["AgencyName"].ToString();

                            //    agency.AgencyID = AgencyID;
                            //    agency.AgencyName = AgencyName; 
                            //    db.TaxAgencys.Add(agency);
                            //    await db.SaveChangesAsync(); 
                            //}
                        }
                    }
                }
            }

            // viewModel.TaxPayersList = db.TaxPayerss.Where(p => (p.TaxPayerName == "")).ToList();
            viewModel.UplodedStatusList = ErrorLogList;
            viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOT CLAIMED").ToList();
            var _result = JsonConvert.SerializeObject(viewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }


        //public JsonResult loadReferenceViewModel()
        //{
        //    var context = new ApplicationDbContext();
        //    var username = User.Identity.Name;
        //    AppViewModels viewModel = new AppViewModels();
        //    viewModel.PaymentList = db.PaymentAdmin_VWs.ToList();
        //    //viewModel.BSCList = new BusinessServiceCenter();
        //    //viewModel.IBCList = new IntegratedServiceCenter();
        //    //viewModel.MarketerList = new List<Marketer>();
        //    var result = JsonConvert.SerializeObject(viewModel);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult BindCustomers()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            DateTime TodaysDate = DateTime.Now;
          //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
            viewModel.PaymentList = new List<CustomerPaymentInfo>();// db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == "27100144685").ToList();
            //viewModel.BSCList = new BusinessServiceCenter();
            //viewModel.IBCList = new IntegratedServiceCenter();
            //viewModel.MarketerList = new List<Marketer>();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
   public JsonResult DirectPaymentClaims()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels(); 
            DateTime TodaysDate = DateTime.Now;
          //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
            viewModel.PaymentList = new List<CustomerPaymentInfo>();// db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == "27100144685").ToList();
            //viewModel.BSCList = new BusinessServiceCenter();
            //viewModel.IBCList = new IntegratedServiceCenter();
            //viewModel.MarketerList = new List<Marketer>();
            viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOTCLAIMED").ToList();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //ApplyPaymentDirect',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    data: JSON.stringify({
        //        'AccountNumber': self.Amount(),
        //        'Amount': self.Amount()





   [HttpPost]
   public JsonResult VerifyAccountNo(string AccountNumber)
   {
       //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
       //return _jsonResults;

       var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
       httpWebRequest.ContentType = "application/json";
       httpWebRequest.Method = "POST";
       //AccountNo = "861137381801";
       // AmountPaid = "450066";
      string  PhoneNumber = "08067807821";
      string  EmailAddress = "ebukaegonu@yahoo.com";

       string SparkBal = "";
       using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
       {
           string json = "{\"Username\":\"phed\"," +
                         "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                         "\"CustomerNumber\":\"" + AccountNumber + "\"," +
                         "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                           "\"Mailid\":\"" + EmailAddress + "\"," +
                         "\"CustomerType\":\"" + "POSTPAID" + "\"}";
           streamWriter.Write(json);
           streamWriter.Flush();
           streamWriter.Close();
       }

       //perform the hash here before going to the Payment Page

       

       var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

       using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
       {
           var result = streamReader.ReadToEnd();
           result = result.Replace("\r", string.Empty);
           result = result.Replace("\n", string.Empty);
           result = result.Replace(@"\", string.Empty);
           result = result.Replace(@"\\", string.Empty);

           //check if the Customer Exists here
           var objResponse1 = JsonConvert.DeserializeObject<List<TokenJSONObject>>(result);
           if (result == "Customer Not Found")
           {
               var _jsonResult = Json(new { result = result, Message = "Error" }, JsonRequestBehavior.AllowGet);
               return _jsonResult;
           }


           var jsonResult = Json(new { result = result, Message = "Success", AccountName = objResponse1[0].CONS_NAME }, JsonRequestBehavior.AllowGet);
           return jsonResult;

           //var Data = new { data = stuff, data2 = otherstuff };
       }
   }







   [HttpPost]
   public JsonResult ApplyPaymentDirect(string AccountNumber, string Amount,string  PaymentId)
   {

       var context = new ApplicationDbContext();
       var username = User.Identity.Name;
       AppViewModels viewModel = new AppViewModels();


     
       string PaymentReferece = RandomPassword.Generate(6);


       string APIKey = "2E639809-58B0-49E2-99D7-38CB4DF2B5B20";
           string Username = "phed";
           string CustReference = AccountNumber;
           string AlternateCustReference = AccountNumber;
           string PaymentLogId = PaymentReferece;
           string PaymentMethod = "Direct Payment Claim";
           string PaymentReference = PaymentReferece;
           string TerminalID = null;
           string ChannelName = "DIRECTPAYMENT";
           string Location = null;
           string InstitutionId = "02";
           string PaymentCurrency = "NGN";
           string DepositSlipNumber = PaymentReferece;
           string DepositorName = "Direct Payment Capture";
           string CustomerPhoneNumber = "00000000000";
           string CustomerAddress = null;
           string BankCode = "023";
           string CollectionsAccount = null;
           string ReceiptNo = PaymentReferece;
           string OtherCustomerInfo = null;
           string CustomerName = "Direct Payment Capture";
           string BankName = "CITI BANK";
           string BranchName = "CITI BANK";
           string InstitutionName = "PHEDBillPay";
           string ItemName = "PHED Bill Payment";
           string ItemCode = "01";
           string ItemAmount = Amount;
          string  PaymentStatus = "Success";
           string IsReversal = null;
           string SettlementDate = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
           string Teller = null;




           //string APIKey = "2E639809-58B0-49E2-99D7-38CB4DF2B5B20";
           //string Username = "phed";
           //string CustReference = CustomerDetails.CustomerReference;
           //string AlternateCustReference = CustomerDetails.AlternateCustReference;
           //string PaymentLogId = transaction_id;
           //string Amount = CustomerDetails.Amount;
           //string PaymentMethod = "Own Bank Cheque";
           //string PaymentReference = CustomerDetails.PaymentReference;
           //string TerminalID = null;
           //string ChannelName = "BANKBRANCH";
           //string Location = null;
           //string InstitutionId = "02";
           //string PaymentCurrency = "NGN";
           //string DepositSlipNumber = transaction_id;
           //string DepositorName = CustomerDetails.DepositorName.ToString();
           //string CustomerPhoneNumber = CustomerDetails.CustomerPhoneNumber;
           //string CustomerAddress = CustomerDetails.CustomerAddress;
           //string BankCode = "023";
           //string CollectionsAccount = null;
           //string ReceiptNo = transaction_id;
           //string OtherCustomerInfo = null;
           //string CustomerName = CustomerDetails.CustomerName;
           //string BankName = "CITI BANK";
           //string BranchName = "CITI BANK";
           //string InstitutionName = "PHEDBillPay";
           //string ItemName = "PHED Bill Payment";
           //string ItemCode = "01";
           //string ItemAmount = CustomerDetails.Amount;
           //PaymentStatus = "Success";
           //string IsReversal = null;
           //string SettlementDate = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
           //string Teller = null;


















           var values = new Dictionary<string, string>
                {
                { "username", "phed" },
                { "apikey", "2E639809-58B0-49E2-99D7-38CB4DF2B5B20" },
                { "PaymentLogId", PaymentLogId },
                { "CustReference", CustReference },
                { "AlternateCustReference", CustReference },
                { "Amount", Amount },
                { "PaymentMethod", "Own Bank Cheque" },
                { "PaymentReference", PaymentReference },
                { "TerminalID", null },
                { "ChannelName", "BANKBRANCH" },
                { "Location", null },
                { "PaymentDate", "22-10-2018 14:47:42"  },
                { "InstitutionId", null},
                { "InstitutionName", null },
                { "BankName", BankName },
                { "BranchName", BranchName },
                { "CustomerName", CustomerName},
                { "OtherCustomerInfo", OtherCustomerInfo },
                { "ReceiptNo", ReceiptNo},
                { "CollectionsAccount", CollectionsAccount },
                { "BankCode", "023" },
                { "CustomerAddress", CustomerAddress },
                { "CustomerPhoneNumber", CustomerPhoneNumber },
                { "DepositorName", DepositorName },
                { "DepositSlipNumber", DepositSlipNumber },
                { "PaymentCurrency", "NGN" }, 
                { "ItemName", null }, 
                { "ItemCode", "01" },
                 { "ItemAmount", ItemAmount },
                  { "PaymentStatus", "Success" },
                   { "IsReversal", null },
                    { "SettlementDate", DateTime.Now.ToString("dd-MM-yyyy h:mm tt") },
                     { "Teller", "CITITELLER3" },
                };
           var content = new FormUrlEncodedContent(values);

           var response = client.PostAsync("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/NotifyPayment", content);

           var responseString = response.Result.ToString();

        

           //.Content.ReadAsStringAsync();
           responseString = responseString.Replace("\r", string.Empty);
           responseString = responseString.Replace("\n", string.Empty);
           responseString = responseString.Replace(@"\", string.Empty);
           responseString = responseString.Replace(@"\\", string.Empty);


           if (responseString.Contains("'Internal Server Error"))
           {
               viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOTCLAIMED").ToList();
               var result = JsonConvert.SerializeObject(viewModel);
               return Json(new { result = result, Message = "Error" }, JsonRequestBehavior.AllowGet);


           }
           else
           {
               var objResponse1 = JsonConvert.DeserializeObject<List<TokenJSONObject>>(responseString);
               if (objResponse1.First().RECEIPTNUMBER != null)
               {

                   DirectPayments g = db.DirectPaymentss.Find(PaymentId);
                   if (g != null)
                   {
                       g.Status = "CLAIMED";
                       g.DateClaimed = DateTime.Now;
                       g.DateClaimedBy = User.Identity.Name;
                   }

                   viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOTCLAIMED").ToList();
                   var result = JsonConvert.SerializeObject(viewModel);
                   return Json(new { result = result, Message = "Success" }, JsonRequestBehavior.AllowGet);
               }

           }
           return Json(new { result = "", Message = "Success" }, JsonRequestBehavior.AllowGet);

   }
        [HttpPost]
        public JsonResult GetPaymentHistory(string MeterNo, string Status, string ConsumerType, string Datefrom, string DateTo)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            DateTime TodaysDate = DateTime.Now;

            if (Status == "ALL")
            {

                viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference == MeterNo)).ToList();

            }

            else if (Status == "ALL")
            {




            }
            viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference ==  MeterNo) && p.PaymentStatus == Status).ToList();
            //viewModel.BSCList = new BusinessServiceCenter();
            //viewModel.IBCList = new IntegratedServiceCenter();
            //viewModel.MarketerList = new List<Marketer>();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult GetPaymentHistory1(string MeterNo, string Status)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            DateTime TodaysDate = DateTime.Now;

            if (Status == "ALL")
            {

                viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference == MeterNo)).ToList();

            }

            else if (Status == "SUCCESS")
            {



                viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference == MeterNo) && p.PaymentStatus == Status).ToList();

            }

            else if (Status == "FAILED")
            {

                viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference == MeterNo) && p.PaymentStatus == Status).ToList();



            }
            else if (Status == "NO TOKEN")
            {


                viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == MeterNo || p.TransactionID == MeterNo || p.PaymentReference == MeterNo) && p.Token == "NOT AVAILABLE").ToList();


            }
            //viewModel.BSCList = new BusinessServiceCenter();
            //viewModel.IBCList = new IntegratedServiceCenter();
            //viewModel.MarketerList = new List<Marketer>();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SuccessQR(string hash, string hash_type, string merchant_id, string status_code, string status_msg, string transaction_id)
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
                        string Amounts = "";

                        //customer has not been given the Token Yet.

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
                        CustomerPaymentInfo CustomerDetails2 = db.CustomerPaymentInfos.Find(transactionID);
                        if (CustomerDetails != null)
                        {

                            if (CustomerDetails2.Token == "NOTAVAILABLE")
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

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();
                                    result = result.Replace("\r", string.Empty);
                                    result = result.Replace("\n", string.Empty);
                                    result = result.Replace(@"\", string.Empty);
                                    result = result.Replace(@"\\", string.Empty);

                                    //check if the Customer Exists here

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
                                        string Token = objResponse1[0].TOKENDESC.ToString();
                                        CustomerDetails2.Token = Token;
                                        CustomerDetails2.Units = objResponse1[0].UNITSACTUAL.ToString();
                                        CustomerDetails2.Tarriff = objResponse1[0].TARIFF.ToString();
                                    }
                                    CustomerDetails2.TokenDate = objResponse1[0].PAYMENTDATETIME.ToString();
                                    CustomerDetails2.ReceiptNo = ReceiptNo;
                                    db.Entry(CustomerDetails2).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }

                            }

                        }

                    }
                }
                else
                {

                    @ViewBag.Error = "Error";

                }
            }

            ViewBag.Id = transaction_id;

            return View();
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

        [HttpGet]
        public async Task<JsonResult> SendEmail(string id)
        {
            var jsonResult = Json("", JsonRequestBehavior.AllowGet);
            db = new ApplicationDbContext();
 
            CustomerPaymentInfo CustomerDetails2 = await db.CustomerPaymentInfos.FindAsync(id);
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
                            // mail.Bcc.Add("payments@phed.com.ng");
                            mail.To.Add(CustomerDetails2.CustomerEmail);
                            string RecipientType = "";
                            string SMTPMailServer = "mail.phed.com.ng";
                            SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                            MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                            //  string htmlMsgBody = this.EmailTextBox.Text;



                            string body = string.Empty;
                            //using streamreader for reading my htmltemplate   

                            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/PREPAID.html")))
                            {
                                body = reader.ReadToEnd();
                            }

                            body = body.Replace("{{CustomerName}}", CustomerDetails2.CustomerName);
                            body = body.Replace("{{PurchaseAmount}}", CustomerDetails2.Amount);
                            body = body.Replace("{{TransactionId}}", CustomerDetails2.TransactionID);
                            body = body.Replace("{{AccountName}}", CustomerDetails2.CustomerName);
                            body = body.Replace("{{AccountNo}}", CustomerDetails2.CustomerReference);
                            body = body.Replace("{{MeterNo}}", CustomerDetails2.AlternateCustReference);
                            body = body.Replace("{{AccountType}}", CustomerDetails2.AccountType);
                            body = body.Replace("{{TransactionDate}}", CustomerDetails2.DatePaid.ToString());
                            body = body.Replace("{{CardType}}", CustomerDetails2.CardType);
                            body = body.Replace("{{TransactionReference}}", CustomerDetails2.PaymentReference);
                            body = body.Replace("{{Bank}}", CustomerDetails2.BankName);
                            body = body.Replace("{{Token}}", CustomerDetails2.Token);
                            body = body.Replace("{{AmountPaid}}", CustomerDetails2.Amount);
                            body = body.Replace("{{CustomerAddress}}", CustomerDetails2.CustomerAddress);
                            mail.Body = body;
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

                            string body = string.Empty;
                            //using streamreader for reading my htmltemplate   

                            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate/POSTPAID.html")))
                            {
                                body = reader.ReadToEnd();
                            }

                            body = body.Replace("{{CustomerName}}", CustomerDetails2.CustomerName); //replacing the required things  

                            body = body.Replace("{{PurchaseAmount}}", CustomerDetails2.Amount);

                            body = body.Replace("{{TransactionId}}", CustomerDetails2.TransactionID);
                            body = body.Replace("{{AccountName}}", CustomerDetails2.CustomerName);
                            body = body.Replace("{{AccountNo}}", CustomerDetails2.CustomerReference);
                            body = body.Replace("{{MeterNo}}", CustomerDetails2.AlternateCustReference);
                            body = body.Replace("{{AccountType}}", CustomerDetails2.AccountType);
                            body = body.Replace("{{TransactionDate}}", CustomerDetails2.DatePaid.ToString());
                            body = body.Replace("{{CardType}}", CustomerDetails2.CardType);
                            body = body.Replace("{{TransactionReference}}", CustomerDetails2.PaymentReference);
                            body = body.Replace("{{Bank}}", CustomerDetails2.BankName);
                            body = body.Replace("{{Token}}", CustomerDetails2.Token);
                            //body = body.Replace("{{VAT}}", CustomerDetails2.);
                            body = body.Replace("{{AmountPaid}}", CustomerDetails2.Amount);
                            body = body.Replace("{{CustomerAddress}}", CustomerDetails2.CustomerAddress);
                            mail.Body = body;

                           // MailSMTPserver.Send(mail);
                        }
                    }
                }

                #endregion
            }

            return jsonResult;
        }






        //public async Task<JsonResult> SendEmail(string id)
        //     {
        //         var jsonResult = Json("", JsonRequestBehavior.AllowGet);
        //         db = new ApplicationDbContext();


        //         CustomerPaymentInfo CustomerDetails2 = await db.CustomerPaymentInfos.FindAsync(id);
        //         string Amounts = "";


        //         if (CustomerDetails2 != null)
        //         {
        //             #region SendEmail
        //             if (CustomerDetails2.Token != "NOTAVAILABLE" && CustomerDetails2.AccountType == "PREPAID" && CustomerDetails2.PaymentStatus == "SUCCESS")
        //             {
        //                 //Send Email again

        //                 if (CustomerDetails2 != null)
        //                 {
        //                     if (CustomerDetails2.CustomerEmail != "")
        //                     {
        //                         //SEND EMAIL TO THE CUSTOMER
        //                         MailMessage mail = new MailMessage();

        //                         mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
        //                         mail.Subject = "Payment Information From PHED";
        //                         mail.IsBodyHtml = true;
        //                         mail.Priority = MailPriority.High;
        //                         mail.Bcc.Add("payments@phed.com.ng");
        //                         mail.To.Add(CustomerDetails2.CustomerEmail);
        //                         string RecipientType = "";

        //                         string SMTPMailServer = "mail.phed.com.ng";

        //                         SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
        //                         MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
        //                         //  string htmlMsgBody = this.EmailTextBox.Text;
        //                         string htmlMsgBody = "<html><head></head>";
        //                         htmlMsgBody = htmlMsgBody + "<body>";

        //                         //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";

        //                         htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + CustomerDetails2.CustomerName + "</P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for Paying your energy bills. Please find below your payment details" + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + CustomerDetails2.CustomerName + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Date: " + CustomerDetails2.TokenDate + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Type: " + CustomerDetails2.CardType + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Brand: " + CustomerDetails2.CardBrand + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Amount: ₦" + CustomerDetails2.Amount + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Token Key: " + CustomerDetails2.Token + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Units: " + CustomerDetails2.Units + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Currency: NAIRA " + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Merchant Name: Port-Harcourt Electricity Distribution Company" + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Merchant URL: www.phed.com.ng" + " </P>";

        //                         htmlMsgBody = htmlMsgBody + "<br><br>";

        //                         htmlMsgBody = htmlMsgBody + "Thank you,";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";

        //                         htmlMsgBody = htmlMsgBody + "<br><br>";
        //                         mail.Body = htmlMsgBody;

        //                         //-Merchant’s Name
        //                         //-Merchant’s Url
        //                         //-Description of the Service/Goods

        //                         MailSMTPserver.Send(mail);

        //                     }
        //                 }
        //             }

        //             else  if (CustomerDetails2.AccountType == "POSTPAID" && CustomerDetails2.PaymentStatus == "SUCCESS")

        //             {
        //                 if (CustomerDetails2 != null)
        //                 {


        //                     if (CustomerDetails2.CustomerEmail != "")
        //                     {
        //                         //SEND EMAIL TO THE CUSTOMER
        //                         MailMessage mail = new MailMessage();

        //                         mail.From = new MailAddress("Payments@phed.com.ng", "Port-harcourt Electricity Distribution Company");
        //                         mail.Subject = "Payment Information From PHED";
        //                         mail.IsBodyHtml = true;
        //                         mail.Priority = MailPriority.High;
        //                         mail.Bcc.Add("payments@phed.com.ng");
        //                         mail.To.Add(CustomerDetails2.CustomerEmail);
        //                         string RecipientType = "";

        //                         string SMTPMailServer = "mail.phed.com.ng";

        //                         SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
        //                         MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
        //                         //  string htmlMsgBody = this.EmailTextBox.Text;
        //                         string htmlMsgBody = "<html><head></head>";
        //                         htmlMsgBody = htmlMsgBody + "<body>";

        //                         //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";

        //                         htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + CustomerDetails2.CustomerName + "</P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for Paying your energy bills. Please find below your payment details" + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + CustomerDetails2.CustomerName + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Date: " + CustomerDetails2.TokenDate + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Type: " + CustomerDetails2.CardType + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Card Brand: " + CustomerDetails2.CardBrand + " </P>";

        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Status: " + CustomerDetails2.PaymentStatus + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction reference: " + CustomerDetails2.PaymentReference + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction ID: " + CustomerDetails2.TransactionID + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Amount: ₦" + CustomerDetails2.Amount + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Transaction Currency: NAIRA " + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Merchant Name: Port-Harcourt Electricity Distribution Company" + " </P>";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "Merchant URL: www.phed.com.ng" + " </P>";

        //                         htmlMsgBody = htmlMsgBody + "<br><br>";
        //                         htmlMsgBody = htmlMsgBody + "Thank you,";
        //                         htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
        //                         htmlMsgBody = htmlMsgBody + "<br><br>";
        //                         mail.Body = htmlMsgBody;

        //                         //-Merchant’s Name
        //                         //-Merchant’s Url
        //                         //-Description of the Service/Goods

        //                         MailSMTPserver.Send(mail);
        //                     }
        //                 }
        //             }

        //             #endregion
        //         }

        //         return jsonResult;
        //     }



        [HttpGet]
        public async Task<JsonResult> ShowDetails(string id)
        {
            var jsonResult = Json("", JsonRequestBehavior.AllowGet);
            db = new ApplicationDbContext();
            CustomerPaymentInfo CustomerDetails = await db.CustomerPaymentInfos.FindAsync(id);
            string Amounts = "";

            if (CustomerDetails.Token != "NOTAVAILABLE" && CustomerDetails.PaymentStatus == "SUCCESS")
            {

                jsonResult = Json(new[] { new 
                {
                        CUSTOMER_NO = CustomerDetails.CustomerReference,
                        METER_NO = CustomerDetails.AlternateCustReference,
                        RECEIPTNUMBER = CustomerDetails.ReceiptNo,
                        PAYMENTDATETIME = CustomerDetails.DatePaid,
                        AMOUNT= CustomerDetails.Amount,
                        TOKENDESC =  CustomerDetails.Token, 
                        UNITSACTUAL = CustomerDetails.Units,
                        ADDRES =  CustomerDetails.CustomerAddress,

                        TARIFF =  CustomerDetails.Tarriff ,
                         STATUS =  CustomerDetails.PaymentStatus ,
                         CONS_NAME = CustomerDetails.CustomerName,
                         CON_TYPE = CustomerDetails.AccountType,
                         ADDRESS = CustomerDetails.CustomerAddress,
                         PAYMENT_PURPOSE = "",
                         MR_NAME = "",
                         COL_TIME = "",
                         PYMNT_TYPE = "",
                         TARIFFCODE = "",
                         TARIFF_RATE = "",
                         TARIFF_INDEX = "",
                         INCIDENT_TYPE = "",
                         CHEQUEDATE = "",
                         CHEQUENO = "",
                         BANKNAME = "",
                         IBC = "",
                         BSC = ""  
                    }}, JsonRequestBehavior.AllowGet);

            }

            else
            {

                jsonResult = Json(new[] { new 
                {
                        CUSTOMER_NO = CustomerDetails.CustomerReference,
                        METER_NO = CustomerDetails.AlternateCustReference,
                        RECEIPTNUMBER = "PAYMENT UNSUCCESSFUL",
                        PAYMENTDATETIME = CustomerDetails.DatePaid,
                        AMOUNT= "PAYMENT UNSUCCESSFUL",
                        TOKENDESC =  "PAYMENT UNSUCCESSFUL",
                          TARIFF = "PAYMENT UNSUCCESSFUL",
                         STATUS =  "PAYMENT UNSUCCESSFUL",
                         CONS_NAME = "PAYMENT UNSUCCESSFUL",
                         CON_TYPE = CustomerDetails.AccountType,
                         ADDRESS = CustomerDetails.CustomerAddress,
                         PAYMENT_PURPOSE = "",
                         MR_NAME = "",
                         COL_TIME = "",
                         PYMNT_TYPE = "",
                         TARIFFCODE = "",
                         TARIFF_RATE = "",
                         TARIFF_INDEX = "",
                         INCIDENT_TYPE = "",
                         CHEQUEDATE = "",
                         CHEQUENO = "",
                         BANKNAME = "",
                         IBC = "",
                         BSC = ""  
                    }}, JsonRequestBehavior.AllowGet);

            }
            return jsonResult;
        }
















        //  Amounts = objResponse1.First().AMOUNT;
        ////Arrears
        //  string Arrears = objResponse1.First().DEATILS[0].AMOUNT;
        ////ByPass
        //  string Bypass = objResponse1.First().DEATILS[1].AMOUNT;
        ////Preload
        //  string Preload = objResponse1.First().DEATILS[2].AMOUNT;
        //  string CustomerNo = objResponse1.First().CUSTOMER_NO;
        //  string MeterNo = objResponse1.First().METER_NO;
        //string ReceiptNo_ = objResponse1.First().RECEIPTNUMBER;
        //string TokenDescription = objResponse1.First().TOKENDESC; 
        //string UnitsActual = objResponse1.First().TOKENDESC;
        //string TokenDescription = objResponse1.First().TOKENDESC;
        //string TokenDescription = objResponse1.First().TOKENDESC;
        //string TokenDescription = objResponse1.First().TOKENDESC;
        //{"username":"xpresspay.test","apikey":"2E639809-58B0-49E2-88D7-38CB4DF2B5B2","PaymentLogId":"181022144830","CustReference":"861137381801",
        //"AlternateCustReference":"861137381801","Amount":"570","PaymentMethod":"Own Bank Cheque","PaymentReference":"PHEDC/CITI/XPS/181022/75810",
        //"TerminalID":null,"ChannelName":"BANKBRANCH","Location":null,"PaymentDate":"22-10-2018 14:47:42","InstitutionId":null,"InstitutionName":null, 
        //"BankName":"CITI BANK","BranchName":"CITI BANK","CustomerName":" ADEYINKA J A ","OtherCustomerInfo":null,"ReceiptNo":null,"CollectionsAccount":null,
        //"BankCode":"023","CustomerAddress":" 8 GEORGE NKORO 8 GEORGE NKORO ","CustomerPhoneNumber":"08184300070","DepositorName":"Dipo",
        //"DepositSlipNumber":"454393939834","PaymentCurrency":"NGN","ItemName":null,"ItemCode":"01","ItemAmount":"570","PaymentStatus":"Success",
        //"IsReversal":null,"SettlementDate":null,"Teller":"CITITELLER3"} 
        //  }

        //send a requery to DL ENhance for the Vending PIN



        [HttpGet]
        public async Task<JsonResult> m(string AccountNo)
        {

            var data = db.CustomerPaymentInfos.Where(p => p.CustomerReference == AccountNo).ToList();
            var jsonResult = Json(data, JsonRequestBehavior.AllowGet);
            if (data.Count > 0)
            {
                return jsonResult;
            }
            return jsonResult;
        }



        private static string GetSparkBalance(String MeterNo)
        {
            string AccountBal = "";


            const string WEBSERVICE_URL = "https://phed-agip.sparkmeter.cloud/api/v0/customers?meter_serial=";

            var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL + MeterNo);

            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authentication-Token", ".eJwFwcsRgDAIBcBeOMsM4aOhFscDLwn9l-DuS0A1lj08bSu7JjhDjIc37iEenkEXzWjT42c-45TpTuxYkdKFgiDp-wHhFBPO.D2f9JQ.Saox9uWCr0qHfmojbeZf1_rnBZE");

                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse = sr.ReadToEnd();

                        //  Console.WriteLine(String.Format("Response: {0}", jsonResponse));

                        var myJsonString = jsonResponse;

                        var jo = JObject.Parse(myJsonString);

                        AccountBal = jo["customers"][0]["credit_balance"].ToString();
                    }
                }
            }


            return AccountBal;
        }


        [HttpGet]
        public async Task<JsonResult> v(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            {

                SparkBal = GetSparkBalance(AccountNo);
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

            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

            string CallbackURL = "http://phedpayments.nepamsonline.com/PhedPay/Success";


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
            string ProductDescription = "EazyPay Electricity Bill";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                + "&trans-id=" + trans_id);

            //Save the Customers Details to the Database

            string HashCode = CreateHash(hashString.ToString(), Key);


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
                string PaymentStatus = "PENDING";
                string IsReversal = "false";

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
                Info.PaymentStatus = PaymentStatus;
                Info.BankName = BankName;
                Info.BranchName = BranchName;
                Info.Location = Location;
                Info.CustomerAddress = objResponse1[0].ADDRESS;
                Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                Info.AlternateCustReference = objResponse1[0].METER_NO;
                Info.Token = "NOTAVAILABLE";
                Info.PaymentMethod = PaymentMethod;
                Info.AccountType = AccountType.ToUpper();
                Info.TransactionProcessDate = DateTime.Now.ToShortDateString();

                db.CustomerPaymentInfos.Add(Info);
                db.SaveChanges();

                var jsonResult = Json(new { CustomerName = CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult;

                //var Data = new { data = stuff, data2 = otherstuff };
            }
        }


        [HttpGet]
        public async Task<JsonResult> s(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            {

                SparkBal = GetSparkBalance(AccountNo);
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

            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

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
            string ProductDescription = "EazyPay Electricity Bill";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                + "&trans-id=" + trans_id);

            //Save the Customers Details to the Database

            string HashCode = CreateHash(hashString.ToString(), Key);


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
                string PaymentStatus = "PENDING";
                string IsReversal = "false";

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
                Info.PaymentStatus = PaymentStatus;
                Info.BankName = BankName;
                Info.BranchName = BranchName;
                Info.Location = Location;
                Info.CustomerAddress = objResponse1[0].ADDRESS;
                Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                Info.AlternateCustReference = objResponse1[0].METER_NO;
                Info.Token = "NOTAVAILABLE";
                Info.PaymentMethod = PaymentMethod;
                Info.AccountType = AccountType.ToUpper();
                db.CustomerPaymentInfos.Add(Info);
                db.SaveChanges();

                var jsonResult = Json(new { CustomerName = CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult;

                //var Data = new { data = stuff, data2 = otherstuff };
            }
        }


        [HttpGet]
        public async Task<JsonResult> x(string AccountNo, string PhoneNumber, string EmailAddress, string AccountType, string AmountPaid)
        {
            //var _jsonResults = Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            //return _jsonResults;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //AccountNo = "861137381801";
            // AmountPaid = "450066";
            // PhoneNumber = "08067807821";
            //EmailAddress = "ebukaegonu@yahoo.com";

            string SparkBal = "";

            if (AccountNo.Substring(0, 2) == "SM" && AccountType.ToUpper() == "PREPAID")
            {

                SparkBal = GetSparkBalance(AccountNo);
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

            //perform the hash here before going to the Payment Page

            string MerchantID = "00024";

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
            string ProductDescription = "EazyPay Electricity Bill";
            string ProductId = "78";

            StringBuilder hashString = new StringBuilder();

            hashString.Append("amount=" + AmountPaid + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                + "&trans-id=" + trans_id);

            //Save the Customers Details to the Database

            string HashCode = CreateHash(hashString.ToString(), Key);


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
                string PaymentStatus = "PENDING";
                string IsReversal = "false";

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
                Info.PaymentStatus = PaymentStatus;
                Info.BankName = BankName;
                Info.BranchName = BranchName;
                Info.Location = Location;
                Info.CustomerAddress = objResponse1[0].ADDRESS;
                Info.CustomerReference = objResponse1[0].CUSTOMER_NO;
                Info.AlternateCustReference = objResponse1[0].METER_NO;
                Info.Token = "NOTAVAILABLE";
                Info.PaymentMethod = PaymentMethod;
                Info.AccountType = AccountType.ToUpper();
                db.CustomerPaymentInfos.Add(Info);
                db.SaveChanges();

                var jsonResult = Json(new { CustomerName = CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
                return jsonResult;

                //var Data = new { data = stuff, data2 = otherstuff };
            }
        }


        [HttpGet]
        public async Task<JsonResult> VerifyPayment(string transaction_id)
        {
            var jsonResult = Json("", JsonRequestBehavior.AllowGet);

            //we need to determine if this is Transaction Id or Meter No





            // CustomerPaymentInfo CustomerDetails = db.CustomerPaymentInfos.Find(transaction_id);

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

                        //CustomerDetails.is_default = false;


                        //CustomerPaymentInfo CustomerDetails =  db.CustomerPaymentInfos.FirstOrDefault(p=>p.TransactionID == transaction_id);
                        //string Amounts = "";

                        //customer has not been given the Token Yet.

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


                //           self.PaymentList1([]);
                // GetPaymentHistory
             
                ////self.PaymentList(data.PaymentList)
                //ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList1);
                //var s = $("#TableGrid").DataTable();
                        AppViewModels viewModel = new AppViewModels();

            DateTime TodaysDate = DateTime.Now;
          //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
            viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == CustomerDetails3.AlternateCustReference).ToList();
            var LIST = JsonConvert.SerializeObject(viewModel);
                        if (CustomerDetails3.Token != "NOTAVAILABLE" && CustomerDetails3.PaymentStatus == "SUCCESS")
                        {
                        jsonResult = Json(new[] { new 
                        {
                            LIST = LIST,
                        CUSTOMER_NO = CustomerDetails3.CustomerReference,
                        METER_NO = CustomerDetails3.AlternateCustReference,
                        RECEIPTNUMBER = CustomerDetails3.ReceiptNo,
                        PAYMENTDATETIME = CustomerDetails3.DatePaid,
                        AMOUNT= CustomerDetails3.Amount,
                        TOKENDESC =  CustomerDetails3.Token, 
                        UNITSACTUAL = CustomerDetails3.Units,
                        TARIFF =  CustomerDetails3.Tarriff ,
                        STATUS =  "SUCCESS",
                        Message =  "PAYMENT SUCCESSFUL"
                        }}, JsonRequestBehavior.AllowGet);
                        }

                        //JsonResult h = await SendEmail(transaction_id);

                        return jsonResult;
                    }
                    else
                    {

                        //payment was not successsful

                        db = new ApplicationDbContext();
                        CustomerPaymentInfo CustomerDetails3 = (from p in db.CustomerPaymentInfos
                                                                where p.TransactionID == transaction_id
                                                                select p).SingleOrDefault();

                        CustomerDetails3.PaymentStatus = "FAILED";
                        CustomerDetails3.Token = "FAILED";
                        db.Entry(CustomerDetails3).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();













                        if (CustomerDetails3 == null)
                        {

                            AppViewModels viewModel = new AppViewModels();

                            DateTime TodaysDate = DateTime.Now;
                            //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
                            viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == CustomerDetails3.AlternateCustReference).ToList();
                            var LIST = JsonConvert.SerializeObject(viewModel);

                            jsonResult = Json(new[] { new 
                        {
                        LIST = LIST,
                        STATUS =  "FAILED", 
                        Message =  "PAYMENT UNSUCCESSFUL,IF DEBITED, PLEASE APPROACH BANK FOR REFUND "}}, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            AppViewModels viewModel = new AppViewModels();

                            DateTime TodaysDate = DateTime.Now;
                            //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
                            viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == CustomerDetails3.AlternateCustReference).ToList();
                            var LIST = JsonConvert.SerializeObject(viewModel);

                            jsonResult = Json(new[] { new 
                        {
                        LIST = LIST,
                        STATUS =  "FAILED", 
                        Message =  "PAYMENT UNSUCCESSFUL,IF DEBITED, PLEASE APPROACH BANK FOR REFUND "}}, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
            }

            return jsonResult;
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
 





	}
}