using System.Threading.Tasks;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ICurrencyBusiness
    {
        /// <summary>
        /// Find all currencies
        /// </summary>
        /// <returns></returns>
        Task<ListResponse<CurrencyResponse>> FindAllAsync();
    }
}