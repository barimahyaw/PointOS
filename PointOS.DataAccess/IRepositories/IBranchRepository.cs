using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface IBranchRepository
    {
        /// <summary>
        /// Add/Attach a new Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        Task AddAsync(Branch branch);

        /// <summary>
        /// Attach changes made to a Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        Task UpdateAsync(Branch branch);
    }
}