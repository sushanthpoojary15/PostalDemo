using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Postal.Common.Models
{
	public class BaseResponse
	{
		[JsonPropertyName("statuscode")]
		public HttpStatusCode StatusCode { get; set; }
		[JsonPropertyName("message")]
		public string Message { get; set; }
		[JsonPropertyName("callstarttime")]
		public string CallStartTime { get; set; }
		[JsonPropertyName("callendtime")]
		public string CallEndTime { get; set; }

		private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

		public BaseResponse()
		{
			this.CallStartTime = DateTime.UtcNow.ToString(_dateTimeFormat);
			//this.ServerName = System.Environment.MachineName;
		}

		public void Complete()
		{
			this.CallEndTime = DateTime.UtcNow.ToString(_dateTimeFormat);
		}
	}
}
