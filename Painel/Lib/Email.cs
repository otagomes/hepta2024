using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Painel.Lib
{
    public class Email : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public static async Task EnviarEmail(string paraEmail, string titulo, string mensagem)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("contato@hepta.com.br", "hepta")
            };

            mail.To.Add(new MailAddress(paraEmail));
            mail.Bcc.Add(new MailAddress("otagomes@hotmail.com"));

            mail.Subject = $"{titulo}";
            mail.Body = $"{mensagem}";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            
            using (SmtpClient smtp = new SmtpClient("mail.hepta.com.br", 587))
            {
                smtp.Credentials = new NetworkCredential("naoresponda@hepta.com.br", "<SENHADOEMAIL>");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}