using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface ISalesRepository
    {
        /// <summary>
        /// Add/Attach a new sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        Task AddAsync(Sales sales);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        Task UpdateAsync(Sales sales);

        /// <summary>
        /// Gets all sales by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IList<Sales>> FindByDate(DateTime date);
    }
}