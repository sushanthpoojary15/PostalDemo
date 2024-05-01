using Postal.Data.Abstraction;
using System;
using System.Data;
using System.Data.SqlClient;

namespace  Postal.Data.Business
{
    public class ParamManager : IParamManager
    {
        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value
            };
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value
            };
        }

        public IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                SqlDbType = sqlDbType,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value
            };
        }

		public IDbDataParameter CreateOutputParameter(string name, SqlDbType dbType)
		{
			return new SqlParameter
			{
				SqlDbType = dbType,
				ParameterName = name,
				Direction = ParameterDirection.Output
			};
		}

	}
}

