using System;
using System.Data;

namespace Postal.Data.Abstraction
{
    public interface IParamManager
    {
        IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input);

        IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType, ParameterDirection parameterDirection = ParameterDirection.Input);
		IDbDataParameter CreateOutputParameter(string name, SqlDbType dbType);
	}
}

