using System;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IReportService
    {
        Task<int> GetTotalUserCount();
    }

    public class ReportService : IReportService
    {
        public Task<int> GetTotalUserCount()
        {
            throw new NotImplementedException();
        }
    }
}