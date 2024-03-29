﻿using PointOS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface IProductCategoryRepository
    {
        /// <summary>
        /// Saves a new product category's record
        /// </summary>
        /// <param name="productCategory"></param>
        Task AddAsync(ProductCategory productCategory);

        /// <summary>
        /// Saves changes made to a product category's record
        /// </summary>
        /// <param name="productCategory"></param>
        Task UpdateAsync(ProductCategory productCategory);

        /// <summary>
        /// Select a record of product category by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product category</returns>
        Task<ProductCategory> FindByIdAsync(int id);

        /// <summary>
        /// Select a record of product category by it's Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product category</returns>
        Task<ProductCategory> FindByIdAsync(Guid id);

        /// <summary>
        /// Select all records of product category by a status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>a list of product category records</returns>
        Task<IList<ProductCategory>> FindAllByStatusAsync(bool status);

        /// <summary>
        /// Select all records of product category 
        /// </summary>
        /// <returns>a list of product category records</returns>
        Task<IList<ProductCategory>> FindAllAsync(int companyId, int skip, int take);

        /// <summary>
        /// Gets the total number of Product Categories by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>

        /// <summary>
        /// Select all records of product category by company Id
        /// </summary>
        /// <returns>a list of product category records</returns>
        public Task<IList<ProductCategory>> FindAllAsync(int companyId);

        /// <summary>
        /// Gets the sum total of all product categories by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalProductTypes(int companyId);
    }
}