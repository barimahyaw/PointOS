using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IProductPricingBusiness
    {
        /// <summary>
        /// Saves a company branch's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(ProductPricingRequest request);

        /// <summary>
        /// Finds all Product records 
        /// </summary>
        /// <returns>list of products</returns>
        Task<ListResponse<ProductPricingResponse>> FindAllAsync(int companyId, int skip, int take);
    }
}