using System.ComponentModel.DataAnnotations;

namespace User_Mgmt_Api.Model.DTO
{
	public class CreateUser
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public int PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
	}
}
