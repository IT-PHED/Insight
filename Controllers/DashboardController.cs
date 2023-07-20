using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult AMRReports()
        {
            return View();
        }


        public ActionResult HistoricData()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> LoadMeterReadingValues(string MeterNo, string AccountNo, string LoginCompanyId, string LoadType)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            AMRReportsModel AMR = new AMRReportsModel();
            DataSet _ds = new DataSet();



            DataSet ds = SQLData("MeterReading", AccountNo, MeterNo);
            var myEnumerable = ds.Tables[0].AsEnumerable();
            List<DailyMeterReading> myClassList =
                (from item in myEnumerable
                 select new DailyMeterReading
                 {
                     Date_M = item.Field<DateTime>("Date_M"),
                     QtyValue = item.Field<Double>("QtyValue"),
                     SerialNumber = item.Field<string>("SerialNumber"),
                     PlantNumber = item.Field<string>("PlantNumber"),
                 }).ToList();


            ds = SQLData("MonthlyConsumption", AccountNo, MeterNo);
            myEnumerable = ds.Tables[0].AsEnumerable();
            List<DailyMeterReading> myClassList2 =
            (from item in myEnumerable
             select new DailyMeterReading
             {
                 Days = item.Field<string>("Days"),
                 QtyValue = item.Field<Double>("QtyValue"),
                 SerialNumber = item.Field<string>("SerialNumber"),
                 PlantNumber = item.Field<string>("PlantNumber"),
             }).ToList();

            MeterNo = "NOMETER";
            string NumberofMonths = "12";
            //string JSONData = "{\r\n  \"customerName\": \"PRINCESS MEDI-CLINICS NIG LTD\",\r\n  \"accountno\": \"810005178501\",\r\n  \"ibc\": \"Paradise City North\",\r\n  \"bsc\": \"Ogoja\",\r\n  \"meterNo\": \"215240700\",\r\n  \"totalOutstanding\": \"12563633.87\",\r\n  \"monthlyBillData\": [\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"January\",\r\n      \"year\": \"2020\",\r\n      \"amountBilled\": \"1271018.77\",\r\n      \"dateBilled\": \"01-01-2020 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"25307\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"December\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1160076.29\",\r\n      \"dateBilled\": \"01-12-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"1171604.45\",\r\n      \"datePaid\": \"06-12-2019 9:43:42 AM\",\r\n      \"consumption\": \"23648\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"November\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1107635.42\",\r\n      \"dateBilled\": \"01-11-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"22579\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"October\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1145555.71\",\r\n      \"dateBilled\": \"01-10-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"909154.85\",\r\n      \"datePaid\": \"08-10-2019 1:06:31 PM\",\r\n      \"consumption\": \"23352\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"September\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1029440.16\",\r\n      \"dateBilled\": \"01-09-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"20985\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"August\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"949527.94\",\r\n      \"dateBilled\": \"01-08-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"19356\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"July\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1018942.18\",\r\n      \"dateBilled\": \"01-07-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"20771\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"June\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1055636.06\",\r\n      \"dateBilled\": \"01-06-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"21519\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"May\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1226350.94\",\r\n      \"dateBilled\": \"01-05-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"24999\",\r\n      \"totalOutStanding\": \"0\"\r\n    }\r\n  ]\r\n}";

            //Set header content
            string struritest = string.Format("http://10.10.25.31:222/Api/GetAmountBilled");
            WebRequest requestIbject = WebRequest.Create(struritest);
            requestIbject.Method = "POST";
            requestIbject.ContentType = "application/json";
            // load post request body content into the postdata var as string
            string postData = "{\"accountNo\": \"" + AccountNo + "\",\"numberOfMonths\": \"" + NumberofMonths + "\",  \"meterNo\": \"NOMETER\"}";
            //opem streamwriter  for requestObject
            using (var streamWriter = new StreamWriter(requestIbject.GetRequestStream()))
            {
                // write the Request body data into the requestIbject amd close the streamwriter
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();

                //call the Api abd get the httpresponse of the requestIbject
                var httpresponse = (HttpWebResponse)requestIbject.GetResponse();

                // Open the a streamReader for httpresponse
                using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    // Read to the end of the resposne data
                    var billDetailResult = streamReader.ReadToEnd();
                    // TODO list

                    MDCustomerData datalist = JsonConvert.DeserializeObject<MDCustomerData>(billDetailResult);
                    if (LoadType == "PageLoad")
                    {
                        myViewModel.APIStatus = "PAGELOAD";
                    }
                    else
                        if (LoadType == "LinkLoad")
                        {
                            myViewModel.APIStatus = "LINKLOAD";
                        }
                        else
                        {
                            myViewModel.APIStatus = "NODATA";
                        }

                    myViewModel.DailyReadingData = myClassList;
                    myViewModel.MDCustomerDataLists = datalist;
                    myViewModel.MonthlyUsage = myClassList2;
                }
            }


           // TicketStatus T = new TicketStatus();

           

            myViewModel.ClosedTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "CLOSED").ToList().Count.ToString();
            myViewModel.ActiveTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "ACTIVE").ToList().Count.ToString();

            myViewModel.CompletedTickets = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId && p.Status == "RESOLVED").ToList().Count.ToString();

            //myViewModel.TicketStatus = T;

            myViewModel.TicketList = db.CustomerTicketss.Where(p => p.CompanyID == LoginCompanyId).ToList();
            myViewModel.ParentAccountProfile = db.Users.FirstOrDefault(p => p.Id == LoginCompanyId);
            myViewModel.ComplaintCategoryList = db.ComplaintCategorys.ToList();
            myViewModel.ComplaintSubCategoryList = db.ComplaintsSubCategorys.ToList();

            myViewModel.LinkedAccountList = db.CustomerAccountss.Where(p => p.CustomerAPIKey == LoginCompanyId).ToList();

             
            var _result = JsonConvert.SerializeObject(myViewModel);
            return Json(_result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> LoadMeterReadingValuesUAT(string MeterNo, string AccountNo, string LoginCompanyId, string LoadType)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AppViewModels myViewModel = new AppViewModels();
            AMRReportsModel AMR = new AMRReportsModel();
            DataSet _ds = new DataSet();

            DataSet ds = SQLData("MeterReading", AccountNo, MeterNo);
            var myEnumerable = ds.Tables[0].AsEnumerable();
            List<DailyMeterReading> myClassList =
                (from item in myEnumerable
                 select new DailyMeterReading
                 {
                     Date_M = item.Field<DateTime>("Date_M"),
                     QtyValue = item.Field<Double>("QtyValue"),
                     SerialNumber = item.Field<string>("SerialNumber"),
                     PlantNumber = item.Field<string>("PlantNumber"),
                 }).ToList();



            ds = SQLData("MonthlyConsumption", AccountNo, MeterNo);
            myEnumerable = ds.Tables[0].AsEnumerable();
            List<DailyMeterReading> myClassList2 =
            (from item in myEnumerable
             select new DailyMeterReading
             {
                 Days = item.Field<string>("Days"),
                 QtyValue = item.Field<Double>("QtyValue"),
                 SerialNumber = item.Field<string>("SerialNumber"),
                 PlantNumber = item.Field<string>("PlantNumber"),
             }).ToList();

            MeterNo = "NOMETER";
            string NumberofMonths = "12";

            string JSONData = "{\r\n  \"customerName\": \"PRINCESS MEDI-CLINICS NIG LTD\",\r\n  \"accountno\": \"810005178501\",\r\n  \"ibc\": \"Paradise City North\",\r\n  \"bsc\": \"Ogoja\",\r\n  \"meterNo\": \"215240700\",\r\n  \"totalOutstanding\": \"12563633.87\",\r\n  \"monthlyBillData\": [\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"January\",\r\n      \"year\": \"2020\",\r\n      \"amountBilled\": \"1271018.77\",\r\n      \"dateBilled\": \"01-01-2020 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"25307\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"December\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1160076.29\",\r\n      \"dateBilled\": \"01-12-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"1171604.45\",\r\n      \"datePaid\": \"06-12-2019 9:43:42 AM\",\r\n      \"consumption\": \"23648\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"November\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1107635.42\",\r\n      \"dateBilled\": \"01-11-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"22579\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"October\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1145555.71\",\r\n      \"dateBilled\": \"01-10-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"909154.85\",\r\n      \"datePaid\": \"08-10-2019 1:06:31 PM\",\r\n      \"consumption\": \"23352\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"September\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1029440.16\",\r\n      \"dateBilled\": \"01-09-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"20985\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"August\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"949527.94\",\r\n      \"dateBilled\": \"01-08-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"19356\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"July\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1018942.18\",\r\n      \"dateBilled\": \"01-07-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"20771\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"June\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1055636.06\",\r\n      \"dateBilled\": \"01-06-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"21519\",\r\n      \"totalOutStanding\": \"0\"\r\n    },\r\n    {\r\n      \"accountNo\": \"810005178501\",\r\n      \"month\": \"May\",\r\n      \"year\": \"2019\",\r\n      \"amountBilled\": \"1226350.94\",\r\n      \"dateBilled\": \"01-05-2019 12:00:00 AM\",\r\n      \"amountPaid\": \"\",\r\n      \"datePaid\": \"\",\r\n      \"consumption\": \"24999\",\r\n      \"totalOutStanding\": \"0\"\r\n    }\r\n  ]\r\n}";

            //Set header content
            //string struritest = string.Format("http://10.10.25.31:222/Api/GetAmountBilled");
            // WebRequest requestIbject = WebRequest.Create(struritest);
            //requestIbject.Method = "POST";
            //requestIbject.ContentType = "application/json";
            // load post request body content into the postdata var as string
            //string postData = "{\"accountNo\": \"" + AccountNo + "\",\"numberOfMonths\": \"" + NumberofMonths + "\",  \"meterNo\": \"NOMETER\"}";
            //opem streamwriter  for requestObject
            // write the Request body data into the requestIbject amd close the streamwriter
            //call the Api abd get the httpresponse of the requestIbject
            //var httpresponse = (HttpWebResponse)requestIbject.GetResponse();
            // Open the a streamReader for httpresponse
            // Read to the end of the resposne data
            // TODO list

            MDCustomerData datalist = JsonConvert.DeserializeObject<MDCustomerData>(JSONData);
            if (LoadType == "PageLoad")
            {
                myViewModel.APIStatus = "PAGELOAD";
            }
            else
                if (LoadType == "LinkLoad")
                {
                    myViewModel.APIStatus = "LINKLOAD";
                }
                else
                {
                    myViewModel.APIStatus = "NODATA";
                }


            TicketStatus T = new TicketStatus();
            T.ActiveTickets = db.CustomerTicketss.Count(p => p.CompanyID == LoginCompanyId && p.Status == "ACTIVE").ToString();
            T.ClosedTickets = db.CustomerTicketss.Count(p => p.CompanyID == LoginCompanyId && p.Status == "CLOSED").ToString();
            T.CompletedTickets = db.CustomerTicketss.Count(p => p.CompanyID == LoginCompanyId && p.Status == "RESOLVED").ToString();

            myViewModel.TicketStatus = T;

            myViewModel.DailyReadingData = myClassList;
            myViewModel.MDCustomerDataLists = datalist;
            myViewModel.MonthlyUsage = myClassList2;
            myViewModel.ParentAccountProfile = db.Users.FirstOrDefault(p => p.Id == LoginCompanyId);
            myViewModel.ComplaintCategoryList = db.ComplaintCategorys.ToList();
            myViewModel.ComplaintSubCategoryList = db.ComplaintsSubCategorys.ToList();

            myViewModel.LinkedAccountList = db.CustomerAccountss.Where(p => p.CustomerAPIKey == LoginCompanyId).ToList();
            var _result = JsonConvert.SerializeObject(myViewModel);
            return Json(_result, JsonRequestBehavior.AllowGet);
        }
         










        public DataSet SQLData(string ReportType, string AccountNo,string MeterNo)
        {
            string SQLData = ""; string Number = "";

            string  FromDate, ToDate;

            string con = "Data Source=10.10.25.13;Initial Catalog=dbmultidrive;Persist Security Info=True;User ID=sa;Password=P@SSW0rd2018";

            DataSet _ds = new DataSet();

            if (ReportType == "TotalCaptured")
            {
                SQLData = @"SELECT        tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(tblDevices.SerialNumber) AS TOTAL_CAPTURED
                            FROM            tblDevices INNER JOIN
                         tblMeterPoints ON tblDevices.DeviceId = tblMeterPoints.DeviceId INNER JOIN
                         tblLnkMeterPointGroups ON tblMeterPoints.MeterPointId = tblLnkMeterPointGroups.MeterPointId INNER JOIN
                         tblMeterPointGroups ON tblLnkMeterPointGroups.MeterGroupId = tblMeterPointGroups.MeterGroupId INNER JOIN
                         tblSites ON tblDevices.SiteId = tblSites.SiteId
GROUP BY tblMeterPointGroups.GroupName";


                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();

            }


            if (ReportType == "TotalRead")
            {
                SQLData = @"SELECT        dbo.tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(distinct dbo.tblTOU.METERPOINTID) AS TOTAL_CAPTURED
                            FROM           dbo.tblTOU  LEFT JOIN
                            dbo.tblLnkMeterPointGroups ON dbo.tblLnkMeterPointGroups.MeterPointId=dbo.tblTOU.MeterPointId
                            left join  dbo.tblMeterPointGroups   ON dbo.tblLnkMeterPointGroups.MeterGroupId = dbo.tblMeterPointGroups.MeterGroupid 
						 
                            GROUP BY tblMeterPointGroups.GroupName";

                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }


            if (ReportType == "TotalCurrentlyRead")
            {
                SQLData = @"SELECT        dbo.tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(distinct dbo.tblTOU.METERPOINTID) AS TOTAL_CAPTURED
                FROM   dbo.tblTOU  LEFT JOIN
                dbo.tblLnkMeterPointGroups ON dbo.tblLnkMeterPointGroups.MeterPointId=dbo.tblTOU.MeterPointId
                left join              dbo.tblMeterPointGroups   ON dbo.tblLnkMeterPointGroups.MeterGroupId = dbo.tblMeterPointGroups.MeterGroupid 
		        where dbo.tblTOU.RecordDate  between '$d1' and '$d2'
		        GROUP BY tblMeterPointGroups.GroupName";

                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }


            if (ReportType == "TotalNotCommunicating")
            {
                SQLData = @"SELECT        dbo.tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(distinct dbo.tblTOU.METERPOINTID) AS TOTAL_CAPTURED
                FROM           dbo.tblTOU  LEFT JOIN
                dbo.tblLnkMeterPointGroups ON dbo.tblLnkMeterPointGroups.MeterPointId=dbo.tblTOU.MeterPointId
                left join              dbo.tblMeterPointGroups   ON dbo.tblLnkMeterPointGroups.MeterGroupId = dbo.tblMeterPointGroups.MeterGroupid 
		        where dbo.tblTOU.RecordDate  between '$d1' and '$d2'
		        GROUP BY tblMeterPointGroups.GroupName";

                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }

            if (ReportType == "TotalMalfunctioning")
            {
                SQLData = @"SELECT        dbo.tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(distinct dbo.tblTOU.METERPOINTID) AS TOTAL_CAPTURED
                FROM           dbo.tblTOU  LEFT JOIN
                dbo.tblLnkMeterPointGroups ON dbo.tblLnkMeterPointGroups.MeterPointId=dbo.tblTOU.MeterPointId
                left join              dbo.tblMeterPointGroups   ON dbo.tblLnkMeterPointGroups.MeterGroupId = dbo.tblMeterPointGroups.MeterGroupid 
		        where dbo.tblTOU.RecordDate  between '$d1' and '$d2'
		        GROUP BY tblMeterPointGroups.GroupName";

                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }

            if (ReportType == "PercentageRead")
            {
                SQLData = @"SELECT        dbo.tblMeterPointGroups.GroupName AS IBC_NAMES, COUNT(distinct dbo.tblTOU.METERPOINTID) AS TOTAL_CAPTURED
                FROM           dbo.tblTOU  LEFT JOIN
                dbo.tblLnkMeterPointGroups ON dbo.tblLnkMeterPointGroups.MeterPointId=dbo.tblTOU.MeterPointId
                left join              dbo.tblMeterPointGroups   ON dbo.tblLnkMeterPointGroups.MeterGroupId = dbo.tblMeterPointGroups.MeterGroupid 
		        where dbo.tblTOU.RecordDate  between '$d1' and '$d2'
		        GROUP BY tblMeterPointGroups.GroupName";
                 
                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds); 
                Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }


            //Daily Meter Reading
            if (ReportType == "MeterReading")
            {

                FromDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

                SQLData = @"SELECT        tblProfileInstI.Date_M, tblProfileInstI.QtyValue, tblDevices.SerialNumber, tblDevices.PlantNumber
                FROM  tblMeterPoints FULL OUTER JOIN
                                         tblLnkMeterPointGroups ON tblMeterPoints.MeterPointId = tblLnkMeterPointGroups.MeterPointId 
						                 FULL OUTER JOIN
                                         tblDevices ON tblMeterPoints.DeviceId = tblDevices.DeviceId FULL OUTER JOIN
                                         tblProfileInstI ON tblLnkMeterPointGroups.MeterPointId = tblProfileInstI.MeterPointId
                WHERE (tblProfileInstI.Date_M BETWEEN CONVERT(DATETIME, " + "'" + FromDate + "'" + @", 102) 
                AND CONVERT(DATETIME, " + "'" + ToDate + "'" + @", 102))
                and  tblDevices.SerialNumber = " + "'" + MeterNo + "'" + @"
                ";
                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);
            }

            if (ReportType == "MonthlyConsumption")
            {

                FromDate = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd 00:00:00");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

                SQLData = @"SELECT CONVERT(varchar(6), tblProfileInstI.Date_M) AS Days,CONVERT(varchar, tblProfileInstI.Date_M, 101) AS ORDERING , SUM(tblProfileInstI.QtyValue) AS  QtyValue, tblDevices.SerialNumber, tblDevices.PlantNumber
FROM tblMeterPoints FULL OUTER JOIN 
tblLnkMeterPointGroups ON tblMeterPoints.MeterPointId = tblLnkMeterPointGroups.MeterPointId FULL OUTER JOIN
tblDevices ON tblMeterPoints.DeviceId = tblDevices.DeviceId FULL OUTER JOIN
tblProfileInstI ON tblLnkMeterPointGroups.MeterPointId = tblProfileInstI.MeterPointId
WHERE  (tblProfileInstI.Date_M BETWEEN CONVERT(DATETIME, " +"'"+ FromDate+"'" + @", 102) AND CONVERT(DATETIME, " +"'"+ ToDate +"'"+ @", 102))
GROUP BY tblProfileInstI.MeterPointId, CONVERT(varchar(6), tblProfileInstI.Date_M),CONVERT(varchar, tblProfileInstI.Date_M, 101), tblLnkMeterPointGroups.MeterGroupId, tblDevices.SerialNumber, tblDevices.PlantNumber
HAVING (tblDevices.SerialNumber = " + "'" + MeterNo + "'" + @") order by ORDERING";


                SqlDataAdapter dataAdapt1 = new SqlDataAdapter(SQLData, con);
                dataAdapt1.Fill(_ds);

                // Number = _ds.Tables[0].Rows[0]["TOTAL_CAPTURED"].ToString();
            }

            return _ds;
        }
        public ActionResult AMRDashboard()
        {
            return View();
        }
	}
}