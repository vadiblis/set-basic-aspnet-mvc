using System.Web.Mvc;

namespace set_basic_aspnet_mvc.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public ViewResult Index()
        {
            return View();
        }
    }
}