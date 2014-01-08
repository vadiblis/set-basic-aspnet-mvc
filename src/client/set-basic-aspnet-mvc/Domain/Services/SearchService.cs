using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;


namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface ISearchService
    {
        Task<List<SearchResult>> Query(string text);
    }

    public class SearchService : ISearchService
    {
        private readonly IRepository<User> _userRepository;
        public SearchService(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<SearchResult>> Query(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            var keys = new[] { text };
            if (text.Contains(" "))
            {
                keys = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            var result = new List<SearchResult>();

            var users = _userRepository.AsQueryable();
            foreach (var key in keys)
            {
                var k = key;
                users = users.Where(x => x.FullName.Contains(k)
                                         || x.Email == k);
            }

            var userResults = users.OrderByDescending(x => x.Id).Skip(0).Take(10).ToList();
            foreach (var item in userResults)
            {
                var exp = string.Format("{0} ({1})", item.FullName, item.Email);
                if (exp.Length > 15)
                {
                    exp = exp.Substring(0, 15);
                }

                result.Add(new SearchResult
                {
                    Url = string.Format("/user/detail/{0}", item.Id),
                    Name = exp,
                    ImgUrl = "/public/img/user.png"
                });
            }

            return Task.FromResult(result);
        }
    }
}