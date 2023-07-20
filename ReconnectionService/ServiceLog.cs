using PHEDServe;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace ReconnectionService
{
  public  class ServiceLog
    {

        ApplicationDbContext db = new ApplicationDbContext();
        GlobalMethodsLib dal = new GlobalMethodsLib();
        AppViewModels viewModel = new AppViewModels();
    
     
        //private static void WriteErrorLog(string text)
        //{
        //    string path = "C:\\PHEDServiceLog.txt";
        //    using (StreamWriter writer = new StreamWriter(path, true))
        //    {
        //        writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
        //        writer.Close();
        //    }
        //}

        /// <summary>  
        /// this function write Message to log file.  
        /// </summary>  
        /// <param name="Message"></param>  
        //public static void WriteErrorLog(string Message)
        //{
        //    StreamWriter sw = null;
        //    try
        //    {
        //        sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\PHEDServiceLog.txt", true);
        //        sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
        //        sw.Flush();
        //        sw.Close();
        //    }
        //    catch
        //    {
        //    }
        //}

        private static void WriteErrorLog(string text)
        {
            string path = "C:\\PHEDServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

        #region Send Email Code Function
        /// <summary>  
        /// Send Email with cc bcc with given subject and message.  
        /// </summary>  
        /// <param name="ToEmail"></param>  
        /// <param name="cc"></param>  
        /// <param name="bcc"></param>  
        /// <param name="Subj"></param>  
        /// <param name="Message"></param>  
        public static void SendEmail(String ToEmail, string cc, string bcc, String Subj, string Message)
        {
            //Reading sender Email credential from web.config file  

            string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
            string Pass = ConfigurationManager.AppSettings["Password"].ToString();

            //creating the object of MailMessage  
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
            mailMessage.Subject = Subj; //Subject of Email  
            mailMessage.Body = Message; //body or message of Email  
            mailMessage.IsBodyHtml = true;

            string[] ToMuliId = ToEmail.Split(',');
            foreach (string ToEMailId in ToMuliId)
            {
                mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id  
            }


            string[] CCId = cc.Split(',');

            foreach (string CCEmail in CCId)
            {
                mailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
            }

            string[] bccid = bcc.Split(',');

            foreach (string bccEmailId in bccid)
            {
                mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id  
            }
            SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
            smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

            //network and security related credentials  

            smtp.EnableSsl = false;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = Pass;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 3535;
            smtp.Send(mailMessage); //sending Email  
        }



     

        #endregion 
    
      
    
       

    }
}
