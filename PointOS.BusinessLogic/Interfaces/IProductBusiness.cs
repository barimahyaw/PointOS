using System;
using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IProductBusiness
    {
        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(ProductRequest request);

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SingleResponse<ProductResponse>> FindById(int id);

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SingleResponse<ProductResponse>> FindById(Guid id);

        /// <summary>
        /// Finds all Product records 
        /// </summary>
        /// <returns>list of products</returns>
        Task<ListResponse<ProductResponse>> FindAllAsync(Guid id);
    }
}