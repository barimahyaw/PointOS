using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerBusiness"></param>
        public CustomerController(ICustomerBusiness customerBusiness) => _customerBusiness = customerBusiness;

        /// <summary>
        /// Saves a customer's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(CustomerRequest request)
            => await _customerBusiness.SaveAsync(request);

        /// <summary>
        /// Updates a customer's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseHeader> Put(CustomerRequest request)
        {
            request.CrudOperation = CrudOperation.Update;
            return await _customerBusiness.SaveAsync(request);
        }

        /// <summary>
        /// Gets customer by phone Number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("getByPhoneNumber")]
        public async Task<SingleResponse<CustomerResponse>> GetByPhoneNumber(string phoneNumber) =>
            await _customerBusiness.FindAsync(phoneNumber);

        /// <summary>
        /// Gets customer by phone Number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("getAllLikePhoneNumber")]
        public async Task<ListResponse<CustomerResponse>> GetAllLikePhoneNumber(string phoneNumber) =>
            await _customerBusiness.FindAllAsync(phoneNumber);

        /// <summary>
        /// Gets customer by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("getByCompany")]
        public async Task<ListResponse<CustomerResponse>> GetByCompany(int companyId, int skip = 0, int take = 5) =>
            await _customerBusiness.FindAllAsync(companyId, skip, take);
    }
}
