using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Helpers;
using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.GetUserRoleId() != SetRole.Admin.Value)
            {
                filterContext.Result = RedirectToHome();
            }

            base.OnActionExecuting(filterContext);
        }

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