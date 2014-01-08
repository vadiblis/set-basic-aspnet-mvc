using System;
using System.Linq.Expressions;
using System.Threading;

using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void create_should_return_user_id()
        {
            // Arrange
            var user = new User { FullName = "test", Email = "test@test.com", RoleId = SetRole.User.Value, Language = Thread.CurrentThread.CurrentUICulture.Name };
            var userRepository = new Mock<IRepository<User>>();

            userRepository.Setup(x => x.Create(user)).Returns(user);
            userRepository.Setup(x => x.SaveChanges()).Returns(true);

            // Act
            var userService = new UserService(userRepository.Object);
            var userId = userService.Create(user.FullName, user.Email, "password", user.RoleId, user.Language);

            // Assert
            userRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);

            Assert.NotNull(userId);
        }

        [Test]
        public void authenticate_should_return_user_result()
        {
            // Arrange
            var user = new User { FullName = "test", Email = "test@test.com", RoleId = SetRole.User.Value, Language = Thread.CurrentThread.CurrentUICulture.Name };
            var userRepository = new Mock<IRepository<User>>();

            // Act
            var userService = new UserService(userRepository.Object);
            var userId = userService.Create(user.FullName, user.Email, "password", user.RoleId, user.Language);

            // Assert
            userRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);

            Assert.NotNull(userId);
        }

        [Test]
        public async void get_user_by_email_should_return_user()
        {
            // Arrange
            const string email = "test@test.com";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Email = email });

            // Act
            var userService = new UserService(userRepository.Object);
            var user = await userService.GetByEmail(email);

            // Assert
            Assert.NotNull(user);
            Assert.AreEqual(user.Email, email);
            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
        [Test]
        public async void change_status_test()
        {
            // Arrange
            const int userId = 8;
            const int updatedBy = 1;
            bool isActive = true;
            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User {Id= userId});

            // Act
            var userService = new UserService(userRepository.Object);
            var user = userService.ChangeStatus(userId,updatedBy,isActive);

            // Assert
            Assert.NotNull(user);
            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
}
