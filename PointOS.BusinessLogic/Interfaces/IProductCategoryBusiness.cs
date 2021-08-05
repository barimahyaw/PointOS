using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IProductCategoryBusiness
    {
        /// <summary>
        /// Saves a product category record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(ProductCategoryRequest request);

        /// <summary>
        /// Select a record of product category by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single product category record</returns>
        Task<SingleResponse<ProductCategoryResponse>> FindByIdAsync(int id);

        /// <summary>
        /// Select a record of product category by it's Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single product category record</returns>
        Task<SingleResponse<ProductCategoryResponse>> FindByIdAsync(Guid id);

        /// <summary>
        /// Select a record of product category by it's Guid Id or integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guidValue"></param>
        /// <returns>a single product category record</returns>
        Task<SingleResponse<ProductCategoryResponse>> GetProductCategory(int? id, Guid? guidValue);

        /// <summary>
        /// Select all records of product category by a status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>a list of product category records</returns>
        Task<ListResponse<ProductCategoryResponse>> FindAllAsync(bool status);

        /// <summary>
        /// Select all records of product category 
        /// </summary>
        /// <returns>a list of product category records</returns>
        Task<ListResponse<ProductCategoryResponse>> FindAllAsync(int companyId, int skip, int take);
    }
}