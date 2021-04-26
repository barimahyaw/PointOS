using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public CompanyRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        ///  Gets Companies as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Company> GetQueryable()
            => _dbContext.Companies.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new Company's record into repository
        /// </summary>
        /// <param name="company"></param>
        public async Task AddAsync(Company company)
            => await _dbContext.Companies.AddAsync(company);

        /// <summary>
        /// Attach changes made to a Company's record into repository
        /// </summary>
        /// <param name="company"></param>
        public Task UpdateAsync(Company company)
        {
            _dbContext.Companies.Update(company);
            return Task.FromResult(0);
        }
    }
}
