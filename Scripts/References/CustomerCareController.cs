using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class CustomerCareController : Controller
    {
        //
        // GET: /CustomerCare/
        public ActionResult Index()
        {
            GetBirthdayList();
            return View();
        }
        
        public ActionResult KYC()
        {
            return View();
        }
        
        public ActionResult ResolveComplaints()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveKYC(string AccountType,string CustomerName, string CustomerSurname,string CustomerMiddleName,string PhoneNumber, string DateOfBirth, string Address, string AccountNumber, string EmailAddress)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();
            KYC SaveData = context.KYCs.FirstOrDefault(p => p.ACCOUNT_NO == AccountNumber);

            SaveData.ACCOUNT_TYPE = AccountType;
            SaveData.DATE_OF_BIRTH = Convert.ToDateTime(DateOfBirth);
            SaveData.E_MAIL = EmailAddress;
           // SaveData.METER_NO =  AccountNumber;
            SaveData.PHONE=  PhoneNumber;
            SaveData.UPDATED_BY = User.Identity.Name;
            SaveData.CustomerMiddleName = CustomerMiddleName;
            SaveData.CustomerName = CustomerName;
            SaveData.CustomerSurname = CustomerSurname;
            SaveData.ADDRESS = Address;

            context.Entry(SaveData).State = EntityState.Modified;
            context.SaveChanges();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchData(string AccountNumber, string AccountType)
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            //Call DLENhance

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string PhoneNumber = "08067807821";
            string EmailAddress = "ebukaegonu@yahoo.com";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                              "\"CustomerNumber\":\"" + AccountNumber + "\"," +
                              "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                                "\"Mailid\":\"" + EmailAddress + "\"," +
                              "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
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

                if (result == "Customer Not Found")
                {
                    var _jsonResult = Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    return _jsonResult;
                }

                //Dear Nsikak,
                //PHED wishes you a very happy Birthday. Many more happy years ahead. Thank you for your patronage.
                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);

                string CustomerName = objResponse1[0].CONS_NAME;
                string IBC = objResponse1[0].IBC_NAME;
                string BSC = objResponse1[0].BSC_NAME;
                string PhoneNo = objResponse1[0].MOB_NO;
                string Address = objResponse1[0].ADDRESS;
                string MeterNo = objResponse1[0].METER_NO;
                string AccountNo = objResponse1[0].CUSTOMER_NO;

                viewModel.KYC = context.KYCs.FirstOrDefault((p => p.ACCOUNT_NO == AccountNumber || p.METER_NO == AccountNumber));
                result = JsonConvert.SerializeObject(viewModel);
                var jsonResult = Json(new { AccountType = AccountType, IBC = IBC, BSC = BSC, PhoneNo = PhoneNo, AccountNo = AccountNo, MeterNo = MeterNo, Address = Address, result = result, CustomerName = CustomerName, EmailAddress = EmailAddress }, JsonRequestBehavior.AllowGet);

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
        }

        private static void SendSMS_Simple(string smsMessage, String managersPhoneNose)
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
        private void GetBirthdayList()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            //check if this has been done today 
            DateTime today = DateTime.Today;
            var send = context.SMSSents.Where(p => p.BirthDate.Month == today.Month && p.BirthDate.Day == today.Day).FirstOrDefault();
            if (send == null)
            {
                //inset todays Date to the 

                SMSSent g = new SMSSent();
                g.BirthDate = today;

                context.SMSSents.Add(g);
                context.SaveChanges();
            }

            else
                if (send.Status == "SENT")
                {


                }
                else
                {

                    var userQuery = context.KYCs.Where(p => p.DATE_OF_BIRTH.Month == today.Month && p.DATE_OF_BIRTH.Day == today.Day).ToList();


                    foreach (var KYC in userQuery)
                    {

                        if (KYC.PHONE == null)
                        {


                        }
                        else
                        {

                            string Message = "Dear" + KYC.CustomerName + ", PHED wishes you a very happy Birthday. Many more happy years ahead. Thank you for your patronage.";
                            SendSMS_Simple(Message, KYC.PHONE);

                            // SendEmail();
                        }
                       



                    }

                    if (send.Status == null)
                    {
                        //it has not been Updated Please do so

                        SMSSent d = context.SMSSents.Find(send.Serial);
                        d.Status = "SENT";
                        context.Entry(d).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }



           
       
            

            

        }

        private void SendSMS()
        {
            throw new NotImplementedException();
        }

        private void SendEmail()
        {
            throw new NotImplementedException();
        }

        public JsonResult KYCLoad()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            DateTime TodaysDate = DateTime.Now;
            //  viewModel.PaymentList = db.CustomerPaymentInfos.Where(p => p.DatePaid == TodaysDate).ToList();
            viewModel.KYC = new KYC();
            //List<KYC>();// db.CustomerPaymentInfos.Where(p => p.AlternateCustReference == "27100144685").ToList();
            //viewModel.BSCList = new BusinessServiceCenter();
            //viewModel.IBCList = new IntegratedServiceCenter();
            //viewModel.MarketerList = new List<Marketer>();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

	}
}