using PointOS.DataAccess.IRepositories;
using PointOS.DataAccess.Repositories;
using System.Threading.Tasks;

namespace PointOS.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Entity Framework Core Repositories
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductPricingRepository ProductPricingRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IBranchRepository BranchRepository { get; }
        #endregion

        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DBContext injection
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            ProductCategoryRepository = new ProductCategoryRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            ProductPricingRepository = new ProductPricingRepository(_dbContext);
            CompanyRepository = new CompanyRepository(_dbContext);
            BranchRepository = new BranchRepository(_dbContext);
        }

        /// <summary>
        /// Saves all changes in the repository (memory) to database
        /// </summary>
        /// <returns>number of rows affected</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
