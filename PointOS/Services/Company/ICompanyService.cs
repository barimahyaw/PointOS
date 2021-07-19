using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;

namespace PointOS.Services.Company
{
    public interface ICompanyService
    {
        Task<ResponseHeader> Add(CompanyRegistrationRequest request);
    }
}