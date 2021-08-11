using PointOS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Attaches Product record into a repository
        /// </summary>
        /// <param name="product"></param>
        Task AddAsync(Product product);

        /// <summary>
        /// Finds all Product records 
        /// </summary>
        /// <returns>list of products</returns>
        Task<IList<Product>> FindAllAsync(int companyId, int skip, int take);

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        Task<Product> FindById(int id);

        /// <summary>
        /// Finds a product record by it's name and product category id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <returns>a record of product</returns>
        Task<Product> FindByNameAndCategoryIdAsync(string name, int categoryId);

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        Task<Product> FindById(Guid id);

        /// <summary>
        /// Gets the sum total of all products by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalProducts(int companyId);

        /// <summary>
        /// Gets all products by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Task<List<Product>> FindAllAsync(int companyId);
    }
}