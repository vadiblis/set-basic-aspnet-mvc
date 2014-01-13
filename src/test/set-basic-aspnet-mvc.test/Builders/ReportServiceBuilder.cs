using Moq;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    class ReportServiceBuilder
    {
        private IRepository<User> _userRepository;

        public ReportServiceBuilder()
        {
            _userRepository = null;
        }

        internal ReportServiceBuilder WithUserRespository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        internal ReportService Build()
        {
            return new ReportService(_userRepository);
        }
    }
}