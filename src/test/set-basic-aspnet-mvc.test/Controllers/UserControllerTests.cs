using Moq;
using NUnit.Framework;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.test.TestHelpers;
using set_basic_aspnet_mvc.test.Builders;

namespace set_basic_aspnet_mvc.test.Controllers
{
    public class UserControllerTests
    {
        [TestFixture]
        public class HomeControllerTests
        {
            [Test]
            public void new_should_return_with_login_model()
            {
                // Arrange
                const string actionName = "New";

                // Act
                var sut = new UserControllerBuilder().Build();
                var view = sut.New();

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName);
                sut.AssertAllowAnonymousAttribute(actionName);
            }

            [Test]
            public void login_should_return_with_login_model()
            {
                // Arrange
                const string actionName = "Login";

                // Act
                var sut = new UserControllerBuilder().Build();
                var view = sut.Login();

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName);
                sut.AssertAllowAnonymousAttribute(actionName);
            }

            [Test]
            public void logout_should_return_view()
            {
                // Arrange
                const string actionName = "Logout";
                var formsAuthenticationService = new Mock<IFormsAuthenticationService>();
                formsAuthenticationService.Setup(x => x.SignOut());

                // Act
                var sut = new UserControllerBuilder().WithFormsAuthenticationService(formsAuthenticationService.Object)
                                                     .Build();
                var view = sut.Logout();

                // Assert
                Assert.NotNull(view);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName);
            }

            [Test]
            public void password_reset_should_return_with_password_reset_model()
            {
                // Arrange
                const string actionName = "PasswordReset";

                // Act
                var sut = new UserControllerBuilder().Build();
                var view = sut.PasswordReset();

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName);
                sut.AssertAllowAnonymousAttribute(actionName);
            }

            [Test]
            public void password_change_should_return_with_password_change_model()
            {
                // Arrange
                const string actionName = "PasswordChange";

                // Act
                var sut = new UserControllerBuilder().Build();
                var view = sut.PasswordChange("email@email.com", "token");

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName, new[] { typeof(string), typeof(string) });
                sut.AssertAllowAnonymousAttribute(actionName, new[] { typeof(string), typeof(string) });
            }

        }
    }
}