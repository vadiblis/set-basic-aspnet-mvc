using System.Web.Mvc;

using set_basic_aspnet_mvc.Models;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Controllers
{
    public class BaseController : Controller
    {
        public RedirectResult RedirectToHome()
        {
            return Redirect("/");
        }
        
        public void SetPleaseTryAgain(BaseModel model)
        {
            model.Msg = LocalizationStringHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");  
        }
    }
}