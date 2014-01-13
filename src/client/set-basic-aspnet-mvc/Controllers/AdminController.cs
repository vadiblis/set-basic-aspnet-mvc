using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using AutoMapper;

using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.DataTransferObjects;
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
        public async Task<ViewResult> Users(int id = 0)
        {
            var page = id;

            var items = await _userService.GetUsers(page);
            var list = items.Items.Select(Mapper.Map<UserDto, UserModel>).ToList();

            var model = new PageModel<UserModel>
            {
                Items = list,
                HasNextPage = items.HasNextPage,
                HasPreviousPage = items.HasPreviousPage,
                Number = items.Number,
                TotalCount = items.TotalCount,
                TotalPageCount = items.TotalPageCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Feedbacks(int id = 0)
        {
            var page = id;

            var items = await _feedbackService.GetFeedbacks(page);
            var list = items.Items.Select(Mapper.Map<FeedbackDto, FeedbackModel>).ToList();

            var model = new PageModel<FeedbackModel>
            {
                Items = list,
                HasNextPage = items.HasNextPage,
                HasPreviousPage = items.HasPreviousPage,
                Number = items.Number,
                TotalCount = items.TotalCount,
                TotalPageCount = items.TotalPageCount
            };

            return View(model);
        }
    }
}