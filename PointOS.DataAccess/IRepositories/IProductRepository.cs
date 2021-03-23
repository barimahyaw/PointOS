using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

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
        Task<IList<Product>> FindAllAsync();

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        Task<Product> FindById(int id);

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        Task<Product> FindById(Guid id);
    }
}