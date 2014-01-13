using Moq;

using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Contracts;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class AdminControllerBuilder
    {
        private IFormsAuthenticationService _formAuthenticationService;
        private IUserService _userService;
        private IFeedbackService _feedbackService;

        public AdminControllerBuilder()
        {
            _formAuthenticationService = null;
            _userService = new Mock<IUserService>().Object;
            _feedbackService = new Mock<IFeedbackService>().Object;
        }

        internal AdminControllerBuilder WithFormsAuthenticationService(IFormsAuthenticationService formAuthenticationService)
        {
            _formAuthenticationService = formAuthenticationService;
            return this;
        }

        internal AdminControllerBuilder WithUserService(IUserService userService)
        {
            _userService = userService;
            return this;
        }

        internal AdminControllerBuilder WithFeedbacService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
            return this;
        }

        internal AdminController Build()
        {
            return new AdminController(_userService, _feedbackService);
        }
    }
}
