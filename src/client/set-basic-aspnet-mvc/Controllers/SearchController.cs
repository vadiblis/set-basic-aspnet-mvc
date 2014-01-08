using System.Threading.Tasks;
using System.Web.Mvc;

using set_basic_aspnet_mvc.Domain.Services;
using set_basic_aspnet_mvc.Models;

namespace set_basic_aspnet_mvc.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
          
        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> Query(string text)
        {
            var model = new ResponseModel { IsOk = false };
            if (string.IsNullOrEmpty(text))
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            var result = await _searchService.Query(text);
            if (result == null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            model.Result = result;
            model.IsOk = true;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}