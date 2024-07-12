using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mohammedhanni222@gmail.com", "ukhnyclinotbmvmz");
			client.Send("mohammedhanni222@gmail.com", email.Recipients, email.Subject, email.Body);
		}
	}
}
