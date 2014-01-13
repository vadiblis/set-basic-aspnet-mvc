using Moq;
<<<<<<< HEAD
=======

>>>>>>> 80ec4d364ee458aa42e0cb4ca3f6ae3621495659
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
<<<<<<< HEAD
    class UserServiceBuilder
=======
    public class UserServiceBuilder
>>>>>>> 80ec4d364ee458aa42e0cb4ca3f6ae3621495659
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
<<<<<<< HEAD
          
=======

>>>>>>> 80ec4d364ee458aa42e0cb4ca3f6ae3621495659
        internal UserService Build()
        {
            return new UserService(_userRepository);
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 80ec4d364ee458aa42e0cb4ca3f6ae3621495659
