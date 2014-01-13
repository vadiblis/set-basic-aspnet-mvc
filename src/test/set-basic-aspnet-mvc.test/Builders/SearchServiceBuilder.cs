using Moq;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    class SearchServiceBuilder
    {
        private IRepository<User> _userRepository;

        public SearchServiceBuilder()
        {
            _userRepository = null;
        }

        internal SearchServiceBuilder WithUserRespository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        internal SearchService Build()
        {
            return new SearchService(_userRepository);
        }
    }
}
