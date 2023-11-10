using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Data;
using System.Threading.Tasks;
using INSTITUE_CONTROLLERS.Models;
 
[ApiController]
[Route("api/[controller]")]
public class SuperAdminController : ControllerBase
{
    private readonly SuperAdminDataAccess _superAdminService;
 
    public SuperAdminController(SuperAdminDataAccess superAdminService)
    {
        _superAdminService = superAdminService;
    }
 
    [HttpPost("login")]
    public async Task<IActionResult> SuperAdminLogin([FromBody] SuperAdmin model)
    {
        try
        {
            var (userCredentials, statusCode) = await _superAdminService.GetUserAsync(model.UserEmail, model.Password);
 
            if (userCredentials != null)
            {
                return Ok(new { Message = "Login successful" });
            }
            else
            {
                return StatusCode(statusCode, new { Message = "Invalid credentials" });
            }
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { Message = "Internal Server Error" });
        }
    }
 
    [HttpPost("Admin_Dashboard")]
    public async Task<IActionResult> Admin_Dashboard_Login([FromBody] Admin_Dashboard model)
    {
        try
        {
            var (rowsAffected, statusCode) = await _superAdminService.PostAdminDashboard(model);
 
            if (rowsAffected > 0)
            {
                return Ok(new { Message = "Login successful" });
            }
            else
            {
                return StatusCode(statusCode, new { Message = "Invalid credentials" });
            }
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { Message = "Internal Server Error" });
        }
    }
 
 
}