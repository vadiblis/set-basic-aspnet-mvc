using System;
using System.Linq;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IReportService
    {
        Task<int> GetTotalUserCount();
    }

    public class ReportService : IReportService
    {
        private readonly IRepository<User> _userRepository;

        public ReportService(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> GetTotalUserCount()
        {
            //var totalUserCount = _userRepository.FindAll(x => x.RoleId != SetRole.User.Value).Count();
            var totalUserCount = 5;

            return Task.FromResult(totalUserCount);
        }
    }
}