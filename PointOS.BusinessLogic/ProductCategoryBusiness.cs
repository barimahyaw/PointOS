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
        public async Task<ResponseHeader> SaveAsync(ProductCategoryRequest request)
        {
            var entity = new ProductCategory
            {
                GuidId = Guid.NewGuid(),
                Name = request.Name,
                CreatedOn = DateTime.UtcNow,
                Status = request.Status,
                CreatedUserId = request.CreatedBy,
                CompanyId = request.CompanyId
            };
            await _unitOfWork.ProductCategoryRepository.AddAsync(entity);

            var result = await _unitOfWork.SaveChangesAsync();

            return result != 0 ? new ResponseHeader { StatusCode = 201, Message = $"Record created for {request.Name}", Success = true }
                : new ResponseHeader { Message = "" };

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
        /// Select a record of product category by it's Guid Id or integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guidValue"></param>
        /// <returns>a single product category record</returns>
        public async Task<SingleResponse<ProductCategoryResponse>> GetProductCategory(int? id, Guid? guidValue)
        {
            var entity = guidValue == Guid.Empty || string.IsNullOrWhiteSpace(guidValue.ToString())
                ? await _unitOfWork.ProductCategoryRepository.FindByIdAsync(id.GetValueOrDefault())
                : await _unitOfWork.ProductCategoryRepository.FindByIdAsync(guidValue.GetValueOrDefault());

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
        public async Task<ListResponse<ProductCategoryResponse>> FindAllAsync(int companyId, int skip, int take)
        {
            var entities = await _unitOfWork.ProductCategoryRepository.FindAllAsync(companyId, skip, take);

            if (entities.Count <= 0) return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Message = "No record found." }, null);

            var result = entities.Select(ProductCategoryResponseEntity);

            return new ListResponse<ProductCategoryResponse>(new ResponseHeader
            {
                Success = true,
                ReferenceNumber = _unitOfWork.ProductCategoryRepository.TotalProductTypes(companyId).ToString()
            }, result);
        }

        /// <summary>
        /// Select all records of product category by company Id
        /// </summary>
        /// <returns>a list of product category records</returns>
        public async Task<ListResponse<ProductCategoryResponse>> FindAllAsync(int companyId)
        {
            var entities = await _unitOfWork.ProductCategoryRepository.FindAllAsync(companyId);

            if (entities.Count <= 0) return new ListResponse<ProductCategoryResponse>(new ResponseHeader { Message = "No record found." }, null);

            var result = entities.Select(ProductCategoryResponseEntity);

            return new ListResponse<ProductCategoryResponse>(new ResponseHeader
            {
                Success = true,
                ReferenceNumber = _unitOfWork.ProductCategoryRepository.TotalProductTypes(companyId).ToString()
            }, result);
        }

        /// <summary>
        /// a private method to initiate and populate Product Category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>a single record</returns>
        private static ProductCategoryResponse ProductCategoryResponseEntity(ProductCategory entity)
        {
            var user = entity.CreatedUser;
            var result = new ProductCategoryResponse
            {
                Id = entity.Id,
                GuidValue = entity.GuidId,
                ProductName = entity.Name,
                Status = entity.Status ? "Active" : "Inactive",
                CreatedBy = user != null ? $"{user.FirstName} {user.MiddleName} {user.LastName}" : string.Empty,
                CreatedOn = entity.CreatedOn
            };
            return result;
        }
    }
}
