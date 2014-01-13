using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public class MessageService : IMessageService
    {
        private const string FROM_EMAIL = "system@argeset.com";

        public Task<bool> SendEmail(string to, string subject, string htmlBody)
        {
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(htmlBody) || !to.IsEmail()) return Task.FromResult(false);

            var destination = new Destination { ToAddresses = new List<string> { to } };

            var contentSubject = new Content { Charset = Encoding.UTF8.EncodingName, Data = subject };
            var contentBody = new Content { Charset = Encoding.UTF8.EncodingName, Data = htmlBody };
            var body = new Body { Html = contentBody };

            var message = new Message { Body = body, Subject = contentSubject };

            var request = new SendEmailRequest
            {
                Source = FROM_EMAIL,
                Destination = destination,
                Message = message
            };
            
            var client = new AmazonSimpleEmailServiceClient();
            
            try
            {
                client.SendEmail(request);
            }
            catch (Exception ex)
            {
               return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}