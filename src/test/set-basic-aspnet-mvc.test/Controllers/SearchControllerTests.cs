using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;
using set_basic_aspnet_mvc.test.Builders;
using set_basic_aspnet_mvc.test.TestHelpers;

namespace set_basic_aspnet_mvc.test.Controllers
{
    public class SearchControllerTests
    {
        [Test]
        public async void query_should_return_with_response_model()
        {
            // Arrange
            var searchService = new Mock<ISearchService>(); 

            searchService.Setup(x => x.Query("test")).Returns(Task.FromResult(new List<SearchResultDto>())); 

            // Act
            var sut = new SearchControllerBuilder().WithSearchService(searchService.Object)
                                                   .Build();
            var result = await sut.Query("test");

            // Assert
            Assert.NotNull(result); 
            var model = result.Data;
            Assert.NotNull(model);
            Assert.IsAssignableFrom(typeof(JsonResult), result);
            Assert.IsAssignableFrom(typeof(ResponseModel), model);
            Assert.IsInstanceOf<BaseController>(sut);

            searchService.Verify(x => x.Query("test"), Times.Once);

            sut.AssertAllowAnonymousAttribute("Query", new[] { typeof(string) });
            sut.AssertGetAttribute("Query", new[] { typeof(string) }); 
        } 
    }
}