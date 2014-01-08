using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IMessageService
    {
        Task<bool> SendEmail(string to, string subject, string htmlBody);
    }

    public class MessageService : IMessageService
    {
        public Task<bool> SendEmail(string to, string subject, string htmlBody)
        {
            throw new NotImplementedException();
        }
    }
}