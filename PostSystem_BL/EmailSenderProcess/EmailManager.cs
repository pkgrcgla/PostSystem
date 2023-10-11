﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_BL.EmailSenderProcess
{
    public class EmailManager : IEmailManager
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(EmailMessageModel model)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("hgyazilimsinifi@outlook.com");
                message.To.Add(new MailAddress(model.To));
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("hgyazilimsinifi@outlook.com", "betulkadikoy2023");
                client.Port = 587;
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true;

                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                //loglasın
            }
        }

        public async Task SendMailAsync(EmailMessageModel model)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("hgyazilimsinifi@outlook.com");
                message.To.Add(new MailAddress(model.To));
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("hgyazilimsinifi@outlook.com", "betulkadikoy2023");
                client.Port = 587;
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true;

                await client.SendMailAsync(message);

            }
            catch (Exception)
            {

                //loglasın
            }

        }


        public bool SendEmailGmail(EmailMessageModel model)
        {
            try
            {
                var xx = _configuration.GetSection("SystemEmailOptions:Email").ToString();
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration.GetSection("SystemEmailOptions:Email").Value?.ToString());
                message.To.Add(new MailAddress(model.To));
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("SystemEmailOptions:Email").Value?.ToString(), _configuration.GetSection("SystemEmailOptions:Token").Value?.ToString());
                client.Port = Convert.ToInt32(_configuration.GetSection("SystemEmailOptions:SmtpPort").Value); ;
                client.Host = _configuration.GetSection("SystemEmailOptions:SmtpHost").Value?.ToString();
                client.EnableSsl = true;

                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                //loglasın
            }
        }

    }
}
