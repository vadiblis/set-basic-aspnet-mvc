using NUnit.Framework;
using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Helpers;
using set_basic_aspnet_mvc.test.TestHelpers;

namespace set_basic_aspnet_mvc.test.Controllers
{
    public class AdminControllerTests 
    {
        [Test]
        public void index_should_return_view()
        {
            // Arrange
            const string actionName = "Index";

            // Act
            var sut = new AdminController();
            var view = sut.Index();

            // Assert
            Assert.NotNull(view);
            Assert.IsInstanceOf<BaseController>(sut);

            sut.AssertGetAttribute(actionName); 
        }

        [Test]
        public void users_should_return_view()
        {
            // Arrange
            const string actionName = "Users";
            const int id = 1;

            // Act
            var sut = new AdminController();
            var view = sut.Users(id);

            // Assert
            Assert.NotNull(view);
            Assert.IsInstanceOf<BaseController>(sut);

            sut.AssertGetAttribute(actionName,new []{typeof(int)});
        }
    }
}