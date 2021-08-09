using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess.IRepositories
{
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Find all currencies
        /// </summary>
        /// <returns></returns>
        Task<List<Currency>> FindAllAsync();
    }
}