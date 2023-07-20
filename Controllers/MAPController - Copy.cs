using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHEDServe.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace PHEDServe.Controllers
{

//    PROCEEDTOMAPPAY
//PROCEEDTOMAPPAY
//APPROVED FOR INSTALLATION
//PROCEEDTOMAPPAY
//APPROVED FOR INSTALLATION
//GOBRC
//MAPPAID
//PAYARREARS
//METER INSTALLED
//METER INSTALLED
//GOBRC
//HASARREARS
//GOBRC
//VERIFY
//GOBRC
//APPROVED FOR INSTALLATION
//APPROVED FOR INSTALLATION
//VERIFY
//METER INSTALLED
//PROCEEDTOMAPPAY
//GOBRC
//APPROVED FOR INSTALLATION
//GOBRC
//GOBRC
    public class MAPController : Controller
    {
        // GET: /MAP/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MapPayment()
        {
            return View();
        }
        public ActionResult ApprovedBills()
        {
            return View();
        }
        public ActionResult IBCApproval()
        {
            return View();
        } 
        public ActionResult PaymentsReport()
        {
            return View();
        }
        public ActionResult InstalledMeters()
        {
            return View();
        }
        public ActionResult UploadVendors()
        {
            return View();
        }
        public ActionResult ListOfPaidCustomers()
        {
            return View();
        }
        public ActionResult UploadContractors()
        {
            return View();
        }
        public ActionResult UploadWhiteListMetersApproval()
        {
            return View();
        }
        public ActionResult CaptureMeters()
        {
            return View();
        }
        public ActionResult ApproveToPayUpfront()
        {
            return View();
        }
        public ActionResult CustomerPaymentInfo()
        {
            return View();
        }
        public ActionResult ApproveInstallation()
        {
            return View();
        }
        public ActionResult ApprovePayment()
        {
            return View();
        }

        public ActionResult Capture_Meters()
        {
            return View();
        }
        public ActionResult ApproveCS()
        {
            return View();
        }
        public ActionResult CSMBRCApproval()
        {
            return View();
        }
        public ActionResult BRCCustomerService()
        {
            return View();
        }
        public ActionResult ApprovePaymentsMade()
        {
            return View();
        }
        public ActionResult CSMApproval()
        {
            return View();
        }

        public ActionResult ApproveCSM()
        {
            return View();
        }


        public ActionResult AuditTrailReports()
        {
            return View();
        }

        public ActionResult ApproveIBC()
        {
            return View();
        }
        public ActionResult ApproveBSC()
        {
            return View();
        }

        public ActionResult UploadWhiteListMeters()
        {
            return View();
        }
        public ActionResult UploadBulkApplicants()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadReference()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED" && p.PaymentFor == "METER").ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = new List<Models.CustomerPaymentInfo>();
            myViewModel.MAPPaymentList = data;
            
           
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public class RCDCStatData
        {
            public string PercentageSuccess { get; set; }
            public string DisconnectedPREPAID { get; set; }
            public string DisconnectedPOSTPAID { get; set; }
            public string PendingForCustomer { get; set; }
            public string ReconnectedCustomers { get; set; }
            public string DisconnectedCustomers { get; set; }
        }

        [HttpGet]
        public JsonResult ReportReference()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

          //  var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED" && p.PaymentFor == "METER").ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.Token == "MAP").ToList();//new List<Models.CustomerPaymentInfo>();
            myViewModel.MAPPaymentList =  new List<MAPPayment>();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

         

        [HttpGet]
        public JsonResult LoadAuditTrailReference()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            //var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED" && p.PaymentFor == "METER").ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = new List<Models.CustomerPaymentInfo>();
            myViewModel.AuditTrailList = db.AuditTrails.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult LoadReferenceWhitelist()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            // var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED" && p.PaymentFor == "METER").ToList();
            myViewModel.UplodedStatusList = new List<UploadedFilesVM>();
            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList(); 
            myViewModel.MeterUploadApprovalList = new List<MeterList>();
            myViewModel.PaymentList = new List<Models.CustomerPaymentInfo>();
            myViewModel.MAPPaymentList = new List<MAPPayment>();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CreateCustomerPaymentInfo()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            myViewModel.CustomerDetails = new CustomerPaymentInfo();
            myViewModel.ComplaintList = db.Complaints.ToList();
            myViewModel.MAPPayment = new MAPPayment();
            //  myViewModel.AuditTrailList = db.AuditTrails.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomer(string in_data)
        {
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            var data = db.CustomerPaymentInfos.Where(
                p => p.MeterPhase == details.MeterPhase &&
                     p.PaymentStatus == details.PaymentStatus &&
                     p.BSC == details.BSC &&
                     p.IBC == details.IBC

                ).Take(100);
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }


        //public  JsonResult UploadBulkMeter(FormCollection collections, HttpPostedFileBase acquisitionFile)


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
                                d.ApprovalStatus = "PENDING";

                                db.MeterLists.Add(d);
                                db.SaveChanges();

                                ErrorLog.MeterNo = MeterNo;
                                ErrorLog.MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                ErrorLog.Status = "FILE UPLOADED BUT NOT APPROVED";
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

        public async Task<JsonResult> UploadApplication(FormCollection collections, HttpPostedFileBase acquisitionFile)
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



                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), DocumentName);


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
                            //DirectPayments d = new DirectPayments();
                            CustomerPaymentInfo Info = new CustomerPaymentInfo();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ErrorLog = new UploadedFilesVM();
                                //string POSTINGDATE = ds.Tables[0].Rows[i]["POSTINGDATE"].ToString();
                                //  string MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                //string Status = ds.Tables[0].Rows[i]["Status"].ToString();
                                string MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();
                                string CustomerName = ds.Tables[0].Rows[i]["MAPCustomerName"].ToString();
                                string MAPVendor = ds.Tables[0].Rows[i]["MAPVendor"].ToString();


                                if (CustomerName.Trim() == "" || CustomerName.Trim() == null)
                                {

                                    continue;
                                }

                                if (MAPVendor.Trim() == "" || MAPVendor.Trim() == null)
                                {

                                    continue;
                                }


                                Info.MAPVendor = ds.Tables[0].Rows[i]["MAPVendor"].ToString();

                                string TicketID = RandomPassword.Generate(10);

                                Info = new CustomerPaymentInfo();
                                Info.Amount = ds.Tables[0].Rows[i]["AmountPaid"].ToString();
                                Info.TransactionID = TicketID;
                                Info.PaymentMethod = "WEB";

                                Info.CustomerName = CustomerName;
                                Info.CustomerEmail = ds.Tables[0].Rows[i]["CustomerEmail"].ToString();
                                Info.CustomerPhoneNumber = ds.Tables[0].Rows[i]["CustomerPhoneNumber"].ToString();
                                Info.DepositorName = ds.Tables[0].Rows[i]["MAPCustomerName"].ToString();

                                Info.ChannelName = "WEB";
                                Info.IBC = ds.Tables[0].Rows[i]["IBC"].ToString(); ;
                                Info.BSC = ds.Tables[0].Rows[i]["BSC"].ToString(); ;
                                Info.PaymentStatus = Status;
                                Info.CustomerAddress = ds.Tables[0].Rows[i]["CustomerAddress"].ToString();
                                Info.CustomerReference = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                                Info.AlternateCustReference = ds.Tables[0].Rows[i]["MeterNo"].ToString();
                                string BRC_ID = "MAP" + RandomPassword.Generate(10).ToString();
                                Info.BRC_ID = BRC_ID;
                                Info.Token = "MAP";
                                Info.PaymentMethod = "WEB";
                                Info.MAPApplicationStatus = "VERIFY";
                                Info.MAPPaymentStatus = Status;
                                Info.PaymentStatus = Status;
                                Info.MAPCustomerName = ds.Tables[0].Rows[i]["MAPCustomerName"].ToString();
                                string MAPplicant = ds.Tables[0].Rows[i]["MAPCustomerName"].ToString();
                                Info.AccountType = ds.Tables[0].Rows[i]["AccountType"].ToString();
                                Info.TransactionProcessDate = DateTime.Now.ToShortDateString();

                                Info.MeterPhase = ds.Tables[0].Rows[i]["MeterPhase"].ToString();


                                db.CustomerPaymentInfos.Add(Info);
                                db.SaveChanges();

                                //MAPPAyment

                                MAPPayment pr = new MAPPayment();
                                pr.PaymentFor = "BRC";
                                pr.TicketId = TicketID;
                                pr.TransRef = TicketID;
                                pr.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                                pr.Amount = ds.Tables[0].Rows[i]["AmountPaid"].ToString();
                                pr.IBC = ds.Tables[0].Rows[i]["IBC"].ToString(); ;
                                pr.BSC = ds.Tables[0].Rows[i]["BSC"].ToString(); ;
                                pr.PaymentMode = "WEB";
                                pr.PaymentStatus = Status;
                                pr.PaymentMode = ds.Tables[0].Rows[i]["ModeOfPayment"].ToString(); ;

                                pr.Phase = ds.Tables[0].Rows[i]["MeterPhase"].ToString(); ;
                                db.MAPPayments.Add(pr);
                                db.SaveChanges();

                                ////////////////////////////////////////////////////////////////


                                CUSTOMER cust = new CUSTOMER();

                                cust.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                                cust.CustomerAddress = ds.Tables[0].Rows[i]["CustomerAddress"].ToString();
                                cust.Occupant = ds.Tables[0].Rows[i]["Occupant"].ToString();


                                cust.Email = ds.Tables[0].Rows[i]["CustomerEmail"].ToString();
                                cust.Phone = ds.Tables[0].Rows[i]["CustomerPhoneNumber"].ToString();
                                cust.CustomerSurname = ds.Tables[0].Rows[i]["MAPCustomerName"].ToString();

                                cust.HouseNo = ds.Tables[0].Rows[i]["CustomerAddress"].ToString();

                                cust.IDcard = ds.Tables[0].Rows[i]["IDcard"].ToString();
                                cust.IDcardNo = ds.Tables[0].Rows[i]["IDcardNo"].ToString();

                                // cust.IdentityCardNumber = ds.Tables[0].Rows[i]["IDcardNo"].ToString();
                                string ModeOofPayment = ds.Tables[0].Rows[i]["ModeOfPayment"].ToString();



                                if (ModeOofPayment == "MSC")
                                {
                                    cust.ModeOfPayment = ds.Tables[0].Rows[i]["ModeOfPayment"].ToString();

                                    cust.PaymentPlan = ds.Tables[0].Rows[i]["PaymentPlan"].ToString();
                                }
                                else
                                {
                                    cust.ModeOfPayment = ds.Tables[0].Rows[i]["ModeOfPayment"].ToString();


                                }



                                db.CUSTOMERS.Add(cust);
                                db.SaveChanges();






                                ///////////////////////////////////////////

                                ErrorLog.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                                ErrorLog.Phase = ds.Tables[0].Rows[i]["MeterPhase"].ToString();
                                ErrorLog.CustomerAddress = ds.Tables[0].Rows[i]["CustomerAddress"].ToString();
                                ErrorLog.MeterVendor = ds.Tables[0].Rows[i]["MAPVendor"].ToString();
                                ErrorLog.Description = MeterNo;
                                ErrorLog.TicketId = TicketID;
                                ErrorLog.CustomerName = CustomerName;
                                ErrorLog.Status = "FILE UPLOADED";
                                ErrorLog.DateUploaded = MeterVendor;
                                ErrorLog.Amount = ds.Tables[0].Rows[i]["AmountPaid"].ToString();
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
        public async Task<JsonResult> UploadMeter(FormCollection collections, HttpPostedFileBase acquisitionFile)
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



                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), DocumentName);


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
                            //DirectPayments d = new DirectPayments();
                            MeterList d = new MeterList();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ErrorLog = new UploadedFilesVM();
                                //string POSTINGDATE = ds.Tables[0].Rows[i]["POSTINGDATE"].ToString();
                                //string MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                //string Status = ds.Tables[0].Rows[i]["Status"].ToString();
                                string MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();


                                var check = db.MeterLists.Where(p => p.MeterNo == MeterNo).ToList();

                                if (check.Count > 0)
                                {

                                    //meter Exists

                                    ErrorLog.MeterNo = MeterNo;
                                    ErrorLog.MeterVendor = ds.Tables[0].Rows[i]["MeterVendor"].ToString();
                                    ErrorLog.Status = "DUPLICATE!! THIS METER ALREADY  EXISTS";
                                    ErrorLog.DateUploaded = MeterVendor;
                                    ErrorLogList.Add(ErrorLog);
                                }

                                else
                                {
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
        public JsonResult ViewUploadBulkMeters(string Vendor, string Status)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            AppViewModels myViewModel = new AppViewModels();

            var data = db.MeterLists.Where(p => p.MAPVendor == Vendor.Trim() && p.ApprovalStatus == Status.Trim()).ToList();

            myViewModel.MeterUploadApprovalList = data;
            //viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOT CLAIMED").ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }


        [HttpPost]
        public JsonResult ApproveMeter(string MeterNo, string StaffID, string Vendor, string Status)
        {

            ApplicationDbContext db = new ApplicationDbContext();


            AppViewModels myViewModel = new AppViewModels();
            var Details = db.MeterLists.FirstOrDefault(p => p.MeterNo == MeterNo);

            if (Details != null)
            {

                var py = db.Users.FirstOrDefault(p => p.Id == StaffID);


                if (py != null)
                {


                    Details.ApprovalStatus = "APPROVED";
                    Details.DateApproved = DateTime.Now;
                    Details.ApprovedBy = py.StaffName.ToString();
                }

            }

            db.Entry(Details).State = EntityState.Modified;
            db.SaveChanges();

            //Audit 


            string StatusId = Guid.NewGuid().ToString();
            GlobalMethodsLib lib = new GlobalMethodsLib();
            string Name = " A MAP Meter was Approved at " + DateTime.Now;
            lib.AuditTrail(StaffID, Name.ToUpper(), DateTime.Now, StatusId, "", "APPROVAL");

             

            var data = db.MeterLists.Where(p => p.MAPVendor == Vendor.Trim() && p.ApprovalStatus == Status.Trim()).ToList();

            myViewModel.MeterUploadApprovalList = data;
            //viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOT CLAIMED").ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;


        }



        [HttpPost]
        public JsonResult ApprovePayment(string TicketId, string in_data)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.CustomerPaymentInfos.Where(
            p => p.MeterPhase == "asass"
            ).ToList();
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels myViewModel = new AppViewModels();
            var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            if (Details != null)
            {
                Details.MAPPaymentStatus = "APPROVED FOR PAYMENT";
                Details.MAPApplicationStatus = "APPROVED FOR PAYMENT";
            }

            db.Entry(Details).State = EntityState.Modified;
            db.SaveChanges();

            if (details.MeterPhase == "ALL")
            {

                data = db.CustomerPaymentInfos.Where(
                p =>
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }
            else
            {
                data = db.CustomerPaymentInfos.Where(
                p => p.MeterPhase == details.MeterPhase &&
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

         

        [HttpPost]
        public JsonResult ViewInstalledMeters(string Vendor, string FromDate, string ToDate)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            DateTime _f = Convert.ToDateTime(FromDate); DateTime _t = Convert.ToDateTime(ToDate);

            var data = db.CustomerPaymentInfos.Where(
            p => p.MAPApplicationStatus == "METER INSTALLED" && (p.DateCaptured >= _f && p.DateCaptured <= _t) && p.MAPVendor == Vendor
            ).ToList();

            AppViewModels myViewModel = new AppViewModels();

            myViewModel.PaymentList = data;
            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

         
        [HttpPost]
        public JsonResult ViewCustomersAudittrailReport(string Activity, string FromDate, string ToDate)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            DateTime _f = Convert.ToDateTime(FromDate); DateTime _t = Convert.ToDateTime(ToDate);

            var data = db.AuditTrails.Where(
            p => (p.DateTime >= _f && p.DateTime <= _t) && p.ActivityType.Trim() == Activity.Trim()
            ).ToList();

            AppViewModels myViewModel = new AppViewModels();

            myViewModel.AuditTrailList = data;

            myViewModel.BSCList = db.BSCs.ToList();

            myViewModel.IBCList = db.IBCs.ToList();

            var _result = JsonConvert.SerializeObject(myViewModel);

            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);

            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }





        /// <summary>
        /// cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
        /// </summary>
        /// <param name="TicketId"></param>
        /// <param name="in_data"></param>
        /// <returns></returns>
        /// 


        //StaffID

        [HttpPost]
        public JsonResult ApproveBRCBillsForPaymentDB(string TicketId, string in_data, string StaffID, string StaffName)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.CustomerPaymentInfos.Where(
            p => p.MeterPhase == "asass"
            ).ToList();
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels myViewModel = new AppViewModels();


            //Settle into DLENhace First

            var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            if (Details != null)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhanceuat.phed.com.ng/dlenhanceapi//MAP/UpdateBRCAmount");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string _DatePaid = DateTime.Now.ToString("dd-MM-yyyy");
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"Username\":\"phed\"," +
                                  "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                                  "\"DateApproved\":\"" + _DatePaid + "\"," +
                                   "\"AccountNo\":\"" + Details.CustomerReference + "\"," +
                                  "\"Amount\":\"" + details.BRCApprovalIBCAmount + "\"," +
                                  "\"ApprovedBy\":\"" + StaffName + "\"}";
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

                    //if (result.Contains("Success"))
                    //{

                        Details.MAPApplicationStatus = "ABOUTTOAPPLY";
                        Details.BRCApprovalIBCAmount = details.BRCApprovalIBCAmount;
                        Details.BRCApprovalIBCHead = details.BRCApprovalIBCHead;  
                        //Details.BRCApprovalIBCDate = Convert.ToDateTime(details.BRCApprovalIBCDate);

                        Details.BRCApprovalIBCHeadComment = details.BRCApprovalIBCHeadComment;
                        db.Entry(Details).State = EntityState.Modified;
                        db.SaveChanges();

                        //send Mail
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                        mail.Subject = "Meter Asset Provider Information From PHED";
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        mail.Bcc.Add("payments@phed.com.ng");
                        mail.To.Add(Details.CustomerEmail);
                        string RecipientType = "";
                        string SMTPMailServer = "mail.phed.com.ng";

                        SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                        MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                        //  string htmlMsgBody = this.EmailTextBox.Text;
                        string htmlMsgBody = "<html><head></head>";
                        htmlMsgBody = htmlMsgBody + "<body>";
                        //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                        htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + Details.MAPCustomerName + "," + "</P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Your BRC Amount has been approved. You may proceed to pay off the Arrears" + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + Details.MAPCustomerName + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Approval Date: " + DateTime.Now + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Amount Approved: " + details.BRCApprovalIBCAmount + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + Details.TransactionID + " </P>";

                        htmlMsgBody = htmlMsgBody + "<br><br>";
                        htmlMsgBody = htmlMsgBody + "Thank you,";
                        htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                        htmlMsgBody = htmlMsgBody + "<br><br>";
                        mail.Body = htmlMsgBody;
                        MailSMTPserver.Send(mail);

                        string StatusId = Guid.NewGuid().ToString();
                        GlobalMethodsLib lib = new GlobalMethodsLib();
                        string Name = Details.MAPCustomerName + " BRC Amount was Just Approved at " + DateTime.Now;
                        lib.AuditTrail(StaffID, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");
                    //}


                     data = db.CustomerPaymentInfos.Where(p => p.BRCApprovalCSM == details.BRCApprovalCSM
                        && p.IBC == details.IBC && p.BSC == details.BSC && p.Token == "MAP").ToList();
                     
                    if (details.BSC == "ALL")
                    {
                        data = db.CustomerPaymentInfos.Where(p => p.BRCApprovalCSM == details.BRCApprovalCSM 
                            && p.IBC == details.IBC && p.Token == "MAP").ToList();
                    } 
                }
            }
             
          //  data = db.CustomerPaymentInfos.Where(p => p.Token == "MAP" && p.BRCApprovalCSM == "").ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = data.ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(_result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }




        [HttpPost]
        public JsonResult ApproveUpfrontPayment(string TicketId, string in_data)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.CustomerPaymentInfos.Where(
            p => p.MeterPhase == "asass"
            ).ToList();
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels myViewModel = new AppViewModels();
            var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            if (Details != null)
            {

                Details.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                db.Entry(Details).State = EntityState.Modified;
                db.SaveChanges();



                //send Mail

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                mail.Subject = "Meter Asset Provider Information From PHED";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Bcc.Add("payments@phed.com.ng");
                mail.To.Add(Details.CustomerEmail);
                string RecipientType = "";

                string SMTPMailServer = "mail.phed.com.ng";

                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                //  string htmlMsgBody = this.EmailTextBox.Text;
                string htmlMsgBody = "<html><head></head>";
                htmlMsgBody = htmlMsgBody + "<body>";
                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + Details.MAPCustomerName + "," + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your Interest in Procuring a Meter. Your REquest has been approved for payment" + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + Details.MAPCustomerName + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Visit Date: " + DateTime.Now.AddHours(8) + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + Details.TransactionID + " </P>";

                htmlMsgBody = htmlMsgBody + "<br><br>";
                htmlMsgBody = htmlMsgBody + "Thank you,";
                htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                mail.Body = htmlMsgBody;

                //-Merchant’s Name
                //-Merchant’s Url
                //-Description of the Service/Goods


                MailSMTPserver.Send(mail);
            }

            if (details.MeterPhase == "ALL")
            {

                data = db.CustomerPaymentInfos.Where(
                p =>
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }
            else
            {
                data = db.CustomerPaymentInfos.Where(
                p => p.MeterPhase == details.MeterPhase &&
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }


            data = db.CustomerPaymentInfos.Where(
             p => p.Token == "MAP" && p.MAPApplicationStatus != "PROCEEDTOMAPPAY"
             ).ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }



        [HttpPost]
        public JsonResult ApproveInstalledMeter(string TicketId, string in_data)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.CustomerPaymentInfos.Where(
            p => p.MeterPhase == "asass"
            ).ToList();
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels myViewModel = new AppViewModels();
            var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            if (Details != null)
            {

                Details.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                db.Entry(Details).State = EntityState.Modified;
                db.SaveChanges();



                //send Mail

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                mail.Subject = "Meter Asset Provider Information From PHED";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Bcc.Add("payments@phed.com.ng");
                mail.To.Add(Details.CustomerEmail);
                string RecipientType = "";

                string SMTPMailServer = "mail.phed.com.ng";

                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                //  string htmlMsgBody = this.EmailTextBox.Text;
                string htmlMsgBody = "<html><head></head>";
                htmlMsgBody = htmlMsgBody + "<body>";
                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + Details.MAPCustomerName + "," + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for your Interest in Procuring a Meter. Your REquest has been approved for payment" + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + Details.MAPCustomerName + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Visit Date: " + DateTime.Now.AddHours(8) + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: NOT PAID" + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + Details.TransactionID + " </P>";

                htmlMsgBody = htmlMsgBody + "<br><br>";
                htmlMsgBody = htmlMsgBody + "Thank you,";
                htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                mail.Body = htmlMsgBody;

                //-Merchant’s Name
                //-Merchant’s Url
                //-Description of the Service/Goods


                MailSMTPserver.Send(mail);
            }

            if (details.MeterPhase == "ALL")
            {

                data = db.CustomerPaymentInfos.Where(
                p =>
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }
            else
            {
                data = db.CustomerPaymentInfos.Where(
                p => p.MeterPhase == details.MeterPhase &&
                p.PaymentStatus == details.PaymentStatus &&
                p.BSC == details.BSC &&
                p.IBC == details.IBC && p.Token == "MAP"
                ).ToList();
            }


            data = db.CustomerPaymentInfos.Where(
             p => p.Token == "MAP" && p.MAPApplicationStatus != "PROCEEDTOMAPPAY"
             ).ToList();

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }



        [HttpPost]
        public JsonResult ApproveMAPPayment_DLEnhance(string StaffID, string TicketId, string DatePaid, string Phase, string ReceiptNo, string AccountNo, string AmountPaid)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhanceuat.phed.com.ng/dlenhanceapi/MAP/ApproveCustomerPayments");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            string _DatePaid = Convert.ToDateTime(DatePaid).ToString("dd-MM-yyyy");

            if (Phase == "THREE PHASE")
            {
                Phase = "3";

            }
            if (Phase == "SINGLE PHASE")
            {
                Phase = "1";

            }
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                              "\"DatePaid\":\"" + _DatePaid + "\"," +
                              "\"Phase\":\"" + Phase + "\"," +
                               "\"AccountNo\":\"" + AccountNo + "\"," +
                              "\"AmountPaid\":\"" + AmountPaid + "\"," +
                                "\"ReceiptNo\":\"" + ReceiptNo + "\"," +
                              "\"TicketNo\":\"" + TicketId + "\"}";
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


                    //update all the Opations in the Cutomer Info Table

                    var pays = db.MAPPayments.FirstOrDefault(p => p.TicketId == TicketId);
                    if (pays != null)
                    {

                        pays.ApprovalStatus = "APPROVED";
                        pays.ApprovedDate = DateTime.Now;
                        pays.ApprovedBy = StaffID;

                        db.Entry(pays).State = EntityState.Modified;
                        db.SaveChanges();

                        // db.Entry(pays).E
                    }


                    var Customer = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);
                    if (Customer != null)
                    {

                        Customer.MAPPaymentStatus = "PAID";
                        Customer.MAPApplicationStatus = "MAPPAID";
                        db.Entry(Customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    return _jsonResult;
                }

                // var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);


                CustomerPaymentInfo Info = new CustomerPaymentInfo();
                Info.Amount = AmountPaid;
                Info.ItemAmount = AmountPaid;

            }

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(_result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }



        [HttpPost]
        public JsonResult ApproveMAPPayment(string StaffID, string TicketId, string DatePaid, string Phase, string ReceiptNo, string AccountNo, string AmountPaid)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhanceuat.phed.com.ng/dlenhanceapi/MAP/ApproveCustomerPayments");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            string _DatePaid = Convert.ToDateTime(DatePaid).ToString("dd-MM-yyyy");

            if (Phase.Trim() == "THREE PHASE")
            {
                Phase = "3";

            }
            if (Phase.Trim() == "SINGLE PHASE")
            {
                Phase = "1";

            }
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                              "\"DatePaid\":\"" + _DatePaid + "\"," +
                              "\"Phase\":\"" + Phase + "\"," +
                               "\"AccountNo\":\"" + AccountNo + "\"," +
                              "\"AmountPaid\":\"" + AmountPaid + "\"," +
                                "\"ReceiptNo\":\"" + ReceiptNo + "\"," +
                              "\"TicketNo\":\"" + TicketId + "\"}";
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


                    #region ApprovePayment
                    var ipay = db.MAPPayments.FirstOrDefault(p => p.TicketId == TicketId);

                    db = new ApplicationDbContext();
                    string Key = ipay.PaymentId;
                    MAPPayment _ipay = db.MAPPayments.Find(Key);

                    if (_ipay != null)
                    {

                        _ipay.ApprovalStatus = "APPROVED";
                        _ipay.ApprovedDate = DateTime.Now;
                        _ipay.ApprovedBy = StaffID;
                        db.Entry(_ipay).State = EntityState.Modified;
                        db.SaveChanges();

                    }




                    var pay = db.MAPPayments.Where(p => p.TicketId == TicketId).ToList();
                    db = new ApplicationDbContext();
                    //string Key = pay.PaymentId;
                    //MAPPayment _pay = db.MAPPayments.Find(Key);
                    foreach (var p in pay)
                    {
                        //get each Item 

                        MAPPayment pp = db.MAPPayments.Find(p.PaymentId);

                        if (pp != null)
                        {

                            pp.ApprovalStatus = "APPROVED";
                            pp.ApprovedDate = DateTime.Now;
                            //pp.ApprovedBy = StaffID;
                            db.Entry(pp).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                    }



                    var Customer = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);
                    if (Customer != null)
                    {

                        Customer.MAPPaymentStatus = "PAID";
                        Customer.MAPApplicationStatus = "MAPPAID";
                        db.Entry(Customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    //var result = JsonConvert.SerializeObject(myViewModel);
                    //var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    //return _jsonResult;

                    CustomerPaymentInfo Info = new CustomerPaymentInfo();
                    Info.Amount = AmountPaid;
                    Info.ItemAmount = AmountPaid;



                    var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED").ToList();//.Where(p => p.BSC == BSC && p.IBC == IBC && p.ApprovalStatus == "NOTAPPROVED").ToList(); 
                    myViewModel.MAPPaymentList = data;

                    #endregion



                }

            }

            string StatusId = Guid.NewGuid().ToString();
            GlobalMethodsLib lib = new GlobalMethodsLib();
            string Name = "A New Payment with TicketId: " + TicketId + " was approved Inserted";
            lib.AuditTrail(StaffID, Name.ToUpper(), DateTime.Now, StatusId, TicketId, "UPDATE");



            //update all the Opations in the Cutomer Info Table

            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(_result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }




        [HttpPost]
        public JsonResult ViewCustomersUnapprovedData(string BSC, string IBC, string STATUS)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            //var data = db.CustomerPaymentInfos.Where(p => p.Token == "NOTAVAILABLE" && p.AccountType == "POSTPAID").Take(10);// _FD >= p.DatePaid && _TD <= p.DatePaid).ToList();
            var data = db.MAPPayments.Where(p => p.PaymentStatus == "PAID" && p.ApprovalStatus == "NOTAPPROVED").ToList();//.Where(p => p.BSC == BSC && p.IBC == IBC && p.ApprovalStatus == "NOTAPPROVED").ToList(); 
            myViewModel.MAPPaymentList = data;
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetCustomerDetails(string TransactionId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            //var data = db.CustomerPaymentInfos.Where(p => p.Token == "NOTAVAILABLE" && p.AccountType == "POSTPAID").Take(10);// _FD >= p.DatePaid && _TD <= p.DatePaid).ToList();
            var data = db.CUSTOMERS.FirstOrDefault(p => p.TransactionID == TransactionId);

            myViewModel.CustomerDetailsFromCustomer = data;
            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult FetchCustomerDataForApproval(string in_data, string Status)
        {
            var details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext db = new ApplicationDbContext();

            AppViewModels myViewModel = new AppViewModels();

            var data = db.CustomerPaymentInfos.Where(p => p.BRCApprovalCSM == details.BRCApprovalCSM
                && p.IBC == details.IBC && p.BSC == details.BSC && p.Token == "MAP").ToList();


            if (details.BSC == "ALL")
            {
                data = db.CustomerPaymentInfos.Where(p => p.BRCApprovalCSM == details.BRCApprovalCSM  && p.IBC == details.IBC && p.Token == "MAP").ToList(); 
            }

            



            //if (details == "ALL")
            //{

            //    if (details.PaymentStatus == "APPROVED")
            //    {
            //    }
            //    else
            //    {
            //    }

            //    data = db.CustomerPaymentInfos.Where(
            //    p =>
            //    p.PaymentStatus == details.PaymentStatus &&
            //    p.BSC == details.BSC &&
            //    p.IBC == details.IBC &&
            //    p.Token == "MAP" && p.MAPApplicationStatus == Status
            //    ).ToList();
            //}
            //else
            //{
            //    data = db.CustomerPaymentInfos.Where(
            //    p => p.PaymentStatus == details.PaymentStatus &&
            //    p.BSC == details.BSC &&
            //    p.IBC == details.IBC && p.Token == "MAP" && p.MAPApplicationStatus == Status
            //    ).ToList();
            //}


          //  data = db.CustomerPaymentInfos.Where(p => p.Token == "MAP").ToList();

            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);
            myViewModel.BSCList = db.BSCs.ToList();
            myViewModel.IBCList = db.IBCs.ToList();
            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }



        /// <summary>
        /// Ebuka View Customer JSON Method
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ViewCustomerBRC(string TransactionId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            //var data = db.CustomerPaymentInfos.Where(p => p.Token == "NOTAVAILABLE" && p.AccountType == "POSTPAID").Take(10);// _FD >= p.DatePaid && _TD <= p.DatePaid).ToList();
            var data = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TransactionId & p.MAPApplicationStatus == "GOBRC");

            myViewModel.CustomerDetails = data;
            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ResetForm(string TransactionId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            // var data = db.CustomerPaymentInfos.FirstOrDefault(p => (p.TransactionID == TransactionId || p.CustomerReference == TransactionId) && p.MAPApplicationStatus == "APPROVED FOR INSTALLATION");

            myViewModel.CustomerDetails = new Models.CustomerPaymentInfo();
            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ViewCustomerInstallMeter(string TransactionId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            var data = db.CustomerPaymentInfos.FirstOrDefault(p => (p.TransactionID == TransactionId || p.CustomerReference == TransactionId) && p.MAPApplicationStatus == "APPROVED FOR INSTALLATION");

            myViewModel.CustomerDetails = data;
            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ViewCustomer(string TransactionId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            //var data = db.CustomerPaymentInfos.Where(p => p.Token == "NOTAVAILABLE" && p.AccountType == "POSTPAID").Take(10);// _FD >= p.DatePaid && _TD <= p.DatePaid).ToList();
            var data = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TransactionId);

            myViewModel.CustomerDetails = data;
            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomerByDate(string fromDate, string toDate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            DateTime _FD = Convert.ToDateTime(fromDate);
            DateTime _TD = Convert.ToDateTime(toDate);


            var data = db.CustomerPaymentInfos.Where(p => p.DatePaid >= _FD && _TD <= p.DatePaid).Take(30);// _FD >= p.DatePaid && _TD <= p.DatePaid).ToList();
            myViewModel.PaymentList = data.ToList();
            var result = JsonConvert.SerializeObject(myViewModel);

            var jsonresult = Json(result, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }




        [HttpPost]
        public JsonResult InsertMapPayment(string PaymentData)
        {
            var PaymentDetails = JsonConvert.DeserializeObject<MAPPayment>(PaymentData);

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            if (PaymentDetails != null)
            {

                db.MAPPayments.Add(PaymentDetails);
                db.SaveChanges();
            }


            //Send a Mail to Miracle


            #region Send Email

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
            mail.Subject = "Meter Asset Provider Information From PHED";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.Bcc.Add("payments@phed.com.ng");
            mail.To.Add("Miracle.Esemuze@phed.com.ng");
            string RecipientType = "";
            string SMTPMailServer = "mail.phed.com.ng";

            SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
            MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
            //  string htmlMsgBody = this.EmailTextBox.Text;
            string htmlMsgBody = "<html><head></head>";
            htmlMsgBody = htmlMsgBody + "<body>";
            //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
            htmlMsgBody = htmlMsgBody + "<P>" + "Dear Approver" + "</P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "You have a pending approval for MAP payment Kindly Login to Approve" + "</P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + PaymentDetails.CustomerName + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Payment Date: " + PaymentDetails.DatePaid + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Amount Paid: " + PaymentDetails.Amount + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: PAID" + " </P>";
            htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + PaymentDetails.TicketId + " </P>";

            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + "Thank you,";
            htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
            htmlMsgBody = htmlMsgBody + "<br><br>";
            mail.Body = htmlMsgBody;

            MailSMTPserver.Send(mail);

            #endregion

            var result = JsonConvert.SerializeObject(myViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult MeterCaptureDetails(string PaymentData, string StaffId)
        {
            var PaymentDetails = JsonConvert.DeserializeObject<CustomerPaymentInfo>(PaymentData);

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            string CapturedByName = getStaffDetailsFromStaffId(StaffId);

            if (PaymentDetails != null)
            {

                string TicketId = PaymentDetails.TransactionID;

                CustomerPaymentInfo details = db.CustomerPaymentInfos.Find(TicketId);

                if (details != null)
                {
                    //Write to DLEnhance 
                    //Check if the Meter is in the list of installed Meters 

                    if (CheckMeterOnWhiteListForCompatibility(PaymentDetails.InstalledMeterNo, PaymentDetails.MAPVendor))
                    {
                        #region Write to DLEnhance

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhanceuat.phed.com.ng/dlenhanceapi/MAP/UpdateMeterInformation");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = "{\"Username\":\"phed\"," +
                                          "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                                          "\"MeterNo\":\"" + PaymentDetails.InstalledMeterNo + "\"," +
                                            "\"InstallerName\":\"" + PaymentDetails.MAPVendor + "\"," +
                                              "\"PoleNo\":\"" + PaymentDetails.PoleNo + "\"," +
                                                "\"DateInstalled\":\"" + DateTime.Now.ToString("dd-MM-yyyy") + "\"," +
                                                  "\"SealNo1\":\"" + PaymentDetails.MeterSeal1 + "\"," +
                                                    "\"SealNo2\":\"" + PaymentDetails.MeterSeal2 + "\"," +
                                                      "\"AccountNo\":\"" + PaymentDetails.CustomerReference + "\"," +
                                          "\"UpdatedBy\":\"" + CapturedByName + "\"}";
                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var results = streamReader.ReadToEnd();
                            results = results.Replace("\r", string.Empty);
                            results = results.Replace("\n", string.Empty);
                            results = results.Replace(@"\", string.Empty);
                            results = results.Replace(@"\\", string.Empty);

                            //check if the Customer Exists here

                            //Parse the JSON here to retrieve the Account Number Sent from DLEnhacne



                            if (results.Contains(PaymentDetails.InstalledMeterNo))
                            {

                                string StaffName = db.Users.FirstOrDefault(p => p.Id == StaffId).StaffName;

                                details.PoleNo = PaymentDetails.PoleNo;
                                details.MeterSeal1 = PaymentDetails.MeterSeal1;
                                details.MeterSeal2 = PaymentDetails.MeterSeal2;
                                details.DateCaptured = DateTime.Now;
                                details.MAPVendor = PaymentDetails.MAPVendor;
                                details.InstalledMeterNo = PaymentDetails.InstalledMeterNo;
                                details.MeterInstalaltionComment = PaymentDetails.MeterInstalaltionComment;
                                details.MAPApplicationStatus = "METER INSTALLED";
                                details.InstalledBy = PaymentDetails.MAPVendor;
                                details.CapturedBy = StaffId;
                                details.CapturedByName = StaffName;
                                db.Entry(details).State = EntityState.Modified;
                                db.SaveChanges();

                                string StatusId = Guid.NewGuid().ToString();
                                GlobalMethodsLib lib = new GlobalMethodsLib();
                                string Name = StaffName + " Captured a " + details.MeterPhase.Trim() + " Meter on " + DateTime.Now;
                                lib.AuditTrail(StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "METER CAPTURE");



                                #region Send Email

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                                mail.Subject = "You Meter has been Captured";
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.High;
                                mail.Bcc.Add("payments@phed.com.ng");
                                mail.To.Add(details.CustomerEmail);
                                string RecipientType = "";
                                string SMTPMailServer = "mail.phed.com.ng";

                                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                                //  string htmlMsgBody = this.EmailTextBox.Text;
                                string htmlMsgBody = "<html><head></head>";
                                htmlMsgBody = htmlMsgBody + "<body>";
                                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                                htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + details.MAPCustomerName + ", " + "</P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Your New Meter has been Installed and Captured, Please find below the meter Details" + "</P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + PaymentDetails.CustomerName + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Capture Date: " + DateTime.Now + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Meter Vendor : " + PaymentDetails.MAPVendor + " </P>";
                                htmlMsgBody = htmlMsgBody + " <P> " + "Captured By: " + StaffName + " </P>"; 
                                htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + PaymentDetails.CustomerReference + " </P>";

                                htmlMsgBody = htmlMsgBody + " <P> " + "Meter No: " + PaymentDetails.InstalledMeterNo + " </P>";


                                htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + details.TransactionID + " </P>";

                                htmlMsgBody = htmlMsgBody + "<br><br>";
                                htmlMsgBody = htmlMsgBody + "Thank you,";
                                htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                                htmlMsgBody = htmlMsgBody + "<br><br>";
                                mail.Body = htmlMsgBody;

                                MailSMTPserver.Send(mail);

                                #endregion


                                myViewModel.CustomerDetails = new Models.CustomerPaymentInfo();
                                var result = JsonConvert.SerializeObject(myViewModel);
                                return Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);

                            }

                            else
                            {

                                myViewModel.CustomerDetails = new Models.CustomerPaymentInfo();
                                var result = JsonConvert.SerializeObject(myViewModel);
                                return Json(new { result = result, error = "Sorry this Meter was not Captured, an error Occured. Kindly try again. Thank you." }, JsonRequestBehavior.AllowGet);

                            }
                        }

                        #endregion
                    }
                    else
                    {
                        //return Error that this Meter is not in the List of  meters to Be Captured 
                        myViewModel.CustomerDetails = new Models.CustomerPaymentInfo();
                        var result = JsonConvert.SerializeObject(myViewModel);
                        return Json(new { result = result, error = "Could not capture Meter. it is either this meter does not belong to the Vendor {" + PaymentDetails.MAPVendor + "}, which you selected, or it has been captured before. Please Check and try again." }, JsonRequestBehavior.AllowGet);
                    }
                }












                //Audit Trail here 

                //Send a Mail to Miracle


                //#region Send Email

                //MailMessage mail = new MailMessage();
                //mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                //mail.Subject = "Meter Asset Provider Information From PHED";
                //mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;
                //mail.Bcc.Add("payments@phed.com.ng");
                //mail.To.Add("Miracle.Esemuze@phed.com.ng");
                //string RecipientType = "";
                //string SMTPMailServer = "mail.phed.com.ng";

                //SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                //MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                ////  string htmlMsgBody = this.EmailTextBox.Text;
                //string htmlMsgBody = "<html><head></head>";
                //htmlMsgBody = htmlMsgBody + "<body>";
                ////  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                //htmlMsgBody = htmlMsgBody + "<P>" + "Dear Finance" + "</P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "A New Meter has been Captured in the Field, Please find below the meter Details"+ "</P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + PaymentDetails.CustomerName + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Payment Date: " + PaymentDetails.DatePaid + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Amount Paid: " + PaymentDetails.Amount + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: PAID" + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + PaymentDetails.TicketId + " </P>";

                //htmlMsgBody = htmlMsgBody + "<br><br>";
                //htmlMsgBody = htmlMsgBody + "Thank you,";
                //htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                //htmlMsgBody = htmlMsgBody + "<br><br>";
                //mail.Body = htmlMsgBody;

                //MailSMTPserver.Send(mail);

                //#endregion
            }

            myViewModel.CustomerDetails = new Models.CustomerPaymentInfo();

            var resultd = JsonConvert.SerializeObject(myViewModel);

            return Json(new { result = resultd, error = "" }, JsonRequestBehavior.AllowGet);
        }







        public class Rootobject
        {
            public Table[] Table { get; set; }
        }

        public class Table
        {
            public string SUCCESSFUL { get; set; }
            public string METERNO { get; set; }
            public string CUSTOMERNAME { get; set; }
            public string PHASE { get; set; }
            public float ARREARS { get; set; }
            public float FINALBILL { get; set; }
        }














        private string getStaffDetailsFromStaffId(string StaffId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var py = db.Users.FirstOrDefault(p => p.Id == StaffId);

            if (py != null)
            {
                return py.StaffName.ToString();
            }

            return "N/A";
        }




        public bool CheckMeterOnWhiteListForCompatibility(string MeterNo, string MAPVendor)
        {
            //Check the Meter 
            ApplicationDbContext db = new ApplicationDbContext();

            var Check = db.MeterLists.Where(p => p.MeterNo == MeterNo && p.MAPVendor == MAPVendor && p.ApprovalStatus == "APPROVED").ToList();

            if (Check.Count > 0)
            {
                return true;


            }
            else
            {

                return false;
            }

            return false;
        }






        [HttpPost]
        public JsonResult InsertBRC(string CustomerData, string StaffID)
        {
            var Details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(CustomerData);

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            if (Details != null)
            {
                var customerPaymentInfo = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == Details.TransactionID);
                //customerPaymentInfo

                if (customerPaymentInfo != null)
                {
                    customerPaymentInfo.BRCApprovalCS = Details.BRCApprovalCS;
                    customerPaymentInfo.Complaints = Details.Complaints;
                    customerPaymentInfo.BRCApprovalCSAmount = Details.BRCApprovalCSAmount;
                    customerPaymentInfo.BRCStatus = "PENDINGCSMAPPROVAL";
                    customerPaymentInfo.BRCDate = DateTime.Now;
                    // customerPaymentInfo.BRCApprovalCSComment = Details.ResponseMessage;
                    customerPaymentInfo.BRCApprovalCSComment = Details.BRCApprovalCSComment;
                    db.Entry(customerPaymentInfo).State = EntityState.Modified;
                }

                db.SaveChanges();

                //
                string StatusId = Guid.NewGuid().ToString();
                GlobalMethodsLib lib = new GlobalMethodsLib();
                string Name = "A New BRC with TicketId: " + Details.TransactionID + " was registered by the Customer Service";
                lib.AuditTrail(StaffID, Name.ToUpper(), DateTime.Now, StatusId, Details.TransactionID, "BRC");

            }
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }









        [HttpPost]
        public JsonResult InsertBRCCSM(string CustomerData, string StaffID)
        {
            var Details = JsonConvert.DeserializeObject<CustomerPaymentInfo>(CustomerData);

            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            if (Details != null)
            {
                var customerPaymentInfo = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == Details.TransactionID);
                //customerPaymentInfo

                if (customerPaymentInfo != null)
                {
                    customerPaymentInfo.BRCApprovalCSM = Details.BRCApprovalCSM;
                    customerPaymentInfo.Complaints = Details.Complaints;
                    customerPaymentInfo.BRCApprovalCSMAmount = Details.BRCApprovalCSMAmount;
                    customerPaymentInfo.BRCStatus = "PENDINGIBCAPPROVAL";
                    customerPaymentInfo.BRCDate = DateTime.Now;
                    // customerPaymentInfo.BRCApprovalCSComment = Details.ResponseMessage;
                    customerPaymentInfo.BRCApprovalCSMComment = Details.BRCApprovalCSMComment;
                    db.Entry(customerPaymentInfo).State = EntityState.Modified;
                }

                db.SaveChanges();

                //audit 
                string StatusId = Guid.NewGuid().ToString();
                GlobalMethodsLib lib = new GlobalMethodsLib();
                string Name = "A New BRC with TicketId: " + Details.TransactionID + " was approved by the CSM";
                lib.AuditTrail(StaffID, Name.ToUpper(), DateTime.Now, StatusId, Details.TransactionID, "BRC");
            }
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}