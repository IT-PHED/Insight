using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class MDPortalController : Controller
    {
        //
        // GET: /MDPortal/
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult ViewReceipt()
        {
            return View();
        } 
        public ActionResult AccountProfile()
        {
            return View();
        }
        public ActionResult ViewBills()
        {
            return View();
        }
        public ActionResult Tickets()
        {
            return View();
        }

        public ActionResult AddAccounts()
        {
            return View();
        }
        public ActionResult MakeComplaints()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult SubmitTicket(string ComplaintCategory, string ComplaintSubCategory, string ComplaintAccount,
            string ComplaintStatus, string ComplaintTitle,
                string ComplaintDescription,
                string LoginCompanyId,
                string LoadType,
                string ParentAccountId)
        { 
            AppViewModels myViewModel = new AppViewModels();
            CustomerTickets Ticket = new CustomerTickets();
            ApplicationDbContext db = new ApplicationDbContext(); 

            Ticket.CategoryID = ComplaintCategory;
            Ticket.CategoryName = ComplaintCategory;
            Ticket.CompanyID = LoginCompanyId;
            Ticket.Status = ComplaintStatus;
            Ticket.SubCategoryID = ComplaintSubCategory;
            Ticket.TicketDate = DateTime.Now.ToString();
            Ticket.TicketDescription = ComplaintDescription;
            Ticket.TicketID = RandomPassword.Generate(8);


            db.CustomerTicketss.Add(Ticket);
            db.SaveChanges();

            var Key = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId).ToList();
            TicketStatus T = new TicketStatus();

            myViewModel.ClosedTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "CLOSED").ToList().Count.ToString();
            myViewModel.ActiveTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "ACTIVE").ToList().Count.ToString(); 
            myViewModel.CompletedTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "RESOLVED").ToList().Count.ToString(); 
            myViewModel.TicketList = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId).ToList();
            myViewModel.CustomerTickets = Key;

             
            myViewModel.ApplicationUser = new ApplicationUser();
            var _result = JsonConvert.SerializeObject(myViewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;
        }

         


        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddAccount(string MeterNo, string BSC, string IBC, string StatusType, string AccountName,
               string AccountType,
               string LinkedAccountDesc,
                string AccountNo,
                string ParentAccountId)
        {
            CustomerAccounts AddAccount = new CustomerAccounts();

            ApplicationDbContext db = new ApplicationDbContext();
            AddAccount.AccountName = LinkedAccountDesc;
            AddAccount.AccountNumber = AccountNo;
            AddAccount.AccountType = AccountType;
            AddAccount.BSC = BSC;
            AddAccount.IBC = IBC;
            AddAccount.MeterNo = MeterNo;
            AddAccount.StatusType = StatusType;
            AddAccount.CustomerAPIKey = ParentAccountId;
            db.CustomerAccountss.Add(AddAccount);
            db.SaveChanges();

            AppViewModels myViewModel = new AppViewModels();
            myViewModel.ParentAccountList = db.Users.Where(p => p.CreatedBy == ParentAccountId).ToList();
            myViewModel.ApplicationUser = new ApplicationUser();
            var _result = JsonConvert.SerializeObject(myViewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult;

        }


        [HttpPost]
        [AllowAnonymous]

        public ActionResult AddAccount2(string MeterNo, string BSC, string IBC, string StatusType, string AccountName,
                string AccountType,
                string LinkedAccountDesc,
                string AccountNo,
                string ParentAccountId)
        {
            CustomerAccounts AddAccount = new CustomerAccounts();

            ApplicationDbContext db = new ApplicationDbContext();
            AddAccount.AccountName = LinkedAccountDesc;
            AddAccount.AccountNumber = AccountNo;
            AddAccount.AccountType = AccountType;
            AddAccount.BSC = BSC;
            AddAccount.IBC = IBC;
            AddAccount.MeterNo = MeterNo;
            AddAccount.StatusType = StatusType;
            AddAccount.CustomerAPIKey = ParentAccountId;
            db.CustomerAccountss.Add(AddAccount);
            db.SaveChanges(); 
            AppViewModels myViewModel = new AppViewModels();
            myViewModel.ParentAccountList = db.Users.Where(p => p.CreatedBy == ParentAccountId).ToList();
            myViewModel.ApplicationUser = new ApplicationUser();
            //viewModel.DirecPaymentList = db.DirectPaymentss.Where(p => p.Status == "NOT CLAIMED").ToList();

            var _result = JsonConvert.SerializeObject(myViewModel);
            var _jsonResult = Json(new { result = _result, error = "" }, JsonRequestBehavior.AllowGet);
            _jsonResult.MaxJsonLength = int.MaxValue;
            return _jsonResult; 
        }
         



        public ActionResult LoadMDPortalContents(string CreatedBy)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            myViewModel.ParentAccountList = db.Users.Where(p => p.CreatedBy == CreatedBy).ToList(); 
           // myViewModel.LinkedAccountList = db.CustomerAccountss.Where(p => p.CustomerAPIKey == LoginCompanyId).ToList(); 
            myViewModel.LinkedAccountList = db.CustomerAccountss.Where(p => p.CustomerAPIKey == CreatedBy).ToList();
            //new List<CustomerAccounts>();
             myViewModel.ApplicationUser = new ApplicationUser();
            var result = JsonConvert.SerializeObject(myViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}