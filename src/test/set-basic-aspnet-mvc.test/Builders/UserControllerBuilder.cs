using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class UserControllerBuilder
    {
        private IFormsAuthenticationService _formAuthenticationService;

        public UserControllerBuilder()
        {
            _formAuthenticationService = null;
        }

        internal UserControllerBuilder WithFormsAuthenticationService(IFormsAuthenticationService formAuthenticationService)
        {
            _formAuthenticationService = formAuthenticationService;
            return this;
        }

        internal UserController Build()
        {
            return new UserController(_formAuthenticationService);
        }
    }
}
