using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class ProductCategoryBusiness : IProductCategoryBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Saves a product category record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<int> SaveAsync(ProductCategoryRequest request)
        {
            var entity = new ProductCategory
            {
                GuidId = Guid.NewGuid(),
                ProductName = request.ProductName,
                CreatedOn = DateTime.UtcNow,
                Status = true,
                CreatedUserId = request.CreatedBy
            };
            await _unitOfWork.ProductCategoryRepository.AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Select a record of product category by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single product category record</returns>
        public async Task<SingleResponse<ProductCategoryResponse>> FindByIdAsync(int id)
        {
            var entity = await _unitOfWork.ProductCategoryRepository.FindByIdAsync(id);

            if (entity == null) return new SingleResponse<ProductCategoryResponse>(
                new ResponseHeader { Message = "No record found." }, null);

            var result = ProductCategoryResponseEntity(entity);

            return new SingleResponse<ProductCategoryResponse>(new ResponseHeader { Success = true }, result);
        }

        /// <summary>
        /// Select a record of product category by it's Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single product category record</returns>
        public async Task<SingleResponse<ProductCategoryResponse>> FindByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ProductCategoryRepository.FindByIdAsync(id);

            if (entity == null) return new SingleResponse<ProductCategoryResponse>(
                new ResponseHeader { Message = "No record found." }, null);

            var result = ProductCategoryResponseEntity(entity);

            return new SingleResponse<ProductCategoryResponse>(new ResponseHeader { Success = true }, result);
        }

        /// <summary>
        /// Select all records of product category by a status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>a list of product category records</returns>
        public async Task<ListResponse<ProductCategoryResponse>> FindAllAsync(bool status)
        {
            var entities = await _unitOfWork.ProductCategoryRepository.FindAllByStatusAsync(status);

            if (entities.Count <= 0) return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Message = "No record found." }, null);

            var result = entities.Select(ProductCategoryResponseEntity);

            return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Success = true }, result);
        }

        /// <summary>
        /// Select all records of product category 
        /// </summary>
        /// <returns>a list of product category records</returns>
        public async Task<ListResponse<ProductCategoryResponse>> FindAllAsync()
        {
            var entities = await _unitOfWork.ProductCategoryRepository.FindAllAsync();

            if (entities.Count <= 0) return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Message = "No record found." }, null);

            var result = entities.Select(ProductCategoryResponseEntity);

            return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Success = true }, result);
        }

        /// <summary>
        /// a private method to initiate and populate Product Category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>a single record</returns>
        private static ProductCategoryResponse ProductCategoryResponseEntity(ProductCategory entity)
        {
            var result = new ProductCategoryResponse
            {
                Id = entity.Id,
                GuidValue = entity.GuidId,
                ProductName = entity.ProductName,
                Status = entity.Status,
                CreatedBy = entity.CreatedUserId,
                CreatedOn = entity.CreatedOn
            };
            return result;
        }
    }
}
