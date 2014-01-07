using System.Web.Mvc;

namespace set_basic_aspnet_mvc.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Users(int id = 1)
        {
            var page = id;

            return View();
        }
    }
}