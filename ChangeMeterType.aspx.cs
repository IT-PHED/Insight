using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHEDServe.Views.Home
{
    public partial class ChangeMeterType : System.Web.UI.Page
    {

        public static string _ThreePhaseAmount = ConfigurationManager.AppSettings["ThreePhaseAmount"].ToString();
        public static string _SinglePhaseAmount = ConfigurationManager.AppSettings["SinglePhaseAmount"].ToString();
        public static string InfoStatus = "";  public static string TicketID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            TicketDetails.Visible = false; TicketInput.Visible = true; InfoPanel.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string s = this.TextBox1.Text;

           CustomerPaymentInfo d =    db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == s);


           if (d != null)
           {
               //check if the Guy i three Phase
               TicketID = s;

               if(d.MeterPhase.Trim() == "THREE PHASE")
               {
                   InfoPanel.Visible = true; TicketInput.Visible = false;
                   InfoLabel1.Text = "Dear " + d.MAPCustomerName + ", You have already applied for a 3-Phase Meter. This request cannot be changed or downgraded. You can only upgrade a single phase meter to a Three phase meter. Thank you";
                   return;
               }
                
               //Bind the Data to the Page and let it Show for the Customer
               TicketDetails.Visible = true;
               TicketInput.Visible = false;
                
               //Bind the Values to the Page

               this.TicketIDTextBox2 .Text= d.TransactionID;
               this.FeederTextBox3.Text = d.BSC;
               this.TransformerTextBox2.Text = d.DTR_NAME;
               this.AccountNameTextBox3.Text = d.CustomerName;
               this.ZoneTextBox3.Text = d.IBC;
               this.AccountNoTextBox3.Text = d.CustomerReference;
               this.MAPPlanTextBox2.Text = d.MAPPlan;
               this.MAPPhaseTextBox2.Text = d.MeterPhase.ToString(); ;
             
               this.AccountNameTextBox3.Text = d.CustomerName;
                this.ApplicantNameTextBox3.Text = d.MAPCustomerName;
               ///////////////////
               this.BalanceTobePaidAddress2.Text   = d.AmountToPayMSC;
               this.MAPAmountPhaseNoUpload.Text = d.MAPAmount; 
               this.UpfrontAmountPhaseNoUpload.Text = d.AmountToPayUpfront;
               this.StatusPaidAddress2.Text = d.MAPApplicationStatus;
               this.PaymentStatusMeterPhaseNoUpload.Text = d.MAPPaymentStatus;
                
               //Calculate the New Meter Amount 
               this.NewMeterAmount.Text = _ThreePhaseAmount;

               //get the Corresponding Amount for the MAP profiel Selected 

               decimal divide = 0;
               if (d.MAPPlan.Trim() == "50UPFRONT")
               {
                   divide = 2;
               }
               if (d.MAPPlan.Trim() == "75UPFRONT")
               {
                   divide = 1.33333M;
               }
               if (d.MAPPlan.Trim() == "25UPFRONT")
               {
                   divide = 4;
               }
               if (d.MAPPlan.Trim() == "100UPFRONT")
               {
                   divide = 1;
               }
               if (d.MAPPlan.Trim() == "UPFRONT")
               {
                   divide = 1;
               }



               if (d.MAPPaymentStatus == "NOT PAID")
               {

                   this.NewAmountUpgradeAmount.Text = (Convert.ToDecimal(_ThreePhaseAmount) / divide).ToString();
               }
               else
               { 
                   this.NewAmountUpgradeAmount.Text = ((Convert.ToDecimal(_ThreePhaseAmount) / divide) - Convert.ToDecimal(d.AmountToPayUpfront)).ToString();
                    
               }
                


            
           }
        

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if(Label1.Text == "UPGRADE")
            {
                Response.Redirect("~/MSCSuccessUpfront.aspx?PaymentType=UPGRADE&TicketId=" + TicketID);
            }
            else
            {

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
          
            ApplicationDbContext db = new ApplicationDbContext();
            string s = this.TextBox1.Text;
            //Check if the Ticket Number has already Upgraded the Meter 
  CustomerPaymentInfo d = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == s);

            var Check = db.MAP_METER_UPGRADEs.FirstOrDefault(p => p.TICKET_ID == d.TransactionID);

            if (Check != null)
            {
                if (Check.PAYMENT_STATUS == "PAID" && d.MAPApplicationStatus != "INSTALLED")
                {

                    InfoPanel.Visible = true; TicketInput.Visible = false; TicketDetails.Visible = false;
                    InfoLabel1.Text = "Dear " + d.MAPCustomerName + ", You have requested for a meter Upgrade earlier from Single phase to three phase and your Payment Status is PAID. Your Meter will be installed Soon. Thank you";

             
                    return;
                }
                if (Check.PAYMENT_STATUS == "PAID" && d.MAPApplicationStatus == "INSTALLED")
                {

                    InfoPanel.Visible = true; TicketInput.Visible = false; TicketDetails.Visible = false;
                    InfoLabel1.Text = "Dear " + d.MAPCustomerName + ", You have requested for a meter Upgrade earlier from Single phase to three phase and your Payment Status is PAID. Your Meter has been installed. Thank you";
                         Label1.Text = "UPGRADE";
                    InfoStatus = "UPGRADE";  
                    return;
                }

                else
                {
                    InfoPanel.Visible = true; TicketInput.Visible = false; TicketDetails.Visible = false;
                    InfoLabel1.Text = "Dear " + d.MAPCustomerName + ", You have requested for a meter Upgrade earlier from Single phase to three phase and your Payment is yet to be made. Kindly click on the button below to make a Payment and have your meter Installed. Your Meter will be installed as soon as the payment is Verified and approved. Thank you";
                    return;
                }

            }

            //Save the Details for the Meter Upgrade 
            d.UpgradeStatus = "UPGRADE";
            d.UpgradeAmount = Math.Round(Convert.ToDecimal( this.NewAmountUpgradeAmount.Text), 2).ToString();  
            d.UpgradeDate = DateTime.Now;
            d.UpgradePaymentStatus = "NOT PAID";
            db.Entry(d).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            MAP_METER_UPGRADE map = new MAP_METER_UPGRADE();

            map.ACCOUNT_NO = d.CustomerReference;
            map.DATE_APPLIED = DateTime.Now;
            map.MAP_CUSTOMER_NAME = d.MAPCustomerName;
            map.MSC_AMOUNT = Math.Round((Convert.ToDecimal(_ThreePhaseAmount) - Convert.ToDecimal(this.NewAmountUpgradeAmount.Text)), 2).ToString();
             
            map.NEW_MAP_AMOUNT = _ThreePhaseAmount;
            map.NEW_MAP_PHASE = "THREE PHASE";
            map.NEW_MAP_PLAN = d.MAPPlan;
            map.OLD_MAP_AMOUNT = d.MAPAmount;
            map.OLD_MAP_PHASE = d.MeterPhase;
            map.OLD_MAP_PLAN = d.MAPPlan;
            map.PAYMENT_STATUS = "NOT PAID";
            map.TICKET_ID = d.TransactionID;
            map.UPFRONT_AMOUNT = this.NewAmountUpgradeAmount.Text;
            db.MAP_METER_UPGRADEs.Add(map);
            db.SaveChanges();


            InfoPanel.Visible = true; TicketInput.Visible = false; TicketDetails.Visible = false;
            InfoLabel1.Text = "Dear " + d.MAPCustomerName + ", Your request to upgrade you to a three phase meter was successful. You may proceed to pay the  already applied for a 3-Phase Meter. This request cannot be changed or downgraded. You can only upgrade a single phase meter to a Three phase meter. Thank you";
            Label1.Text = "UPGRADE";
            InfoStatus = "UPGRADE";
        }
    }
}