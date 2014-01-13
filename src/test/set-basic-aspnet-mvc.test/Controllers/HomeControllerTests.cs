using NUnit.Framework;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.test.TestHelpers;

namespace set_basic_aspnet_mvc.test.Controllers
{
    [TestFixture]
    public class HomeControllerTests : BaseTests
    {
        [Test]
        public void index_should_return_view()
        {
            // Arrange
            const string actionName = "Index";

            // Act
            var sut = new HomeController();
            var view = sut.Index();

            // Assert
            Assert.NotNull(view);
            Assert.IsInstanceOf<BaseController>(sut);

            sut.AssertGetAttribute(actionName);
            sut.AssertAllowAnonymousAttribute(actionName);
        }
    }
}