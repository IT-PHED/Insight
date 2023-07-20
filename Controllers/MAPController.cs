using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PHEDServe;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class MAPController : Controller
    {
        public static string apikey = ConfigurationManager.AppSettings["API"].ToString();
        public MAPController()
        {
        }
        public ActionResult ApprovedPaymentsForInstallations()
        {
            return View();
        }
        public ActionResult UploadVendors()
        {
            return View();
        }
  public ActionResult NERCInstallationReport()
        {
            return View();
        }
       public ActionResult UploadInstallers()
        {
            return View();
        }
       public ActionResult UploadContractors()
       {
           return View();
       }
       public ActionResult SealManagement()
       {
           return View();
       }
       public ActionResult UploadedMeters()
       {
           return View();
       }
         
       public ActionResult InstalledMetersSignage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ApproveBRCBillsForPaymentDB2(string TicketId, string in_data, string StaffID, string StaffName)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where p.MeterPhase == "asass"
                select p).ToList<CustomerPaymentInfo>();
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo bRCApprovalIBCAmount = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (bRCApprovalIBCAmount != null)
            {
                bRCApprovalIBCAmount.MAPApplicationStatus = "ABOUTTOAPPLY";
                bRCApprovalIBCAmount.BRCApprovalIBCAmount = customerPaymentInfo.BRCApprovalIBCAmount;
                bRCApprovalIBCAmount.BRCApprovalIBCHead = customerPaymentInfo.BRCApprovalIBCHead;
                bRCApprovalIBCAmount.BRCApprovalIBCDate = new DateTime?(DateTime.Now);
                bRCApprovalIBCAmount.BRCApprovalIBCHeadComment = customerPaymentInfo.BRCApprovalIBCHeadComment;
                applicationDbContext.Entry(bRCApprovalIBCAmount).State =  EntityState.Modified;
                
                applicationDbContext.SaveChanges();
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                    Subject = "Meter Asset Provider Information From PHED",
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                mailMessage.Bcc.Add("payments@phed.com.ng");
                mailMessage.To.Add(bRCApprovalIBCAmount.CustomerEmail);
                SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                {
                    Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                };
                string str = string.Concat("<html><head></head>", "<body>");
                str = string.Concat(str, "<P>Dear ", bRCApprovalIBCAmount.MAPCustomerName, ",</P>");
                str = string.Concat(str, " <P> Your BRC Amount has been approved. You may proceed to pay off the Arrears </P>");
                str = string.Concat(str, " <P> Customer's Name: ", bRCApprovalIBCAmount.MAPCustomerName, " </P>");
                object[] now = new object[] { str, " <P> Approval Date: ", DateTime.Now, " </P>" };
                str = string.Concat(now);
                str = string.Concat(str, " <P> Amount Approved: ", customerPaymentInfo.BRCApprovalIBCAmount, " </P>");
                str = string.Concat(str, " <P> Payment Status: NOT PAID </P>");
                str = string.Concat(str, " <P> TicketID: ", bRCApprovalIBCAmount.TransactionID, " </P>");
                str = string.Concat(str, "<br><br>");
                str = string.Concat(str, "Thank you,");
                str = string.Concat(str, " <P> PHED MAP Team </P> ");
                mailMessage.Body = string.Concat(str, "<br><br>");
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception exception)
                {
                }
                string str1 = Guid.NewGuid().ToString();
                GlobalMethodsLib globalMethodsLib = new GlobalMethodsLib();
                string str2 = string.Concat(bRCApprovalIBCAmount.MAPCustomerName, " BRC Amount was Just Approved at ", DateTime.Now);
                globalMethodsLib.AuditTrail(StaffID, str2.ToUpper(), DateTime.Now, str1, "", "APPROVAL");
            }
            list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.IBC == customerPaymentInfo.IBC) && (p.BSC == customerPaymentInfo.BSC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>();

            if (customerPaymentInfo.BSC == "ALL")
            {
                list = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                    select p).ToList<CustomerPaymentInfo>();
            }
            appViewModel.PaymentList = list;
            string str3 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str3, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }


        [HttpPost]
        public JsonResult ApproveBRCBillsForPaymentDB(string TicketId, string in_data, string StaffID, string StaffName)
        {
            Exception exception;
            JsonResult jsonResult;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where p.MeterPhase == "asass"
                select p).ToList<CustomerPaymentInfo>();
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo bRCApprovalIBCAmount = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            try
            {
                if (bRCApprovalIBCAmount != null)
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/UpdateBRCAmount");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    string str = DateTime.Now.ToString("dd-MM-yyyy");
                    StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    try
                    {
                        string[] customerReference = new string[] { "{\"Username\":\"phed\",\"APIKEY\":\"", apikey, "\",\"DateApproved\":\"", str, "\",\"AccountNo\":\"", bRCApprovalIBCAmount.CustomerReference, "\",\"Amount\":\"", customerPaymentInfo.BRCApprovalIBCAmount, "\",\"ApprovedBy\":\"", StaffName, "\"}" };
                        streamWriter.Write(string.Concat(customerReference));
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
                    StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
                    try
                    {
                        if (streamReader.ReadToEnd().Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\\", string.Empty).Replace("\\\\", string.Empty).Contains("Success"))
                        {
                            bRCApprovalIBCAmount.MAPApplicationStatus = "ABOUTTOAPPLY";
                            bRCApprovalIBCAmount.BRCApprovalIBCAmount = customerPaymentInfo.BRCApprovalIBCAmount;
                            bRCApprovalIBCAmount.BRCApprovalIBCHead = customerPaymentInfo.BRCApprovalIBCHead;
                            bRCApprovalIBCAmount.BRCApprovalIBCHeadComment = customerPaymentInfo.BRCApprovalIBCHeadComment;
                            bRCApprovalIBCAmount.BRCApprovalIBCDate = DateTime.Now;
                            bRCApprovalIBCAmount.BRCApprovedBy = StaffName;
                           
                            applicationDbContext.Entry(bRCApprovalIBCAmount).State = EntityState.Modified;
                            applicationDbContext.SaveChanges();
                            MailMessage mailMessage = new MailMessage()
                            {
                                From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                                Subject = "Meter Asset Provider Information From PHED",
                                IsBodyHtml = true,
                                Priority = MailPriority.High
                            };
                            mailMessage.Bcc.Add("payments@phed.com.ng");
                            mailMessage.To.Add(bRCApprovalIBCAmount.CustomerEmail);
                            SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                            {
                                Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                            };
                            string str1 = string.Concat("<html><head></head>", "<body>");
                            str1 = string.Concat(str1, "<P>Dear ", bRCApprovalIBCAmount.MAPCustomerName, ",</P>");
                            str1 = string.Concat(str1, " <P> Your BRC Amount has been approved. You may proceed to pay off the Arrears </P>");
                            str1 = string.Concat(str1, " <P> Customer's Name: ", bRCApprovalIBCAmount.MAPCustomerName, " </P>");
                            object[] now = new object[] { str1, " <P> Approval Date: ", DateTime.Now, " </P>" };
                            str1 = string.Concat(now);
                            str1 = string.Concat(str1, " <P> Amount Approved: ", customerPaymentInfo.BRCApprovalIBCAmount, " </P>");
                            str1 = string.Concat(str1, " <P> Payment Status: NOT PAID </P>");
                            str1 = string.Concat(str1, " <P> TicketID: ", bRCApprovalIBCAmount.TransactionID, " </P>");
                            str1 = string.Concat(str1, "<br><br>");
                            str1 = string.Concat(str1, "Thank you,");
                            str1 = string.Concat(str1, " <P> PHED MAP Team </P> ");
                            mailMessage.Body = string.Concat(str1, "<br><br>");
                            try
                            {
                                smtpClient.Send(mailMessage);
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                            }
                            string str2 = Guid.NewGuid().ToString();
                            GlobalMethodsLib globalMethodsLib = new GlobalMethodsLib();
                            string str3 = string.Concat(bRCApprovalIBCAmount.MAPCustomerName, " BRC Amount was Just Approved at ", DateTime.Now);
                            globalMethodsLib.AuditTrail(StaffID, str3.ToUpper(), DateTime.Now, str2, "", "LOGIN");
                        }
                         list = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.BRCApprovalIBCHead != "APPROVED") && (p.IBC == customerPaymentInfo.IBC) && (p.BSC == customerPaymentInfo.BSC) && (p.Token == "MAP")
                    select p).ToList<CustomerPaymentInfo>();
                        if (customerPaymentInfo.BSC == "ALL")
                        {
                            list = (
                                from p in applicationDbContext.CustomerPaymentInfos
                                where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.BRCApprovalIBCHead == null) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                                select p).ToList<CustomerPaymentInfo>();
                        } 
                    }
                    finally
                    {
                        if (streamReader != null)
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
                appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
                appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
                string str4 = JsonConvert.SerializeObject(appViewModel);
                JsonResult nullable = base.Json(new { result = str4, error = string.Concat("The BRC could not be saved successfully because ", exception.Message) }, JsonRequestBehavior.AllowGet);
                nullable.MaxJsonLength = new int?(2147483647);
                jsonResult = nullable;
                return jsonResult;
            }

            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            string str5 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable1 = base.Json(new { result = str5, error = "" }, JsonRequestBehavior.AllowGet);
            nullable1.MaxJsonLength = new int?(2147483647);
            jsonResult = nullable1;
            return jsonResult;
        }

        public ActionResult ApproveBSC()
        {
            return base.View();
        }

        public ActionResult ApproveCS()
        {
            return base.View();
        }

        public ActionResult ApproveCSM()
        {
            return base.View();
        }

        public ActionResult ApprovedBills()
        {
            return base.View();
        }

        public ActionResult ApproveIBC()
        {
            return base.View();
        }

        public ActionResult ApproveInstallation()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult ApproveInstalledMeter(string TicketId, string in_data)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where p.MeterPhase == "asass"
                select p).ToList<CustomerPaymentInfo>();
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo1 = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (customerPaymentInfo1 != null)
            {
                customerPaymentInfo1.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                applicationDbContext.Entry(customerPaymentInfo1).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                    Subject = "Meter Asset Provider Information From PHED",
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                mailMessage.Bcc.Add("payments@phed.com.ng");
                mailMessage.To.Add(customerPaymentInfo1.CustomerEmail);
                SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                {
                    Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                };
                string str = string.Concat("<html><head></head>", "<body>");
                str = string.Concat(str, "<P>Dear ", customerPaymentInfo1.MAPCustomerName, ",</P>");
                str = string.Concat(str, " <P> Thank you for your Interest in Procuring a Meter. Your REquest has been approved for payment </P>");
                str = string.Concat(str, " <P> Customer's Name: ", customerPaymentInfo1.MAPCustomerName, " </P>");
                object[] objArray = new object[] { str, " <P> Visit Date: ", null, null };
                DateTime now = DateTime.Now;
                objArray[2] = now.AddHours(8);
                objArray[3] = " </P>";
                str = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
                str = string.Concat(str, " <P> TicketID: ", customerPaymentInfo1.TransactionID, " </P>");
                str = string.Concat(str, "<br><br>");
                str = string.Concat(str, "Thank you,");
                str = string.Concat(str, " <P> PHED MAP Team </P> ");
                mailMessage.Body = string.Concat(str, "<br><br>");
                smtpClient.Send(mailMessage);
            }
            
                list = (!(customerPaymentInfo.MeterPhase == "ALL") ? (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.MeterPhase == customerPaymentInfo.MeterPhase) && (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>() : (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>());
                
                list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.Token == "MAP") && (p.MAPApplicationStatus != "PROCEEDTOMAPPAY")
                select p).ToList<CustomerPaymentInfo>();
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }



        [HttpPost]
        public JsonResult ApproveMAPPayment(string StaffID, string TicketId, string DatePaid, string Phase, string ReceiptNo, string AccountNo, string AmountPaid)
        {
            
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            
            AppViewModels appViewModel = new AppViewModels();

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/ApproveCustomerPayments");
            
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "POST";

            string str = Convert.ToDateTime(DatePaid).ToString("dd-MM-yyyy");

            if (Phase.Trim() == "THREE PHASE")
            {
                Phase = "3";
            }
            if (Phase.Trim() == "SINGLE PHASE")
            {
                Phase = "4";
            }
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                string[] phase = new string[] { "{\"Username\":\"phed\",\"APIKEY\":\"", apikey, "\",\"DatePaid\":\"", str, "\",\"Phase\":\"", Phase, "\",\"AccountNo\":\"", AccountNo, "\",\"AmountPaid\":\"", AmountPaid, "\",\"ReceiptNo\":\"", ReceiptNo, "\",\"TicketNo\":\"", TicketId, "\"}" };
                streamWriter.Write(string.Concat(phase));
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
          
            try
            {  StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
                if (streamReader.ReadToEnd().Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\\", string.Empty).Replace("\\\\", string.Empty).Contains("Successful"))
                {
                    MAPPayment mAPPayment = applicationDbContext.MAPPayments.FirstOrDefault<MAPPayment>((MAPPayment p) => p.TicketId == TicketId);
                    applicationDbContext = new ApplicationDbContext();
                    string paymentId = mAPPayment.PaymentId;
                    DbSet<MAPPayment> mAPPayments = applicationDbContext.MAPPayments;
                    object[] objArray = new object[] { paymentId };
                    MAPPayment nullable = mAPPayments.Find(objArray);
                    if (nullable != null)
                    {
                        nullable.ApprovalStatus = "APPROVED";
                        nullable.ApprovedDate = new DateTime?(DateTime.Now);
                        nullable.ApprovedBy = StaffID;
                        applicationDbContext.Entry(nullable).State = EntityState.Modified;
                        
                        applicationDbContext.SaveChanges();
                    }
                    List<MAPPayment> list = (
                        from p in applicationDbContext.MAPPayments
                        where p.TicketId == TicketId
                        select p).ToList<MAPPayment>();
                    applicationDbContext = new ApplicationDbContext();
                    foreach (MAPPayment mAPPayment1 in list)
                    {
                        DbSet<MAPPayment> dbSet = applicationDbContext.MAPPayments;
                        objArray = new object[] { mAPPayment1.PaymentId };
                        MAPPayment nullable1 = dbSet.Find(objArray);
                        if (nullable1 != null)
                        {
                            nullable1.ApprovalStatus = "APPROVED";
                            nullable1.ApprovedDate = new DateTime?(DateTime.Now);
                            applicationDbContext.Entry(nullable1).State = EntityState.Modified;
                            applicationDbContext.SaveChanges();
                        }
                    }

                    CustomerPaymentInfo customerPaymentInfo = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
                    if (customerPaymentInfo != null)
                    {
                        customerPaymentInfo.MAPPaymentStatus = "PAID";
                        customerPaymentInfo.MAPApplicationStatus = "APPROVED FOR INSTALLATION"; 
                        applicationDbContext.Entry(customerPaymentInfo).State = EntityState.Modified;
                       
                        applicationDbContext.SaveChanges();
                    }
                    CustomerPaymentInfo customerPaymentInfo1 = new CustomerPaymentInfo()
                    {
                        Amount = AmountPaid,
                        ItemAmount = AmountPaid
                    };
                    List<MAPPayment> list1 = (
                        from p in applicationDbContext.MAPPayments
                        where (p.PaymentStatus == "PAID") && (p.ApprovalStatus == "NOTAPPROVED")
                        select p).ToList<MAPPayment>();
                    appViewModel.MAPPaymentList = list1;
                } 
                
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            catch(Exception ex)
            {
    string _str1 = Guid.NewGuid().ToString();
           
            string _str2 = string.Concat("A New Payment with TicketId: ", TicketId, " was approved Inserted");
  JsonResult _jsonResult = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = new int?(2147483647);
            return _jsonResult;
            }
            string str1 = Guid.NewGuid().ToString();
            GlobalMethodsLib globalMethodsLib = new GlobalMethodsLib();
            string str2 = string.Concat("A New Payment with TicketId: ", TicketId, " was approved Inserted");
            globalMethodsLib.AuditTrail(StaffID, str2.ToUpper(), DateTime.Now, str1, TicketId, "UPDATE");
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            JsonResult jsonResult = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = new int?(2147483647);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ApproveMAPPayment_DLEnhance(string StaffID, string TicketId, string DatePaid, string Phase, string ReceiptNo, string AccountNo, string AmountPaid)
        {
            JsonResult jsonResult;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng//dlenhanceapi/MAP/ApproveCustomerPayments");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string str = Convert.ToDateTime(DatePaid).ToString("dd-MM-yyyy");
            if (Phase == "THREE PHASE")
            {
                Phase = "3";
            }
            if (Phase == "SINGLE PHASE")
            {
                Phase = "1";
            }
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                string[] phase = new string[] { "{\"Username\":\"phed\",\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\",\"DatePaid\":\"", str, "\",\"Phase\":\"", Phase, "\",\"AccountNo\":\"", AccountNo, "\",\"AmountPaid\":\"", AmountPaid, "\",\"ReceiptNo\":\"", ReceiptNo, "\",\"TicketNo\":\"", TicketId, "\"}" };
                streamWriter.Write(string.Concat(phase));
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
            StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream());
            try
            {
                string end = streamReader.ReadToEnd();
                end = end.Replace("\r", string.Empty);
                end = end.Replace("\n", string.Empty);
                end = end.Replace("\\", string.Empty);
                end = end.Replace("\\\\", string.Empty);
                if (!end.Contains("Successful"))
                {
                    CustomerPaymentInfo customerPaymentInfo = new CustomerPaymentInfo()
                    {
                        Amount = AmountPaid,
                        ItemAmount = AmountPaid
                    };
                }
                else
                {
                    MAPPayment nullable = applicationDbContext.MAPPayments.FirstOrDefault<MAPPayment>((MAPPayment p) => p.TicketId == TicketId);
                    if (nullable != null)
                    {
                        nullable.ApprovalStatus = "APPROVED";
                        nullable.ApprovedDate = new DateTime?(DateTime.Now);
                        nullable.ApprovedBy = StaffID;
                        applicationDbContext.Entry(nullable).State =  EntityState.Modified;  
                        applicationDbContext.SaveChanges();
                    }
                    CustomerPaymentInfo customerPaymentInfo1 = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
                    if (customerPaymentInfo1 != null)
                    {
                        customerPaymentInfo1.MAPPaymentStatus = "PAID";
                        customerPaymentInfo1.MAPApplicationStatus = "APPROVED FOR INSTALLATION";
                        applicationDbContext.Entry(customerPaymentInfo1).State = EntityState.Modified;
                       
                        applicationDbContext.SaveChanges();
                    }
                    jsonResult = base.Json(new { result = end }, JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            JsonResult nullable1 = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable1.MaxJsonLength = new int?(2147483647);
            jsonResult = nullable1;
            return jsonResult;
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


        public ActionResult ApprovePayment()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult ApprovePayment(string TicketId, string in_data)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where p.MeterPhase == "asass"
                select p).ToList<CustomerPaymentInfo>();
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo1 = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (customerPaymentInfo1 != null)
            {
                customerPaymentInfo1.MAPPaymentStatus = "APPROVED FOR PAYMENT";
                customerPaymentInfo1.MAPApplicationStatus = "APPROVED FOR PAYMENT";
            }

            applicationDbContext.Entry(customerPaymentInfo1).State = EntityState.Modified;
           
            applicationDbContext.SaveChanges();
            list = (!(customerPaymentInfo.MeterPhase == "ALL") ? (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.MeterPhase == customerPaymentInfo.MeterPhase) && (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>() : (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>());
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        public ActionResult ApprovePaymentsMade()
        {
            return base.View();
        }

        public ActionResult ApproveToPayUpfront()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult ApproveUpfrontPayment(string TicketId, string in_data)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where p.MeterPhase == "asass"
                select p).ToList<CustomerPaymentInfo>();
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo1 = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TicketId);
            if (customerPaymentInfo1 != null)
            {
                customerPaymentInfo1.MAPApplicationStatus = "PROCEEDTOMAPPAY";
                applicationDbContext.Entry(customerPaymentInfo1).State =  EntityState.Modified; 
                applicationDbContext.SaveChanges();
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                    Subject = "Meter Asset Provider Information From PHED",
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                mailMessage.Bcc.Add("payments@phed.com.ng");
                mailMessage.To.Add(customerPaymentInfo1.CustomerEmail);
                SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
                {
                    Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
                };
                string str = string.Concat("<html><head></head>", "<body>");
                str = string.Concat(str, "<P>Dear ", customerPaymentInfo1.MAPCustomerName, ",</P>");
                str = string.Concat(str, " <P> Thank you for your Interest in Procuring a Meter. Your REquest has been approved for payment </P>");
                str = string.Concat(str, " <P> Customer's Name: ", customerPaymentInfo1.MAPCustomerName, " </P>");
                object[] objArray = new object[] { str, " <P> Visit Date: ", null, null };
                DateTime now = DateTime.Now;
                objArray[2] = now.AddHours(8);
                objArray[3] = " </P>";
                str = string.Concat(string.Concat(objArray), " <P> Payment Status: NOT PAID </P>");
                str = string.Concat(str, " <P> TicketID: ", customerPaymentInfo1.TransactionID, " </P>");
                str = string.Concat(str, "<br><br>");
                str = string.Concat(str, "Thank you,");
                str = string.Concat(str, " <P> PHED MAP Team </P> ");
                mailMessage.Body = string.Concat(str, "<br><br>");
                smtpClient.Send(mailMessage);
            }
            list = (!(customerPaymentInfo.MeterPhase == "ALL") ? (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.MeterPhase == customerPaymentInfo.MeterPhase) && (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>() : (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>());
            list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.Token == "MAP") && (p.MAPApplicationStatus != "PROCEEDTOMAPPAY")
                select p).ToList<CustomerPaymentInfo>();
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        public ActionResult AuditTrailReports()
        {
            return base.View();
        }

        public ActionResult BRCCustomerService()
        {
            return base.View();
        }

        public ActionResult Capture_Meters()
        {
            return base.View();
        }

        public ActionResult CaptureMeters()
        {
            return base.View();
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

        [HttpGet]
        public JsonResult CreateCustomerPaymentInfo()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels()
            {
                CustomerDetails = new CustomerPaymentInfo(),
                ComplaintList = applicationDbContext.Complaints.ToList<Complaint>(),
                MAPPayment = new MAPPayment(),
                VendorList = applicationDbContext.MAP_VENDORS.ToList<MAP_VENDOR>(),
                ContractorList = applicationDbContext.MAP_CONTRACTORS.ToList<MAP_CONTRACTOR>(),
                InstallersList = applicationDbContext.MAP_INSTALLERS.ToList<MAP_INSTALLER>()
            };
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CSMApproval()
        {
            return base.View();
        }

        public ActionResult CSMBRCApproval()
        {
            return base.View();
        }

        public ActionResult CustomerPaymentInfo()
        {
            return base.View();
        }
       [HttpPost]
        public JsonResult FetchListOfPaidCustomers(string in_data, string Status)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
       
         
              List<CustomerPaymentInfo>   list = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP") && (p.BSC == customerPaymentInfo.BSC)&& (p.MAPApplicationStatus == "APPROVED FOR INSTALLATION")
                    select p).ToList<CustomerPaymentInfo>();
          
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            string str = JsonConvert.SerializeObject(appViewModel);
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            JsonResult nullable = base.Json(str, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        [HttpPost]
        public JsonResult FetchCustomerDataForApproval(string in_data, string Status)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.BRCApprovalIBCHead != "APPROVED") && (p.IBC == customerPaymentInfo.IBC) && (p.BSC == customerPaymentInfo.BSC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>();
            if (customerPaymentInfo.BSC == "ALL")
            {
                list = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.BRCApprovalIBCHead == null) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                    select p).ToList<CustomerPaymentInfo>();
            }
             
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            string str = JsonConvert.SerializeObject(appViewModel);
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            JsonResult nullable = base.Json(str, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }



        [HttpPost]
        public JsonResult FetchCustomerDataForApproval2(string in_data, string Status)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();

            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.IBC == customerPaymentInfo.IBC) && (p.BSC == customerPaymentInfo.BSC) && (p.Token == "MAP")
                select p).ToList<CustomerPaymentInfo>();
            if (customerPaymentInfo.BSC == "ALL")
            {
                list = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where (p.BRCApprovalCSM == customerPaymentInfo.BRCApprovalCSM) && (p.IBC == customerPaymentInfo.IBC) && (p.Token == "MAP")
                    select p).ToList<CustomerPaymentInfo>();
            }

            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            string str = JsonConvert.SerializeObject(appViewModel);
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            JsonResult nullable = base.Json(str, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }


         

        [HttpPost]
        public JsonResult GetCustomer(string in_data)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(in_data);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            IQueryable<CustomerPaymentInfo> customerPaymentInfos = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.MeterPhase == customerPaymentInfo.MeterPhase) && (p.PaymentStatus == customerPaymentInfo.PaymentStatus) && (p.BSC == customerPaymentInfo.BSC) && (p.IBC == customerPaymentInfo.IBC)
                select p).Take<CustomerPaymentInfo>(100);
            appViewModel.PaymentList = customerPaymentInfos.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        [HttpPost]
        public JsonResult GetCustomerByDate(string fromDate, string toDate)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            DateTime dateTime = Convert.ToDateTime(fromDate);
            DateTime dateTime1 = Convert.ToDateTime(toDate);
            IQueryable<CustomerPaymentInfo> customerPaymentInfos = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.DatePaid >= (DateTime?)dateTime) && ((DateTime?)dateTime1 <= p.DatePaid)
                select p).Take<CustomerPaymentInfo>(30);
            appViewModel.PaymentList = customerPaymentInfos.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        [HttpPost]
        public JsonResult GetCustomerDetails(string TransactionId)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            CUSTOMER cUSTOMER = applicationDbContext.CUSTOMERS.FirstOrDefault<CUSTOMER>((CUSTOMER p) => p.TransactionID == TransactionId);
            appViewModel.CustomerDetailsFromCustomer = cUSTOMER;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
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

        public class RCDCStatData
        {
            public string PercentageSuccess { get; set; }
            public string DisconnectedPREPAID { get; set; }
            public string DisconnectedPOSTPAID { get; set; }
            public string PendingForCustomer { get; set; }
            public string ReconnectedCustomers { get; set; }
            public string DisconnectedCustomers { get; set; }

            public string IllegalConnections { get; set; }

            public string NewCustomers { get; set; }

            public string Separation { get; set; }

            public string Approved { get; set; }

            public string TotalPending { get; set; }
        }

        public ActionResult IBCApproval()
        {
            return base.View();
        }

        public ActionResult Index()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult InsertBRC(string CustomerData, string StaffID)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(CustomerData);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            if (customerPaymentInfo != null)
            {
                CustomerPaymentInfo bRCApprovalCS = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == customerPaymentInfo.TransactionID);
                if (bRCApprovalCS != null)
                {
                    bRCApprovalCS.BRCApprovalCS = customerPaymentInfo.BRCApprovalCS;
                    bRCApprovalCS.Complaints = customerPaymentInfo.Complaints;
                    bRCApprovalCS.BRCApprovalCSAmount = customerPaymentInfo.BRCApprovalCSAmount;
                    bRCApprovalCS.BRCStatus = "PENDINGCSMAPPROVAL";
                    bRCApprovalCS.BRCDate = new DateTime?(DateTime.Now);
                    bRCApprovalCS.BRCApprovalCSComment = customerPaymentInfo.BRCApprovalCSComment;
                    applicationDbContext.Entry(bRCApprovalCS).State = EntityState.Modified; 
                }
                applicationDbContext.SaveChanges();
                string str = Guid.NewGuid().ToString();
                GlobalMethodsLib globalMethodsLib = new GlobalMethodsLib();
                string str1 = string.Concat("A New BRC with TicketId: ", customerPaymentInfo.TransactionID, " was registered by the Customer Service");
                globalMethodsLib.AuditTrail(StaffID, str1.ToUpper(), DateTime.Now, str, customerPaymentInfo.TransactionID, "BRC");
            }
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertBRCCSM(string CustomerData, string StaffID)
        {
            CustomerPaymentInfo customerPaymentInfo = JsonConvert.DeserializeObject<CustomerPaymentInfo>(CustomerData);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            if (customerPaymentInfo != null)
            {
                CustomerPaymentInfo bRCApprovalCSM = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == customerPaymentInfo.TransactionID);
                if (bRCApprovalCSM != null)
                {
                    bRCApprovalCSM.BRCApprovalCSM = customerPaymentInfo.BRCApprovalCSM;
                    bRCApprovalCSM.Complaints = customerPaymentInfo.Complaints;
                    bRCApprovalCSM.BRCApprovalCSMAmount = customerPaymentInfo.BRCApprovalCSMAmount;
                    bRCApprovalCSM.BRCStatus = "PENDINGIBCAPPROVAL";
                    bRCApprovalCSM.BRCDate = new DateTime?(DateTime.Now);
                    bRCApprovalCSM.BRCApprovalCSMComment = customerPaymentInfo.BRCApprovalCSMComment;
                    applicationDbContext.Entry(bRCApprovalCSM).State =  EntityState.Modified; 
                }
                applicationDbContext.SaveChanges();
                string str = Guid.NewGuid().ToString();
                GlobalMethodsLib globalMethodsLib = new GlobalMethodsLib();
                string str1 = string.Concat("A New BRC with TicketId: ", customerPaymentInfo.TransactionID, " was approved by the CSM");
                globalMethodsLib.AuditTrail(StaffID, str1.ToUpper(), DateTime.Now, str, customerPaymentInfo.TransactionID, "BRC");
            }
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertMapPayment(string PaymentData)
        {
            MAPPayment mAPPayment = JsonConvert.DeserializeObject<MAPPayment>(PaymentData);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            if (mAPPayment != null)
            {
                applicationDbContext.MAPPayments.Add(mAPPayment);
                applicationDbContext.SaveChanges();
            }
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                Subject = "Meter Asset Provider Information From PHED",
                IsBodyHtml = true,
                Priority = MailPriority.High
            };
            mailMessage.Bcc.Add("payments@phed.com.ng");
            mailMessage.To.Add("Miracle.Esemuze@phed.com.ng");
            SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
            {
                Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed")
            };
            string str = string.Concat("<html><head></head>", "<body>");
            str = string.Concat(str, "<P>Dear Approver</P>");
            str = string.Concat(str, " <P> You have a pending approval for MAP payment Kindly Login to Approve</P>");
            str = string.Concat(str, " <P> Customer's Name: ", mAPPayment.CustomerName, " </P>");
            str = string.Concat(str, " <P> Payment Date: ", mAPPayment.DatePaid, " </P>");
            str = string.Concat(str, " <P> Amount Paid: ", mAPPayment.Amount, " </P>");
            str = string.Concat(str, " <P> Payment Status: PAID </P>");
            str = string.Concat(str, " <P> TicketID: ", mAPPayment.TicketId, " </P>");
            str = string.Concat(str, "<br><br>");
            str = string.Concat(str, "Thank you,");
            str = string.Concat(str, " <P> PHED MAP Team </P> ");
            mailMessage.Body = string.Concat(str, "<br><br>");
            smtpClient.Send(mailMessage);
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        public ActionResult InstalledMeters()
        {
            return base.View();
        }

        public ActionResult ListOfPaidCustomers()
        {
            return base.View();
        }

        [HttpGet]
        public JsonResult LoadAuditTrailReference()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels()
            {
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>(),
                PaymentList = new List<CustomerPaymentInfo>(),
                AuditTrailList = new List<AuditTrail>()
                // applicationDbContext.AuditTrails.ToList<AuditTrail>()
            };
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadReference()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<MAPPayment> list = (
                from p in applicationDbContext.MAPPayments
                where (p.PaymentStatus == "PAID") && (p.ApprovalStatus == "NOTAPPROVED") && (p.PaymentFor == "METER" || p.PaymentFor == "METER PAYMENT")
                select p).ToList<MAPPayment>();
            appViewModel.BSCList = applicationDbContext.BSCs.ToList<BSC>();
            appViewModel.IBCList = applicationDbContext.IBCs.ToList<IBC>();
            appViewModel.PaymentList = new List<CustomerPaymentInfo>();
            appViewModel.MAPPaymentList = list;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }
 
        [HttpGet]
        public JsonResult LoadReferenceWhitelist()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels()
            {
                UplodedStatusList = new List<UploadedFilesVM>(),
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>(),
                MeterUploadApprovalList = new List<MeterList>(),
                PaymentList = new List<CustomerPaymentInfo>(),
                MAPPaymentList = new List<MAPPayment>(),
                ContractorList =  applicationDbContext.MAP_CONTRACTORS.ToList()
            };

            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapPayment()
        {
            return base.View();
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

        public ActionResult PaymentsReport()
        {
            return base.View();
        }

        [HttpGet]
        public JsonResult ReportReference()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels()
            {
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>(),
                PaymentList = (
                    from p in applicationDbContext.CustomerPaymentInfos
                    where p.Token == "MAP"
                    select p).ToList<CustomerPaymentInfo>(),
                MAPPaymentList = new List<MAPPayment>()
            };
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetForm(string TransactionId)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels()
            {
                CustomerDetails = new CustomerPaymentInfo()
            };
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
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

        public ActionResult UploadBulkApplicants()
        {
            return base.View();
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


                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), DocumentName);


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
        public async Task<JsonResult> UploadMeter(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Guid.NewGuid().ToString();
            UploadedFilesVM uploadedFilesVM = new UploadedFilesVM();
            List<UploadedFilesVM> uploadedFilesVMs = new List<UploadedFilesVM>();
            AppViewModels appViewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string str = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string item1 = System.Web.HttpContext.Current.Request.Params["Status"];
                string str1 = System.Web.HttpContext.Current.Request.Params["MAPVendor"];
                string str2 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), str);
                item.SaveAs(str2);
                DataSet dataSet = new DataSet();
                OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", str2, ";Extended Properties=Excel 12.0;"));
                try
                {
                    oleDbConnection.Open();
                    DataTable schema = oleDbConnection.GetSchema("Tables");
                    try
                    {
                        string str3 = schema.Rows[0]["TABLE_NAME"].ToString();
                        string str4 = string.Concat("SELECT * FROM [", str3, "]");
                        (new OleDbDataAdapter(str4, oleDbConnection)).Fill(dataSet, "Items");
                        if (dataSet.Tables.Count == 0)
                        {
                        }
                        if (dataSet.Tables.Count > 0)
                        {
                            MeterList meterList = new MeterList();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                uploadedFilesVM = new UploadedFilesVM();
                                string str5 = dataSet.Tables[0].Rows[i]["MeterNo"].ToString();
                                DbSet<MeterList> meterLists = applicationDbContext.MeterLists;
                                if ((
                                    from p in meterLists
                                    where p.MeterNo == str5
                                    select p).ToList<MeterList>().Count <= 0)
                                {
                                    meterList = new MeterList()
                                    {
                                        MeterNo = str5,
                                        MAPVendor = dataSet.Tables[0].Rows[i]["MeterVendor"].ToString(),
                                        InstallationStatus = item1,
                                        ApprovalStatus = "PENDING"
                                    };
                                    applicationDbContext.MeterLists.Add(meterList);
                                    applicationDbContext.SaveChanges();
                                    uploadedFilesVM.MeterNo = str5;
                                    uploadedFilesVM.MeterVendor = dataSet.Tables[0].Rows[i]["MeterVendor"].ToString();
                                    uploadedFilesVM.Status = "FILE UPLOADED";
                                
                                    uploadedFilesVM.DateUploaded = str1;
                                    uploadedFilesVMs.Add(uploadedFilesVM);
                                }
                                else
                                {
                                    uploadedFilesVM.MeterNo = str5;
                                    uploadedFilesVM.MeterVendor = dataSet.Tables[0].Rows[i]["MeterVendor"].ToString();
                                    uploadedFilesVM.Status = "DUPLICATE!! THIS METER ALREADY  EXISTS";
                                    uploadedFilesVM.DateUploaded = str1;
                                    uploadedFilesVMs.Add(uploadedFilesVM);
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
            appViewModel.UplodedStatusList = uploadedFilesVMs;
            string str6 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str6, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }



       [HttpPost]
        public async Task<JsonResult> UploadContractors(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Guid.NewGuid().ToString();
            UploadedFilesVM uploadedFilesVM = new UploadedFilesVM();
            List<UploadedFilesVM> uploadedFilesVMs = new List<UploadedFilesVM>();
            AppViewModels appViewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string str = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string item1 = System.Web.HttpContext.Current.Request.Params["Status"];
                string MAPVendor = System.Web.HttpContext.Current.Request.Params["MAPVendor"];
                string str2 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), str);
                item.SaveAs(str2);
                DataSet dataSet = new DataSet();
                OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", str2, ";Extended Properties=Excel 12.0;"));
                try
                {
                    oleDbConnection.Open();
                    DataTable schema = oleDbConnection.GetSchema("Tables");
                    try
                    {
                        string str3 = schema.Rows[0]["TABLE_NAME"].ToString();
                        string str4 = string.Concat("SELECT * FROM [", str3, "]");
                        (new OleDbDataAdapter(str4, oleDbConnection)).Fill(dataSet, "Items");
                        if (dataSet.Tables.Count == 0)
                        {
                        }
                        if (dataSet.Tables.Count > 0)
                        {
                            MAP_CONTRACTOR meterList = new MAP_CONTRACTOR();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {meterList = new MAP_CONTRACTOR();
                                string ContractorName = dataSet.Tables[0].Rows[i]["ContractorName"].ToString();
                                string ContractorEmail = dataSet.Tables[0].Rows[i]["ContractorEmail"].ToString();
                                string Address  = dataSet.Tables[0].Rows[i]["ContractorAddress"].ToString();

                                meterList.ContractorName = ContractorName;
                                meterList.ContractorId = Guid.NewGuid().ToString();
                                meterList.Phone = dataSet.Tables[0].Rows[i]["ContractorPhoneNumber"].ToString();
                                meterList.Email = ContractorEmail;

                                meterList.Address = Address;
                                meterList.ProviderId = MAPVendor;
                                applicationDbContext.MAP_CONTRACTORS.Add(meterList);
                                applicationDbContext.SaveChanges();


                                uploadedFilesVM = new UploadedFilesVM();
                               
                                DbSet<MeterList> meterLists = applicationDbContext.MeterLists;
                    
                                    uploadedFilesVM.ContractorName = ContractorName;
                                    uploadedFilesVM.MeterVendor = MAPVendor;
                                    uploadedFilesVM.Status = "Contractor Details Uploaded";
                                    uploadedFilesVM.DateUploaded = DateTime.Now.ToShortTimeString();
                                    uploadedFilesVMs.Add(uploadedFilesVM);
                             
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
            appViewModel.UplodedStatusList = uploadedFilesVMs;
            string str6 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str6, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }





  [HttpPost]
        public async Task<JsonResult> UploadInstallers(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Guid.NewGuid().ToString();
            UploadedFilesVM uploadedFilesVM = new UploadedFilesVM();
            List<UploadedFilesVM> uploadedFilesVMs = new List<UploadedFilesVM>();
            AppViewModels appViewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string str = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string item1 = System.Web.HttpContext.Current.Request.Params["Status"];
                string ContractorName = System.Web.HttpContext.Current.Request.Params["ContractorName"];
                string ContractorId = System.Web.HttpContext.Current.Request.Params["ContractorId"];
                string MAPVendor = System.Web.HttpContext.Current.Request.Params["MAPVendor"];
                string str2 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), str);
                item.SaveAs(str2);
                DataSet dataSet = new DataSet();
                OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", str2, ";Extended Properties=Excel 12.0;"));
                try
                {
                    oleDbConnection.Open();
                    DataTable schema = oleDbConnection.GetSchema("Tables");
                    try
                    {
                        string str3 = schema.Rows[0]["TABLE_NAME"].ToString();
                        string str4 = string.Concat("SELECT * FROM [", str3, "]");
                        (new OleDbDataAdapter(str4, oleDbConnection)).Fill(dataSet, "Items");
                        if (dataSet.Tables.Count == 0)
                        {
                        }
                        if (dataSet.Tables.Count > 0)
                        {
                            MAP_INSTALLER meterList = new MAP_INSTALLER();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                meterList = new MAP_INSTALLER();
                                string InstallerName = dataSet.Tables[0].Rows[i]["InstallerName"].ToString();
                                string InstallerEmail = dataSet.Tables[0].Rows[i]["InstallerEmail"].ToString();
                                string InstallerNumber = dataSet.Tables[0].Rows[i]["InstallerNumber"].ToString();
                                string InstallerAddress = dataSet.Tables[0].Rows[i]["InstallerAddress"].ToString();


                                if (string.IsNullOrEmpty(InstallerName))
                                {
                                    uploadedFilesVM = new UploadedFilesVM(); 
                                    DbSet<MeterList> meterLists = applicationDbContext.MeterLists; 
                                    uploadedFilesVM.ContractorName = ContractorName;
                                    uploadedFilesVM.InstallerName = InstallerName;
                                    uploadedFilesVM.Status = "Installer Details not Uploaded because contractor name is Empty";
                                    uploadedFilesVM.DateUploaded = DateTime.Now.ToShortTimeString();
                                    uploadedFilesVMs.Add(uploadedFilesVM);
                                    continue;
                                }
 
                                meterList.Name = InstallerName;
                                meterList.InstallerId = Guid.NewGuid().ToString();
                                meterList.ContractorId = ContractorId;
                                meterList.ContractorName = ContractorName;
                                meterList.Phone = InstallerNumber;
                                meterList.Email = InstallerEmail;
                                 
                                meterList.Address = InstallerAddress;
                                //meterList.ProviderId = MAPVendor;
                                applicationDbContext.MAP_INSTALLERS.Add(meterList);
                                applicationDbContext.SaveChanges(); 
                                uploadedFilesVM = new UploadedFilesVM();

                                //DbSet<MeterList> meterLists = applicationDbContext.MeterLists;

                                uploadedFilesVM.ContractorName = ContractorName;
                                uploadedFilesVM.InstallerName = InstallerName;
                                uploadedFilesVM.Status = "Installer Details Uploaded";
                                uploadedFilesVM.DateUploaded = DateTime.Now.ToShortTimeString();
                                uploadedFilesVMs.Add(uploadedFilesVM);
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
            appViewModel.UplodedStatusList = uploadedFilesVMs;
            string str6 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str6, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }






  [HttpPost]
        public async Task<JsonResult> UploadPHEDStaff(FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Guid.NewGuid().ToString();
            UploadedFilesVM uploadedFilesVM = new UploadedFilesVM();
            List<UploadedFilesVM> uploadedFilesVMs = new List<UploadedFilesVM>();
            AppViewModels appViewModel = new AppViewModels();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any<string>())
            {
                HttpPostedFile item = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                string str = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                string item1 = System.Web.HttpContext.Current.Request.Params["Status"];
                
               
                string str2 = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), str);
                item.SaveAs(str2);
                DataSet dataSet = new DataSet();
                OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", str2, ";Extended Properties=Excel 12.0;"));
                try
                {
                    oleDbConnection.Open();
                    DataTable schema = oleDbConnection.GetSchema("Tables");
                    try
                    {
                        string str3 = schema.Rows[0]["TABLE_NAME"].ToString();
                        string str4 = string.Concat("SELECT * FROM [", str3, "]");
                        (new OleDbDataAdapter(str4, oleDbConnection)).Fill(dataSet, "Items");
                        if (dataSet.Tables.Count == 0)
                        {
                        }
                        if (dataSet.Tables.Count > 0)
                        {
                            StaffBasicData meterList = new StaffBasicData();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                meterList = new StaffBasicData();
                                string Staff_Id = dataSet.Tables[0].Rows[i]["Staff_Id"].ToString();
                                string Surname = dataSet.Tables[0].Rows[i]["Surname"].ToString();
                                string OtherNames = dataSet.Tables[0].Rows[i]["OtherNames"].ToString();
                                string Employment_Type = dataSet.Tables[0].Rows[i]["Employment_Type"].ToString();
                                //------------------------------------------------------------

                                string Role = dataSet.Tables[0].Rows[i]["Role"].ToString();
                                string Zone = dataSet.Tables[0].Rows[i]["Zone"].ToString();
                                string Feeder = dataSet.Tables[0].Rows[i]["Feeder"].ToString();
                                string Department = dataSet.Tables[0].Rows[i]["Department"].ToString();
                                string Email = dataSet.Tables[0].Rows[i]["Email"].ToString();
                                string CUG = dataSet.Tables[0].Rows[i]["CUG"].ToString();
                                string Phone = dataSet.Tables[0].Rows[i]["Phone"].ToString();


                                if (string.IsNullOrEmpty(Staff_Id))
                                {
                                    uploadedFilesVM = new UploadedFilesVM();
                                    uploadedFilesVM.StaffId = Staff_Id;
                                    uploadedFilesVM.StaffSurname = Surname;
                                    uploadedFilesVM.OtherNames = OtherNames;
                                    uploadedFilesVM.Status = "Staff Details Could not be Uploaded because Staff ID is Empty";
                                    uploadedFilesVM.DateUploaded = DateTime.Now.ToShortTimeString();
                                    uploadedFilesVMs.Add(uploadedFilesVM);
                                    continue;
                                }

                                meterList.Staff_Id = Staff_Id;
                                meterList.Surname = Surname;
                                meterList.OtherNames = OtherNames;
                                meterList.Phone = Phone;
                                meterList.Email = Email;
                                meterList.BSU = Feeder;
                                meterList.IBC = Zone;

                                meterList.Department = Department;
                                applicationDbContext.StaffBasicDatas.Add(meterList);
                                applicationDbContext.SaveChanges();

                                uploadedFilesVM = new UploadedFilesVM();

                                uploadedFilesVM.StaffId = Staff_Id;
                                uploadedFilesVM.StaffSurname = Surname;
                                uploadedFilesVM.OtherNames = OtherNames;
                                uploadedFilesVM.Status = "Staff Details uploaded successfully";
                                uploadedFilesVM.DateUploaded = DateTime.Now.ToShortTimeString();
                                uploadedFilesVMs.Add(uploadedFilesVM);
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
            appViewModel.UplodedStatusList = uploadedFilesVMs;
            string str6 = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str6, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }






        public ActionResult UploadWhiteListMeters()
        {
            return base.View();
        }

        public ActionResult UploadWhiteListMetersApproval()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult ViewApprovedBulkMeters(string Vendor, string Status, string ApprovalStatus)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<MeterList> list = (
                from p in applicationDbContext.MeterLists
                where (p.MAPVendor == Vendor.Trim()) && (p.ApprovalStatus == ApprovalStatus.Trim()) && (p.InstallationStatus == Status)
                select p).ToList<MeterList>();
            appViewModel.MeterUploadApprovalList = list;
            string str = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        [HttpPost]
        public JsonResult ViewCustomer(string TransactionId)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => p.TransactionID == TransactionId);
            appViewModel.CustomerDetails = customerPaymentInfo;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ViewCustomerBRC(string TransactionId)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => (p.TransactionID == TransactionId) & (p.MAPApplicationStatus == "GOBRC"));
            appViewModel.CustomerDetails = customerPaymentInfo;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ViewCustomerInstallMeter(string TransactionId)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            CustomerPaymentInfo customerPaymentInfo = applicationDbContext.CustomerPaymentInfos.FirstOrDefault<CustomerPaymentInfo>((CustomerPaymentInfo p) => ((p.TransactionID == TransactionId) || (p.CustomerReference == TransactionId)) && (p.MAPApplicationStatus == "APPROVED FOR INSTALLATION"));
            appViewModel.CustomerDetails = customerPaymentInfo;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ViewCustomersAudittrailReport(string Activity, string FromDate, string ToDate)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            DateTime _FromDate = Convert.ToDateTime(FromDate);
            DateTime _ToDate = Convert.ToDateTime(ToDate);


            List<AuditTrail> list = (
                from p in applicationDbContext.AuditTrails
                where (p.DateTime >= _FromDate && p.DateTime <= _ToDate) && (p.ActivityType.Trim() == Activity.Trim())
                select p).ToList<AuditTrail>();
            AppViewModels appViewModel = new AppViewModels()
            {
                AuditTrailList = list,
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>()
            };
            string str = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        [HttpPost]
        public JsonResult ViewCustomersUnapprovedData(string BSC, string IBC, string STATUS)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<MAPPayment> list = (
                from p in applicationDbContext.MAPPayments
                where (p.PaymentStatus == "PAID") && (p.ApprovalStatus == "NOTAPPROVED")
                select p).ToList<MAPPayment>();
            appViewModel.MAPPaymentList = list;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }
    [HttpPost]
        public JsonResult ViewCustomersForInstallation(string BSC, string IBC, string STATUS)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<MAPPayment> list = (
                from p in applicationDbContext.MAPPayments
                where (p.PaymentStatus == "PAID") && (p.ApprovalStatus == "APPROVED")
                select p).ToList<MAPPayment>();
            appViewModel.MAPPaymentList = list;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ViewInstalledMeters(string Vendor, string FromDate, string ToDate)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            DateTime dateTime = Convert.ToDateTime(FromDate);
            DateTime dateTime1 = Convert.ToDateTime(ToDate);
            List<CustomerPaymentInfo> list = (
                from p in applicationDbContext.CustomerPaymentInfos
                where (p.MAPApplicationStatus == "METER INSTALLED") && (p.DateCaptured >= (DateTime?)dateTime) && (p.DateCaptured <= (DateTime?)dateTime1) && (p.MAPVendor == Vendor)
                select p).ToList<CustomerPaymentInfo>();
            AppViewModels appViewModel = new AppViewModels()
            {
                PaymentList = list,
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>()
            };
            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }



        [HttpPost]
        public JsonResult ViewInstalledMetersSignage(string Vendor, string FromDate, string ToDate, string status)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            DateTime dateTime = Convert.ToDateTime(FromDate);
            DateTime dateTime1 = Convert.ToDateTime(ToDate);

            List<CustomerPaymentInfo> list = new List<CustomerPaymentInfo>();
            if (Vendor != "ALL")
            {
                list = (
                  from p in applicationDbContext.CustomerPaymentInfos
                  where (p.MAPApplicationStatus == status) && (p.MAPApplicationDate >= (DateTime?)dateTime) && (p.MAPApplicationDate <= (DateTime?)dateTime1) && (p.MAPVendor == Vendor)
                                select p).ToList<CustomerPaymentInfo>();
            }
            else
            {
                list = (
                                from p in applicationDbContext.CustomerPaymentInfos
                                where (p.MAPApplicationStatus == status ) && (p.MAPApplicationDate >= (DateTime?)dateTime) && (p.MAPApplicationDate <= (DateTime?)dateTime1)
                                select p).ToList<CustomerPaymentInfo>();
            }

           
            AppViewModels appViewModel = new AppViewModels()
            {
                PaymentList = list,
                BSCList = applicationDbContext.BSCs.ToList<BSC>(),
                IBCList = applicationDbContext.IBCs.ToList<IBC>()
            };


            appViewModel.PaymentList = list.ToList<CustomerPaymentInfo>();
            JsonResult nullable = base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }


         
        [HttpPost]
        public JsonResult ViewUploadBulkMeters(string Vendor, string Status)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            List<MeterList> list = (
                from p in applicationDbContext.MeterLists
                where (p.MAPVendor == Vendor.Trim()) && (p.ApprovalStatus == Status.Trim())
                select p).ToList<MeterList>();
            appViewModel.MeterUploadApprovalList = list;
            string str = JsonConvert.SerializeObject(appViewModel);
            JsonResult nullable = base.Json(new { result = str, error = "" }, JsonRequestBehavior.AllowGet);
            nullable.MaxJsonLength = new int?(2147483647);
            return nullable;
        }

        public class Rootobject
        {
            public MAPController.Table[] Table
            {
                get;
                set;
            }

            public Rootobject()
            {
            }
        }

        public class Table
        {
            public float ARREARS
            {
                get;
                set;
            }

            public string CUSTOMERNAME
            {
                get;
                set;
            }

            public float FINALBILL
            {
                get;
                set;
            }

            public string METERNO
            {
                get;
                set;
            }

            public string PHASE
            {
                get;
                set;
            }

            public string SUCCESSFUL
            {
                get;
                set;
            }

            public Table()
            {
            }
        }
    }
}