using School_Login_SignUp.Models;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;

using System.Data;

using Razorpay.Api;
using School_Login_SignUp.Models;

namespace School_Login_SignUp.Controllers
{
     [Route("api/[controller]")]

    [ApiController]

    public class PaymentLinkController : ControllerBase

    {

        private readonly IConfiguration _configuration;


        public PaymentLinkController(IConfiguration configuration)

        {

            _configuration = configuration;

        }

        [Route("MakePayment")]

        [HttpPost]

        public async Task<IActionResult> MakePayment(SchoolPayment paymentData)

        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings"));

            try

            {

                string instituteCode = paymentData.InstituteCode;

                string instituteName = paymentData.InstituteName;

                decimal amountToPay = paymentData.AmountToPay;


                con.Open();

                string insertSql = "INSERT INTO Payment (InstitutionName, SchoolCode, Amount) VALUES (@InstitutionName, @SchoolCode, @Amount);";

                using (SqlCommand command = new SqlCommand(insertSql, con))

                {

                    command.Parameters.AddWithValue("@InstitutionName", instituteName);

                    command.Parameters.AddWithValue("@SchoolCode", instituteCode);

                    command.Parameters.AddWithValue("@Amount", amountToPay);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)

                    {

                        return Ok("Data inserted successfully.");

                    }

                    else

                    {

                        return BadRequest("Failed to insert data.");

                    }

                }

            }



            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

            }

        }



    }

}
