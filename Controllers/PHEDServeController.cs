using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace PHEDServe.Controllers
{
    public class PHEDServeController : Controller
    {
        //
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: /PHEDServe/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StaffBillVerification()
        {
            return View();
        }
        public ActionResult GenerateCurrentBillStatus()
        {
            return View();
        }
        public ActionResult ManageOnboardedStaff()
        {
            return View();
        }
        public ActionResult StaffOnboard()
        {
            return View();
        }
        public ActionResult PasswordChangeSuccessful()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]

        //public ActionResult VerifyAccount(string AccountNo, string AccountType,string staffId)
        //{
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "POST";

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        string json = "{\"Username\":\"phed\"," +
        //                      "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
        //                      "\"CustomerNumber\":\"" + AccountNo + "\"," +
        //                      "\"Mobile_Number\":\"" + "08067807821" + "\"," +
        //                      "\"Mailid\":\"" + "MDPortal@phed.com.ng" + "\"," +
        //                      "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
        //        streamWriter.Write(json);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }
             
        //    string trans_id = RandomPassword.Generate(10).ToString();

        //    StringBuilder hashString = new StringBuilder();

        //    //Save the Customers Details to the Database

        //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        var result = streamReader.ReadToEnd();
        //        result = result.Replace("\r", string.Empty);
        //        result = result.Replace("\n", string.Empty);
        //        result = result.Replace(@"\", string.Empty);
        //        result = result.Replace(@"\\", string.Empty);

        //        //check if the Customer Exists here

        //        if (result == "Customer Not Found")
        //        {
        //            var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
        //            return _jsonResult;
        //        }

        //        var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);


        //        CustomerPaymentInfo Info = new CustomerPaymentInfo();

        //        Info.CustomerName = objResponse1[0].CONS_NAME;
        //        string CustomerName = objResponse1[0].CONS_NAME;
        //        string Address = objResponse1[0].ADDRESS;
        //        string IBC = objResponse1[0].IBC_NAME;
        //        string BSC = objResponse1[0].BSC_NAME;
        //        string TariffCode = objResponse1[0].TARIFFCODE;
        //        string AccountNumber = objResponse1[0].CUSTOMER_NO;
        //        string Arrears = objResponse1[0].ARREAR;

        //        string MeterNumber = objResponse1[0].METER_NO;
        //        ApplicationUser User = new ApplicationUser();


        //        //Check if the Staff Data Exists


        //        var Staff = db.StaffBasicDatas.Where(p => p.Staff_Id == staffId).ToList();

        //        ApplicationUser ExisitingData = db.Users.FirstOrDefault(p => p.StaffId == staffId);


        //        RegViewModel regViewmodel = new RegViewModel();
        //        ApplicationUser pp = new ApplicationUser();
        //        if (Staff.Count > 0)
        //        {
        //            pp.Email = Staff.FirstOrDefault().Email;
        //            pp.PhoneNo = Staff.FirstOrDefault().Phone;

        //        }
        //        pp.Address = Address;
        //        pp.IBC = IBC;
        //        pp.BSC = BSC;
        //        pp.TariffCode = TariffCode;
        //        pp.FirstName = CustomerName;
        //        pp.AccountNumber = AccountNumber;
        //        pp.PHEDKeyAccountsPhone = "";
        //        pp.PHEDKeyAccountsEmail = "";
        //        pp.ContactPersonEmail = "";
        //        pp.ContactPersonPhone = "";
        //        pp.BillReflection = "";
                
        //        if (MeterNumber == null || MeterNumber == "")
        //        {
        //            pp.MeterNo = "NOMETER";
        //            pp.MeterType = "N/A";
        //        }
        //        else
        //        {
        //            pp.MeterNo = MeterNumber;
        //            pp.MeterType = "";
        //        }


        //        if (ExisitingData != null)
        //        {
        //            pp.CUGLine = ExisitingData.CUGLine;
        //            pp.Arrears = Arrears;
        //            pp.OfficeLocation = ExisitingData.OfficeLocation;
        //            pp.PeriodToClearDebt = ExisitingData.PeriodToClearDebt;
        //            pp.JobRole = ExisitingData.JobRole;
        //            pp.ResolvedBalance = ExisitingData.ResolvedBalance;
        //            pp.Installment = ExisitingData.Installment;
        //            pp.DepartmentId = ExisitingData.DepartmentId;
        //            pp.DepartmentName = ExisitingData.DepartmentName;
        //            pp.Submission = ExisitingData.Submission;
        //            pp.Remarks = ExisitingData.Remarks;
        //            pp.Email = ExisitingData.Email;
        //            pp.PhoneNo = ExisitingData.PhoneNo;
        //            pp.Designation = ExisitingData.Designation;
        //            pp.AccountName = ExisitingData.AccountName;
        //        }


        //        pp.UserCategory = "PHEDSTAFF";
        //        regViewmodel.ApplicationUser = pp;
        //        var results = JsonConvert.SerializeObject(regViewmodel);

        //        return Json(new { result = results }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GenerateBillPaymentList(string SubmissionStatus, string ReportStatus, string ReportDate, string FromNo, string ToNo)
        {
            RegViewModel regViewmodel = new RegViewModel();
            ApplicationUser pp = new ApplicationUser();
            StaffBillPaymentData spd = new StaffBillPaymentData();
            List<StaffBillPaymentData> spdlist = new List<StaffBillPaymentData>();
            DateTime dateTime = DateTime.Parse(ReportDate);
            string _month = dateTime.ToString("dd-MM-yyyy");
            dateTime = DateTime.Parse(ReportDate);
            dateTime = DateTime.Parse(dateTime.ToString("MM-dd-yyyy"));
            string str = dateTime.ToString("MMMM");
            string str1 = Convert.ToDateTime(_month).Year.ToString();// DateTime.Parse(_month).Year.ToString();
            List<ApplicationUser> StaffList = db.Users.Where(p => p.UserCategory == "PHEDSTAFF2").ToList();

            int FromSerial = Convert.ToInt32(FromNo);
            int ToSerial = Convert.ToInt32(ToNo);

            int diff = ((ToSerial + 1) - FromSerial);



            //db.Users.Where(p => p.UserCategory == "PHEDSTAFF" && p.Submission == "SUBMITTED" && (p.SerialNo >= FromSerial && ToSerial <= p.SerialNo)).ToList());





            //(
            //from p in this.db.get_Users()
            //where (p.UserCategory == "PHEDSTAFF") && (p.Submission == "NOTCOMPLETED")
            //select p).ToList<ApplicationUser>() : (
            //from p in this.db.get_Users()
            //where (p.UserCategory == "PHEDSTAFF") && (p.Submission == "SUBMITTED")
            //select p).ToList<ApplicationUser>());



            if (ReportStatus == "Generate Payment List")
            {
                if ((SubmissionStatus == "COMPLETED"))
                {

                    StaffList = db.Users.Where(p => p.UserCategory == "PHEDSTAFF" && p.Submission == "SUBMITTED" && FromSerial <= p.SerialNo && p.SerialNo <= ToSerial).ToList();

                }
                else
                {

                    StaffList = db.Users.Where(p => p.UserCategory == "PHEDSTAFF" && p.Submission == "NOTCOMPLETED" && FromSerial <= p.SerialNo && p.SerialNo <= ToSerial).ToList();

                }

                if (StaffList.Count > 0)
                {

                    foreach (ApplicationUser applicationUser in StaffList)
                    {
                        this.db = new ApplicationDbContext();
                        spd = new StaffBillPaymentData();
                        string AccountNo = applicationUser.AccountNumber;
                        var cheeck = db.StaffBillPaymentDatas.FirstOrDefault(p => p.Staff_Id == applicationUser.StaffId && (p.Year == str1) && (p.Month == str));

                        if (cheeck != null)
                        {
                            spd.Comments = "ALREADY GENERATED";
                            if (!string.IsNullOrEmpty(applicationUser.Email))
                            {
                                spd.Email = applicationUser.Email;
                            }
                            else
                            {
                                spd.Email = "N/A"; ;
                            }
                            spd.OtherNames = applicationUser.FirstName;
                            spd.Surname = applicationUser.LastName;
                            spd.Staff_Id = applicationUser.StaffId;
                            spd.Phone = applicationUser.PhoneNo;
                            spd.AmountPaid = new decimal?(new decimal(0));
                            spd.Year = str1;
                            spd.AccountType = applicationUser.AccountType;
                            spd.AccountNo = applicationUser.AccountNumber;
                            spd.AccountType = applicationUser.AccountType;
                            spd.Month = str;
                            spd.DateGenerated = new DateTime?(DateTime.Now);
                            spdlist.Add(spd);

                            continue;
                        }
                        else if (!string.IsNullOrEmpty(AccountNo))
                        {
                            StaffPaymentDetails details = this.getStaffPaymentDetails(AccountNo, _month);
                            if (details.Status == "SUCCESSFUL")
                            {
                                if (details.AccountType.ToUpper() == "POSTPAID")
                                {
                                    spd.Comments = string.Concat("Bill PAID FOR ", str, ", ", str1);
                                }
                                if (details.AccountType.ToUpper() == "PREPAID")
                                {
                                    spd.Comments = "PPM";
                                }
                                if (!string.IsNullOrEmpty(applicationUser.Email))
                                {
                                    spd.Email = applicationUser.Email;
                                }
                                else
                                {
                                    spd.Email = applicationUser.Email;
                                }
                                spd.OtherNames = applicationUser.FirstName;
                                spd.Surname = applicationUser.LastName;
                                spd.Staff_Id = applicationUser.StaffId;
                                spd.Phone = applicationUser.PhoneNo;
                                spd.AmountPaid = details.AmountPaid;
                                spd.DatePaid = details.DatePaid;
                                spd.Year = str1;
                                spd.AccountType = applicationUser.AccountType;
                                spd.Arrears = new decimal?(Convert.ToDecimal(details.Arrears));
                                spd.Month = str;
                                spd.AccountNo = details.AccountNo;
                                spd.DateGenerated = new DateTime?(DateTime.Now);
                                spdlist.Add(spd);
                                this.db.StaffBillPaymentDatas.Add(spd);
                                this.db.SaveChangesAsync();
                            }
                            else if (details.Status == "ERROR")
                            {
                                spd.Comments = "No Bill Payment Information for the Month selected";
                                if (!string.IsNullOrEmpty(applicationUser.Email))
                                {
                                    spd.Email = applicationUser.Email;
                                }
                                else
                                {
                                    spd.Email = applicationUser.Email;
                                }
                                spd.OtherNames = applicationUser.FirstName;
                                spd.Surname = applicationUser.LastName;
                                spd.Staff_Id = applicationUser.StaffId;
                                spd.Phone = applicationUser.PhoneNo;
                                spd.AmountPaid = details.AmountPaid;
                                spd.DatePaid = details.DatePaid;
                                spd.Year = str1;
                                spd.Arrears = new decimal?(Convert.ToDecimal(details.Arrears));
                                spd.Month = str;
                                spd.AccountNo = details.AccountNo;
                                spd.DateGenerated = new DateTime?(DateTime.Now);
                                spdlist.Add(spd);
                                this.db.StaffBillPaymentDatas.Add(spd);
                                this.db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            spd.Comments = "This Staff Account details does not exist so payment Information could not be retrieved";
                            if (!string.IsNullOrEmpty(applicationUser.Email))
                            {
                                spd.Email = applicationUser.Email;
                            }
                            else
                            {
                                spd.Email = applicationUser.Email;
                            }
                            spd.OtherNames = applicationUser.FirstName;
                            spd.Surname = applicationUser.LastName;
                            spd.Staff_Id = applicationUser.StaffId;
                            spd.Phone = applicationUser.PhoneNo;
                            spd.AmountPaid = new decimal?(new decimal(0));
                            spd.Year = str1;
                            spd.Month = str;
                            spd.DateGenerated = new DateTime?(DateTime.Now);
                            spdlist.Add(spd);
                            this.db.StaffBillPaymentDatas.Add(spd);
                            this.db.SaveChangesAsync();
                            continue;
                        }
                    }
                }
            }
            if (ReportStatus == "View Payment List")
            {
               
                spdlist = db.StaffBillPaymentDatas.Where(p => p.Month == str && p.Year == str1).ToList();
            }

            regViewmodel.StaffBillPaymentDataList = spdlist;
            regViewmodel.ApplicationUser = pp;
            string results = JsonConvert.SerializeObject(regViewmodel);
            ActionResult actionResult = base.Json(new { result = results }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }


        [HttpPost]
        public async Task<JsonResult> UploadStaffAndOnboard(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            StaffBasicData staffBasicDatum;
            string str;
            string str1;
            string str2;
            string str3;
            string str4;
            JsonResult jsonResult;
            object[] objArray;
            string str5;
            string str6;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Guid.NewGuid().ToString();
            StaffBasicData staffBasicDatum1 = new StaffBasicData();
            List<StaffBasicData> staffBasicDatas = new List<StaffBasicData>();
            RegViewModel regViewModel = new RegViewModel();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string item1 = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string str7 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), item1);
                item.SaveAs(str7);
                DataSet dataSet = new DataSet();
                OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", str7, ";Extended Properties=Excel 12.0;"));
                try
                {
                    oleDbConnection.Open();
                    DataTable schema = oleDbConnection.GetSchema("Tables");
                    try
                    {
                        string str8 = schema.Rows[0]["TABLE_NAME"].ToString();
                        string str9 = string.Concat("SELECT * FROM [", str8, "]");
                        (new OleDbDataAdapter(str9, oleDbConnection)).Fill(dataSet, "Items");
                        if (dataSet.Tables.Count == 0)
                        {
                        }
                        if (dataSet.Tables.Count > 0)
                        {
                            staffBasicDatum = new StaffBasicData();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                str = "";
                                str1 = "";
                                str2 = "";
                                str3 = "";
                                str5 = "";
                                str4 = "";
                                str6 = "";
                                try
                                {
                                    staffBasicDatum1 = new StaffBasicData();
                                    str5 = dataSet.Tables[0].Rows[i]["Staff_Id"].ToString();
                                    if (!string.IsNullOrEmpty(str5))
                                    {
                                        str6 = dataSet.Tables[0].Rows[i]["Email"].ToString();
                                        if (!string.IsNullOrEmpty(str6))
                                        {
                                            DbSet<StaffBasicData> dbSet = applicationDbContext.StaffBasicDatas;
                                            if (dbSet.FirstOrDefault<StaffBasicData>((StaffBasicData p) => (p.Staff_Id == str5) && (p.Email.Trim() == str6)) == null)
                                            {
                                                str2 = dataSet.Tables[0].Rows[i]["OtherNames"].ToString();
                                                if (!string.IsNullOrEmpty(str2))
                                                {
                                                    str3 = dataSet.Tables[0].Rows[i]["Phone"].ToString();
                                                    str4 = dataSet.Tables[0].Rows[i]["Surname"].ToString();
                                                    if (!string.IsNullOrEmpty(str4))
                                                    {
                                                        staffBasicDatum = new StaffBasicData()
                                                        {
                                                            BSU = str,
                                                            IBC = str1,
                                                            OtherNames = str2,
                                                            Phone = str3,
                                                            Email = str6,
                                                            Staff_Id = str5,
                                                            Surname = str4
                                                        };
                                                        applicationDbContext.StaffBasicDatas.Add(staffBasicDatum);
                                                        applicationDbContext.SaveChanges();
                                                        staffBasicDatum1.Staff_Id = str5;
                                                        staffBasicDatum1.OtherNames = str2;
                                                        staffBasicDatum1.Surname = str4;
                                                        staffBasicDatum1.Status = "Staff Onboarded Successfully";
                                                        staffBasicDatas.Add(staffBasicDatum1);
                                                    }
                                                    else
                                                    {
                                                        staffBasicDatum1.Staff_Id = str5;
                                                        staffBasicDatum1.OtherNames = str2;
                                                        staffBasicDatum1.Surname = str4;
                                                        objArray = new object[] { "Surname Column is Blank for No ", i, 1, " please Correct and try again " };
                                                        staffBasicDatum1.Status = string.Concat(objArray);
                                                        staffBasicDatas.Add(staffBasicDatum1);
                                                    }
                                                }
                                                else
                                                {
                                                    staffBasicDatum1.Staff_Id = str5;
                                                    staffBasicDatum1.OtherNames = str2;
                                                    staffBasicDatum1.Surname = str4;
                                                    objArray = new object[] { "OtherNames Column is Blank for No ", i, 1, " please Correct and try again " };
                                                    staffBasicDatum1.Status = string.Concat(objArray);
                                                    staffBasicDatas.Add(staffBasicDatum1);
                                                }
                                            }
                                            else
                                            {
                                                staffBasicDatum1.Staff_Id = str5;
                                                staffBasicDatum1.OtherNames = str2;
                                                staffBasicDatum1.Surname = str4;
                                                staffBasicDatum1.Email = str6;

                                                staffBasicDatum1.Status = string.Concat(str5, " Staff Data is already in the Database");
                                                staffBasicDatas.Add(staffBasicDatum1);
                                            }
                                        }
                                        else
                                        {
                                            staffBasicDatum1.Staff_Id = str5;
                                            staffBasicDatum1.OtherNames = str2;
                                            staffBasicDatum1.Surname = str4;
                                            staffBasicDatum1.Email = str6;
                                            objArray = new object[] { "Email Column is Blank for No ", i, 1, " please Correct and try again " };
                                            staffBasicDatum1.Status = string.Concat(objArray);
                                            staffBasicDatas.Add(staffBasicDatum1);
                                        }
                                    }
                                    else
                                    {
                                        staffBasicDatum1.Staff_Id = str5;
                                        staffBasicDatum1.OtherNames = str2;
                                        staffBasicDatum1.Surname = str4;
                                        staffBasicDatum1.Email = str6;
                                        objArray = new object[] { "StaffId Column is Blank for No ", i, 1, " please Correct and try again " };
                                        staffBasicDatum1.Status = string.Concat(objArray);
                                        staffBasicDatas.Add(staffBasicDatum1);
                                    }

                                    //save the files to the Database
                                     
                                }
                                catch (Exception exception)
                                {
                                    string str10 = JsonConvert.SerializeObject(regViewModel);
                                    JsonResult nullable = base.Json(new { result = str10, error = "Please ensure the heading in your Excel Sheet is Correct. the Headings should be Staff_Id,\tSurname\t,OtherNames\t,Employment_Type,\tRole,\tBSU,\tIBC,\tDepartment,\tEmail,\tCUG\t,Phone " }, JsonRequestBehavior.AllowGet);
                                    nullable.MaxJsonLength = new int?(2147483647);
                                    jsonResult = nullable;
                                    return jsonResult;
                                }
                          
                            }
                        }
                    }
                    
                    finally
                    {
                        if (schema != null)
                        {
                            ((IDisposable)schema).Dispose();
                        }
                    }
                }
                finally
                {
                    if (oleDbConnection != null)
                    {
                        ((IDisposable)oleDbConnection).Dispose();
                    }
                }
            }
            regViewModel.UplodedStaffList = staffBasicDatas;
            string str11 = JsonConvert.SerializeObject(regViewModel);
            JsonResult nullable1 = base.Json(new { result = str11, error = "" }, JsonRequestBehavior.AllowGet);
            nullable1.MaxJsonLength = new int?(2147483647);
            jsonResult = nullable1;
            return jsonResult;
        //Label1:
        //    staffBasicDatum = new StaffBasicData()
        //    {
        //        BSU = str,
        //        IBC = str1,
        //        OtherNames = str2,
        //        Phone = str3,
        //        Email = str6,
        //        Staff_Id = str5,
        //        Surname = str4
        //    };
        //    applicationDbContext.StaffBasicDatas.Add(staffBasicDatum);
        //    applicationDbContext.SaveChanges();
        //    staffBasicDatum1.Staff_Id = str5;
        //    staffBasicDatum1.OtherNames = str2;
        //    staffBasicDatum1.Surname = str4;
        //    staffBasicDatum1.Status = "Staff Onboarded Successfully";
        //    staffBasicDatas.Add(staffBasicDatum1);
        //    goto Label2;
        }


        private StaffPaymentDetails getStaffPaymentDetails(string AccountNo, string BillMonth)
        {
            StaffPaymentDetails p = new StaffPaymentDetails();
            DataSet _ds = new DataSet();
            DeliveredBills Bill = new DeliveredBills();
            List<DeliveredBills> Bills = new List<DeliveredBills>();
            string connectionString = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
            DataSet ds = new DataSet();
            string[] accountNo = new string[] { "select CONVERT(decimal(18,2), CAST(CurrentBalance AS FLOAT)) as Arrears,* from staffpaymentlist  where (AccountNo = '", AccountNo, "' or MeterNo = '", AccountNo, "' ) and(CONVERT(datetime, LPDate ) >= '", BillMonth, "') " };
            string CheckUsername = string.Concat(accountNo);
            (new SqlDataAdapter(CheckUsername, connectionString)).Fill(ds);
             
            if (ds.Tables[0].Rows.Count <= 0)
            {
                p.Status = "This account Number did not return any Payment Detail";
                p.DatePaid = DateTime.Now;
                p.AmountPaid = new decimal?(new decimal(0));
                p.AccountType = "N/A";
                p.Arrears = "";
                p.AccountNo = AccountNo;
                p.AccountType = ""; 
            }
            else
            {
                p.DatePaid = new DateTime?(Convert.ToDateTime(ds.Tables[0].Rows[0]["LPDate"].ToString()));
                p.AmountPaid = new decimal?(Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString()));
                p.Status = "SUCCESSFUL";
                p.AccountType = ds.Tables[0].Rows[0]["AccountType"].ToString();
                p.Arrears = ds.Tables[0].Rows[0]["Arrears"].ToString();
                p.AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            }
            return p;
        }

        public ActionResult VerifyAccount(string AccountNo, string AccountType, string staffId)
        {
            ActionResult actionResult;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                string[] accountNo = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", AccountNo, "\",\"Mobile_Number\":\"08067807821\",\"Mailid\":\"MDPortal@phed.com.ng\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
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
            RandomPassword.Generate(10).ToString();
            StringBuilder hashString = new StringBuilder();
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string result = streamReader.ReadToEnd();
                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace("\\", string.Empty);
                result = result.Replace("\\\\", string.Empty);

                if (!(result == "Customer Not Found"))
                {
                    List<DirectJSON> objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);
                    (new CustomerPaymentInfo()).CustomerName = objResponse1[0].CONS_NAME;
                    string CustomerName = objResponse1[0].CONS_NAME;
                    string Address = objResponse1[0].ADDRESS;
                    string IBC = objResponse1[0].IBC_NAME;
                    string BSC = objResponse1[0].BSC_NAME;
                    string TariffCode = objResponse1[0].TARIFFCODE;
                    string AccountNumber = objResponse1[0].CUSTOMER_NO;
                    string Arrears = objResponse1[0].ARREAR;
                    string MeterNumber = objResponse1[0].METER_NO;
                    ApplicationUser User = new ApplicationUser();
                    List<StaffBasicData> Staff = (
                        from p in this.db.StaffBasicDatas
                        where p.Staff_Id == staffId
                        select p).ToList<StaffBasicData>();
                    ApplicationUser ExisitingData = this.db.Users.FirstOrDefault<ApplicationUser>((ApplicationUser p) => p.StaffId == staffId);
                    RegViewModel regViewmodel = new RegViewModel();
                    ApplicationUser pp = new ApplicationUser();
                    if (Staff.Count > 0)
                    {
                        pp.Email = Staff.FirstOrDefault<StaffBasicData>().Email;
                        pp.PhoneNo = Staff.FirstOrDefault<StaffBasicData>().Phone;
                    }
                    pp.Address = Address;
                    pp.IBC = IBC;
                    pp.BSC = BSC;
                    pp.TariffCode = TariffCode;
                    pp.AccountName = CustomerName;
                    pp.AccountNumber = AccountNumber;
                    pp.PHEDKeyAccountsPhone = "";
                    pp.PHEDKeyAccountsEmail = "";
                    pp.ContactPersonEmail = "";
                    pp.ContactPersonPhone = "";
                    pp.BillReflection = "";
                    pp.MeterMake = "N/A";
                    if (!string.IsNullOrEmpty(MeterNumber))
                    {
                        pp.MeterNo = MeterNumber;
                        pp.MeterType = "";
                    }
                    else
                    {
                        pp.MeterNo = "NOMETER";
                        pp.MeterType = "N/A";
                    }

                    if (ExisitingData != null && ExisitingData.Submission == "SUBMITTED")
                    {
                        pp.UserCategory = "PHEDSTAFF";
                        regViewmodel.ApplicationUser = pp;
                        string _results = JsonConvert.SerializeObject(regViewmodel);
                        actionResult = base.Json(new { result = _results ,error = "DUPLICATE"}, JsonRequestBehavior.AllowGet);
                        return actionResult;
                    }

                    if (ExisitingData != null & ExisitingData.Submission != "SUBMITTED")
                    {
                        pp.Email = ExisitingData.UserName;
                        pp.StaffId = ExisitingData.StaffId;
                        pp.CUGLine = ExisitingData.CUGLine;
                        pp.Arrears = Arrears;
                        pp.OfficeLocation = ExisitingData.OfficeLocation;
                        pp.PeriodToClearDebt = ExisitingData.PeriodToClearDebt;
                        pp.JobRole = ExisitingData.JobRole;
                        pp.ResolvedBalance = ExisitingData.ResolvedBalance;
                        pp.Installment = ExisitingData.Installment;
                        pp.DepartmentId = ExisitingData.DepartmentId;
                        pp.DepartmentName = ExisitingData.DepartmentName;
                        pp.Submission = "SUBMITTED";
                        pp.Remarks = ExisitingData.Remarks;
                        pp.PhoneNo = ExisitingData.PhoneNo;
                        pp.Designation = ExisitingData.Designation;
                        pp.BillReflection = ExisitingData.BillReflection;
                        pp.AccountName = objResponse1[0].CONS_NAME;
                        pp.LastAmount = "";
                        pp.LastDatePaid = new DateTime?(DateTime.Now);
                        ZoneFeederMapping p = this.GetZoneMapping(AccountNumber, AccountType.ToUpper());
                        pp.Zone = p.Zone;
                        pp.Feeder = p.Feeder; 
                        pp.UserName = ExisitingData.UserName;
                        pp.CIN = p.CIN;
                        pp.MeterMake = "N/A";
                        if (!string.IsNullOrEmpty(p.CIN))
                        {
                            pp.CIN = p.CIN;
                        }
                        else
                        {
                            pp.CIN = "N/A";
                        }
                        if (!string.IsNullOrEmpty(p.DTR_Name))
                        {
                            pp.DTR_Name = p.DTR_Name;
                        }
                        else
                        {
                            pp.DTR_Name = "N/A";
                        }
                    }

                    pp.UserCategory = "PHEDSTAFF";
                    regViewmodel.ApplicationUser = pp;
                    string results = JsonConvert.SerializeObject(regViewmodel);
                    actionResult = base.Json(new { result = results, error = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            return actionResult;
        }

        private ZoneFeederMapping GetZoneMapping(string AccountNo, string AccountType)
        {
            ZoneFeederMapping p = new ZoneFeederMapping();
            DataSet _ds = new DataSet();
            DeliveredBills Bill = new DeliveredBills();
            List<DeliveredBills> Bills = new List<DeliveredBills>();
            string connectionString = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
            DataSet ds = new DataSet();
            string CheckUsername = "";
            CheckUsername = string.Concat("select DTR_Name, Feeder33Name, CIN, Zone from  EnumerationFeederMapping where (AccountNo = '", AccountNo, "')");
            (new SqlDataAdapter(CheckUsername, connectionString)).Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                p.Feeder = ds.Tables[0].Rows[0]["Feeder33Name"].ToString();
                p.Zone = ds.Tables[0].Rows[0]["Zone"].ToString();
                p.CIN = ds.Tables[0].Rows[0]["CIN"].ToString();
                p.DTR_Name = ds.Tables[0].Rows[0]["DTR_Name"].ToString();
            }
            return p;
        }
            
            //cccccccccccccccccccccccccccccccccccccc
       //  ListOfSubmittedBills
        public ActionResult ListOfSubmittedBills()
        {
            return View();
        }
 
        [HttpGet]
        [AllowAnonymous]
        public JsonResult loadRegisterModel()
        {

            RegViewModel regViewmodel = new RegViewModel();

            regViewmodel.ApplicationUser = new ApplicationUser();
            regViewmodel.isLoggedIn = false;
            regViewmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
            regViewmodel.TitleTbls = db.TitleTbls.ToList();
            regViewmodel.SexTbls = db.Sexs.ToList();

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            //this.AddToastMessage("Another User is already Logged in", "Please log out the other user or use another browser if you want to use another user account.", ToastType.Warning);

            if (!string.IsNullOrEmpty(username))
            {
                var user = context.Users.SingleOrDefault(u => u.UserName == username);
                regViewmodel.isLoggedIn = true;
            }

            var result = JsonConvert.SerializeObject(regViewmodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

	}
}