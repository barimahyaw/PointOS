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

        /// <summary>
        /// Saves all changes in the repository (memory) to database
        /// </summary>
        /// <returns>number of rows affected</returns>
        Task<int> SaveChangesAsync();
    }
}
