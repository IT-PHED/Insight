using Oracle.DataAccess.Client;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PHEDServe.Controllers
{
    public class TransformerAPIController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private static MscPlan mPlanData = new MscPlan();
        public static string STSSETUPURL = ConfigurationManager.AppSettings["STSSETUPURI"];
        public static string SPARKSETUPURI = ConfigurationManager.AppSettings["SPARKSETUPURI"];
        public static string SPARKTOKEN = ConfigurationManager.AppSettings["SPARKTOKEN"];
        private static string strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString();
        private OracleConnection conn = new OracleConnection(strConnString);
        private static string _strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnectionLOAD"].ConnectionString.ToString();
        private OracleConnection connLOAD = new OracleConnection(_strConnString);
        private SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        string EnumerationConnection = System.Configuration.ConfigurationManager.ConnectionStrings["EnumerationConnection"].ConnectionString.ToString();
        string EnumsConnection = System.Configuration.ConfigurationManager.ConnectionStrings["EnumsConnection"].ConnectionString.ToString();
        string NOMSConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NOMSConnectionString"].ConnectionString.ToString();




        #region Transformer


        #endregion




    }
}