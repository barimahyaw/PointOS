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
    public class BranchController : ControllerBase
    {
        private readonly IBranchBusiness _businessBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessBusiness"></param>
        public BranchController(IBranchBusiness businessBusiness)
        {
            _businessBusiness = businessBusiness;
        }

        /// <summary>
        /// Saves a company branch's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(BranchRequest request)
        {
            request.Operation = CrudOperation.Create;
            return await _businessBusiness.SaveAsync(request);
        }

        /// <summary>
        /// Updates a company branch's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseHeader> Put(BranchRequest request)
        {
            request.Operation = CrudOperation.Update;
            return await _businessBusiness.SaveAsync(request);
        }
    }
}
