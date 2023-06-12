using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WorkManagment.Infraestructure.Data
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AppConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
