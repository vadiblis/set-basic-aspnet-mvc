using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface IUserService
    {
        Task<long?> Create(string fullName, string email, string password, int roleId, string language);
        Task<bool> IsEmailExists(string email);
        Task<bool> Authenticate(string email, string password);

        Task<bool> RequestPasswordReset(string email);
        Task<bool> IsPasswordResetRequestValid(string email, string token);
        Task<bool> ChangePassword(string email, string token, string password);

        Task<bool> ChangeStatus(long userId, long updatedBy, bool isActive);

        Task<UserDto> Get(long id);
        Task<UserDto> GetByEmail(string email);

        Task<PagedList<UserDto>> GetUsers(int pageNumber);
    }
}