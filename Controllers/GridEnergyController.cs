using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class GridEnergyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET:/GridEnergy//
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridEnergyReport()
        {
            return View();
        }


        public ActionResult ActionThatRetrieveAndAspx()
        {
            return View("WebForm1"); //Aspx file Views/Products/WebForm1.aspx
        }
        public FileResult GetAllDetails()
        {
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/Reports"), "EnergyUse.rdlc");

            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }

            List<NationalGridEnergy> Grid = new List<NationalGridEnergy>();

            NationalGridEnergy EntryDetails = new NationalGridEnergy();

            var GetGridDetails = db.NationalGridEnergys.ToList();



            ReportDataSource rd = new ReportDataSource("EnergyUse", GetGridDetails);

            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;


            string deviceInfo =


           "<DeviceInfo>" +
           "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
           "  <PageWidth>16in</PageWidth>" +
           "  <PageHeight>14in</PageHeight>" +
           "  <MarginTop>0.25in</MarginTop>" +
           "  <MarginLeft>0.25in</MarginLeft>" +
           "  <MarginRight>0.25in</MarginRight>" +
           "  <MarginBottom>0.25in</MarginBottom>" +
           "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
            reportType,
            deviceInfo,
            out mimeType,
            out encoding,
            out fileNameExtension,
            out streams,
            out warnings);



            return File(renderedBytes, mimeType);


        }



        public JsonResult loadReferenceViewModel()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            AppViewModels viewModel = new AppViewModels();

            TransmissionStation Stataions = new TransmissionStation();


            List<TransmissionStation> StationList = new List<TransmissionStation>();

            var s = db.TransmissionStationss.ToList().OrderBy(p => p.Location.ToString().Trim());

            foreach (var item in s)
            {
                if (item.Location == "" || item.Location == null)
                {
                    continue;
                }
                else
                {
                    Stataions = new TransmissionStation();
                    
                    Stataions.TransmissionStationID = item.StationID;
                    
                    Stataions.TransmissionStationName = item.Location.Replace("Transmission Station", "");

                    if (StationList.Contains(Stataions))
                    {
                        continue;
                    }
                    else
                    {
                        StationList.Add(Stataions);
                    }
                }
            }

            viewModel.AllTransformerList = new List<Transformers>();
            viewModel.ModifiedTransmissionStationList = StationList;
            viewModel.Feeder33List = new List<Feeders33VM>();
            var result = JsonConvert.SerializeObject(viewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertReading(string DailyReadings, string Date)
        {
            AppViewModels viewModel = new AppViewModels();

            //FeederId,FeederName,FeederVoltLevel,PanelCTR,MeterNo,Reading,TransmissionStation,TransmissionStationID,TransformerCapacity,Date

            //Check if the items are allowed for Update

            NationalGridEnergy GridEnergy = new NationalGridEnergy();

            var dataFromClient = JsonConvert.DeserializeObject<List<NationalGridEnergy>>(DailyReadings);

            DateTime _date = Convert.ToDateTime(Date);

            //var orderForBooks = from bk in db.Feeders33s
            //                    join ordr in db.NationalGridEnergys
            //                    on bk.FeederID equals ordr.FeederID
            //                    into a
            //                    where ordr.Date == date
            //                    from b in a.DefaultIfEmpty(new Order())
            //                    select new
            //                    {
            //                        bk.FeederID,
            //                        Name = bk.FeederVoltlevel,
            //                        bk.FeederOrder
            //                    };


            DateTime _Date = Convert.ToDateTime(_date);

            //check if the date of the application is already Inserted into the aDatabase 
            // DateTime SelectedDate  = 

            List<string> FeederIds = new List<string>();

            foreach (var item in dataFromClient)
            {

                db = new ApplicationDbContext();

                var IterationId = item.FeederID;

                var Data = db.NationalGridEnergys.FirstOrDefault(p=>p.FeederID == IterationId && p.Date == _date);

                if (Data != null)
                {
                    //perform the Update of the Feeder Item
                    FeederIds.Add(item.FeederID);
                    Data.ReadingValue = item.ReadingValue;
                    db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    if (FeederIds.Contains(item.FeederID))
                    {
                        continue;
                    }
                    GridEnergy = new NationalGridEnergy();
                    GridEnergy.Date = Convert.ToDateTime(Date);
                    GridEnergy.Day = Convert.ToDateTime(Date).Day.ToString();
                    GridEnergy.FeederID = item.FeederID;
                    GridEnergy.FeederName = item.FeederName;
                    GridEnergy.FeederVoltLevel = item.FeederVoltLevel;
                    GridEnergy.MeterNo = item.MeterNo;
                    GridEnergy.Month = Convert.ToDateTime(Date).Month.ToString();
                    GridEnergy.PanelCTR = item.PanelCTR;
                    GridEnergy.ReadingValue = item.ReadingValue;
                    GridEnergy.TransformerCapacity = item.TransformerCapacity;
                    GridEnergy.TransmissionStation = item.TransmissionStation;
                    GridEnergy.TransmissionStationID = item.TransmissionStationID;
                    GridEnergy.Year = Convert.ToDateTime(Date).Year.ToString();
                    db.NationalGridEnergys.Add(GridEnergy);
                    db.SaveChanges();
                }
            }

            var result = JsonConvert.SerializeObject(viewModel);
            var jsonResult = Json(new { result = result, Message = "Success" }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        [HttpPost]
        public JsonResult GetFeeders33KV(string FeederID, string FeederName)
        {
            AppViewModels viewModel = new AppViewModels();

            int _FeederID = 0;

            //have a Model that accomodates Comments and Date

            DateTime _FeederDate = DateTime.Now;

            var AllTransformers = db.TransmissionStationss.Where(p => p.Location.Replace("Transmission Station", "") == FeederName).ToList();

            List<Transformers> Transformers = new List<Transformers>();
            Transformers _trans = new Transformers();

            foreach (var item in AllTransformers)
            {
                _trans = new Transformers();
                _trans.TransformerID = item.StationID;
                _trans.TransformerName = item.Description; 
                Transformers.Add(_trans);
            }
            viewModel.AllTransformerList = Transformers;

            viewModel.Feeder33List = new List<Feeders33VM>();

            var result = JsonConvert.SerializeObject(viewModel);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Get33KVFeeders(string FeederID,string Date)
        {
            AppViewModels viewModel = new AppViewModels();
            int _FeederID = 0;
            //have a Model that accomodates Comments and Date
            DateTime _FeederDate = DateTime.Now; 
            _FeederID = Convert.ToInt32(FeederID);
            viewModel.AllTransformerList = new List<Transformers>();


            //Do somthing Thaht looks like a Left Join here
             
            //select all the items from the National Grid By the date and Feeder ID and form a list

            List<Feeders33VM> FeederList = new List<Feeders33VM>();
            //fetch Data by Date 



            Feeders33VM NewFeeders = new Feeders33VM();
            DateTime _Dates = Convert.ToDateTime(Date);
            var NatGridData = db.NationalGridEnergys.Where(p => p.Date == _Dates && p.TransmissionStationID == FeederID).ToList();
            var Feeders = db.Feeders33s.Where(p => p.TransmissionStationID == _FeederID).ToList();
            List<string> FeederIds = new List<string>();
           int i = 1;  
            if (NatGridData.Count > 0)
            {
                //there is some data in the List that will be useful
               
                foreach (var item in NatGridData)
                {
                    NewFeeders = new Feeders33VM();
                    NewFeeders.Description = item.FeederName;
                    NewFeeders.FeederID = Convert.ToInt32(item.FeederID);
                    NewFeeders.FeederOrder = i;
                    NewFeeders.FeederVoltlevel = item.FeederVoltLevel;
                    NewFeeders.MeterNo = item.MeterNo;
                    NewFeeders.PanelCTR = item.PanelCTR;
                    NewFeeders.Reading = item.ReadingValue;
                    NewFeeders.TransmissionStationID = _FeederID;
                    FeederIds.Add(item.FeederID);
                    i++;
                    FeederList.Add(NewFeeders);
                }

                foreach (var item in Feeders)
                {
                    NewFeeders = new Feeders33VM();
               
                    if (FeederIds.Contains(item.FeederID.ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        NewFeeders.Description = item.Description;
                        NewFeeders.FeederID = item.FeederID;
                        NewFeeders.FeederOrder = item.FeederOrder;
                        NewFeeders.FeederVoltlevel = item.FeederVoltlevel;
                        NewFeeders.LAT = item.LAT;
                        NewFeeders.Location = item.Location;
                        NewFeeders.LON = item.LON;
                        NewFeeders.MeterNo = item.MeterNo;
                        NewFeeders.PanelCTR = item.PanelCTR;
                        NewFeeders.Reading = item.Reading;
                        NewFeeders.RouteLenght = item.RouteLenght;
                        NewFeeders.TransmissionStationID = item.TransmissionStationID;
                        FeederList.Add(NewFeeders);
                        i++;
                    }
                }
              
            }

            else
            { 
                foreach (var item in Feeders)
                {
                    NewFeeders = new Feeders33VM();
                    if (FeederIds.Contains(item.FeederID.ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        NewFeeders.Description = item.Description;
                        NewFeeders.FeederID = item.FeederID;
                        NewFeeders.FeederOrder = item.FeederOrder;
                        NewFeeders.FeederVoltlevel = item.FeederVoltlevel;
                        NewFeeders.LAT = item.LAT;
                        NewFeeders.Location = item.Location;
                        NewFeeders.LON = item.LON;
                        NewFeeders.MeterNo = item.MeterNo;
                        NewFeeders.PanelCTR = item.PanelCTR;
                        NewFeeders.Reading = item.Reading;
                        NewFeeders.RouteLenght = item.RouteLenght;
                        NewFeeders.TransmissionStationID = item.TransmissionStationID;
                        FeederList.Add(NewFeeders);
                        i++;
                    }
                }
            }


            //Step 2. Selelct all the Items in the 

            //Step 3. Selelct all the Items in the Feeder33List and use iterate through it

            //Step 4. The items that are to be iterated over sould be items that 

            //Step 5. The items in the 


             

            viewModel.Feeder33List = FeederList.ToList();
            var result = JsonConvert.SerializeObject(viewModel);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet); 
            jsonResult.MaxJsonLength = int.MaxValue; 
            return jsonResult;
        }
    }
}