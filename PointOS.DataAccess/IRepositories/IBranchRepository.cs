using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets Branches filtering by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<Branch>> FindByCompanyIdAsync(int companyId, int skip, int take);
    }
}