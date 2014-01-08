using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;
using set_basic_aspnet_mvc.Helpers;
using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Controllers
{
    public class UserController : BaseController
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IUserService _userService;

        public UserController(
            IUserService userService, 
            IFormsAuthenticationService formsAuthenticationService)
        {
            _formsAuthenticationService = formsAuthenticationService;
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public ViewResult New()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> New(UserModel model)
        {
            if (!model.IsValidNewUser())
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }

            model.Language = Thread.CurrentThread.CurrentUICulture.Name;
            
            var userId = await _userService.Create(model.FullName, model.Email, model.Password, SetRole.User.Value, model.Language);
            if (userId == null)
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }

            _formsAuthenticationService.SignIn(userId, model.FullName, model.Email, true);

            return Redirect("/user/detail");
        }
        
        [HttpGet, AllowAnonymous]
        public ViewResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!model.IsValid())
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }

            var isAuthenticated = await _userService.Authenticate(model.Email, model.Password);
            if (!isAuthenticated)
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }

            var user = await _userService.GetByEmail(model.Email);
            
            _formsAuthenticationService.SignIn(user.Id, user.FullName, user.Email, true);

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToHome();
        }

        [HttpGet]
        public RedirectResult Logout()
        {
            _formsAuthenticationService.SignOut();
            return RedirectToHome();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordReset()
        {
            var model = new PasswordResetModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ViewResult PasswordReset(PasswordResetModel model)
        {

            if (!model.IsValid())
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }
            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public ViewResult PasswordChange(string email, string token)
        {
            var model = new PasswordChangeModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> PasswordChange(PasswordChangeModel model)
        {

            if (!model.IsValid())
            {
                model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
                return View(model);
            }
         return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            if (id < 1)
            {
                id = User.Identity.GetUserId();
            }

            var user = await _userService.Get(id);
            if (user == null)
            {
                return RedirectToHome();
            }

            var model = new UserModel 
                        {
                            FullName= user.FullName,
                            Email = user.Email,
                            IsActive = user.IsActive,
                            RoleName = user.RoleName,
                            Language = user.Language
                        };            

            return View(model);
        }
    }
}