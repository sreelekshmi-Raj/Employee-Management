using Microsoft.AspNetCore.Mvc;

namespace User_Mgmt_Api.Repository.EmailRepo
{
	public interface IEmailService
	{
		void SendEmail(EmailDTO emailDTO);
	}
}
