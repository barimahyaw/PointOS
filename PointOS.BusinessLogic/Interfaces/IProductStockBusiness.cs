using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IProductStockBusiness
    {
        /// <summary>
        /// Saves a new product's stock record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(ProductStockRequest request);
    }
}