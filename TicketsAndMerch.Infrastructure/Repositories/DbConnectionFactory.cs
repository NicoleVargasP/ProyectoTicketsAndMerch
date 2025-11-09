using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.Enum;
using MySqlConnector;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _sqlConn;
        private readonly string _mySqlConn;
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
            _sqlConn = _config.GetConnectionString("ConnectionSqlServer") ?? string.Empty;
            _mySqlConn = _config.GetConnectionString("ConnectionMySql") ?? string.Empty;

            var providerStr = _config.GetSection("DatabaseProvider").Value
                ?? "SqlServer";

            Provider = providerStr.Equals("MySql",
                StringComparison.OrdinalIgnoreCase)
                ? DatabaseProvider.MySql
                : DatabaseProvider.SqlServer;
        }

        public DatabaseProvider Provider { get; }

        public IDbConnection CreateConnection()
        {
            return Provider switch
            {
                DatabaseProvider.MySql => new MySqlConnection(_mySqlConn),
                _ => new SqlConnection(_sqlConn)
            };
        }
    }
}
