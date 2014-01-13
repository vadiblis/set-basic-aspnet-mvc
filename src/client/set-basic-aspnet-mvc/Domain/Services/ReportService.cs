using System.Linq;
using System.Threading.Tasks;
using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<User> _userRepository;

        public ReportService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> GetTotalUserCount()
        {
            var totalUserCount = _userRepository.AsQueryable(null, x => x.RoleId != SetRole.User.Value).Count();
            return Task.FromResult(totalUserCount);
        }
    }
}