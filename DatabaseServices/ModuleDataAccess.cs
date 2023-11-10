//using System.Data.SqlClient;
//using System.Data;
//using School_Login_SignUp.Models;

//namespace School_Login_SignUp.DatabaseServices
//{
//    public 

//    public async Task<int> AddSelectedModulesAsync(string schoolCode, List<int> moduleIds)
//    {
//        int rowsAffected = 0;

//        using (SqlConnection connection = new SqlConnection(ConnectionStrings))
//        {
//            await connection.OpenAsync();

//            using (SqlCommand command = new SqlCommand("sp_AddSelectedModules", connection))
//            {
//                command.CommandType = CommandType.StoredProcedure;

//                DataTable moduleIdsTable = new DataTable();
//                moduleIdsTable.Columns.Add("ModuleId", typeof(int));

//                foreach (int moduleId in moduleIds)
//                {
//                    moduleIdsTable.Rows.Add(moduleId);
//                }

//                SqlParameter parameter = new SqlParameter
//                {
//                    ParameterName = "@ModuleIds",
//                    SqlDbType = SqlDbType.Structured,
//                    TypeName = "dbo.ModuleIdList", // Create a user-defined table type in the database for this
//                    Value = moduleIdsTable
//                };

//                command.Parameters.Add(parameter);
//                command.Parameters.AddWithValue("@SchoolCode", schoolCode);

//                rowsAffected = await command.ExecuteNonQueryAsync();
//            }
//        }
//        return rowsAffected;
//    }





//    public async Task<List<Module>> GetAllModulesAsync()
//    {
//        List<Module> modules = new List<Module>();

//        using (SqlConnection connection = new SqlConnection(ConnectionStrings))
//        {
//            await connection.OpenAsync();

//            using (SqlCommand command = new SqlCommand("sp_GetAllModules", connection))
//            {
//                command.CommandType = CommandType.StoredProcedure;

//                using (SqlDataReader reader = await command.ExecuteReaderAsync())
//                {
//                    while (await reader.ReadAsync())
//                    {
//                        Module module = new Module
//                        {
//                            ModuleId = Convert.ToInt32(reader["ModuleId"]),
//                            ModuleName = reader["ModuleName"].ToString(),
//                            ModuleAmount = Convert.ToDecimal(reader["ModuleAmount"])
//                        };

//                        modules.Add(module);
//                    }
//                }
//            }
//        }

//        return modules;
//    }
//}