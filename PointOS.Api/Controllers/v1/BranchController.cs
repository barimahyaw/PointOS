﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/v1/[controller]")]
    [ApiController]
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