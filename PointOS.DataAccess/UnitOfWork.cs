using Microsoft.EntityFrameworkCore.Storage;
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
        public ISalesRepository SalesRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IDashboardRepository DashboardRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IProductStockRepository ProductStockRepository { get; set; }
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
            SalesRepository = new SalesRepository(_dbContext);
            TransactionRepository = new TransactionRepository(_dbContext);
            DashboardRepository = new DashboardRepository(_dbContext);
            CurrencyRepository = new CurrencyRepository(dbContext);
            ProductStockRepository = new ProductStockRepository(dbContext);
        }

        /// <summary>
        /// Saves all changes in the repository (memory) to database
        /// </summary>
        /// <returns>number of rows affected</returns>
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        /// <summary>
        /// Performs db transactions on a two or more db write jobs
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> TransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
    }
}
