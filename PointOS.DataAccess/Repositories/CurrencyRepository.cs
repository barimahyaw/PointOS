using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;

namespace PointOS.DataAccess.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public CurrencyRepository(AppDbContext dbContext)
            => _dbContext = dbContext;

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Currency> GetQueryable()
            => _dbContext.Currencies.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Find all currencies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Currency>> FindAllAsync() 
            => await GetQueryable().ToListAsync();
    }
}
