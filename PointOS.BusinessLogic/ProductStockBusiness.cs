using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class ProductStockBusiness : IProductStockBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductStockBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Saves a new product's stock record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(ProductStockRequest request)
        {
            var entity = new ProductStock
            {
                CreatedOn = DateTime.UtcNow,
                CreatedUserId = request.CreatedBy,
                GuidId = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            await _unitOfWork.ProductStockRepository.AddAsync(entity);
            var numRows = await _unitOfWork.SaveChangesAsync();

            return numRows != 0 ? new ResponseHeader
            {
                StatusCode = 201,
                Message = "Quantity updated successfully for the product.",
                Success = true
            } : new ResponseHeader { Message = "Sorry, operation failed. Try again later!" };
        }
    }
}
