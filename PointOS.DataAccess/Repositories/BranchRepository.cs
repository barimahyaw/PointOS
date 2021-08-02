using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class BranchRepository : IBranchRepository
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

        ///// <summary>
        ///// Gets Branches filtering by company Id
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public async Task<List<BranchResponse>> FindByCompanyIdAsync(int companyId) =>
        //    await GetQueryable().Where(b => b.CompanyId == companyId).Select(r => new BranchResponse
        //    {
        //        Id = r.Id,
        //        Name = r.Name,
        //        CreatedBy = r.CreatedUser.FirstName,
        //        CreatedOn = r.CreatedOn
        //    }).ToListAsync();

        /// <summary>
        /// Gets Branches filtering by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<Branch>> FindByCompanyIdAsync(int companyId, int skip, int take) =>
            await GetQueryable().Include(b => b.CreatedUser)
                .Where(b => b.CompanyId == companyId).Skip(skip).Take(take).ToListAsync();
    }
}
