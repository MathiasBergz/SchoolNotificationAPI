using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SchoolNotificationAPI.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(
                _configuration.GetConnectionString(
                    "DefaultConnection"));
        }
    }
}
