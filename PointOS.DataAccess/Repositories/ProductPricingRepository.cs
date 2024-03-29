﻿using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class ProductPricingRepository : IProductPricingRepository
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

        /// <summary>
        /// Finds all active Product pricing by company Id 
        /// </summary>
        /// <returns>list of products</returns>
        public async Task<IList<ProductPricing>> FindAllAsync(int companyId, int skip, int take)
            => await GetQueryable()
            .Where(p => p.Product.ProductCategory.CompanyId == companyId && p.Status)
            .Skip(skip)
            .Take(take)
            .Select(p => new ProductPricing
            {
                Id = p.Id,
                GuidId = p.GuidId,
                CostPrice = p.CostPrice,
                WholeSalePrice = p.WholeSalePrice,
                RetailPrice = p.RetailPrice,
                CreatedOn = p.CreatedOn,
                ProductId = p.ProductId,
                Status = p.Status,
                Product = new Product
                {
                    Name = p.Product.Name
                }
            })
            .ToListAsync();


        /// <summary>
        /// Gets the sum total of all active product pricing by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalProductPricing(int companyId)
            => GetQueryable().Count(p => p.Product.ProductCategory.CompanyId == companyId && p.Status);
    }
}
