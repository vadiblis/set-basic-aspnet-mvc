using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.Controllers
{
    public class BaseController : Controller
    {
        public RedirectResult RedirectToHome()
        {
            return Redirect("/home/index");
        }
    }
}