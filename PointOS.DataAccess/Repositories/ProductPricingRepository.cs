using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class ProductPricingRepository
    {
        private readonly AppDbContext _dbContext;
        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>

        public ProductPricingRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        ///  Gets Product Pricing as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<ProductPricing> GetQueryable()
            => _dbContext.ProductPricing.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new product Pricing record into repository
        /// </summary>
        /// <param name="productPricing"></param>
        public async Task AddAsync(ProductPricing productPricing)
            => await _dbContext.ProductPricing.AddAsync(productPricing);

        /// <summary>
        /// Attach changes made to a product Pricing record into repository
        /// </summary>
        /// <param name="productPricing"></param>
        public Task UpdateAsync(ProductPricing productPricing)
        {
            _dbContext.ProductPricing.Update(productPricing);
            return Task.FromResult(0);
        }
    }
}
