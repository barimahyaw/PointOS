using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ICustomerBusiness
    {
        /// <summary>
        /// Saves a customer record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        Task<ResponseHeader> SaveAsync(CustomerRequest request);

        /// <summary>
        /// Gets customer's id and names by phone number to populate customer drop down
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<SingleResponse<CustomerResponse>> FindAsync(string phoneNumber);
    }
}