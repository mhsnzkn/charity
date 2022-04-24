using Data.Utility.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utility.MailService
{
    public interface IMailService
    {
        Task<Result> SendMail(string subject, string body, string to, string[] cc = null);
        Task<Result> SendNewApplicationMail(string firstName, string lastName, string email);
        Task<Result> SendDBSMail(string firstName, string lastName, string email);
        Task<Result> SendDBSUploadDocMail(string firstName, string lastName, string email, Guid key);
        Task<Result> SendAgreementMail(string firstName, string lastName, string email, Guid key);
        Task<Result> SendOnHoldMail(string firstName, string lastName, string email);
        Task<Result> SendCompletedMail(string firstName, string lastName, string email);
    }
}
