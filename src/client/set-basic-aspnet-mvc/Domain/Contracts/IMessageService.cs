using System.Threading.Tasks;

namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface IMessageService
    {
        Task<bool> SendEmail(string to, string subject, string htmlBody);
    }
}