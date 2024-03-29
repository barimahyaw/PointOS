﻿using PointOS.Common.DTO.Response;
using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface ISalesRepository
    {
        /// <summary>
        /// Add/Attach a new sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        Task AddAsync(IList<Sales> sales);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        Task UpdateAsync(IList<Sales> sales);

        /// <summary>
        /// Gets all sales by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<IList<SalesResponse>> FindByTransactionId(string transactionId);

        /// <summary>
        /// Gets all sales by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IList<SalesResponse>> FindByCompanyId(int companyId, int skip, int take);

        /// <summary>
        /// Gets all sales by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<IList<SalesResponse>> FindByCompanyId(int companyId);

        /// <summary>
        /// Gets the total number of sales by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalSalesNumber(int companyId);

        /// <summary>
        /// Gets the total quantity of a product sales by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        int TotalQuantitySales(int productId);
    }
}