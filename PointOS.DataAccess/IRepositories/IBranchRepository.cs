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
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<List<Branch>> FindByCompanyIdAsync(int companyId, int skip, int take, string orderBy);

        /// <summary>
        /// Gets the total number of branches by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        int TotalBranchesNumber(int companyId);
    }
}