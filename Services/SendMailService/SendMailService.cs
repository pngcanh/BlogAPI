using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BlogAPI.Services.SendMailService
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSettings mailSettings;

        public SendMailService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public async Task SenMail(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            // thong tin nguoi gui
            email.Sender = new MailboxAddress(mailSettings.SenderName, mailSettings.SenderEmail);
            email.From.Add(new MailboxAddress(mailSettings.SenderName, mailSettings.SenderEmail));
            //thong tin nguoi nhan
            email.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            //tieu do mail
            email.Subject = emailDTO.Subject;
            //noi dung mail
            var builder = new BodyBuilder();
            builder.HtmlBody = emailDTO.Body;//Thiet lap noi dung gui di 
            email.Body = builder.ToMessageBody();
            //ket noi voi may chu
            using var smtp = new SmtpClient();// su dung smtp client cuar mailkit
            try
            {
                smtp.Connect(mailSettings.Server, mailSettings.Port, SecureSocketOptions.StartTls); // mo ket noi toi may chu
                                                                                                    //xac thuc ket noi
                smtp.Authenticate(mailSettings.SenderEmail, mailSettings.Password);
                //gui mail
                await smtp.SendAsync(email);
                //sau khi gui thu ngat ket noi
                await smtp.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

    }
}