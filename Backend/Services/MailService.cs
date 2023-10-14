using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backend.Services
{
    public static class MailService
    {
        public static void SendEmail(MailMessage mailMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("ispagrindai945@gmail.com", "rolczdktsktctrfq"),
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
        }
    }
}