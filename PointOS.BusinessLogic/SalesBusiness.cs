using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class SalesBusiness : ISalesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Gets Sales details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<ListResponse<SalesResponse>> FindByCompany(int companyId, int skip , int take)
        {
            var sales = await _unitOfWork.SalesRepository.FindByCompanyId(companyId,skip,take);

            if (sales == null) return new ListResponse<SalesResponse>(new ResponseHeader
            {
                Message = "No record found"
            }, null);

            return new ListResponse<SalesResponse>
            {
                ResponseBodyList = sales,
                ResponseHeader = new ResponseHeader
                {
                    Success = true,
                    ReferenceNumber = _unitOfWork.SalesRepository.TotalSalesNumber(companyId).ToString()
                }
            };
        }
    }
}
