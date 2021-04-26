using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class ProductPricingBusiness : IProductPricingBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductPricingBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Saves a company branch's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(ProductPricingRequest request)
        {
            var entity = new ProductPricing
            {
                ProductId = request.ProductId,
                CostPrice = request.CostPrice,
                RetailPrice = request.RetailPrice,
                WholeSalePrice = request.WholeSalePrice,
                Status = request.Status
            };

            switch (request.Operation)
            {
                case CrudOperation.Create:
                    entity.GuidId = Guid.NewGuid();
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedUserId = request.CreatedBy;
                    await _unitOfWork.ProductPricingRepository.AddAsync(entity);
                    break;
                case CrudOperation.Update:
                    await _unitOfWork.ProductPricingRepository.UpdateAsync(entity);
                    break;
                case CrudOperation.Read:
                    break;
                case CrudOperation.Delete:
                    break;
                default:
                    goto case CrudOperation.Create;
            }

            var numOfRows = await _unitOfWork.SaveChangesAsync();

            var result = numOfRows != 0
                ? request.Operation == CrudOperation.Create
                    ? new ResponseHeader { StatusCode = 201, Message = "Record created successfully", Success = true }
                    : new ResponseHeader { StatusCode = 202, Message = "Record updated successfully", Success = true }
                : new ResponseHeader { Message = "Operation failed. please try again later!" };

            return result;
        }
    }
}
