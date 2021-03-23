using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(ProductRequest request)
        {
            var entity = new Product
            {
                GuidId = Guid.NewGuid(),
                Name = request.Name,
                Status = true,
                CreatedOn = DateTime.UtcNow,
                ProductCategoryId = request.ProductCategoryId,
                CreatedUserId = request.CreatedUserId
            };

            await _unitOfWork.ProductRepository.AddAsync(entity);
            var result = await _unitOfWork.SaveChangesAsync();

            return result != 0 ? new ResponseHeader { StatusCode = 201, Message = $"Record created for {request.Name}", Success = true }
                : new ResponseHeader { Message = "" };
        }

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SingleResponse<ProductResponse>> FindById(int id)
        {
            var entity = await _unitOfWork.ProductRepository.FindById(id);

            if (entity == null) return new SingleResponse<ProductResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Product))
            }, null);

            var response = ProductEntity(entity);

            return new SingleResponse<ProductResponse>(new ResponseHeader { Success = true }, response);
        }

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SingleResponse<ProductResponse>> FindById(Guid id)
        {
            var entity = await _unitOfWork.ProductRepository.FindById(id);

            if (entity == null) return new SingleResponse<ProductResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Product))
            }, null);

            var response = ProductEntity(entity);

            return new SingleResponse<ProductResponse>(new ResponseHeader { Success = true }, response);
        }

        /// <summary>
        /// Finds all Product records 
        /// </summary>
        /// <returns>list of products</returns>
        public async Task<ListResponse<ProductResponse>> FindAllAsync(Guid id)
        {
            var entities = await _unitOfWork.ProductRepository.FindAllAsync();

            if (entities == null) return new ListResponse<ProductResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Product))
            }, null);

            var response = entities.Select(ProductEntity);

            return new ListResponse<ProductResponse>(new ResponseHeader { Success = true }, response);
        }

        /// <summary>
        /// Prepares a single product record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static ProductResponse ProductEntity(Product entity)
        {
            var response = new ProductResponse
            {
                Id = entity.Id,
                GuidValue = entity.GuidId,
                Name = entity.Name,
                Status = entity.Status,
                ProductCategoryId = entity.ProductCategoryId,
                CreatedOn = entity.CreatedOn,
                CreatedUserId = entity.CreatedUserId
            };
            return response;
        }
    }
}
