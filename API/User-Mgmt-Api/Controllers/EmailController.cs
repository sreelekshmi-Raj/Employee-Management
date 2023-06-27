using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace User_Mgmt_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmailController : Controller
	{
		[HttpPost]
		public IActionResult SendEmail(string body)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("tia.wolf65@ethereal.email"));
			email.To.Add(MailboxAddress.Parse("tia.wolf65@ethereal.email"));
			email.Subject = "Important message";
			email.Body=new TextPart(TextFormat.Html) { Text=body};

			using var smtp = new SmtpClient();
			smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);

			smtp.Authenticate("tia.wolf65@ethereal.email", "RZMyaz4T3rCguxNM9N");
			smtp.Send(email);
			smtp.Disconnect(true);
			return Ok();

		

		}
	}
}
