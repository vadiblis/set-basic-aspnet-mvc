using System;
using System.Linq.Expressions;

using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Services
{
    [TestFixture]
    public class ReportServiceTests
    {
        [Test]
        public async void get_total_user_count()
        {
            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<User, bool>>>()));

            // Act
            var reportService = new ReportService(userRepository.Object);
            var userCount = await reportService.GetTotalUserCount();

            // Assert
            Assert.NotNull(userCount);
            //userRepository.Verify(x => x.FindAll(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }
    }
}
