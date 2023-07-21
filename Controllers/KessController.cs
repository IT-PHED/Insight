using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Configuration;

using System.Data;

using PHEDServe.Models;

namespace PHEDServe.Controllers
{


    public class KessController : ApiController
    {
        // Define the connection string
        private readonly string connectionString =  ConfigurationManager.ConnectionStrings["EnumsConnection"].ConnectionString;

        // POST api/kess
        [HttpPost]
        [Route("api/kess")]
        public IHttpActionResult Post(KessModel kess)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Create and configure the SqlCommand for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_InsertKESS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@title", kess.Title);
                        cmd.Parameters.AddWithValue("@titleKey", kess.TitleKey);
                        cmd.Parameters.AddWithValue("@count", kess.Count);

                        // Execute the stored procedure
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Success message
                            var response = new
                            {
                                status = "SUCCESSFUL",
                                msg = "New KESS item added successfully"
                            };

                            return Ok(response);
                        }
                        else
                        {
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
                        msg = "New KESS item addition failed"
                    };

                    return StatusCode(System.Net.HttpStatusCode.Forbidden);

                }
            }
        }



        [HttpGet]
        public IHttpActionResult GetKessData()
        {
            List<KessModel> arr = new List<KessModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Create SqlCommand for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_GetKESS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Execute the stored procedure and retrieve the data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add each row to the list
                                KessModel obj = new KessModel();
                                {
                                    // Replace column names with appropriate names from your KESS table
                                    // Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    //Title = reader.GetString(reader.GetOrdinal("Title")),
                                    //   obj.Title = reader.GetInt32(reader.GetOrdinal("Title")),
                                    // Add other properties as needed
                                };

                                arr.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(System.Net.HttpStatusCode.BadRequest);
                }
            }

            // Return the JSON response
            return Ok(arr);
        }



    }


}