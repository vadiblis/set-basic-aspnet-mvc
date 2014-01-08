using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using Moq;

using set_basic_aspnet_mvc.Controllers;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class LangControllerBuilder
    {
        internal LangController BuildWithMockControllerContext(Mock<HttpResponseBase> httpResponse)
        {
            var controllerContext = new Mock<ControllerContext>();

            var httpContext = new Mock<HttpContextBase>();
            var httpRequest = new Mock<HttpRequestBase>();            

            var user = new Mock<IPrincipal>();
            var currentUser = new Mock<IIdentity>();

            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);

            httpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            httpContext.Setup(x => x.User).Returns(user.Object);

            user.Setup(x => x.Identity).Returns(currentUser.Object);
            currentUser.Setup(x => x.IsAuthenticated).Returns(true);

            var sut = Build();
            sut.ControllerContext = controllerContext.Object;
            return sut;
        }

        internal LangController Build()
        {
            return new LangController();
        }        
    }
}
