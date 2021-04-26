using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ICompanyBusiness
    {
        /// <summary>
        /// Saves a company record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(CompanyRequest request);
    }
}