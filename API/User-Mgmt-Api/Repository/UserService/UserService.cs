using System.Security.Claims;

namespace User_Mgmt_Api.Repository.UserService
{
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor httpContext;

		public UserService(IHttpContextAccessor httpContext)
        {
			this.httpContext = httpContext;
		}

        public string GetUserName()
		{
			string userName = null;
			if(httpContext.HttpContext != null) {
				userName = httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
			}
			return userName;
		}
	}
}
