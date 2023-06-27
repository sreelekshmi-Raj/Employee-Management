using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using User_Mgmt_Api.Repository.UserService;

namespace User_Mgmt_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : Controller
	{
		public static UserLogin user = new UserLogin();
		private readonly IConfiguration configuration;
		private readonly IUserService userService;

		public AuthenticationController(IConfiguration configuration,IUserService userService)
        {
			this.configuration = configuration;
			this.userService = userService;
		}
		[HttpGet,Authorize]
		public ActionResult<Object> GetMe()
		{
			string userName=userService.GetUserName();
			return Ok(userName);
			//string userName = User?.Identity?.Name;
			//string userName2 = User.FindFirstValue(ClaimTypes.Name);
			//string role=User.FindFirstValue(ClaimTypes.Role);
			//return Ok(new {userName,userName2,role});
		}
        [HttpPost("register")]
		public async Task<ActionResult<UserLogin>> Register(UserDTO userDTO)
		{
			CreatePasswordHash(userDTO.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

			// UserLogin user=new UserLogin() { 
			//	UserName = userDTO.UserName,
			//	PasswordHash = PasswordHash,
			//	PasswordSalt = PasswordSalt
			//};
			user.UserName = userDTO.UserName;
			user.PasswordHash = PasswordHash;
			user.PasswordSalt=PasswordSalt;
			return Ok(user);

		}
		[HttpPost("Login")]
		public async Task<ActionResult<string>> Login(UserDTO userDTO)
		{
			if (user.UserName != userDTO.UserName)
				return BadRequest("User not Found");

			if(!VerifyPasswordHash(userDTO.Password,user.PasswordHash,user.PasswordSalt))
			{
				return BadRequest("Wrong Password");
			}
			string token = CreateToken(user);
			//Generate refresh token
			RefreshToken refreshToken = GenerateRefreshToken();
			SetRefreshToken(refreshToken);
			return Ok(token);
		}

		[HttpGet("refresh-token")]
		public async Task<ActionResult<string>> RefreshToken()
		{
			var cookieRefreshToke = Request.Cookies["refreshToken"];
			if (!user.RefreshToken.Equals(cookieRefreshToke))
			{
				return Unauthorized("Invalid Rfresh Token");
			}
			else if (user.Expires < DateTime.Now)
				return Unauthorized("Token expired");

			string token=CreateToken(user);
			RefreshToken refreshToken = GenerateRefreshToken();
			SetRefreshToken(refreshToken);

			return Ok(token);
		}
		private RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken()
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Expires = DateTime.Now.AddHours(1),
				Created = DateTime.Now
			};
			return refreshToken;
		}
		private void SetRefreshToken(RefreshToken refreshToken)
		{
			var cookieOption = new CookieOptions()
			{
				HttpOnly = true,
				Expires=refreshToken.Expires
			};
			Response.Cookies.Append("refreshToken",refreshToken.Token,cookieOption);

			user.RefreshToken=refreshToken.Token;
			user.TokenCreated = refreshToken.Created;
			user.Expires = refreshToken.Expires;

		}
		private string CreateToken(UserLogin userLogin)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, userLogin.UserName),
				new Claim(ClaimTypes.Role,"Admin")
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				configuration.GetSection("AppSettings:Token").Value
				));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(
				claims:claims,
				expires:DateTime.Now.AddDays(1),
				signingCredentials:credentials
				);
			var jwt=new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
		private void CreatePasswordHash(string password,out byte[] PasswordHash,out byte[] PasswordSalt)
		{
			using(var hmac=new HMACSHA512())
			{
				PasswordSalt = hmac.Key;
				PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}
		private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
		{
			using (var hmac = new HMACSHA512(PasswordSalt))
			{
				var currentPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				//return currentPasswordHash == PasswordHash;
				return currentPasswordHash.SequenceEqual(PasswordHash);
			}
		}
		
	}
}
