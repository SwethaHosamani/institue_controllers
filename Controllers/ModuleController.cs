using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Login_SignUp.Models;
using System.Data;
using System.Data.SqlClient;
namespace School_Login_SignUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {

        private InstitutionDataAccess moduleDataAccess;

        public ModuleController(InstitutionDataAccess moduleDataAccess)
        {
            this.moduleDataAccess = moduleDataAccess;
        }

        // GET api/modules
        [HttpGet]
        [Route("api/modules")]
        public async Task<IActionResult> GetAllModules()
        {
            List<Module> modules = await moduleDataAccess.GetAllModulesAsync();
            return Ok(modules);
        }

        // POST api/modules
        [HttpPost]
        [Route("api/modules")]
        public async Task<IActionResult> AddSelectedModules([FromBody] List<int> moduleIds, string schoolCode)
        {
            if (moduleIds != null && moduleIds.Count > 0 && !string.IsNullOrEmpty(schoolCode))
            {
                int rowsAffected = await moduleDataAccess.AddSelectedModulesAsync(schoolCode, moduleIds);

                if (rowsAffected > 0)
                {
                    return Ok("Selected modules added successfully");
                }
                else
                {
                    return BadRequest(new System.Exception("Failed to add selected modules"));
                }
            }
            else
            {
                return BadRequest("Invalid moduleIds or schoolCode");
            }
        }

        public async Task<List<Module>> GetAllModulesAsync()
        {
            List<Module> modules = new List<Module>();

            using (SqlConnection connection = new SqlConnection("Server = DESKTOP - RDT234N\\SQLEXPRESS; Database = Institutions; Trusted_Connection = True; Integrated Security = True; TrustServerCertificate = True; "))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("sp_GetAllModules", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Module module = new Module
                            {
                                ModuleId = Convert.ToInt32(reader["ModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                ModuleAmount = Convert.ToDecimal(reader["ModuleAmount"])
                            };

                            modules.Add(module);
                        }
                    }
                }
            }

            return modules;
        }
    }


}
