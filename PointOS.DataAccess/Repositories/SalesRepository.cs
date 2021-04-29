using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDbContext _dbContext;

        public SalesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Sales> GetQueryable()
            => _dbContext.Sales.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        public async Task AddAsync(Sales sales)
            => await _dbContext.Sales.AddAsync(sales);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="sales"></param>
        public Task UpdateAsync(Sales sales)
        {
            _dbContext.Sales.Update(sales);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Gets all sales by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IList<Sales>> FindByDate(DateTime date)
            => await GetQueryable().Where(s => s.CreatedOn == date).ToListAsync();
    }
}
