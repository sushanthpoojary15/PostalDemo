using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postal.Data.Abstraction
{
	public interface IDatabaseHandler
	{
		DbConnection CreateConnection(string connectionString = null);
		void CloseConnection(DbConnection dbConnection);
		DbCommand CreateCommand(string commandText, DbConnection connection);
		IDataAdapter CreateAdapter(DbCommand dbCommand);
		IDbDataParameter CreateParameter(DbCommand command);
	}
}
