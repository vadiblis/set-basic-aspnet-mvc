using System.Web;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Controllers
{
    public class LangController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public ActionResult Change(string id)
        {
            Response.SetCookie(new HttpCookie(ConstHelper.__SetLang, id));

            return HttpContext.Request.UrlReferrer != null ? Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri) : RedirectToHome();
        }
    }
}