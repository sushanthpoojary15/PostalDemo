using System;
using System.Data;
using System.Data.Common;
using Postal.Common.Models;


using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Postal.Data.Abstraction;


namespace Postal.Data.Business
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly IOptions<AppSettings> _options;

        public DatabaseHandler(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public void CloseConnection(DbConnection dbConnection)
        {
            if (!ReferenceEquals(dbConnection, null))
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public IDataAdapter CreateAdapter(DbCommand dbCommand)
        {
            return new SqlDataAdapter((SqlCommand)dbCommand);
        }

        public DbCommand CreateCommand(string commandText, DbConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = CommandType.StoredProcedure
            };
        }

        public DbConnection CreateConnection(string connectionString = null)
        {
            connectionString = "Server=localhost;Database=Postal_Db;Integrated Security=True;Trusted_Connection=True;";

			if (string.IsNullOrEmpty(connectionString))
                return new SqlConnection(_options.Value.ConnectionStrings.DefaultConnection);
            else
                return new SqlConnection(connectionString);
        }

        public IDbDataParameter CreateParameter(DbCommand command)
        {
            return ((SqlCommand)command).CreateParameter();
        }
    }
}

