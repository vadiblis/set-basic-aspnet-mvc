using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Domain.Services;

namespace set_basic_aspnet_mvc.test.Builders
{
    class SearchServiceBuilder
    {
        private IRepository<User> _userService;

        public SearchServiceBuilder()
        {
            _userService = null;
        }

        internal SearchServiceBuilder WithUserService(IRepository<User> userService)
        {
            _userService = userService;
            return this;
        }

        internal SearchService Build()
        {
            return new SearchService(_userService);
        }
    }
}
