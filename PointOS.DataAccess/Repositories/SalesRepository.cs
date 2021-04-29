using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOS.Common.DTO.Response;

namespace PointOS.DataAccess.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDbContext _dbContext;

        public SalesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Sales> GetQueryable()
            => _dbContext.Sales.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        public async Task AddAsync(IList<Sales> sales)
            => await _dbContext.Sales.AddRangeAsync(sales);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        public Task UpdateAsync(IList<Sales> sales)
        {
            _dbContext.Sales.UpdateRange(sales);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Gets all sales by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<IList<SalesResponse>> FindByTransactionId(string transactionId)
            => await GetQueryable()
                .Where(s => s.TransactionId == transactionId)
                .Select(s => new SalesResponse
                {
                    Product = s.ProductPricing.Product.Name,
                    CostPrice = s.ProductPricing.CostPrice,
                    ProductCategory = s.ProductPricing.Product.ProductCategory.Name,
                    Quantity = s.Quantity,
                    RetailPrice = s.ProductPricing.RetailPrice
                })
                .ToListAsync();
    }
}
