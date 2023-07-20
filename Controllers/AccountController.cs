using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PHEDServe.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Http;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using AllowAnonymousAttribute = System.Web.Mvc.AllowAnonymousAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;

namespace PHEDServe.Controllers
{
    [System.Web.Http.Authorize]
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager) { AllowOnlyAlphanumericUserNames = false };
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult RecoverAccount()
        {
           
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(LoginViewModel model, string returnUrl)
        {
         
            //check if the username even exists

            var StudentLoginID = db.Users.FirstOrDefault(p => p.UserName == model.UserName || p.StaffId == model.UserName);

            if (StudentLoginID != null)
            {
                string Id = StudentLoginID.Id;
                ApplicationUser _user = await UserManager.FindByIdAsync(Id);

                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Your Password and Confirmation do not match. Please try again.");
                    return View("RecoverAccount", model);
                }
                if (_user != null)
                {
                    string Email = _user.Email;

                    //send Email to the Username
                    SendEmailResetLink(_user.StaffName, model.UserName, model.Password, _user.Email, _user.UserName,Id);

                    string Password = model.Password;

                    string input = Email;
                    string pattern = @"(?<=[\w]{3})[\w-\._\+%]*(?=[\w]{2}@)";
                    string result = Regex.Replace(input, pattern, m => new string('*', m.Length));

                    ModelState.AddModelError("", "Your reset Password link has been sent as an Email to " + result + " . Open  your Email to complete the password reset.");
                    return View("RecoverAccount", model); 
                }
            }
            else
            {
                ModelState.AddModelError("", "The Username or StaffId you provided is wrong. Please in put the correct one and try again.");
                return View("RecoverAccount", model);
            }
            return View("RecoverAccount", model);
        }












        private void SendEmailResetLink(string Name, string Username, string Password, string Email, string SchoolID, string Id)
        {
           
            DataSet ds = new DataSet(); 
            DataSet server = new DataSet();
            string msg = "";
            MailMessage mail = new MailMessage();
            string SchoolName = "Port-harcourt Electricity Distribution Company";
            
             
            mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
            mail.Subject = "Password reset Information From PHED";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.Bcc.Add("payments@phed.com.ng");
            mail.To.Add(Email);
            string RecipientType = "";
            
            
            //SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            //{
            //    Credentials = new NetworkCredential(str, str2),
            //    Port = 587,
            //    EnableSsl = true
            //};

            string SMTPMailServer = "smtp.office365.com"; 
            SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
            MailSMTPserver.Port = 587;
            MailSMTPserver.EnableSsl = true;

            MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
             
            //  string HashSHA12 = 
            PasswordHasher dafl = new PasswordHasher();

            string Hash = dafl.HashPassword(Password);


            string URL = "https://insight.phed.com.ng/Account/EffectChange?" + "QnnjipoeDws=" + Id + "&MkieslkdPows=" + Hash + "&Powqwswwre=" + Password;


            mail.To.Add(Email);

            string htmlMsgBody = "<html><head></head>";

            htmlMsgBody = htmlMsgBody + "<body>";

            htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + Name + "</P>";

            htmlMsgBody = htmlMsgBody + " You have requested to change your password access to the portal";


            htmlMsgBody = htmlMsgBody + "<br><br>";

            htmlMsgBody = htmlMsgBody + "<a href=" + URL + ">Please click here</a>" + " to reset your Password for " + SchoolName + ", or copy and paste the code below into your browser to change";

            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + "*******************************************************************";
            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody +  URL ;
            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + "*******************************************************************";
            htmlMsgBody = htmlMsgBody + "<br><br>";

            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + "<br><br>";
            htmlMsgBody = htmlMsgBody + " If you did not request for this password reset, never mind, your credentials are still secure. However Don't Click on the Link. Simply Delete this Message.";

            htmlMsgBody = htmlMsgBody + " <P> " + "Thank you," + " </P> ";

            htmlMsgBody = htmlMsgBody + " <P> " + "PHED-IT Team," + " </P> ";

            htmlMsgBody = htmlMsgBody + " <P> " + SchoolName + "." + " </P> ";

            htmlMsgBody = htmlMsgBody + "<br><br>";
            mail.Body = htmlMsgBody;


            //SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);

            //MailSMTPserver.Credentials = new NetworkCredential(EmailAddress, PasswordEmailAddress);

            try
            {

                MailSMTPserver.Send(mail);


            }

            catch (Exception MailError)
            {
                // MessageBox.Show("Unable to Send your Mail Because " + MailError.Message);
            }


        }


        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult> EffectChange(string QnnjipoeDws, string MkieslkdPows, string Powqwswwre)
        {
            //User = QnnjipoeDws
            //Password = MkieslkdPows
             
            //get the User

            //Change the Password hash
             
            //Login to the Portal

           // var StudentLoginID = db.Users.Where(p => p.Id == QnnjipoeDws);

            ApplicationUser _user = await UserManager.FindByIdAsync(QnnjipoeDws);

            if (_user != null)
            {
               // string Id = StudentLoginID.FirstOrDefault().Id;
               
                if (_user != null)
                {
                    //_user.PasswordHash = MkieslkdPows; 

                    _user.PasswordHash = UserManager.PasswordHasher.HashPassword(Powqwswwre);

                    var result = await UserManager.UpdateAsync(_user);
                     
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindAsync(_user.UserName, Powqwswwre);

                        if (user != null)
                        {
                            await SignInAsync(user, true);
                           
                            if (user.UserCategory == "PHEDSTAFF" && user.LoginSequence == "0")
                            {
                                ModelState.AddModelError("", "Your Password has been reset Successfully");
                                user.LoginSequence= "1";

                                //var __user = await UserManager.FindByIdAsync(user.Id);

                                //// change username and email
                                //user.LoginSequence = "1";

                                // Persiste the changes
                                await UserManager.UpdateAsync(user);
                                db.SaveChanges();
                                return RedirectToAction("PasswordChangeSuccessful", "PHEDServe");
                            }
                             
                            if (user.UserCategory == "PHEDSTAFF" && user.LoginSequence == "1")
                            {
                                ModelState.AddModelError("", "Your Password has been reset Successfully");
                                return RedirectToAction("PasswordChangeSuccessful", "PHEDServe");
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Login", "Account");

        }
        //


















        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> StaffBillVerification(string userToBeAdded,string  userID)
        {

       
            string str = "0";
            string subscriberName = "";
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            string name = base.User.Identity.Name;
            RegViewModel regViewModel = new RegViewModel()
            {
                isLoggedIn = false
            };
            ApplicationUser applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(userToBeAdded);
            if (string.IsNullOrEmpty(name))
            {
                str = this.GenerateSubscriberId().ToString();
                subscriberName = applicationUser.SubscriberName;
            }
            else
            {
          

                if (applicationUser != null)
                {

                    ApplicationUser applicationUser1 = db.Users.FirstOrDefault(p => p.UserName == applicationUser.UserName);

                    if (applicationUser1 != null)
                    {

                        var user = await UserManager.FindByIdAsync(userID);

                        str = applicationUser.SubscriberId;
                        subscriberName = applicationUser.SubscriberName;
                        regViewModel.isLoggedIn = true;
                        ApplicationUser accountName = await this.UserManager.FindByIdAsync(userID);
                        accountName.AccountName = applicationUser.AccountName;
                        accountName.Email = applicationUser.Email;
                        accountName.SubscriberId = str;
                        accountName.SubscriberName = subscriberName;
                        accountName.AccountNo = applicationUser.AccountNumber;
                        accountName.MeterNo = applicationUser.MeterNo;
                        accountName.JobRole = applicationUser.JobRole;
                        accountName.OfficeLocation = applicationUser.OfficeLocation;
                        accountName.PeriodToClearDebt = applicationUser.PeriodToClearDebt;
                        accountName.PhoneNo = applicationUser.PhoneNo;
                        accountName.Remarks = applicationUser.Remarks;
                        accountName.ResolvedBalance = applicationUser.ResolvedBalance;
                        accountName.PeriodToClearDebt = applicationUser.PeriodToClearDebt;
                        accountName.Installment = applicationUser.Installment;
                        accountName.IBC = applicationUser.IBC;
                        accountName.Designation = applicationUser.Designation;
                        accountName.DateRegistered = new DateTime?(DateTime.Now);
                        accountName.CUGLine = applicationUser.CUGLine;
                        accountName.BSC = applicationUser.BSC;
                        accountName.BillReflection = applicationUser.BillReflection;
                        accountName.Arrears = applicationUser.Arrears;
                        accountName.Address = applicationUser.Address;
                        accountName.AccountNumber = applicationUser.AccountNumber;
                        accountName.AccountNo = applicationUser.AccountNumber;
                        accountName.Submission = "SUBMITTED";
                        accountName.MeterMake = applicationUser.MeterMake;
                        accountName.MeterType = applicationUser.MeterType;
                        accountName.AccountType = applicationUser.MeterType;
                        accountName.IsBalanceCorrect = applicationUser.BillReflection;
                        accountName.Address = applicationUser.Address;
                        accountName.IBC = applicationUser.IBC;
                        accountName.BSC = applicationUser.BSC;
                        accountName.Feeder = applicationUser.Feeder;
                        accountName.Zone = applicationUser.Zone;
                        accountName.CIN = applicationUser.CIN;
                        accountName.DTR_Name = applicationUser.DTR_Name;
                        accountName.BillReflection = applicationUser.BillReflection;
                        accountName.UserName = applicationUser.UserName;

                        await this.UserManager.UpdateAsync(accountName);

                    }
                }
            }
            regViewModel.ApplicationUser = new ApplicationUser();
            regViewModel.MaritalStatusTbls = this.db.MaritalStatusTbls.ToList<MaritalStatusTbl>();
            regViewModel.TitleTbls = this.db.TitleTbls.ToList<TitleTbl>();
            regViewModel.SexTbls = this.db.Sexs.ToList<Sex>();
            return base.Json(JsonConvert.SerializeObject(regViewModel), JsonRequestBehavior.AllowGet);
      
        }



      //[HttpPost]
      //  [AllowAnonymous]
      //  public async Task<JsonResult> StaffBillVerification(string userToBeAdded,string  userID)
      //  {
      //      string subscriberId = "0";
      //      string subscriberName = "";

      //      var context = new ApplicationDbContext();
      //      var username = User.Identity.Name;

          
          
          
      //    RegViewModel regmodel = new RegViewModel();
      //      regmodel.isLoggedIn = false;
             
      //      var userDataFromClient = JsonConvert.DeserializeObject<ApplicationUser>(userToBeAdded);

      //      if (!string.IsNullOrEmpty(username))
      //      {
      //          var userloggedIn = context.Users.FirstOrDefault(u => u.UserName == username);
      //          subscriberId = userloggedIn.SubscriberId;
      //          subscriberName = userloggedIn.SubscriberName;
      //          regmodel.isLoggedIn = true; 
      //          //Update the Applicaiton User Data 
      //          var user = await UserManager.FindByIdAsync(userID); 
                
      //          // change username and email 
      //          ////////////////////////////  

      //          user.AccountName =   userDataFromClient.AccountName; 


      //          user.Email = userDataFromClient.UserName; 
      //          user.SubscriberId = subscriberId;
      //          user.SubscriberName = subscriberName;  
      //          user.AccountNo = userDataFromClient.AccountNumber;
      //          user.MeterNo = userDataFromClient.MeterNo; 
      //          user.JobRole = userDataFromClient.JobRole;
      //          user.OfficeLocation = userDataFromClient.OfficeLocation; 
      //          user.PeriodToClearDebt = userDataFromClient.PeriodToClearDebt;
      //          user.PhoneNo = userDataFromClient.PhoneNo;
      //          user.Remarks = userDataFromClient.Remarks;
      //          user.ResolvedBalance = userDataFromClient.ResolvedBalance;
      //          user.PeriodToClearDebt = userDataFromClient.PeriodToClearDebt;
      //          user.Installment = userDataFromClient.Installment;
      //          user.IBC = userDataFromClient.IBC;
      //          user.Designation = userDataFromClient.Designation;
      //          user.DateRegistered = DateTime.Now;
      //          user.CUGLine = userDataFromClient.CUGLine;
      //          user.BSC = userDataFromClient.BSC;
      //          user.BillReflection = userDataFromClient.BillReflection;
      //          user.Arrears = userDataFromClient.Arrears;
      //          user.Address = userDataFromClient.Address;
      //          user.AccountNumber = userDataFromClient.AccountNumber;
      //          user.AccountNo = userDataFromClient.AccountNo;
      //          user.Submission = "SUBMITTED";
      //          user.Address = userDataFromClient.Address;
      //          // user.CreatedBy = CreatedBy;
      //          user.IBC = userDataFromClient.IBC;
      //          user.BSC = userDataFromClient.BSC;
      //          ///////////////////////////

      //          // Persiste the changes
      //          await UserManager.UpdateAsync(user);
      //      }
      //      else
      //      {
      //          subscriberId = GenerateSubscriberId().ToString();
      //          subscriberName = userDataFromClient.SubscriberName;
      //      }

         
           
          
      //      regmodel.ApplicationUser = new ApplicationUser();
      //      regmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
      //      regmodel.TitleTbls = db.TitleTbls.ToList();
      //      regmodel.SexTbls = db.Sexs.ToList();
      //      var result2 = JsonConvert.SerializeObject(regmodel);
      //      return Json(result2, JsonRequestBehavior.AllowGet);
      //  }


         
        [HttpGet]
        [AllowAnonymous]
        public JsonResult loadRegisterModel()
        {

            RegViewModel regViewmodel = new RegViewModel();

            regViewmodel.ApplicationUser = new ApplicationUser();
            regViewmodel.isLoggedIn = false;
            regViewmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
            regViewmodel.TitleTbls = db.TitleTbls.ToList();
            regViewmodel.SexTbls = db.Sexs.ToList();

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            //this.AddToastMessage("Another User is already Logged in", "Please log out the other user or use another browser if you want to use another user account.", ToastType.Warning);

            if (!string.IsNullOrEmpty(username))
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == username);
                regViewmodel.isLoggedIn = true;
            }

            regViewmodel.StaffBillPaymentDataList = new List<StaffBillPaymentData>();


            int year = DateTime.Now.Year;
           string _year = year.ToString();
          string Month =  DateTime.Now.AddMonths(-1).ToString("MMMM");


            decimal NoPaidThisMonth = db.StaffBillPaymentDatas.Count(p=>p.Month == Month && p.Year == _year);
            decimal TotalNoComplete = db.Users.Count(p=>p.UserCategory == "PHEDSTAFF" && p.Submission == "SUBMITTED");
        
            regViewmodel.NumberPaidThisMonth = NoPaidThisMonth.ToString();
            regViewmodel.TotalNumberOfCompletedStaff = TotalNoComplete.ToString();
            regViewmodel.TotalNumberOfStaff = db.StaffBasicDatas.Count(p => p.Staff_Id != null).ToString();
             
            decimal SuccessRate= ( NoPaidThisMonth / TotalNoComplete ) * 100; 
            regViewmodel.PercentageSuccess = SuccessRate.ToString();
            regViewmodel.UplodedStaffList = new List<StaffBasicData>();
            regViewmodel.StaffPrepaid = db.Users.Count(p => p.UserCategory == "PHEDSTAFF" && p.AccountType == "PREPAID").ToString();
            regViewmodel.StaffPostpaid = db.Users.Count(p => p.UserCategory == "PHEDSTAFF" && p.AccountType == "POSTPAID").ToString();

            var result = JsonConvert.SerializeObject(regViewmodel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> AddNewUser(string userToBeAdded, string password)
        {
            string subscriberId = "0";
            string subscriberName = "";

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            //this.AddToastMessage("Another User is already Logged in", "Please log out the other user or use another browser if you want to use another user account.", ToastType.Warning);
            RegViewModel regmodel = new RegViewModel();
            regmodel.isLoggedIn = false;
            var result2 = JsonConvert.SerializeObject(regmodel);

            var userDataFromClient = JsonConvert.DeserializeObject<ApplicationUser>(userToBeAdded);

            if (!string.IsNullOrEmpty(username))
            {
                var userloggedIn = context.Users.FirstOrDefault(u => u.UserName == username);
                subscriberId = userloggedIn.SubscriberId;
                subscriberName = userloggedIn.SubscriberName;
                regmodel.isLoggedIn = true;
            }
            else
            {
                subscriberId = GenerateSubscriberId().ToString();
                subscriberName = userDataFromClient.SubscriberName;
            }




            // UIN

            //


            userDataFromClient.UIN = GenerateUIN().ToString();
            var user = new ApplicationUser();

            user = userDataFromClient;

            user.Email = userDataFromClient.UserName;
            user.StaffName = userDataFromClient.LastName + ", " + userDataFromClient.FirstName;
            user.SubscriberId = subscriberId;
            user.SubscriberName = subscriberName;
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {


                //Check if h has been Registered Before

                #region Register the User to the LinkAccount

                var Check = db.CustomerAccountss.FirstOrDefault(p => p.MeterNo == user.MeterNo);

                if (Check == null)
                {
                    //Add the Customer



                }

                #endregion


                await SignInAsync(user, isPersistent: false);
            }
            else
            {
                AddErrors(result);


                return Json(new { result = result2, error = "This user cannot be added. Please use another Email Address" }, JsonRequestBehavior.AllowGet);

                //Response.StatusCode = (int)HttpStatus. return Json(result2, JsonRequestBehavior.AllowGet);
            }
             
            regmodel.ApplicationUser = new ApplicationUser();

            regmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
            regmodel.TitleTbls = db.TitleTbls.ToList();
            regmodel.SexTbls = db.Sexs.ToList();
            result2 = JsonConvert.SerializeObject(regmodel);
            return Json(new { result = result2, error = "" }, JsonRequestBehavior.AllowGet); 
        }

    [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> AddNewUserAdmin(string userToBeAdded, string password)
        {
            string subscriberId = "0";
            string subscriberName = "";

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            //this.AddToastMessage("Another User is already Logged in", "Please log out the other user or use another browser if you want to use another user account.", ToastType.Warning);
            RegViewModel regmodel = new RegViewModel();
            regmodel.isLoggedIn = false;
            var result2 = JsonConvert.SerializeObject(regmodel);

            var userDataFromClient = JsonConvert.DeserializeObject<ApplicationUser>(userToBeAdded);

            if (!string.IsNullOrEmpty(username))
            {
                var userloggedIn = context.Users.FirstOrDefault(u => u.UserName == username);
                subscriberId = userloggedIn.SubscriberId;
                subscriberName = userloggedIn.SubscriberName;
                regmodel.isLoggedIn = true;
            }
            else
            {
                subscriberId = GenerateSubscriberId().ToString();
                subscriberName = userDataFromClient.SubscriberName;
            }




            // UIN

            //


            userDataFromClient.UIN = GenerateUIN().ToString();
            var user = new ApplicationUser();

            user = userDataFromClient;

            user.Email = userDataFromClient.UserName;
            user.StaffName = userDataFromClient.LastName + ", " + userDataFromClient.FirstName;
            user.SubscriberId = subscriberId;
            user.SubscriberName = subscriberName;
            user.UserCategory = "Admin";
            user.LoginSequence = "1";
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false);
            }
            else
            {
                AddErrors(result);

                  result2 = JsonConvert.SerializeObject(regmodel);


                return Json(new { result = result2, error = "This user cannot be added. Please use another Email Address" }, JsonRequestBehavior.AllowGet);
                //Response.StatusCode = (int)HttpStatus.                return Json(result2, JsonRequestBehavior.AllowGet);
            }




            regmodel.ApplicationUser = new ApplicationUser();

            regmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
            regmodel.TitleTbls = db.TitleTbls.ToList();
            regmodel.SexTbls = db.Sexs.ToList();
              result2 = JsonConvert.SerializeObject(regmodel);
              return Json(new { result = result2, error = "" }, JsonRequestBehavior.AllowGet);
              

           
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> AddNewUserMD(string userToBeAdded, string password, string CreatedBy)
        {
            string subscriberId = "0";
            string subscriberName = "";

            var context = new ApplicationDbContext();
            var username = User.Identity.Name;

            //this.AddToastMessage("Another User is already Logged in", "Please log out the other user or use another browser if you want to use another user account.", ToastType.Warning);
            RegViewModel regmodel = new RegViewModel();
            regmodel.isLoggedIn = false;
            var result2 = JsonConvert.SerializeObject(regmodel);
            var userDataFromClient = JsonConvert.DeserializeObject<ApplicationUser>(userToBeAdded);

            if (!string.IsNullOrEmpty(username))
            {
                var userloggedIn = context.Users.FirstOrDefault(u => u.UserName == username);
                subscriberId = userloggedIn.SubscriberId;
                subscriberName = userloggedIn.SubscriberName;
                regmodel.isLoggedIn = true;
            }
            else
            {
                subscriberId = GenerateSubscriberId().ToString();
                subscriberName = userDataFromClient.SubscriberName;
            }

            userDataFromClient.UIN = GenerateUIN().ToString();
            var user = new ApplicationUser();

            user = userDataFromClient;

            user.Email = userDataFromClient.UserName;
            user.StaffName =   userDataFromClient.FirstName;
            user.SubscriberId = subscriberId;
            user.SubscriberName = subscriberName;
            user.UserCategory = "MDPortal";
            user.LoginSequence = "0";
            user.AccountNo = userDataFromClient.AccountNumber;
            user.MeterNo = userDataFromClient.MeterNo;
            user.PHEDKeyAccountsEmail = userDataFromClient.PHEDKeyAccountsEmail;
            user.PHEDKeyAccountsPhone = userDataFromClient.PHEDKeyAccountsPhone;
            user.ContactPersonEmail = userDataFromClient.ContactPersonEmail;
            user.ContactPersonPhone = userDataFromClient.ContactPersonPhone;
            user.Address = userDataFromClient.Address;
            user.CreatedBy = CreatedBy;
            user.IBC = userDataFromClient.IBC;
            user.BSC = userDataFromClient.BSC;

            user.Zone = userDataFromClient.IBC;
            user.Feeder = userDataFromClient.BSC;
            user.ContactPersonName = userDataFromClient.ContactPersonName;
            user.PHEDKeyAccountsName =  userDataFromClient.PHEDKeyAccountsName;

            //string Passwrod = RandomPassword.Generate(10).ToString();

            string Passwrod = password;


            var result = await UserManager.CreateAsync(user, Passwrod);
            if (result.Succeeded)
            { 
                //await SignInAsync(user, isPersistent: false);
                #region Send Email

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                mail.Subject = "Welcome to the PHED MDPortal";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Bcc.Add("payments@phed.com.ng");
                mail.To.Add(userDataFromClient.UserName);
                string RecipientType = "";
             


                string SMTPMailServer = "smtp.office365.com";
                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                MailSMTPserver.Port = 587;
                MailSMTPserver.EnableSsl = true;
                MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                //  string htmlMsgBody = this.EmailTextBox.Text;
                string htmlMsgBody = "<html><head></head>";
                htmlMsgBody = htmlMsgBody + "<body>";
                //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                htmlMsgBody = htmlMsgBody + "<P>" + "Dear Customer" + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "You have been registered on the PHED MDPortal For Energy Monitoring and Visibility" + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Username: " + userDataFromClient.UserName + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Registration Date: " + DateTime.Now + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Password : " + Passwrod + " </P>";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                htmlMsgBody = htmlMsgBody + "Thank you,";
                htmlMsgBody = htmlMsgBody + " <P> " + "PHED MD Team" + " </P> ";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                mail.Body = htmlMsgBody;

                MailSMTPserver.Send(mail);

                #endregion
            }
            else
            {
                AddErrors(result);

                result2 = JsonConvert.SerializeObject(regmodel);
             
                return Json(new { result = result2, error = "This user cannot be added. Please uses another Email Address" }, JsonRequestBehavior.AllowGet);
             
            }

           
          
            regmodel.ApplicationUser = new ApplicationUser();
            regmodel.MaritalStatusTbls = db.MaritalStatusTbls.ToList();
            regmodel.TitleTbls = db.TitleTbls.ToList();
            regmodel.SexTbls = db.Sexs.ToList();
            result2 = JsonConvert.SerializeObject(regmodel);
            return Json(new { result = result2, error = "This user cannot be added. Please uses another Email Address" }, JsonRequestBehavior.AllowGet);
             
        }


         

        public int GenerateUIN()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int Uin;
            Uin = rand.Next(100000000, 999999999);

            return Uin;
        }

        public int GenerateSubscriberId()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int Uin;
            Uin = rand.Next(100000000, 999999999);

            return Uin;
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string UserName = model.UserName.Replace(" ", "").Trim();
                string password = model.Password.Trim();
                //Check if the USer is a PHED Staff

                StaffBasicData _StaffData = db.StaffBasicDatas.FirstOrDefault(p => (p.Staff_Id.Replace(" ", "").Trim() == UserName || p.Email.Replace(" ", "").Trim() == UserName));

                var user = await UserManager.FindAsync(model.UserName.Replace(" ", "").Trim(), model.Password);
                if (_StaffData != null)
                { 
                    //The Staff Exists
                    user = await UserManager.FindAsync(_StaffData.Email.Replace(" ", "").Trim(), model.Password);
                }
                else
                {
                    user = await UserManager.FindAsync(model.UserName.Replace(" ", "").Trim(), model.Password); 
                }
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    string StatusId = Guid.NewGuid().ToString();
                    GlobalMethodsLib lib = new GlobalMethodsLib();
                    string Name = user.StaffName + " Just Logged in Now at " + DateTime.Now;
                    lib.AuditTrail(user.Id, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");

                    if (user.UserCategory == "PHEDSTAFF" && user.LoginSequence == "0")
                    { 
                        return RedirectToAction("ManageMD", "Account");
                    }

                    if (user.UserCategory == "PHEDSTAFF" && user.LoginSequence == "1")
                    {
                        return RedirectToAction("StaffBillVerification", "PHEDServe");
                    }

                    if (user.UserCategory == "MDPortal" && user.LoginSequence == "0")
                    { 
                        return RedirectToAction("ManageMD", "Account");
                    }

                    if (user.UserCategory == "MDPortal" && user.LoginSequence == "1")
                    { 
                        return RedirectToAction("AMRDashboard", "Dashboard");
                    }

                    if (user.UserCategory == "Admin")
                    {
                        return RedirectToAction("Onboard", "Account");
                    }

                    return RedirectToLocal(returnUrl);
                } 
                else
                {
                    //Check if the StaffID Exists in the Staff Basic Data Table


                   
                      StaffBasicData StaffData = db.StaffBasicDatas.FirstOrDefault(p => (p.Staff_Id.Trim() == model.UserName.Trim() || p.Email.Trim() == model.UserName.Trim()));

                      if (StaffData != null)
                      {
                          //He has not been registered. Please register this Staff on the Table 
                           
                          string Passwrod = "PHED@123";
                          string StaffName = StaffData.Surname.ToUpper() + " " + StaffData.OtherNames.ToUpper();
                          var _user = new ApplicationUser() { UserName = StaffData.Email, StaffName = StaffName, LoginSequence = "0", FirstName = StaffData.OtherNames, StaffId = StaffData.Staff_Id, LastName = StaffData.Surname, UserCategory = "PHEDSTAFF", Email = StaffData.Email, PhoneNo = StaffData.Phone, DateRegistered = DateTime.Now };

                          var Check = UserManager.FindByName(StaffData.Email.Replace(" ", "").Trim());

                          //delete all instances of the Staff Already Existing. Avoid Duplication


                          //var Staff = db.Users.Where(p => p.UserName == StaffData.Email).ToList();

                          //if(Staff.Count >  0)
                          //{
                          //    foreach (var s in Staff)
                          //    {
                          //        //delete all the Occurences

                          //        var dd =  db.Users.Find(s.Id);
                          //        db.Users.Remove(dd);
                          //        db.SaveChanges(); 
                          //    }
                          //}

                          
                          if (Check == null)
                          {
                              var result = await UserManager.CreateAsync(_user, Passwrod);
                              
                              if (result.Succeeded)
                              {
                                  await SignInAsync(_user, isPersistent: false);
                                  string StatusId = Guid.NewGuid().ToString();
                                  GlobalMethodsLib lib = new GlobalMethodsLib();
                                  string Name = _user.StaffName + " was Just registered at " + DateTime.Now;
                                  lib.AuditTrail(_user.StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");

                                  if (_user.UserCategory == "PHEDSTAFF" && _user.LoginSequence == "0")
                                  { 
                                      return RedirectToAction("ManageMD", "Account");
                                  }

                                  if (_user.UserCategory == "PHEDSTAFF" && _user.LoginSequence == "1")
                                  { 
                                      return RedirectToAction("StaffBillVerification", "PHEDServe");
                                  }
                              }
                              else
                              {
                                  AddErrors(result);
                              }

                          }
                          
                      }
                     
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        /// <summary>
        /// Staff Login to the API here the Staff can login with either his Email address or his StaffID
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public RCDCModel LoginAPI(string Username, string Password, string IMEI)
        {

            RCDCModel model = new RCDCModel();
            GlobalMethodsLib lib = new GlobalMethodsLib();
            //Check if the USer is a PHED Staff

            StaffBasicData _StaffData = db.StaffBasicDatas.FirstOrDefault(p => (p.Staff_Id == Username || p.Email == Username));
            string StatusId = Guid.NewGuid().ToString();

            var user =   UserManager.Find(Username, "120940384304580445");
            
            if (_StaffData != null)
            {
                //The Staff Exists
                Username = _StaffData.Email;
                user = UserManager.Find(_StaffData.Email, Password);
            }
            else
            {
                user = UserManager.Find(model.UserName, Password);
            }

            if (user != null)
            {
                //Get Staff Details 
                 
                //Check IMEI
                string IMEIStatus = "NO";
                if (user.IMEILogin == "True" || user.IMEILogin == "true" || user.IMEILogin == "TRUE")
                { 
                    //Is the IMEI the same as the one Passed in?
                    if (user.IMEI2 == IMEI)
                    {
                        IMEIStatus = "YES";
                    }
                    
                    if (user.IMEI1 == IMEI)
                    {
                        IMEIStatus = "YES";
                       
                    }


                    if (IMEIStatus == "NO")
                    { 
                        model.Status = "ERROR";
                        model.Message = "The Device you're loggin into is not associated with this account. Please ensure you're not logging in with another Person's Device, or that the IMEI mapped to your device is Correct.";
                        return model;
                    }
                }
                    
                model.StaffName = user.StaffName;
                model.StaffId = user.StaffId;
                model.UserName = user.UserName;

                RPDGang gang = IsStaffGangMember(user.StaffId);

                if (!gang.Status)
                {
                    model.FeederName = user.Feeder;
                    model.FeederId = user.Feeder;
                    model.Zone = user.Zone;
                }
                else
                {
                    model.GangID = gang.GangID;
                    model.GangName = gang.GangName;
                    model.GangLeader = gang.GangLeader;
                    model.GangLeaderEmail = gang.GangLeaderEmail;
                    model.GangLeaderPhone = gang.GangLeaderPhone;
                    model.Zone = gang.Zone;
                    // model.Feeder = gang;
                    model.FeederName = gang.FeederName;
                    model.FeederId = gang.Feeder;
                  
                }

                var Modules = db.StaffRoles.Where(p => p.StaffId == user.Id).Select(p=>  new {MenuText = p.MenuText, MenuId =  p.MenuId }).ToList();

                if (Modules.Count > 0)
                {
                    model.Modules = Modules;
                }

                else
                {
                    model.Modules = new List<StaffRole>();
                }
                
                model.Email = user.Email;
                model.Id = user.Id;
                model.PhoneNo = user.PhoneNo;
                model.Address = user.Address;
               
                
               




                model.Status = "SUCCESSFUL";
                model.Message = "The Staff is a Staff of PHED and has been Logged in Successfully";
                string Name = user.StaffName + " Just Logged in at " + DateTime.Now;
                lib.AuditTrail(user.Id, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");


                #region Emmanuels API Request
                
                model.Designation =  user.Designation;
                model.Department =  user.DepartmentName;
                //model.DateOfEmployment = user.
               
                model.Location = user.OfficeLocation;
                //model.HighestAcademicQual = user.
                model.PhoneNo =  user.PhoneNo;
                #endregion
                 
                return model; 
            }
            else
            {
                model.Status = "ERROR";
                model.Message = "The Staff username or Password is wrong. Please cross check and try again.";
                return model;
            }

        }


        private RPDGang IsStaffGangMember(string StaffId)
        {

            RPDGang g = new RPDGang();
            g.Status = false;

            //check if the Guy belongs to an RPD Gang

            var check = db.RCDCMembers.FirstOrDefault(p => p.StaffID == StaffId && p.Status == "RPD");

            if (check != null)
            {
                g.Status = true;
                g.GangID = check.GangID;

                //get Gang details
                RCDC_Gang gang = db.RCDC_Gangs.FirstOrDefault(p => p.Gang_ID == check.GangID);

                if (gang != null)
                {
                    g.GangLeader = gang.TeamLeadName;
                    g.GangLeaderEmail = gang.TeamLeadEmail;
                    g.GangLeaderPhone = gang.TeamLeadPhone;
                    g.GangName = gang.GangName;
                    g.Feeder = gang.Feeder;
                    g.FeederName = gang.FeederName;
                    g.Zone = gang.Zone; 
                }
            }
            else
            {

                return g;
            }
            return g;

        }
         

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        } 
        
          [AllowAnonymous]
        public ActionResult RegisterAdmin()
        {
            return View();
        } 
        
        [AllowAnonymous]
        public ActionResult Onboard()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    string StatusId = Guid.NewGuid().ToString();
                    GlobalMethodsLib lib = new GlobalMethodsLib();
                    string Name = user.StaffName + " was Just registered at " + DateTime.Now;
                    lib.AuditTrail(user.StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");

                   return RedirectToAction("Index", "Home");
                     
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



    
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public  async  Task<JsonResult>  RegisterMD(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };

                string Passwrod = RandomPassword.Generate(10).ToString();

              

                var result = await UserManager.CreateAsync(user, Passwrod);

                if (result.Succeeded)
                {
                    string StatusId = Guid.NewGuid().ToString();
                    GlobalMethodsLib lib = new GlobalMethodsLib();
                    string Name = user.StaffName + " was Just registered at " + DateTime.Now;
                    lib.AuditTrail(user.StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");
                    //   return RedirectToAction("Index", "Home"); 
                     
                    #region Send Email

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                    mail.Subject = "Welcome to the PHED MDPortal";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    mail.Bcc.Add("payments@phed.com.ng");
                    mail.To.Add(model.UserName);
                    string RecipientType = "";

                    string SMTPMailServer = "smtp.office365.com";
                    SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                    MailSMTPserver.Port = 587;
                    MailSMTPserver.EnableSsl = true;




                    MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                    //  string htmlMsgBody = this.EmailTextBox.Text;
                    string htmlMsgBody = "<html><head></head>";
                    htmlMsgBody = htmlMsgBody + "<body>";
                    //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                    htmlMsgBody = htmlMsgBody + "<P>" + "Dear Customer" + "</P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "You have been registered on the PHED MDPortal For Energy Monitoring and Visibility" + "</P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Username: " + model.UserName + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Registration Date: " + DateTime.Now + " </P>";
                    htmlMsgBody = htmlMsgBody + " <P> " + "Password : " + Passwrod + " </P>";
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    htmlMsgBody = htmlMsgBody + "Thank you,";
                    htmlMsgBody = htmlMsgBody + " <P> " + "PHED MD Team" + " </P> ";
                    htmlMsgBody = htmlMsgBody + "<br><br>";
                    mail.Body = htmlMsgBody;

                    MailSMTPserver.Send(mail);

                    #endregion
                }
                else
                {
                    AddErrors(result);
                }
            }
            // If we got this'' far, something failed, redisplay form
          //  return View("Onboard");



            var _jsonResult = Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            return _jsonResult;



        }
        
  

        [HttpPost]
        [AllowAnonymous]
       
        public ActionResult VerifyAccount(string AccountNo, string AccountType)
        { 
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST"; 

            
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"phed\"," +
                              "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                              "\"CustomerNumber\":\"" + AccountNo + "\"," +
                              "\"Mobile_Number\":\"" + "08067807821" + "\"," +
                                "\"Mailid\":\"" + "MDPortal@phed.com.ng" + "\"," +
                              "\"CustomerType\":\"" + AccountType.ToUpper() + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            //perform the hash here before going to the Payment Page

                
            string trans_id = RandomPassword.Generate(10).ToString();
            
            StringBuilder hashString = new StringBuilder();
             
            //Save the Customers Details to the Database
             
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

                var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);


                CustomerPaymentInfo Info = new CustomerPaymentInfo();

                Info.CustomerName = objResponse1[0].CONS_NAME;
                string CustomerName = objResponse1[0].CONS_NAME;
                string Address = objResponse1[0].ADDRESS;
                string IBC = objResponse1[0].IBC_NAME;
                string BSC = objResponse1[0].BSC_NAME;
                string TariffCode = objResponse1[0].TARIFFCODE;
                string AccountNumber = objResponse1[0].CUSTOMER_NO;
                string MeterNumber = objResponse1[0].METER_NO;
                ApplicationUser User = new ApplicationUser();
                 
                RegViewModel regViewmodel = new RegViewModel();
                ApplicationUser pp = new ApplicationUser();


                pp.Address = Address;
                pp.IBC = IBC;
                pp.BSC = BSC;
                pp.TariffCode = TariffCode;
                pp.FirstName = CustomerName;
                pp.AccountNumber = AccountNumber;
                pp.PHEDKeyAccountsPhone = "";
                pp.PHEDKeyAccountsEmail = "";
                pp.ContactPersonEmail = "";
                pp.ContactPersonPhone = "";
                pp.MeterNo = MeterNumber;
                regViewmodel.ApplicationUser = pp;
                var results = JsonConvert.SerializeObject(regViewmodel);
                 
                return Json(new { result = results }, JsonRequestBehavior.AllowGet);
                 
                //var jsonResult = Json(new {CustomerName  =CustomerName,Address = Address,IBC = IBC,BSC = BSC,TariffCode = TariffCode,  result = result }, JsonRequestBehavior.AllowGet);
                //return jsonResult;
            }

           // var jsonResult = Json(new { CustomerName = CustomerName, result = result, HashCode = HashCode.ToLower(), SparkBal = SparkBal, amount = AmountPaid, customer_email = EmailAddress, ProductId = ProductId, trans_id = trans_id, ProductDescription = ProductDescription, PublicKey = PublicKey, hashString = hashString.ToString() }, JsonRequestBehavior.AllowGet);
            //return jsonResult;
             
            // If we got this far, something failed, redisplay form
           // return View(model);
        }
         
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Onboard(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false); 
                    string StatusId = Guid.NewGuid().ToString();
                    GlobalMethodsLib lib = new GlobalMethodsLib();
                    string Name = user.StaffName + " was Just registered at " + DateTime.Now;
                    lib.AuditTrail(user.StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "LOGIN");

                   return RedirectToAction("Index", "Home");
                     
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //PATRICK//
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        public ActionResult ManageMD(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }





 // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageMD(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("ManageMD");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                     
                    if (result.Succeeded)
                    {
                        //Update the Login to 1

                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                        // change username and email
                        user.LoginSequence = "1";

                        // Persiste the changes
                        await UserManager.UpdateAsync(user);

                         
                        //ApplicationUser _user = UserManager.FindById(User.Identity.GetUserId());

                        //user = new ApplicationUser
                        //{ 
                        //    //mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm

                        //    LoginSequence = "1", 
                        //};

                        //UserManager.Update(user);
                        //db.Entry(user).State = EntityState.Modified;
                        //db.SaveChanges();



                        if (user.UserCategory == "PHEDSTAFF" && user.LoginSequence == "1")
                        {

                            return RedirectToAction("StaffBillVerification", "PHEDServe");
                        }
                        if (user.UserCategory == "MDPortal" && user.LoginSequence == "1")
                        {

                            #region Register the User to the LinkAccount

                            var Check = db.CustomerAccountss.FirstOrDefault(p => p.MeterNo == user.MeterNo  && p.CustomerAPIKey == user.Id);

                            if (Check == null)
                            {
                                //Add the Customer

                                CustomerAccounts AddCustomer = new CustomerAccounts();
                                AddCustomer.AccountName = user.StaffName;
                                AddCustomer.AccountNumber = user.AccountNumber;
                                AddCustomer.AccountType = "POSTPAID";
                                AddCustomer.BSC = user.BSC;
                                AddCustomer.IBC = user.IBC;
                                AddCustomer.MeterNo = user.MeterNo;
                                AddCustomer.CustomerAPIKey = user.Id;
                                AddCustomer.CustomerEmail = user.Email;
                                AddCustomer.StatusType = "MDPortal";


                                db.CustomerAccountss.Add(AddCustomer);
                                db.SaveChanges();
                            }

                            #endregion

                            return RedirectToAction("AMRDashboard", "Dashboard");
                        }
                         

                        
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());

                        user = new ApplicationUser
                        {
                            LoginSequence = "1"
                        };

                        UserManager.Update(user);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("AMRDashboard", "Dashboard");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


         


        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

       
    }
}