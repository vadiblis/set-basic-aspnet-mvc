using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcPaging;
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

        private const int DefaultFeedbacksPageSize = 10;

        public AdminController(IUserService userService, IFeedbackService feedbackService)
        {
            _userService = userService;
            _feedbackService = feedbackService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //temporarty commented because no admin user exists

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

        //temporary allowanonymous
        [HttpGet, AllowAnonymous]
        public ActionResult Feedbacks(string feedbackFilter, int? page = 1)
        {
            ViewData["feedbackFilter"] = feedbackFilter;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            var model = fillRandomFeedbackModelsData();

            //filter must be implemented
            /*if (string.IsNullOrWhiteSpace(feedbackFilter))
            {
                model = model.ToPagedList(currentPageIndex, DefaultFeedbacksPageSize);
            }
            else
            {
                model = model.Where(p => p.Reviewed.HasValue).ToPagedList(currentPageIndex, DefaultFeedbacksPageSize);    
            }*/

            var result = model.ToPagedList(currentPageIndex, DefaultFeedbacksPageSize);
            
            return View(result);
        }
        
        private IEnumerable<FeedbackModel> fillRandomFeedbackModelsData()
        {
            for (int i = 0; i < 200; i++)
            {
                yield return
                    new FeedbackModel()
                    {
                        Id = i,
                        Info = String.Format("info number {0}", i),
                        UserEmail = "mx@email.com",
                        UserId = i
                    };
            }
        }
    }
}