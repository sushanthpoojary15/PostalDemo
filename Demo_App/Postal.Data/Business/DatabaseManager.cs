using System;
using Postal.Common.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using System.Text;
//using System.Data.SqlClient;
using Postal.Data.Abstraction;
using Microsoft.Data.SqlClient;
using Postal.Common.Extensions;


namespace Postal.Data.Business
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseHandler _databaseHandler;
        private readonly IParamManager _paramManager;
        private readonly IOptions<AppSettings> _options;
     

        public DatabaseManager(IOptions<AppSettings> options, IDatabaseHandler databaseHandler, IParamManager paramManager)
        {
            _databaseHandler = databaseHandler;
            _paramManager = paramManager;
            _options = options;
          
        }

		public DbConnection CreateDbConnection(string connectionString = null)
		{
			return _databaseHandler.CreateConnection(connectionString);
		}

		public void CloseConnection(DbConnection dbConnection)
		{
			_databaseHandler.CloseConnection(dbConnection);
		}

		public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
		{
			return _paramManager.CreateParameter(name, value, dbType);
		}

		public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
		{
			return _paramManager.CreateParameter(name, value, dbType, parameterDirection);
		}

		public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
		{
			return _paramManager.CreateParameter(name, size, value, dbType);
		}

		public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection)
		{
			return _paramManager.CreateParameter(name, size, value, dbType, parameterDirection);
		}

		public IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType)
		{
			return _paramManager.CreateParameter(name, value, sqlDbType);
		}

		public IDbDataParameter CreateOutputParameter(string name, SqlDbType sqlDbType)
		{
			return _paramManager.CreateOutputParameter(name, sqlDbType);
		}

		private async void SendMail(Exception ex)
		{
			var subject = "SQL Server Connection";
			StringBuilder htmlContent = new StringBuilder();
			htmlContent.AppendFormat("<div>");
			htmlContent.AppendFormat($"<p>Hi Team,</p>");
			htmlContent.AppendFormat($"<p>An error has occurred while establishing a connection to the SQL server.\n\n</p>");
			htmlContent.AppendFormat($"<p>Thank you,</p>");
			htmlContent.AppendFormat($"<p>HotelIQ</p>");
			htmlContent.AppendFormat("</div>");

			byte[] data = Encoding.ASCII.GetBytes(ex.ToString());
			MemoryStream ms = new MemoryStream(data);
			var attachment = new System.Net.Mail.Attachment(ms, "SQLServer.txt");

			//var mailResponse = await _commonUtility.SendEmailAsync(null, _options.Value.SupportList, subject, htmlContent.ToString(), "HotelIQ API App", attachment);
		}


		public async Task<IEnumerable<T>> GetAllColumns<T>(string commandText, IDbDataParameter[] parameters = null, string connectionString = null) where T : class
		{
			using (var connection = _databaseHandler.CreateConnection(connectionString))
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				if (parameters != null)
					command.Parameters.AddRange(parameters);
				command.CommandTimeout = 300;
				var dataAdapter = _databaseHandler.CreateAdapter(command);
				var dataSet = new DataSet();
				dataAdapter.Fill(dataSet);

				return dataSet.Tables[0].ToList<T>();
			}
		}
		public async Task<IEnumerable<T>> Select<T>(string commandText, Func<IDataReader, T> selector, IDbDataParameter[] parameters = null) where T : class
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				if (parameters != null)
					command.Parameters.AddRange(parameters);
				command.CommandTimeout = 300;
				var reader = await command.ExecuteReaderAsync();
				return reader.Select<T>(selector);
			}
		}

		public async Task<object> GetScalarValue(string commandText, IDbDataParameter[] parameters = null)
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				if (parameters != null)
					command.Parameters.AddRange(parameters);
				command.CommandTimeout = 300;
				return await command.ExecuteScalarAsync();
			}
		}

		public async Task<object> Insert(string commandText, IDbDataParameter[] parameters = null)
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);

				command.CommandType = CommandType.StoredProcedure;
				command.CommandTimeout = 300;
				if (parameters != null)
					command.Parameters.AddRange(parameters);
				return await command.ExecuteScalarAsync();
			}
		}

		public async Task Update(string commandText, IDbDataParameter[] parameters = null)
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				command.CommandTimeout = 300;
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task Delete(string commandText, IDbDataParameter[] parameters = null)
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				command.CommandTimeout = 300;
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task InsertOrUpdateWithTransaction(string commandText, IDbDataParameter[] parameters = null)
		{
			using (var connection = _databaseHandler.CreateConnection())
			{
				SqlTransaction transaction = null;
				try
				{
					await connection.OpenAsync();
				}
				catch (Exception ex)
				{
					SendMail(ex);
					throw new Exception(ex.Message);
				}
				var command = _databaseHandler.CreateCommand(commandText, connection);
				if (parameters != null)
					command.Parameters.AddRange(parameters);
				command.CommandTimeout = 300;
				transaction = await Task.Run(
						() => (SqlTransaction)connection.BeginTransaction(IsolationLevel.ReadUncommitted)
				);

				try
				{
					command.Transaction = transaction;
					command.CommandTimeout = 300;
					await command.ExecuteNonQueryAsync();
					await Task.Run(() => transaction.Commit());
				}
				catch (Exception ex)
				{
					try
					{
						transaction.Rollback();
					}
					catch (Exception ex1)
					{
						// throw new Exception($"{ex1.Message}; Transaction failed, rollback not successfull : {(ex1.InnerException != null ? ex1.InnerException.Message : "")}");
						throw new Exception($"{ex1.Message}");
					}

					var msg = ex.Message.Split('\'');
					//throw new Exception($"{ex.Message}; Transaction failed : {(ex.InnerException != null ? ex.InnerException.Message : "")}");
					throw new Exception($"{ex.Message}");

				}
			}
		}
	}
}

