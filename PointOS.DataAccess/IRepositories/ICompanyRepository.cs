using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// Add/Attach a new Company's record into repository
        /// </summary>
        /// <param name="company"></param>
        Task AddAsync(Company company);

        /// <summary>
        /// Attach changes made to a Company's record into repository
        /// </summary>
        /// <param name="company"></param>
        Task UpdateAsync(Company company);
    }
}