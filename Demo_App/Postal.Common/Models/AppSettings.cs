using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Postal.Common.Models
{
	public class ConnectionStrings
	{
		
		public string DefaultConnection { get; set; }

	}


	public class AppSettings
	{
		public ConnectionStrings ConnectionStrings { get; set; }
		public SendGridAcc SendGrid { get; set; }
		public SmtpServer Smtp { get; set; }
		public string SupportList { get; set; }
		public string ApiBaseUrl { get; set; }
		public Jwt Jwt { get; set; }
		public ClaimTypes ClaimTypes { get; set; }
		public BlobData BlobData { get; set; }
	}

	public class SmtpServer
	{
		public string SmtpHost { get; set; }
		public int PortNumber { get; set; }
		public bool EnableSSL { get; set; }
		public string SmtpUserName { get; set; }
		public string SmtpPassword { get; set; }
		public string FromAddress { get; set; }
	}

	public class SendGridAcc
	{
		public string From { get; set; }
		public string To { get; set; }
		public string CC { get; set; }
		public string Key { get; set; }

	}

	public class Jwt
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public string Secret { get; set; }
		public int ClockSkew { get; set; }
		public int ExpiresIn { get; set; }
	}


	public class ClaimTypes
	{
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
		public string Country { get; set; }
		public string UserObjectId { get; set; }
	}
	public class BlobData
	{
		public string SupportContainer { get; set; }
		public string StorageConnectionString { get; set; }
	}
}
