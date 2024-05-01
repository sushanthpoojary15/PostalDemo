using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Postal.Common.Models
{
	public class ApiResponse:BaseResponse
	{
		[JsonPropertyName("response")]
		public dynamic Response { get; set; }
	}
}
