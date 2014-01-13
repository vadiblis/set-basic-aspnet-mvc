using set_basic_aspnet_mvc.Controllers;
using set_basic_aspnet_mvc.Domain.Contracts;

namespace set_basic_aspnet_mvc.test.Builders
{
    public class SearchControllerBuilder
    { 
        private ISearchService _searchService;
         
        public SearchControllerBuilder()
        { 
            _searchService = null;
        }
          
        internal SearchControllerBuilder WithSearchService(ISearchService searchService)
        {
            _searchService = searchService;
            return this;
        }

        internal SearchController Build()
        {
            return new SearchController(_searchService);
        }
    }
}