using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHEDServe
{
    public partial class MSCSuccess : System.Web.UI.Page
    {
        protected PaymentConfigurationManager ConfigSource { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
      protected void Page_Load(object sender, EventArgs e)
        {
            //decimal ThreePhaseAmount = 72085.68M;
            //decimal SinglePhaseAmount = 39765.86M;

             string _ThreePhaseAmount =    ConfigurationManager.AppSettings["ThreePhaseAmount"].ToString();
             string   _SinglePhaseAmount  =  ConfigurationManager.AppSettings["SinglePhaseAmount"].ToString();
             string _MerchantID = ConfigurationManager.AppSettings["XpressPayMerchantID"].ToString();

             decimal ThreePhaseAmount = Convert.ToDecimal(_ThreePhaseAmount);
             decimal SinglePhaseAmount = Convert.ToDecimal(_SinglePhaseAmount); ; 
           
            string TicketId = Request.QueryString["TicketId"].ToString();
            string PaymentType = Request.QueryString["Type"].ToString();

            //get the Details of the Guy

            //var Details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

            CustomerPaymentInfo Details = (from p in db.CustomerPaymentInfos
                                           where p.TransactionID == TicketId
                                                    select p).SingleOrDefault(); 
            if (Details != null)
            {
                //Bind Server details
                
                TicketId45.Value = Details.TransactionID;
                MapTicketIdApplied.InnerText = Details.TransactionID;
                MAPTicketId.InnerText = Details.TransactionID;
                MAPApplicantsName.InnerText = Details.MAPCustomerName;
                MAPMeterPhase.InnerText = Details.MeterPhase;
                ApplicantsName1.InnerText = Details.MAPCustomerName;
                MAPMeterType1.InnerText = Details.MeterPhase;

                decimal divide = 0;

                if (Details.MAPPlan.Trim() == "50UPFRONT")
                {
                    divide = 2;
                }
                if (Details.MAPPlan.Trim() == "75UPFRONT")
                {
                    divide = 1.33333M;
                }
                if (Details.MAPPlan.Trim() == "25UPFRONT")
                {
                    divide = 4;
                }
                if (Details.MAPPlan.Trim() == "100UPFRONT")
                {
                    divide = 1;
                }
                



                if (Details.MAPPlan.Trim() == "UPFRONT")
                {
                    //show upfront
                    MapApplicationPayment.Visible = true;
                    MapApplicationSuccess.Visible = false;
                    //Do all the HASHING HERE AND 
                    string trans_id = RandomPassword.Generate(10).ToString();
                    string ProductID = "101";


                    string Amount = "";

                    string CustomerName = Details.MAPCustomerName;
                    if (Details.MeterPhase.Trim() == "THREE PHASE")
                    {
                        Amount = ThreePhaseAmount.ToString();
                        MAPAmount.InnerText = ThreePhaseAmount.ToString();
                    }
                    else
                    {
                        Amount = SinglePhaseAmount.ToString();
                        MAPAmount.InnerText = SinglePhaseAmount.ToString();
                    }

                    //Amount = "100";
                    string TransactionId = Details.TransactionID;
                    MAPTransRef.InnerText = trans_id;


                    string CustomerEmail = Details.CustomerEmail;
                    string Currency = "NGN";
                    string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
                    string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
                    string ProductDescription = "MAP Meter Payment";
                    string ProductId = "78";
                    string CallbackURL = "map.nepamsonline.com/successPage.aspx";
                    string MerchantID = _MerchantID;

                    StringBuilder hashString = new StringBuilder();

                    hashString.Append("amount=" + Amount + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                        "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                        + "&trans-id=" + trans_id);
                     
                }


                if (Details.MAPPlan == "50UPFRONT" || Details.MAPPlan == "75UPFRONT" || Details.MAPPlan == "25UPFRONT" || Details.MAPPlan == "100UPFRONT")
                {
                    //show upfront
                    MapApplicationPayment.Visible = true;
                    MapApplicationSuccess.Visible = false;
                    //Do all the HASHING HERE AND 
                    string trans_id = RandomPassword.Generate(10).ToString();
                    string ProductID = "101";


                    string Amount = "";

                    string CustomerName = Details.MAPCustomerName;
                    if (Details.MeterPhase.Trim() == "THREE PHASE")
                    {
                        Amount = (ThreePhaseAmount / divide).ToString();
                        MAPAmount.InnerText = (ThreePhaseAmount / divide).ToString();
                    }
                    else
                    {
                        Amount = (SinglePhaseAmount / divide).ToString();
                        MAPAmount.InnerText = (SinglePhaseAmount / divide).ToString();
                    }

                    //Amount = "100";
                    string TransactionId = Details.TransactionID;
                    MAPTransRef.InnerText = trans_id;


                    string CustomerEmail = Details.CustomerEmail;
                    string Currency = "NGN";
                    string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
                    string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
                    string ProductDescription = "MAP Meter Payment";
                    string ProductId = "78";
                    string CallbackURL = "map.nepamsonline.com/successPage.aspx";
                    string MerchantID = _MerchantID;

                    StringBuilder hashString = new StringBuilder();

                    hashString.Append("amount=" + Amount + "&callback-url=" + CallbackURL + "&currency=" + Currency + "&customer-email=" + CustomerEmail +
                        "&merchant-id=" + MerchantID + "&product-desc=" + ProductDescription + "&product-id=" + ProductId + "&public-key=" + PublicKey
                        + "&trans-id=" + trans_id);
                     
                }
                else
                {
                   // string HashCode = CreateHash(hashString.ToString(), Key);
                     
                    MapApplicationPayment.Visible = false;
                    MapApplicationSuccess.Visible = true; 
                } 
            } 
        }
         
    }
}