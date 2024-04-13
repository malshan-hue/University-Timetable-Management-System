using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.GlobalServices.Interfaces;

namespace BusinessLayer.GlobalServices
{
    public class GmailServiceImpl : IGmailService
    {
        public async Task<bool> SendEmail(string[] recipients, string subject, string body)
        {
            bool sent = true;
            var senderEmail = "thirangamicrosoft@gmail.com";
            var senderPassword = "svxg mlqh bavo wtfh";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, "UTMS"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (string recipient in recipients)
            {
                mail.To.Add(recipient);
            }

            try
            {
                await smtpClient.SendMailAsync(mail);
                return sent;
            }
            catch (Exception ex)
            {
                sent = false;
                return sent;
            }
        }
    }
}
