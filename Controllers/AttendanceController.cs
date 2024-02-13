using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PHEDServe.Models;
using System.Data;
using System.IO;
using System.Reflection;

namespace PHEDServe.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AttendanceController : ApiController
    {
        // Define the connection string
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["AttendanceConnection"].ConnectionString;
        // GET: Attendance
        // POST api/kess
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PHEDConnectAPI/AddAttendence")]
        public IHttpActionResult Post(attandence att)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    LogWrite("input:"+att.StaffId.ToString() + ":" + att.LogTime + ":" + att.CapturedImagePath + ":" + att.Latitude + ":" + att.Longitude);
                    conn.Open();

                    // Create and configure the SqlCommand for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_InsertAttendence", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        //int rowsAffected1=0;
                        cmd.Parameters.AddWithValue("@StaffId", att.StaffId);
                        cmd.Parameters.AddWithValue("@LogTime", att.LogTime);
                        cmd.Parameters.AddWithValue("@CapturedImagePath", att.CapturedImagePath);
                        cmd.Parameters.AddWithValue("@Latitude", att.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", att.Longitude);
                        // Execute the stored procedure
                        //int rowsAffected = cmd.ExecuteNonQuery();
                        var rowsAffected = (int)cmd.ExecuteScalar();

                        LogWrite("data inseted or not:" + rowsAffected.ToString());
                        if (rowsAffected > 0)
                        {
                            // Success message
                            var response = new
                            {
                                status = "SUCCESSFUL",
                                msg = "New Attendence item added successfully"
                            };

                            return Ok(response);
                        }
                        else
                        {
                            LogWrite("expection:Return 400 Bad Request if no rows were affected");
                            // Return 400 Bad Request if no rows were affected
                            return BadRequest();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var response = new
                    {
                        status = "FAILED",
                        msg = "New Attendence item addition failed"
                    };
                    if(ex.Message.ToString().Contains("The duplicate key"))
                    {
                        var responseDuplicate = new
                        {
                            status = "SUCCESSFUL",
                            msg = "New Attendence item added successfully"
                        };

                        return Ok(responseDuplicate);
                    }
                    LogWrite("expection:"+ex.StackTrace.ToString()+":"+ex.Message.ToString());
                    return StatusCode(System.Net.HttpStatusCode.Forbidden);

                }
            }
        }
        private string m_exePath = string.Empty;
        
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(@"E:\SmartworkforceAPITest\bin");
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}