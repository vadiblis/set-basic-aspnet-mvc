using System.Web.Mvc;

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