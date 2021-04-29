using Microsoft.EntityFrameworkCore;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Transactions> GetQueryable()
            => _dbContext.Transactions.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new Transactions' record into repository
        /// </summary>
        /// <param name="transaction"></param>
        public async Task AddAsync(Transactions transaction)
            => await _dbContext.Transactions.AddAsync(transaction);

        /// <summary>
        /// Attach changes made to a sale's record into repository
        /// </summary>
        /// <param name="transaction"></param>
        public Task UpdateAsync(Transactions transaction)
        {
            _dbContext.Transactions.Update(transaction);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Gets all sales by transaction Id
        /// </summary>
        /// <param name="tranId"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> FindByTransactionId(string tranId)
            => await GetQueryable()
                .Select(tran => new TransactionResponse
                {
                    TransactionId = tran.TransactionId,
                    TransactionType = tran.TransactionType,
                    PaymentType = tran.PaymentType,
                    Amount = tran.Amount,
                    CreatedBy = $"{tran.CreatedUser.FirstName} {tran.CreatedUser.MiddleName} {tran.CreatedUser.LastName}",
                    CreatedOn = tran.CreatedOn
                })
                .FirstOrDefaultAsync(s => s.TransactionId == tranId);
    }
}
