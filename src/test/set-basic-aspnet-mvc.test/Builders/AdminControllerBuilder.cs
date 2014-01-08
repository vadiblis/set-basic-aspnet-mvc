using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Controllers;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class AdminControllerBuilder
    {
        public AdminControllerBuilder()
        {

        }

        internal static AdminController Build()
        {
            return new AdminController();
        }
    }   
}
