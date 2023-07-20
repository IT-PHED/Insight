using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PHEDServe.Models;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace PHEDServe
{
    public partial class ReportPage : System.Web.UI.Page
    {
        ApplicationDbContext db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        
            DataSet ds = new DataSet();
            string msg = "";
            string Category = Request.QueryString["TYPE"];
            ReportViewer1.LocalReport.EnableHyperlinks = true;
             if (Category == "RECEIPT")
             {
                 string TicketId = Request.QueryString["TicketId"];

                 ApplicationDbContext db = new ApplicationDbContext();
                 var ApplicationComplete = db.CUSTOMERS.Where(p => p.TransactionID == TicketId).ToList();

                 ReportViewer1.Reset();
                 LocalReport reportengine = ReportViewer1.LocalReport;
                 reportengine.ReportPath = Server.MapPath("~/Reports/Receipt.rdlc");
                 //reportengine.DataSources.Clear();
                 reportengine.DataSources.Add(new ReportDataSource("MAPDDS", ApplicationComplete));
                 // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                 ////////////////download PDF browser////////////////
                 byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
                 //display pdf in browser
                 Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                 Response.ContentType = "application/pdf";
                 Response.BinaryWrite(bytes);
                 Response.End();

             }

             if (Category == "PAYBANKMAP")
             {
                 string TicketId = Request.QueryString["TicketId"];

                 ApplicationDbContext db = new ApplicationDbContext();
                 var ApplicationComplete = db.CustomerPaymentInfos.Where(p => p.TransactionID == TicketId).ToList();

                 ReportViewer1.Reset();
                 LocalReport reportengine = ReportViewer1.LocalReport;
                 reportengine.ReportPath = Server.MapPath("~/Reports/PayBankInvoice.rdlc");
                 //reportengine.DataSources.Clear();
                 reportengine.DataSources.Add(new ReportDataSource("BankPaymentDS", ApplicationComplete));
                 // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                 ////////////////download PDF browser////////////////
                 byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
                 //display pdf in browser
                 Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                 Response.ContentType = "application/pdf";
                 Response.BinaryWrite(bytes);
                 Response.End();

             }

             if (Category == "PAYBANKARREARS")
             {
                 string TicketId = Request.QueryString["TicketId"];

                 ApplicationDbContext db = new ApplicationDbContext();
                 var ApplicationComplete = db.CustomerPaymentInfos.Where(p => p.TransactionID == TicketId).ToList();

                 ReportViewer1.Reset();
                 LocalReport reportengine = ReportViewer1.LocalReport;
                 reportengine.ReportPath = Server.MapPath("~/Reports/PayBankInvoiceArrears.rdlc");
                 //reportengine.DataSources.Clear();
                 reportengine.DataSources.Add(new ReportDataSource("BankPaymentDS", ApplicationComplete));
                 // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                 ////////////////download PDF browser////////////////
                 byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
                 //display pdf in browser
                 Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                 Response.ContentType = "application/pdf";
                 Response.BinaryWrite(bytes);
                 Response.End();

             }


             if (Category == "OFFICEREPRINT")
            {
                string TicketId = Request.QueryString["TicketId"];
              
                ApplicationDbContext db = new ApplicationDbContext(); 
                var ApplicationComplete = db.CUSTOMERS.Where(p => p.TransactionID == TicketId).ToList();


                string MeterType = ApplicationComplete.FirstOrDefault().MeterType.Trim();
                string ModeOfPayment = ApplicationComplete.FirstOrDefault().ModeOfPayment.Trim();


                 //get the Payment schedulef rothe Customers Selection of his Meters

                var paymentschedule = db.METER_REPAYMENT_PLANs.Where(p => p.Repayment_Plan_Phase.Trim() == MeterType.Trim().Replace(" ","") && p.Repayment_MAP_Plan.Trim() == ModeOfPayment.Trim()).ToList();

       
                ReportViewer1.Reset();
                LocalReport reportengine = ReportViewer1.LocalReport;
                reportengine.ReportPath = Server.MapPath("~/Reports/CompletedMAPApplicationForm2.rdlc"); 
                reportengine.DataSources.Add(new ReportDataSource("MAPDATA", ApplicationComplete));
                reportengine.DataSources.Add(new ReportDataSource("REPRINT", paymentschedule));

                  
                ////////////////download PDF browser////////////////
                byte[] bytess = ReportViewer1.LocalReport.Render("PDF");
                //display pdf in browser
                Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(bytess);
                Response.End();             

            }
            
            if (Category == "APPLICATIONFORM")
            {
                string TicketId = Request.QueryString["TicketId"];
              
                ApplicationDbContext db = new ApplicationDbContext(); 
                var ApplicationComplete = db.CUSTOMERS.Where(p => p.TransactionID == TicketId).ToList();



                #region Send Email

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                mail.Subject = "Meter Asset Provider Information From PHED";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Bcc.Add("payments@phed.com.ng");
                mail.To.Add(ApplicationComplete.FirstOrDefault().Email);
                string RecipientType = "";
                string SMTPMailServer = "mail.phed.com.ng";


                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
               


                //Attachement

                ReportViewer1.Reset();
                LocalReport reportengines = new LocalReport();

                reportengines.ReportPath = Server.MapPath("~/Reports/CompletedMAPApplicationForm.rdlc");
                reportengines.DataSources.Add(new ReportDataSource("MAPDATA", ApplicationComplete));
                // ReportViewer1.LocalReport.Refresh();

                Warning[] warning;
                string[] streamids;
                string MimeType;
                string encoding;
                string extension;
                byte[] bytes = reportengines.Render("PDF", null, out MimeType, out encoding, out extension, out streamids, out warning);

                MemoryStream s = new MemoryStream(bytes);
                s.Seek(0, SeekOrigin.Begin);
                Attachment a = new Attachment(s, TicketId + "_MeterApplication.pdf");
                 
                string htmlMsgBody = "<html><head></head>";
                htmlMsgBody = htmlMsgBody + "<body>";
                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                htmlMsgBody = htmlMsgBody + "<P>" + "Dear Customer" + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Thank you for Applying for a MAP meter. Kindly see attached the Coompleted Undertaking Form Sign and return to PHED" + "</P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + ApplicationComplete + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Payment Date: " + PaymentDetails.DatePaid + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Amount Paid: " + PaymentDetails.Amount + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "Payment Status: PAID" + " </P>";
                //htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + PaymentDetails.TicketId + " </P>";

                htmlMsgBody = htmlMsgBody + "<br><br>";
                htmlMsgBody = htmlMsgBody + "Thank you,";
                htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                mail.Body = htmlMsgBody;
              
                    mail.Attachments.Add(a);

                    try
                    {
                        MailSMTPserver.Send(mail);
                    }
                    catch (Exception ex)
                    {


                    }
                #endregion


                    //http://localhost:14996/successPage.aspx?status-code=08&merchant-id=300034&transaction-id=5989081269&hash=c29tZWhhc2hlcw==&hash-type=c29tZWhhc2hlczE=&status-message=Transaction%20Successful,%20Approved%20by%20Financial%20Institution&payment-ref=XPS/012007/1250259220000001029567

                ReportViewer1.Reset();
                LocalReport reportengine = ReportViewer1.LocalReport;
                reportengine.ReportPath = Server.MapPath("~/Reports/CompletedMAPApplicationForm.rdlc");
                //reportengine.DataSources.Clear();
                reportengine.DataSources.Add(new ReportDataSource("MAPDATA", ApplicationComplete));
               // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                ////////////////download PDF browser////////////////
                byte[] bytess = ReportViewer1.LocalReport.Render("PDF");
                //display pdf in browser
                Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(bytess);
                Response.End();             

            }


            if (Category == "BRCFORM")
            {
                string TicketId = Request.QueryString["TicketId"];
                ApplicationDbContext db = new ApplicationDbContext();

                if (TicketId != null)
                {
                    var status = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TicketId);

                    if (status != null)
                    {
                        status.MAPApplicationStatus = "GOBRC";
                        db.Entry(status).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    var ApplicationComplete = db.CustomerPaymentInfos.Where(p => p.TransactionID == TicketId).ToList();


                    ReportViewer1.Reset();
                    LocalReport reportengine = ReportViewer1.LocalReport;
                    reportengine.ReportPath = Server.MapPath("~/Reports/BRCApplication.rdlc");
                    //reportengine.DataSources.Clear();
                    reportengine.DataSources.Add(new ReportDataSource("BRCDS", ApplicationComplete));
                    // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                    //////download PDF browser////////////////
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
                    //display pdf in browser
                    Response.AddHeader("Content-Disposition", "inline; filename=" + TicketId + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }

            if (Category == "STAFFPERFORMANCE")
            {

                ApplicationDbContext db = new ApplicationDbContext();

                string Feeder = Request.QueryString["Feeder"];
                string Zone = Request.QueryString["Zone"];
                string _Category = Request.QueryString["Category"];
                string PhaseType = Request.QueryString["PhaseType"];
                string FromDate = Request.QueryString["PhaseType"];
                string ToDate = Request.QueryString["PhaseType"];


                RCDC_DisconnectionList d = new RCDC_DisconnectionList();

                //  var f = db.RCDC_OnboardCustomerss.Where(p => p.Status == "FEEDER" && p.DateCaptured <= Convert.ToDateTime(FromDate) && p.DateCaptured >= Convert.ToDateTime(FromDate)).AsQueryable().ToList();
                var f = db.RCDC_OnboardCustomerss.ToList();

                //.Where(p => p.Status == "FEEDER" ).AsQueryable().ToList();

                if (Feeder != "ALL")
                {
                    f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
                }

                if (Zone != "ALL")
                {
                    f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
                }


                if (PhaseType != "ALL PHASES")
                {
                    f = f.Where(p => p.TypeOfMeterRequired == PhaseType).AsQueryable().ToList();
                }

                if (Category != "ALL")
                {
                    f = f.Where(p => p.OnboardCategory == Category).AsQueryable().ToList();
                }



                ReportViewer1.Reset();
                LocalReport reportengine = ReportViewer1.LocalReport;
                reportengine.ReportPath = Server.MapPath("~/Reports/ExecutiveCustomerOnboardReport.rdlc");
                //reportengine.DataSources.Clear();
                reportengine.DataSources.Add(new ReportDataSource("CustomerCapture", db.RCDC_OnboardCustomerss.OrderBy(p=>p.DateCaptured).ToList()));
                // reportengine.DataSources.Add(new ReportDataSource("Psychomotors", ds.Tables[1]));

                //////download PDF browser////////////////
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF");
                //display pdf in browser
                Response.AddHeader("Content-Disposition", "inline; filename=" + DateTime.Now.ToString() + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(bytes);
                Response.End();
            }







            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/EnergyUse.rdlc");

            //List<NationalGridEnergy> Grid = new List<NationalGridEnergy>();

            //NationalGridEnergy EntryDetails = new NationalGridEnergy();

            //var GetGridDetails = db.NationalGridEnergys.ToList();

            //ReportDataSource datasource = new ReportDataSource("EnergyUse", GetGridDetails);

            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
    }
}