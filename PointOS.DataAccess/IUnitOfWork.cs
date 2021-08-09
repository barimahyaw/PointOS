using Microsoft.EntityFrameworkCore.Storage;
using PointOS.DataAccess.IRepositories;
using System.Threading.Tasks;

namespace PointOS.DataAccess
{
    public interface IUnitOfWork
    {
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductPricingRepository ProductPricingRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IBranchRepository BranchRepository { get; }
        public ISalesRepository SalesRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IDashboardRepository DashboardRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; set; }

        /// <summary>
        /// Saves all changes in the repository (memory) to database
        /// </summary>
        /// <returns>number of rows affected</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Performs db transactions on a two or more db write jobs
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> TransactionAsync();
    }
}
