using System;
using System.Linq;
using System.Linq.Expressions;

using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.test.Builders;
using System.Collections.Generic;

namespace set_basic_aspnet_mvc.test.Services
{
    class SearchServiceTests
    {
        [Test]
        public async void should_query_if_query_string_is_empty()
        {
            // Arrange
            const string key = "test";
            var userList = new List<User> { new User { Email = "test@test.com", FullName = "test" } };

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<User, bool>>>()))
                          .Returns(userList.AsQueryable());

            userRepository.Setup(x => x.AsQueryable(userList.AsQueryable(), It.IsAny<Expression<Func<User, bool>>>()))
                          .Returns(userList.AsQueryable());

            // Act
            var sut = new SearchServiceBuilder().WithUserRespository(userRepository.Object)
                                                .Build();

            var nullTask = await sut.Query(string.Empty);
            var notNullResult = await sut.Query(key); 

            // Assert
            Assert.IsNull(nullTask);
            Assert.NotNull(notNullResult);
            Assert.IsInstanceOf(typeof(ISearchService), sut);
             
            userRepository.Verify(x => x.FindAll(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
        }
         
    }
}
