using System.Data.SqlClient;
using System.Data;
using Dapper;
 
namespace RegistrationAPI.Data
{
    public class SuperAdminDataAccess
    {
        private readonly string _connectionString;
 
        public SuperAdminDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CS");
        }
 
        public async Task<(SuperAdmin? userCredentials, int statusCode)> GetUserAsync(string userID, string password)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);
                var parameters = new { UserEmail = userID, UserPassword = password }; // Corrected parameter name
                var result = await db.QueryFirstOrDefaultAsync<SuperAdmin>(
                    "sp_CheckCredentials",
                    parameters,
                    commandType: CommandType.StoredProcedure);
 
                if (result != null)
                {
                    return (result, 200); // 200 OK
                }
                else
                {
                    return (null, 401); // 401 Unauthorized
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);
                return (null, 500); // 500 Internal Server Error
            }
        }
 
        public async Task<(int userCredentials, int statusCode)> PostAdminDashboard(Admin_Dashboard admin)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);
                var parameters = new { admin.Email, admin.Password , admin.Role}; // Corrected parameter name
                var result = await db.ExecuteAsync(
                    "sp_AdminDashboardLogin",
                    parameters,
                    commandType: CommandType.StoredProcedure);
 
                if (result != null)
                {
                    return (result, 200); // 200 OK
                }
                else
                {
                    return (0, 401); // 401 Unauthorized
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);
                return (0, 500); // 500 Internal Server Error
            }
        }
 
    }
}