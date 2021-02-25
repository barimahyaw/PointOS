﻿using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    /// <summary>
    /// Product Category Repository
    /// </summary>
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductCategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets Employee Attendance queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<ProductCategory> GetQueryable()
        {
            return _dbContext.ProductCategories.AsNoTrackingWithIdentityResolution();
        }

        /// <summary>
        /// Saves a new product category's record
        /// </summary>
        /// <param name="productCategory"></param>
        public async Task AddAsync(ProductCategory productCategory)
        {
            await _dbContext.ProductCategories.AddAsync(productCategory);
        }

        /// <summary>
        /// Saves changes made to a product category's record
        /// </summary>
        /// <param name="productCategory"></param>
        public Task UpdateAsync(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Update(productCategory);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Select a record of product category by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product category</returns>
        public async Task<ProductCategory> FindByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Select a record of product category by it's Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product category</returns>
        public async Task<ProductCategory> FindByIdAsync(Guid id)
        {
            return await GetQueryable().FirstOrDefaultAsync(p => p.GuidId == id);
        }

        /// <summary>
        /// Select all records of product category by a status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>a list of product category records</returns>
        public async Task<IList<ProductCategory>> FindAllByStatusAsync(bool status)
        {
            return await GetQueryable().Where(p => p.Status == status)
                .ToListAsync();
        }

        /// <summary>
        /// Select all records of product category 
        /// </summary>
        /// <returns>a list of product category records</returns>
        public async Task<IList<ProductCategory>> FindAllAsync()
        {
            return await GetQueryable().ToListAsync();
        }
    }
}
