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
            public async void new_should_redirect_if_model_is_valid()
            {
                // Arrange
                const string actionName = "New";
                var validModel = new UserModel { Id = 1, FullName = "test", Password = "test", Email = "test@test.com", Language = Thread.CurrentThread.CurrentUICulture.Name };

                var userService = new Mock<IUserService>();
                userService.Setup(x => x.Create(validModel.FullName, validModel.Email, validModel.Password, SetRole.User.Value, validModel.Language))
                           .Returns(Task.FromResult<long?>(1));

                var formsAuthenticationService = new Mock<IFormsAuthenticationService>();
                formsAuthenticationService.Setup(x => x.SignIn(validModel.Id, validModel.FullName, validModel.Email, SetRole.User.Value, true));

                // Act
                var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                     .WithFormsAuthenticationService(formsAuthenticationService.Object)
                                                     .Build();
                var view = await sut.New(validModel) as RedirectResult;

                // Assert
                Assert.NotNull(view);
                Assert.AreEqual(view.Url, "/user/detail");
                Assert.IsInstanceOf(typeof(BaseController), sut);

                sut.AssertPostAttribute(actionName, new[] { typeof(UserModel) });
                sut.AssertAllowAnonymousAttribute(actionName, new[] { typeof(UserModel) });

                userService.Verify(x => x.Create(validModel.FullName, validModel.Email, validModel.Password, SetRole.User.Value, validModel.Language), Times.Once);
                formsAuthenticationService.Verify(x => x.SignIn(validModel.Id, validModel.FullName, validModel.Email, SetRole.User.Value, true), Times.Once);
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
            public async void login_should_redirect_if_model_is_valid()
            {
                // Arrange
                const string actionName = "Login";
                const int id = 1;
                const string email = "test@test.com";
                const string fullName = "test";
                const string password = "pass";
                var validModel = new LoginModel { Password = password, Email = email };

                var userService = new Mock<IUserService>();
                userService.Setup(x => x.Authenticate(email, password))
                        .Returns(() => Task.FromResult(true));
                userService.Setup(x => x.GetByEmail(email))
                        .Returns(() => Task.FromResult(new User { Email = email }));

                var formsAuthenticationService = new Mock<IFormsAuthenticationService>();
                formsAuthenticationService.Setup(x => x.SignIn(id, fullName, email, SetRole.User.Value, true));

                // Act
                var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                     .WithFormsAuthenticationService(formsAuthenticationService.Object)
                                                     .Build();
                var view = await sut.Login(validModel) as RedirectResult;

                // Assert
                Assert.NotNull(view);
                Assert.AreEqual(view.Url, "/home/index");
                Assert.IsInstanceOf(typeof(BaseController), sut);

                userService.Verify(x => x.Authenticate(email, password), Times.Once);
                formsAuthenticationService.Verify(x => x.SignIn(id, fullName, email, SetRole.User.Value, true), Times.Once);

                sut.AssertPostAttribute(actionName, new[] { typeof(LoginModel) });
                sut.AssertAllowAnonymousAttribute(actionName, new[] { typeof(LoginModel) });
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
                var task = sut.PasswordChange("email@email.com", "token");
                task.Wait();
                var view = task.Result as ViewResult;

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                sut.AssertGetAttribute(actionName, new[] { typeof(string), typeof(string) });
                sut.AssertAllowAnonymousAttribute(actionName, new[] { typeof(string), typeof(string) });
            }

            [Test]
            public async void password_change_should_return_with_password_change_model_if_model_is_valid()
            {
                // Arrange
                const string actionName = "PasswordChange";
                const string email = "test@test.com";
                const string token = "token";
                const string password = "pass";

                var validModel = new PasswordChangeModel { Email = email, Password = password, Token = token };

                var userService = new Mock<IUserService>();
                userService.Setup(x => x.IsPasswordResetRequestValid(email, token))
                           .Returns(() => Task.FromResult(true));

                userService.Setup(x => x.ChangePassword(email, token, password))
                           .Returns(() => Task.FromResult(true));

                var formsAuthenticationService = new Mock<IFormsAuthenticationService>();
                formsAuthenticationService.Setup(x => x.SignOut());

                // Act
                var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                     .WithFormsAuthenticationService(formsAuthenticationService.Object)
                                                     .BuildWithMockControllerContext();

                var view = await sut.PasswordChange(validModel) as ViewResult;

                // Assert
                Assert.NotNull(view);
                Assert.NotNull(view.Model);
                Assert.IsInstanceOf<BaseController>(sut);

                userService.Verify(x => x.IsPasswordResetRequestValid(email, token), Times.Once);
                userService.Verify(x => x.ChangePassword(email, token, password), Times.Once);
                formsAuthenticationService.Verify(x => x.SignOut(), Times.Once);

                sut.AssertPostAttribute(actionName, new[] { typeof(PasswordChangeModel) });
                sut.AssertAllowAnonymousAttribute(actionName, new[] { typeof(PasswordChangeModel) });
            }
        }
    }
}