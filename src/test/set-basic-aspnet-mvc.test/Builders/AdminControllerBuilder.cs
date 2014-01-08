using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Services;
using Moq;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class AdminControllerBuilder
    {
        private IFormsAuthenticationService _formAuthenticationService;
        private IUserService _userService;

        public AdminControllerBuilder()
        {
            _formAuthenticationService = null;
            _userService = new Mock<IUserService>().Object;
        }

        internal AdminControllerBuilder WithFormsAuthenticationService(IFormsAuthenticationService formAuthenticationService)
        {
            _formAuthenticationService = formAuthenticationService;
            return this;
        }

        internal AdminControllerBuilder WithUserService(IUserService userService)
        {
            _userService = userService;
            return this;
        }

        internal AdminController Build()
        {
            return new AdminController(_userService);
        }
    }
}
