using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;

namespace set_basic_aspnet_mvc.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public AdminController(IUserService userService, IFeedbackService feedbackService)
        {
            _userService = userService;
            _feedbackService = feedbackService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //todo:temporarty commented because no admin user exists
            /*if (User.Identity.GetUserRoleId() != SetRole.Admin.Value)
            {
                filterContext.Result = RedirectToHome();
            }*/

            base.OnActionExecuting(filterContext);
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ViewResult> Users(int id = 0, int lastId = 0)
        {
            var page = id;

            var items = await _userService.GetUsers(lastId, page);
            var model = items.Items.Select(UserModel.Map).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Feedbacks(int id = 0, int lastId = 0)
        {
            var page = id;

            var items = await _feedbackService.GetFeedbacks(lastId, page);
            var model = items.Items.Select(FeedbackModel.Map).ToList();

            return View(model);
        }
    }
}