using System.Threading.Tasks;

namespace set_basic_aspnet_mvc.Domain.Contracts
{
    public interface IReportService
    {
        Task<int> GetTotalUserCount();
    }
}