using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class CustomerCareController : Controller
    {
        public CustomerCareController()
        {
        }

        private void GetBirthdayList()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string username = base.User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();
            DateTime today = DateTime.Today;
            SMSSent send = (
                from p in context.SMSSents
                where p.BirthDate.Month == today.Month && p.BirthDate.Day == today.Day
                select p).FirstOrDefault<SMSSent>();
            DateTime Date = DateTime.Now;
            string str = Date.ToString("MMMM");
            int day = Date.Day;
            if (send == null)
            {
                SMSSent g = new SMSSent()
                {
                    BirthDate = today
                };
                context.SMSSents.Add(g);
                context.SaveChanges();
            }
            else if (!(send.Status == "SENT"))
            {
                List<PHEDServe.Models.KYC> userQuery = (
                    from p in context.KYCs
                    where (p.MonthOfBirth == str) && p.DayOfBirth == (int?)day
                    select p).ToList<PHEDServe.Models.KYC>();
                foreach (PHEDServe.Models.KYC KYC in userQuery)
                {
                    if (KYC.PHONE != null)
                    {
                        string Message = string.Concat("Dear", KYC.CustomerName, ", PHED wishes you a very happy Birthday. Many more happy years ahead. Thank you for your patronage.");
                        CustomerCareController.SendSMS_Simple(Message, KYC.PHONE);
                    }
                }
                if (send.Status == null)
                {
                    DbSet<SMSSent> sMSSents = context.SMSSents;
                    object[] serial = new object[] { send.Serial };
                    SMSSent d = sMSSents.Find(serial);
                    d.Status = "SENT";
                    context.Entry<SMSSent>(d).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public ActionResult Index()
        {
            this.GetBirthdayList();
            return base.View();
        }

        public ActionResult KYC()
        {
            return base.View();
        }

        public JsonResult KYCLoad()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string username = base.User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();
            DateTime TodaysDate = DateTime.Now;
            viewModel.KYC = new KYC();
            return base.Json(JsonConvert.SerializeObject(viewModel), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResolveComplaints()
        {
            return base.View();
        }

        [HttpPost]
        public JsonResult SaveKYC(string AccountType, string CustomerName, string CustomerSurname, string CustomerMiddleName, string PhoneNumber, string DateOfBirth, string Address, string AccountNumber, string EmailAddress, string MeterNo)
        {
            DateTime Date;
            ApplicationDbContext context = new ApplicationDbContext();
            string username = base.User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();
            KYC SaveData = context.KYCs.FirstOrDefault(p => p.ACCOUNT_NO == AccountNumber);
            if (SaveData == null)
            {
                KYC data = new KYC();
               
              data.ACCOUNT_TYPE = AccountType ;
                Date = Convert.ToDateTime(DateOfBirth);
                data.DATE_OF_BIRTH = new DateTime?(Date);
                data.DayOfBirth = new int?(Date.Day);
                data.MonthOfBirth = Date.ToString("MMMM");
                data.E_MAIL = EmailAddress;
                data.METER_NO = MeterNo;
                data.PHONE = PhoneNumber;
                data.UPDATED_BY = base.User.Identity.Name;
                data.CustomerMiddleName = CustomerMiddleName;
                data.CustomerName = CustomerName;
                data.CustomerSurname = CustomerSurname;
                data.ADDRESS = Address;
                data.ACCOUNT_NO = AccountNumber;
                context.KYCs.Add(data);
                context.SaveChanges();
            }
            else
            {
                SaveData.ACCOUNT_TYPE = AccountType;
                Date = Convert.ToDateTime(DateOfBirth);
                SaveData.DATE_OF_BIRTH = new DateTime?(Date);
                SaveData.DayOfBirth = new int?(Date.Day);
                SaveData.MonthOfBirth = Date.ToString("MMMM");
                SaveData.E_MAIL = EmailAddress;
                SaveData.PHONE = PhoneNumber;
                SaveData.UPDATED_BY = base.User.Identity.Name;
                SaveData.CustomerMiddleName = CustomerMiddleName;
                SaveData.CustomerName = CustomerName;
                SaveData.CustomerSurname = CustomerSurname;
                SaveData.ADDRESS = Address;
                SaveData.METER_NO = MeterNo;
                context.Entry(SaveData).State = EntityState.Modified;
                context.SaveChanges();
            }
            viewModel.KYC = context.KYCs.FirstOrDefault(p=> (p.ACCOUNT_NO == AccountNumber) || (p.METER_NO == AccountNumber));
            string result = JsonConvert.SerializeObject(viewModel);
            JsonResult jsonResult = base.Json(new { AccountType = AccountType, MeterNo = MeterNo, Address = Address, result = result, CustomerName = CustomerName, EmailAddress = EmailAddress }, JsonRequestBehavior.AllowGet);
            return base.Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchData(string AccountNumber, string AccountType)
        {
            JsonResult jsonResult1;
            ApplicationDbContext context = new ApplicationDbContext();
            string username = base.User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string PhoneNumber = "08067807821";
            string EmailAddress = "ebukaegonu@yahoo.com";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                string[] accountNumber = new string[] { "{\"Username\":\"phed\",\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\",\"CustomerNumber\":\"", AccountNumber, "\",\"Mobile_Number\":\"", PhoneNumber, "\",\"Mailid\":\"", EmailAddress, "\",\"CustomerType\":\"", AccountType.ToUpper(), "\"}" };
                streamWriter.Write(string.Concat(accountNumber));
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
                string result = streamReader.ReadToEnd();
                result = result.Replace("\r", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace("\\", string.Empty);
                result = result.Replace("\\\\", string.Empty);
                if (!(result == "Customer Not Found"))
                {
                    List<DirectJSON> objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);
                    string CustomerName = objResponse1[0].CONS_NAME;
                    string IBC = objResponse1[0].IBC_NAME;
                    string BSC = objResponse1[0].BSC_NAME;
                    string PhoneNo = objResponse1[0].MOB_NO;
                    string Address = objResponse1[0].ADDRESS;
                    string MeterNo = objResponse1[0].METER_NO;
                    string AccountNo = objResponse1[0].CUSTOMER_NO;
                    viewModel.KYC = context.KYCs.FirstOrDefault<KYC>((KYC p) => (p.ACCOUNT_NO == AccountNumber) || (p.METER_NO == AccountNumber));
                    result = JsonConvert.SerializeObject(viewModel);
                    JsonResult jsonResult = base.Json(new { AccountType = AccountType, IBC = IBC, BSC = BSC, PhoneNo = PhoneNo, AccountNo = AccountNo, MeterNo = MeterNo, Address = Address, result = result, CustomerName = CustomerName, EmailAddress = EmailAddress }, JsonRequestBehavior.AllowGet);
                    jsonResult1 = base.Json(jsonResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonResult1 = base.Json(new { result = result }, JsonRequestBehavior.AllowGet);
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

        private void SendEmail()
        {
            throw new NotImplementedException();
        }

        private void SendSMS()
        {
            throw new NotImplementedException();
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
    }
}