using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.Services.ProductCategory
{
    public interface IProductCategoryService
    {
        Task<ResponseHeader> Add(ProductCategoryRequest request);
    }
}