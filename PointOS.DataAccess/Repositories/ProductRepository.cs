using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Gets Product as queryable with no tracking of changes
        /// </summary>
        /// <returns>queryable of products</returns>
        private IQueryable<Product> GetQueryable() => _dbContext.Products.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Attaches Product record into a repository
        /// </summary>
        /// <param name="product"></param>
        public async Task AddAsync(Product product) => await _dbContext.Products.AddAsync(product);

        /// <summary>
        /// Finds all Product records 
        /// </summary>
        /// <returns>list of products</returns>
        public async Task<IList<Product>> FindAllAsync() => await GetQueryable().ToListAsync();

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        public async Task<Product> FindById(int id) => await GetQueryable().FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        public async Task<Product> FindById(Guid id) => await GetQueryable().FirstOrDefaultAsync(p => p.GuidId == id);
    }
}
