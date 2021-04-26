using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class BranchRepository
    {
        private readonly AppDbContext _dbContext;
        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public BranchRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Branch> GetQueryable()
            => _dbContext.Branches.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        public async Task AddAsync(Branch branch)
            => await _dbContext.Branches.AddAsync(branch);

        /// <summary>
        /// Attach changes made to a Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        public Task UpdateAsync(Branch branch)
        {
            _dbContext.Branches.Update(branch);
            return Task.FromResult(0);
        }
    }
}
