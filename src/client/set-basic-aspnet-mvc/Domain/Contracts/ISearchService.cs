using System.Collections.Generic;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.DataTransferObjects;

namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface ISearchService
    {
        Task<List<SearchResultDto>> Query(string text);
    }
}