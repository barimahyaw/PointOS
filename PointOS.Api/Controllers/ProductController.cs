using Microsoft.AspNetCore.Mvc;
using PointOS.Common.DTO.Response;

namespace PointOS.Api.Controllers
{
    /// <summary>
    /// Product Controller Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseHeader Post()
        {
            return new ResponseHeader();
        }
    }
}
