using System;
using System.Data;
using System.Data.Common;
namespace Postal.Data.Abstraction
{
    public interface IDatabaseManager
    {
        DbConnection CreateDbConnection(string connectionString = null);
        void CloseConnection(DbConnection dbConnection);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType);
        IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection);

		IDbDataParameter CreateOutputParameter(string name, SqlDbType dbType);
		Task<IEnumerable<T>> GetAllColumns<T>(string commandText, IDbDataParameter[] parameters = null, string connectionString = null) where T : class;

		Task<IEnumerable<T>> Select<T>(string commandText, Func<IDataReader, T> selector, IDbDataParameter[] parameters = null) where T : class;
        Task<object> GetScalarValue(string commandText, IDbDataParameter[] parameters = null);
        Task<object> Insert(string commandText, IDbDataParameter[] parameters = null);
        Task Update(string commandText, IDbDataParameter[] parameters = null);
        Task Delete(string commandText, IDbDataParameter[] parameters = null);
        Task InsertOrUpdateWithTransaction(string commandText, IDbDataParameter[] parameters = null);
    }
}

