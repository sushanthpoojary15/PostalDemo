using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postal.Common.Models;
using Postal.Store.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Postal.API.Controllers
{
	public class LoginController : Controller
	{
		public IAuthStore _authStore { get; set; }
		public LoginController(IAuthStore authStore)
        {
            _authStore = authStore;
        }
        public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		[AllowAnonymous]
		// Token generation for Normal login
		public async Task<IActionResult> LoginUser(string username,string password)
		{
			ApiResponse resposne = new ApiResponse();
			try
			{
				LoginAuthDTO loginAuthDTO = new LoginAuthDTO();
				loginAuthDTO.UserName = username;
				loginAuthDTO.Password = password;
			    var result=await _authStore.VerifyUserLogin(loginAuthDTO);
				if(result.StatusCode== System.Net.HttpStatusCode.OK)
				{
					//HttpContext.Session.SetString(Constants.ACCESS_TOKEN, (string)result.Response.token.accesstoken);
					//HttpContext.Session.SetString(Constants.REFRESH_TOKEN, (string)result.Response.token.refreshtoken);
					//HttpContext.Session.SetString(Constants.USER_NAME, (string)res);
					HttpContext.Session.SetString(Constants.NAME, (string)(result.Response.FirstName+" "+result.Response.LastName));
					HttpContext.Session.SetString(Constants.USER_ROLE, (string)result.Response.RoleCode);
					HttpContext.Session.SetInt32(Constants.USER_ID,(int)result.Response.ID);
					return  RedirectToAction("Index", "home", null);
				}
				return RedirectToAction("Index", "Login", null);
			}
			catch (Exception ex)
			{
				throw ex;
				return RedirectToAction("Index", "Login", null);
			}
			// Authenticate user
			//serDetailsDTO userDetails = JsonConvert.DeserializeObject<UserDetailsDTO>(test);
		//	var response = await _authStore.


			//if(response.Response != null)
			//{
			//    foreach (var parameter in response.Response)
			//    {
			//        var claims = new[]
			//        {
			//                new Claim("UserName", UserDetails.UserName),
			//                new Claim(ClaimTypes.Name, parameter.Name.ToString()),
			//                new Claim(ClaimTypes.Role, parameter.Role.ToString()),
			//                new Claim(ClaimTypes.Country, parameter.Country.ToString()),

			//            };

			//        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			//        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
			//        new ClaimsPrincipal(identity));
			//    }

			//}
		
		}

	}
}
