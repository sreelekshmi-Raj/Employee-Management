using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Mgmt_Api.Model
{
	public class UserLogin
	{
		//this is saving in db
		public string UserName { get; set; }=string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; } = DateTime.MinValue;
		public DateTime Expires { get; set; }

    }
}
