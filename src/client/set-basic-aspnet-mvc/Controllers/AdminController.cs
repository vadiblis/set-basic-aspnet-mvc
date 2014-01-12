using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Helpers;
using set_basic_aspnet_mvc.Domain.Entities;
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
        public ViewResult Users(int id = 1)
        {
            var page = id;



            return View();
        }

        //todo:temporary allowanonymous
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> Feedbacks(int id = 0, int lastId = 0)
        {
            var page = id;

            var items = await _feedbackService.GetFeedbacks(lastId, page);

            var model = new List<FeedbackModel>();
            foreach (var item in items.Items)
            {
                model.Add(FeedbackModel.Map(item));
            }
            
            return View(model);
        }
    }
}