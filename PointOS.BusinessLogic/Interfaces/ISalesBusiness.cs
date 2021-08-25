using PointOS.Common.DTO.Response;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ISalesBusiness
    {
        /// <summary>
        /// Gets Sales details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<ListResponse<SalesResponse>> FindByCompany(int companyId, int skip, int take);
    }
}