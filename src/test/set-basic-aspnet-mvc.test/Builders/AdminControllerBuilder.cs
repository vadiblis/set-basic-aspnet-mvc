using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class AdminControllerBuilder
    {
       private IFormsAuthenticationService _formAuthenticationService;

       public AdminControllerBuilder()
        {
            _formAuthenticationService = null;
        }

       internal AdminControllerBuilder WithFormsAuthenticationService(IFormsAuthenticationService formAuthenticationService)
        {
            _formAuthenticationService = formAuthenticationService;
            return this;
        }

        internal AdminController Build()
        {
            return new AdminController();
        }
    }
}
