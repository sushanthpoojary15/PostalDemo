using Postal.Common.Extensions;
using Postal.Common.Models;
using Postal.Store.Abstraction;
using System.Data;
using Postal.Data.Abstraction;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using Microsoft.Extensions.Options;


namespace Postal.Stores.Buisness
{
    public class AuthStore : IAuthStore
    {
        private IDatabaseManager _databaseManager;
		private readonly IOptions<AppSettings> _options;
		public AuthStore(IDatabaseManager databaseManager, IOptions<AppSettings> options)
        {
            _databaseManager = databaseManager;
			_options = options;

        }
        public async Task<ApiResponse> VerifyUserLogin(LoginAuthDTO userDetails)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var passWord = userDetails.Password.MD5Hash();
                string commandText = "usp_VerifyUser";
                IDbDataParameter[] parameters = new IDbDataParameter[]  {
                 
                _databaseManager.CreateParameter("@Username", userDetails.UserName,  DbType.String),
                _databaseManager.CreateParameter("@Password",  passWord, DbType.String)
                };

                var result = await (_databaseManager.GetAllColumns<LoginAuthDTO>(commandText, parameters));
                var result1 = result.FirstOrDefault();
                if(result1== null)
                {
					response.Response = null;
					response.Message = "Not Authorized";
					response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
				}
                else
                {
					response.Response = result1;
					response.Message = "Success";
					response.StatusCode = System.Net.HttpStatusCode.OK;
					//TokenDTO tokenDTO = new TokenDTO();
					//tokenDTO.accessToken = GenerateAccessToken(result1);
					//tokenDTO.expiresIn = (int)TimeSpan.FromMinutes(_options.Value.Jwt.ExpiresIn).TotalSeconds;
					//tokenDTO.refreshToken = refreshToken;
					//tokenDTO.userData.UserName = result1.UserName;
					//tokenDTO.userData.Name = result1.Name;
					//tokenDTO.userData.Role = result1.Role;
					//tokenDTO.userData.Country = result1.Country;
					//tokenDTO.userData.EmailId = result1.EmailId;
					//tokenDTO.userData.IsSpares = result1.IsSpares;
					//tokenDTO.userData.IsServices = result1.IsServices;
					//tokenDTO.userData.Workflow = result1.Workflow;
					//tokenDTO.userData.UserId = result1.UserId;
					//response.Response = new { token = tokenDTO };
					//response.Message = string.Empty;
					//response.StatusCode = System.Net.HttpStatusCode.OK;
				}
               
            }
            catch (Exception ex)
            {

                //_logger.LogError(null, ex.Message, "VerifyUserLogin");
                response.Message = ex.Message;
                response.Response = null;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;

            }

            return response;

        }

		//private async Task<string> CreateRefreshToken(LoginAuthDTO result)
		//{
		//	try
		//	{
		//		string refreshToken = Guid.NewGuid().ToString().Replace("-", "");
		//		string commandText = "usp_CreateRefreshToken";
		//		IDbDataParameter[] parameters = new IDbDataParameter[]  {
		//			_databaseManager.CreateParameter("@UserId", result.UserName,  DbType.String),
		//			_databaseManager.CreateParameter("@RefreshToken", refreshToken,  DbType.String),
		//			_databaseManager.CreateParameter("@Role", result.Role,  DbType.String)
		//		};
		//		await _databaseManager.Insert(commandText, parameters);
		//		return refreshToken;
		//	}
		//	catch (Exception ex)
		//	{
		//		// _logger.LogError(authDTO, ex.Message, "GetRefreshToken");
		//		return string.Empty;
		//	}
		//}

		//public static string GenerateToken(LoginAuthDTO userModel, string signingKey, int tokenExpiry)
		//{
		//	var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
		//	var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		//	var claims = new[]
		//	{
		//		new Claim(ClaimTypes.NameIdentifier,userModel.UserId.ToString()),
		//		new Claim(ClaimTypes.Name,userModel.UserName),
		//		new Claim(ClaimTypes.Role,userModel.Role),
		//	};
		//	var token = new JwtSecurityToken(claims: claims,
		//									 expires: DateTime.Now.AddDays(tokenExpiry),
		//									 signingCredentials: credentials);


		//	return new JwtSecurityTokenHandler().WriteToken(token);

		//}

	}
}
