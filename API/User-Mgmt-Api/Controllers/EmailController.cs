using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using User_Mgmt_Api.Repository.EmailRepo;

namespace User_Mgmt_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmailController : Controller
	{
		private readonly IEmailService emailService;

		public EmailController(IEmailService emailService)
		{
			this.emailService = emailService;
		}
		[HttpPost]
		public IActionResult SendEmail(EmailDTO request)
		{
			emailService.SendEmail(request);
			return Ok();
		}
	}
}
