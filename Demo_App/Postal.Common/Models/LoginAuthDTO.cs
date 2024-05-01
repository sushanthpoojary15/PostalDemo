using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Postal.Common.Models
{
	public  class LoginAuthDTO
	{

		[JsonPropertyName("id")]
		public int ID { get; set; }
		[JsonPropertyName("username")]
		public string? UserName { get; set; }
		[JsonPropertyName("password")]
		public string? Password { get; set; }

		[JsonPropertyName("firstname")]
		public string? FirstName { get; set; }

		[JsonPropertyName("lastname")]
		public string? LastName { get; set; }

		[JsonPropertyName("roleid")]
		public int? RoleId { get; set; }

		[JsonPropertyName("rolecode")]
		public string? RoleCode { get; set; }
	}
}
