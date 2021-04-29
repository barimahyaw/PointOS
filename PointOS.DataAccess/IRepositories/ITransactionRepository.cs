using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// Add/Attach a new Transactions' record into repository
        /// </summary>
        /// <param name="transaction"></param>
        Task AddAsync(Transactions transaction);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="transaction"></param>
        Task UpdateAsync(Transactions transaction);

        /// <summary>
        /// Gets all sales by transaction Id
        /// </summary>
        /// <param name="tranId"></param>
        /// <returns></returns>
        Task<IList<Transactions>> FindByTransactionId(string tranId);
    }
}