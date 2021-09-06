using System.Threading.Tasks;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IDashboardBusiness
    {
        Task<SingleResponse<GeneralDashboardResponse>> General(int companyId);
    }
}