using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface IProductPricingRepository
    {
        /// <summary>
        /// Add/Attach a new product Pricing record into repository
        /// </summary>
        /// <param name="productPricing"></param>
        Task AddAsync(ProductPricing productPricing);

        /// <summary>
        /// Attach changes made to a product Pricing record into repository
        /// </summary>
        /// <param name="productPricing"></param>
        Task UpdateAsync(ProductPricing productPricing);

        /// <summary>
        /// Finds all active Product pricing by company Id 
        /// </summary>
        /// <returns>list of products</returns>
        Task<IList<ProductPricing>> FindAllAsync(int companyId, int skip, int take);

        /// <summary>
        /// Gets the sum total of all active product pricing by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalProductPricing(int companyId);
    }
}