using Moq;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    class UserServiceBuilder
    {
        private IRepository<User> _userRepository;

        public UserServiceBuilder()
        {
            _userRepository = new Mock<IRepository<User>>().Object;
        }

        internal UserServiceBuilder WithUserRespository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
          
        internal UserService Build()
        {
            return new UserService(_userRepository);
        }
    }
}