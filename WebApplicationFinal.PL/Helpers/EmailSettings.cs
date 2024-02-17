using System.Net.Mail;
using System.Net;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email) {
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mail@gmail.com", "code");
			client.Send("mail@gmail.com", email.To, email.Subject, email.Body);
		}

	}
}
