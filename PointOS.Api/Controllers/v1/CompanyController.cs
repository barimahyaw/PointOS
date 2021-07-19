using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusiness _companyBusiness;
        private readonly IUserAccountBusiness _userAccountBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyBusiness"></param>
        /// <param name="userAccountBusiness"></param>
        public CompanyController(ICompanyBusiness companyBusiness, IUserAccountBusiness userAccountBusiness)
        {
            _companyBusiness = companyBusiness;
            _userAccountBusiness = userAccountBusiness;
        }

        /// <summary>
        /// Saves a company's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseHeader> Post(CompanyRegistrationRequest request)
        {
            request.CompanyRequest.Operation = CrudOperation.Create;
            var userResponse = await _userAccountBusiness.AddUser(request.UserRegistrationRequest);

            if (!userResponse.Success) return userResponse;

            request.CompanyRequest.CreatedBy = userResponse.ReferenceNumber;
            await _companyBusiness.SaveAsync(request.CompanyRequest);


            return userResponse;
        }

        /// <summary>
        /// Updates a company's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseHeader> Put(CompanyRequest request)
        {
            request.Operation = CrudOperation.Update;
            return await _companyBusiness.SaveAsync(request);
        }
    }
}
