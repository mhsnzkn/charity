using Data.Utility.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utility.MailService
{
    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;
        public MailService(IConfiguration config)
        {
            this.mailSettings = config.GetSection("MailSettings").Get<MailSettings>();
        }
        public async Task<Result> SendMail(string subject, string body, string to, string[] cc = null)
        {
            var result = new Result();
            try
            {
                var client = new SmtpClient
                {
                    Host = mailSettings.Host,
                    Port = mailSettings.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Email, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mailSettings.Email, mailSettings.DisplayName),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                };
                if(cc != null && cc.Length > 0)
                {
                    foreach(var item in cc)
                    {
                        mailMessage.CC.Add(item);
                    }
                }
                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
            catch (System.Exception ex)
            {
                result.SetError(ex.ToString());
            }
            return result;
        }

        public async Task<Result> SendNewApplicationMail(string firstName, string lastName, string email)
        {
            string body = $"<p>Dear {firstName} {lastName}</p><p>Thank you for you application to volunteer with us.</p><p>A member of our team will contact you soon.</p><br/><p>Kind Regards</p><p>Heart4refugees Team</p>";
            var result = await SendMail("New Application", body, email, mailSettings.CC);
            
            return result;
        }

        
    }

}
