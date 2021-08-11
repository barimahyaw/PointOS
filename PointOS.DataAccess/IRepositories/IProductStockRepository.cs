using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface IProductStockRepository
    {
        /// <summary>
        /// Add/Attach a new Product Quantity's record into repository
        /// </summary>
        /// <param name="stock"></param>
        Task AddAsync(ProductStock stock);

        /// <summary>
        /// Attach changes made to a Product Quantity's record into repository
        /// </summary>
        /// <param name="stock"></param>
        Task UpdateAsync(ProductStock stock);

        /// <summary>
        /// Finds all Product Stock by company Id 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<List<ProductStock>> FindAllAsync(int companyId);
    }
}