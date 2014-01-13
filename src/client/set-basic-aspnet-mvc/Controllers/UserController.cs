using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.DataTransferObjects;
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
            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            model.Language = Thread.CurrentThread.CurrentUICulture.Name;

            var userId = await _userService.Create(model.FullName, model.Email, model.Password, SetRole.User.Value, model.Language);
            if (userId == null)
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            _formsAuthenticationService.SignIn(userId.Value, model.FullName, model.Email, SetRole.User.Value, true);

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
                SetPleaseTryAgain(model);
                return View(model);
            }

            var isAuthenticated = await _userService.Authenticate(model.Email, model.Password);
            if (!isAuthenticated)
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            var user = await _userService.GetByEmail(model.Email);

            _formsAuthenticationService.SignIn(user.Id, user.FullName, user.Email, user.RoleId, true);

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
        public async Task<ViewResult> PasswordReset(PasswordResetModel model)
        {
            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            var isValid = await _userService.RequestPasswordReset(model.Email);
            if (!isValid)
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            model.Msg = LocalizationStringHtmlHelper.LocalizationString("password_reset_link_sent_to_your_email");
            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> PasswordChange(string email, string token)
        {
            var isValid = await _userService.IsPasswordResetRequestValid(email, token);
            if (!isValid) return RedirectToHome();

            var model = new PasswordChangeModel();
            model.Email = email;
            model.Token = token;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> PasswordChange(PasswordChangeModel model)
        {
            if (!model.IsValid())
            {
                return RedirectToHome();
            }

            var isValid = await _userService.IsPasswordResetRequestValid(model.Email, model.Token);
            if (!isValid) return RedirectToHome();

            isValid = await _userService.ChangePassword(model.Email, model.Token, model.Password);
            if (!isValid) return RedirectToHome();

            if (User.Identity.IsAuthenticated)
            {
                _formsAuthenticationService.SignOut();
            }

            model.Msg = LocalizationStringHtmlHelper.LocalizationString("password_reset_successfull");

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(long id = 0)
        {
            if (id < 1)
            {
                id = User.Identity.GetUserId();
            }

            var user = await _userService.Get(id);
            if (user == null) return RedirectToHome();

            var model = Mapper.Map<UserDto, UserModel>(user);
            return View(model);
        }
    }
}