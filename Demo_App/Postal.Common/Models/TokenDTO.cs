using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Postal.Common.Models
{
	public class TokenDTO
	{
		[JsonPropertyName("accesstoken")]
		public string accessToken { get; set; }

		[JsonPropertyName("expiresin")]
		public int expiresIn { get; set; }

		[JsonPropertyName("refreshtoken")]
		public string refreshToken { get; set; }

		[JsonPropertyName("userdata")]
		public LoginAuthDTO userData { get; set; } = new LoginAuthDTO();
	}
}
