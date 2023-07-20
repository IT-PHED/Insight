using ERDBManager;
using Newtonsoft.Json;
using Oracle.DataAccess.Client;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class PHEDConnectController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Report_TariffChange()
        {
            return View();
        }
        public ActionResult Report_IllegalConnections()
        {
            return View();
        } 

        public ActionResult Report_NewCustomer()
        {
            return View();
        }

        public ActionResult Report_Ireports()
        {
            return View();
        }

        public ActionResult GenerateDisconList()
        {
            return View();
        }
        public ActionResult NewConnectionStaffPerformanceReport()
        {
            return View();
        }

        public ActionResult ReconnectionList()
        {
            return View();
        }


        public ActionResult Report_AccountSeparations()
        {
            return View();
        }

        public ActionResult Report_AccountActivation()
        {
            return View();
        }
         
        public ActionResult NewConnectionApprovalFeeder()
        {
            return View();
        }
        public ActionResult IreportersReport()
        {
            return View();
        }
        public ActionResult BillDistributionReport()
        {
            return View();
        }


        public ActionResult NewConnectionApprovalIAD()
        {
            return View();
        }
        public ActionResult NewConnectionApprovalZone()
        {
            return View();
        }
        public ActionResult NewConnectionApprovalApproved()
        {
            return View();
        }

        public ActionResult DisconnectionList()
        {
            return View();
        }
        public ActionResult Addgang()
        {
            return View();
        }
        public ActionResult AddGangMember()
        {
            return View();
        }
        public ActionResult RCDCGang()
        {
            return View();
        }

        public ActionResult RCDCMember()
        {
            return View();
        }
         
        public ActionResult IADApproval()
        {
            return View();
        }
         
        [HttpGet]
        public JsonResult SearchStaff(string searchText)
        {
            db = new ApplicationDbContext();

            var array = db.Users.Where(p => p.FirstName.ToLower().Contains(searchText.ToLower()) || p.Email.ToLower().Contains(searchText.ToLower()) || p.LastName.ToLower().Contains(searchText.ToLower())).Select(u => new { StaffId = u.StaffId, StaffName = u.StaffName, Email = u.Email, UserName = u.UserName, Id = u.Id }).ToArray();
             
            return base.Json(array, 0);
        }
         

        [HttpPost]
        public ActionResult AddGangToList(string GangModel, string CreatedBy)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();

            var s = JsonConvert.DeserializeObject<RCDC_Gang>(GangModel);

            RCDC_Gang gang = new RCDC_Gang();

            string error = "";

            try
            {
                string d = Guid.NewGuid().ToString();

                s.Gang_ID = d;
                s.GangID = d;
                s.AddedBy = CreatedBy;
                s.DateAdded = DateTime.Now;

                db.RCDC_Gangs.Add(s);



                db.SaveChanges();

            }
            catch (Exception ex)
            {

                error = "unable to save because " + ex.Message;

            }

            myViewModel.GangList = db.RCDC_Gangs.ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = error }, JsonRequestBehavior.AllowGet);
            return actionResult;

        }




        [HttpPost]
        public ActionResult AddGangMemberToGang(string GangModel, string CreatedBy)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();

            var s = JsonConvert.DeserializeObject<RCDCMember>(GangModel);



            string error = "";

            try
            {
                string d = Guid.NewGuid().ToString();

                s.MemberID = d;
                s.Status = "RPD";
                s.AddedBy = CreatedBy;
                s.DateAdded = DateTime.Now;
                db.RCDCMembers.Add(s);
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                error = "unable to save because " + ex.Message;
            }

            myViewModel.RCDCMemberList = db.RCDCMembers.ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = error }, JsonRequestBehavior.AllowGet);
            return actionResult;

        }




        [HttpPost]
        public ActionResult ApproveAccountForOpeningfeeder(string Zone, string Feeder, string AccountType, string Category, string CreatedBy, string TicketId, string Status)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";

            //approve 

            var dd = db.RCDC_OnboardCustomerss.FirstOrDefault(p => p.TicketNo == TicketId);

            if (dd != null)
            {
                dd.Status = "ZONE";
                dd.ApprovedBy_Feeder = CreatedBy;
                dd.DateApproved_Feeder = DateTime.Now;
                db.Entry(dd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }







            RCDC_DisconnectionList d = new RCDC_DisconnectionList();

            var f = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == Category).AsQueryable().ToList();


            if (Feeder != "ALL")
            {
                f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }


            if (AccountType != "ALL ACCOUNTS")
            {
                f = f.Where(p => p.AccountType == AccountType).AsQueryable().ToList();
            }


            myViewModel.DisconnectionList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [HttpPost]
        public ActionResult ApproveAccountForOpeningZone(string Zone, string Feeder, string AccountType, string Category, string CreatedBy, string TicketId, string Status)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";

            //approve 

            var dd = db.RCDC_OnboardCustomerss.FirstOrDefault(p => p.TicketNo == TicketId);

            if (dd != null)
            {
                dd.Status = "IAD";
                dd.ApprovedBy_Feeder = CreatedBy;
                dd.DateApproved_Feeder = DateTime.Now;
                db.Entry(dd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }







            RCDC_DisconnectionList d = new RCDC_DisconnectionList();

            var f = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == Category).AsQueryable().ToList();


            if (Feeder != "ALL")
            {
                f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }


            if (AccountType != "ALL ACCOUNTS")
            {
                f = f.Where(p => p.AccountType == AccountType).AsQueryable().ToList();
            }


            myViewModel.DisconnectionList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }





        [HttpPost]
        public ActionResult ViewBillDistributionDetails(string Zone, string Feeder, string AccountType, string Category, string CreatedBy, string TicketId, string Status, string FromDate, string ToDate)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";

            //approve 


            RCDC_DisconnectionList d = new RCDC_DisconnectionList();

            //var f = db.RCDC_BillDistributions.Where(p => p.Status == Status).AsQueryable().ToList();

            //if (Feeder != "ALL")
            //{
            //    f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            //}

            //if (Zone != "ALL")
            //{
            //    f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            //}


            //if (AccountType != "ALL PHASES")
            //{
            //    f = f.Where(p => p.TypeOfMeterRequired == AccountType).AsQueryable().ToList();
            //}

            //if (Category != "ALL")
            //{
            //    f = f.Where(p => p.OnboardCategory == Category).AsQueryable().ToList();
            //}

            //myViewModel.NewCustomerOnboardList = f;

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }












        public ActionResult ApproveAccountForOpening(string Zone, string Feeder, string AccountType, string Category, string CreatedBy, string TicketId, string Status)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";

            //approve 

            var dd = db.RCDC_OnboardCustomerss.FirstOrDefault(p => p.TicketNo == TicketId);

            if (dd != null)
            {
                if (Status == "FEEDER")
                {

                    dd.Status = "ZONE";
                    dd.ApprovedBy_Feeder = CreatedBy;
                    dd.DateApproved_Feeder = DateTime.Now;
                    db.Entry(dd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (Status == "ZONE")
                {

                    dd.Status = "IAD";
                    dd.ApprovedBy_Zone = CreatedBy;
                    dd.DateApproved_Zone = DateTime.Now;
                    db.Entry(dd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                if (Status == "IAD")
                {

                    dd.Status = "APPROVED";
                    dd.ApprovedBy_IAD = CreatedBy;
                    dd.DateApproved_IAD = DateTime.Now;
                    db.Entry(dd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }


            RCDC_DisconnectionList d = new RCDC_DisconnectionList();

            var f = db.RCDC_OnboardCustomerss.Where(p => p.Status == Status).AsQueryable().ToList();

            if (Feeder != "ALL")
            {
                f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }


            if (AccountType != "ALL PHASES")
            {
                f = f.Where(p => p.TypeOfMeterRequired == AccountType).AsQueryable().ToList();
            }

            if (Category != "ALL")
            {
                f = f.Where(p => p.OnboardCategory == Category).AsQueryable().ToList();
            }

            myViewModel.NewCustomerOnboardList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [HttpPost]
        public ActionResult DisconnectedCustomers(string Zone, string Feeder, string AccountType, string Category, string CreatedBy)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";


            RCDC_DisconnectionList d = new RCDC_DisconnectionList();

            var f = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == Category).AsQueryable().ToList();


            if (Feeder != "ALL")
            {
                f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }


            if (AccountType != "ALL ACCOUNTS")
            {
                f = f.Where(p => p.AccountType == AccountType).AsQueryable().ToList();
            }


            myViewModel.DisconnectionList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }




        [HttpPost]
        public ActionResult NewConnectionApprovalFeeder(string Zone, string Feeder, string AccountType, string Category, string CreatedBy, string Status, string FromDate, string ToDate)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";
            RCDC_DisconnectionList d = new RCDC_DisconnectionList();



            DateTime _FromDate = Convert.ToDateTime(FromDate); 
            DateTime _ToDate = Convert.ToDateTime(ToDate);
             
            var f = db.RCDC_OnboardCustomerss.Where(p => p.Status == Status &&  (p.DateCaptured >= _FromDate && p.DateCaptured <= _ToDate)).AsQueryable().ToList();

            if (Feeder != "ALL")
            {
                f = f.Where(p => p.FeederId == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }
              
            if (AccountType != "ALL PHASES")
            {
                f = f.Where(p => p.TypeOfMeterRequired == AccountType).AsQueryable().ToList();
            }

            if (Category != "ALL")
            {
                f = f.Where(p => p.OnboardCategory == Category).AsQueryable().ToList();
            }

            myViewModel.NewCustomerOnboardList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

         
        [HttpPost]
        public ActionResult IreportReports(string Zone, string Feeder, string Category, string CreatedBy, string Status, string FromDate, string ToDate, string Subcategory, string SubcategoryName)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            var _results = "";

            string str = "";
            RCDC_DisconnectionList d = new RCDC_DisconnectionList();
             
            DateTime _FromDate = Convert.ToDateTime(FromDate); 
            DateTime _ToDate = Convert.ToDateTime(ToDate);
             
            var f = db.RCDC_Ireports.Where(p =>   (p.DateReported >= _FromDate && p.DateReported <= _ToDate)).AsQueryable().ToList();

            if (Feeder != "ALL")
            {
                f = f.Where(p => p.Feeder_Id == Feeder).AsQueryable().ToList();
            }

            if (Zone != "ALL")
            {
                f = f.Where(p => p.Zone == Zone).AsQueryable().ToList();
            }
 
            if (Category != "ALL")
            {
                f = f.Where(p => p.ReportCategory == Category).AsQueryable().ToList();
            }
              
            if (SubcategoryName != "ALL")
            {
                f = f.Where(p => p.ReportSubCategory == Subcategory).AsQueryable().ToList();
            }

             
           myViewModel.IReportList = f;
            //db.RCDCDisconnectionLists.Where(p => p.FeederId == Feeder && p.Zone == Zone && p.AccountType == AccountType && p.DisconStatus == Category).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }


        [HttpPost]
        public ActionResult GetLossOfRevenueData(string DisconId, string AccountNo)
        {

            ActionResult actionResult;


            AppViewModels myViewModel = new AppViewModels();

            myViewModel.UploadedDocumentList = db.DOCUMENTSs.Where(p => p.REFERENCE_CODE == DisconId).ToList();
            myViewModel.LossOfRevenueList = db.RCDCLoadApplicancess.Where(p => p.DisconId == DisconId).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }


        [HttpPost]
        public ActionResult GetImageDocumentsView(string TicketId)
        {
            ActionResult actionResult;
            AppViewModels myViewModel = new AppViewModels();
            myViewModel.UploadedDocumentList = db.DOCUMENTSs.Where(p => p.REFERENCE_CODE == TicketId).ToList();
            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }


        [HttpPost]
        public ActionResult RPDApprovalLOR(string DisconId, string AccountNo, string Availability, string DurationOfInfraction, string Load, string ApprovalStatus, string ApprovalComment, string StaffId)
        {


            ActionResult actionResult;
            PHEDConnectAPIController pop = new PHEDConnectAPIController();

            AppViewModels myViewModel = new AppViewModels();

            var LORUpdate = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == DisconId);

            if (LORUpdate != null)
            {
                LORUpdate.RPDLossOfRevenueAvailabilty = Availability;
                LORUpdate.RPDLossOfRevenueInfractionDuration = Convert.ToInt32(DurationOfInfraction);
                LORUpdate.RPDLossOfRevenueAmount = pop.GenerateLossOfRevenueAmount(Availability, Load, "", Convert.ToInt32(DurationOfInfraction), LORUpdate.AccountType, AccountNo, "2", LORUpdate.TariffAmount, LORUpdate.Phase).ToString();
                LORUpdate.RPDCalculatedLoad = Load;
                LORUpdate.RPDApproval = ApprovalStatus;
                LORUpdate.RPDApprovalComments = ApprovalComment;
                LORUpdate.RPDApprovalDate = DateTime.Now;
                LORUpdate.RPDApprovedBy = StaffId;
                //--------------------------------
                db.Entry(LORUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }


            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }



        [HttpPost]
        public ActionResult IADApprovalLOR(string DisconId, string AccountNo, string ApprovalStatus, string ApprovalComment, string StaffId)
        {
            ActionResult actionResult;
            PHEDConnectAPIController pop = new PHEDConnectAPIController();
            AppViewModels myViewModel = new AppViewModels();

            var LORUpdate = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == DisconId);

            if (LORUpdate != null)
            {
                LORUpdate.IADApproval = ApprovalStatus;
                LORUpdate.IADApprovalComments = ApprovalComment;
                LORUpdate.IADApprovalDate = DateTime.Now;
                LORUpdate.IADApprovedBy = StaffId;
                db.Entry(LORUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = "" }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }


         

        [HttpPost]
        public ActionResult ProcessAction(string Zone, string Feeder, string ActionDate, string DebtProfile, string AccountType, string Category, string CreatedBy)
        {
            DataSet dataSet1 = new DataSet();

            ActionResult actionResult;

            string Amount = "50000";

            GlobalMethodsLib DTRExec = new GlobalMethodsLib();
             
            Amount = DebtProfile.Replace(" and Above", "");

            Amount = Amount.Replace(",", "").Trim();

            string str = "";
            string str2 = "";

            if (Amount == "Any Amount Owed")
            {
                Amount = "1000";
            }


          RCDC_DisconnectionList  d = new RCDC_DisconnectionList();
            AppViewModels myViewModel = new AppViewModels();
            string Message = "";

            DateTime DateGenerated = DateTime.Now;

            string SystemDate = Convert.ToDateTime(ActionDate).ToString("MM-dd-yyyy");

            if (AccountType == "PREPAID ONLY")
            { 
                //prepaid
                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                WHERE t2.cons_type='PREPAID'   and t2.con_consumerstatus!='TD'  and   TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";

            }

            else if (AccountType == "POSTPAID ONLY")
            {
                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                                WHERE t2.cons_type='POSTPAID'   and TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";
            }


            else if (AccountType == "ALL ACCOUNTS")
            {


                //POSTPAID
                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                    'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                    '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                    t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                    t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                    t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                    t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                    TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                    TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                    nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                    t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                    LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                    LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                    and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                    LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                    AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                    left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                    WHERE t2.cons_type='POSTPAID'   and TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";

                //prepaid
                str2 = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                WHERE t2.cons_type='PREPAID'  and t2.con_consumerstatus!='TD'  and   TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";

            }
            
            string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString();
            //conn.Open(); 
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();


            if (Zone != "ALL")
            {
                str = str + " and t3.ZONE = '" + Zone + "'";

                if (str2 != "")
                {
                    str2 = str2 + " and t3.ZONE = '" + Zone + "'";
                }
            }

            if (Feeder != "ALL")
            {
                str = str + " and t3.FEEDER33ID = '" + Feeder + "'";

                if (str2 != "")
                {
                    str2 = str2 + " and t3.FEEDER33ID = '" + Feeder + "'";
                }
            }
            OracleCommand cmd = new OracleCommand(str);

            cmd.CommandType = CommandType.Text;

            cmd.Connection = conn;

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
            {
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataSet1);
            }

            if (dataSet1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                {
                    d = new RCDC_DisconnectionList();

                    //Save the List to the Database here
                    string AccountNo = dataSet1.Tables[0].Rows[i]["customer_no"].ToString();
                    string AccountName = dataSet1.Tables[0].Rows[i]["cons_name"].ToString();
                    string Address = dataSet1.Tables[0].Rows[i]["Address"].ToString();
                    string CIN =     dataSet1.Tables[0].Rows[i]["CIN"].ToString();
                    string DTR_Name = dataSet1.Tables[0].Rows[i]["DTR_Name"].ToString();
                    string DTR_Code = dataSet1.Tables[0].Rows[i]["DTRID"].ToString();
                    string FeederID = dataSet1.Tables[0].Rows[i]["FeederId"].ToString();
                    string MeterNo = dataSet1.Tables[0].Rows[i]["meter_no"].ToString();
                    string FeederName = dataSet1.Tables[0].Rows[i]["FEEDER33Name"].ToString();
                    string ConsumerStatus = dataSet1.Tables[0].Rows[i]["ConsumerStatus"].ToString();
                    string ZoneName = dataSet1.Tables[0].Rows[i]["Zone"].ToString();
                    string DTR_Exec_Name = "";
                    string DTR_Exec_Email = "";
                    string DTR_Exec_Phone = "";

                    DTRExecutives DTRData = DTRExec.GetDTRExecutiveDetails(DTR_Code);

                    if (DTRData.Status)
                    {
                        DTR_Exec_Name = DTRData.DTRExecutiveName;
                        DTR_Exec_Email = DTRData.DTRExecutiveEmail;
                        DTR_Exec_Phone = DTRData.DTRExecutivePhone;
                    }
                    else
                    {
                        DTR_Exec_Name = "N/A";
                        DTR_Exec_Email = "N/A";
                        DTR_Exec_Phone = "N/A";
                    }

                    string Arrears = "";
                    string LastPaymentDate = dataSet1.Tables[0].Rows[i]["LastPaymentDate"].ToString();
                    string LastPaymentAmount = dataSet1.Tables[0].Rows[i]["LastPaymentAmount"].ToString();
                    string DisconReason = "DISCONNECT";
                    string Month = ActionDate;
                    string Year = ActionDate;
                    string _AccountType = dataSet1.Tables[0].Rows[i]["AccountType"].ToString();


                    if (_AccountType == "PREPAID")
                    {
                        Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding_PPM"].ToString();

                    }
                    else
                    {
                        Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding"].ToString();
                    }


                    string TariffCode = dataSet1.Tables[0].Rows[i]["TariffCode"].ToString();
                    string GeneratedBy = "";


                    var checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == AccountNo && p.Arrears == Arrears && p.DisconStatus == "DISCONNECT").ToList();
                    if (checkAm.Count > 0)
                    {

                        continue;
                    }


                    d.AccountNo = AccountNo;
                    d.AccountName = AccountName;
                    d.AccountType = _AccountType;
                    d.Address = Address;
                    d.Arrears = Arrears;
                    d.ConsumerStatus = ConsumerStatus;
                    d.AvgConsumption = dataSet1.Tables[0].Rows[i]["AverageConsumption"].ToString();
                    d.CIN = CIN;
                    d.DateGenerated = DateGenerated ;
                    d.DisconID = Guid.NewGuid().ToString();
                    d.DisconStatus = DisconReason;
                    d.DTR_Exec_Email = DTR_Exec_Email;
                    d.DTR_Exec_Name = DTR_Exec_Name;
                    d.DTR_Exec_Phone = DTR_Exec_Phone;
                    d.DTR_Id = DTR_Code;
                    d.DTR_Name = DTR_Name;
                    d.DTRCode = DTR_Code;
                    d.FeederId = FeederID;
                    d.FeederName = FeederName;
                    d.LastPayDate = LastPaymentDate;
                    d.AmountPaid = LastPaymentAmount;
                    d.GeneratedBy = CreatedBy;

                    d.Zone = Zone;
                    string Phase = "";
                    if (dataSet1.Tables[0].Rows[i]["Phase"].ToString() == "4")
                    {

                        d.Phase = "1";
                    }
                    else
                    {

                        d.Phase = dataSet1.Tables[0].Rows[i]["Phase"].ToString();
                    }

                    d.Tariff = TariffCode;

                    db.RCDCDisconnectionLists.Add(d);
                    db.SaveChanges();
                }
            }

            dataSet1 = new DataSet();

            if(str2 != null)
            {


                cmd.CommandType = CommandType.Text;

                cmd.Connection = conn;

                using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                {
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet1);
                }


                if (dataSet1.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                    {
                        d = new RCDC_DisconnectionList();

                        //Save the List to the Database here
                        string AccountNo = dataSet1.Tables[0].Rows[i]["customer_no"].ToString();
                        string AccountName = dataSet1.Tables[0].Rows[i]["cons_name"].ToString();
                        string Address = dataSet1.Tables[0].Rows[i]["Address"].ToString();
                        string CIN = dataSet1.Tables[0].Rows[i]["CIN"].ToString();
                        string DTR_Name = dataSet1.Tables[0].Rows[i]["DTR_Name"].ToString();
                        string DTR_Code = dataSet1.Tables[0].Rows[i]["DTRID"].ToString();
                        string FeederID = dataSet1.Tables[0].Rows[i]["FeederId"].ToString();
                        string MeterNo = dataSet1.Tables[0].Rows[i]["meter_no"].ToString();
                        string FeederName = dataSet1.Tables[0].Rows[i]["FEEDER33Name"].ToString();
                        string ConsumerStatus = dataSet1.Tables[0].Rows[i]["ConsumerStatus"].ToString();
                        string ZoneName = dataSet1.Tables[0].Rows[i]["Zone"].ToString();
                        string DTR_Exec_Name = "";
                        string DTR_Exec_Email = "";
                        string DTR_Exec_Phone = "";

                        DTRExecutives DTRData = DTRExec.GetDTRExecutiveDetails(DTR_Code);

                        if (DTRData.Status)
                        {
                            DTR_Exec_Name = DTRData.DTRExecutiveName;
                            DTR_Exec_Email = DTRData.DTRExecutiveEmail;
                            DTR_Exec_Phone = DTRData.DTRExecutivePhone;
                        }
                        else
                        {
                            DTR_Exec_Name = "N/A";
                            DTR_Exec_Email = "N/A";
                            DTR_Exec_Phone = "N/A";
                        }

                        string Arrears = "";
                        string LastPaymentDate = dataSet1.Tables[0].Rows[i]["LastPaymentDate"].ToString();
                        string LastPaymentAmount = dataSet1.Tables[0].Rows[i]["LastPaymentAmount"].ToString();
                        string DisconReason = "DISCONNECT";
                        string Month = ActionDate;
                        string Year = ActionDate;
                        string _AccountType = dataSet1.Tables[0].Rows[i]["AccountType"].ToString();


                        if (_AccountType == "PREPAID")
                        {
                            Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding_PPM"].ToString();

                        }
                        else
                        {
                            Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding"].ToString();
                        }


                        string TariffCode = dataSet1.Tables[0].Rows[i]["TariffCode"].ToString();
                        string GeneratedBy = "";


                        var checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == AccountNo && p.Arrears == Arrears && p.DisconStatus == "DISCONNECT").ToList();
                        if (checkAm.Count > 0)
                        {

                            continue;
                        }


                        d.AccountNo = AccountNo;
                        d.AccountName = AccountName;
                        d.AccountType = _AccountType;
                        d.Address = Address;
                        d.Arrears = Arrears;
                        d.ConsumerStatus = ConsumerStatus;
                        d.AvgConsumption = dataSet1.Tables[0].Rows[i]["AverageConsumption"].ToString();
                        d.CIN = CIN;
                        d.DateGenerated =  DateGenerated;
                        d.DisconID = Guid.NewGuid().ToString();
                        d.DisconStatus = DisconReason;
                        d.DTR_Exec_Email = DTR_Exec_Email;
                        d.DTR_Exec_Name = DTR_Exec_Name;
                        d.DTR_Exec_Phone = DTR_Exec_Phone;
                        d.DTR_Id = DTR_Code;
                        d.DTR_Name = DTR_Name;
                        d.DTRCode = DTR_Code;
                        d.FeederId = FeederID;
                        d.FeederName = FeederName;
                        d.LastPayDate = LastPaymentDate;
                        d.AmountPaid = LastPaymentAmount;
                        d.GeneratedBy = CreatedBy;

                        d.Zone = Zone;
                        string Phase = "";
                        if (dataSet1.Tables[0].Rows[i]["Phase"].ToString() == "4")
                        {

                            d.Phase = "1";
                        }
                        else
                        {

                            d.Phase = dataSet1.Tables[0].Rows[i]["Phase"].ToString();
                        }

                        d.Tariff = TariffCode;

                        db.RCDCDisconnectionLists.Add(d);
                        db.SaveChanges();
                    }


                }



            }

            myViewModel.DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == "DISCONNECT" && p.DateGenerated == DateGenerated && p.Zone ==  Zone && p.FeederId ==  Feeder).ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = Message }, JsonRequestBehavior.AllowGet);
            return actionResult;

        }




        [HttpPost]
        public ActionResult ProcessAction1(string Zone, string Feeder, string ActionDate, string DebtProfile, string AccountType, string Category, string CreatedBy)
        {
            DataSet dataSet = new DataSet();
            ActionResult actionResult;
            string Amount = "50000";
            GlobalMethodsLib DTRExec = new GlobalMethodsLib();

            Amount = DebtProfile.Replace(" and Above", "");

            Amount = Amount.Replace(",", "").Trim();

            if (Amount == "Any Amount Owed")
            {
                Amount = "1000";
            }

            AppViewModels myViewModel = new AppViewModels();

            string Message = "";

            DBManager dBManager = new DBManager(DataProvider.Oracle)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
            };


            dBManager.Open();
            string SystemDate = Convert.ToDateTime(ActionDate).ToString("MM-dd-yyyy");


            var _results = "";

            string str = ""; 
            
            string str2 = "";

            if (AccountType == "PREPAID ONLY")
            {

         




                //POSTPAID
//                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
//                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
//                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
//                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
//                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
//                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
//                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
//                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
//                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
//                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
//                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
//                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
//                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
//                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
//                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
//                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
//                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
//                WHERE t2.cons_type='POSTPAID'   and TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";



                //prepaid
                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                WHERE t2.cons_type='PREPAID'   and t2.con_consumerstatus!='TD'  and   TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";




            }

            else if (AccountType == "POSTPAID ONLY")
            {
                str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                                WHERE t2.cons_type='POSTPAID'   and TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";
            }


            else if (AccountType == "ALL ACCOUNTS")
            {


                //POSTPAID
                    str = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                    'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                    '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                    t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                    t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                    t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                    t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                    TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                    TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                    nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                    t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                    LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                    LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                    and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                    LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                    AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                    left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                    WHERE t2.cons_type='POSTPAID'   and TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";



                //prepaid
                str2 = @"SELECT t3.Zone,            t3.Feeder33Name,            t3.FEEDER33ID as FeederID, t3.DTR_Name,t3.DTRID,  
                'DTR Executive ' as DTR_Executive_Name,            'DTR_Executive@phed.com.ng' as DTR_Executive_Email,            
                '0809-DTR-PHONE' as DTR_Executive_PhoneNo,            t2.cons_name,           
                t2.cons_addr1 || ' ' || cons_addr2 AS address, t2.con_consumerstatus as ConsumerStatus,                    
                t2.cons_meterno AS meter_no,            t2.cons_acc AS customer_no,            t2.con_mobileno AS mob_no,      
                t2.CON_CIN AS CIN,  t2.con_phase as Phase,  
                t2.cons_type as AccountType,  t5.AVGCONSUMPTION as AverageConsumption,     
                TO_CHAR(to_number(nvl(t1.slabec1,0) ) + to_number(nvl(t1.ed,0))+to_number(nvl(t1.arr_ec_df,0))+to_number(nvl(t1.arr_ed_df,0) )) AS total_Outstanding,      
                TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT  WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) )) AS total_Outstanding_PPM,  
                nvl(t2.CONS_CATEGORY,' ') as TariffCode,            t4.paymentdatetime as LastPaymentDate,       
                t4.Amount as LastPaymentAmount            FROM ENSERV.tbl_consmast t2        
                LEFT JOIN ENSERV.tbl_enumerationfeedermapping t3 ON t2.cons_acc = t3.AccountNo      
                LEFT JOIN ENSERV.tbl_allpayment t4 on t2.cons_acc = t4.consumer_no      
                and (t4.paymentdatetime = (SELECT MAX(paymentdatetime) FROM ENSERV.tbl_allpayment WHERE consumer_no = t4.consumer_no))   
                LEFT JOIN ENSERV.tbl_billinfo t1 ON t1.consumerno = t2.cons_acc         
                AND (t1.billmonth = (SELECT MAX(billmonth) FROM ENSERV.tbl_billinfo WHERE consumerno = t1.consumerno))         
                left join ENSERV.TBL_AVGCONSUMPTION t5 on t2.cons_acc = t5.consumerno           
                WHERE t2.cons_type='PREPAID'  and t2.con_consumerstatus!='TD'  and   TO_CHAR(to_number(NVL((SELECT NVL(SUM(AMOUNTTOSETTLED),0) FROM ENSERV.TBL_INCIDENT WHERE TBL_INCIDENT.CONSUMERNO=T2.CONS_ACC ),0) ))  >= to_binary_float('" + Amount + "') and t4.paymentdatetime <= to_date('" + SystemDate + "', 'mm-dd-yyyy')";

            }

            dBManager.Open();
            try
            {
                if (Zone != "ALL")
                {
                    str = str + " and t3.ZONE = '" + Zone + "'";

                    if (str2 != "")
                    {
                        str2 = str2 + " and t3.ZONE = '" + Zone + "'";
                    }
                }

                if (Feeder != "ALL")
                {
                    str = str + " and t3.FEEDER33ID = '" + Feeder + "'";

                    if (str2 != "")
                    {
                        str2 = str2 + " and t3.FEEDER33ID = '" + Feeder + "'";
                    }
                }

                DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);

                dBManager.Close();

                int count = dataSet1.Tables[0].Rows.Count;

                RCDC_DisconnectionList d = new RCDC_DisconnectionList();


                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        d = new RCDC_DisconnectionList();

                        //Save the List to the Database here
                        string AccountNo = dataSet1.Tables[0].Rows[i]["customer_no"].ToString();
                        string AccountName = dataSet1.Tables[0].Rows[i]["cons_name"].ToString();
                        string Address = dataSet1.Tables[0].Rows[i]["Address"].ToString();
                        string CIN = dataSet1.Tables[0].Rows[i]["CIN"].ToString();
                        string DTR_Name = dataSet1.Tables[0].Rows[i]["DTR_Name"].ToString();
                        string DTR_Code = dataSet1.Tables[0].Rows[i]["DTRID"].ToString();
                        string FeederID = dataSet1.Tables[0].Rows[i]["FeederId"].ToString();
                        string MeterNo = dataSet1.Tables[0].Rows[i]["meter_no"].ToString();
                        string FeederName = dataSet1.Tables[0].Rows[i]["FEEDER33Name"].ToString();
                        string ConsumerStatus = dataSet1.Tables[0].Rows[i]["ConsumerStatus"].ToString();
                        string ZoneName = dataSet1.Tables[0].Rows[i]["Zone"].ToString();
                        string DTR_Exec_Name = "";
                        string DTR_Exec_Email = "";
                        string DTR_Exec_Phone = "";

                        DTRExecutives DTRData = DTRExec.GetDTRExecutiveDetails(DTR_Code);

                        if (DTRData.Status)
                        {
                            DTR_Exec_Name = DTRData.DTRExecutiveName;
                            DTR_Exec_Email = DTRData.DTRExecutiveEmail;
                            DTR_Exec_Phone = DTRData.DTRExecutivePhone;
                        }
                        else
                        {
                            DTR_Exec_Name = "N/A";
                            DTR_Exec_Email = "N/A";
                            DTR_Exec_Phone = "N/A";
                        }

                        string Arrears = "";  
                        string LastPaymentDate = dataSet1.Tables[0].Rows[i]["LastPaymentDate"].ToString();
                        string LastPaymentAmount = dataSet1.Tables[0].Rows[i]["LastPaymentAmount"].ToString();
                        string DisconReason = "DISCONNECT";
                        string Month = ActionDate;
                        string Year = ActionDate;
                        string _AccountType = dataSet1.Tables[0].Rows[i]["AccountType"].ToString();


                        if (_AccountType == "PREPAID")
                        {
                            Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding_PPM"].ToString();

                        }
                        else
                        {
                            Arrears = ""; dataSet1.Tables[0].Rows[i]["total_Outstanding"].ToString();
                        }


                        string TariffCode = dataSet1.Tables[0].Rows[i]["TariffCode"].ToString();
                        string GeneratedBy = "";


                        var checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == AccountNo && p.Arrears == Arrears && p.DisconStatus == "DISCONNECT").ToList();
                        if (checkAm.Count > 0)
                        {

                            continue;
                        }


                        d.AccountNo = AccountNo;
                        d.AccountName = AccountName;
                        d.AccountType = _AccountType;
                        d.Address = Address;
                        d.Arrears = Arrears;
                        d.ConsumerStatus = ConsumerStatus;
                        d.AvgConsumption = dataSet1.Tables[0].Rows[i]["AverageConsumption"].ToString();
                        d.CIN = CIN;
                        d.DateGenerated = DateTime.Now;
                        d.DisconID = Guid.NewGuid().ToString();
                        d.DisconStatus = DisconReason;
                        d.DTR_Exec_Email = DTR_Exec_Email;
                        d.DTR_Exec_Name = DTR_Exec_Name;
                        d.DTR_Exec_Phone = DTR_Exec_Phone;
                        d.DTR_Id = DTR_Code;
                        d.DTR_Name = DTR_Name;
                        d.DTRCode = DTR_Code;
                        d.FeederId = FeederID;
                        d.FeederName = FeederName;
                        d.LastPayDate = LastPaymentDate;
                        d.AmountPaid = LastPaymentAmount;
                        d.GeneratedBy = CreatedBy;

                        d.Zone = Zone;
                        string Phase = "";
                        if (dataSet1.Tables[0].Rows[i]["Phase"].ToString() == "4")
                        {

                            d.Phase = "1";
                        }
                        else
                        {

                            d.Phase = dataSet1.Tables[0].Rows[i]["Phase"].ToString();
                        }

                        d.Tariff = TariffCode;

                        db.RCDCDisconnectionLists.Add(d);
                        db.SaveChanges();

                    }

                }


                if (str2 != "")
                {

                    dataSet1 = new DataSet();
                    dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str2);
                    int count2 = dataSet1.Tables[0].Rows.Count;

                    if (count2 > 0)
                    {

                        for (int i = 0; i < count2; i++)
                        {
                            d = new RCDC_DisconnectionList();

                            //Save the List to the Database here
                            string AccountNo = dataSet1.Tables[0].Rows[i]["customer_no"].ToString();
                            string AccountName = dataSet1.Tables[0].Rows[i]["cons_name"].ToString();
                            string Address = dataSet1.Tables[0].Rows[i]["Address"].ToString();
                            string CIN = dataSet1.Tables[0].Rows[i]["CIN"].ToString();
                            string DTR_Name = dataSet1.Tables[0].Rows[i]["DTR_Name"].ToString();
                            string DTR_Code = dataSet1.Tables[0].Rows[i]["DTRID"].ToString();
                            string FeederID = dataSet1.Tables[0].Rows[i]["FeederId"].ToString();
                            string MeterNo = dataSet1.Tables[0].Rows[i]["meter_no"].ToString();
                            string FeederName = dataSet1.Tables[0].Rows[i]["FEEDER33Name"].ToString();
                            string ZoneName = dataSet1.Tables[0].Rows[i]["Zone"].ToString();
                            string DTR_Exec_Name = dataSet1.Tables[0].Rows[i]["DTR_Executive_Name"].ToString();
                            string DTR_Exec_Email = dataSet1.Tables[0].Rows[i]["DTR_Executive_Email"].ToString();

                            string ConsumerStatus = dataSet1.Tables[0].Rows[i]["ConsumerStatus"].ToString();
                            string DTR_Exec_Phone = dataSet1.Tables[0].Rows[i]["DTR_Executive_PhoneNo"].ToString();
                            string Arrears = dataSet1.Tables[0].Rows[i]["total_Outstanding"].ToString();
                            string LastPaymentDate = dataSet1.Tables[0].Rows[i]["LastPaymentDate"].ToString();
                            string LastPaymentAmount = dataSet1.Tables[0].Rows[i]["LastPaymentAmount"].ToString();
                            //string AccountType = dataSet1.Tables[0].Rows[i]["cons_type"].ToString();
                            string DisconReason = "DISCONNECT";
                            string Month = ActionDate;
                            string Year = ActionDate;
                            string _AccountType = dataSet1.Tables[0].Rows[i]["AccountType"].ToString();
                            string TariffCode = dataSet1.Tables[0].Rows[i]["TariffCode"].ToString();
                            string GeneratedBy = "";


                            var checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == AccountNo && p.Arrears == Arrears && p.DisconStatus == "DISCONNECT").ToList();

                            if (checkAm.Count > 0)
                            {

                                continue;
                            }


                            d.AccountNo = AccountNo;
                            d.AccountName = AccountName;
                            d.AccountType = _AccountType;
                            d.Address = Address;
                            d.Arrears = Arrears;
                            d.AvgConsumption = dataSet1.Tables[0].Rows[i]["AverageConsumption"].ToString();
                            d.CIN = CIN;
                            d.DateGenerated = DateTime.Now;
                            d.DisconID = Guid.NewGuid().ToString();
                            d.DisconStatus = DisconReason; d.ConsumerStatus = ConsumerStatus;
                            d.DTR_Exec_Email = DTR_Exec_Email;
                            d.DTR_Exec_Name = DTR_Exec_Name;
                            d.DTR_Exec_Phone = DTR_Exec_Phone;
                            d.DTR_Id = DTR_Code;
                            d.DTR_Name = DTR_Name;
                            d.DTRCode = DTR_Code;
                            d.FeederId = FeederID;
                            d.FeederName = FeederName;
                            d.LastPayDate = LastPaymentDate;
                            d.AmountPaid = LastPaymentAmount;
                            d.GeneratedBy = CreatedBy;

                            d.Zone = Zone;
                            string Phase = "";
                            if (dataSet1.Tables[0].Rows[i]["Phase"].ToString() == "4")
                            {

                                d.Phase = "1";
                            }
                            else
                            {

                                d.Phase = dataSet1.Tables[0].Rows[i]["Phase"].ToString();
                            }

                            d.Tariff = TariffCode;

                            db.RCDCDisconnectionLists.Add(d);
                            db.SaveChanges();

                        }
                    }
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dBManager.Close();
                Message = string.Format("Could not retrieve Payment history because " + exception1.Message + ". Please try again Thank you");
            }


            myViewModel.DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconStatus == "DISCONNECT").ToList();

            var result = JsonConvert.SerializeObject(myViewModel);
            actionResult = base.Json(new { result = result, error = Message }, JsonRequestBehavior.AllowGet);
            return actionResult;

        }






        [HttpGet]
        public JsonResult LoadIndexPage()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();


            //Select All Zone and Feeder from the mapping table


            myViewModel.BSCList = db.BSCs.ToList();


            var List = db.IBCs.ToList();

            List.Insert(0, new IBC { IBCId = "ALL", IBCName = "ALL ZONES" });


            myViewModel.IBCList = List;


            PHEDServe.Controllers.MAPController.RCDCStatData da = new PHEDServe.Controllers.MAPController.RCDCStatData();

            decimal Disconnected = db.RCDCDisconnectionLists.Count(p => p.DisconStatus == "DISCONNECTED");
            decimal Connected = db.RCDCDisconnectionLists.Count(p => p.DisconStatus == "RECONNECTED");

            da.DisconnectedCustomers = Disconnected.ToString();
            da.ReconnectedCustomers = Connected.ToString();
            da.PendingForCustomer = db.RCDCDisconnectionLists.Count(p => p.DisconStatus == "RECONNECT").ToString();
            da.DisconnectedPOSTPAID = db.RCDCDisconnectionLists.Count(p => p.DisconStatus == "DISCONNECTED" && p.AccountType == "POSTPAID").ToString();
            da.DisconnectedPREPAID = db.RCDCDisconnectionLists.Count(p => p.DisconStatus == "DISCONNECTED" && p.AccountType == "PREPAID").ToString();


            da.IllegalConnections = db.RCDC_OnboardCustomerss.Count(p => p.OnboardCategory == "DISCONNECTED" && p.OnboardCategory == "ILLEGAL CONNECTION").ToString();

            da.NewCustomers = db.RCDC_OnboardCustomerss.Count(p => p.OnboardCategory == "DISCONNECTED" && p.OnboardCategory == "ILLEGAL CONNECTION").ToString();




            try
            {
                decimal Percent = ((Connected / Disconnected) * 100 / 1);
                da.PercentageSuccess = Percent.ToString() + "%";
            }
            catch (Exception ex)
            {
                da.PercentageSuccess = "0%";
            }
            myViewModel.UploadedDocumentList = new List<DOCUMENTS>();

            myViewModel.RCDCStatData = da;


            myViewModel.GangModel = new RCDC_Gang();
            myViewModel.GangMember = new RCDCMember();


            myViewModel.GangList = db.RCDC_Gangs.ToList();
            myViewModel.NewCustomerOnboardList = new List<RCDC_OnboardCustomers>();
            myViewModel.RCDCMemberList = db.RCDCMembers.ToList();
            myViewModel.DisconnectionList = new List<RCDC_DisconnectionList>();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult LoadIndexPages(string Status)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();

            myViewModel.BSCList = db.BSCs.ToList();

            var List = db.IBCs.ToList();

            List.Insert(0, new IBC { IBCId = "ALL", IBCName = "ALL ZONES" });

            myViewModel.IBCList = List;
            PHEDServe.Controllers.MAPController.RCDCStatData da = new PHEDServe.Controllers.MAPController.RCDCStatData();

            da.IllegalConnections = db.RCDC_OnboardCustomerss.Count(p => p.Status == Status && p.OnboardCategory == "ILLEGAL CONNECTION").ToString();

            da.NewCustomers = db.RCDC_OnboardCustomerss.Count(p => p.Status == Status && p.OnboardCategory == "NEW CUSTOMER").ToString();


            da.Separation = db.RCDC_OnboardCustomerss.Count(p => p.Status == Status && p.OnboardCategory == "ACCOUNT SEPERATION").ToString();



            decimal Disconnected = 0;
            decimal Connected = 0;


            if (Status == "FEEDER")
            {
                Disconnected = db.RCDC_OnboardCustomerss.Count(p => p.Status == "FEEDER");
                Connected = db.RCDC_OnboardCustomerss.Count(p => p.Status != "FEEDER");
                da.Approved = Connected.ToString();

                da.TotalPending = Disconnected.ToString();


            }

            if (Status == "ZONE")
            {
                Disconnected = db.RCDC_OnboardCustomerss.Count(p => p.Status == "ZONE");
                Connected = db.RCDC_OnboardCustomerss.Count(p => p.Status != "ZONE" && p.Status != "IAD");
                da.Approved = Connected.ToString();

                da.TotalPending = Disconnected.ToString();


            }
            if (Status == "IAD")
            {
                Disconnected = db.RCDC_OnboardCustomerss.Count(p => p.Status == "IAD");
                Connected = db.RCDC_OnboardCustomerss.Count(p => p.Status == "APPROVED");
                da.Approved = Connected.ToString(); 
                da.TotalPending = Disconnected.ToString();

            }
            try
            {
                decimal Percent = ((Connected / Disconnected) * 100 / 1);
                da.PercentageSuccess = Percent.ToString() + "%";
            }
            catch (Exception ex)
            {
                da.PercentageSuccess = "0%";
            }


            da.Approved = db.RCDC_OnboardCustomerss.Count(p => p.Status == Status && p.OnboardCategory == "TARIFF CHANGE").ToString();
       
            myViewModel.UploadedDocumentList = new List<DOCUMENTS>();

            myViewModel.RCDCStatData = da;

            myViewModel.RCDC_ReportCategory = db.RCDC_ReportCategorys.ToList();

            myViewModel.RCDC_ReportSubCategory = db.RCDC_ReportSubCategorys.ToList();
            myViewModel.IReportList = new List<RCDC_Ireport>();
            myViewModel.GangModel = new RCDC_Gang();
            myViewModel.GangMember = new RCDCMember();
            myViewModel.BillDistributionList = new List<RCDC_BillDistribution>();
            myViewModel.GangList = db.RCDC_Gangs.ToList();
            myViewModel.NewCustomerOnboardList = new List<RCDC_OnboardCustomers>();
            myViewModel.RCDCMemberList = db.RCDCMembers.ToList();
            myViewModel.DisconnectionList = new List<RCDC_DisconnectionList>();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
         
        public ActionResult DisconnectionFee()
        {
            return View();
        }
        //Reports 

        public ActionResult DisconnectedCustomers()
        {
            return View();
        }
        public ActionResult ReconnectedCustomers()
        {
            return View();
        }

        public ActionResult SettlementPlan()
        {
            return View();
        }
        public ActionResult RCDCDashboard()
        {
            return View();
        }
    }
}