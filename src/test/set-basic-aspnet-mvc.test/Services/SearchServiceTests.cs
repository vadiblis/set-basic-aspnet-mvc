using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.test.Builders;

namespace set_basic_aspnet_mvc.test.Services
{
    class SearchServiceTests
    {
        [Test]
        public async void should_get_user_by_email()
        {
            // Arrange
            const string email = "test@test.com";

            var userRepository = new Mock<IRepository<User>>();
            var queryableVal = new Mock<IQueryable<User>>();

            userRepository.Setup(x => x.AsQueryable()).Returns(queryableVal.Object);
            //queryableVal.Setup(
            //    x => x.Where(user1 => user1.FullName.Contains(It.IsAny<string>()) || user1.Email == It.IsAny<string>()))
            //    .Returns(queryableVal.Object);
            //queryableVal.Setup(
            //    x => x.Where(It.IsAny<Expression<Func<User, bool>>>()))
            //    .Returns(queryableVal.Object);
             

            // Act
            var sut = new SearchServiceBuilder().WithUserService(userRepository.Object)
                                                .Build();

            var user = await sut.Query("test");
             
            // Assert
            //Assert.NotNull(user);
            //Assert.AreEqual(user.Email, email);
            //userRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
}
