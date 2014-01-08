using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.Controllers
{
    public class BaseController : Controller
    {
        public IFormsAuthenticationService FormsAuthenticationService { get; set; }
        public IUserService UserService { get; set; }

        public RedirectResult RedirectToHome()
        {
            return Redirect("/home/index");
        }
    }
}