using set_basic_aspnet_mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IReportService
    {
        Task<List<SearchResult>> GetMostSearchedItems(int count);
        Task<int> GetTotalUserCount(int count);
    }

    public class ReportService : IReportService
    {
        public Task<List<SearchResult>> GetMostSearchedItems(int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalUserCount(int count)
        {
            throw new NotImplementedException();
        }
    }
}