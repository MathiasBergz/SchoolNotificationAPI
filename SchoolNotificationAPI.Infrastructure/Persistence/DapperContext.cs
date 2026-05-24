using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace SchoolNotificationAPI.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
