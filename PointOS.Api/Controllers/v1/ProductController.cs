﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// Product Controller Class
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;
        /// <summary>
        /// Controller DI
        /// </summary>
        /// <param name="productBusiness"></param>
        public ProductController(IProductBusiness productBusiness) => _productBusiness = productBusiness;

        /// <summary>
        /// Gets all Product records 
        /// </summary>
        /// <returns>a list of products</returns>
        [HttpGet]
        public async Task<ListResponse<ProductResponse>> Get(int companyId, string search, int skip, int take = 5)
            => await _productBusiness.FindAllAsync(companyId, skip, take, search);

        /// <summary>
        /// Gets all Product records by company Id
        /// </summary>
        /// <returns>a list of products</returns>
        [HttpGet("getAll")]
        public async Task<ListResponse<ProductResponse>> GetAll(int companyId)
            => await _productBusiness.FindAllAsync(companyId);

        /// <summary>
        /// Gets a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        [HttpGet("get")]
        public async Task<SingleResponse<ProductResponse>> Get(int id)
            => await _productBusiness.FindById(id);

        /// <summary>
        /// Gets a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="guidValue"></param>
        /// <returns>a record of product</returns>
        [HttpGet("{guidValue}")]
        public async Task<SingleResponse<ProductResponse>> Get(Guid guidValue)
            => await _productBusiness.FindById(guidValue);

        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(ProductRequest request)
            => await _productBusiness.SaveAsync(request);
    }
}
