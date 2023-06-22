﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Mgmt_Api.Model
{
	public class UserLogin
	{
		public string UserName { get; set; }=string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
	}
}