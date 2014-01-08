using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Moq;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class UserControllerBuilder
    {
        private IFormsAuthenticationService _formAuthenticationService;
        private IUserService _userService;

        public UserControllerBuilder()
        {
            _formAuthenticationService = null;
            _userService = null;
        }

        internal UserControllerBuilder WithFormsAuthenticationService(IFormsAuthenticationService formAuthenticationService)
        {
            _formAuthenticationService = formAuthenticationService;
            return this;
        }

        internal UserControllerBuilder WithUserService(IUserService userService)
        {
            _userService = userService;
            return this;
        }

        internal UserController BuildWithMockControllerContext()
        {
            var sut = Build();

            var controllerContext = new Mock<ControllerContext>();
            var httpContext = new Mock<HttpContextBase>();
            var httpRequest = new Mock<HttpRequestBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            var user = new Mock<IPrincipal>();
            var currentUser = new Mock<IIdentity>();

            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);            
            httpContext.Setup(x => x.User).Returns(user.Object);
            user.Setup(x => x.Identity).Returns(currentUser.Object);
            currentUser.Setup(x => x.IsAuthenticated).Returns(true);

            httpResponse.Setup(x => x.SetCookie(It.IsAny<HttpCookie>()));

            sut.ControllerContext = controllerContext.Object;
            return sut;
        }

        internal UserController Build()
        {
            return new UserController(_userService, _formAuthenticationService);
        }
    }
}
