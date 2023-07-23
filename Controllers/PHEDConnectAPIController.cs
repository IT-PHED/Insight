
using ERDBManager;
using Newtonsoft.Json;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace PHEDServe.Controllers
{
    public class PHEDConnectAPIController : ApiController
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

        // private SqlConnection EnumsConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EnumsConnection"].ConnectionString);


        string EnumerationConnection = System.Configuration.ConfigurationManager.ConnectionStrings["EnumerationConnection"].ConnectionString.ToString();

        string EnumsConnection = System.Configuration.ConfigurationManager.ConnectionStrings["EnumsConnection"].ConnectionString.ToString();

        //LIVE
        //String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString(); 
        //UAT 

        string NOMSConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NOMSConnectionString"].ConnectionString.ToString(); 
             
        #region Bill Verification by Wilfred

        [HttpGet]
        [Route("api/PHEDConnectAPI/GetIdentityUsers")]
        public HttpResponseMessage GetIdentityUsers()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select StaffId, AccountNumber, AccountNo, MeterNo, Arrears, MeterType, LastDatePaid, LastAmount, AccountType from [IdentityUser]";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        DataSet ds = new DataSet();

                        ds.Merge(dt);
                        List<EnhanceIdentityUser> identityUsersList = new List<EnhanceIdentityUser>();

                        if (ds != null)
                        {

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                EnhanceIdentityUser identityUser = new EnhanceIdentityUser();
                                identityUser.StaffId = ds.Tables[0].Rows[i]["StaffId"].ToString();
                                identityUser.AccountNumber = ds.Tables[0].Rows[i]["AccountNumber"].ToString();
                                identityUser.AccountNo = ds.Tables[0].Rows[i]["AccountNo"].ToString();
                                identityUser.MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();
                                identityUser.LastAmount = ds.Tables[0].Rows[i]["LastAmount"].ToString();
                                identityUser.AccountType = ds.Tables[0].Rows[i]["AccountType"].ToString();
                                identityUser.Arrears = ds.Tables[0].Rows[i]["Arrears"].ToString();
                                identityUser.LastDatePaid = ds.Tables[0].Rows[i]["LastDatePaid"].ToString();
                                identityUser.MeterType = ds.Tables[0].Rows[i]["MeterType"].ToString(); 
                                identityUsersList.Add(identityUser);
                            }

                        } 

                        try
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK, identityUsersList, new JsonMediaTypeFormatter());

                        }
                        catch (Exception ex)
                        {
                            HttpError Error = new HttpError(ex.Message) { { "IsSuccess", false } };
                            return Request.CreateErrorResponse(HttpStatusCode.OK, Error);
                        }

                    }
                }
            }

        }

        #endregion

        #region Account Separation By Ekene

        #region NOMS

          //Ukonu Chidozie

          [System.Web.Http.HttpPost]
          [Route("api/PHEDConnectAPI/GetAMRFeederDetails")]
          public HttpResponseMessage GetAMRFeederDetails(AccountSeparation Data)
          {
              DataSet amrDS = new DataSet();
              AMRFeederDataDTO dtoGetGrid = new AMRFeederDataDTO();
              List<AMRFeederData> list = new List<AMRFeederData>();
              string msg = "";
              string constr = ConfigurationManager.ConnectionStrings["NOMSConnectionString"].ConnectionString;

              using (SqlConnection con = new SqlConnection(NOMSConnectionString))
              {
                  using (SqlCommand cmd = new SqlCommand("getnonsData", con))
                  {
                      try
                      {
                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.Parameters.Add("@passtime", SqlDbType.VarChar).Value = Data.timestamp;
                          cmd.Parameters.Add("@latdate", SqlDbType.VarChar).Value = Data.Date;
                          cmd.Parameters.Add("@Currenttime", SqlDbType.VarChar).Value = Data.CurrentTime;
                          cmd.Parameters.Add("@feeder11id", SqlDbType.VarChar).Value = Data.FeederId;
                          con.Open();
                          SqlDataReader rdr = cmd.ExecuteReader();

                          if (rdr.HasRows)
                          {
                              while (rdr.Read())
                              {
                                  var feeder33name = rdr[0].ToString();
                                  var InjSubname = rdr[1].ToString();
                                  var Feeder11name = rdr[2].ToString();
                                  var Bands = rdr[3].ToString();
                                  var METER_NO = rdr[4].ToString();
                                  var RECORDDATE = rdr[5].ToString();
                                  var MW = rdr[6].ToString();
                                  var feeder33id = rdr[7].ToString();
                                  var feeder11id = rdr[8].ToString();
                                  var InjSubId = rdr[9].ToString();
                                  var power_factor = rdr[10].ToString();
                                  var Frequency = rdr[11].ToString();
                                  var getdateval = rdr[12].ToString();
                                  var getdateval1 = rdr[13].ToString();
                                  var IA = rdr[14].ToString();
                                  var IB = rdr[15].ToString();
                                  var IC = rdr[16].ToString();
                                  var VA = rdr[17].ToString();
                                  var VB = rdr[18].ToString();
                                  var VC = rdr[19].ToString();
                                   
                                  list.Add(new AMRFeederData
                                  {
                                      feeder33name = feeder33name,
                                      InjSubname = InjSubname,
                                      Feeder11name = Feeder11name,
                                      Bands = Bands,
                                      METER_NO = METER_NO,
                                      RECORDDATE = Convert.ToDateTime(RECORDDATE).ToString("yyyy/MM/dd HH:mm:ss"),                                 
                                      MW = MW,
                                      feeder11id = feeder11id,
                                      feeder33id = feeder33id,
                                      InjSubId = InjSubId,
                                      power_factor = power_factor,
                                      Frequency = Frequency,
                                      getdateval = getdateval,
                                      getdateval1 = getdateval1,
                                      IA = IA,
                                      IB = IB,
                                      IC = IC,
                                      VA = VA,
                                      VB = VB,
                                      VC = VC,
                                  });
                              } 
                              con.Close();
                              return Request.CreateResponse(HttpStatusCode.OK, list);
                          }
                      }
                      catch (Exception ex)
                      {
                          msg = ex.Message;
                      }
                  }
              }

              var message = string.Format("The Data could not be loaded because " + msg);
              HttpError err = new HttpError(message);
              return Request.CreateResponse(HttpStatusCode.NotFound, err);

          }

          #endregion

          private string GetSeparationAccount(string primaryaccount, string StaffName, string StaffId)
          {
              List<string> list = new List<string>();
              List<string> subactlist = new List<string>();
              string OncString = primaryaccount.Substring(10, 2);

              string Message = "";
              //8122637163 01
              //if (primaryaccount.Length >= 12 & OncString != "01")
              //{
              //    var message = string.Format("This Account cannot be separated because it is not a Primary Account. Please check and try again. Thank You.");
              //    HttpError err = new HttpError(message);
              //    return message;
              //}

              string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
              conn.Open();

              OracleDataAdapter da = new OracleDataAdapter();
              OracleCommand cmd = new OracleCommand
              {
                  Connection = conn,
                  CommandType = CommandType.StoredProcedure,
                  CommandText = "ENSERV.SP_MAP_GET_ALLSEPARATED_ACCOUNTS"
              };

              cmd.CommandTimeout = 900;

              //cmd.Parameters.Add(new OracleParameter("in_accountno", OracleDbType.Varchar2, ParameterDirection.Input));
              // cmd.Parameters.Add(new OracleParameter("p_MobileNo", OracleDbType.Varchar2, ParameterDirection.Input));
              cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
              cmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = primaryaccount;

              using (OracleDataReader rdr = cmd.ExecuteReader())
              {

                  if (rdr.HasRows)
                  {

                      while (rdr.Read())
                      {
                          String acctno = rdr["CONS_ACC"].ToString();

                          if (acctno.Length > 12)
                          {
                              list.Add(acctno.Substring(acctno.Length - 1));
                          }
                      }

                      String[] str = list.ToArray();
                      IEnumerable<string> alphas = from planet in alphabet.Except(str)
                                                   select planet;
                      string[] alphastoarray = alphas.Cast<string>().ToArray();


                      if (AddsubacctToSQL(primaryaccount.ToString() + alphastoarray[0], primaryaccount.ToString()) > 0)
                      {
                          Message = primaryaccount.ToString() + alphastoarray[0];

                          int a = InsertParentAccountToSQL(StaffName, StaffId, primaryaccount, 1, DateTime.Now.ToString());

                          int b = UpdateSecondaryAcctsforSeperation(primaryaccount, Message, StaffId, StaffName);
                      }
                      else
                      {
                          Message = "An error occured and Account Could not be Saved for separation. Please try again";
                      }
                  }
                  else
                  {
                    conn.Close();
                    conn.Dispose();
                      Message = "An Error Occured Getting the Data from DLEnhance. Please try again.";
                  }
              }
              return Message;
          }




        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DoaccountSeperation")]
        public HttpResponseMessage DoaccountSeperation(AccountSeparation Data)
        {

            List<string> list = new List<string>();
            List<string> subactlist = new List<string>();

            string OncString = Data.primaryaccount.Substring(10, 2);
             
            if (Data.primaryaccount.Length >= 12 & OncString != "01")
            {
                var message = string.Format("This Account cannot be separated because it is not a Primary Account. Please check and try again. Thank You.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err); 
            }
            
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            conn.Open();
            //conn.ConnectionTimeout = 900;
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_MAP_GET_ALLSEPARATED_ACCOUNTS"
            };   
             
            cmd.CommandTimeout = 900;
           
            //cmd.Parameters.Add(new OracleParameter("in_accountno", OracleDbType.Varchar2, ParameterDirection.Input));
            // cmd.Parameters.Add(new OracleParameter("p_MobileNo", OracleDbType.Varchar2, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            cmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.primaryaccount;


            using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                //responsedatakyc.Satatus = rdr.GetString(0);
                if (rdr.HasRows)
                {
                    if (InsertParentAccountToSQL(Data.requestbyname, Data.requestbyid, Data.primaryaccount.ToString(), Convert.ToInt32( Data.noofseparation), Data.requestdate) > 0)
                    {
                        while (rdr.Read())
                        {
                            String acctno = rdr["CONS_ACC"].ToString();
                            if (acctno.Length > 12)
                            {
                            list.Add(acctno.Substring(acctno.Length - 1));
                            } 
                        }
                        conn.Close();
                        conn.Dispose();
                        
                        String[] str = list.ToArray();
                        IEnumerable<string> alphas = from planet in alphabet.Except(str)
                                                     select planet;
                        string[] alphastoarray = alphas.Cast<string>().ToArray();

                        //conn.Close();
                        for (int i = 0; i < Data.noofseparation; i++)
                        {
                            if (AddsubacctToSQL(Data.primaryaccount.ToString() + alphastoarray[i], Data.primaryaccount.ToString()) > 0)
                            {
                                subactlist.Add(Data.primaryaccount + alphastoarray[i]);

                            }
                        }
                    }
                    else
                    {
                        conn.Close();
                        conn.Dispose();
                        subactlist.Add("Separation has been requested for this Primary Account Before.");
                    }
                }
                else
                {
                    subactlist.Add("Invalid Account");
                }

                return Request.CreateResponse(HttpStatusCode.OK, subactlist);
            }
        }
         
         
        private int InsertParentAccountToSQL(string requestbyname, string requestbyid, string primaryaccount, int noofseparation, string requestdate)
        {
            string query = "insert into [ENHANCE].[ebuka].[tbl_map_accountsseparation] (noofseparation,requestdate,primaryaccount,requestbyid,requestbyname)" +
                              "values(@noofseparation, @requestdate, @primaryaccount, @requestbyid, @requestbyname) ";
            //using (SqlConnection sqlconn = new SqlConnection(sqlconnstring))

            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                //a shorter syntax to adding parameters
                command.Parameters.Add("@noofseparation", SqlDbType.VarChar).Value = noofseparation;
                // command.Parameters.Add("@noofseparation", SqlDbType.VarChar).Value = noofseparation;
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = primaryaccount;
                command.Parameters.Add("@requestbyname", SqlDbType.VarChar).Value = requestbyname;
                command.Parameters.Add("@requestbyid", SqlDbType.VarChar).Value = requestbyid;
                command.Parameters.Add("@requestdate", SqlDbType.DateTime).Value = requestdate;

                //make sure you open and close(after executing) the connection
                sqlcon.Open();

                try
                {
                    int result = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    return 0;
                }
            }
        }

        private int AddsubacctToSQL(string subaccount, string parentsaccount )
        {
            string query = "INSERT INTO [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] (primaryaccounts, secondaryaccount) VALUES (@primaryaccounts,@secondaryaccount)";
            
            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                //a shorter syntax to adding parameters
                command.Parameters.Add("@primaryaccounts", SqlDbType.VarChar).Value = parentsaccount;
                command.Parameters.Add("@secondaryaccount", SqlDbType.VarChar).Value = subaccount;
          


                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    int res = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return res;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    return 0;
                }
            }
        }


    //private int AddsubacctToSQL(string subaccount, string parentsaccount , string StaffId, string StaffName)
    //    {
    //        string query = "INSERT INTO [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] (primaryaccounts, secondaryaccount, aprovedbyid, approvedbyname, approveddate) VALUES (@primaryaccounts,@secondaryaccount,@StaffId, @StaffName, @DateCaptured)";
            
    //        using (SqlCommand command = new SqlCommand(query, sqlcon))
    //        {
    //            //a shorter syntax to adding parameters
    //            command.Parameters.Add("@primaryaccounts", SqlDbType.VarChar).Value = parentsaccount;
    //            command.Parameters.Add("@secondaryaccount", SqlDbType.VarChar).Value = subaccount;
    //            command.Parameters.Add("@StaffId", SqlDbType.VarChar).Value = StaffId;
    //            command.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = StaffName;
    //            command.Parameters.Add("@DateCaptured", SqlDbType.VarChar).Value = DateTime.Now.ToString();
                 
    //            //make sure you open and close(after executing) the connection
    //            sqlcon.Open();
    //            try
    //            {
    //                int res = command.ExecuteNonQuery();
    //                sqlcon.Close();
    //                return res;
    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e.StackTrace);
    //                sqlcon.Close();
    //                return 0;
    //            }
    //        }
    //    }



        // GET: api/AccountSeperation/5
        public string GET(int id)
        {
            return "value";
        }


        /// <summary>
        /// This returns the Accounts that have been approved and the Accounts that have not been approved
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/getAllPrimaryAccountsPendingSubAccountsforApproval")]
        public HttpResponseMessage getAllPrimaryAccountsPendingSubAccountsforApproval(AccountSeparation Data)
        {
            var list = new List<AllSubAcct>();

            string query = "select * from [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] where primaryaccounts=@primaryaccount";
            
            using (SqlCommand command = new SqlCommand(query,sqlcon))
            {
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = Data.primaryaccount;
                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var sn = Convert.ToInt32(rdr[0]);
                            var primaryaccounto = rdr[1].ToString();
                            var seconderyaccount0 = rdr[2].ToString();
                            var Status = rdr[7].ToString();

                            list.Add(new AllSubAcct { sn = sn, primaryact = primaryaccounto, secact = seconderyaccount0, Status = Status });
                        }
                    }
                    sqlcon.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    var siteID = 0;
                    var siteName = "Error Occured";
                    list.Add(new AllSubAcct { sn = siteID, primaryact = siteName });
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }

        }



      
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetAllApprovedAccountsByStaffID")]
        public HttpResponseMessage GetAllApprovedAccountsByStaffID(AccountSeparation Data)
        {
            var list = new List<AllPrimaryAccounts>();
            string query = "select sn, primaryaccount, noofseparation-subaccountscreated as pending, requestdate,requestbyname from [ENHANCE].[ebuka].[tbl_map_accountsseparation]" +
                "where requestbyid  = @staffid and checked is NOT null ";
           


            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                command.Parameters.Add("@staffid", SqlDbType.VarChar).Value = Data.staffid;
                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    SqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var sn = rdr[0].ToString();
                            var primaryaccounto = rdr[1].ToString();
                            var pending = rdr[2].ToString();
                            var requestdate = rdr[3].ToString();
                            var requestbyname = rdr[4].ToString();
                            list.Add(new AllPrimaryAccounts { sn = sn, primaryaccount = primaryaccounto, pending = pending, requestdate = requestdate, requestbyname = requestbyname });
                        }
                        sqlcon.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }
                    else
                    {
                        var sn = "0";
                        var siteName = "No Record";
                        list.Add(new AllPrimaryAccounts { sn = sn, primaryaccount = siteName });
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    var siteID = "0";
                    var siteName = "Please ensure you are a Feeder Manager or a Zonal Manager before you can Approve Accounts.";
                    list.Add(new AllPrimaryAccounts { sn = siteID, primaryaccount = siteName });
                    return Request.CreateResponse(HttpStatusCode.OK, list);
             
                }
            }
        }

         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetAllPendingAccountsByStaffID")]
        public HttpResponseMessage GetAllPendingAccountsByStaffID(AccountSeparation Data)
        {
            var list = new List<AllPrimaryAccounts>();
            string query = "select sn, primaryaccount, noofseparation-subaccountscreated as pending, requestdate,requestbyname from [ENHANCE].[ebuka].[tbl_map_accountsseparation]" +
                "where requestbyid  = @staffid and (checked is  null) or (noofseparation > checked) ";
            
            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                command.Parameters.Add("@staffid", SqlDbType.VarChar).Value = Data.staffid;
                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var sn = rdr[0].ToString();
                            var primaryaccounto = rdr[1].ToString();
                            var pending = rdr[2].ToString();
                            var requestdate = rdr[3].ToString();
                            var requestbyname = rdr[4].ToString();
                            list.Add(new AllPrimaryAccounts { sn = sn, primaryaccount = primaryaccounto, pending = pending, requestdate = requestdate, requestbyname = requestbyname });
                        }
                        sqlcon.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }
                    else
                    {
                        var sn = "0";
                        var siteName = "No Record";
                        list.Add(new AllPrimaryAccounts { sn = sn, primaryaccount = siteName });
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    var siteID = "0";
                    var siteName = "The requested Data could not be retrieved because " + e.Message;
                    list.Add(new AllPrimaryAccounts { sn = siteID, primaryaccount = siteName });
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
        }
        
        
        // POST: api/AccountSeperation
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/PostAccounts")]
        public HttpResponseMessage PostAccounts(AccountSeparation Data)
        {
            List<string> list = new List<string>();
            List<Category> categoryRepo = new List<Category>();
            string s = "";
            String[] secacctRepo = Data.SeparationAccount.Split(',');

            foreach (var secact in secacctRepo)
            {
                /*categoryRepo.Add(new Category()
                {
                    secaccountno = secact,
                    CategoryName = String.Format("Category_{0}", secact) 
                }) ;
               */
                //conn.Open();

                int i = 0;
                s = secact;
                bool isNumeric = int.TryParse(s.Substring(s.Length - 1), out i);
                Console.WriteLine(isNumeric + " " + s);

                if (isNumeric)
                {
                    s += s + ",";
                }

                #region Add Secondary Account to DLEnhance
                string StaffID = "PHEDConnect-" + Data.staffid;

                int output = AddseconderyaccountstoDlEnhance(secact.ToString(), Data.primaryaccount, StaffID);

                //conn.ConnectionTimeout = 900;

                //#region Account Separation on LIVE
                //OracleDataAdapter da = new OracleDataAdapter();
                //int res = 0;
                //OracleCommand cmd = new OracleCommand
                //{
                //    Connection = conn,
                //    CommandType = CommandType.StoredProcedure,
                //    CommandText = "ENSERV.SP_MAP_INSERT_SEPARATEDACCOUNTS"
                //};
                //cmd.CommandTimeout = 900;
                //cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = StaffID;
                //cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.primaryaccount;
                //cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = secact.ToString();
                ////cmd.Parameters.Add(new OracleParameter("p_status", OracleDbType.Int32, ParameterDirection.Output));

                //try
                //{
                //    cmd.ExecuteNonQuery();
                //    res = 1;
                //}
                //catch (Exception ex)
                //{
                //    var suc = false;
                //    var msg = "Account not added because " + ex.Message;
                //    categoryRepo.Add(new Category { success = suc, message = msg });
                //    return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
                //}
                //#endregion



            //#region Account Separation on Training
            //    OracleDataAdapter _da = new OracleDataAdapter();
            //    int _res = 0;
            //    OracleCommand _cmd = new OracleCommand
            //    {
            //        Connection = connLOAD,
            //        CommandType = CommandType.StoredProcedure,
            //        CommandText = "ENSERV.SP_MAP_INSERT_SEPARATEDACCOUNTS"
            //    };
            //    _cmd.CommandTimeout = 900;
            //    _cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = StaffID;
            //    _cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.primaryaccount;
            //    _cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = secact.ToString();
            //    //cmd.Parameters.Add(new OracleParameter("p_status", OracleDbType.Int32, ParameterDirection.Output));

            //    try
            //    {
            //        _cmd.ExecuteNonQuery();
            //        _res = 1;
            //    }
            //    catch (Exception ex)
            //    {
            //        var suc = false;
            //        var msg = "Account not added because " + ex.Message;
            //        categoryRepo.Add(new Category { success = suc, message = msg });
            //        return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
            //    } 
            //    #endregion


                 
                if (output == 1)
                {
                    //if (Updateprimaryactsforseperation(Data.primaryaccount, secact.ToString()) > 0)
                    //{
                    //    var suc = true;
                    //    categoryRepo.Add(new Category { success = suc });
                    //    // return Request.CreateResponse(HttpStatusCode.OK, categoryRepo);
                    //    conn.Close();
                    //}
                    //else
                    //{
                    //    var suc = false;
                    //    var msg = "Primary accounts could not be updated";
                    //    categoryRepo.Add(new Category { success = suc, message = msg });
                    //    // return Request.CreateResponse(HttpStatusCode.OK, categoryRepo);
                    //}



                    int a =    InsertParentAccountToSQL(Data.StaffName,Data.StaffId , Data.primaryaccount,Data.noofseparation,DateTime.Now.ToString());
                    int b = UpdateSecondaryAcctsforSeperation(Data.primaryaccount, secact.ToString(), Data.staffid, Data.StaffName );


                    if (a == 1 && b == 1)
                    {
                        var suc = true;
                        categoryRepo.Add(new Category { success = suc });
                        // return Request.CreateResponse(HttpStatusCode.OK, categoryRepo);
                    }
                }
                else
                {

                    var suc = false;
                    var msg = "Account not added, and Error Occured";
                    categoryRepo.Add(new Category { success = suc, message = msg });
                    return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
                }

                #endregion
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, categoryRepo); 
        }
         
        //POST: api/AccountSeperation
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/SeparateAccount")]
        public HttpResponseMessage SeparateAccount(AccountSeparation Data)
        {
            List<string> list = new List<string>();
            List<Category> categoryRepo = new List<Category>();
            
            string AccountToSeparate = "";

            try
            {
                string OncString = Data.AccountNo.Substring(10, 2);

                if (Data.AccountNo.Length >= 12 & OncString != "01")
                {
                    var message = string.Format("This Account cannot be separated because it is not a Primary Account. Please check and try again. Thank You.");
                    categoryRepo.Add(new Category { success = false, message = message });
                    return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
                }

                AccountToSeparate = GetSeparationAccount(Data.AccountNo, Data.StaffName, Data.StaffId);

                if( AccountToSeparate.ToCharArray().Length > 13)
                {
                    categoryRepo.Add(new Category { success = false, message = AccountToSeparate });
                    return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
                }
            }
            catch (Exception ex)
            {
                categoryRepo.Add(new Category { success = false, message = ex.Message });
                return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
            }

            string StaffID = "PHEDConnect-" + Data.StaffId;

            

            try
            {
                conn = new OracleConnection(strConnString);
                conn.Open();

                int res = 0;

                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ENSERV.SP_MAP_INSERT_SEPARATEDACCOUNTS"
                };
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = StaffID;
                cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;
                cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountToSeparate;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                res = 1;
                var suc = true;
                var msg = "Account " + AccountToSeparate + " Separated Successfully ";
                var Account = AccountToSeparate;
                categoryRepo.Add(new Category { success = suc, message = msg, AccountNo = Account });
                return Request.CreateResponse(HttpStatusCode.OK, categoryRepo);
            }
            catch (Exception ex)
            {
                var suc = false;
                var msg = "Account not added because " + ex.Message;
                categoryRepo.Add(new Category { success = suc, message = msg });
                return Request.CreateResponse(HttpStatusCode.NotFound, categoryRepo);
            }
             
            #endregion


            return Request.CreateResponse(HttpStatusCode.OK, categoryRepo);
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/ClearWrongCapture")]
        public HttpResponseMessage ClearWrongCapture(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string Status = "";

            if (Data == null || string.IsNullOrEmpty(Data.MeterNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("The AccountNo, MeterNo or TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            Data.MeterNo = Data.MeterNo.Trim().Replace(",", "").Replace("'", "").Replace("+", "");

            var check = db.MassMeterCaptures.FirstOrDefault(p => p.MeterNo == Data.MeterNo);

            if (check == null)
            {
                Status = "The MeterNo / Account Number does not exist please verify and try again later. Thank you.";
            }
            else
            {
                db.MassMeterCaptures.Remove(check);
                db.SaveChanges();
                Status = "The wrong record was  corrected Successfully.Thank you.";

                var _message = Status;
                var _err = new HttpError(_message);
                return Request.CreateResponse(HttpStatusCode.OK, _err);
            }
            var __message = Status;
            var __err = new HttpError(__message);
            return Request.CreateResponse(HttpStatusCode.OK, __err);
        }

        public bool CheckMeterOnWhiteListForCompatibility(string MeterNo)
        {
            //Check the Meter 
            ApplicationDbContext db = new ApplicationDbContext();

            var Check = db.MeterLists.Where(p => p.MeterNo == MeterNo).ToList();

            if (Check.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/InstallMassMeters")]
        public HttpResponseMessage InstallMassMeters(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            Data.MeterNo = Data.MeterNo.Trim().Replace(",", "").Replace("'", "").Replace("+", "");
             
            if (!CheckMeterOnWhiteListForCompatibility(Data.MeterNo))
            {

                var message = string.Format("This meter has not been whitelisted, or may be a Stolen Meter. Please refer to PHED Metering Team to resolve. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
           
            if (Data == null || string.IsNullOrEmpty(Data.MeterNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The AccountNo, MeterNo or TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //if (Data == null || string.IsNullOrEmpty(Data.BVN))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("The BVN was not Selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

             
            if (Data == null || string.IsNullOrEmpty(Data.SealNo1))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo1 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } 
            if (Data == null || string.IsNullOrEmpty(Data.SealNo2))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo2 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.InstallerId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("The   InstallerId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ContractorID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   ContractorID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } 

            if (Data == null || string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Latitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Longitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //Save to the Metering Table
                //Save to DLEnhance Database 
                 string Status = ""; 
                //trim the MeterNo 
                //#region  SaveDataToMeteringDB 
                //Status =     SaveMeteringData(Data); 
                //#endregion


                  var check = db.MassMeterCaptures.Where(p => p.MeterNo == Data.MeterNo).ToList();

                  if (check.Count > 0)
                  {
                      Status = "The Meter Number has been captured before against an Account. You cannot repeat Meter Numbers. Please try again with the Correct Meter No";
                      var __message = string.Format(Status);
                      var __err = new HttpError(__message);
                      return Request.CreateResponse(HttpStatusCode.NotFound, __err);
                  }

                //string CheckDLenhance =   CheckForMeterDuplicateInDlEnhance(Data.MeterNo);
                //if (CheckDLenhance == "DUPLICATE")
                //{
                //    Status = "The Meter Number has been captured before against an Account. You cannot repeat Meter Numbers. Please try again with the Correct Meter No";
                //    var __message = string.Format(Status);
                //    var __err = new HttpError(__message);
                //    return Request.CreateResponse(HttpStatusCode.NotFound, __err);
                //}

                 

                #region  SaveDataToMeterToDLEnhance

                Status = SaveDataToMeterToDLEnhance(Data);

                #endregion
                
                if (Status == "1")
                {
                    var __message = string.Format("The Meter " + Data.MeterNo + " was  captured Successfully.Thank you.");
                    var __err = new HttpError(__message);
                    return Request.CreateResponse(HttpStatusCode.OK, __err);
                }
                else
                {
                    var __message = string.Format(Status);
                    var __err = new HttpError(__message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, __err);
                }


            }
            var _message = string.Format("The Meter was  captured Successfully.Thank you.");
            var _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.OK, _err);
        }

        private string SaveDataToMeterToDLEnhance(MAPModel Data)
        {
            conn.Open();

            OracleDataAdapter da = new OracleDataAdapter();
            int res = 0;


            //,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            //#region Training Deployment
            //connLOAD = new OracleConnection(_strConnString);
            //connLOAD.Open(); 
            //OracleCommand _cmd = new OracleCommand
            //{
            //    Connection = connLOAD,
            //    CommandType = CommandType.StoredProcedure,
            //    CommandText = "ENSERV.SP_MASS_METER_CAPTURE"
            //};
            //_cmd.CommandTimeout = 900;
            //_cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = "PhedConnect-" + Data.StaffId;
            //_cmd.Parameters.Add("P_MeterNo", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.MeterNo;
            //_cmd.Parameters.Add("P_AccountNo", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

            //if (Data.MeterPhase == "3")
            //{
            //    _cmd.Parameters.Add("P_meter_phase", OracleDbType.Varchar2, ParameterDirection.Input).Value = "3";
            //}
            //else
            //{
            //    _cmd.Parameters.Add("P_meter_phase", OracleDbType.Varchar2, ParameterDirection.Input).Value = "4";
            //}
            //_cmd.Parameters.Add("p_InstallDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = DateTime.Now.ToString();
            ////cmd.Parameters.Add(new OracleParameter("p_status", OracleDbType.Int32, ParameterDirection.Output));

            //try
            //{
            //    _cmd.ExecuteNonQuery();
            //    res = 1;


            //}
            //catch (Exception Ex)
            //{
            //    return Ex.Message;

            //}


            //connLOAD.Close();
            //connLOAD.Dispose();

            //#endregion
             
            #region LIVE Deployment

                    conn = new OracleConnection(strConnString);
                    conn.Open();
                    OracleCommand cmd = new OracleCommand
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "ENSERV.SP_MASS_METER_CAPTURE"
                    }; 
                    cmd.CommandTimeout = 900;
                    cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = "PhedConnect-" + Data.StaffId;
                    cmd.Parameters.Add("P_MeterNo", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.MeterNo;
                    cmd.Parameters.Add("P_AccountNo", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

                    if (Data.MeterPhase == "3")
                    {
                        cmd.Parameters.Add("P_meter_phase", OracleDbType.Varchar2, ParameterDirection.Input).Value = "3";
                    }
                    else
                    {
                        cmd.Parameters.Add("P_meter_phase", OracleDbType.Varchar2, ParameterDirection.Input).Value = "4";
                    }

                    cmd.Parameters.Add("p_InstallDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = DateTime.Now.ToString();
            
                    try
                    {
                        cmd.ExecuteNonQuery();
                        res = 1;
                        cmd.Dispose(); 
                        conn.Close();
                        conn.Dispose();

                      string Status =  SaveMeteringData(Data);

                      if (Status == "OK")
                      {
                          return "1";
                      }
                      else
                      {
                          return "0";
                      }
               
                    }
                    catch (Exception Ex)
                    {
                        conn.Close();
                        conn.Dispose();
                        return Ex.Message;

                    }


            #endregion
        }
       
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomerfeedback")]
        public HttpResponseMessage GetCustomerfeedback(CustomerfeedbackModel Data)
        { 
            CustomerfeedbackModel d = new CustomerfeedbackModel();
            db = new ApplicationDbContext();
            try
            {
                Customerfeedbacks fbk = new Customerfeedbacks();
                fbk.AccountNo = Data.AccountNo;
                fbk.CustomerName = Data.CustomerName;
                fbk.CustomerPhone = Data.CustomerPhone;
                fbk.CustomerEmail = Data.CustomerEmail;
                fbk.Comments = Data.Comments;
                fbk.DateCommented = DateTime.Now;
                fbk.Latitude = Data.Latitude;
                fbk.Longitude = Data.Longitude;
                fbk.ModuleName = Data.ModuleName;
                fbk.ModuleRating = Data.ModuleRating;
                fbk.ServiceName = Data.ServiceName;
                fbk.ServiceRating = Data.ServiceRating;
                db.Customerfeedbackss.Add(fbk);
                db.SaveChanges();

                var message = string.Format("Saved Successfully. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomercomplaints")]
        public HttpResponseMessage GetCustomercomplaints(CustomercomplaintsModel Data)
        {

            CustomercomplaintsModel d = new CustomercomplaintsModel();
            db = new ApplicationDbContext();
            try
            {
                Customercomplaints fbk = new Customercomplaints();
                fbk.AccountNo = Data.AccountNo;
                fbk.ComplaintsDetails = Data.ComplaintsDetails;
                fbk.Mobileno = Data.Mobileno;
                fbk.DateCommented = DateTime.Now;
                fbk.complainttype =   Data.complainttype;

                db.Customercomplaintss.Add(fbk);
                db.SaveChanges();

                #region Send Email

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("customer.complaints@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                mail.Subject = "New Complaint from " + Data.AccountNo;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                mail.To.Add("customer.complaints@phed.com.ng");
                string RecipientType = "";
                string SMTPMailServer = "mail.phed.com.ng";
                SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                MailSMTPserver.Credentials = new NetworkCredential("customer.complaints@phed.com.ng", "223#phed#");
                string htmlMsgBody = "<html><head></head>";
                htmlMsgBody = htmlMsgBody + "<body>";
                htmlMsgBody = htmlMsgBody + "<P>" + "Dear PHED Customer Care" +", " + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "A new Complaint has been lodged by a customer, kindly find the details below" + "</P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Phone: " + Data.Mobileno + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Complaint Date: " + DateTime.Now + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Complaint Type : " + Data.complainttype + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Compaint Details: " + Data.ComplaintsDetails + " </P>";
                htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + Data.AccountNo + " </P>";
             
                htmlMsgBody = htmlMsgBody + "<br><br>";
                htmlMsgBody = htmlMsgBody + "Kindly ensure all complaints are logged and attended to immediately with the courtesy it demands. Remember our Customer is our greatest asset. Thank you,";
                htmlMsgBody = htmlMsgBody + " <P> " + "PHED Team" + " </P> ";
                htmlMsgBody = htmlMsgBody + "<br><br>";
                mail.Body = htmlMsgBody;
                MailSMTPserver.Send(mail);

                #endregion

                var message = string.Format("Saved Successfully. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
        }

        private string CheckForMeterDuplicateInDlEnhance(string MeterNo)
        {
            conn = new OracleConnection(strConnString);
            conn.Open();
            OracleDataAdapter da = new OracleDataAdapter();

            string Status = null;

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_MASS_METER_CAPTURE_DUPLICATE"
            };

            cmd.CommandTimeout = 900;
            cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            cmd.Parameters.Add("p_meterno", OracleDbType.Varchar2, ParameterDirection.Input).Value = MeterNo;

            using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Status = "DUPLICATE";

                    }
                }
            }

            conn.Close();
            conn.Dispose();
            cmd.Dispose(); 
            return Status;
        }

        private string SaveMeteringData(MAPModel Data)
        {

            MassMeterCapture add = new MassMeterCapture();


            string Status = "OK";

            //check 

            //var check = db.MassMeterCaptures.Where(p => p.SealNo1 == Data.SealNo1 || p.SealNo2 == Data.SealNo2).ToList();

            //if(check.Count > 0 )
            //{
            //    return Status = "The Seal Number has been captured before. You cannot repeat Seal Numbers. Please try again with the Correct Seal No";

            //}

            string TicketNo = Guid.NewGuid().ToString();
            add.MeterCaptureId = TicketNo;
            add.StaffName = Data.StaffName;
            add.DateCaptured = DateTime.Now;
            add.DTRId = Data.DTRCode;
            add.DTRName = Data.DTRName;
            
            add.CIN = GenerateCIN(Data.AccountNo);
            add.FeederId = Data.FeederId;
            add.FeederName = Data.FeederName;
            add.FilePath = Data.filePaths;
            add.Latitude = Data.Latitude;
            add.Longitude = Data.Longitude;
            add.MeterNo = Data.MeterNo;
            add.AccountNo = Data.AccountNo;
            add.Address = Data.Address;
            add.AccountName = Data.AccountName;
            add.MeterPhase = Data.MeterPhase;
            add.StaffId = Data.StaffId;
            add.Zone = Data.Zone;
            add.SealNo1 = Data.SealNo1;
            add.SealNo2 = Data.SealNo2;
            add.InstallerName = Data.InstallerName;
            add.ContractorName = Data.ContractorName;
            add.MAPVendor = Data.MAPVendor;

            //***********NEW FIELDS***********

            add.BVN = Data.BVN;
            add.PROG = Data.PROG;
            add.CustomerPhone = Data.CustomerPhone;
            add.CustomerEmail = Data.CustomerEmail;
            add.OldMeterNo = Data.OldMeterNo;
            add.IsReplaced = Data.IsReplaced;
            add.Band = Data.Band;
            db.MassMeterCaptures.Add(add);
            db.SaveChanges();

            DOCUMENTS Docs = new DOCUMENTS();
            if (!string.IsNullOrEmpty(Data.filePaths))
            {
                List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);
                string[] filePaths = der.ToArray();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    string ImgName = filePaths[i].ToString();
                    var name = ImgName.Split('.');
                    String filename = name[0];
                    String fileext = name[3];
                    Docs = new DOCUMENTS();

                    if (Data.OnboardCategory == "MASSMETERCAPTURE")
                    {
                        Docs.COMMENTS = "Mass Meter Capture for" + Data.ParentAccountNo;
                        Docs.DOCUMENT_NAME = "Mass Meter Capture for " + Data.ParentAccountNo;
                        Docs.DocumentDescription = "Mass Meter Capture for" + Data.ParentAccountNo;
                        Docs.STATUS = "MASSMETERCAPTURE";
                    }

                    Docs.DATE_UPLOADED = DateTime.Now;
                    Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                    Docs.DOCUMENT_EXTENSION = fileext;
                    Docs.DOCUMENT_PATH = filePaths[i].ToString();
                    Docs.REFERENCE_CODE = TicketNo;
                    Docs.SENDER_ID = Data.UserId;
                    Docs.Size = "123KB";
                    db.DOCUMENTSs.Add(Docs);
                    db.SaveChanges();
                }
            }

            GlobalMethodsLib Audit = new GlobalMethodsLib();
            Audit.AuditTrail(Data.UserId, TicketNo + " was captured on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "NEW CUSTOMER");
            RCDCModel Success = new RCDCModel();
            Success.Message = "The Customer with Ticket Number " + TicketNo + "  has been onboarded Successfully";
            Success.Status = "SUCCESS";
            return Status;
        }

        private int Updateprimaryactsforseperation(string primaryacctno)
        {
            string query1 = "update [ENHANCE].[ebuka].[tbl_map_accountsseparation] set checked=1 where primaryaccount=@primaryaccount";
            //string query2 = "update [ENHANCE].[ebuka].[tbl_map_accountsseparation] set checked=1 where primaryaccount=@primaryaccount";

            using (SqlCommand command = new SqlCommand(query1, sqlcon))
            {
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = primaryacctno; 
               // command.Parameters.Add("@Secondaryaccount", SqlDbType.VarChar).Value = primaryacctno;
                //make sure you open and close(after executing) the connection
                
                sqlcon.Open();
                try
                {
                    int res = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return res;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    return 0;
                }
            }
        }

        private int UpdateSecondaryAcctsforSeperation(string primaryacctno, string SecondaryAccount, string StaffId, string StaffName)
        {
            string query1 = "update [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] set Status = 'APPROVED', aprovedbyid = @aprovedbyid,approvedbyname  = @approvedbyname, approveddate = @approveddate  where primaryaccounts = @primaryaccount and secondaryaccount = @Secondaryaccount";
            //string query2 = "update [ENHANCE].[ebuka].[tbl_map_accountsseparation] set checked=1 where primaryaccount=@primaryaccount";

            using (SqlCommand command = new SqlCommand(query1, sqlcon))
            {
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = primaryacctno;
                command.Parameters.Add("@Secondaryaccount", SqlDbType.VarChar).Value = SecondaryAccount; 
                command.Parameters.Add("@aprovedbyid", SqlDbType.VarChar).Value = StaffId;
                command.Parameters.Add("@approvedbyname", SqlDbType.VarChar).Value = StaffName;
                command.Parameters.Add("@approveddate", SqlDbType.VarChar).Value = DateTime.Now.ToShortDateString();

                //make sure you open and close(after executing) the connection
                sqlcon.Open(); 
                try
                {
                    int res = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return res;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    return 0;
                }
            }
        }

        private int AddseconderyaccountstoDlEnhance(string secact, string primaryacctno, string staffid)
        {
            conn = new OracleConnection(strConnString);
            conn.Open();

            OracleDataAdapter da = new OracleDataAdapter();
            int res = 0;
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_MAP_INSERT_SEPARATEDACCOUNTS"
            };

            cmd.CommandTimeout = 900;
            cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = staffid;
            cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = primaryacctno;
            cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = secact.ToString();
      
            try
            {
                res = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                var suc = false;
                var msg = "Account not added because " + ex.Message;
                res = 0;
            }

            return res;

            //conn.ConnectionTimeout = 900;
            //OracleDataAdapter da = new OracleDataAdapter();
            //int res = 0;
            //OracleCommand cmd = new OracleCommand
            //{
            //    Connection = conn,
            //    CommandType = CommandType.StoredProcedure,
            //    CommandText = "ENSERV.SP_MAP_INSERT_SEPARATEDACCOUNTS"
            //};
            //cmd.CommandTimeout = 900;
            //cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = staffid;
            //cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = primaryacctno;
            //cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = secact;
            //cmd.Parameters.Add(new OracleParameter("p_status", OracleDbType.Int32, ParameterDirection.Output));

            //try
            //{
            //    cmd.ExecuteNonQuery();
            //    res = 1;
            //}
            //catch
            //{
            //    res = 0;

            //}

            //conn.Close();

        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetMassMetersInstalled")]
        public HttpResponseMessage GetMassMetersInstalled(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The Staff was not selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year 
                var DisconnectionList = db.MassMeterCaptures.Where(p => (p.StaffId == Data.StaffId)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetAccountsSeparated")]
        public HttpResponseMessage GetAccountsSeparated(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The Staff was not selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year 
                var DisconnectionList = db.tbl_map_accountseparation_secoderyaccountss.Where(p => (p.aprovedbyid == Data.StaffId)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        //[System.Web.Http.HttpPost]
        //[Route("api/PHEDConnectAPI/GetMAPContractorDetails")]
        //public HttpResponseMessage GetMAPContractorDetails(MAPModel Data)
        //{
        //    RCDCModel d = new RCDCModel();
        //    db = new ApplicationDbContext();

        //    if (Data == null || string.IsNullOrEmpty(Data.MAPVendor))
        //    {
        //        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
        //        var message = string.Format("The MAP Vendor was not selected. Please select and Try again");
        //        HttpError err = new HttpError(message);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //    }
        //    else
        //    {
        //        //convert the Date to DateTime and Get Year 
        //        var DisconnectionList = db.MAP_CONTRACTORS.Where(p => (p.ProviderId == Data.MAPVendor)).ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
        //    }
        //}


         
        #region Enumeration 

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetGeoFencePremises")]
        public HttpResponseMessage GetGeoFencePremises(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            List<BaseEnumerationData> list = new List<BaseEnumerationData>();

            if (Data == null || string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Longitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Latitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Perimeter))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Perimeter was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                using (SqlConnection con = new SqlConnection(EnumerationConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("[RCDC_User].[GetAllCustomerPremiseByDistance]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@RefLat", SqlDbType.Float).Value = Data.Latitude;
                        cmd.Parameters.Add("@RefLong", SqlDbType.Float).Value = Data.Longitude;
                        cmd.Parameters.Add("@RefDistance", SqlDbType.Float).Value = Data.Perimeter;
                         
                        con.Open();
                        //cmd.ExecuteScalar();

                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var PremiseId = rdr[0].ToString();
                                var PremiseCount = rdr[1].ToString();
                                list.Add(new BaseEnumerationData { PremiseId = PremiseId, CustomerCount = PremiseCount });
                                 
                            }

                            con.Close();
                            return Request.CreateResponse(HttpStatusCode.OK, list);
                        }
                    }
                }

            }


            var _message = string.Format("Could not retrieve Premise records.  Please try again Thank you");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.NotFound, _err);
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomersByPremiseId")]
        public HttpResponseMessage GetCustomersByPremiseId(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            List<EnumerationData> list = new List<EnumerationData>();

            if (Data == null || string.IsNullOrEmpty(Data.PremiseId))
            { 
                var message = string.Format("The PremiseId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } 
            else
            {
                using (SqlConnection con = new SqlConnection(EnumerationConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("[RCDC_User].[GetAllCustomersByPremiseId]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PremiseId", SqlDbType.Float).Value = Data.PremiseId;
                        con.Open();
                        //cmd.ExecuteScalar();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var SerialNo = rdr[0].ToString();
                                var AccountNo = rdr[1].ToString(); 
                                var CustomerPhone = rdr[2].ToString();
                                var CustomerName = rdr[3].ToString();
                                var CustomerEmail = rdr[4].ToString();
                                var FlatNo = rdr[5].ToString();
                                var ConnectionType = rdr[6].ToString();
                                var MeterType = rdr[7].ToString();
                                var MeterNo = rdr[8].ToString();
                                var TariffClass = rdr[9].ToString();
                                var PremiseId = rdr[10].ToString();
                                list.Add(new EnumerationData
                                {
                                    SerialNo = SerialNo,
                                    AccountNo = AccountNo,
                                    CustomerName = CustomerName,
                                    CustomerPhone = CustomerPhone,
                                    FlatNo = CustomerPhone,
                                    ConnectionType = ConnectionType,
                                    MeterType = MeterType,
                                    MeterNo = MeterNo,
                                    TariffClass = TariffClass,
                                    PremiseId = PremiseId
                                });
                            }

                            con.Close();
                            return Request.CreateResponse(HttpStatusCode.OK, list);
                        }


                    }
                }

            }


            var _message = string.Format("Could not retrieve Premise records.  Please try again Thank you");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.NotFound, _err);
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DTRSeparationApproval")]
        public HttpResponseMessage DTRSeparationApproval(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            List<EnumerationData> list = new List<EnumerationData>();

            if (Data == null || string.IsNullOrEmpty(Data.SerialNo))
            {
                var message = string.Format("The SerialNo was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
           
            if (Data == null || string.IsNullOrEmpty(Data.ApprovalStatus))
            {
                var message = string.Format("The ApprovalStatus was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.ApprovalDate))
            {
                var message = string.Format("The ApprovalDate was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("The StaffId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                using (SqlConnection con = new SqlConnection(EnumerationConnection))
                {
                    using (var cmd = new SqlCommand("[RCDC_User].[DTRSeparationApproval]", con))
                    {

                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@SerialNo", SqlDbType.Int).Value = Convert.ToInt32( Data.SerialNo);
                            cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = Data.ApprovalStatus;
                            cmd.Parameters.Add("@ApprovalDate", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@ApprovedBy", SqlDbType.VarChar).Value = Data.StaffId;
                            con.Open();
                            cmd.ExecuteNonQuery(); 
                        }
                        catch (Exception ex)
                        { 
                            var ___message = ex.Message;
                            HttpError ___err = new HttpError(___message);
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, ___err); 
                        }
                    }
                }

                var __message = string.Format("Approval Successful");
                HttpError __err = new HttpError(__message);
                return Request.CreateResponse(HttpStatusCode.OK, __err);
            }

            var _message = string.Format("Could not update the Separation records.  Please try again Thank you");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.NotFound, _err);
        }
         
        #endregion
          
        #region  MAP MeterCapture 
       
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetMAPContractorDetails")]
        public HttpResponseMessage GetMAPContractorDetails(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
 
            //if (Data == null || string.IsNullOrEmpty(Data.MAPVendor))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
            //    var message = string.Format("The MAP Vendor was not selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}
            //else
            //{
                //convert the Date to DateTime and Get Year 
               // var DisconnectionList = db.MAP_CONTRACTORS.Where(p => (p.ProviderId == Data.MAPVendor)).ToList();
                var DisconnectionList = db.MAP_CONTRACTORS.ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
           // }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetMAPInstallerDetails")]
        public HttpResponseMessage GetMAPInstallerDetails(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            if (Data == null || string.IsNullOrEmpty(Data.ContractorID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("The ContractorID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year
                var DisconnectionList = db.MAP_INSTALLERS.Where(p => p.ContractorId == Data.ContractorID).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/CheckBillId")]
        public HttpResponseMessage CheckBillId(KYCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (!CheckIfAccountNumberExists(Data.BillId))
            {
                var message = string.Format("The BillId is Wrong or does not exist on the PHED Database. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
             
            if (!CheckIfAccountNumberExists(Data.AccountNo))
            {
                var message = string.Format("The Account No is Wrong or does not exist on the PHED Database. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }


            //#region CheckBill ID
            //KYCModels Prov = new KYCModels();
            //List<KYCModels> _Prov = new List<KYCModels>();


            //conn.Open();

            //OracleDataAdapter da = new OracleDataAdapter();
            //OracleCommand cmd = new OracleCommand
            //{
            //    Connection = conn,
            //    CommandType = CommandType.StoredProcedure,
            //    CommandText = "ENSERV.SP_CheckBillId"
            //};

            //cmd.CommandTimeout = 900;
            //cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            //cmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;
            //cmd.Parameters.Add("BILL_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.BillId;
            //using (OracleDataReader rdr = cmd.ExecuteReader())
            //{
            //    if (rdr.HasRows)
            //    {
            //        while (rdr.Read())
            //        {
            //            Prov = new KYCModels();
            //            //Iterate through the Dataset and Set the Payment history Objects to the Model
            //            Prov.AccountNo = rdr["Incidence"].ToString();
            //            Prov.Status = rdr["PRI_FT_FA_OUT_CRE_COM"].ToString();
            //            _Prov.Add(Prov);
            //        }
            //    }

            //    Customer.ProvisionalOutstanding = _Prov;
            //}

            //conn.Close();
            //conn.Dispose();

            //#endregion


            KYCModels Model = new KYCModels();

            Model.Status = true;
            Model.AccountNo = Data.AccountNo;

            //Insert data into he Application  
            var _message = string.Format("OK");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.OK, Model);
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/UpdateCustomerGenerateCIN")]
        public HttpResponseMessage UpdateCustomerGenerateCIN(EnumsUpdateCustomer updateCustomer)
        {
            string CIN = "";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EnumsConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_updateCustomernoElementAndGenerateCin", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@accountno", SqlDbType.VarChar).Value = updateCustomer.AccountNo ?? null;
                        cmd.Parameters.Add("@Customername", SqlDbType.VarChar).Value = updateCustomer.CustomerName ?? null;
                        cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = updateCustomer.Address ?? null;
                        cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = updateCustomer.Status ?? null;
                        cmd.Parameters.Add("@GSM", SqlDbType.VarChar).Value = updateCustomer.GSM ?? null;
                        cmd.Parameters.Add("@ACCOUNTTYPE", SqlDbType.VarChar).Value = updateCustomer.AccountType ?? null;
                        cmd.Parameters.Add("@MAXDEMAND", SqlDbType.VarChar).Value = updateCustomer.MaxDemand ?? null;
                        cmd.Parameters.Add("@TARIFF", SqlDbType.VarChar).Value = updateCustomer.Tariff ?? null;
                        cmd.Parameters.Add("@METERNO", SqlDbType.VarChar).Value = updateCustomer.MeterNo ?? null;
                        cmd.Parameters.Add("@transformerid", SqlDbType.VarChar).Value = updateCustomer.TransformerId ?? null;
                        cmd.Parameters.Add("@upriser", SqlDbType.VarChar).Value = updateCustomer.Upriser ?? null;
                        cmd.Parameters.Add("@Service_LT_Pole", SqlDbType.VarChar).Value = updateCustomer.ServiceLTPole ?? null;
                        cmd.Parameters.Add("@servicewire", SqlDbType.VarChar).Value = updateCustomer.ServiceWire ?? null;
                        cmd.Parameters.Add("@Customersn", SqlDbType.VarChar).Value = updateCustomer.CustomerSn ?? null;
                        cmd.Parameters.Add("@meteringlat", SqlDbType.Float).Value = updateCustomer.MeteringLat;
                        cmd.Parameters.Add("@meteringlon", SqlDbType.Float).Value = updateCustomer.MeteringLon;
                        cmd.Parameters.Add("@capturedby", SqlDbType.VarChar).Value = updateCustomer.CapturedBy ?? null;

                        if (con.State != ConnectionState.Open)
                            con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();

                        while (rd.Read())
                        {
                            CIN = rd["CIN"].ToString();
                        }

                        var Result = this.Request.CreateResponse(HttpStatusCode.OK, CIN, new JsonMediaTypeFormatter());

                        return Result;
                    }
                    catch (Exception ex)
                    {
                        HttpError Error = new HttpError(ex.Message) { { "IsSuccess", false } };
                        return this.Request.CreateErrorResponse(HttpStatusCode.OK, Error);
                    }
                }
            }
        }
         
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetKYCDataByAccountNo")]
        public HttpResponseMessage GetKYCDataByAccountNo(KYCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext(); 

            if (!CheckIfAccountNumberExists(Data.AccountNo))
            {
                var message = string.Format("The Account No is Wrong or does not exist on the PHED Database. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            { 
                var kyc = db.KYCs.Where(p => p.ACCOUNT_NO == Data.AccountNo).ToList(); 
                return Request.CreateResponse(HttpStatusCode.OK, kyc); 
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/KYCApplicationUpdate")]
        public HttpResponseMessage KYCApplicationUpdate(KYCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext(); 
            if (!CheckIfAccountNumberExists(Data.AccountNo))
            {
                var message = string.Format("The Account No is Wrong or does not exist on the PHED Database. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            { 
                KYC k = new KYC(); 
                k.ACCOUNT_NO = Data.AccountNo;
                k.ACCOUNT_TYPE = Data.AccountType;
                k.ADDRESS = Data.Address;
                k.CustomerAddress = Data.Address;
                k.CustomerMiddleName = Data.Othernames;
                k.CustomerName = Data.FirstName;
                k.CustomerSurname = Data.Surname;
                k.DATE_OF_BIRTH = Convert.ToDateTime(Data.DOB);
                DateTime Date = Convert.ToDateTime(Data.DOB);
                k.DATE_OF_BIRTH = new DateTime?(Date);
                k.DayOfBirth = new int?(Date.Day);
                k.MonthOfBirth = Date.ToString("MMMM"); 
                k.E_MAIL = Data.EmailAddress;
                k.PHONE = Data.PhoneNo;
                k.UPDATED_BY = "CUSTOMER"; 
                k.BVN = Data.BVN;
                k.SMS = Data.SMS;
                k.EmailCheck = Data.EmailCheck;
                k.HardCopy = Data.HardCopy;
                k.ResidentType = Data.Type; 
                k.TENANT_PHONE =   Data.TENANT_PHONE;
                k.TENANT_PHONE2 =  Data.TENANT_PHONE2;
                db.KYCs.Add(k);
                db.SaveChanges(); 
                //Insert data into he Application  
                var message = string.Format("Your Data was saved Successfully. You will begin to get an SMS and Email Notifications from PHED. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);

            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/KYCGetCustomerDetails")]
        public HttpResponseMessage KYCGetCustomerDetails(KYCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (!CheckIfAccountNumberExists(Data.AccountNo))
            {
                var message = string.Format("The Account No is Wrong or does not exist on the PHED Database. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            { 
                DataSet dataSet = new DataSet();
                
                #region GetCustomerInfoFromDenhance

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhance.phed.com.ng/dlenhanceapi/Collection/GetCustomerInfo");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string PhoneNumber = "08067807821";
                string EmailAddress = "customer@Customer.com";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"Username\":\"phed\"," +
                                  "\"apikey\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                                  "\"CustomerNumber\":\"" + Data.AccountNo + "\"," +
                                  "\"Mobile_Number\":\"" + PhoneNumber + "\"," +
                                    "\"Mailid\":\"" + EmailAddress + "\"," +
                                  "\"CustomerType\":\"" + Data.AccountType.ToUpper() + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                KYC p = new KYC();

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

                        return Request.CreateResponse(HttpStatusCode.NotFound, p);

                    }
                    else
                    {

                        var objResponse1 = JsonConvert.DeserializeObject<List<DirectJSON>>(result);
                        p.ACCOUNT_NO = objResponse1[0].CUSTOMER_NO;
                        p.ACCOUNT_TYPE = objResponse1[0].CONS_TYPE;
                        p.ADDRESS = objResponse1[0].ADDRESS;
                        p.E_MAIL = "";
                        p.METER_NO = objResponse1[0].METER_NO;
                        p.PHONE = "";
                        p.CustomerName = objResponse1[0].CONS_NAME;

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, p);

                }
                #endregion

                //string str = "payments@phed.com.ng";
                //string str1 = "";
                //string str2 = "Dlenhance4phed";
                //string str3 = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
               
                //string str4 = ""; 
                //string strPostpaid = ""; 
                //str4 = string.Concat("select AccountNo, Name, Addr1, Addr2, GSM, Email, RowNum from Customers where Email is not null");
                 
                //DataSet _ds = new DataSet();
                //KYC p = new KYC();
                //DeliveredBills Bill = new DeliveredBills();
                //List<DeliveredBills> Bills = new List<DeliveredBills>();
                //string connectionString = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
                //DataSet ds = new DataSet(); 
                //if (Data.AccountType == "PREPAID")
                //{
                //    string[] strPPM = new string[] { " select AccountNo, STSMeterNo as MeterNo, CustName, Address, TelNo as GSM, '' as Email, 'PREPAID' as AccountType from RegisterDataAgg  where (STSMeterNo = '", Data.AccountNo, "' )" };
                //    string _CheckUsername = string.Concat(strPPM);
                //    (new SqlDataAdapter(_CheckUsername, connectionString)).Fill(ds);
                //}
                //else
                //{
                //    string[] strPP = new string[] { " select cast(AccountNo as varchar) as AccountNo, CurrentMeterSerialNo as MeterNo, Name as CustName, LTRIM(Addr1) + ' '+ LTRIM(Addr2) as Address, GSM, Email, 'POSTPAID' as AccountType from customers   where (AccountNo = '", Data.AccountNo, "' )" };
                //    string CheckUsername = string.Concat(strPP);
                //    (new SqlDataAdapter(CheckUsername, connectionString)).Fill(ds);
                //} 
                //if (ds.Tables[0].Rows.Count >= 0)
                //{
                //    p.ACCOUNT_NO = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                //    p.ACCOUNT_TYPE = ds.Tables[0].Rows[0]["AccountType"].ToString();
                //    p.ADDRESS = ds.Tables[0].Rows[0]["Address"].ToString();
                //    p.E_MAIL = ds.Tables[0].Rows[0]["Email"].ToString();
                //    p.METER_NO = ds.Tables[0].Rows[0]["MeterNo"].ToString();
                //    p.PHONE = ds.Tables[0].Rows[0]["GSM"].ToString();
                //    p.CustomerName = ds.Tables[0].Rows[0]["CustName"].ToString();
                //} 
                //return Request.CreateResponse(HttpStatusCode.OK, p);

            }
        }

        private bool CheckIfAccountNumberExists(string p)
        {
            return true;
        }

        //[System.Web.Http.HttpPost]
        //[Route("api/PHEDConnectAPI/InstallMeters")]
        //public HttpResponseMessage GetMAPPlan(MAPModel Data)
        //{

        //    //DataSet pDataset = new DataSet();
        //    //DBManager m_db = new DBManager(DataProvider.Oracle);
        //    //try
        //    //{
        //    //    m_db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        //    //    m_db.Open();
        //    //    m_db.CreateParameters(2);
        //    //    m_db.AddParameters(0, "C_SELECT", "", OracleDbType.RefCursor, ParameterDirection.Output);
        //    //    m_db.AddParameters(1, "P_PLANID", planId, OracleDbType.Varchar2, ParameterDirection.Input);
        //    //    pDataset = m_db.ExecuteDataSet(CommandType.StoredProcedure, "SP_GET_MAPPLAN");


        //    //}
        //    //catch
        //    //{
        //    //    return  return Request.CreateResponse(HttpStatusCode.NotFound, _err);
        //    //}
        //    //finally
        //    //{ m_db.Close(); }


        //    var _message = string.Format("The Meter was  captured Succesfuly.Thank you.");
        //    var _err = new HttpError(_message);
        //    return Request.CreateResponse(HttpStatusCode.NotFound, _err);

        //}


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/InstallMeters")]
        public HttpResponseMessage InstallMeters(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.MeterNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The AccountNo, MeterNo or TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.TicketId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.SealNo1))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo1 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.SealNo2))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo2 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.InstallerId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("The   InstallerId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ContractorID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   ContractorID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }


            //if (Data == null || string.IsNullOrEmpty(Data.AmountPaid))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("The   AmountPaid was not Selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}
            //if (Data == null || string.IsNullOrEmpty(Data.MAPType))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("The   MAPType was not Selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}  

            if (Data == null || string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Latitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Longitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            { 
                var details = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == Data.TicketId);
                MAPController Control = new MAPController();
                if (details != null)
                {
                    //Write to DLEnhance 
                    //Check if the Meter is in the list of installed Meters 

                    if (Control.CheckMeterOnWhiteListForCompatibility(Data.MeterNo, Data.MAPVendor))
                    {
                        

                        //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/UpdateMeterInformation");
                        //httpWebRequest.ContentType = "application/json";
                        //httpWebRequest.Method = "POST";

                        //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        //{
                        //    string json = "{\"Username\":\"phed\"," +
                        //                  "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B20\"," +
                        //                    "\"MeterNo\":\"" + Data.MeterNo + "\"," +
                        //                        "\"InstallerName\":\"" + Data.MAPVendor + "\"," +
                        //                        "\"PoleNo\":\"" + Data.PoleNo + "\"," +
                        //                            "\"DateInstalled\":\"" + DateTime.Now.ToString("dd-MM-yyyy") + "\"," +
                        //                            "\"SealNo1\":\"" + Data.SealNo1 + "\"," +
                        //                            "\"SealNo2\":\"" + Data.SealNo2 + "\"," +
                        //                              "\"AccountNo\":\"" + Data.AccountNo + "\"," +
                        //                                "\"UpdatedBy\":\"" + Data.StaffName + "\"}";
                        //    streamWriter.Write(json);
                        //    streamWriter.Flush();
                        //    streamWriter.Close();
                        //}

                        //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        //{
                        //    var results = streamReader.ReadToEnd();
                        //  results = results.Replace("\r", string.Empty);
                        //  results = results.Replace("\n", string.Empty);
                        //   results = results.Replace(@"\", string.Empty);
                        //   results = results.Replace(@"\\", string.Empty);

                        //check if the Customer Exists here

                        //Parse the JSON here to retrieve the Account Number Sent from DLEnhacne

                        //if (results.Contains(Data.MeterNo))
                        //{

                        try
                        { 
                            string Phase = "";

                            if (details.MeterPhase.Trim() == "THREE PHASE")
                            { 
                                Phase = "3";
                            }
                            else
                            {
                                Phase = "4";
                            }

                            MeterReplacementData obj = new MeterReplacementData();
                            obj.ticketnumber = Data.TicketId;
                            obj.phase = Phase;
                            obj.devicebrand = "METER";
                            obj.manufacturername = details.MAPVendor;
                            obj.devicebrandtype = Data.MAPVendor;
                            obj.meterdeviceserialnumber = Data.MeterNo.Trim();
                            obj.ctr = "0";
                            obj.ptr = "0";
                            obj.meterinstalleddate = DateTime.Now.ToString("dd-MM-yyyy");
                            obj.migir = "0";
                            obj.createdby = "PHEDConnect-" + Data.StaffId;
                            obj.createddatetime = DateTime.Now.ToString("dd-MM-yyyy");
                            obj.meterdigits = "5";
                            obj.metersealnumber1 = Data.SealNo1;
                            obj.meterseal_no2 = Data.SealNo2;
                            obj.terminalsealno1 = Data.TerminalSeal1;
                            obj.terminalsealno2 = Data.TerminalSeal2;
                            obj.meteris = "Pre-paid";
                            obj.consumerno = Data.AccountNo.Replace(" ", "");
                            obj.oldkwh = "0";
                            obj.meterstatus = "1";
                            obj.oldmeterstatus = "OLD";
                            obj.orderno = "CRMD";
                            obj.orderdate = DateTime.Now.ToString("dd-MM-yyyy");
                            obj.meterreplacement = "0";
                            obj.remaininginstallmentsofmr = "0";
                            obj.totalinstallmentsofmr = "60";
                            obj.mfnum = "1";
                            obj.oldmetermanufacturer = Data.MAPVendor;
                            obj.oldmeterinstallationdate = DateTime.Now.ToString("dd-MM-yyyy");
                            obj.oldmeteris = "Pre-paid";
                            obj.oldmeterno = "0";
                            obj.oldmeterdigit = "0";
                            obj.oldmetertype = "0";
                            obj.reason = "MAP Installation";
                            obj.oldmtrownership = "MAP";
                            obj.mtrownership = "MAP";

                            string mtrNo = Data.MeterNo.Trim().Replace(" ", "");
                            int result = 0;

                            //MAP/////// // // // // // / //
                            mPlanData.checkMap = 1;
                            mPlanData.cosumerNo = Data.AccountNo;
                            mPlanData.comDate = DateTime.Now.ToString("dd-MM-yyyy");
                            mPlanData.totalAmount = details.MAPPlanTotalAmount;
                            mPlanData.planID = details.MAPPlanId;
                            mPlanData.meterNo = Data.MeterNo;

                            result = InsertMeterReplacementDetails(obj, mPlanData);
                            // result = 1;
                            
                            if (result > 0)
                            {
                              //Push Meter into the TSM

                                string MeterTechnology = "STS";
                                if(Data.MeterNo.Contains("SM"))
                                {

                                    MeterTechnology = "SPARK";
                                }

                                    //ppmmeterupdate(Data.MeterNo,MeterTechnology, Phase,  Data.AccountNo); 

                                        



                                    details.PoleNo = Data.PoleNo;
                                    details.MeterSeal1 = Data.SealNo1;
                                    details.MeterSeal2 = Data.SealNo2;
                                    details.DateCaptured = DateTime.Now;
                                    details.MAPVendor = Data.MAPVendor;
                                    details.InstalledMeterNo = Data.MeterNo;
                                    details.MeterInstalaltionComment = Data.MeterInstallationComment;
                                    details.MAPApplicationStatus = "METER INSTALLED";
                                    details.InstallerId = Data.InstallerId;
                                    //details.InstalledBy = Data.InstallerName;
                                    details.InstalledBy = Data.MAPVendor;
                                    details.ContactorId = Data.ContractorID;
                                    details.ContractorName = Data.ContractorName;

                                    details.CapturedBy = Data.StaffId ;
                                    details.filePaths = Data.filePaths;
                                    details.Latitude = Data.Latitude;
                                    details.Longitude = Data.Longitude;
                                    details.CapturedByName = Data.StaffName;
                                    db.Entry(details).State = EntityState.Modified;
                                    db.SaveChanges();

                                    string StatusId = Guid.NewGuid().ToString();
                                    GlobalMethodsLib lib = new GlobalMethodsLib();
                                 
                                    string Name = Data.StaffName + " Captured a " + details.MeterPhase.Trim() + " Meter on " + DateTime.Now;

                                    //#region Send Email

                                    //MailMessage mail = new MailMessage();
                                    //mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                                    //mail.Subject = "You Meter has been Captured";
                                    //mail.IsBodyHtml = true;
                                    //mail.Priority = MailPriority.High;
                                    //mail.Bcc.Add("payments@phed.com.ng");
                                    //mail.To.Add(details.CustomerEmail);
                                    //string RecipientType = "";
                                    //string SMTPMailServer = "mail.phed.com.ng";
                                    //SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                                    //MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                                    //string htmlMsgBody = "<html><head></head>";
                                    //htmlMsgBody = htmlMsgBody + "<body>";
                                    //htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + details.MAPCustomerName + ", " + "</P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Your New Meter has been Installed and Captured, Please find below the meter Details" + "</P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + Data.CustomerName + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Capture Date: " + DateTime.Now + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Meter Vendor : " + Data.MAPVendor + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Captured By: " + Data.StaffName + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + Data.AccountNo + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "Meter No: " + Data.MeterNo + " </P>";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + details.TransactionID + " </P>";
                                    //htmlMsgBody = htmlMsgBody + "<br><br>";
                                    //htmlMsgBody = htmlMsgBody + "Thank you,";
                                    //htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                                    //htmlMsgBody = htmlMsgBody + "<br><br>";
                                    //mail.Body = htmlMsgBody;
                                    //MailSMTPserver.Send(mail);

                                    //#endregion

                                    #region Documents


                                    DOCUMENTS Docs = new DOCUMENTS(); 
                                    List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);

                                    string[] filePaths = der.ToArray();

                                    for (int i = 0; i < filePaths.Length; i++)
                                    {
                                        string ImgName = filePaths[i].ToString();
                                        var name = ImgName.Split('.'); 
                                        String filename = name[0];
                                        String fileext = name[3];
                                        Docs = new DOCUMENTS(); 
                                        Docs.COMMENTS = "New Meter Installation Data for " + Data.MeterNo;
                                        Docs.DOCUMENT_NAME = "Meter Installation Data for " + Data.MeterNo;
                                        Docs.DocumentDescription = Data.MeterInstallationComment;
                                        Docs.STATUS = "METER CAPTURE"; 
                                        Docs.DATE_UPLOADED = DateTime.Now;
                                        Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                                        Docs.DOCUMENT_EXTENSION = fileext; 
                                        Docs.DOCUMENT_PATH = filePaths[i].ToString(); 
                                        Docs.REFERENCE_CODE = Data.MeterNo; 
                                        lib.AuditTrail(Data.UserId, Name.ToUpper(), DateTime.Now, StatusId, "", "METER CAPTURE"); 
                                        Docs.SENDER_ID = Data.StaffId; 
                                        Docs.Size = "123KB";
                                        db.DOCUMENTSs.Add(Docs);
                                        db.SaveChanges();
                                    }

                                    #endregion 
                                
                                
                                    var ___message = string.Format("The Meter was  captured Successfully.Thank you.");
                                    var ___err = new HttpError(___message);
                                    return Request.CreateResponse(HttpStatusCode.OK, ___err);
                                 
                                //}
                                //catch (Exception ex)
                                //{

                                //     HttpError err = new HttpError("The Meter could not be Installed because " + ex.Message);
                                //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                                //} 
                            }
                            else
                            { 
                                var message = string.Format("An Error Occured and the Meter could not be captured in DLEnhance Please try again later. Thank you");
                                HttpError err = new HttpError(message);
                                return Request.CreateResponse(HttpStatusCode.NotFound, err); 
                            } 
                        }
                        catch (Exception ex)
                        { 
                            HttpError err = new HttpError("The Meter could not be Installed because " + ex.Message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }
                    else
                    {
                        //return Error that this Meter is not in the List of  meters to Be Captured 
                        var message = string.Format("This meter has not been whitelisted, and may be a Stolen Meter. Please refer to PHED to resolve. Thank you.");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    } 
                }
                else
                {
                    //return Error that this Meter is not in the List of  meters to Be Captured 
                    var message = string.Format("This TicketID has not requested for a MAP Meter Installation. Please cross-check and try again. Thank you.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

                var __message = string.Format("The Meter was  captured Successfully.Thank you.");
                var __err = new HttpError(__message);
                return Request.CreateResponse(HttpStatusCode.OK, __err);
            }
        }

        public static int checkLuhn(string str)
        {

            int s = 0, tmp = 0;
            for (int i = 1; i <= str.Length; i++)
            {
                tmp = System.Convert.ToInt32(str.Substring(str.Length - i, 1));
                if (i % 2 > 0)
                    s = s + tmp;
                else if (tmp < 5)
                    s = s + (tmp * 2);
                else
                    s = s + 1 + ((tmp * 2) % 10);
            }

            return (10 - (s % 10)) % 10;
        }

        public void PostToSparkAPI(string url, string data)
        {

            string vystup = null;
            try
            {
                //Our postvars
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
                //Initialisation, we use localhost, change if appliable
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
                //Our method is post, otherwise the buffer (postvars) would be useless
                WebReq.Method = "POST";
                //We use form contentType, for the postvars.
                WebReq.ContentType = "application/x-www-form-urlencoded";
                //if (stsmeter.SelectedValue.ToString() == "1")
                //{
                string credentials = String.Format("{0}:{1}", "FeedbackInfo", "asficctofbi2018");

                byte[] bytes = Encoding.ASCII.GetBytes(credentials);
                string base64 = Convert.ToBase64String(bytes);
                string authorization = String.Concat("Basic ", base64);
                WebReq.Headers.Add("Authorization", authorization);
                WebReq.Headers.Add("Authentication-Token", SPARKTOKEN);
                WebReq.UseDefaultCredentials = true;
                //}
                //The length of the buffer (postvars) is used as contentlength.
                WebReq.ContentLength = buffer.Length;
                //We open a stream for writing the postvars
                Stream PostData = WebReq.GetRequestStream();
                //Now we write, and afterwards, we close. Closing is always important!
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                //Get the response handle, we have no true response yet!
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                //Now, we read the response (the string), and output it.
                Stream Answer = WebResp.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                vystup = _Answer.ReadToEnd();
                //Congratulations, you just requested your first POST page, you
                //can now start logging into most login forms, with your application
                //Or other examples.
            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterStartupScript(Page, GetType(), "showalert", "<script>callError('Error in saving to API');</script>", false);
            }
            // return vystup.Trim() + "\n";

        }

        public void PostToSTSAPI(string url, string data)
        {

            string vystup = null;
            string idrecord, vendtimeunix, tokenHex, tokendec = "";
            Token tkn = new Token();

            try
            {
                var client = new WebClient();
                var method = "POST";
                var parameters = new NameValueCollection();
                parameters.Add("subclass", "5");
                parameters.Add("meterId", data);
                //parameters.Add("value", "0");

                var response_data = client.UploadValues(url, method, parameters);
                var responseString = UnicodeEncoding.UTF8.GetString(response_data);
                XDocument XDocuments = XDocument.Parse(responseString);

                List<Token> tikenList = XDocuments.Descendants("response").Select(d =>
                new Token
                {
                    idRecord = d.Element("idRecord").Value,
                    subclass = d.Element("subclass").Value,
                    description = d.Element("description").Value,
                    vendTimeUnix = d.Element("vendTimeUnix").Value,
                    unitsActual = d.Element("unitsActual").Value,
                    unitName = d.Element("unitName").Value,
                    tokenHex = d.Element("tokenHex").Value,
                    tokenDec = d.Element("tokenDec").Value
                }).ToList();
                foreach (var str in tikenList)
                {
                    tkn.idRecord = str.idRecord.ToString();
                    tkn.vendTimeUnix = str.vendTimeUnix.ToString();
                    tkn.tokenDec = str.tokenDec.ToString();
                    tkn.tokenHex = str.tokenHex.ToString();
                }
                //string[] tokens = responseString.Split('\n');
                //idrecord = tokens[0];
                //vendtimeunix = tokens[2];
                //tokenHex = tokens[5];
                //tokendec = tokens[6];
                idrecord = tkn.idRecord.ToString();
                vendtimeunix = tkn.vendTimeUnix.ToString();
                tokenHex = tkn.tokenHex.ToString();
                tokendec = tkn.tokenDec.ToString();

                string action = "1";
                //List<Token> tkn = new List<Token>();
//int rtnval = ApiTransaction.Insertvendlog(txtconsumerno.Text, tkn.idRecord, tkn.vendTimeUnix, tkn.tokenDec, tokendec, action, "0", "");
                List<Token> newlist = new List<Token>();
                newlist.Add(new Token() { idRecord = idrecord, tokenDec = tokendec, tokenHex = tokenHex });

                //}
            }
            catch (Exception ex)
            {
              //  int rtnval = ApiTransaction.Insertvendlog(txtconsumerno.Text, "", "", "", "", "", "1", ex.Message + "PREPIAD METER REPLACEMENT");
                //ScriptManager.RegisterStartupScript(Page, GetType(), "showalert", "<script>callError('Error in saving to API');</script>", false);
            }
            // return vystup.Trim() + "\n";

        }

        public string GetMeterNo(string mtrno, string index)
        {
            string panmeter = "";

            if (mtrno.Length == 11)
            {

                panmeter = "600727" + mtrno;

            }

            else if (mtrno.Length == 13)
            {

                panmeter = "0000" + mtrno;

            }



            int panno = checkLuhn(panmeter + "0");



            panmeter = panmeter + panno.ToString();

            panmeter = panmeter + "00000207600289" + index + "1";

            return panmeter;

        }

        //public static int InsertPrepaidMeterReplacement(string oldCustomerNo, string newCustomerNo, string oldMtrNo, string newMtrNo, string meterMake, string instalDate, string userName, string reason, string mtrownership, MscPlan mp)
        //{
        //    int result = 0;
        //    DBManager m_db = new DBManager(DataProvider.Oracle);
        //    try
        //    {
        //        m_db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        //        m_db.Open();
        //        m_db.CreateParameters(16);
        //        m_db.AddParameters(0, "p_result", 10, OracleDbType.Int32, ParameterDirection.Output);
        //        m_db.AddParameters(1, "p_oldcustno", oldCustomerNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(2, "p_newcustno", newCustomerNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(3, "p_oldmtrno", oldMtrNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(4, "p_newmtrno", newMtrNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(5, "p_mtrmake", meterMake, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(6, "p_instaldate", instalDate, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(7, "p_createdby", userName, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(8, "p_reason", reason, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(9, "p_mtrownership", mtrownership, OracleDbType.Varchar2, ParameterDirection.Input);

        //        //======================INSERT INTO MAP PAYMENT CONTROLLER=============================//
        //        m_db.AddParameters(10, "P_CHECKMAP", mp.checkMap, OracleDbType.Int16, ParameterDirection.Input);
        //        m_db.AddParameters(11, "P_PLANID", mp.planID, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(12, "P_CONSUMERNO_MAP", mp.cosumerNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(13, "P_METERNO_MAP", mp.meterNo, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(14, "P_COMMENCEMENT_DATE", mp.comDate, OracleDbType.Varchar2, ParameterDirection.Input);
        //        m_db.AddParameters(15, "P_PAYBLEAMOUNT", mp.totalAmount, OracleDbType.Varchar2, ParameterDirection.Input);
        //        //==================END OF INSERT INTO MAP PAYMENT CONTROLLER==========================//

        //        m_db.ExecuteNonQuery(CommandType.StoredProcedure, "SP_PREPAIDMTR_REPLACE");
        //        //result = Int32.Parse(m_db.ExecuteScalar(CommandType.StoredProcedure, "SP_PREPAIDMTR_REPLACE").ToString());
        //        //m_db.ExecuteScalar(CommandType.StoredProcedure, "SP_PREPAIDMTR_REPLACE").ToString();
        //        //result = Int32.Parse(((OracleParameter)(m_db.Parameters[0])).Value.ToString());
        //        result = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        m_db.Close();
        //    }
        //    return result;
        //}

        public void ppmmeterupdate(string meternom, string MeterTechnology,string Phase,string AccountNo)
        {

            //singe phase 4
            //Three Phase 3
             
            if (MeterTechnology == "SPARK")
            {
                PostToSparkAPI(SPARKSETUPURI, "serial=1&meter_tariff_name=2");
            }
            else
            {
                int phase = 1; string index = "1";
                string consumerno = AccountNo;
                if (Phase == "3")
                {
                    phase = 2;
                    index = "2";
                }

               // DataSet ds1 = GetStsMeterDeails(consumerno, phase);

               // string consmtrno = ds1.Tables[0].Rows[0]["CONS_METERNO"].ToString();

               
                
                // ds1.Tables[0].Rows[0]["STS_INDEX"].ToString();

                string Meter_PAN = GetMeterNo(meternom, index);
                //int Meter_PAN = checkLuhn(Meter_PAN + "0");
                String meterId = Meter_PAN + "0000" + "02" + "07" + "600289" + index + "1";
                //PostToSTSAPI(STSSETUPURL, "subclass=5&meterId=" + Meter_PAN);
                PostToSTSAPI(STSSETUPURL, Meter_PAN);
            }
        }

        public static int InsertMeterReplacementDetails(MeterReplacementData obj, MscPlan mp)
        {
           
            OracleConnection con = new OracleConnection(strConnString);
            OracleCommand cmd = new OracleCommand();
            OracleParameter param = new OracleParameter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ENSERV.SP_INSERTMETER_REPLACEMENT_INCIDENT";
            cmd.Parameters.Add("c_result", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_ticketnumber", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.ticketnumber;
            cmd.Parameters.Add("p_phase", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.phase;
            cmd.Parameters.Add("p_devicebrand", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.devicebrand;
            cmd.Parameters.Add("p_manufacturername", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.manufacturername;
            cmd.Parameters.Add("p_devicebrandtype", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.devicebrandtype;
            cmd.Parameters.Add("p_meterdeviceserialnumber", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterdeviceserialnumber;
            cmd.Parameters.Add("p_ctr", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.ctr;
            cmd.Parameters.Add("p_ptr", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.ptr;
            //cmd.Parameters.Add("p_meterowner", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterowner;
            cmd.Parameters.Add("p_meterinstalleddate", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterinstalleddate;
            cmd.Parameters.Add("p_migir", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.migir;
            cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.createdby;
            cmd.Parameters.Add("p_createddatetime", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.createddatetime;
            cmd.Parameters.Add("p_meterdigits", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterdigits;
            cmd.Parameters.Add("p_metersealnumber1", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.metersealnumber1;
            cmd.Parameters.Add("p_meterseal_no2", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterseal_no2;
            cmd.Parameters.Add("p_terminalsealno1", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.terminalsealno1;
            cmd.Parameters.Add("p_terminalsealno2", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.terminalsealno2;
            cmd.Parameters.Add("p_meteris", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meteris;
            cmd.Parameters.Add("p_consumerno", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.consumerno;
            cmd.Parameters.Add("p_oldkwh", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldkwh;
            cmd.Parameters.Add("p_meterstatus", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterstatus;
            cmd.Parameters.Add("p_oldmeterstatus", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeterstatus;
            cmd.Parameters.Add("p_orderno", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.orderno;
            cmd.Parameters.Add("p_orderdate", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.orderdate;
            cmd.Parameters.Add("p_meterreplacement", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterreplacement;
            //cmd.Parameters.Add("p_meterrentflag", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.meterrentflag;
            cmd.Parameters.Add("p_remaininginstallmentsofmr", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.remaininginstallmentsofmr;
            cmd.Parameters.Add("p_totalinstallmentsofmr", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.totalinstallmentsofmr;
            cmd.Parameters.Add("p_mfnum", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.mfnum;
            cmd.Parameters.Add("p_oldmetermanufacturer", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmetermanufacturer;
            cmd.Parameters.Add("p_oldmeterinstallationdate", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeterinstallationdate;
            cmd.Parameters.Add("p_oldmeteris", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeteris;
            //cmd.Parameters.Add("p_oldmeterowner", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeterowner;
            cmd.Parameters.Add("p_oldmeterno", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeterno;
            cmd.Parameters.Add("p_oldmeterdigit", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmeterdigit;
            cmd.Parameters.Add("p_oldmetertype", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmetertype;
            cmd.Parameters.Add("p_reason", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.reason;
            cmd.Parameters.Add("p_oldmtrownership", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.oldmtrownership;
            cmd.Parameters.Add("p_mtrownership", OracleDbType.Varchar2, ParameterDirection.Input).Value = obj.mtrownership; 
            //======================INSERT INTO MAP PAYMENT CONTROLLER=============================//
            cmd.Parameters.Add("P_CHECKMAP", OracleDbType.Int16, ParameterDirection.Input).Value = mp.checkMap;
            cmd.Parameters.Add("P_PLANID", OracleDbType.Varchar2, ParameterDirection.Input).Value = mp.planID;
            cmd.Parameters.Add("P_CONSUMERNO_MAP", OracleDbType.Varchar2, ParameterDirection.Input).Value = mp.cosumerNo;
            cmd.Parameters.Add("P_METERNO_MAP", OracleDbType.Varchar2, ParameterDirection.Input).Value = mp.meterNo;
            cmd.Parameters.Add("P_COMMENCEMENT_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = mp.comDate;
            cmd.Parameters.Add("P_PAYBLEAMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = mp.totalAmount;
            //==================END OF INSERT INTO MAP PAYMENT CONTROLLER==========================//
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                int i = int.Parse(cmd.Parameters["c_result"].Value.ToString());
                i = 1;
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

         

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/InstallMeters111")]
        public HttpResponseMessage InstallMeters111(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.MeterNo))
            {
                var message = string.Format("The AccountNo, MeterNo or TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.TicketId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.SealNo1))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo1 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.SealNo2))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   SealNo2 was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.InstallerId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("The   InstallerId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ContractorID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   ContractorID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }


            //if (Data == null || string.IsNullOrEmpty(Data.AmountPaid))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("The   AmountPaid was not Selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            //if (Data == null || string.IsNullOrEmpty(Data.MAPType))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("The   MAPType was not Selected. Please select and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            if (Data == null || string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Latitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Longitude was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                CustomerPaymentInfo details = db.CustomerPaymentInfos.Find(Data.TicketId);
                MAPController Control = new MAPController();
                if (details != null)
                {
                    //Write to DLEnhance 
                    //Check if the Meter is in the list of installed Meters 

                    if (Control.CheckMeterOnWhiteListForCompatibility(Data.MeterNo, Data.MAPVendor))
                    {
                        #region Write to DLEnhance

                        //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dlenhancetraining.phed.com.ng/dlenhanceapi/MAP/UpdateMeterInformation");
                        //httpWebRequest.ContentType = "application/json";
                        //httpWebRequest.Method = "POST";

                        //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        //{
                        //    string json = "{\"Username\":\"phed\"," +
                        //                  "\"APIKEY\":\"2E639809-58B0-49E2-99D7-38CB4DF2B5B2\"," +
                        //                    "\"MeterNo\":\"" + Data.MeterNo + "\"," +
                        //                        "\"InstallerName\":\"" + Data.MAPVendor + "\"," +
                        //                        "\"PoleNo\":\"" + Data.PoleNo + "\"," +
                        //                            "\"DateInstalled\":\"" + DateTime.Now.ToString("dd-MM-yyyy") + "\"," +
                        //                            "\"SealNo1\":\"" + Data.SealNo1 + "\"," +
                        //                            "\"SealNo2\":\"" + Data.SealNo2 + "\"," +
                        //                              "\"AccountNo\":\"" + Data.AccountNo + "\"," +
                        //                                "\"UpdatedBy\":\"" + Data.StaffName + "\"}";
                        //    streamWriter.Write(json);
                        //    streamWriter.Flush();
                        //    streamWriter.Close();
                        //}

                        //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        //{
                        //    var results = streamReader.ReadToEnd();
                        //    results = results.Replace("\r", string.Empty);
                        //    results = results.Replace("\n", string.Empty);
                        //    results = results.Replace(@"\", string.Empty);
                        //    results = results.Replace(@"\\", string.Empty);

                        //    //check if the Customer Exists here

                        //    //Parse the JSON here to retrieve the Account Number Sent from DLEnhacne



                        //    if (results.Contains(Data.MeterNo))
                        //    {

                        string StaffName = db.Users.FirstOrDefault(p => p.Id == Data.StaffId).StaffName;

                        details.PoleNo = Data.PoleNo;
                        details.MeterSeal1 = Data.SealNo1;
                        details.MeterSeal2 = Data.SealNo2;
                        details.DateCaptured = DateTime.Now;
                        details.MAPVendor = Data.MAPVendor;
                        details.InstalledMeterNo = Data.MeterNo;
                        details.MeterInstalaltionComment = Data.MeterInstallationComment;
                        details.MAPApplicationStatus = "METER INSTALLED";
                        details.InstallerId = Data.InstallerId;
                        details.InstalledBy = Data.InstallerName;
                        details.InstalledBy = Data.MAPVendor;
                        details.ContactorId = Data.ContractorID;
                        details.ContractorName = Data.ContractorName;


                        details.CapturedBy = Data.StaffId;
                        details.Latitude = Data.Latitude;
                        details.Longitude = Data.Longitude;
                        details.CapturedByName = StaffName;
                        db.Entry(details).State = EntityState.Modified;
                        db.SaveChanges();

                        string StatusId = Guid.NewGuid().ToString();
                        GlobalMethodsLib lib = new GlobalMethodsLib();
                        string Name = StaffName + " Captured a " + details.MeterPhase.Trim() + " Meter on " + DateTime.Now;
                        lib.AuditTrail(Data.StaffId, Name.ToUpper(), DateTime.Now, StatusId, "", "METER CAPTURE");



                        #region Send Email

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("Payments@phed.com.ng", "Port-Harcourt Electricity Distribution Company");
                        mail.Subject = "You Meter has been Captured";
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        mail.Bcc.Add("payments@phed.com.ng");
                        mail.To.Add(details.CustomerEmail);
                        string RecipientType = "";
                        string SMTPMailServer = "mail.phed.com.ng";

                        SmtpClient MailSMTPserver = new SmtpClient(SMTPMailServer);
                        MailSMTPserver.Credentials = new NetworkCredential("payments@phed.com.ng", "Dlenhance4phed");
                        //  string htmlMsgBody = this.EmailTextBox.Text;
                        string htmlMsgBody = "<html><head></head>";
                        htmlMsgBody = htmlMsgBody + "<body>";
                        //  htmlMsgBody = htmlMsgBody + "<P>" + "<div>" + "<img> " + "</div>" + "</P>";
                        htmlMsgBody = htmlMsgBody + "<P>" + "Dear " + details.MAPCustomerName + ", " + "</P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Your New Meter has been Installed and Captured, Please find below the meter Details" + "</P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Customer's Name: " + Data.CustomerName + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Capture Date: " + DateTime.Now + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Meter Vendor : " + Data.MAPVendor + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Captured By: " + StaffName + " </P>";
                        htmlMsgBody = htmlMsgBody + " <P> " + "Account No: " + Data.AccountNo + " </P>";

                        htmlMsgBody = htmlMsgBody + " <P> " + "Meter No: " + Data.MeterNo + " </P>";


                        htmlMsgBody = htmlMsgBody + " <P> " + "TicketID: " + details.TransactionID + " </P>";

                        htmlMsgBody = htmlMsgBody + "<br><br>";
                        htmlMsgBody = htmlMsgBody + "Thank you,";
                        htmlMsgBody = htmlMsgBody + " <P> " + "PHED MAP Team" + " </P> ";
                        htmlMsgBody = htmlMsgBody + "<br><br>";
                        mail.Body = htmlMsgBody;

                        MailSMTPserver.Send(mail);

                        #endregion



                        //}

                        //else
                        //{

                        //    var message = string.Format("The Meter Could not be captured Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                        //    HttpError err = new HttpError(message);
                        //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        //}
                    }

                        #endregion
                }
                else
                {
                    //return Error that this Meter is not in the List of  meters to Be Captured 
                    var message = string.Format("THis meter was not Approved for Installation. Please check and try again. Thank you.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                } 
            }

            var _message = string.Format("The Meter was  captured Succesfuly.Thank you.");
            var _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.OK, _err);
        }



        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetMAPCustomerDetails")]
        public HttpResponseMessage GetMAPCustomerDetails(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The AccountNo, MeterNo or TicketID was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year 
               var  DisconnectionList = db.CustomerPaymentInfos.Where(p => (p.CustomerReference == Data.AccountNo || p.AlternateCustReference == Data.AccountNo || p.TransactionID == Data.AccountNo)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        #endregion
        
        #region KYC
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomerKYCData")]
        public HttpResponseMessage GetCustomerKYCData()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            DateTime Date = DateTime.Now;

            int? DayOfBirth = new int?(Date.Day);
            string MonthOfBirth = Date.ToString("MMMM");

            try
            {
                var KYCList = db.KYCs.Where(p => p.DayOfBirth == DayOfBirth && p.MonthOfBirth.ToUpper() == MonthOfBirth.ToUpper()).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, (KYCList));
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/KYCCommunicationReport")]
        public HttpResponseMessage KYCCommunicationReport(KYCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                KYCReport j = new KYCReport();

                j.ACCOUNT_NO = Data.AccountNo;
                j.CustomerMiddleName = Data.Othernames;
                j.CustomerSurname = Data.Surname;
                j.EmailStatus = Data.EmailStatus;
                j.SMSStatus = Data.SMSStatus;
                j.DateSent = DateTime.Now;


                db.KYCReports.Add(j);
                db.SaveChanges();
                var message = string.Format("Saved Successfully. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetStaffSuggestions")]
        public HttpResponseMessage  GetStaffSuggestions(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                StaffSuggestions j = new StaffSuggestions(); 
                j.StaffId = Data.StaffId;
                j.StaffName = Data.StaffName;
                j.StaffPhone = Data.StaffPhone;
                j.StaffEmail = Data.Email;
                j.Comments = Data.Comments;
                j.DateCommented = DateTime.Now;
                j.Latitude = Data.Latitude;
                j.Longitude = Data.Longitude;
                j.ModuleName = Data.ModuleName;
                db.StaffSuggestionss.Add(j);
                db.SaveChanges();
                var message = string.Format("Saved Successfully. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.OK, err);
            }
        }

        #endregion

        #region Authentication
         

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/Login")]
        public RCDCModel Login(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            if (Data == null || string.IsNullOrEmpty(Data.Password) || string.IsNullOrEmpty(Data.UserName))
            {
                d.Status = "ERROR";
                d.Message = "The username or Password was not supplied. Please supply the Username or password to continue.";

                return d;
            }

            string Username = Data.UserName;
            string Password = Data.Password;
            string IMEI = Data.DeviceIMEI;
            AccountController ll = new AccountController();
            string URL = "";
            d = ll.LoginAPI(Username, Password, IMEI);
            return d;
        }

        #endregion

        #region Customer LookUp

        /// <summary>
        /// Get the Customer's Data by his Account Number. This returns his Name, Account, LastDatePaid, DTRExecutive, Address, Zone, Feeder, Arrears, DTR_Name AVG_consumption
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        /// 

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomerDataByAccountNo")]
        public HttpResponseMessage GetCustomerDataByAccountNo(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.DisconnId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Disconnection Id to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                try
                {
                    conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString());
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ENSERV.SP_WF_GET_CUSTOMERDATA_BY_ACCOUNTNO"
                };
                int Count = 6;
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                cmd.Parameters.Add("P_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;
                cmd.Parameters.Add("P_COUNT", OracleDbType.Int64, ParameterDirection.Input).Value =Count;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        RCDCCustomer Customer = new RCDCCustomer();
                        List<RCDCCustomerPayments> Pay = new List<RCDCCustomerPayments>();

                        RCDCCustomerPayments _pay = new RCDCCustomerPayments();
                        var DefaultingCustomer = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId);
                        if (DefaultingCustomer == null)
                        {
                            conn.Close();
                            conn.Dispose();
                            cmd.Dispose();
                            var message = string.Format("It seems the Disconnection Id you passed in is wrong, kindly crosscheck and try again.");
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);

                        }
                        else
                        {
                            Customer.AccountName = DefaultingCustomer.AccountName;
                            Customer.AccountNo = DefaultingCustomer.AccountNo;
                            Customer.Address = DefaultingCustomer.Address;
                            Customer.Arrears = DefaultingCustomer.Arrears;
                            Customer.Avg_Consumption = DefaultingCustomer.AvgConsumption;
                            Customer.CIN = DefaultingCustomer.CIN;
                            Customer.DisconnectionStatus = DefaultingCustomer.DisconStatus;
                            Customer.DTR_Exec_Email = DefaultingCustomer.DTR_Exec_Email;
                            Customer.DTR_Exec_Name = DefaultingCustomer.DTR_Exec_Name;
                            Customer.DTR_Exec_Phone = DefaultingCustomer.DTR_Exec_Phone;
                            Customer.DTR_Name = DefaultingCustomer.DTR_Name;
                            Customer.Feeder = DefaultingCustomer.FeederName;
                            Customer.IncidenceHistory = db.RCDC_Disconnection_Incidence_Historys.Where(p => p.DisconnId == Data.DisconnId).ToList();
                            while (rdr.Read())
                            {

                                _pay = new RCDCCustomerPayments();
                                //Iterate through the Dataset and Set the Payment history Objects to the Model
                                _pay.AmountPaid = Convert.ToDouble(rdr[2].ToString());
                                _pay.DatePaid = (DateTime)(rdr[3]);
                                _pay.PaymentDescription = rdr[4].ToString();
                                _pay.PaymentID = rdr[1].ToString();
                                Pay.Add(_pay);
                            }
                            Customer.PaymentHistory = Pay;
                        }

                        conn.Close();
                        conn.Dispose();
                        cmd.Dispose();
                        return Request.CreateResponse(HttpStatusCode.OK, (Customer));
                    }
                    else
                    {
                        var message = string.Format("No Account record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
                }
                catch (Exception exception1)
                {
                    conn.Close();
                    conn.Dispose();
                    Exception exception = exception1;
                    var message = string.Format("Could not retrieve Customer records because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/getCustomerDetailsByAccountNo")]
        public HttpResponseMessage getCustomerDetailsByAccountNo(RCDCModel Data)
        {
            try
            {
            GlobalMethodsLib DTRExec = new GlobalMethodsLib(); 
            db = new ApplicationDbContext();
            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString());
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_WF_GET_CUSTOMERBYACCOUNTNO"
                };
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                cmd.Parameters.Add(new OracleParameter("P_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input)).Value = Data.AccountNo;
                // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        RCDCCustomer Customer = new RCDCCustomer();
                        List<RCDCCustomerPayments> Pay = new List<RCDCCustomerPayments>();
                        RCDCCustomerPayments _pay = new RCDCCustomerPayments();
                        int Count = 6;
                        RCDC_DisconnectionList rcdcda = new RCDC_DisconnectionList();
                        string DisconID = Guid.NewGuid().ToString();
                        while (rdr.Read())
                        {
                            rcdcda = new RCDC_DisconnectionList();
                            //Save the List to the Database here
                            string _AccountNo = rdr[11].ToString();
                            string AccountName = rdr[8].ToString();
                            string Address = rdr[9].ToString();
                            string CIN = rdr[14].ToString();
                            string DTR_Name = rdr[16].ToString();
                            string DTR_Code = rdr[15].ToString();
                            string FeederID = rdr[2].ToString();

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

                            string MeterNo = rdr[10].ToString();
                            string FeederName = rdr[1].ToString();
                            string ZoneName = rdr[0].ToString();
                            string Band = rdr[12].ToString();
                            string Arrears = "";

                            string ConsumerStatus = rdr[18].ToString();
                            string LastPaymentDate = rdr[24].ToString();
                            string LastPaymentAmount = rdr[25].ToString();
                            //string AccountType = dataSet1.Tables[0].Rows[i]["cons_type"].ToString();
                            string DisconReason = "DISCONNECT";
                            string _AccountType = rdr[19].ToString();
                            string TariffCode = rdr[23].ToString();
                            string GeneratedBy = "";

                            if (_AccountType == "PREPAID")
                            {
                                Arrears = rdr[22].ToString();

                            }
                            else
                            {
                                Arrears = rdr[21].ToString();

                            }
                            var checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == _AccountNo).ToList();

                            if (checkAm.Count > 0)
                            {

                                //update the Data and then return it
                                string DIsconId = checkAm.FirstOrDefault().DisconID;
                                RCDC_DisconnectionList ff = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == DIsconId);
                                if (ff != null)
                                {
                                    ff.LastPayDate = LastPaymentDate;
                                    ff.AmountPaid = LastPaymentAmount;
                                    ff.Arrears = Arrears;
                                    ff.MeterNo = MeterNo;
                                    ff.Zone = ZoneName;
                                    ff.FeederId = FeederID;
                                    ff.FeederName = FeederName;
                                    ff.DTR_Exec_Email = DTR_Exec_Email;
                                    ff.DTR_Exec_Name = DTR_Exec_Name;
                                    ff.DTR_Exec_Phone = DTR_Exec_Phone;
                                    ff.DTR_Id = DTR_Code;
                                    ff.DTR_Name = DTR_Name;
                                    ff.DTRCode = DTR_Code;
                                    ff.FeederId = FeederID;
                                    ff.MeterNo = MeterNo;
                                    ff.Band = Band;
                                    ff.AccountType = _AccountType;
                                    ff.ConsumerStatus = ConsumerStatus;
                                    db.Entry(ff).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                                conn.Close();
                                var _checkAm = db.RCDCDisconnectionLists.Where(p => p.AccountNo == _AccountNo && (p.DisconStatus == "DISCONNECT" || p.DisconStatus == "DISCONNECTED" || p.DisconStatus == "RECONNECT" || p.DisconStatus == "RECONNECTED")).ToList();
                                return Request.CreateResponse(HttpStatusCode.OK, (_checkAm));
                            }
                            rcdcda.AccountNo = _AccountNo;
                            rcdcda.AccountName = AccountName;
                            rcdcda.AccountType = _AccountType;
                            rcdcda.Address = Address;
                            rcdcda.Arrears = Arrears;
                            if (_AccountType == "PREPAID")
                            {
                                rcdcda.AvgConsumption = "0";
                            }
                            else
                            {
                                rcdcda.AvgConsumption = rdr[20].ToString();
                            }
                            rcdcda.CIN = CIN;
                            rcdcda.ConsumerStatus = ConsumerStatus;
                            rcdcda.DateGenerated = DateTime.Now;
                            rcdcda.DisconID = DisconID;
                            rcdcda.DisconStatus = DisconReason;
                            rcdcda.DTR_Exec_Email = DTR_Exec_Email;
                            rcdcda.DTR_Exec_Name = DTR_Exec_Name;
                            rcdcda.DTR_Exec_Phone = DTR_Exec_Phone;
                            rcdcda.DTR_Id = DTR_Code;
                            rcdcda.DTR_Name = DTR_Name;
                            rcdcda.DTRCode = DTR_Code;
                            rcdcda.FeederId = FeederID;
                            rcdcda.MeterNo = MeterNo;
                            rcdcda.FeederName = FeederName;
                            rcdcda.LastPayDate = LastPaymentDate;
                            rcdcda.AmountPaid = LastPaymentAmount;
                            rcdcda.Zone = ZoneName;
                            rcdcda.FeederId = FeederID;
                            rcdcda.Band = Band;
                            rcdcda.GeneratedBy = "RPD";
                            rcdcda.Zone = rdr[0].ToString();
                            string Phase = "";
                            if (rdr[17].ToString() == "4")
                            {
                                rcdcda.Phase = "1";
                            }
                            else
                            {
                                rcdcda.Phase = rdr[17].ToString();
                            }

                            rcdcda.Tariff = TariffCode;

                            db.RCDCDisconnectionLists.Add(rcdcda);
                            db.SaveChanges();

                        }
                        var DefaultingCustomer = db.RCDCDisconnectionLists.Where(p => p.DisconID == DisconID).ToList();

                        return Request.CreateResponse(HttpStatusCode.OK, (DefaultingCustomer));
                    }
                    else
                    {
                        var message = string.Format("No Account record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
               
                
                
            }
                }

                catch (Exception exception1)
                {
                Exception exception = exception1;
                //dBManager.Close(); dBManager.Dispose();
                var message = string.Format("Could not retrieve Customer records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/ActivateCustomerxx")]
        public HttpResponseMessage ActivateCustomerxx(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();

            GlobalMethodsLib DTRExec = new GlobalMethodsLib();
             
            db = new ApplicationDbContext();
  if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            { 
                var message = string.Format("Please select an Staff Id to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            { 
                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);
                DataSet dataSet = new DataSet();


                #region New Line


                string AccountNo = Data.AccountNo;
                string str = @"update ENSERV.tbl_consmastxx set CON_CONSUMERSTATUS = 'Active', modifiedby = '" + "PHEDConnect-" + Data.StaffId.Trim() + "', modifieddatetime = sysdate where CONS_acc = '" + Data.AccountNo.Trim() + "' and not(CON_CONSUMERSTATUS = 'Active')";

            
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = str,
                    CommandTimeout = 1600,
                    
                };

              
                 
                conn.Open();
                try
                {
                    int aff = cmd.ExecuteNonQuery();
                    var message = string.Format(" Customer's record Activated successfully");
                    //write that the Custoemrs Account has been Actiated

                    //get the Activated Customers Details

                    RCDC_DisconnectionList List = db.RCDCDisconnectionLists.FirstOrDefault(p => p.AccountNo == Data.AccountNo);

                    if (List != null)
                    {
                        RCDC_OnboardCustomers onboard = new RCDC_OnboardCustomers();
                        onboard.DTRName = List.DTR_Name;
                        onboard.Zone = List.Zone;
                        onboard.FeederId = List.FeederId;
                        onboard.FeederName = List.FeederName;
                        onboard.DTRCode = List.DTRCode;
                        onboard.DateCaptured = DateTime.Now;
                        onboard.ParentAccountNo = List.AccountNo;
                        onboard.StreetName = List.Address;
                        onboard.StaffId = Data.StaffId.Trim();
                        onboard.Surname = List.AccountName;
                        onboard.OnboardCategory = "ACTIVATION";
                        var k = db.Users.FirstOrDefault(p => p.StaffId == Data.StaffId);

                        if (k != null)
                        {
                            onboard.CapturedBy = k.StaffName;
                          //  DTRExec.AuditTrail(k.Id, Data.AccountNo + " was Activated on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "ACTIVATION");
                        }

                        onboard.ParentAccountNo = Data.AccountNo;

                        db.RCDC_OnboardCustomerss.Add(onboard);
                        db.SaveChanges();
                    }

                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.OK, err);
                }
                catch (Exception ex)
                { 
                    var message = string.Format("Could not Activate Customer's record because " + ex.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                finally
                {
                    conn.Close();
                }


               
                #endregion
                 
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/TariffChange")]
        public HttpResponseMessage TariffChange(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = "";
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "TARIFF CHANGE").ToList();

                if (DisconnectionList.Count > 0)
                {
                    //;;;;;;;;;;;;;;;;;;;;;;;;
                    var k = db.Users.FirstOrDefault(p => p.StaffId == Data.StaffId);


                    if (k != null)
                    {
                        Audit.AuditTrail(k.Id, Data.AccountNo + "Tariff was changed on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TARIFF CHANGE");

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    message = string.Format("There are no activities you have done on Tariff Change");
                    HttpError _err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, _err);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetProvisionalOutstanding")]
        public HttpResponseMessage GetProvisionalOutstanding(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            RCDCCustomer Customer = new RCDCCustomer();

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            #region Provisional Outstanding
            ProvisionalOutstanding Prov = new ProvisionalOutstanding();
            List<ProvisionalOutstanding> _Prov = new List<ProvisionalOutstanding>();


            conn.Open();

            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_RCDC_GETINCIDENTS"
            };

            cmd.CommandTimeout = 900;
            cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            cmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

            using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Prov = new ProvisionalOutstanding();
                        //Iterate through the Dataset and Set the Payment history Objects to the Model
                        Prov.INCIDENCE = rdr["Incidence"].ToString();
                        Prov.PRI_OUT_CRE_COM = rdr["PRI_FT_FA_OUT_CRE_COM"].ToString();
                        Prov.Amount = rdr["Amount"].ToString();
                        Prov.Inc_Date = rdr["Inc_Date"].ToString();
                        _Prov.Add(Prov);
                    }
                }

                Customer.ProvisionalOutstanding = _Prov;
            }

            conn.Close();
            conn.Dispose();

            #endregion

            return Request.CreateResponse(HttpStatusCode.OK, Customer);
        }

        
        
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetTicketNumbers")]
        public HttpResponseMessage GetTicketNumbers(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            RCDCCustomer Customer = new RCDCCustomer();

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
             
            #region Provisional Outstanding
            PaymentReceiptNumbers Prov = new PaymentReceiptNumbers();
            List<PaymentReceiptNumbers> _Prov = new List<PaymentReceiptNumbers>();


            conn.Open();

            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_receiptNumber"
            };

            cmd.CommandTimeout = 900;
            cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            cmd.Parameters.Add("p_accountno", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

            using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Prov = new PaymentReceiptNumbers();
                        //Iterate through the Dataset and Set the Payment history Objects to the Model
                        Prov.RECEIPTNO = rdr["RECEIPTNO"].ToString();
                        Prov.DATETIME = rdr["CREATEDDATETIME"].ToString();
                        _Prov.Add(Prov);
                    }
                }

                Customer.ReceiptNoList = _Prov;
            }

            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            #endregion
             
            return Request.CreateResponse(HttpStatusCode.OK, Customer);
        }

          

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/CustomerPaymentInfo")]
        public HttpResponseMessage CustomerPaymentInfo(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            DateTime Date = DateTime.Now;

            int? DayOfBirth = new int?(Date.Day);
            string MonthOfBirth = Date.ToString("MMMM");

            string trans_id = RandomPassword.Generate(10).ToString();
            string CustomerEmail = Data.CustomerEmail.Trim();
            string Currency = "NGN";
            string PublicKey = "1d8c210a3b1a5d32496204618cf5bd5a";
            string Key = "fbf1f5bbf7d4bfcaead84b46022286e4";
            string ProductDescription = "EazyPay Electricity Bill";
            string ProductId = "78";
            string ChannelName = "WEB";
            string Location = "WEB";
            string PaymentMethod = "Debit Card";
            string InstitutionId = "PHEDCPP";
            string PaymentCurrency = "NGN";
            string BankName = "WEB Payment";
            string BranchName = "WEB";
            string InstitutionName = "PHEDBillPay";
            string ItemName = "PHED Bill Payment";
            string ItemCode = "PHEDBill";
            string PaymentStatus = "PENDING";
            string IsReversal = "false";
            try
            {
                
                CustomerPaymentInfo Info = new CustomerPaymentInfo();
                Info.Amount = Data.Amount;
                Info.ItemAmount = Data.Amount;
                Info.ItemCode = "01";
                Info.TransactionID = trans_id;
                Info.PaymentMethod = "WEB";
                Info.CustomerName = Data.CustomerName;
               
                Info.CustomerEmail = Data.CustomerEmail;
                Info.CustomerPhoneNumber = Data.PhoneNo;
                Info.DepositorName = Data.CustomerName;
                Info.DepositSlipNumber = trans_id;
                Info.InstitutionId = InstitutionId;
                Info.InstitutionName = InstitutionName;
                Info.ItemName = ItemName;
                Info.ChannelName = "MOBILE";
                Info.PaymentCurrency = Currency;
                Info.IsReversal = IsReversal;
                Info.PaymentStatus = PaymentStatus;
                Info.BankName = BankName;
                Info.BranchName = BranchName;
                Info.Location = Location;
                Info.CustomerAddress = Data.Address;
                Info.CustomerReference = Data.AccountNo;
                Info.AlternateCustReference = Data.MeterNo;
                Info.Token = "NOTAVAILABLE";
                Info.PaymentMethod = PaymentMethod;
                Info.AccountType = Data.AccountType.ToUpper();
                Info.TransactionProcessDate = DateTime.Now.ToShortDateString();

                db.CustomerPaymentInfos.Add(Info);
                db.SaveChanges();


                var KYCList = db.CustomerPaymentInfos.Where(p => p.TransactionID == trans_id).ToList();

                
                return Request.CreateResponse(HttpStatusCode.OK, (KYCList));
            }
            catch (Exception ex)
            {
                var message = string.Format("Could not complete your request because + " +ex.Message +", Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        /// <summary>
        /// Get last 5 Transaction Id
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetLastNPaymentId")]
        public HttpResponseMessage GetLastNPaymentId(RCDCModel Data)
        {
            try
            {
                var KYCList = db.CustomerPaymentInfos.Where(p => (p.AlternateCustReference == Data.AccountNo || p.CustomerReference == Data.AccountNo)).OrderBy(o => o.TransactionProcessDate).Take(10).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, (KYCList));
            }

            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/UpdateCustomerPaymentInfo")]
        public HttpResponseMessage UpdateCustomerPaymentInfo(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            DateTime Date = DateTime.Now;

            string TransId = Data.PaymentTransId;

            var PaymentData = db.CustomerPaymentInfos.FirstOrDefault(p => p.TransactionID == TransId);
            try
            {

                
                PaymentData.PaymentStatus = Data.PaymentStatus; 
                PaymentData.Token = Data.Token;
                PaymentData.TokenDate =  DateTime.Now.ToShortDateString(); 
                db.Entry(PaymentData).State = EntityState.Modified;
                db.SaveChanges();
                 
                var KYCList = db.CustomerPaymentInfos.Where(p => p.TransactionID == TransId).ToList(); 
                return Request.CreateResponse(HttpStatusCode.OK, (KYCList));
            }
            catch (Exception ex)
            {
                var message = string.Format("An error Occured, Please try again Later. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }
         
         

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomerPaymentHistory")]
        public HttpResponseMessage GetCustomerPaymentHistory(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);
                DataSet dataSet = new DataSet();
                string AccountNo = Data.AccountNo;
                int Count = 30;
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString());
                conn.Open();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ENSERV.SP_WF_GET_CUSTOMER_PAYMENTHISTORY"
                };
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                cmd.Parameters.Add("P_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountNo;
                cmd.Parameters.Add("P_COUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = Count;


                try
                {
                    RCDCCustomer Customer = new RCDCCustomer();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //Formulate the Customer details here before Sending

                            List<RCDCCustomerPayments> Pay = new List<RCDCCustomerPayments>();

                            RCDCCustomerPayments _pay = new RCDCCustomerPayments();
                            while (rdr.Read())
                            {
                                //Status = "DUPLICATE";
                                _pay = new RCDCCustomerPayments();
                                //Iterate through the Dataset and Set the Payment history Objects to the Model
                                _pay.AmountPaid = Convert.ToDouble(rdr["Amount"].ToString());
                                _pay.DatePaid = (DateTime)rdr["paymentdatetime"];
                                _pay.PaymentDescription = rdr["paymentpurpose"].ToString();
                                _pay.PaymentID = rdr["receiptnumber"].ToString();
                                Pay.Add(_pay);
                            }
                            Customer.PaymentHistory = Pay.OrderByDescending(p => p.DatePaid).ToList();
                            //provisional Outstanding 
                            //;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

                            #region Provisional Outstanding
                            ProvisionalOutstanding Prov = new ProvisionalOutstanding();
                            List<ProvisionalOutstanding> _Prov = new List<ProvisionalOutstanding>();

                            OracleDataAdapter da = new OracleDataAdapter();
                            OracleCommand oracmd = new OracleCommand
                            {
                                Connection = conn,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "ENSERV.SP_RCDC_GETINCIDENTS"
                            };

                            oracmd.CommandTimeout = 900;
                            oracmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                            oracmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

                            using (OracleDataReader rdrRcdc = oracmd.ExecuteReader())
                            {
                                if (rdrRcdc.HasRows)
                                {
                                    while (rdrRcdc.Read())
                                    {
                                        Prov = new ProvisionalOutstanding();
                                        //Iterate through the Dataset and Set the Payment history Objects to the Model
                                        Prov.INCIDENCE = rdr["Incidence"].ToString();
                                        Prov.PRI_OUT_CRE_COM = rdr["PRI_FT_FA_OUT_CRE_COM"].ToString();
                                        _Prov.Add(Prov);
                                    }
                                }

                                Customer.ProvisionalOutstanding = _Prov;
                            }

                            // conn.Close();
                            //conn.Dispose();
                            oracmd.Dispose();
                            #endregion

                            //Billing History


                            OracleCommand cmdBill = new OracleCommand
                            {
                                Connection = conn,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "ENSERV.SP_WF_GET_BILLINFO_BY_CUSTOMERACCOUNT"
                            };
                            cmdBill.CommandTimeout = 900;
                            cmdBill.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                            cmdBill.Parameters.Add("P_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountNo;
                            cmdBill.Parameters.Add("P_COUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = Count;
                            using (OracleDataReader billrdr = cmdBill.ExecuteReader())
                            {
                                RCDC_Spot_Billing Bills = new RCDC_Spot_Billing();
                                List<RCDC_Spot_Billing> _Bills = new List<RCDC_Spot_Billing>();
                                if (billrdr.HasRows)
                                {
                                    while (billrdr.Read())
                                    {
                                        Bills = new RCDC_Spot_Billing();
                                        //Iterate through the Dataset and Set the Payment history Objects to the Model
                                        Bills.BilledQty = billrdr["BilledAmount"].ToString();
                                        Bills.BillingDate = Convert.ToDateTime(billrdr["BillMonth"].ToString());
                                        _Bills.Add(Bills);
                                    }
                                    Customer.BillingHistory = _Bills;
                                }
                                else
                                {

                                }
                            }

                            //FT-00462 
                            return Request.CreateResponse(HttpStatusCode.OK, Customer);

                        }
                        else
                        {
                            var message = string.Format("No Account record exists for this Account Selected ");
                            HttpError err = new HttpError(message);
                            return Request.CreateResponse(HttpStatusCode.NotFound, err);
                        }
                    }

                }
                catch (Exception exception1)
                {
                    conn.Close();
                    conn.Dispose();
                    Exception exception = exception1;
                    var message = string.Format("Could not retrieve Customer records because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

            }
        }


        public string GenerateCIN(string AccountNo)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            List<GeneratedCIN> list = new List<GeneratedCIN>();

            string CINN = "";



            try
            {
                using (SqlConnection con = new SqlConnection(EnumsConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_generate_cin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Accountno", SqlDbType.VarChar).Value = AccountNo;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var CIN = rdr[0].ToString();
                                if (CIN == "D" || CIN == "")
                                {
                                    CIN = null;
                                }
                                CINN = CIN;
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {

                CINN = "Unable to Generate CIN, Pls Generate Manually. Thanks";
            }
         
            return CINN;
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GenerateCINFromAccountNumber")]
        public HttpResponseMessage GenerateCINFromAccountNumber(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            List<GeneratedCIN> list = new List<GeneratedCIN>();

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } 

            using (SqlConnection con = new SqlConnection(EnumsConnection))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_generate_cin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Accountno", SqlDbType.VarChar).Value = Data.AccountNo;


                    con.Open();
                    //cmd.ExecuteScalar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var CIN = rdr[0].ToString();

                            if (CIN == "D" || CIN == "")
                            {
                                CIN = null;

                            }


                            list.Add(new GeneratedCIN { CIN = CIN });

                        }

                        con.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }


                }
            }

            var _message = string.Format("Could not retrieve incidence records.  Please try again Thank you");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.NotFound, _err);

        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetCustomerPaymentHistoryWithToken")]
        public HttpResponseMessage GetCustomerPaymentHistoryWithToken(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);
                DataSet dataSet = new DataSet();
                DBManager dBManager = new DBManager(DataProvider.Oracle)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
                };

                dBManager.Open();
                // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");

                string AccountNo = Data.AccountNo;
                int Count = 20;
                // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";
                ///861203339501	
                //string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, to_char(paymentdatetime,'DD/MM/YYYY') as paymentdatetime , paymentpurpose, channelname from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by  paymentdatetime desc";

               // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname, rownum from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";

                string str = "select * from (select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, t2.purpose paymentpurpose, channelname, Tokendec Token, SUBSTR(IDRECORD, 33, 2) TI from ENSERV.tbl_allpayment t1 inner join ENSERV.tbl_paymentpurpose t2 on t1.paymentpurpose = t2.ID where consumer_no = '" + Data.AccountNo + "' and cancel_status = '0' order by paymentdatetime desc) where rownum <='" + Count + "'";
                 
                dBManager.Open();
                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close(); dBManager.Dispose();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        var message = string.Format("No Account record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);

                    }
                    else
                    {
                        //Formulate the Customer details here before Sending

                        RCDCCustomer Customer = new RCDCCustomer();
                        List<RCDCCustomerPayments> Pay = new List<RCDCCustomerPayments>();

                        RCDCCustomerPayments _pay = new RCDCCustomerPayments();
                        // var DefaultingCustomer = db.RCDCDisconnectionLists.FirstOrDefault(p=>p.DisconID == Data.DisconnId);

                        //Payment History
                        for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                        {
                            _pay = new RCDCCustomerPayments();
                            //Iterate through the Dataset and Set the Payment history Objects to the Model
                            _pay.AmountPaid = Convert.ToDouble(dataSet1.Tables[0].Rows[i]["Amount"].ToString());
                            _pay.DatePaid = (DateTime)dataSet1.Tables[0].Rows[i]["paymentdatetime"];
                            _pay.PaymentDescription = dataSet1.Tables[0].Rows[i]["paymentpurpose"].ToString();
                            _pay.PaymentID = dataSet1.Tables[0].Rows[i]["receiptnumber"].ToString();
                            _pay.PaymentChannel = dataSet1.Tables[0].Rows[i]["channelname"].ToString();
                            _pay.Token = dataSet1.Tables[0].Rows[i]["Token"].ToString();
                            _pay.TarriffIndex = dataSet1.Tables[0].Rows[i]["TI"].ToString();
                            Pay.Add(_pay);
                        }
                         
                        Customer.PaymentHistory = Pay.OrderByDescending(p=>p.DatePaid).ToList();

                        #region Provisional Outstanding
                        ProvisionalOutstanding Prov = new ProvisionalOutstanding();
                        List<ProvisionalOutstanding> _Prov = new List<ProvisionalOutstanding>();
                         
                        conn.Open();

                        OracleDataAdapter da = new OracleDataAdapter();
                        OracleCommand cmd = new OracleCommand
                        {
                            Connection = conn,
                            CommandType = CommandType.StoredProcedure,
                            CommandText = "ENSERV.SP_RCDC_GETINCIDENTS"
                        };

                        cmd.CommandTimeout = 900;
                        cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                        cmd.Parameters.Add("IN_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = Data.AccountNo;

                        using (OracleDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    Prov = new ProvisionalOutstanding();
                                    //Iterate through the Dataset and Set the Payment history Objects to the Model
                                    Prov.INCIDENCE = rdr["Incidence"].ToString();
                                    Prov.PRI_OUT_CRE_COM = rdr["PRI_FT_FA_OUT_CRE_COM"].ToString();
                                    Prov.Amount = rdr["Amount"].ToString();
                                    Prov.Inc_Date = rdr["Inc_Date"].ToString();
                                    _Prov.Add(Prov);
                                }
                            }

                            Customer.ProvisionalOutstanding = _Prov;
                        }

                        conn.Close();
                        conn.Dispose();
                        cmd.Dispose();
                        #endregion

                        //Billing History

                         // string Billstr =  "select slabec1+ed as BilledAmount, BILLMONTH as BillMonth from ENSERV.tbl_BILLINFO where consumerno = '" + AccountNo + "' and  rownum <= 6 order by BILLMONTH desc";

                          string Billstr = "select BilledAmount, BillMonth from (select slabec1+ed as BilledAmount, BILLMONTH as BillMonth from ENSERV.tbl_BILLINFO where consumerno = '" + AccountNo + "' order by BILLMONTH desc) where rownum <= 12";
                        
                        
                        dBManager.Open();
                         RCDC_Spot_Billing Bills = new RCDC_Spot_Billing(); 
                        
                       List<RCDC_Spot_Billing> _Bills = new List<RCDC_Spot_Billing>();
                         DataSet dataSet2 = dBManager.ExecuteDataSet(CommandType.Text, Billstr);
                          dBManager.Close(); dBManager.Dispose();
                          if (dataSet2.Tables[0].Rows.Count > 0)
                          {
                              for (int i = 0; i < dataSet2.Tables[0].Rows.Count; i++)
                              {
                                  Bills = new RCDC_Spot_Billing();
                                  //Iterate through the Dataset and Set the Payment history Objects to the Model
                                  Bills.BilledQty = dataSet2.Tables[0].Rows[i]["BilledAmount"].ToString();
                                  Bills.BillingDate = Convert.ToDateTime(dataSet2.Tables[0].Rows[i]["BillMonth"].ToString());
                                  _Bills.Add(Bills);
                              }
                          }
                         
                          Customer.BillingHistory = _Bills;
                         
                          dBManager.Open(); 
                        //DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                         dBManager.Close(); 
                        dBManager.Dispose();



                        //FT-00462


                        return Request.CreateResponse(HttpStatusCode.OK, Customer);
                    }

                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    //dBManager.Close(); dBManager.Dispose();
                    var message = string.Format("Could not retrieve Customer records because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCIncidence")]
        public HttpResponseMessage GetRCDCIncidence()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                var list = db.RCDC_Incidences.Select(p => new RCDC_Incidence_VM { IncidenceId = p.IncidenceId, IncidenceName = p.IncidenceName }).ToList();


                if (list.Count <= 0)
                {
                    var message = string.Format("No incidence record exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCFeeder")]
        public HttpResponseMessage GetRCDCFeeder(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.Zone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Zone as *Zone* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            try
            {
                var list = db.BSCs.Where(p => p.IBCId == Data.Zone).ToList();

                if (list.Count <= 0)
                {
                    var message = string.Format("No Zone Exists for the selection you made");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCDTR")]
        public HttpResponseMessage GetRCDCDTR(RCDCModel Data)
        { 
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            if (Data == null || string.IsNullOrEmpty(Data.FeederId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Please supply a Feeder Id as *FeederId*. Please Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            {
                var list = db.RCDC_DTRs.Where(p => p.FeederId == Data.FeederId).ToList();
                //.Select(p => new RCDC_Incidence_VM { IncidenceId = p.IncidenceId, IncidenceName = p.IncidenceName }).ToList();


                if (list.Count <= 0)
                {
                    var message = string.Format("No DTR Exists for the selection you made");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCZones")]
        public HttpResponseMessage GetRCDCZones()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                var list = db.IBCs.ToList();
            
                if (list.Count <= 0)
                {
                    var message = string.Format("No incidence record exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err); 
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                } 
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetBillinformation")]
        public HttpResponseMessage GetBillinformation(RCDCModel Data)
        {


            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            try
            {
                RCDC_Billinformation Bill = new RCDC_Billinformation();

                Bill.AccountNo = "90987656789";
                Bill.Address = "Moscow Road";
                Bill.AmountBilled = "4500";
                Bill.BillId = "4233";
                Bill.CustomerName = "QUEEN OHIRO";
                Bill.DTR_Id = "30844";
                Bill.DTR_Name = "RUMUOLA";
                Bill.Zone = "";
                Bill.FeederName = "";
                Bill.FeederId = "";
                Bill.Month = "June";
                Bill.Year = "2020";

                return Request.CreateResponse(HttpStatusCode.OK, Bill);

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCState")]
        public HttpResponseMessage GetRCDCState()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                var list = db.States.ToList().Select(p => new STATE { STATE_CODE = p.STATE_CODE, STATE_NAME = p.STATE_NAME }).ToList();

                if (list.Count <= 0)
                {
                    var message = string.Format("No incidence record exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCLGA")]
        public HttpResponseMessage GetRCDCLGA(RCDCModel Data)
        { 
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.State_Id))
            { 
                var message = string.Format("Please input the Zone as *Zone* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            { 
                var list = db.LGAs.Where(p => p.STATE_CODE == Data.State_Id).ToList();
                
                if (list.Count <= 0)
                {
                    var message = string.Format("No LGA Exists for the Selected exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1; 
                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReportCategory")]
        public HttpResponseMessage GetReportCategory()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                var list = db.RCDC_ReportCategorys.ToList();
                //.Select(p => new RCDC_Incidence_VM { IncidenceId = p.IncidenceId, IncidenceName = p.IncidenceName }).ToList();


                if (list.Count <= 0)
                {
                    var message = string.Format("No Report Category record exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve Report Category records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReportSubCategory")]
        public HttpResponseMessage GetReportSubCategory(RCDCModel Data)
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            if (Data == null || string.IsNullOrEmpty(Data.ReportCategoryId))
            {
                var message = string.Format("Please specify the Report Category Id.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            {
                var list = db.RCDC_ReportSubCategorys.Where(p => p.ReportCategoryId == Data.ReportCategoryId).ToList();
                //.Select(p => new RCDC_Incidence_VM { IncidenceId = p.IncidenceId, IncidenceName = p.IncidenceName }).ToList();

                if (list.Count <= 0)
                {
                    var message = string.Format("No Report Sub Category Exists for the Selected exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve Report Sub Categories because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetRCDCReasonsForDisconnection")]
        public HttpResponseMessage GetRCDCReasonsForDisconnection()
        {

            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            try
            {
                var list = db.RCDC_Reasons_For_Disconnections.ToList();

                if (list.Count <= 0)
                {
                    var message = string.Format("No incidence record exists");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;

                var message = string.Format("Could not retrieve incidence records because " + exception1.Message + ". Please try again Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }

        #endregion

        #region Disconnection APIs
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectionList")]
        public HttpResponseMessage DisconnectionList(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.Zone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Zone was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.FeederId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The Feeder was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //if (Data == null || string.IsNullOrEmpty(Data.Year))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("Please select a Year and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            //if (Data == null || string.IsNullOrEmpty(Data.Month))
            //{
            //    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

            //    var message = string.Format("Please select a Month and Try again");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederId == Data.FeederId && p.DisconStatus == "DISCONNECT").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetSettlementDuration")]
        public HttpResponseMessage GetSettlementDuration()
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            var Duration = db.RCDC_Settlement_Durations.ToList(); 
            return Request.CreateResponse(HttpStatusCode.OK, Duration);
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectionCustomerIncidencesTest")]
        public HttpResponseMessage DisconnectionCustomerIncidencesTest(RCDCModel Data)
        {
            EnergyBill Bill = GetEnergyBillComponents(Data.AccountNo);
            RCDC_Disconnection_Incidence_History _Calc = new RCDC_Disconnection_Incidence_History();
            return Request.CreateResponse(HttpStatusCode.OK, _Calc);
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectionCustomerIncidences")]
        public HttpResponseMessage DisconnectionCustomerIncidences(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            RCDC_Disconnection_Incidence_History Calc = new RCDC_Disconnection_Incidence_History();
            List<RCDC_Disconnection_Incidence_History> _Calc = new List<RCDC_Disconnection_Incidence_History>();


            if (Data == null || string.IsNullOrEmpty(Data.AccountType))
            {
                var message = string.Format("Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.DateOfLastPayment))
            {
                var message = string.Format("The Load of the Customer has not been passed. please pass it to continue.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("The Reason for Disconnection was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                var message = string.Format("The Account Number was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Phase))
            {
                var message = string.Format("Please supply the Phase of the Meter to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Flag))
            {
                var message = string.Format("Please supply the Flag of the Load to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                RCDC_Disconnection_Incidence IncidenceAmount = new RCDC_Disconnection_Incidence();
                List<RCDC_Disconnection_Incidence> _IncidenceAmount = new List<RCDC_Disconnection_Incidence>();

                var check = db.RCDC_Reasons_For_Disconnections.FirstOrDefault(p => p.ReasonForDisconnectionId == Data.ReasonForDisconnectionId);

                if (check != null)
                {
                    _Calc = GenerateCustomerIncidenceList(Data);
                }

                return Request.CreateResponse(HttpStatusCode.OK, _Calc);
            }
        }

        private List<RCDC_Disconnection_Incidence_History> GenerateCustomerIncidenceList(RCDCModel Data)
        {

            RCDC_Disconnection_Incidence_History Calc = new RCDC_Disconnection_Incidence_History();
            List<RCDC_Disconnection_Incidence_History> _Calc = new List<RCDC_Disconnection_Incidence_History>();

            //get the Amount for the SettlementPlane
          var Amount =   db.RCDC_SettlementPlan_Settingss.FirstOrDefault();
            string Amt = "";
            
            if(Amount != null)
            {
                   Amt = Amount.Settlement_Plan_Amount; 
            }



            var In = db.RCDC_Incidence_For_Reasonss.Where(p => p.ReasonForDisconnectionId == Data.ReasonForDisconnectionId).ToList();
            if (In.Count > 0)
            {
                //Duration of Days from the Last Recharge
                int Duration = 0;

                if (Data.Flag == "1")
                {

                    Duration = (DateTime.Now.Date - Convert.ToDateTime(Data.DateOfLastPayment).Date).Days;
                }
                else
                {
                    Duration = Convert.ToInt32(Data.Duration);

                }
                    
                    
                   
                string Availability = Data.Availability;
                string AverageConsumption = "";
                string AccountType = Data.AccountType;
                string Load = Data.LoadProfile;
                //there are incidences
                foreach (var incidence in In)
                {
                    //Energy Bill
                    //ReconnectionFee

                    Calc = new RCDC_Disconnection_Incidence_History();
                    
                    //Calculate For Reconnection Cost
                    if (incidence.IncidenceId == "3")
                    {
                        //Calculate the Loss of Revenue from the Incidence here and use it to calculate the Necessary amount to be paid
                        if (Data.Phase == "1")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            ///
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.Status == "RPD" && p.Phase == Data.Phase).Amount;
                        }
                        if (Data.Phase == "3")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.Status == "RPD" && p.Phase == Data.Phase).Amount;

                        }
                        
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.CalculateAmount = "false";
                        Calc.DisconnId = Data.DisconnId;
                        Calc.DurationInDays = Duration;
                        _Calc.Add(Calc);
                    }

                    //Calculate For loss of revenue
                    if (incidence.IncidenceId == "7")
                    {
                        //Calculate the Loss of Revenue from the Incidence here and use it to calculate the Necessary amount to be paid
                        Calc.IncidenceAmount = GenerateLossOfRevenueAmount(Availability, Load, AverageConsumption, Duration, AccountType, Data.AccountNo, Data.Flag, Data.TariffRate, Data.Phase).ToString();
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.CalculateAmount = "false";
                        Calc.DisconnId = Data.DisconnId;
                        Calc.DurationInDays = Duration;
                        _Calc.Add(Calc);
                    }

                    //Calculate For ENERGY BILL
                    if (incidence.IncidenceId == "14")
                    {
                        EnergyBill Bill = GetEnergyBillComponents(Data.AccountNo);

                        if (Bill.Status == "SUCCESS")
                        {
                            //get the 100% of the Current Charge and the 
                            double CurrentBillCharge = Convert.ToDouble(Bill.CurrentCharges) * 1;
                            double PercentOfArrears = Convert.ToDouble(Bill.Arrears) * 0.1;

                            #region Settlement for Payment
                            double TotalAmount = Convert.ToDouble(Bill.TotalOutstanding) - (CurrentBillCharge + PercentOfArrears); 

                            if (TotalAmount >= Convert.ToDouble(Amt))
                            {
                                Calc.Settlement = "YES";
                                Calc.SettlementAmount = TotalAmount.ToString();
                            }
                            #endregion

                            Calc.IncidenceAmount = (CurrentBillCharge + PercentOfArrears).ToString();
                            Calc.IncidenceId = incidence.IncidenceId;
                            Calc.IncidenceName = incidence.Incidence.IncidenceName;
                            Calc.Percentpayment = incidence.PercentageToPay;
                            Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                            Calc.ToDate = DateTime.Now;
                            Calc.DurationInDays = Duration;
                            Calc.CalculateAmount = "false";
                            //Calc.DurationInDays = Duration;
                            Calc.DisconnId = Data.DisconnId;
                            _Calc.Add(Calc);
                            //get the 10% of the Arrears Charge and the  
                        }
                    }

                    //Add Reconnection Fee to the Guy
                    //Calculate For ENERGY BILL
                    if (incidence.IncidenceId == "10")
                    { 
                        if (Data.AccountType == "POSTPAID")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            ///
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType && p.Phase == Data.Phase).Amount;

                        }
                        if (Data.AccountType == "PREPAID")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType && p.Phase == Data.Phase).Amount;

                        }
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.DurationInDays = Duration;
                        Calc.DisconnId = Data.DisconnId;
                        _Calc.Add(Calc);
                        //get the 10% of the Arrears Charge and the  
                        //}
                    }
                }

            }
            return _Calc;
        }
         
        private List<RCDC_Disconnection_Incidence_History> GenerateCustomerIncidenceListDTR(RCDCModel Data)
        {

            RCDC_Disconnection_Incidence_History Calc = new RCDC_Disconnection_Incidence_History();
            List<RCDC_Disconnection_Incidence_History> _Calc = new List<RCDC_Disconnection_Incidence_History>();

            //get the Amount for the SettlementPlane
          var Amount =   db.RCDC_SettlementPlan_Settingss.FirstOrDefault();
            string Amt = "";
            
            if(Amount != null)
            {
                   Amt = Amount.Settlement_Plan_Amount; 
            }



            var In = db.RCDC_Incidence_For_Reasonss.Where(p => p.ReasonForDisconnectionId == Data.ReasonForDisconnectionId).ToList();
            if (In.Count > 0)
            {
                //Duration of Days from the Last Recharge
                int Duration = 0;

                
                   
                //string Availability = Data.Availability;
                //string AverageConsumption = "";
                string AccountType = Data.AccountType;
                //string Load = Data.LoadProfile;
                //there are incidences
                foreach (var incidence in In)
                {
                    //Energy Bill
                    //ReconnectionFee

                    Calc = new RCDC_Disconnection_Incidence_History();
                    
                    //Calculate For Reconnection Cost
                    if (incidence.IncidenceId == "3")
                    {
                        //Calculate the Loss of Revenue from the Incidence here and use it to calculate the Necessary amount to be paid
                        if (Data.Phase == "1")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            ///
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.Status == "RPD" && p.Phase == Data.Phase).Amount;
                        }
                        if (Data.Phase == "3")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.Status == "RPD" && p.Phase == Data.Phase).Amount;

                        }
                        
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.CalculateAmount = "false";
                        Calc.DisconnId = Data.DisconnId;
                        Calc.DurationInDays = Duration;
                        _Calc.Add(Calc);
                    }

                    //Calculate For loss of revenue
                    if (incidence.IncidenceId == "7")
                    {
                        //Calculate the Loss of Revenue from the Incidence here and use it to calculate the Necessary amount to be paid
                        Calc.IncidenceAmount = "0";
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.CalculateAmount = "false";
                        Calc.DisconnId = Data.DisconnId;
                        Calc.DurationInDays = Duration;
                        _Calc.Add(Calc);
                    }

                    //Calculate For ENERGY BILL
                    if (incidence.IncidenceId == "14")
                    {
                       
                        Calc.IncidenceAmount = "0";
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.DurationInDays = Duration;
                        Calc.CalculateAmount = "false";
                        //Calc.DurationInDays = Duration;
                        Calc.DisconnId = Data.DisconnId;
                        _Calc.Add(Calc);
                    }

                    //Add Reconnection Fee to the Guy
                    //Calculate For ENERGY BILL
                    if (incidence.IncidenceId == "10")
                    { 
                        if (Data.AccountType == "POSTPAID")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            ///
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType && p.Phase == Data.Phase).Amount;

                        }
                        if (Data.AccountType == "PREPAID")
                        {
                            ///Get the Reconnection Cost for POSTPAID
                            Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType && p.Phase == Data.Phase).Amount;

                        }
                        Calc.IncidenceId = incidence.IncidenceId;
                        Calc.IncidenceName = incidence.Incidence.IncidenceName;
                        Calc.Percentpayment = incidence.PercentageToPay;
                        Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                        Calc.ToDate = DateTime.Now;
                        Calc.DurationInDays = Duration;
                        Calc.DisconnId = Data.DisconnId;
                        _Calc.Add(Calc); 
                    }
                } 
            }
            return _Calc;
        }


         
        #endregion
        

        private EnergyBill GetEnergyBillComponents(string AccountNo)
        {
            EnergyBill e = new EnergyBill();

            try
            {
                conn = new OracleConnection(strConnString);
                conn.Open();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ENSERV.SP_WF_GET_BILLCOMPONENTS_BY_CUSTOMERACCOUNT"
                };
                //Get arrears from DLEnhance
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                cmd.Parameters.Add("P_CONSUMERNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountNo;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            e.Arrears = rdr["ARREAR"].ToString();
                            e.CurrentCharges = rdr["CurrentCharge"].ToString();
                            e.TotalOutstanding = rdr["TOTALOUTSTANDING"].ToString();

                        }
                        e.Status = "SUCCESS";
                        conn.Close();
                        conn.Dispose();
                        cmd.Dispose();
                        return e;
                    }
                    else
                    {
                        e.Status = "FAILED";
                    }
                }
            }
            catch (Exception ex)
            {
                e.Status = "FAILED";
            }

            return e;
        }

        public double GenerateLossOfRevenueAmount(string Availability, string Load, string AverageConsumption, int Duration, string AccountType, string AccountNo, string LoadCalculationType, string Tariff, string Phase)
        {
            //select the Last 6 Payments of the Customr and Average it adn Give him the Loss of Revenue Amount to be Paid

            DataSet dataSet = new DataSet();
            DBManager dBManager = new DBManager(DataProvider.Oracle)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
            };

            dBManager.Open();
            // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");

            int Count = 6;
            // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";
             
            //  string str = "select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0)) As CurrentCharge,ARR_EC_DF + T1.ARR_ED_DF AS ARREAR,TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + T1.ARR_ED_DF AS TOTAL_OUTSTANDING from tbl_billinfo where billmonth = (SELECT MAX(billmonth) FROM tbl_billinfo where consumerno = '" + AccountNo + "') and consumerno = '" + AccountNo + "' and rownum = 6 order by billmonth desc ";
            
            double LORAmount = 0; 
            if (LoadCalculationType == "1")
            {
                if (AccountType == "POSTPAID")
                {

                    string str = @"select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))) As CurrentCharge,
                ARR_EC_DF + ARR_ED_DF AS ARREAR,
                TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + ARR_ED_DF) AS TOTALOUTSTANDING
                from tbl_billinfo
                where consumerno = '" + AccountNo + "' and rownum <= 6 order by billmonth desc ";

                    dBManager.Open();
                    try
                    {
                        DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                        dBManager.Close(); dBManager.Dispose();
                        int divideby = dataSet1.Tables[0].Rows.Count;


                        if (dataSet1.Tables[0].Rows.Count <= 0)
                        {

                            return 0;
                        }
                        else
                        {
                            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                            {
                                LORAmount = LORAmount + Convert.ToDouble(dataSet1.Tables[0].Rows[i]["CurrentCharge"].ToString());

                            }
                        }
                        if (divideby < Count)
                        {
                            divideby = Count;

                        }

                        LORAmount = LORAmount / divideby;

                        return LORAmount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }




                if (AccountType == "PREPAID")
                {

                    //Get the Last 6 Vending the guy has done and divide it by 6 and send the amount to the console for the LOR Amount
                    string str = @"select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))) As CurrentCharge,
                    ARR_EC_DF + ARR_ED_DF AS ARREAR,
                    TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + ARR_ED_DF) AS TOTALOUTSTANDING
                    from tbl_billinfo
                    where consumerno = '" + AccountNo + "' and rownum <= 6 order by billmonth desc ";

                    dBManager.Open();

                    try
                    {
                        DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                        dBManager.Close(); dBManager.Dispose();
                        int divideby = dataSet1.Tables[0].Rows.Count;


                        if (dataSet1.Tables[0].Rows.Count <= 0)
                        {

                            return 0;
                        }
                        else
                        {
                            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                            {
                                LORAmount = LORAmount + Convert.ToDouble(dataSet1.Tables[0].Rows[i]["CurrentCharge"].ToString());
                            }
                        }
                        if (divideby < Count)
                        {
                            divideby = Count;
                        }

                        return LORAmount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }

            if (LoadCalculationType == "2" || LoadCalculationType == "3")
            {
                double KW = 0.24 * 0.85 * Convert.ToDouble(Load);

               // double KWH = Convert.ToDouble(Load) * Convert.ToDouble(Availability);

                // double KWH = KW * 0.6 * Convert.ToDouble(Availability);

                //double Total_KWH = Convert.ToDouble(Load) * Convert.ToDouble(Duration);

                if (string.IsNullOrEmpty(Tariff))
                {
                    Tariff = "30.23";
                }

                LORAmount = Convert.ToDouble(Load) * Convert.ToDouble(Tariff) * 1.075;
            }

            return LORAmount;
        }

        private string GetMonthFromDays(int Duration)
        {
           
            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddDays(Duration);
            int monthsDiff = d2.Month - d1.Month + 12 * (d2.Year - d1.Year);
            DateTime d3 = d1.AddMonths(monthsDiff);
            TimeSpan tf = d2 - d3;
            return monthsDiff.ToString();
        }



        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectCustomerFORMDATA")]
        public HttpResponseMessage DisconnectCustomerFORMDATA()
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here

            try
            {
                if (System.Web.HttpContext.Current.Request.Params.Count > 0)
                {
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files;

                    var DocumentName = System.Web.HttpContext.Current.Request.Params["DocumentName"];
                    var Incidences = System.Web.HttpContext.Current.Request.Params["Incidence"];
                    var AverageBillReading = System.Web.HttpContext.Current.Request.Params["AverageBillReading"];
                    var CustomerPhone = System.Web.HttpContext.Current.Request.Params["CustomerPhone"];
                    var CustomerEmail = System.Web.HttpContext.Current.Request.Params["CustomerEmail"];
                    var StaffId = System.Web.HttpContext.Current.Request.Params["StaffId"];
                    AccountNo = System.Web.HttpContext.Current.Request.Params["AccountNo"];
                    var AccountType = System.Web.HttpContext.Current.Request.Params["AccountType"];
                    var UserId = System.Web.HttpContext.Current.Request.Params["UserId"];
                    var DisconnId = System.Web.HttpContext.Current.Request.Params["DisconnId"];
                    var GangID = System.Web.HttpContext.Current.Request.Params["GangID"];
                    var Latitude = System.Web.HttpContext.Current.Request.Params["Latitude"];
                    var Longitude = System.Web.HttpContext.Current.Request.Params["Longitude"];
                    var Comments = System.Web.HttpContext.Current.Request.Params["Comments"];
                    var Incidence = System.Web.HttpContext.Current.Request.Params["Incidence"];

                    if (string.IsNullOrEmpty(Incidence))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("Please input the incidences in a List as *IncidenceAmount , Percentpayment , CalculateAmount , FromDate ,ToDate , DurationInDays , UnitChargePerday  DisconnId* for the Disconnection to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(GangID))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("Please input the Gang ID to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(AverageBillReading))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("Please input the Customers Average Bill Reading to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(CustomerPhone))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please input the CustomerPhoneNo as *CustomerPhone* to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    if (string.IsNullOrEmpty(CustomerEmail))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please input the Customer Email as *CustomerEmail* to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(StaffId))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please input the Staff ID as *StaffId* to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    if (string.IsNullOrEmpty(AccountNo))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please supply the Account Number as *AccountNo* of the Customer to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(AccountType))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please supply the Account type of the Customer as *AccountType* to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    if (string.IsNullOrEmpty(UserId))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                        var message = string.Format("Please supply the UserId of the Staff as *UserId* to Proceed. Thank you");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    if (string.IsNullOrEmpty(DisconnId))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("The Disconnection Id is Missing.Kindly Supply it as *DisconnId* and try again");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(Latitude))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("The Customer Latitude is Missing.Kindly Supply it as *Latitude* and try again");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(Longitude))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(CustomerEmail))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    if (string.IsNullOrEmpty(CustomerPhone))
                    {
                        // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                        var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }

                    //pass the Names of the Fields from Emmanuel Here in the Form Data

                    //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);

                    List<RCDC_Disconnection_Incidence> _incidences = JsonConvert.DeserializeObject<List<RCDC_Disconnection_Incidence>>(Incidences);

                    if (httpPostedFile.Count > 0)
                    { 
                        for (int i = 0; i < httpPostedFile.Count; i++)
                        {
                            HttpPostedFile file = httpPostedFile[i];
                            var Assignments = System.Web.HttpContext.Current.Request.Form[i].ToString();

                            string FN = Guid.NewGuid().ToString();

                            string Description = System.Web.HttpContext.Current.Request.Params["DocumentDescription"];
                            string Size = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                            string Extension = System.Web.HttpContext.Current.Request.Params["DocumentExtension"];

                            // string     DocPath = "/Documents/" + FN + "_" + file.FileName;
                            //Docx.UploadDocument(ClassCode, ArmCode, "", "", SchoolCode, SubjectCode, FileName, Description_, "", key, CreatedBy, "ASSIGNMENT", "", Extension_, Size_, "", DocPath);
                            //  file.SaveAs(path + file.FileName);
                             
                            var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), FN + "_" + file.FileName);
                            file.SaveAs(fileSavePath);
                        }
                    }

                    RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == DisconnId);

                    if (Discon == null)
                    {
                        var message = string.Format("The Disconnection Id is wrong. No record was Found. Please cross check and try again. Thank you.");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NoContent, err);
                    }
                    else
                    {
                        Discon.DisconStatus = "DISCONNECTED";
                        Discon.DisconReason = Comments;
                        Discon.DateOfDiscon = DateTime.Now;
                        Discon.DisconBy = StaffId;
                        Discon.Gang_ID = GangID;
                        Discon.Latitude = Latitude;
                        Discon.Longitude = Longitude;
                        Discon.CustomerEmail = CustomerEmail;
                        Discon.CustomerPhone = CustomerPhone;


                        db.Entry(Discon).State = EntityState.Modified;
                        db.SaveChanges();

                    }



                    //Insert the Incidences
                    RCDC_Disconnection_Incidence_History incid = new RCDC_Disconnection_Incidence_History();

                    if (_incidences.Count > 0 || _incidences != null)
                    {
                        foreach (var I in _incidences)
                        {
                            incid = new RCDC_Disconnection_Incidence_History();


                            if (I.CalculateAmount)
                            {
                                incid.IncidenceAmount = I.IncidenceAmount;
                                //CalculateAmountToPay(I.FromDate, I.ToDate, I.UnitChargePerday, I.Percentpayment).ToString();
                                incid.CalculateAmount = "YES";
                            }
                            else
                            {

                                string IncidenceName  =db.RCDC_Incidences.FirstOrDefault(p => p.IncidenceId == I.IncidenceId).IncidenceName.ToString();
                                incid.IncidenceAmount = I.IncidenceAmount;
                                incid.IncidenceName = IncidenceName;
                                incid.IncidenceDefaultId = Guid.NewGuid().ToString();
                                incid.Status = "NOT PAID";
                                incid.IncidenceId = I.IncidenceId;
                                incid.DisconnId = DisconnId;
                                incid.CalculateAmount = "NO";
                                incid.Percentpayment = I.Percentpayment;
                                incid.DurationInDays = I.DurationInDays;
                                incid.DateDisconnected = DateTime.Now;
                                db.RCDC_Disconnection_Incidence_Historys.Add(incid);
                                db.SaveChanges();


                                //Insert Incidence into DLENHANCE for POSTPAID and PREPAID
                                try
                                {

                                   
                                 //   AddIncidenceToDLEnhance(I.IncidenceAmount, I.IncidenceId,AccountNo, IncidenceName, Data );
                                }
                                catch (Exception ex)
                                {


                                }
                            }
                        }

                    }
                    //Get his Average and Stop his Billing
                    if (AccountType == "POSTPAID")
                    {
                        // do Something About his Billing and Write to RCDC_Bill_Adjustment


                        RCDC_Spot_Billing Bill = new RCDC_Spot_Billing();
                        Bill.AvgConsumption = AverageBillReading;
                        Bill.BilledQty = CalcualteBilledQty(AverageBillReading).ToString();
                        Bill.BillingDate = DateTime.Now;
                        Bill.BillingId = Guid.NewGuid().ToString();
                        Bill.DisconnId = DisconnId;
                        Bill.NoofDaysBilled = DateTime.Now.Day.ToString();
                        Bill.Status = "BILLED";
                        Bill.Year = DateTime.Now.Year.ToString();
                        db.RCDC_Spot_Billings.Add(Bill);
                        db.SaveChanges();
                    }


                    //AUDIT TRAIL
                    Audit.AuditTrail(UserId, AccountNo + " was Disconnected on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "DISCONNECTION");

                }

                RCDCModel Success = new RCDCModel();

                Success.Message = "The Customer with Account Number " + AccountNo + "  has been disconnected Successfully";
                Success.Status = "SUCCESS";

                return Request.CreateResponse(HttpStatusCode.OK, Success);
            }
            catch (Exception ex)
            {
                RCDCModel Success = new RCDCModel();

                Success.Message = "Could not Disconnect the Customer because " + ex.Message + ". Please try again.";
                Success.Status = "FAILED";
                return Request.CreateResponse(HttpStatusCode.OK, Success);
            }

            RCDCModel _Success = new RCDCModel();

            _Success.Message = "The Customer with Account Number" + AccountNo + "could not be disconnected. Please try again";
            _Success.Status = "FAILED";

            return Request.CreateResponse(HttpStatusCode.OK, _Success);

        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetTariffList")]
        public HttpResponseMessage GetTariffList()
        {

            DOCUMENTS Docs = new DOCUMENTS();
            
            try
            {
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString());
                conn.Open();
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_WF_GET_TARIFFLIST"
                };
                cmd.CommandTimeout = 900;
                cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
                using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        TariffModel Customer = new TariffModel();
                        List<TariffModel> Pay = new List<TariffModel>();
                        while (rdr.Read())
                        {

                            Customer = new TariffModel();

                            Customer.TariffID = rdr[0].ToString();
                            Customer.TariffCode = rdr[1].ToString();
                            Customer.Description = rdr[2].ToString();
                            Customer.TariffRate = rdr[3].ToString();
                            Pay.Add(Customer);
                        }
                        conn.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, Pay);
                    }
                    else
                    {
                        conn.Close();
                        var message = string.Format("The Tariff Table does not contain any tariffRates. Please try again later ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                var message = string.Format("Could not get the tariff List because " + ex.Message);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);

            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/CheckAppVersion")]
        public HttpResponseMessage CheckAppVersion()
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";


            var Verison = db.RCDC_AppVersions.FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, Verison);
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/OnboardNewCustomer")]
        public HttpResponseMessage OnboardNewCustomer(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here
            if (Data == null || string.IsNullOrEmpty(Data.OtherNames))
            {
                var message = string.Format("Please input the customers Name to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }


            if (Data == null || string.IsNullOrEmpty(Data.Surname))
            {
                var message = string.Format("Please input the surname to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            RCDC_OnboardCustomers add = new RCDC_OnboardCustomers(); 
            string TicketNo = RandomPassword.Generate(10).ToString(); 
            add.TicketNo = TicketNo;
            add.ApplicantsSignature = Data.ApplicantsSignature;
            add.BookCode = Data.BookCode;
            add.CapturedBy = Data.CapturedBy;
            add.CommunityName = Data.CommunityName;
            add.CustomerEmail = Data.CustomerEmail;
            add.OfficeEmail = Data.OfficeEmail;
            add.CustomerLoad = Data.CustomerLoad;
            add.DateCaptured = DateTime.Now;
            add.DTRCode = Data.DTRCode;
            add.DTRName = Data.DTRName;
            add.FeederId = Data.FeederId;
            add.FeederName = Data.FeederName;
            add.filePaths = Data.filePaths;
            add.HouseNo = Data.HouseNo;
            add.LandMark = Data.LandMark;
            add.Latitude = Data.Latitude;
            add.LGA = Data.LGA;
            add.Longitude = Data.Longitude;
            add.MDA = Data.MDA;
            add.MeansOfIdentification = Data.MeansOfIdentification;
            add.MeterNo = Data.MeterNo;
            add.NearbyAccountNo = Data.NearbyAccountNo;
            add.Occupation = Data.Occupation; 
            add.DebulkingNumber = Data.DebulkingNumber;
            add.OnboardCategory = Data.OnboardCategory;
            add.OtherNames = Data.OtherNames;
            add.ParentAccountNo = Data.ParentAccountNo;
            add.Passport = Data.Passport;
            add.PhoneNumber1 = Data.PhoneNumber1;
            add.PhoneNumber2 = Data.PhoneNumber2;
            add.State = Data.State;
            add.Status = "FEEDER";
            add.StreetName = Data.StreetName;
            add.Surname = Data.Surname;
            add.TypeOfMeterRequired = Data.TypeOfMeterRequired;
            add.TypeOfPremises = Data.TypeOfPremises;
            add.UseOfPremises = Data.UseOfPremises;
            add.UserId = Data.UserId;
            add.StaffId = Data.UserId;
            add.ZipCode = Data.ZipCode;
            add.Zone = Data.Zone;

            db.RCDC_OnboardCustomerss.Add(add);
            db.SaveChanges();

            DOCUMENTS Docs = new DOCUMENTS();

            List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);

            string[] filePaths = der.ToArray();

            for (int i = 0; i < filePaths.Length; i++)
            {
                string ImgName = filePaths[i].ToString();
                var name = ImgName.Split('.');

                String filename = name[0];
                String fileext = name[3];

                Docs = new DOCUMENTS();

                if (Data.OnboardCategory == "SEPARATION")
                {

                    Docs.COMMENTS = "Separation Data for " + Data.ParentAccountNo;
                    Docs.DOCUMENT_NAME = "Disconnection Data for " + Data.ParentAccountNo;
                    Docs.DocumentDescription = "Disconnection Documents for " + Data.ParentAccountNo;
                    Docs.STATUS = "SEPARATION";
                }
                else
                {
                    Docs.COMMENTS = "New Connection Data for " + TicketNo;
                    Docs.DOCUMENT_NAME = "Disconnection Data for " + TicketNo;
                    Docs.DocumentDescription = "Disconnection Documents for " + TicketNo;
                    Docs.STATUS = "NEW CONNECTION";
                }

                Docs.DATE_UPLOADED = DateTime.Now;
                Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                Docs.DOCUMENT_EXTENSION = fileext;
                Docs.DOCUMENT_PATH = filePaths[i].ToString();
                Docs.REFERENCE_CODE = TicketNo;
                Docs.SENDER_ID = Data.UserId;
                Docs.Size = "123KB";
                db.DOCUMENTSs.Add(Docs);
                db.SaveChanges();
            }

            Audit.AuditTrail(Data.UserId, TicketNo + " was Onboarded on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "NEW CUSTOMER");

            RCDCModel Success = new RCDCModel();
            if (Docs.STATUS == "SEPARATION")
            {

                Success.Message = "The Customer with Ticket Number " + TicketNo + "  has been submitted for separation.";
                Success.Status = "SUCCESS";
            }
            else if (Docs.STATUS == "NEW CONNECTION")
            {
                Success.Message = "The Customer with Ticket Number " + TicketNo + "  has been onboarded Successfully";
                Success.Status = "SUCCESS";
            }

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectCustomer")]
        public HttpResponseMessage DisconnectCustomer(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here
            if (Data == null || string.IsNullOrEmpty(Data.AverageBillReading))
            {
                var message = string.Format("Please input the Average Billing to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("Please input the StaffId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("Please input the ReasonForDisconnectionId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //Disconnect Customer

            if (Data == null || string.IsNullOrEmpty(Data.AccountType))
            {
                var message = string.Format("Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.DateOfLastPayment))
            {
                var message = string.Format("The Load of the Customer has not been passed. please pass it to continue.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("The Reason for Disconnection was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                var message = string.Format("The Account Number was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Phase))
            {
                var message = string.Format("Please supply the Phase of the Meter to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Tariff))
            {
                var message = string.Format("Please supply the Tariff of the Customer to proceed xx.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (string.IsNullOrEmpty(Data.GangID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Gang ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.AverageBillReading))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Customers Average Bill Reading to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the CustomerPhoneNo as *CustomerPhone* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Customer Email as *CustomerEmail* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Staff ID as *StaffId* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the Account Number as *AccountNo* of the Customer to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.AccountType))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the Account type of the Customer as *AccountType* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.UserId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the UserId of the Staff as *UserId* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.DisconnId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Disconnection Id is Missing.Kindly Supply it as *DisconnId* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Latitude is Missing.Kindly Supply it as *Latitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The CustomerEmail is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The CustomerPhone is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } if (string.IsNullOrEmpty(Data.TariffRate))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Tariff Rate is Missing.Kindly Supply it as *TariffRate* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            {
                RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId); 
                if (Discon == null)
                {
                    var message = string.Format("The Disconnection Id is wrong. No record was Found. Please cross check and try again. Thank you.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NoContent, err);
                }
                else
                {

                    if (Data.Settlement_Status == "YES")
                    {
                        Discon.Settlement_Period = Data.Settlement_Period;
                        Discon.Settlement_Amount = Data.Settlement_Amount;
                        Discon.Settlement_Status = Data.Settlement_Status;
                        Discon.Settlement_Agreement = "NOT SIGNED";
                    }

                    Discon.DisconStatus = "DISCONNECTED";
                    Discon.DisconReason = Data.Comments;
                    Discon.DateOfDiscon = DateTime.Now;
                    Discon.Tariff = Data.Tariff;
                    Discon.TariffAmount = Data.TariffRate;
                    Discon.DisconBy = Data.StaffId;
                    Discon.Gang_ID = Data.GangID;
                    Discon.Latitude = Data.Latitude;
                    Discon.Longitude = Data.Longitude;
                    Discon.CustomerEmail = Data.CustomerEmail;
                    Discon.CustomerPhone = Data.CustomerPhone;

                    db.Entry(Discon).State = EntityState.Modified;
                    db.SaveChanges();
                }
              
                //Insert the Incidences
                RCDC_Disconnection_Incidence_History incid = new RCDC_Disconnection_Incidence_History();
                List<RCDC_Disconnection_Incidence_History> _incid = new List<RCDC_Disconnection_Incidence_History>();
                 
                //Settlement
                 
                _incid = GenerateCustomerIncidenceList(Data);

                if (_incid.Count > 0 || _incid != null)
                {
                    foreach (var I in _incid)
                    {
                        incid = new RCDC_Disconnection_Incidence_History();
                        var _IncidenceName = db.RCDC_Incidences.FirstOrDefault(p => p.IncidenceId == I.IncidenceId);
                        string IncidenceName = "";
                        if (_IncidenceName != null)
                        {
                            IncidenceName = _IncidenceName.IncidenceName.ToString(); 
                        }
                        incid.IncidenceAmount = I.IncidenceAmount;
                        incid.IncidenceName = IncidenceName;
                        incid.IncidenceDefaultId = Guid.NewGuid().ToString();
                        incid.Status = "NOT PAID";
                        incid.IncidenceId = I.IncidenceId;
                        incid.DisconnId = Data.DisconnId;
                        incid.CalculateAmount = "NO";
                        incid.Percentpayment = I.Percentpayment;
                        incid.DurationInDays = I.DurationInDays;
                        incid.DateDisconnected = DateTime.Now;
                        db.RCDC_Disconnection_Incidence_Historys.Add(incid);
                        db.SaveChanges();

                        //Insert Incidence into DLENHANCE for POSTPAID and PREPAID

                        if (Discon.GeneratedBy == "RPD")
                        {
                          //  Dont Allow Incidence to Flow

                            if (I.IncidenceName == "Loss of Revenue" || I.IncidenceId == "")
                            {

                                Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId);
                                //----------------------------------
                                Discon.RPDApproval = "NOT APPROVED";
                                Discon.IADApproval = "NOT APPROVED";
                                Discon.RPDCalculatedLoad = Data.LoadProfile;
                                Discon.RPDLossOfRevenueAmount = I.IncidenceAmount;
                                Discon.RPDLossOfRevenueAvailabilty = Data.Availability;
                                Discon.RPDLossOfRevenueInfractionDuration = I.DurationInDays;
                                db.Entry(Discon).State = EntityState.Modified;
                                db.SaveChanges();
                                //-------------------------
                            
                            }
                        }
                        else
                        {
                            try
                            {
                                AddIncidenceToDLEnhance(I.IncidenceAmount, I.IncidenceId, Data.AccountNo, IncidenceName, "PHEDConnect-" + Data.StaffId, Discon.AccountType);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                 
                //Get his Average and Stop his Billing
                if (Data.AccountType == "POSTPAID")
                {
                    // do Something About his Billing and Write to RCDC_Bill_Adjustment


                    RCDC_Spot_Billing Bill = new RCDC_Spot_Billing();
                    Bill.AvgConsumption = Data.AverageBillReading;
                    Bill.BilledQty = CalcualteBilledQty(Data.AverageBillReading).ToString();
                    Bill.BillingDate = DateTime.Now;
                    Bill.BillingId = Guid.NewGuid().ToString();
                    Bill.DisconnId = Data.DisconnId;
                    Bill.NoofDaysBilled = DateTime.Now.Day.ToString();
                    Bill.Status = "BILLED";
                    Bill.Year = DateTime.Now.Year.ToString();
                    db.RCDC_Spot_Billings.Add(Bill);
                    db.SaveChanges();
                } 

                //

                //Save Images
                 
                DOCUMENTS Docs = new DOCUMENTS();

                //Deserialis the code herre 

                 string[] _incidences = JsonConvert.DeserializeObject<string[]>(Data.filePaths);

                List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);

                string[] filePaths = der.ToArray();

                for (int i = 0; i < filePaths.Length; i++)
                {
                    string ImgName =  filePaths[i].ToString();
                    var name = ImgName.Split('.');

                    String filename = name[0];
                    String fileext = name[3];
                    Docs = new DOCUMENTS();

                    Docs.COMMENTS = "Disconnection Data for " + Data.AccountNo;
                    Docs.DATE_UPLOADED = DateTime.Now;
                    Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                    Docs.DOCUMENT_EXTENSION = fileext;
                    Docs.DOCUMENT_NAME = "Disconnection Data for " + Data.AccountNo;
                    Docs.DOCUMENT_PATH = filePaths[i].ToString();
                    Docs.DocumentDescription = "Disconnection Documents for " + Data.AccountNo;
                    Docs.REFERENCE_CODE = Data.DisconnId;
                    Docs.SENDER_ID = Data.UserId;
                    Docs.Size = "123KB";
                    Docs.STATUS = "DISCONNECTION";

                    db.DOCUMENTSs.Add(Docs);
                    db.SaveChanges();
                }


              //  save the Load Profile

                RCDC_LoadApplicances app = new RCDC_LoadApplicances();
                List<RCDC_LoadApplicances> RCDCApp = JsonConvert.DeserializeObject<List<RCDC_LoadApplicances>>(Data.ListOfAppliances);

                foreach (var a in RCDCApp)
                {   app = new RCDC_LoadApplicances();
                    app.AccountNo = Data.AccountNo;
                    app.DisconId = Data.DisconnId;
                    app.ApplianceName = a.ApplianceName;
                    app.Qty = a.Qty;
                    app.ApplianceId = a.ApplianceId;
                    app.TotalWattage = a.TotalWattage;
                    app.LoadApplianceId = Guid.NewGuid().ToString();
                    db.RCDCLoadApplicancess.Add(app);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                RCDCModel success = new RCDCModel();

                success.Message = "The Customer with Account Number " + Data.AccountNo + " was not disconnected because " + ex.Message;
                success.Status = "FAILED";
                return Request.CreateResponse(HttpStatusCode.NotFound, success);
            }
            //AUDIT TRAIL
            Audit.AuditTrail(Data.UserId, Data.AccountNo + " was Disconnected on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "DISCONNECTION");

            RCDCModel Success = new RCDCModel();

            Success.Message = "The Customer with Account Number " + Data.AccountNo + "  has been disconnected Successfully";
            Success.Status = "SUCCESS";

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }

    [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/ApproveAccountSeparation")]
        public HttpResponseMessage ApproveAccountSeparation(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here
            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                var message = string.Format("Please input the Account No to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("Please input the StaffId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("Please input the ReasonForDisconnectionId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //Disconnect Customer

            if (Data == null || string.IsNullOrEmpty(Data.AccountType))
            {
                var message = string.Format("Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.DateOfLastPayment))
            {
                var message = string.Format("The Load of the Customer has not been passed. please pass it to continue.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("The Reason for Disconnection was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                var message = string.Format("The Account Number was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Phase))
            {
                var message = string.Format("Please supply the Phase of the Meter to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Tariff))
            {
                var message = string.Format("Please supply the Tariff of the Customer to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
         
            if (string.IsNullOrEmpty(Data.GangID))
            { 
                var message = string.Format("Please input the Gang ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.AverageBillReading))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Customers Average Bill Reading to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the CustomerPhoneNo as *CustomerPhone* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Customer Email as *CustomerEmail* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Staff ID as *StaffId* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the Account Number as *AccountNo* of the Customer to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.AccountType))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the Account type of the Customer as *AccountType* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.UserId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please supply the UserId of the Staff as *UserId* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.DisconnId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Disconnection Id is Missing.Kindly Supply it as *DisconnId* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.Latitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The   Latitude is Missing.Kindly Supply it as *Latitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The CustomerEmail is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The CustomerPhone is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } if (string.IsNullOrEmpty(Data.TariffRate))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Tariff Rate is Missing.Kindly Supply it as *TariffRate* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            {
                RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId); 
                if (Discon == null)
                {
                    var message = string.Format("The Disconnection Id is wrong. No record was Found. Please cross check and try again. Thank you.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NoContent, err);
                }
                else
                {

                    if (Data.Settlement_Status == "YES")
                    {
                        Discon.Settlement_Period = Data.Settlement_Period;
                        Discon.Settlement_Amount = Data.Settlement_Amount;
                        Discon.Settlement_Status = Data.Settlement_Status;
                        Discon.Settlement_Agreement = "NOT SIGNED";
                    }

                    Discon.DisconStatus = "DISCONNECTED";
                    Discon.DisconReason = Data.Comments;
                    Discon.DateOfDiscon = DateTime.Now;
                    Discon.Tariff = Data.Tariff;
                    Discon.TariffAmount = Data.TariffRate;
                    Discon.DisconBy = Data.StaffId;
                    Discon.Gang_ID = Data.GangID;
                    Discon.Latitude = Data.Latitude;
                    Discon.Longitude = Data.Longitude;
                    Discon.CustomerEmail = Data.CustomerEmail;
                    Discon.CustomerPhone = Data.CustomerPhone;

                    db.Entry(Discon).State = EntityState.Modified;
                    db.SaveChanges();
                }
              
                //Insert the Incidences
                RCDC_Disconnection_Incidence_History incid = new RCDC_Disconnection_Incidence_History();
                List<RCDC_Disconnection_Incidence_History> _incid = new List<RCDC_Disconnection_Incidence_History>();
                 
                //Settlement
                 
                _incid = GenerateCustomerIncidenceList(Data);

                if (_incid.Count > 0 || _incid != null)
                {
                    foreach (var I in _incid)
                    {
                        incid = new RCDC_Disconnection_Incidence_History();
                        var _IncidenceName = db.RCDC_Incidences.FirstOrDefault(p => p.IncidenceId == I.IncidenceId);
                        string IncidenceName = "";
                        if (_IncidenceName != null)
                        {
                            IncidenceName = _IncidenceName.IncidenceName.ToString(); 
                        }
                        incid.IncidenceAmount = I.IncidenceAmount;
                        incid.IncidenceName = IncidenceName;
                        incid.IncidenceDefaultId = Guid.NewGuid().ToString();
                        incid.Status = "NOT PAID";
                        incid.IncidenceId = I.IncidenceId;
                        incid.DisconnId = Data.DisconnId;
                        incid.CalculateAmount = "NO";
                        incid.Percentpayment = I.Percentpayment;
                        incid.DurationInDays = I.DurationInDays;
                        incid.DateDisconnected = DateTime.Now;
                        db.RCDC_Disconnection_Incidence_Historys.Add(incid);
                        db.SaveChanges();

                        //Insert Incidence into DLENHANCE for POSTPAID and PREPAID

                        if (Discon.GeneratedBy == "RPD")
                        {
                          //  Dont Allow Incidence to Flow

                            if (I.IncidenceName == "Loss of Revenue" || I.IncidenceId == "")
                            {

                                Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId);
                                //----------------------------------
                                Discon.RPDApproval = "NOT APPROVED";
                                Discon.IADApproval = "NOT APPROVED";
                                Discon.RPDCalculatedLoad = Data.LoadProfile;
                                Discon.RPDLossOfRevenueAmount = I.IncidenceAmount;
                                Discon.RPDLossOfRevenueAvailabilty = Data.Availability;
                                Discon.RPDLossOfRevenueInfractionDuration = I.DurationInDays;
                                db.Entry(Discon).State = EntityState.Modified;
                                db.SaveChanges();
                                //-------------------------
                            
                            }
                        }
                        else
                        {
                            try
                            {
                                AddIncidenceToDLEnhance(I.IncidenceAmount, I.IncidenceId, Data.AccountNo, IncidenceName, "PHEDConnect-" + Data.StaffId, Discon.AccountType);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                 
                //Get his Average and Stop his Billing
                if (Data.AccountType == "POSTPAID")
                {
                    // do Something About his Billing and Write to RCDC_Bill_Adjustment


                    RCDC_Spot_Billing Bill = new RCDC_Spot_Billing();
                    Bill.AvgConsumption = Data.AverageBillReading;
                    Bill.BilledQty = CalcualteBilledQty(Data.AverageBillReading).ToString();
                    Bill.BillingDate = DateTime.Now;
                    Bill.BillingId = Guid.NewGuid().ToString();
                    Bill.DisconnId = Data.DisconnId;
                    Bill.NoofDaysBilled = DateTime.Now.Day.ToString();
                    Bill.Status = "BILLED";
                    Bill.Year = DateTime.Now.Year.ToString();
                    db.RCDC_Spot_Billings.Add(Bill);
                    db.SaveChanges();
                } 

                //

                //Save Images
                 
                DOCUMENTS Docs = new DOCUMENTS();

                //Deserialis the code herre 

                 string[] _incidences = JsonConvert.DeserializeObject<string[]>(Data.filePaths);

                List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);

                string[] filePaths = der.ToArray();

                for (int i = 0; i < filePaths.Length; i++)
                {
                    string ImgName =  filePaths[i].ToString();
                    var name = ImgName.Split('.');

                    String filename = name[0];
                    String fileext = name[3];
                    Docs = new DOCUMENTS();

                    Docs.COMMENTS = "Disconnection Data for " + Data.AccountNo;
                    Docs.DATE_UPLOADED = DateTime.Now;
                    Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                    Docs.DOCUMENT_EXTENSION = fileext;
                    Docs.DOCUMENT_NAME = "Disconnection Data for " + Data.AccountNo;
                    Docs.DOCUMENT_PATH = filePaths[i].ToString();
                    Docs.DocumentDescription = "Disconnection Documents for " + Data.AccountNo;
                    Docs.REFERENCE_CODE = Data.DisconnId;
                    Docs.SENDER_ID = Data.UserId;
                    Docs.Size = "123KB";
                    Docs.STATUS = "DISCONNECTION";

                    db.DOCUMENTSs.Add(Docs);
                    db.SaveChanges();
                }


              //  save the Load Profile

                RCDC_LoadApplicances app = new RCDC_LoadApplicances();
                List<RCDC_LoadApplicances> RCDCApp = JsonConvert.DeserializeObject<List<RCDC_LoadApplicances>>(Data.ListOfAppliances);

                foreach (var a in RCDCApp)
                {   app = new RCDC_LoadApplicances();
                    app.AccountNo = Data.AccountNo;
                    app.DisconId = Data.DisconnId;
                    app.ApplianceName = a.ApplianceName;
                    app.Qty = a.Qty;
                    app.ApplianceId = a.ApplianceId;
                    app.TotalWattage = a.TotalWattage;
                    app.LoadApplianceId = Guid.NewGuid().ToString();
                    db.RCDCLoadApplicancess.Add(app);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                RCDCModel success = new RCDCModel();

                success.Message = "The Customer with Account Number " + Data.AccountNo + " was not disconnected because " + ex.Message;
                success.Status = "FAILED";
                return Request.CreateResponse(HttpStatusCode.NotFound, success);
            }
            //AUDIT TRAIL
            Audit.AuditTrail(Data.UserId, Data.AccountNo + " was Disconnected on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "DISCONNECTION");

            RCDCModel Success = new RCDCModel();

            Success.Message = "The Customer with Account Number " + Data.AccountNo + "  has been disconnected Successfully";
            Success.Status = "SUCCESS";

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/TariffClassChange")]
        public HttpResponseMessage TariffClassChange(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";
             
            #region  Change Tariff

             

            if (Data == null || string.IsNullOrEmpty(Data.CustomerEmail))
            {
                var message = string.Format("Please input the Customer Email to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.CapturedBy))
            {
                var message = string.Format("Please input the surname to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.DTRCode))
            {
                var message = string.Format("Please input the DTRCode to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

             
            RCDC_OnboardCustomers add = new RCDC_OnboardCustomers();  
            string TicketNo = RandomPassword.Generate(10).ToString();

            add.TicketNo = TicketNo;
           // add.ApplicantsSignature = Data.ApplicantsSignature;
          //  add.BookCode = Data.BookCode;
            add.CapturedBy = Data.CapturedBy;
          //  add.CommunityName = Data.CommunityName;
            add.CustomerEmail = Data.CustomerEmail;
           // add.OfficeEmail = Data.OfficeEmail;
           // add.CustomerLoad = Data.CustomerLoad;
            add.DateCaptured = DateTime.Now;
            add.DTRCode = Data.DTRCode;
            add.DTRName = Data.DTRName;
            add.FeederId = Data.FeederId;
            add.FeederName = Data.FeederName;
           // add.filePaths = Data.filePaths;
            add.HouseNo = Data.HouseNo;
            //add.LandMark = Data.LandMark;
            add.Latitude = Data.Latitude;
            //add.LGA = Data.LGA;
            add.Longitude = Data.Longitude;
            add.ParentAccountNo = Data.AccountNo;


            //add.MDA = Data.MDA;
           // add.MeansOfIdentification = Data.MeansOfIdentification;
            add.MeterNo = Data.MeterNo;
           // add.NearbyAccountNo = Data.NearbyAccountNo;
           // add.Occupation = Data.Occupation;

            //add.DebulkingNumber = Data.DebulkingNumber;
            add.OnboardCategory = "TARIFF CHANGE";
            add.OtherNames = Data.OtherNames;
            add.ParentAccountNo = Data.ParentAccountNo;
            add.Passport = Data.Passport;
           // add.PhoneNumber1 = Data.PhoneNumber1;
           // add.PhoneNumber2 = Data.PhoneNumber2;
            //add.State = Data.State;
            add.Status = "FEEDER";
            add.StreetName = Data.StreetName;
            add.Surname = Data.AccountName;
           // add.TypeOfMeterRequired = Data.TypeOfMeterRequired;
            add.TypeOfPremises = Data.TypeOfPremises;
          add.UseOfPremises = Data.UseOfPremises;
            add.UserId = Data.UserId;
            add.StaffId = Data.StaffId;
            add.OldTariff =  Data.OldTariff;
            add.NewTariff =  Data.NewTariff;
           // add.ZipCode = Data.ZipCode;
            add.Zone = Data.Zone; 
            db.RCDC_OnboardCustomerss.Add(add);
            db.SaveChanges();
             
            #endregion 

            RCDCModel Success = new RCDCModel();

            Success.Message = "The Tariff Change request for Account Number " + Data.AccountNo + "  has been submitted Successfully. Necessary approvals will be needed to effect this on the Bill";
            Success.Status = "SUCCESS";

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }
         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/DisconnectCustomerDTR")]
        public HttpResponseMessage DisconnectCustomerDTR(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";
            
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("Please input the StaffId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("Please input the ReasonForDisconnectionId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
  if (Data == null || string.IsNullOrEmpty(Data.DisconNoticeNo))
            {
                var message = string.Format("Please input the DisconNotice No  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            //Disconnect Customer

            if (Data == null || string.IsNullOrEmpty(Data.AccountType))
            {
                var message = string.Format("Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

          
            if (string.IsNullOrEmpty(Data.GangID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Gang ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the CustomerPhoneNo as *CustomerPhone* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Customer Email as *CustomerEmail* to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
             
            //if (string.IsNullOrEmpty(Data.UserId))
            //{
            //    var message = string.Format("Please supply the UserId of the Staff as *UserId* to Proceed. Thank you");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            if (string.IsNullOrEmpty(Data.DisconnId)){
                var message = string.Format("The Disconnection Id is Missing.Kindly Supply it as *DisconnId* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.Latitude))
            {
                var message = string.Format("The   Latitude is Missing.Kindly Supply it as *Latitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (string.IsNullOrEmpty(Data.Longitude))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Longitude is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerEmail))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32); 
                var message = string.Format("The CustomerEmail is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (string.IsNullOrEmpty(Data.CustomerPhone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The CustomerPhone is Missing.Kindly Supply it as *Longitude* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            } 

            try
            {
                RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId); 
                if (Discon == null)
                {
                    var message = string.Format("The Disconnection Id is wrong. No record was Found. Please cross check and try again. Thank you.");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NoContent, err);
                }
                else
                { 
                    //if (Data.Settlement_Status == "YES")
                    //{
                    //    Discon.Settlement_Period = Data.Settlement_Period;
                    //    Discon.Settlement_Amount = Data.Settlement_Amount;
                    //    Discon.Settlement_Status = Data.Settlement_Status;
                    //    Discon.Settlement_Agreement = "NOT SIGNED";
                    //}

                    Discon.DisconStatus = "DISCONNECTED";
                    Discon.DisconReason = Data.Comments;
                    Discon.DateOfDiscon = DateTime.Now;
                    //Discon.Tariff = Data.Tariff;
                    
                    Discon.DisconNoticeNo = Data.DisconNoticeNo;
                    //Discon.TariffAmount = Data.TariffRate;
                    Discon.DisconBy = Data.StaffId; 
                    Discon.Gang_ID = Data.GangID;
                    Discon.Latitude = Data.Latitude;
                    Discon.Longitude = Data.Longitude;
                    Discon.CustomerEmail = Data.CustomerEmail;
                    Discon.CustomerPhone = Data.CustomerPhone;

                    db.Entry(Discon).State = EntityState.Modified;
                    db.SaveChanges();
                }
              
                //Insert the Incidences
                RCDC_Disconnection_Incidence_History incid = new RCDC_Disconnection_Incidence_History();
                List<RCDC_Disconnection_Incidence_History> _incid = new List<RCDC_Disconnection_Incidence_History>();
                 
                //Settlement

                _incid = GenerateCustomerIncidenceListDTR(Data);

                if (_incid.Count > 0 || _incid != null)
                {
                    foreach (var I in _incid)
                    {
                        incid = new RCDC_Disconnection_Incidence_History();
                        var _IncidenceName = db.RCDC_Incidences.FirstOrDefault(p => p.IncidenceId == I.IncidenceId);
                        string IncidenceName = "";
                        if (_IncidenceName != null)
                        {
                            IncidenceName = _IncidenceName.IncidenceName.ToString(); 
                        }
                        incid.IncidenceAmount = I.IncidenceAmount;
                        incid.IncidenceName = IncidenceName;
                        incid.IncidenceDefaultId = Guid.NewGuid().ToString();
                        incid.Status = "NOT PAID";
                        incid.IncidenceId = I.IncidenceId;
                        incid.DisconnId = Data.DisconnId;
                        incid.CalculateAmount = "NO";
                        incid.Percentpayment = I.Percentpayment;
                        incid.DurationInDays = I.DurationInDays;
                        incid.DateDisconnected = DateTime.Now;
                        db.RCDC_Disconnection_Incidence_Historys.Add(incid);
                        db.SaveChanges();

                         
                        if (I.IncidenceName == "Loss of Revenue" || I.IncidenceId == "")
                        { 
                            Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId);
                            //----------------------------------
                            Discon.RPDApproval = "NOT APPROVED";
                            Discon.IADApproval = "NOT APPROVED";
                            Discon.RPDCalculatedLoad = Data.LoadProfile;
                            Discon.RPDLossOfRevenueAmount = I.IncidenceAmount;
                            Discon.RPDLossOfRevenueAvailabilty = Data.Availability;
                            Discon.RPDLossOfRevenueInfractionDuration = I.DurationInDays;
                            db.Entry(Discon).State = EntityState.Modified;
                            db.SaveChanges();
                            //------------------------- 
                        }
                    }
                    if (Data.AccountType == "POSTPAID")
                    {
                        // do Something About his Billing and Write to RCDC_Bill_Adjustment


                        RCDC_Spot_Billing Bill = new RCDC_Spot_Billing();
                        Bill.AvgConsumption = Data.AverageBillReading;
                        Bill.BilledQty = CalcualteBilledQty(Data.AverageBillReading).ToString();
                        Bill.BillingDate = DateTime.Now;
                        Bill.BillingId = Guid.NewGuid().ToString();
                        Bill.DisconnId = Data.DisconnId;
                        Bill.NoofDaysBilled = DateTime.Now.Day.ToString();
                        Bill.Status = "BILLED";
                        Bill.Year = DateTime.Now.Year.ToString();
                        db.RCDC_Spot_Billings.Add(Bill);
                        db.SaveChanges();
                    }
                }
                 
                //Get his Average and Stop his Billing
              

                //

                //Save Images
                 
                //DOCUMENTS Docs = new DOCUMENTS();

                ////Deserialis the code herre 

                //// string[] _incidences = JsonConvert.DeserializeObject<string[]>(Data.filePaths);

                //List<string> der = JsonConvert.DeserializeObject<List<string>>(Data.filePaths);

                //string[] filePaths = der.ToArray();

                //for (int i = 0; i < filePaths.Length; i++)
                //{
                //    string ImgName =  filePaths[i].ToString();
                //    var name = ImgName.Split('.');

                //    String filename = name[0];
                //    String fileext = name[3];
                //    Docs = new DOCUMENTS();

                //    Docs.COMMENTS = "Disconnection Data for " + Data.AccountNo;
                //    Docs.DATE_UPLOADED = DateTime.Now;
                //    Docs.DOCUMENT_CODE = Guid.NewGuid().ToString();
                //    Docs.DOCUMENT_EXTENSION = fileext;
                //    Docs.DOCUMENT_NAME = "Disconnection Data for " + Data.AccountNo;
                //    Docs.DOCUMENT_PATH = filePaths[i].ToString();
                //    Docs.DocumentDescription = "Disconnection Documents for " + Data.AccountNo;
                //    Docs.REFERENCE_CODE = Data.DisconnId;
                //    Docs.SENDER_ID = Data.UserId;
                //    Docs.Size = "123KB";
                //    Docs.STATUS = "DISCONNECTION";

                //    db.DOCUMENTSs.Add(Docs);
                //    db.SaveChanges();
                //}


                //save the Load Profile

                //RCDC_LoadApplicances app = new RCDC_LoadApplicances();
                //List<RCDC_LoadApplicances> RCDCApp = JsonConvert.DeserializeObject<List<RCDC_LoadApplicances>>(Data.ListOfAppliances);

                //foreach (var a in RCDCApp)
                //{   app = new RCDC_LoadApplicances();
                //    app.AccountNo = Data.AccountNo;
                //    app.DisconId = Data.DisconnId;
                //    app.ApplianceName = a.ApplianceName;
                //    app.Qty = a.Qty;
                //    app.ApplianceId = a.ApplianceId;
                //    app.TotalWattage = a.TotalWattage;
                //    app.LoadApplianceId = Guid.NewGuid().ToString();
                //    db.RCDCLoadApplicancess.Add(app);
                //    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {

                RCDCModel success = new RCDCModel();

                success.Message = "The Customer with Account Number " + Data.AccountNo + " was not disconnected because " + ex.Message;
                success.Status = "FAILED";
                return Request.CreateResponse(HttpStatusCode.NotFound, success);
            }
            //AUDIT TRAIL
            Audit.AuditTrail(Data.UserId, Data.AccountNo + " was Disconnected on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "DISCONNECTION");

            RCDCModel Success = new RCDCModel();

            Success.Message = "The Customer with Account Number " + Data.AccountNo + "  has been disconnected Successfully";
            Success.Status = "SUCCESS";

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }
          
        private void AddIncidenceToDLEnhance(string IncidenceAmount, string IncidenceId, string AccountNo, string IncidenceName, string CreatedBy, string AccountType)
        {
               conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString());
            try
            {
                conn.Open();

            String status = "";
               OracleCommand cmd = new OracleCommand
               {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ENSERV.SP_WF_INSERT_INCIDENCE_CUSTOMERACCOUNT"
               };
               cmd.CommandTimeout = 900;
               cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.Int64, ParameterDirection.Output));
               cmd.Parameters.Add("P_ACCOUNTNO", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountNo;
               cmd.Parameters.Add("P_INCIDENCEID", OracleDbType.Varchar2, ParameterDirection.Input).Value = IncidenceId;
               cmd.Parameters.Add("P_INCIDENCEAMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = IncidenceAmount;
               cmd.Parameters.Add("P_CREATEDBY", OracleDbType.Varchar2, ParameterDirection.Input).Value = CreatedBy;
               cmd.Parameters.Add("P_ACCOUNTTYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = AccountType;
              using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        status = "INSERTED";

                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            cmd.Dispose();
           
                }
                catch (Exception ex)
                {
                conn.Close();
                conn.Dispose();

            }

        }

        private decimal CalcualteBilledQty(string AvgBillConsumption)
        {

            decimal BilledQty = 0;
            //This is a Representation of the 30 day Period for the Month in View
            int NoOfDays = DateTime.Now.Day;
            try
            {
                BilledQty = ((Convert.ToDecimal(AvgBillConsumption) / 30) * NoOfDays);

            }
            catch (Exception ex)
            {

            }





            return BilledQty;


        }

        private decimal CalculateAmountToPay(DateTime? nullable1, DateTime? nullable2, decimal? nullable3, string p)
        {
            return 120;
        }

        //     [System.Web.Http.HttpPost]
        //     [Route("api/PHEDConnectAPI/DisconnectCustomer")]
        //public HttpResponseMessage DisconnectCustomer(RCDCModel Data)
        //     {
        //         RCDCModel d = new RCDCModel();
        //         db = new ApplicationDbContext();


        //         if (Data == null || Data.Incidence.Count <= 0)
        //         {
        //             // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

        //             var message = string.Format("Please input the incidences for the Disconnection to Proceed. Thank you");
        //             HttpError err = new HttpError(message);
        //             return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //         }



        //         if (Data == null || string.IsNullOrEmpty(Data.StaffId))
        //         {
        //             // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

        //             var message = string.Format("Please input the Staff ID to Proceed. Thank you");
        //             HttpError err = new HttpError(message);
        //             return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //         }

        //         if (Data == null || string.IsNullOrEmpty(Data.Year))
        //         {
        //             // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

        //             var message = string.Format("Please select a Year and Try again");
        //             HttpError err = new HttpError(message);
        //             return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //         }

        //         if (Data == null || string.IsNullOrEmpty(Data.Month))
        //         {
        //             // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

        //             var message = string.Format("Please select a Month and Try again");
        //             HttpError err = new HttpError(message);
        //             return Request.CreateResponse(HttpStatusCode.NotFound, err);
        //         }
        //         else
        //         {
        //             //convert the Date to DateTime and Get Year

        //             string Year = DateTime.Now.Year.ToString();
        //             string Month = DateTime.Now.Month.ToString();

        //             var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.DisconBy == Data.StaffId && p.DisconStatus == "DISCONNECTED").ToList();

        //             return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
        //         }
        //     }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetDisconnectedListByStaffID")]
        public HttpResponseMessage GetDisconnectedListByStaffID(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.DisconBy == Data.StaffId && p.DisconStatus == "DISCONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetDisconnectedListByGang")]
        public HttpResponseMessage GetDisconnectedListByGang(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.GangID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.Gang_ID == Data.GangID && p.DisconStatus == "DISCONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetDisconnectedListByFeeder")]
        public HttpResponseMessage GetDisconnectedListByFeeder(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.Feeder))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.DisconStatus == "DISCONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetDisconnectedListByDate")]
        public HttpResponseMessage GetDisconnectedListByDate(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Disconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.DateOfDiscon == DateofDiscon && p.DisconStatus == "DISCONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetDisconnectedAccountByDate")]
        public HttpResponseMessage GetDisconnectedAccountByDate(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Disconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.DateOfDiscon == DateofDiscon && p.AccountNo == Data.AccountNo && p.DisconStatus == "DISCONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        #region Reconnection APIs


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/CheckEligibilityForReconnectionOLD")]
        public HttpResponseMessage CheckEligibilityForReconnectionOLD(RCDCModel Data, FormCollection collections, HttpPostedFileBase acquisitionFile)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            RCDC_Disconnection_Incidence_History Calc = new RCDC_Disconnection_Incidence_History();
            List<RCDC_Disconnection_Incidence_History> _Calc = new List<RCDC_Disconnection_Incidence_History>();


            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {

                var httpPostedFile = System.Web.HttpContext.Current.Request.Files;
                var _httpPostedFile = System.Web.HttpContext.Current.Request.Files["DocumentFile"];
                var DocumentName = System.Web.HttpContext.Current.Request.Params["DocumentName"];


                //pass the Names of the Fields from Emmanuel Here in the Form Data



                var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);


                for (int i = 0; i < httpPostedFile.Count; i++)
                {
                    HttpPostedFile file = httpPostedFile[i];
                    var Assignments = System.Web.HttpContext.Current.Request.Form[i].ToString();

                    string FN = Guid.NewGuid().ToString();

                    string Description = System.Web.HttpContext.Current.Request.Params["DocumentDescription"];
                    string Size = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                    string Extension = System.Web.HttpContext.Current.Request.Params["DocumentExtension"];


                }
                _httpPostedFile.SaveAs(fileSavePath);
            }


            if (Data == null || string.IsNullOrEmpty(Data.AccountType))
            {
                var message = string.Format("Please specify the Account type. Is this Prepaid or Postpaid?.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.DateOfLastPayment))
            {
                var message = string.Format("The Load of the Customer has not been passed. please pass it to continue.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.LoadProfile))
            {
                var message = string.Format("The Load of the Customer has not been passed. please pass it to continue.Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.ReasonForDisconnectionId))
            {
                var message = string.Format("The Reason for Disconnection was not passed. Please insert it and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Phase))
            {
                var message = string.Format("Please supply the Phase of the Meter to proceed.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            else
            {
                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();
                RCDC_Disconnection_Incidence IncidenceAmount = new RCDC_Disconnection_Incidence();
                List<RCDC_Disconnection_Incidence> _IncidenceAmount = new List<RCDC_Disconnection_Incidence>();



                var check = db.RCDC_Reasons_For_Disconnections.FirstOrDefault(p => p.ReasonForDisconnectionId == Data.ReasonForDisconnectionId);

                if (check != null)
                {
                    //This reason is a valid reason for Disconnection


                    //Go and Calculate the Various Incidences for the Customer and Return the incidences so the App can show him what he's owing in the cumulative


                    //Get all the Incidences that are attached to the Condition of the Customer

                    var In = db.RCDC_Incidence_For_Reasonss.Where(p => p.ReasonForDisconnectionId == Data.ReasonForDisconnectionId).ToList();
                    if (In.Count > 0)
                    {

                        //Duration of Days from the Last Recharge
                        int Duration = (DateTime.Now.Date - Convert.ToDateTime(Data.DateOfLastPayment).Date).Days;
                        string Availability = "17";
                        string AverageConsumption = "";
                        string AccountType = Data.AccountType;
                        string Load = Data.LoadProfile;





                        //there are incidences
                        foreach (var incidence in In)
                        {
                            //Energy Bill
                            //ReconnectionFee

                            Calc = new RCDC_Disconnection_Incidence_History();


                            //Calculate For loss of revenue
                            if (incidence.IncidenceId == "7")
                            {
                                //get the last date he Paid and use it to calculate the 


                                //get his Avaialbility

                                //Calculate the Loss of Revenue from the Incidence here and use it to calculate the Necessary amount to be paid
                              //  Calc.IncidenceAmount = GenerateLossOfRevenueAmount(Availability, Load, AverageConsumption, Duration, AccountType, Data.AccountNo, Data.Flag).ToString();
                                Calc.IncidenceId = incidence.IncidenceId;
                                Calc.Percentpayment = incidence.PercentageToPay;
                                Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                                Calc.ToDate = DateTime.Now;
                                Calc.DurationInDays = Duration;
                                Calc.DisconnId = Data.DisconnId;
                                Calc.DurationInDays = Duration;

                                _Calc.Add(Calc);
                            }

                            //Calculate For ENERGY BILL
                            if (incidence.IncidenceId == "14")
                            {
                                EnergyBill Bill = GetEnergyBillComponents(Data.AccountNo);

                                if (Bill.Status == "SUCCESS")
                                {
                                    //get the 100% of the Current Charge and the 
                                    double CurrentBillCharge = Convert.ToDouble(Bill.CurrentCharges) * 1;
                                    double PercentOfArrears = Convert.ToDouble(Bill.Arrears) * 0.1;

                                    Calc.IncidenceAmount = (CurrentBillCharge + PercentOfArrears).ToString();
                                    Calc.IncidenceId = incidence.IncidenceId;
                                    Calc.IncidenceName = incidence.Incidence.IncidenceName;
                                    Calc.Percentpayment = incidence.PercentageToPay;
                                    Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                                    Calc.ToDate = DateTime.Now;
                                    Calc.DurationInDays = Duration;
                                    //Calc.DurationInDays = Duration;
                                    Calc.DisconnId = Data.DisconnId;
                                    _Calc.Add(Calc);
                                    //get the 10% of the Arrears Charge and the  
                                }
                            }

                            //Add Reconnection Fee to the Guy
                            //Calculate For ENERGY BILL
                            if (incidence.IncidenceId == "10")
                            {
                               // EnergyBill Bill = GetEnergyBillComponents(Data.AccountNo);

                               
                                    //get the 100% of the Current Charge and the 
                                    //double CurrentBillCharge = Convert.ToDouble(Bill.CurrentCharges) * 1;
                                    //double PercentOfArrears = Convert.ToDouble(Bill.Arrears) * 0.1;

                                    if (Data.AccountType == "POSTPAID")
                                    {
                                        ///Get the Reconnection Cost for POSTPAID
                                        Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType).Amount;

                                    }
                                    if (Data.AccountType == "PREPAID")
                                    {
                                        ///Get the Reconnection Cost for POSTPAID
                                        ///
                                        Calc.IncidenceAmount = db.RCDCReconnectionFee.FirstOrDefault(p => p.AccountType == Data.AccountType && p.Phase == Data.Phase).Amount;

                                    }

                                    Calc.IncidenceId = incidence.IncidenceId;
                                    Calc.IncidenceName = incidence.Incidence.IncidenceName;
                                    Calc.Percentpayment = incidence.PercentageToPay;
                                    Calc.FromDate = Convert.ToDateTime(Data.DateOfLastPayment);
                                    Calc.ToDate = DateTime.Now;
                                    //Calc.DurationInDays = Duration;
                                    Calc.DisconnId = Data.DisconnId;
                                    _Calc.Add(Calc);
                                    //get the 10% of the Arrears Charge and the  
                                
                            }

                        }

                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, _Calc);
            }
        }

         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/CheckEligibilityForReconnection")]
        public HttpResponseMessage CheckEligibilityForReconnection(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; string AccountType = "";

                

                var DisconId = db.RCDCDisconnectionLists.FirstOrDefault(p => p.AccountNo == Data.AccountNo && p.DisconStatus == "DISCONNECTED");

                if (DisconId != null)
                { 
                    DisconnId = DisconId.DisconID;
                    AccountType = DisconId.AccountType;
                }
                else
                { 
                    message = string.Format("This customer has been Reconnected before or the Account Number was wrong.");
                    HttpError _err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, _err);
                }

                if (CheckIfHeIsEligibleForReconnection(AccountNo, DisconnId, AccountType))
                {

                    RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p=>p.DisconID == DisconnId);

                    if (Discon != null)
                    {
                        Discon.DisconStatus = "RECONNECT";

                        Discon.LastPayDate = getLastPaymentDate(AccountNo);
                        db.Entry(Discon).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    //He has been Added to the List for reconnection
                     
                    var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.AccountNo == AccountNo && p.DisconStatus == "RECONNECT" && p.DisconID == DisconnId).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    //he did not pass the test for Reconnection ;
                    message = "AccountNo " + AccountNo + " did not pass the test for reconnection and has not been added to the reconnection List ";
                }

            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityDisconnection")]
        public HttpResponseMessage MyActivityDisconnection(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconBy == Data.StaffId && p.DisconStatus == "DISCONNECTED").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_DisconnectionList> empty = new List<RCDC_DisconnectionList>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityEnumVerify")]
        public HttpResponseMessage MyActivityEnumVerify(MAPModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            List<EnumerationDataVerification> list = new List<EnumerationDataVerification>();
            DataSet ds = new DataSet();
            string msg = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("The StaffId was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {

                string constr = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;

                using (SqlConnection con = new SqlConnection(NOMSConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("MyActivityEnumVerify", con))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@StaffData", SqlDbType.VarChar).Value = Data.StaffId; 

                            con.Open();
                            SqlDataReader rdr = cmd.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    var DateVerified = rdr[0].ToString();
                                    var AccountNo = rdr[1].ToString();
                                    var AccountName = rdr[2].ToString();
                                    var VerifiedBy = rdr[3].ToString();
                                    var PremiseId = rdr[3].ToString();
                                    var Address = rdr[4].ToString();
                                    var ConnectionType = rdr[4].ToString();

                                    var Longitude = rdr[5].ToString();
                                    var Latitude = rdr[6].ToString();
                                    var VerificationStatus = rdr[7].ToString();


                                    list.Add(new EnumerationDataVerification
                                    {

                                        AccountNo = AccountNo,
                                        CustomerName = AccountName,
                                        PremiseId = PremiseId,
                                        Address = Address,
                                        Longitude = Longitude,
                                        Latitude = Latitude,
                                        ConnectionType = ConnectionType,
                                        VerificationStatus = VerificationStatus
                                    });
                                }
                                con.Close();
                                return Request.CreateResponse(HttpStatusCode.OK, list);
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }
                    }
                }

                var __message = string.Format("Approval Successful");
                HttpError __err = new HttpError(__message);
                return Request.CreateResponse(HttpStatusCode.OK, __err);
            }



            var _message = string.Format("Could not update the Separation records.  Please try again Thank you");
            HttpError _err = new HttpError(_message);
            return Request.CreateResponse(HttpStatusCode.NotFound, _err);
        }

  //[System.Web.Http.HttpPost]
  //      [Route("api/PHEDConnectAPI/MyActivityActivation")]
  //      public HttpResponseMessage MyActivityActivation(RCDCModel Data)
  //      {
  //          RCDCModel d = new RCDCModel();
  //          db = new ApplicationDbContext();
  //          string message = "";
  //          if (Data == null || string.IsNullOrEmpty(Data.StaffId))
  //          {
  //              // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

  //              message = string.Format("Please select an Account Number to Proceed and Try again");
  //              HttpError _err = new HttpError(message);
  //              return Request.CreateResponse(HttpStatusCode.NotFound, _err);
  //          }
  //          else
  //          {
  //              string AccountNo = Data.AccountNo;

  //              //get the DisconID
  //              string DisconnId = ""; 
  //              string AccountType = "";

  //              var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconBy == Data.StaffId && p.DisconStatus == "ACTIVATION").ToList();

  //              if (DisconnectionList.Count > 0)
  //              {
  //                  return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
  //              }
  //              else
  //              {
  //                  List<RCDC_DisconnectionList> empty = new List<RCDC_DisconnectionList>();
  //                  return Request.CreateResponse(HttpStatusCode.OK, empty);
  //              }
  //          }

  //          HttpError err = new HttpError(message);
  //          return Request.CreateResponse(HttpStatusCode.NotFound, err);

  //      }

  [System.Web.Http.HttpPost]
  [Route("api/PHEDConnectAPI/MyActivityActivation")]
  public HttpResponseMessage MyActivityActivation(RCDCModel Data)
  {
      RCDCModel d = new RCDCModel();
      db = new ApplicationDbContext();
      string message = "";
      if (Data == null || string.IsNullOrEmpty(Data.StaffId))
      {
          // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

          message = string.Format("Please select an Account Number to Proceed and Try again");
          HttpError _err = new HttpError(message);
          return Request.CreateResponse(HttpStatusCode.NotFound, _err);
      }
      else
      {
          string AccountNo = Data.AccountNo;

          //get the DisconID
          string DisconnId = "";
          string AccountType = "";

          var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "ACTIVATION").ToList();

          if (DisconnectionList.Count > 0)
          {
              return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
          }
          else
          {
              List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
              return Request.CreateResponse(HttpStatusCode.OK, empty);
          }
      }

      HttpError err = new HttpError(message);
      return Request.CreateResponse(HttpStatusCode.NotFound, err);

  }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityReconnection")]
        public HttpResponseMessage MyActivityReconnection(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.DisconBy == Data.StaffId && p.DisconStatus == "RECONNECTED").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    message = string.Format("There are no activities you have done on Disconnection");
                    HttpError _err = new HttpError(message);

                    List<RCDC_DisconnectionList> empty = new List<RCDC_DisconnectionList>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

         
  
 
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityIreport")]
        public HttpResponseMessage MyActivityIreport(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_Ireports.Where(p => p.StaffId == Data.StaffId).ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_Ireport> empty = new List<RCDC_Ireport>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }
        
        
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityIllegalConnection")]
        public HttpResponseMessage MyActivityIllegalConnection(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "ILLEGAL CONNECTION").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

           [System.Web.Http.HttpPost]
           [Route("api/PHEDConnectAPI/MyActivityTariffChange")]
        public HttpResponseMessage MyActivityTariffChange(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "TARIFF CHANGE").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivitySeparation")]
        public HttpResponseMessage MyActivitySeparation(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "SEPARATION").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

          
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityNewCustomer")]
        public HttpResponseMessage MyActivityNewCustomer(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "NEW CUSTOMER").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

             [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/MyActivityBillDistribution")]
        public HttpResponseMessage MyActivityBillDistribution(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            string message = "";
            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError _err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, _err);
            }
            else
            {
                string AccountNo = Data.AccountNo;

                //get the DisconID
                string DisconnId = ""; 
                string AccountType = "";

                var DisconnectionList = db.RCDC_OnboardCustomerss.Where(p => p.StaffId == Data.StaffId && p.OnboardCategory == "NEW CUSTOMER").ToList();

                if (DisconnectionList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
                }
                else
                {
                    List<RCDC_OnboardCustomers> empty = new List<RCDC_OnboardCustomers>();
                    return Request.CreateResponse(HttpStatusCode.OK, empty);
                }
            }

            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);

        }

        private string getLastPaymentDate(string AccountNo)
        {
            

            DBManager dBManager = new DBManager(DataProvider.Oracle)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
            };

            dBManager.Open();
           
          

            string str = "select  max(paymentdatetime) as LastPaymentDate from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0'     order by paymentdatetime";


            dBManager.Open();
            try
            {
                DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                dBManager.Close(); dBManager.Dispose();
                if (dataSet1.Tables[0].Rows.Count <= 0)
                {
                    var message = string.Format("No Account record exists for this Account Selected ");
                    HttpError err = new HttpError(message);
                    return null;
                }
                else
                {
                    for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                    {
                        return dataSet1.Tables[0].Rows[i]["LastPaymentDate"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return null;

        }

        private bool CheckIfHeIsEligibleForReconnection(string AccountNo, string DisconnId, string AccountType)
        {

            //get the Incidences that the Guy has been biled

            var Incidence = db.RCDC_Disconnection_Incidence_Historys.Where(p => p.DisconnId == DisconnId).ToList();

            int Paid = 0;
            GlobalMethodsLib dal = new GlobalMethodsLib();


            foreach (var o in Incidence)
            {
                //Check if he has paid
                string PercentageOfPayment = o.Percentpayment;

                //go to DLEnhance and Get the Payment from the 

                DateTime DisconnectionDate = Convert.ToDateTime(o.DateDisconnected);

                if (dal.HasHePaid(o.IncidenceId, o.IncidenceAmount, o.Percentpayment, DisconnectionDate, AccountNo, AccountType))
                {
                    //Make this incidence Paid
                    
                    Paid = Paid + 1;
                }
            }

            if (Paid == Incidence.Count)
            {
                return true;
            }
            return false;
        }
         

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectionPaymentHistory")]
        public HttpResponseMessage GetReconnectionPaymentHistory(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.DisconnId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Date to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                string DateofDiscon = Convert.ToDateTime(Data.Date).ToString("MM-dd-yyyy");
                
                DataSet dataSet = new DataSet();
                DBManager dBManager = new DBManager(DataProvider.Oracle)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
                };

                dBManager.Open();
                // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");


                string AccountNo = Data.AccountNo;
                int Count = 6;
                // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";


                //get the Disconnection Date

                //DateTime Disconn = data

                string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0' and to_char(paymentdatetime,'MM-dd-yyyy')  >= to_char('" + DateofDiscon + "','MM-dd-yyyy')   and   rownum <= '" + Count + "'";

                dBManager.Open();
                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close(); dBManager.Dispose();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        var message = string.Format("No Account record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);

                    }
                    else
                    {
                        //Formulate the Customer details here before Sending

                        RCDCCustomer Customer = new RCDCCustomer();
                        List<RCDCCustomerPayments> Pay = new List<RCDCCustomerPayments>();

                        RCDCCustomerPayments _pay = new RCDCCustomerPayments();
                        // var DefaultingCustomer = db.RCDCDisconnectionLists.FirstOrDefault(p=>p.DisconID == Data.DisconnId);

                        // Customer.IncidenceHistory = db.RCDC_Disconnection_Incidence_Historys.Where(p => p.DisconnId == Data.DisconnId).ToList();
                        //  Customer.AccountNo = Data.AccountNo;
                        for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                        {
                            _pay = new RCDCCustomerPayments();
                            //Iterate through the Dataset and Set the Payment history Objects to the Model
                            _pay.AmountPaid = Convert.ToDouble(dataSet1.Tables[0].Rows[i]["Amount"].ToString());
                            _pay.DatePaid = (DateTime)dataSet1.Tables[0].Rows[i]["paymentdatetime"];
                            _pay.PaymentDescription = dataSet1.Tables[0].Rows[i]["paymentpurpose"].ToString();
                            _pay.PaymentID = dataSet1.Tables[0].Rows[i]["receiptnumber"].ToString();
                            Pay.Add(_pay);
                        }

                        Customer.PaymentHistory = Pay;



                        //get the Guy's Incidence


                        var IncidenceList = db.RCDC_Disconnection_Incidence_Historys.Where(p => p.DisconnId == Data.DisconnId).ToList();


                        Customer.IncidenceHistory = IncidenceList;

                        return Request.CreateResponse(HttpStatusCode.OK, Customer);
                    }

                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dBManager.Close(); dBManager.Dispose();
                    var message = string.Format("Could not retrieve Customer records because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

            }
        }

         
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectionList")]
        public HttpResponseMessage GetReconnectionList(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.Zone))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Zone was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.FeederId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("The Feeder was not Selected. Please select and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederId == Data.FeederId && p.DisconStatus == "RECONNECT").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectedListByStaffID")]
        public HttpResponseMessage GetReconnectedListByStaffID(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.DisconBy == Data.StaffId && p.DisconStatus == "RECONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }


        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectedListByGang")]
        public HttpResponseMessage GetReconnectedListByGang(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.GangID))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.Gang_ID == Data.GangID && p.DisconStatus == "RECONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectedListByFeeder")]
        public HttpResponseMessage GetReconnectedListByFeeder(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.Feeder))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Staff ID to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Year))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Year and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.Month))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select a Month and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.Year == Year && p.Month == Month && p.DisconStatus == "RECONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectedListByDate")]
        public HttpResponseMessage GetReconnectedListByDate(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Disconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.DateOfDiscon == DateofDiscon && p.DisconStatus == "RECONNECTED").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetReconnectedAccountByDate")]
        public HttpResponseMessage GetReconnectedAccountByDate(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();



            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Reconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.DateOfDiscon == DateofDiscon && p.AccountNo == Data.AccountNo && p.DisconStatus == "RECONNECT").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/ReconnectCustomerOLD")]
        public HttpResponseMessage ReconnectCustomerOLD(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            
            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Reconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                var DisconnectionList = db.RCDCDisconnectionLists.Where(p => p.Zone == Data.Zone && p.FeederName == Data.Feeder && p.DateOfDiscon == DateofDiscon && p.AccountNo == Data.AccountNo && p.DisconStatus == "RECONNECT").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, DisconnectionList);
            }
        }

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/ReconnectCustomer")]
        public HttpResponseMessage ReconnectCustomer(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here

            //try

            if (string.IsNullOrEmpty(Data.DisconnId))
            {
                
                var message = string.Format("The Disconnection Id is Missing.Kindly Supply it as *DisconnId* and try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            RCDC_DisconnectionList Discon = db.RCDCDisconnectionLists.FirstOrDefault(p => p.DisconID == Data.DisconnId);

            if (Discon == null)
            {
                var message = string.Format("The Disconnection Id is wrong. No record was Found. Please cross check and try again. Thank you.");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NoContent, err);
            }
            else
            {
                Discon.DisconStatus = "RECONNECTED";
                Discon.ReconnectedBy = Data.StaffId;
                Discon.DateReconnected = DateTime.Now;

                db.Entry(Discon).State = EntityState.Modified;
                db.SaveChanges();
            }

            //AUDIT TRAIL
            Audit.AuditTrail(Data.UserId, AccountNo + " was Reconnected on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "RECONNECTION");

            RCDCModel Success = new RCDCModel();
            Success.Message = "The Customer with Account Number " + AccountNo + "  has been Reconnection Successfully";
            Success.Status = "SUCCESS";
            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }


        #endregion


        #region Payment Records
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetPaymentHistoryByDate")]
        public HttpResponseMessage GetPaymentHistoryByDate(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();


            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                var message = string.Format("Please input the Payment Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                DataSet dataSet = new DataSet();
                DBManager dBManager = new DBManager(DataProvider.Oracle)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
                };
                dBManager.Open();
                // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");


                string AccountNo = Data.AccountNo;
                int Count = 6;
                // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";

                //ooooooooooooooooooooooooooooooooooooooooooooooooo
                string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' and      paymentdatetime >= '" + DateofDiscon + "' order by paymentdatetime desc";

                dBManager.Open();
                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close(); dBManager.Dispose();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        var message = string.Format("No Payment record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);

                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(dataSet1.Tables[0]));
                    }

                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dBManager.Close(); dBManager.Dispose();
                    var message = string.Format("Could not retrieve Payment history because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

            }
        }



        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/GetPaymentHistoryByAccountNoAndCount")]
        public HttpResponseMessage GetPaymentHistoryByAccountNoAndCount(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();

            if (Data == null || string.IsNullOrEmpty(Data.Date))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please input the Disconnection Date to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }



            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);

                var message = string.Format("Please select an Account Number to Proceed and Try again");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                //convert the Date to DateTime and Get Year

                string Year = DateTime.Now.Year.ToString();
                string Month = DateTime.Now.Month.ToString();

                DateTime DateofDiscon = Convert.ToDateTime(Data.Date);

                DataSet dataSet = new DataSet();
                DBManager dBManager = new DBManager(DataProvider.Oracle)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
                };
                dBManager.Open();
                // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");


                string AccountNo = Data.AccountNo;
                int Count = 6;
                // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";


                string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment  where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";

                dBManager.Open();
                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close(); dBManager.Dispose();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        var message = string.Format("No Payment record exists for this Account Selected ");
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotFound, err);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(dataSet1.Tables[0]));
                    }
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dBManager.Close(); dBManager.Dispose();
                    var message = string.Format("Could not retrieve Payment history because " + exception1.Message + ". Please try again Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
        }


        #endregion


        #region Bill Distribution

        #endregion


        #region Energy Theft

        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/IreportFORMDATA")]
        public HttpResponseMessage IreportFORMDATA()
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            //Emmanuel Method Here

            //try
            //{
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files;

                var Zone = System.Web.HttpContext.Current.Request.Params["Zone"];
                var Feeder = System.Web.HttpContext.Current.Request.Params["Feeder_Id"];
                var ReportCategory = System.Web.HttpContext.Current.Request.Params["ReportCategory"];
                var ReportSubCategory = System.Web.HttpContext.Current.Request.Params["ReportSubCategory"];

                var StaffId = System.Web.HttpContext.Current.Request.Params["StaffId"];
                var UserId = System.Web.HttpContext.Current.Request.Params["UserId"];
                AccountNo = System.Web.HttpContext.Current.Request.Params["AccountNo"];
                var AccountName = System.Web.HttpContext.Current.Request.Params["AccountName"];
                var PhoneNumber = System.Web.HttpContext.Current.Request.Params["PhoneNumber"];
                var CustomerEmail = System.Web.HttpContext.Current.Request.Params["CustomerEmail"];
                var Address = System.Web.HttpContext.Current.Request.Params["Address"];
                var DTR_Id = System.Web.HttpContext.Current.Request.Params["DTR_Id"];
                var Comments = System.Web.HttpContext.Current.Request.Params["Comments"];


                if (string.IsNullOrEmpty(StaffId))
                {
                    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                    var message = string.Format("Please input the Staff ID as *StaffId* to Proceed. Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

                if (string.IsNullOrEmpty(AccountNo))
                {
                    // var message = string.Format("The Zone or the Feeder was not Selected with id = {0} not found", id,32);
                    var message = string.Format("Please supply the Account Number as *AccountNo* of the Customer to Proceed. Thank you");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }

                //pass the Names of the Fields from Emmanuel Here in the Form Data

                //var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), DocumentName);


                for (int i = 0; i < httpPostedFile.Count; i++)
                {
                    HttpPostedFile file = httpPostedFile[i];
                    var Assignments = System.Web.HttpContext.Current.Request.Form[i].ToString();

                    string FN = Guid.NewGuid().ToString();

                    string Description = System.Web.HttpContext.Current.Request.Params["DocumentDescription"];
                    string Size = System.Web.HttpContext.Current.Request.Params["DocumentSize"];
                    string Extension = System.Web.HttpContext.Current.Request.Params["DocumentExtension"];

                    // string     DocPath = "/Documents/" + FN + "_" + file.FileName;
                    //Docx.UploadDocument(ClassCode, ArmCode, "", "", SchoolCode, SubjectCode, FileName, Description_, "", key, CreatedBy, "ASSIGNMENT", "", Extension_, Size_, "", DocPath);
                    //  file.SaveAs(path + file.FileName);
                    var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents"), FN + "_" + file.FileName);
                    file.SaveAs(fileSavePath);
                }
                
                //Insert the Incidences
                RCDC_Ireport hg = new RCDC_Ireport();

                hg.Zone = Zone;
                hg.Feeder_Id = Feeder;
                hg.AccountName = AccountName;
                hg.Address = Address;
                hg.DTR_Id = DTR_Id;
                hg.Comments = Comments;
                hg.StaffId = StaffId;
                hg.ReportCategory = ReportCategory;
                hg.ReportSubCategory = ReportSubCategory;
                hg.PhoneNumber = PhoneNumber;
                hg.CustomerEmail = CustomerEmail;
                hg.DateReported = DateTime.Now;

                db.RCDC_Ireports.Add(hg);
                db.SaveChanges();

                //AUDIT TRAIL
                Audit.AuditTrail(UserId, AccountNo + " was Reported on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "IREPORT");

            }



            RCDCModel Success = new RCDCModel();

            Success.Message = "The Customer with Account Number " + AccountNo + "  has been reported Successfully";
            Success.Status = "SUCCESS";

            return Request.CreateResponse(HttpStatusCode.OK, Success);
        }

       
        [System.Web.Http.HttpPost]
        [Route("api/PHEDConnectAPI/Ireport")]
        public HttpResponseMessage Ireport(RCDCModel Data)
        {
            RCDCModel d = new RCDCModel();
            db = new ApplicationDbContext();
            GlobalMethodsLib Audit = new GlobalMethodsLib();
            string AccountNo = "";

            if (Data == null || string.IsNullOrEmpty(Data.AccountNo))
            {
                var message = string.Format("Please input the AccountNo  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.StaffId))
            {
                var message = string.Format("Please input the StaffId  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //if (Data == null || string.IsNullOrEmpty(Data.FeederId))
            //{
            //    var message = string.Format("Please input the FeederId  to Proceed. Thank you");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            //if (Data == null || string.IsNullOrEmpty(Data.AccountName))
            //{
            //    var message = string.Format("Please input the Account Name  to Proceed. Thank you");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            if (Data == null || string.IsNullOrEmpty(Data.Address))
            {
                var message = string.Format("Please input the Address    to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            //if (Data == null || string.IsNullOrEmpty(Data.DTR_Id))
            //{
            //    var message = string.Format("Please input the DTR Id    to Proceed. Thank you");
            //    HttpError err = new HttpError(message);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, err);
            //}

            if (Data == null || string.IsNullOrEmpty(Data.Comments))
            {
                var message = string.Format("Please input the Comments  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReportCategory))
            {
                var message = string.Format("Please input the ReportCategory  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.ReportSubCategory))
            {
                var message = string.Format("Please input the ReportCategory  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.IreportersPhoneNo))
            {
                var message = string.Format("Please input the Ireporter's PhoneNo  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            if (Data == null || string.IsNullOrEmpty(Data.IreportersEmail))
            {
                var message = string.Format("Please input the Ireporter's PhoneNo  to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }

            if (Data == null || string.IsNullOrEmpty(Data.UserId))
            {
                var message = string.Format("Please input the User Id to Proceed. Thank you");
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            try
            {
                RCDC_Ireport hg = new RCDC_Ireport();

                hg.Zone = Data.Zone;
                hg.CustomerName = Data.CustomerName;
                hg.IreportersPhone = Data.IreportersPhoneNo;
                hg.IreportersEmail = Data.IreportersEmail; 
                hg.Status = Data.Status;
                hg.Feeder_Id = Data.FeederId;
                hg.AccountName = Data.AccountName;
                hg.Address = Data.Address;
                hg.DTR_Id = Data.DTR_Id;
                hg.Comments = Data.Comments;
                hg.StaffId = Data.StaffId;
                hg.ReportCategory = Data.ReportCategory;
                hg.ReportCategoryName =  Data.ReportCategoryName;
                hg.ReportSubCategory = Data.ReportSubCategory;

                var g = db.RCDC_ReportCategorys.FirstOrDefault(p => p.ReportCategoryId == hg.ReportCategory);

                if (g != null)
                {

                    hg.ReportSubCategoryName = g.ReportCategoryName;

                }


                var h = db.RCDC_ReportSubCategorys.FirstOrDefault(p => p.ReportSubCategoryId == hg.ReportSubCategory);
                
                if (h != null)
                {
                    hg.ReportSubCategoryName = h.ReportSubCategoryName;
                }
                 
                hg.PhoneNumber = Data.PhoneNo;
                hg.CustomerEmail = Data.Email;
                hg.AccountNo = Data.AccountNo;
                hg.DateReported = DateTime.Now;
                hg.Latitude = Data.Latitude;
                hg.Longitude = Data.Longitude;
                hg.UserId = Data.UserId;
                hg.filePaths = Data.filePaths;
                db.RCDC_Ireports.Add(hg);
                db.SaveChanges();

                Audit.AuditTrail(Data.UserId, AccountNo + " was Reported on " + DateTime.Now, DateTime.Now, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "IREPORT");


                RCDCModel Success = new RCDCModel();

                Success.Message = "The Customer with Account Number " + AccountNo + "  has been reported";
                Success.Status = "SUCCESS";

                return Request.CreateResponse(HttpStatusCode.OK, Success);


            }
            catch (Exception ex)
            {
                RCDCModel Success = new RCDCModel();

                Success.Message = "An Error Occured because" + ex.Message ;
                Success.Status = "ERROR";

                return Request.CreateResponse(HttpStatusCode.NotImplemented, Success);

            }
            //AUDIT TRAIL
             
    }
         
        #endregion
         
        public string RPDLossOfRevenueAmount { get; set; }

        public int? RPDLossOfRevenueInfractionDuration { get; set; }
    }

     
}