using System.Net;
using System.Net.Mail;

namespace EcommerceWebApp.Services
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var fromMail = "maazulhaq.aptech@gmail.com";
            var fromPw = "YOUR APP PASSWORD";

            // SMTP server address: smtp.gmail.com, port: 587 (TLS) or 465 (SSL)
            
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, fromPw) 
            };

            return client.SendMailAsync(new MailMessage(fromMail, email, subject, body));


        }
    }
}
