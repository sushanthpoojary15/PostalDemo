using Postal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postal.Store.Abstraction
{
	public interface IAuthStore
	{
		Task<ApiResponse> VerifyUserLogin(LoginAuthDTO userDetails);

	}
}
