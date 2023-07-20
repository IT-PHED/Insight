using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class EbillsController : Controller
    {
        private bool invalid = false;

        public EbillsController()
        {
        }

        public ActionResult DeliverBills()
        {
            return base.View();
        }

        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();
            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException argumentException)
            {
                this.invalid = true;
            }
            string str = string.Concat(match.Groups[1].Value, domainName);
            return str;
        }


        [HttpGet]
        public async Task<JsonResult> EbillsDelivery()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            AMRReportsModel aMRReportsModel = new AMRReportsModel();
            DataSet dataSet = new DataSet();
            DeliveredEbills EBills = new DeliveredEbills();
            DeliveredBills deliveredBill = new DeliveredBills();
            List<DeliveredBills> deliveredBills = new List<DeliveredBills>();
            appViewModel.DelivererdBillsList = new List<DeliveredBills>();

    
            string str = "payments@phed.com.ng";
            string str1 = "";
            string str2 = "Dlenhance4phed";
            string str3 = "Data Source=23.91.122.233;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
            DataSet dataSet1 = new DataSet();
            string str4 = ""; string strPostpaid = "";
          
                str4 = string.Concat("select AccountNo, Name, GSM, Email, RowNum from MDBillsAlert where Email is not null");
         
          
            (new SqlDataAdapter(str4, str3)).Fill(dataSet1);
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            
            
           
            string str11 = "";
 
          string   BillMonth = DateTime.Now.ToString("MMMM");
            


            appViewModel.NoOfMDCustomers =  dataSet1.Tables[0].Rows.Count.ToString();
            appViewModel.NoOfPostpaidCustomers = "0";


            appViewModel.NoOfMDEmailSent = "";
                
                //db.DeliveredEbillss.Where(p => p.ACCOUNT_STATUS == "MD").ToList().Count.ToString();
                
               // var f = db.DeliveredEbillss.Where(p => p.).ToList().Count.ToString();
            appViewModel.NoOfPOSTPAIDEmailSent = "1,234";

            appViewModel.PercentSuccessMD = "94%";
            appViewModel.PercentSuccessPOSTPAID = "99%";

             
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }

       


        [HttpPost]
        public async Task<JsonResult> EbillsDeliveryLIVE(string Category, string BillMonth, string ModeOfDelivery, string FromRange, string EndRange)
        {
            Exception exception;
            MailMessage mailMessage;
            bool flag;
            bool flag1;
             

            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            DeliveredEbills EBills = new DeliveredEbills();
            AMRReportsModel aMRReportsModel = new AMRReportsModel();
            DataSet dataSet = new DataSet();
            DeliveredBills deliveredBill = new DeliveredBills();
            List<DeliveredBills> deliveredBills = new List<DeliveredBills>();
            string str = "payments@phed.com.ng";
            string str1 = "";
            string str2 = "Dlenhance4phed";
            string str3 = "Data Source=23.91.122.233;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
            DataSet dataSet1 = new DataSet();
            string str4 = "";
            
            if (Category == "MD Customers")
            {
                str4 = string.Concat("select AccountNo, Name, GSM, Email, RowNum from MDBillsAlert where Email is not null and RowNum BETWEEN ", FromRange, " AND ", EndRange);
            }

            else if (Category == "POSTPAID Customers")
            {


            }





            (new SqlDataAdapter(str4, str3)).Fill(dataSet1);
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            DateTime dateTime = DateTime.Parse(BillMonth);
            string str9 = dateTime.ToString("dd-MM-yyyy");
            dateTime = DateTime.Parse(BillMonth);
            string str10 = dateTime.ToString("MM-dd-yyyy");
            string str11 = "";
            dateTime = DateTime.Parse(str9);
            BillMonth = dateTime.ToString("MMMM");
            str6 = DateTime.Parse(str9).Year.ToString();
            int count = dataSet1.Tables[0].Rows.Count;
            
             if (dataSet1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                { //Label0:
                    deliveredBill = new DeliveredBills();
                    EBills = new DeliveredEbills();
                    str5 = dataSet1.Tables[0].Rows[i]["AccountNo"].ToString();
                    string str12 = string.Concat(" and t2.cons_acc ='", str5, "'");
                    str11 = dataSet1.Tables[0].Rows[i]["Name"].ToString();
                    str8 = dataSet1.Tables[0].Rows[i]["GSM"].ToString();
                    str1 = dataSet1.Tables[0].Rows[i]["Email"].ToString();
                    DeliveredBills arrearsFromDLEnhance = new DeliveredBills();
                    try
                    {
                        arrearsFromDLEnhance = this.GetArrearsFromDLEnhance(str5);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        deliveredBill.AccountName = str11;
                        deliveredBill.AccountNo = str5;
                        deliveredBill.Amount = "";
                        deliveredBill.DateSent = new DateTime?(DateTime.Now);
                        deliveredBill.Email = "";
                        deliveredBill.Phone = "";
                        deliveredBill.Status = "An Error Occured and the Arrears could not be retrieved from DLEnhance.";
                        deliveredBills.Add(deliveredBill);
                        
                        SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11,"An Error Occured and the Arrears could not be retrieved from DLEnhance.","NOT SENT","NOT SENT","MD");
                        
                        continue;
                    }
                    if (!(arrearsFromDLEnhance.Arrears == "ERROR"))
                    {
                        str7 = string.Concat("₦", arrearsFromDLEnhance.Current_Amount);
                        string[] billMonth = new string[] { "PHED_Bill_", BillMonth, "_", str6, "_", null };
                        billMonth[5] = Guid.NewGuid().ToString();
                        string str13 = string.Concat(billMonth);
                        string str14 = string.Concat("https://phedpayments.nepamsonline.com/phedpay/pay?AccountNo=", str5, "&AccountType=POSTPAID");
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/RptPages/Billing/ExportBill.aspx/SetBillSessionVariables");
                            httpWebRequest.ContentType = "application/json; charset=utf-8";
                            httpWebRequest.Method = "POST";
                            DateTime.Now.ToString("dd-MM-yyyy");
                            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                            try
                            {
                                billMonth = new string[] { "{\"fileName\":\"", str13, "\",\"consNo\":\"", str12, "\",\"billMonth\":\"", str9, "\"}" };
                                streamWriter.Write(string.Concat(billMonth));
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
                                string str15 = streamReader.ReadToEnd().Replace("\r", string.Empty);
                                str15 = str15.Replace("\n", string.Empty);
                                str15 = str15.Replace("\\", string.Empty);
                                str15 = str15.Replace("\\\\", string.Empty);
                            }
                            finally
                            {
                                if (streamReader != null)
                                {
                                    ((IDisposable)streamReader).Dispose();
                                }
                            }
                        }
                        catch
                        {
                            deliveredBill.AccountName = str11;
                            deliveredBill.AccountNo = str5;
                            deliveredBill.Amount = "";
                            deliveredBill.DateSent = new DateTime?(DateTime.Now);
                            deliveredBill.Email = "";
                            deliveredBill.Phone = "";
                            deliveredBill.Status = "An Error Occured and the Bill File could not be produced from DLEnhance.";
                            deliveredBills.Add(deliveredBill);
                            SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, "An Error Occured and the Arrears could not be retrieved from DLEnhance.", "NOT SENT", "NOT SENT", "MD");
                        
                            continue;
                        }

                        string str16 = string.Concat("https://dlenhance.phed.com.ng//BillPDF//", str13, ".pdf");
                        DataSet dataSet2 = new DataSet();
                        string str17 = "";
                        flag = (ModeOfDelivery == "E-MAIL" ? false : !(ModeOfDelivery == "ALL"));
                        if (!flag)
                        {
                            mailMessage = new MailMessage()
                            {
                                From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                                Subject = "Bill Information From PHED",
                                IsBodyHtml = true
                            };
                            if (!string.IsNullOrEmpty(str1))
                            {
                                if (this.IsEmailAddress(str1))
                                {
                                    mailMessage.To.Add(str1);
                                   mailMessage.Bcc.Add("ebukaegonu@yahoo.com");
                                }
                                else
                                {

                                    string Status = "The specified Email address is not in the Correct Format. Email was not SENT";

                                    deliveredBill.AccountName = str11;
                                    deliveredBill.AccountNo = str5;
                                    deliveredBill.Amount = "";
                                    deliveredBill.DateSent = new DateTime?(DateTime.Now);
                                    deliveredBill.Email = "";
                                    deliveredBill.Phone = "";
                                    deliveredBill.Status = Status;
                                    deliveredBills.Add(deliveredBill);

                                    SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, Status, "NOT SENT", "NOT SENT", "MD");
                        
                                    continue;
                                }
                            }


 


                            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
                            {
                                Credentials = new NetworkCredential(str, str2),
                                Port=   587, 
                                EnableSsl = true
                            };

                            string empty = string.Empty;
                            StreamReader streamReader1 = new StreamReader(Server.MapPath("~/EmailCampaign/PHEDEbills.html"));
                            try
                            {
                                empty = streamReader1.ReadToEnd();
                            }
                            finally
                            {
                                if (streamReader1 != null)
                                {
                                    ((IDisposable)streamReader1).Dispose();
                                }
                            }

                            empty = empty.Replace("{{AccountNo}}", str5);
                            empty = empty.Replace("{{BillMonth}}", BillMonth);
                            empty = empty.Replace("{{BillYear}}", str6);
                            empty = empty.Replace("{{Arrears}}", str7);
                            empty = empty.Replace("{{AccountName}}", str11);
                            empty = empty.Replace("{{BillViewer}}", str16);
                            mailMessage.Body = empty;
                            mailMessage.IsBodyHtml = true;
                            MemoryStream memoryStream = new MemoryStream((new WebClient()).DownloadData(str16));
                            Attachment attachment = new Attachment(memoryStream, new ContentType("application/pdf"));
                            attachment.ContentDisposition.FileName = string.Concat(str13, ".pdf");
                            mailMessage.Attachments.Add(new Attachment(memoryStream, string.Concat(str13, ".pdf")));
                            try
                            {
                                smtpClient.Send(mailMessage);
                                str17 = "Email Sent";
                            }
                            catch (Exception exception3)
                            {
                                Exception exception2 = exception3;
                                deliveredBill.AccountName = str11;
                                deliveredBill.AccountNo = str5;
                                deliveredBill.Amount = str7;
                                deliveredBill.DateSent = new DateTime?(DateTime.Now);
                                deliveredBill.Email = str1;
                                deliveredBill.Phone = str8;
                                deliveredBill.Status = exception2.Message.ToString();
                                str17 = exception2.Message.ToString();
                                memoryStream.Dispose();
                                memoryStream.Close();
                                SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, "Email was not sent because " + exception3.Message, "NOT SENT", "NOT SENT", "MD");

                                continue;
                            }
                            memoryStream.Dispose();
                            memoryStream.Close();
                        }
                        flag1 = (ModeOfDelivery == "SMS" ? false : !(ModeOfDelivery == "ALL"));
                        if (!flag1)
                        {
                            if (str8 != null)
                            {
                                try
                                {
                                    billMonth = new string[] { "Dear ", str11, ",  Your PHED e-bill for ", BillMonth, ", ", str6, ", is ", str7, " Kindly click here to Pay Bills ", str14, " It can only be better with PHED." };
                                    string.Concat(billMonth);
                                    billMonth = new string[] { arrearsFromDLEnhance.AccountName, " / ", arrearsFromDLEnhance.AccountNo, "\nYour Bill for ", BillMonth, ", ", str6, ":\nCurrent Charges: ", null, null, null, null, null, null, null, null, null, null };
                                    decimal num = Convert.ToDecimal(arrearsFromDLEnhance.Current_Amount);
                                    billMonth[8] = num.ToString("#,##0.00");
                                    billMonth[9] = "\nArrears:";
                                    num = Convert.ToDecimal(arrearsFromDLEnhance.Arrears);
                                    billMonth[10] = num.ToString("#,##0.00");
                                    billMonth[11] = "\nAmount Due:";
                                    num = Convert.ToDecimal(arrearsFromDLEnhance.TotalBill);
                                    billMonth[12] = num.ToString("#,##0.00");
                                    billMonth[13] = "\nDue Date:15th ";
                                    billMonth[14] = BillMonth;
                                    billMonth[15] = ", ";
                                    billMonth[16] = str6;
                                    billMonth[17] = ".\nFor enquires call 070022557433";
                                    EbillsController.SendSMS_Simple(string.Concat(billMonth), str8);
                                    str17 = string.Concat(str17, " ,SMS Sent");


                                }
                                catch (Exception exception4)
                                {
                                    exception = exception4;
                                    str17 = string.Concat(str17, " ,SMS NOT Sent");


                                    
                                    deliveredBill.AccountName = str11;
                                    deliveredBill.AccountNo = str5;
                                    deliveredBill.Amount = str7;
                                    deliveredBill.DateSent = new DateTime?(DateTime.Now);
                                    deliveredBill.Email = str1;
                                    deliveredBill.Phone = str8;
                                    deliveredBill.Status = exception4.Message.ToString();
                                    str17 = exception4.Message.ToString();
                                  
                                    SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, "SMS was not sent because " + exception4.Message, "SENT", "NOT SENT", "MD");

                                    continue;
                                     
                                }
                            }
                        }
                        deliveredBill.AccountName = str11;
                        deliveredBill.AccountNo = str5;
                        deliveredBill.Amount = str7;
                        deliveredBill.DateSent = new DateTime?(DateTime.Now);
                        deliveredBill.Email = str1;
                        deliveredBill.Phone = str8;
                        deliveredBill.Status = str17;
                        deliveredBills.Add(deliveredBill); 
                        SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, str17, "SENT", "SENT", "MD");

                        
                    }
                    else
                    {
                        deliveredBill.AccountName = str11;
                        deliveredBill.AccountNo = str5;
                        deliveredBill.Amount = "";
                        deliveredBill.DateSent = new DateTime?(DateTime.Now);
                        deliveredBill.Email = "";
                        deliveredBill.Phone = "";
                        deliveredBill.Status = "An Error Occured and the Arrears could not be retrieved from DLEnhance.";
                        deliveredBills.Add(deliveredBill); 
                        SaveEmailSMSData(BillMonth, applicationDbContext, EBills, str1, str5, str6, str11, "An Error Occured and the Arrears could not be retrieved from DLEnhance.", "NOT SENT", "NOT SENT", "MD");
                    }
                }
            }

           // appViewModel.PercentSuccessMD =



            appViewModel.DelivererdBillsList = deliveredBills;
            return base.Json(JsonConvert.SerializeObject(appViewModel), 0);

        }

         private static void SaveEmailSMSData(string BillMonth, ApplicationDbContext applicationDbContext, DeliveredEbills EBills, string str1, string str5, string str6, 
             string str11, string Comment, string EmailStatus, string SMSStatus, string AccountStatus)
         {
             EBills.ACCOUNT_NAME = str11;
             EBills.ACCOUNT_NO = str5;
             EBills.ACCOUNT_STATUS = AccountStatus;
             EBills.DATE_SENT = DateTime.Now;
             EBills.E_MAIL = str1;
             EBills.SMS_STATUS = EmailStatus;
             EBills.EMAIL_STATUS = SMSStatus;
             EBills.COMMENT = Comment;
             EBills.MONTH = BillMonth;
             EBills.YEAR = str6; 
             applicationDbContext.DeliveredEbillss.Add(EBills);
             applicationDbContext.SaveChanges();
         }






        [HttpPost]
        public async Task<JsonResult> EbillsDeliveryLIVE1(string Category, string BillMonth, string ModeOfDelivery, string FromRange, string ToRange)
        {
            string[] billMonth;
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            AppViewModels appViewModel = new AppViewModels();
            AMRReportsModel aMRReportsModel = new AMRReportsModel();
            DataSet dataSet = new DataSet();
            DeliveredBills deliveredBill = new DeliveredBills();
            List<DeliveredBills> deliveredBills = new List<DeliveredBills>();
            string str = "payments@phed.com.ng";
            string str1 = "Dlenhance4phed";
            if (Category == "MD Customers")
            {
            }
            string str2 = "810005116901";
            string str3 = "";
            string str4 = "";
            string str5 = "810005116901";
            DateTime dateTime = DateTime.Parse(BillMonth);
            string str6 = dateTime.ToString("dd-MM-yyyy");
            dateTime = DateTime.Parse(BillMonth);
            string str7 = dateTime.ToString("MM-dd-yyyy");
            string str8 = string.Concat(" and t2.cons_acc ='", str5, "'");
            string str9 = "ALIBERT NIG.LTD";
            dateTime = DateTime.Parse(str7);
            BillMonth = dateTime.ToString("MMMM");
            str3 = DateTime.Parse(str6).Year.ToString();
            str4 = "₦134,099";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/RptPages/Billing/ExportBill.aspx/SetBillSessionVariables");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            DateTime.Now.ToString("dd-MM-yyyy");
            string str10 = Guid.NewGuid().ToString();
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                billMonth = new string[] { "{\"fileName\":\"", str10, "\",\"consNo\":\"", str8, "\",\"billMonth\":\"", str6, "\"}" };
                streamWriter.Write(string.Concat(billMonth));
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
                string str11 = streamReader.ReadToEnd().Replace("\r", string.Empty);
                str11 = str11.Replace("\n", string.Empty);
                str11 = str11.Replace("\\", string.Empty);
                str11 = str11.Replace("\\\\", string.Empty);
            }
            finally
            {
                if (streamReader != null)
                {
                    ((IDisposable)streamReader).Dispose();
                }
            }
            string str12 = string.Concat("https://dlenhance.phed.com.ng//BillPDF//", str10, ".pdf");
            DataSet dataSet1 = new DataSet();
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company"),
                Subject = "Bill Information From PHED",
                IsBodyHtml = true
            };
            mailMessage.To.Add("Chimebuka.Egonu@phed.com.ng");
            mailMessage.To.Add("ebukaegonu@yahoo.com");
            SmtpClient smtpClient = new SmtpClient("mail.phed.com.ng")
            {
                Credentials = new NetworkCredential(str, str1)
            };
            string empty = string.Empty;
            StreamReader streamReader1 = new StreamReader(base.Server.MapPath("~/EmailCampaign/PHEDEbills.html"));
            try
            {
                empty = streamReader1.ReadToEnd();
            }
            finally
            {
                if (streamReader1 != null)
                {
                    ((IDisposable)streamReader1).Dispose();
                }
            }
            empty = empty.Replace("{{AccountNo}}", str2);
            empty = empty.Replace("{{BillMonth}}", BillMonth);
            empty = empty.Replace("{{BillYear}}", str3);
            empty = empty.Replace("{{Arrears}}", str4);
            empty = empty.Replace("{{AccountName}}", str9);
            empty = empty.Replace("{{BillViewer}}", str12);
            mailMessage.Body = empty;
            mailMessage.IsBodyHtml = true;
            MemoryStream memoryStream = new MemoryStream((new WebClient()).DownloadData(str12));
            Attachment attachment = new Attachment(memoryStream, new ContentType("application/pdf"));
            attachment.ContentDisposition.FileName = string.Concat(str10, ".pdf");
            mailMessage.Attachments.Add(new Attachment(memoryStream, string.Concat(str10, ".pdf")));
            deliveredBill = new DeliveredBills();
            try
            {
                smtpClient.Send(mailMessage);
                string str13 = string.Concat("https://phedpayments.nepamsonline.com/phedpay/pay?AccountNo=", str2, "&AccountType=POSTPAID");
                string str14 = "08067807821";
                billMonth = new string[] { "Dear ", str9, ",  Your PHED e-bill for ", BillMonth, ", ", str3, ", is ", str4, " Kindly click here to Pay Bills ", str13, " It can only be better with PHED." };
                EbillsController.SendSMS_Simple(string.Concat(billMonth), str14);
                deliveredBill.AccountName = str9;
                deliveredBill.AccountNo = str2;
                deliveredBill.Amount = "";
                deliveredBill.DateSent = new DateTime?(DateTime.Now);
                deliveredBill.Email = "";
                deliveredBill.Phone = "";
                deliveredBill.Status = "SENT";
            }
            catch (Exception exception)
            {
                string.Concat("Unable to Send a Mail to you because ", exception.Message);
                deliveredBill.AccountName = str9;
                deliveredBill.AccountNo = str2;
                deliveredBill.Amount = "";
                deliveredBill.DateSent = new DateTime?(DateTime.Now);
                deliveredBill.Email = "";
                deliveredBill.Phone = "";
                deliveredBill.Status = "SENT";
            }
            memoryStream.Dispose();
            memoryStream.Close();
            deliveredBills.Add(deliveredBill);
            appViewModel.DelivererdBillsList = deliveredBills;
            return base.Json(JsonConvert.SerializeObject(appViewModel), JsonRequestBehavior.AllowGet);
        }


        private DeliveredBills GetArrearsFromDLEnhance(string AccountNo)
        {
            DeliveredBills deliveredBill;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            DeliveredBills A = new DeliveredBills()
            {
                Arrears = "ERROR"
            };
            try
            {
                string PhoneNumber = "08067807821";
                string EmailAddress = "chimebuka.egonu@phed.com.ng";
                string AccountType = "POSTPAID";
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                try
                {
                    string[] accountNo = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", AccountNo, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
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
                        A.Arrears = objResponse1[0].ARREAR;
                        A.AccountNo = objResponse1[0].CUSTOMER_NO;
                        A.Current_Amount = objResponse1[0].CURRENT_AMOUNT;
                        A.Address = objResponse1[0].ADDRESS;
                        A.Tarrif_Code = objResponse1[0].TARIFFCODE;
                        A.AccountName = objResponse1[0].CONS_NAME;
                        A.TotalBill = objResponse1[0].TOTAL_BILL;
                    }
                    else
                    {
                        A.Arrears = "ERROR";
                        deliveredBill = A;
                        return deliveredBill;
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
            catch (Exception exception)
            {
                A.Arrears = "ERROR";
            }
            deliveredBill = A;
            return deliveredBill;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        private bool IsEmailAddress(string strIn)
        {
            bool flag;
            this.invalid = false;
            if (!string.IsNullOrEmpty(strIn))
            {
                strIn = Regex.Replace(strIn, "(@)(.+)$", new MatchEvaluator(this.DomainMapper));
                flag = (!this.invalid ? Regex.IsMatch(strIn, "^(?(\")(\"[^\"]+?\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase) : false);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public void SendEmail()
        {
        }

        private static void SendSMS_Simple(string smsMessage, string managersPhoneNose)
        {
            string smsapikey = ConfigurationManager.AppSettings["SMS_APIKEY"];
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.QueryString.Add("cmd", "sendmsg");
                client.QueryString.Add("sessionid", smsapikey);
                client.QueryString.Add("message", smsMessage);
                client.QueryString.Add("sender", "PHED");
                client.QueryString.Add("sendto", managersPhoneNose);
                client.QueryString.Add("msgtype", "0");
                StreamReader reader = new StreamReader(client.OpenRead("http://www.smslive247.com/http/index.aspx"));
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        private DataSet SQLData(string p, string AccountNo, string MeterNo)
        {
            throw new NotImplementedException();
        }
    }
}