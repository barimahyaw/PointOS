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
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusiness _companyBusiness;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyBusiness"></param>
        public CompanyController(ICompanyBusiness companyBusiness)
        {
            _companyBusiness = companyBusiness;
        }

        /// <summary>
        /// Saves a company's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(CompanyRequest request)
        {
            request.Operation = CrudOperation.Create;
            return await _companyBusiness.SaveAsync(request);
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
