using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PHEDServe;
using PHEDServe.Models;
using System.Data.Entity;
using ERDBManager;
using System.IO;

namespace ReconnectionService
{
    public partial class MailService : ServiceBase
    {
        Timer timer1  = new Timer();
        int getCallType;
        string timeString = ""; 
        ApplicationDbContext db = new ApplicationDbContext();
        GlobalMethodsLib dal = new GlobalMethodsLib(); 
        AppViewModels viewModel = new AppViewModels();
        //  System.Configuration..ConfigurationSettings ConfigurationManager = new ConfigurationSettings();
        //string value = System.Configuration.ConfigurationManager.AppSettings[key];

        public MailService()
        {
            InitializeComponent();
            int strTime = Convert.ToInt32(ConfigurationManager.AppSettings["callDuration"]);
            getCallType = Convert.ToInt32(ConfigurationManager.AppSettings["CallType"]);
            if (getCallType == 1)
            {
                timer1 = new System.Timers.Timer();
                double inter = (double)GetNextInterval();
                timer1.Interval = inter;
                timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
            }
            else
            {
                timer1 = new System.Timers.Timer();
                timer1.Interval = strTime * 1000;
                timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
            }
        }

        protected override void OnStart(string[] args)
        {
            timer1.AutoReset = true;
            timer1.Enabled = true;
            WriteErrorLog("Daily Reporting service started");   
        }

        protected override void OnStop()
        {
            timer1.AutoReset = false;
            timer1.Enabled = false;
            WriteErrorLog("Daily Reporting service stopped");   
        }


        public void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
           /// string Msg = "Hi ! This is DailyMailSchedulerService mail.";//whatever msg u want to send write here.  


            CheckDisconnectionList();
            if (getCallType == 1)
            {
                timer1.Stop();
                System.Threading.Thread.Sleep(1000000);
                SetTimer();
            }
        }


       

        private double CalculatePercentageOfAmount(string Percentpayment, string IncidenceAmount)
        { 
            double AmountPaid = 0;

            AmountPaid = Convert.ToDouble(IncidenceAmount) * (Convert.ToDouble(Percentpayment) / 100);


            return AmountPaid;
        }
          
        private double GetNextInterval()
        {
            timeString = ConfigurationManager.AppSettings["StartTime"];
            DateTime t = DateTime.Parse(timeString);
            TimeSpan ts = new TimeSpan();
            int x;
            ts = t - System.DateTime.Now;
            if (ts.TotalMilliseconds < 0)
            {
                ts = t.AddDays(1) - System.DateTime.Now;
                //Here you can increase the timer interval based on your requirments.   
            }
            return ts.TotalMilliseconds;
        }

        public void CheckDisconnectionList()
        {
            DataSet ds = new DataSet();

            //Pull Data into the Dataset
            var ReconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == "DISCONNECTED").ToList();
             
            foreach (var discon in ReconnectionList)
            {

                string AccountNo = discon.AccountNo;

                //var  list  = from list in db.RCDCDisconnectionLists select  


                if (CheckIfHeIsEligibleForReconnection(AccountNo, discon.DisconID))
                {

                    RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.Find(discon.DisconID);

                    if (Discon != null)
                    {

                        Discon.DisconStatus = "RECONNECT";
                        db.Entry(Discon).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                    //He has been Added to the List for reconnection
                    WriteErrorLog("AccountNo has " + AccountNo + " has passed the test for reconnection and has been added to the reconnection List on ");
                }
                else
                {
                    //he did not pass the test for Reconnection ;
                    WriteErrorLog("AccountNo " + AccountNo + " did not pass the test for reconnection and has not been added to the reconnection List on ");

                } 
            }
        }

        private  void WriteErrorLog(string text)
        {
            string path = "C:\\PHEDServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

        private bool CheckIfHeIsEligibleForReconnection(string AccountNo, string DisconnId)
        {

            //get the Incidences that the Guy has been biled

            var Incidence = db.RCDC_Disconnection_Incidence_Historys.Where(p => p.DisconnId == DisconnId).ToList();

            int Paid = 0;


            foreach (var o in Incidence)
            {
                //Check if he has paid
                string PercentageOfPayment = o.Percentpayment;

                //go to DLEnhance and Get the Payment from the 

                DateTime DisconnectionDate = Convert.ToDateTime(o.DateDisconnected);

                if (dal.HasHePaid(o.IncidenceId, o.IncidenceAmount, o.Percentpayment, DisconnectionDate, AccountNo))
                {
                    Paid = Paid + 1;
                }
            }

            if (Paid == Incidence.Count)
            {
                return true;
            }
            return false;
        }

        private void SetTimer()
        {
            try
            {
                double inter = (double)GetNextInterval();
                timer1.Interval = inter;
                timer1.Start();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
