using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

 

namespace accountseperations.Controllers
{
    public class AccountSeperationController : ApiController
    {
        private OracleConnection conn = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DLDataAccess2"].ConnectionString);
        private SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["NepamsOnlineConnection"].ConnectionString);


        // GET: api/AccountSeperation
        [HttpGet]
        public IHttpActionResult DoaccountSeperation(string requestbyname, string requestbyid,string primaryaccount, int noofseparation,string requestdate)
        {
            List<string> list = new List<string>();
            List<string> subactlist = new List<string>();
           
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
           conn.Open();
            //conn.ConnectionTimeout = 900;
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_MAP_GET_ALLSEPARATED_ACCOUNTS"
            };
            cmd.CommandTimeout = 900;
            cmd.Parameters.Add("in_accountno", OracleDbType.Varchar2, ParameterDirection.Input).Value = primaryaccount;
            //cmd.Parameters.Add(new OracleParameter("in_accountno", OracleDbType.Varchar2, ParameterDirection.Input));
            // cmd.Parameters.Add(new OracleParameter("p_MobileNo", OracleDbType.Varchar2, ParameterDirection.Input));
              cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            using (OracleDataReader rdr = cmd.ExecuteReader())
            {
                //responsedatakyc.Satatus = rdr.GetString(0);
                if (rdr.HasRows)
                {
                    if (InsertParentAccountToSQL( requestbyname,  requestbyid,  primaryaccount,  noofseparation,requestdate) > 0){ 
                    while (rdr.Read())
                    {
                        String acctno = rdr["CONS_ACC"].ToString();
                        if (acctno.Length>12)
                        {
                            list.Add(acctno.Substring(acctno.Length-1));
                        }
                    }
                    String[] str = list.ToArray();
                    IEnumerable<string> alphas = from planet in alphabet.Except(str)
                                                 select planet;
                    string[] alphastoarray = alphas.Cast<string>().ToArray();

                    //conn.Close();
                    for (int i = 0; i < noofseparation; i++)
                    {
                        if (AddsubacctToSQL(primaryaccount + alphastoarray[i], primaryaccount) > 0)
                        {
                            subactlist.Add(primaryaccount + alphastoarray[i]);

                        }
                    }
                    }
                    else
                    {
                        subactlist.Add("error inserting");
                    }
                }
                else
                {
                    subactlist.Add("Invalid Account");
                }
                
                return Ok(new { V = subactlist });

            }

           
        }

        private int InsertParentAccountToSQL(string requestbyname, string requestbyid, string primaryaccount, int noofseparation,string requestdate)
        {
            string query = "insert into [ENHANCE].[ebuka].[tbl_map_accountsseparation] (noofseparation,requestdate,primaryaccount,requestbyid,requestbyname)"+
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
                try { 

                    int result = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return result;
            }catch (Exception e)
            {

                   Console.WriteLine(e.StackTrace);
                        sqlcon.Close();
                return 0;
            }


        }
        }

        private int AddsubacctToSQL(string subaccount,string parentsaccount) { 
            string query = "INSERT INTO [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] (primaryaccounts, secondaryaccount) VALUES (@primaryaccounts,@secondaryaccount)";
            //using (SqlConnection sqlconn = new SqlConnection(sqlconnstring))

            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                //a shorter syntax to adding parameters
                command.Parameters.Add("@primaryaccounts", SqlDbType.VarChar).Value = parentsaccount;
                command.Parameters.Add("@secondaryaccount", SqlDbType.VarChar).Value = subaccount;


                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    int res  = command.ExecuteNonQuery();
                    sqlcon.Close();
                    return res ;
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    return 0;
                }
            }
        }

        // GET: api/AccountSeperation/5
        public string GET(int id)
        {
            return "value";
        }
        [HttpGet]
        public IHttpActionResult getAllPrimaryAccountsPendingSubAccountsforApproval(string primaryaccount,string m)
        {
            var list = new List<AllSubAcct>();
            string query = " select * from [ENHANCE].[ebuka].[tbl_map_accountseparation_secoderyaccounts] where primaryaccounts=@primaryaccount ";
            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = primaryaccount;
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
                            list.Add(new AllSubAcct { sn = sn, primaryact = primaryaccounto,secact=seconderyaccount0 });
                        }
                    }
                    sqlcon.Close();
                    return Ok(new { V = list });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    var siteID = 0;
                    var siteName ="Error Occured";
                    list.Add(new AllSubAcct { sn = siteID, primaryact = siteName });
                    return Ok(new { V = list });
                }
            }
   
        }
        [HttpGet]
        public IHttpActionResult getAllPendingPrimaryAccounts(string staffid,string feederid)
        {
            var list = new List<AllPrimaryAccounts>();
            string query = "select sn, primaryaccount, noofseparation-subaccountscreated as pending, requestdate,requestbyname from [tbl_map_accountsseparation]" +
                "where primaryaccount in(SELECT accountno from [RCDC_User].[HierarchyEnumsData33415_11415_temp4Updated] t1 join "+
                 "[ENHANCE].[RCDC_User].[HierarchyEnumsData33415_11415Dtr_temp] t2 on t1.DTRID = t2.DTRID where feedermgr33id = @staffid or feedermgr11id = @staffid) and checked is null ";
            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                command.Parameters.Add("@staffid", SqlDbType.VarChar).Value = staffid;
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
                            list.Add(new AllPrimaryAccounts {sn=sn,  primaryaccount = primaryaccounto, pending = pending, requestdate= requestdate, requestbyname= requestbyname });
                        }
                        sqlcon.Close();
                        return Ok(new { V = list });
                    }
                    else
                    {
                        var sn = "0";
                        var siteName = "No Record";
                        list.Add(new AllPrimaryAccounts { sn = sn, primaryaccount = siteName });
                        return Ok(new { V = list });
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    sqlcon.Close();
                    var siteID = "0";
                    var siteName = "Error Occured";
                    list.Add(new AllPrimaryAccounts { sn = siteID, primaryaccount = siteName });
                    return Ok(new { V = list });
                }
            }
        }


        // POST: api/AccountSeperation
        public IHttpActionResult Post(string secaccts,string primaryacctno,string staffid)
        {
            List<string> list = new List<string>();
            List<Category> categoryRepo = new List<Category>();
            string s="";
            String[] secacctRepo = secaccts.Split(',');

            foreach (var secact in secacctRepo)
            {
                /*categoryRepo.Add(new Category()
                {
                    secaccountno = secact,
                    CategoryName = String.Format("Category_{0}", secact)
                   
                }) ;*/
                int i = 0;
                 s = secact;
                bool isNumeric = int.TryParse(s.Substring(s.Length - 1), out i);
                Console.WriteLine(isNumeric+" "+s);
                if (isNumeric) {
                    s += s + ",";
                   
                }
               
            }
            int output = AddseconderyaccountstoDlEnhance(secaccts, primaryacctno, "PHEDConnect-" + staffid);
            if (output > 0)
            {
                
                if(Updateprimaryactsforseperation(primaryacctno) > 0)
                {
                    var suc = true;
                    categoryRepo.Add(new Category { success = suc });
                    return Ok(new { V = categoryRepo });
                }
                else
                {
                    var suc = false;
                    var msg = "Primary accounts could not be updated";
                    categoryRepo.Add(new Category { success = suc,message=msg });
                    return Ok(new { V = categoryRepo });
                }
                
            }
            else
            {
                var suc = false;
                var msg = "Secondery accounts could not be added to DLENhance";
                categoryRepo.Add(new Category { success = suc, message = msg });
                return Ok(new { V = categoryRepo });
            }
            
        }

        private int Updateprimaryactsforseperation(string primaryacctno)
        {
            string query = "update [tbl_map_accountsseparation] set checked=1 where primaryaccount=@primaryaccount";
            using (SqlCommand command = new SqlCommand(query, sqlcon))
            {
                command.Parameters.Add("@primaryaccount", SqlDbType.VarChar).Value = primaryacctno;
                //make sure you open and close(after executing) the connection
                sqlcon.Open();
                try
                {
                    int res = command.ExecuteNonQuery();
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

        private int AddseconderyaccountstoDlEnhance(string secact, string primaryacctno,string staffid)
        {
            conn.Open();
            //conn.ConnectionTimeout = 900;
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_MAP_INSERT_SEPARATEDACCOUNTS"
            };
            cmd.CommandTimeout = 900;
            cmd.Parameters.Add("p_PRIACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = primaryacctno;
            cmd.Parameters.Add("p_SEPACCOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value=secact;
            cmd.Parameters.Add("p_createdby", OracleDbType.Varchar2, ParameterDirection.Input).Value = staffid;
            //cmd.Parameters.Add(new OracleParameter("c_select", OracleDbType.RefCursor, ParameterDirection.Output));
            int res = cmd.ExecuteNonQuery();
            conn.Close();
            return res;
            
            }


        // PUT: api/AccountSeperation/5
        /*public void Put(int id, [FromBody]string value)
        {
        }*/

        // DELETE: api/AccountSeperation/5
        public void Delete(int id)
        {
        }
    }
}
