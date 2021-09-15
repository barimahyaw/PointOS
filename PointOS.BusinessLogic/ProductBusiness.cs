using PointOS.BusinessLogic.Interfaces;
using PointOS.BusinessLogic.Validators.IValidators;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.Common.Helpers.IHelpers;
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
        private readonly IProductValidator _productValidator;
        private readonly IUtils _utils;

        public ProductBusiness(IUnitOfWork unitOfWork, IProductValidator productValidator, IUtils utils)
        {
            _unitOfWork = unitOfWork;
            _productValidator = productValidator;
            _utils = utils;
        }

        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(ProductRequest request)
        {
            var valResult = await _productValidator.Validate(request);
            if (valResult.IsError) return new ResponseHeader { Message = valResult.Message };

            var entity = new Product
            {
                GuidId = Guid.NewGuid(),
                Name = request.Name,
                PhotoUrl = _utils.GetUniqueFileName(request.Photo),
                Status = true,
                CreatedOn = DateTime.UtcNow,
                ProductCategoryId = request.ProductCategoryId,
                CreatedUserId = request.CreatedBy
            };


            await using var tran = await _unitOfWork.TransactionAsync();

            try
            {
                // prepare, attach and save Product
                await _unitOfWork.ProductRepository.AddAsync(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                // prepare and attach product pricing
                var pricing = new ProductPricing
                {
                    ProductId = entity.Id,
                    GuidId = Guid.NewGuid(),
                    WholeSalePrice = request.WholeSalePrice,
                    CostPrice = request.CostPrice,
                    RetailPrice = request.RetailPrice,
                    CurrencyId = request.CurrencyId,
                    CreatedOn = entity.CreatedOn,
                    CreatedUserId = request.CreatedBy,
                    Status = request.Status
                };
                await _unitOfWork.ProductPricingRepository.AddAsync(pricing);

                // prepare and attach product stock
                var stock = new ProductStock
                {
                    GuidId = Guid.NewGuid(),
                    ProductId = entity.Id,
                    Quantity = request.Quantity,
                    CreatedOn = entity.CreatedOn,
                    CreatedUserId = request.CreatedBy
                };
                await _unitOfWork.ProductStockRepository.AddAsync(stock);

                // save all changes to db if no error
                await _unitOfWork.SaveChangesAsync();

                // commit all db changes
                tran.Commit();

                // upload file after tran successfully committed
                await _utils.UploadFile(request.Photo, FileUploadFolder.ProductsPhoto);

                return result != 0 ? new ResponseHeader { StatusCode = 201, Message = $"Record created for {request.Name}", Success = true }
                    : new ResponseHeader { Message = "Sorry, transaction failed. Try again later!" };
            }
            catch
            {
                // restore db to previous state if any error occurs
                await tran.RollbackAsync();

                return new ResponseHeader { Message = "Sorry, transaction failed. Try again later!" };
            }
        }

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
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
        /// <returns>a record of product</returns>
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
        /// Finds all Product records by company Id within a skip and take parameter
        /// </summary>
        /// <returns>a list of products</returns>
        public async Task<ListResponse<ProductResponse>> FindAllAsync(int companyId, int skip, int take, string search)
        {
            var entities = string.IsNullOrWhiteSpace(search)
                ? await _unitOfWork.ProductRepository.FindAllAsync(companyId, skip, take)
                : await _unitOfWork.ProductRepository.FindAllAsync(search, skip, take);

            if (entities == null) return new ListResponse<ProductResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Product))
            }, null);

            var responseBodyList = entities.Select(ProductEntity);
            return new ListResponse<ProductResponse>(new ResponseHeader
            {
                Success = true,
                ReferenceNumber = _unitOfWork.ProductRepository.TotalProducts(companyId).ToString()
            }, responseBodyList);
        }

        /// <summary>
        /// Gets all products by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<ListResponse<ProductResponse>> FindAllAsync(int companyId)
        {
            var entities = await _unitOfWork.ProductRepository.FindAllAsync(companyId);

            if (entities == null) return new ListResponse<ProductResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Product))
            }, null);

            return new ListResponse<ProductResponse>(new ResponseHeader
            {
                Success = true
            }, entities.Select(ProductEntity));
        }

        /// <summary>
        /// Prepares a single product record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ProductResponse ProductEntity(Product entity)
        {
            var quantitySold = _unitOfWork.SalesRepository.TotalQuantitySales(entity.Id);
            var response = new ProductResponse
            {
                Id = entity.Id,
                GuidValue = entity.GuidId,
                Name = entity.Name,
                Status = entity.Status ? "Active" : "Inactive",
                ProductCategory = entity.ProductCategory == null ? string.Empty : entity.ProductCategory.Name,
                ProductCategoryId = entity.ProductCategoryId,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedUser == null
                    ? string.Empty
                    : $"{entity.CreatedUser.FirstName} {entity.CreatedUser.MiddleName} {entity.CreatedUser.LastName}",
                CurrentRetailPrice = entity.ProductPricing?.FirstOrDefault()?.RetailPrice ?? 0,
                PreviousRetailPrice = entity.ProductPricing?.Take(2).OrderByDescending(o => o.Id).FirstOrDefault()
                    ?.RetailPrice ?? 0,
                ProductPricingId = entity.ProductPricing?.FirstOrDefault()?.Id ?? 0,
                Stock = entity.ProductQuantity.Sum(q => q.Quantity) - quantitySold
            };
            return response;
        }

    }
}

