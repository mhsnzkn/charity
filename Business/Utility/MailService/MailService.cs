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
        private const string Header = "<div style=\"width:100%;background-color:#f2f2f0\"><img width=\"350\" src=\"https://management.heart4refugees.org/logo.png\"/></div>";
        private const string Footer = "<br/><p>Kind Regards</p><p>The Admin Team</p><p>Heart4Refugees</p>";
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
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>Thank you for you application to volunteer with us.</p><p>A member of our team will contact you soon.</p>{Footer}";
            var result = await SendMail("New Application", body, email, mailSettings.CC);
            return result;
        }

        public async Task<Result> SendDBSMail(string firstName, string lastName, string email)
        {
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>Following our earlier conversation, we would like to proceed to the next stage and invite you to complete an enhanced DBS check by following the link below.</p><ul><li>Please go to <a href=\"https://www.carecheck.co.uk\" target=\"_blank\">www.carecheck.co.uk</a></li><li>Click on 'Start a DBS application'</li><li>Click on 'complete your DBS application'</li><li>Tick the important note box</li><li>You must request an enhanced application</li><li>You will be taken to the log in page.</li><li>Organisation ref is <b>HEART4REFUGEES</b></li><li>Please leave the organisation code blank and then click start.</li></ul><p>Once you have submitted it, a member of our team will contact you to check your ID documents.</p><p>Turn around is pretty quick and we will be in touch as soon as we have clearance.</p>{Footer}";
            var result = await SendMail("DBS Request", body, email);
            return result;
        }

        public async Task<Result> SendDBSUploadDocMail(string firstName, string lastName, string email, Guid key)
        {
            var link = "https://management.heart4refugees.org/Forms/VolunteerDocument/"+key;
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>We received your DBS application and now we need to verify your ID. Please click on the following link to read the ID requirements.</p><a href=\"https://www.carecheck.co.uk/support/applicant-id-requirements/\" target=\"_blank\">https://www.carecheck.co.uk/support/applicant-id-requirements/</a><br/><br/><p>We will then need you to upload the picture of your documents. Once we have checked them your documents will be deleted. Please make sure you send us a document for each of the three sections.</p><a href=\"{link}\" target=\"_blank\" style=\"text-decoration: none;padding: 8px;background-color:dodgerblue;color: white;\"><b>Upload Your Docs</b></a><br/><br/><p>Turn around is usually a few days so hopefully we can get you on board quickly.</p>{Footer}";
            var result = await SendMail("Docs for DBS", body, email);
            return result;
        }

        public async Task<Result> SendAgreementMail(string firstName, string lastName, string email, Guid key)
        {
            var link = "https://management.heart4refugees.org/Forms/VolunteerAgreement/" + key;
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>We have received your DBS clearance from Care Check and now need you to read and sign the Volunteer Agreement. Please ensure you have read all the policies. Once we have received your signed agreement we will arrange a date for your induction.</p><p>Please go to the page here and follow the instructions.</p><br/><br/><a href=\"{link}\" target=\"_blank\" style=\"text-decoration: none;padding: 8px;background-color:dodgerblue;color: white;\"><b>Go to the page</b></a><br/><br/>{Footer}";
            var result = await SendMail("Volunteer Agreement", body, email);
            return result;
        }

        public async Task<Result> SendOnHoldMail(string firstName, string lastName, string email)
        {
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>Thank you for your recent application to volunteer with us. At the moment we do not have any volunteer opportunities which would use your skills. However, we will keep your details on hold and if other opportunities arise we will contact you. Thanks again for your interest in us and the refugee crisis.</p>{Footer}";
            var result = await SendMail("Heart4Refugees", body, email);
            return result;
        }
        public async Task<Result> SendCompletedMail(string firstName, string lastName, string email)
        {
            string body = $"{Header}<p>Dear {firstName} {lastName}</p><p>Your application process is now complete and we are happy to welcome you to the Heart4Refugees team. We have added you to our volunteer email and whatsapp groups and hope you enjoy being part of our growing organisation.</p>{Footer}";
            var result = await SendMail("Heart4Refugees", body, email);
            return result;
        }
    }

}
