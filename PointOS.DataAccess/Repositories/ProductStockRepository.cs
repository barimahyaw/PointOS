using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductStockRepository(AppDbContext dbContext)
            => _dbContext = dbContext;

        /// <summary>
        ///  Gets Product Quantities as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<ProductStock> GetQueryable()
            => _dbContext.ProductStocks.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new Product Quantity's record into repository
        /// </summary>
        /// <param name="stock"></param>
        public async Task AddAsync(ProductStock stock)
            => await _dbContext.AddRangeAsync(stock);

        /// <summary>
        /// Attach changes made to a Product Quantity's record into repository
        /// </summary>
        /// <param name="stock"></param>
        public Task UpdateAsync(ProductStock stock)
        {
            _dbContext.ProductStocks.Update(stock);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds all Product Stock by company Id 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<List<ProductStock>> FindAllAsync(int companyId)
        {
            //var result = await _dbContext.Sales
            //    .Where(s => s.ProductPricing.Product.ProductCategory.CompanyId == companyId)
            //    .Select(q => new ProductStock
            //    {
            //        Quantity = q.ProductPricing.Product.ProductQuantity.Sum(p => p.Quantity)
            //                  - q.Transaction.Sales.Sum(s => s.Quantity)

            //    })
            //    .ToListAsync();

            //return result;

            return await GetQueryable().Where(pS => pS.Product.ProductCategory.CompanyId == companyId).ToListAsync();
        }


    }
}
