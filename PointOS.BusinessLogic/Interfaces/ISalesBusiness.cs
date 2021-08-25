using System.Threading.Tasks;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ISalesBusiness
    {
        /// <summary>
        /// Gets Sales details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<ListResponse<SalesResponse>> FindByCompany(int companyId);
    }
}