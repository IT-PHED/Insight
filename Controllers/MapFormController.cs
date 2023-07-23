using PHEDServe.Models;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using System.Configuration;
using System.Web.Mvc.Html;


namespace PHEDServe.Controllers
{
    public class MapFormController : ApiController

    {
        private readonly string connectionString;
        public MapFormController()
        {
            connectionString = "Data Source=172.30.52.89;Initial Catalog=RCDC_App;User ID=RCDC_user;Password=3k3n3@321#com5";

            //ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        [HttpPost]
        [Route("api/map/preinstallationform")]
        public IHttpActionResult AddFormInstallation([FromBody] MapForm formData)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Create SqlCommand for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("sp_InsertFormInstallation", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@ApplicationID", formData.ApplicationID);
                        cmd.Parameters.AddWithValue("@Surname", formData.Surname);
                        cmd.Parameters.AddWithValue("@OtherNames", formData.OtherNames);
                        cmd.Parameters.AddWithValue("@HouseNo", formData.HouseNo);
                        cmd.Parameters.AddWithValue("@Address", formData.Address);
                        cmd.Parameters.AddWithValue("@PhoneNo", formData.PhoneNo);
                        cmd.Parameters.AddWithValue("@Email", formData.Email);
                        cmd.Parameters.AddWithValue("@BusStop", formData.BusStop);
                        cmd.Parameters.AddWithValue("@LandMark", formData.LandMark);
                        cmd.Parameters.AddWithValue("@State", formData.State);
                        cmd.Parameters.AddWithValue("@LGA", formData.LGA);
                        cmd.Parameters.AddWithValue("@OtherPremise", formData.OtherPremise);
                        cmd.Parameters.AddWithValue("@UseOfPremise", formData.UseOfPremise);
                        // Add other parameters as needed

                        // Execute the stored procedure
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Success message
                            var response = new
                            {
                                status = "SUCCESSFUL",
                                msg = "New Form Installation added successfully"
                            };

                            return Ok(response);
                        }
                        else
                        {
                            // Return 400 Bad Request if no rows were affected
                            return BadRequest("Form Installation failed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Return 403 Forbidden on connection failure or other exceptions
                    return StatusCode(System.Net.HttpStatusCode.Forbidden);
                }
            }
        }

    }

}




