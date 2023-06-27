using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using User_Mgmt_Api.Repository.EmailRepo;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;

namespace User_Mgmt_Api.Repository.EmailService
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration configuration;

		public EmailService(IConfiguration configuration)
        {
			this.configuration = configuration;
		}
        public void SendEmail(EmailDTO emailDTO)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailSettings:EmailUserName").Value));
			email.To.Add(MailboxAddress.Parse(emailDTO.To));
			email.Subject = emailDTO.Subject;
			email.Body = new TextPart(TextFormat.Text) { Text = emailDTO.Body };

			using var smtp = new SmtpClient();
			smtp.Connect(configuration.GetSection("EmailSettings:EmailHost").Value, 587, SecureSocketOptions.StartTls);

			smtp.Authenticate(configuration.GetSection("EmailSettings:AuthEmailUser").Value, configuration.GetSection("EmailSettings:AuthEmailPassword").Value);
			smtp.Send(email);
			smtp.Disconnect(true);
	
		}
	}
}
