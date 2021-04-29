using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// Add/Attach a new Transactions' record into repository
        /// </summary>
        /// <param name="transactions"></param>
        Task AddAsync(IList<Transactions> transactions);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="transaction"></param>
        Task UpdateAsync(IList<Transactions> transaction);

        /// <summary>
        /// Gets all sales by transaction Id
        /// </summary>
        /// <param name="tranId"></param>
        /// <returns></returns>
        Task<IList<Transactions>> FindByTransactionId(string tranId);
    }
}