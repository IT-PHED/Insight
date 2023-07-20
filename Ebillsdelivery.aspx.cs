using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHEDServe
{
    public partial class Ebillsdelivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=phedmis.com;Initial Catalog=PHEDCMS;Integrated Security=false;User ID=ebuka;Password=ebukastaffpayment";
            //SQL to get all the Accounts for the MD Customers

            DataSet ds = new DataSet();
            string CheckUsername = "";
            CheckUsername = "select AccountNo, Name, GSM, Email, RowNum from MDBillsAlert where Email is not null and RowNum BETWEEN 1 and 3000";// +1 + " AND " + 3000;
            


            SqlDataAdapter dataAdapt1 = new SqlDataAdapter(CheckUsername, connectionString);
            dataAdapt1.Fill(ds);
              
            string AccountName = "";

            GridView1.DataSource = ds;
            GridView1.DataBind();

             
        }
    }
}