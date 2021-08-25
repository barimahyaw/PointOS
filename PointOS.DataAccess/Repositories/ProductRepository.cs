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
        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>

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
        public async Task<IList<Product>> FindAllAsync(int companyId, int skip, int take)
            => await GetQueryable().Where(p => p.ProductCategory.CompanyId == companyId)
                .Skip(skip)
                .Take(take)
                .Select(p => new Product
                {
                    Name = p.Name,
                    PhotoUrl = p.PhotoUrl,
                    CreatedOn = p.CreatedOn,
                    CreatedUserId = p.CreatedUserId,
                    Status = p.Status,
                    Id = p.Id,
                    CreatedUser = new ApplicationUser
                    {
                        FirstName = p.CreatedUser.FirstName,
                        MiddleName = p.CreatedUser.MiddleName,
                        LastName = p.CreatedUser.LastName
                    },
                    //ProductPricing = p.ProductPricing.Where(x=>x.Status).ToList(),
                    ProductCategory = new ProductCategory
                    {
                        //CompanyId = p.ProductCategory.CompanyId,
                        Name = p.ProductCategory.Name
                    },
                    ProductPricing = p.ProductPricing.ToList(),
                    ProductQuantity = p.ProductQuantity.Select(q => new ProductStock
                    {
                        Quantity = q.Quantity
                    }).ToList()
                })
                .ToListAsync();

        /// <summary>
        /// Finds a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        public async Task<Product> FindById(int id) 
            => await GetQueryable().Select(p => new Product
            {
                Name = p.Name,
                CreatedOn = p.CreatedOn,
                CreatedUserId = p.CreatedUserId,
                Status = p.Status,
                Id = p.Id,
                CreatedUser = new ApplicationUser
                {
                    FirstName = p.CreatedUser.FirstName,
                    MiddleName = p.CreatedUser.MiddleName,
                    LastName = p.CreatedUser.LastName
                },
                //ProductPricing = p.ProductPricing.Where(x=>x.Status).ToList(),
                ProductCategory = new ProductCategory
                {
                    //CompanyId = p.ProductCategory.CompanyId,
                    Name = p.ProductCategory.Name
                },
                ProductPricing = p.ProductPricing.ToList(),
                ProductQuantity = p.ProductQuantity.Select(q => new ProductStock
                {
                    Quantity = q.Quantity
                }).ToList()
            }).FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// Finds a product record by it's name and product category id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <returns>a record of product</returns>
        public async Task<Product> FindByNameAndCategoryIdAsync(string name, int categoryId)
            => await GetQueryable().FirstOrDefaultAsync(p => p.Name == name && p.ProductCategoryId == categoryId);

        /// <summary>
        /// Finds a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        public async Task<Product> FindById(Guid id) => await GetQueryable().FirstOrDefaultAsync(p => p.GuidId == id);

        /// <summary>
        /// Gets the sum total of all products by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalProducts(int companyId)
            => GetQueryable().Count(p => p.ProductCategory.CompanyId == companyId);

        /// <summary>
        /// Gets all products by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<List<Product>> FindAllAsync(int companyId)
            => await GetQueryable().Where(p => p.ProductCategory.CompanyId == companyId).ToListAsync();
    }
}
