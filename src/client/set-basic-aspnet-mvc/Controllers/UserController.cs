using System.Threading.Tasks;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;

namespace set_basic_aspnet_mvc.Controllers
{
    public class UserController : BaseController
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public UserController(IFormsAuthenticationService formsAuthenticationService)
        {
            _formsAuthenticationService = formsAuthenticationService;
        }

        [HttpGet, AllowAnonymous]
        public ViewResult New()
        {
            return View(new LoginModel());
        }

        [HttpGet, AllowAnonymous]
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpGet]
        public ViewResult Logout()
        {
            _formsAuthenticationService.SignOut(); 
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordReset()
        {
            return View(new PasswordResetModel());
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordChange(string email, string token)
        {
            return View(new PasswordChangeModel());
        }

        [HttpGet]
        public ViewResult Detail()
        {
            return View();
        }
    }
}