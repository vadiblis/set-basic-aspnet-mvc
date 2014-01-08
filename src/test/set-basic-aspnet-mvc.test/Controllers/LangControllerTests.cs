using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;
using set_basic_aspnet_mvc.test.TestHelpers;
using set_basic_aspnet_mvc.test.Builders;
using System.Web;


namespace set_basic_aspnet_mvc.test.Controllers
{
    public class LangControllerTests
    {
        [Test]
        public void change_should_add_lang_cookie()
        {
            // Arrange      
            var httpResponse = new Mock<HttpResponseBase>();

            // Act
            var sut = new LangControllerBuilder().BuildWithMockControllerContext(httpResponse);            
            var view = sut.Change("tr");

            // Assert
            Assert.NotNull(view);

            sut.AssertGetAttribute("Change", new[] { typeof(string) });
            sut.AssertAllowAnonymousAttribute("Change", new[] { typeof(string) });

            httpResponse.Verify(x => x.SetCookie(It.IsAny<HttpCookie>()), Times.AtLeastOnce);
        }
    }
}
