using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

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
    }
}