using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface IFeedbackService
    {
        Task<bool> AddFeedback(long userId, string userEmail, string info);

        Task<FeedbackDto> GetFeedback(int id);
        
        Task<bool> SetFeedbackToReviewed(int id);

        Task<PagedList<FeedbackDto>> GetFeedbacks(int page, bool isReviewed = false);
    }
}