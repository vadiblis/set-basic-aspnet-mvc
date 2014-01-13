using System;
using System.Linq.Expressions;
using System.Threading;

using Moq;
using NUnit.Framework;
using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.test.Builders;

namespace set_basic_aspnet_mvc.test.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async void create_should_return_user_id()
        {
            // Arrange
            var user = new User { FullName = "test", Email = "test@test.com", RoleId = SetRole.User.Value, Language = Thread.CurrentThread.CurrentUICulture.Name };
            var userRepository = new Mock<IRepository<User>>();

            userRepository.Setup(x => x.Create(user)).Returns(user);
            userRepository.Setup(x => x.SaveChanges()).Returns(true);

            // Act 
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build(); 
            var userId = await sut.Create(user.FullName, user.Email, "password", user.RoleId, user.Language);

            // Assert
            Assert.NotNull(userId);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<long>(userId);

            userRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);

        }

        [Test]
        public async void authenticate_should_return_with_bool()
        {
            // Arrange 
            var user = new User { FullName = "test", Email = "test@test.com", RoleId = SetRole.User.Value, PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), Language = Thread.CurrentThread.CurrentUICulture.Name };
            var userRepository = new Mock<IRepository<User>>(); 
            
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(user); 
            userRepository.Setup(x => x.Update(user)).Returns(user);

            // Act
            var  sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build();
            var result = await sut.Authenticate(user.Email, user.PasswordHash);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result);

            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once); 
            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce); 
        }

        [Test]
        public async void get_user_by_email_should_return_with_user()
        {
            // Arrange
            const string email = "test@test.com";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Email = email });

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build();
            var user = await sut.GetByEmail(email);

            // Assert
            Assert.NotNull(user);
            Assert.AreEqual(user.Email, email);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<User>(user);

            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public async void change_status_should_return_with_bool()
        {
            // Arrange
            const long userId = 8;
            const long updatedBy = 1;
            const bool isActive = true;
            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Id = userId });

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build();
            var result = await sut.ChangeStatus(userId, updatedBy, isActive);

            // Assert
            Assert.NotNull(result); 
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result);

            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);  
        }

        [Test]
        public async void get_should_return_user()
        {
            // Arrange
            const long id = 2;

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Id = id });

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build();
            var user = await sut.Get(id);

            // Assert
            Assert.NotNull(user);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<User>(user);

            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public async void change_password_return_with_bool()
        {
            // Arrange
            const string email = "test@test.com";
            const string token = "test";
            const string password = "password";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Email = email, PasswordResetToken = token, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), PasswordResetRequestedAt = DateTime.Now });

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                              .Build();
            var result = await sut.ChangePassword(email, token, password);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result);

            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);
            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
        }

        [Test]
        public async void request_password_reset_return_with_bool()
        {
            // Arrange
            const string email = "test@test.com";
            const string token = "test";
            const long userId = 1;            

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()))
                          .Returns(new User { Email = email, PasswordResetToken = token, PasswordResetRequestedAt = null, UpdatedAt = DateTime.Now, UpdatedBy = userId });

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                             .Build();
            var result = await sut.RequestPasswordReset(email);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result);

            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.SaveChanges(), Times.AtLeastOnce);
            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
        }

        [Test]
        public async void is_password_reset_request_valid_return_with_bool()
        {
            // Arrange
            const string email = "test@test.com";
            const string token = "test";

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Email = email, PasswordResetToken = token});

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                             .Build();
            var result = await sut.IsPasswordResetRequestValid(email,token);

            // Assert
            Assert.NotNull(result); 
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result);
            userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce); 
        }

        [Test]
        public async void is_email_exists_return_with_bool()
        {
            // Arrange
            const string email ="test@test.com"; 
            var userRepository = new Mock<IRepository<User>>();

            // Act
            var sut = new UserServiceBuilder().WithUserRespository(userRepository.Object)
                                             .Build();
            var result = await sut.IsEmailExists(email);

            // Assert

            Assert.NotNull(result);
            Assert.IsInstanceOf<IUserService>(sut);
            Assert.IsAssignableFrom<bool>(result); 
        }
    }
}
