using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHEDServe.Controllers
{
    public class MeterInstallationController : Controller
    {
        //
        // GET: /MeterInstallation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NetworkRehab()
        {
            return View();
        }

        public ActionResult ApproveForInstallation()
        {
            return View();
        }

        public ActionResult CaptureMeters()
        {
            return View();
        }
        public ActionResult ApprovalList()
        {
            return View();
        }
        public ActionResult InstalledMeters()
        {
            return View();
        }
	}
}