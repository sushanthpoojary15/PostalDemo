using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Postal.Common.Extensions
{
	public static class LinqExtensions
	{
		public static List<T> ToList<T>(this DataTable dataTable) where T : class
		{
			List<T> data = new List<T>();

			foreach (DataRow row in dataTable.Rows)
			{
				T item = GetItem<T>(row);
				data.Add(item);
			}

			return data;
		}

		private static T GetItem<T>(DataRow row) where T : class
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in row.Table.Columns)
			{
				foreach (PropertyInfo propertyInfo in temp.GetProperties())
				{
					if (propertyInfo.Name == column.ColumnName)
						propertyInfo.SetValue(obj, DBNull.Value.Equals(row[column.ColumnName]) ? null : row[column.ColumnName], null);
					else
						continue;
				}
			}

			return obj;
		}

		public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
		{
			while (reader.Read())
			{
				yield return projection(reader);
			}
		}

		public static string MD5Hash(this string text)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			//compute hash from the bytes of text
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

			//get hash result after compute it
			byte[] result = md5.Hash;

			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				//change it into 2 hexadecimal digits
				//for each byte
				strBuilder.Append(result[i].ToString("x2"));
			}

			return strBuilder.ToString();
		}
	}
}
