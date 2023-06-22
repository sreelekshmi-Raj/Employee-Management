using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace User_Mgmt_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : Controller
	{
		public static UserLogin user = new UserLogin();
		private readonly IConfiguration configuration;

		public AuthenticationController(IConfiguration configuration)
        {
			this.configuration = configuration;
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
			return Ok(token);
		}   
		private string CreateToken(UserLogin userLogin)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, userLogin.UserName),
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
