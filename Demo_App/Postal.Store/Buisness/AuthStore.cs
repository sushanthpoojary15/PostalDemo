using Postal.Common.Extensions;
using Postal.Common.Models;
using Postal.Store.Abstraction;
using System.Data;
using Postal.Data.Abstraction;


namespace Postal.Store.Buisness
{
	public  class AuthStore:IAuthStore
	{
		//private IDatabaseManager _databaseManger;
		public AuthStore()
        {
            
        }
		//public async Task<ApiResponse> VerifyUserLogin(LoginAuthDTO userDetails)
		//{
		//	ApiResponse response = new ApiResponse();

		//	try
		//	{
		//		var passWord = userDetails.Password.MD5Hash();
		//		string commandText = "usp_VerifyUserLogin";
		//		IDbDataParameter[] parameters = new IDbDataParameter[]  {
		//	 _databaseManager.CreateParameter("@Username", userDetails.UserName,  DbType.String),
		//	 _databaseManager.CreateParameter("@Password",  passWord, DbType.String)
		// };

		//		var result = await (_databaseManager.GetAllColumns<LoginAuthDTO>(commandText, parameters));
		//		var result1 = result.FirstOrDefault();
		//		if (result1 != null)
		//		{
		//			string refreshToken = await CreateRefreshToken(result1);
		//			if (String.IsNullOrEmpty(refreshToken))
		//			{
		//				response.Response = null;
		//				response.Message = "Failed to generate refresh token";
		//				response.StatusCode = System.Net.HttpStatusCode.BadRequest;

		//			}
		//			else
		//			{

		//				TokenDTO tokenDTO = new TokenDTO();
		//				tokenDTO.accessToken = GenerateAccessToken(result1);
		//				tokenDTO.expiresIn = (int)TimeSpan.FromMinutes(_options.Value.Jwt.ExpiresIn).TotalSeconds;
		//				tokenDTO.refreshToken = refreshToken;
		//				tokenDTO.userData.UserName = result1.UserName;
		//				tokenDTO.userData.Name = result1.Name;
		//				tokenDTO.userData.Role = result1.Role;
		//				tokenDTO.userData.Country = result1.Country;
		//				tokenDTO.userData.EmailId = result1.EmailId;
		//				tokenDTO.userData.IsSpares = result1.IsSpares;
		//				tokenDTO.userData.IsServices = result1.IsServices;
		//				tokenDTO.userData.Workflow = result1.Workflow;
		//				tokenDTO.userData.UserId = result1.UserId;
		//				response.Response = new { token = tokenDTO };
		//				response.Message = string.Empty;
		//				response.StatusCode = System.Net.HttpStatusCode.OK;
		//			}


		//			//response.Response = result;
		//			//response.Message = "Success";
		//			//response.StatusCode = System.Net.HttpStatusCode.OK;

		//		}
		//		else
		//		{
		//			response.Response = null;
		//			response.Message = "Not Authorized";
		//			response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
		//		}
		//	}
		//	catch (Exception ex)
		//	{

		//		//_logger.LogError(null, ex.Message, "VerifyUserLogin");
		//		response.Message = ex.Message;
		//		response.Response = null;
		//		response.StatusCode = System.Net.HttpStatusCode.InternalServerError;

		//	}

		//	return response;

		//}
	}
}
