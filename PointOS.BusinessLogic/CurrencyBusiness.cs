using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class CurrencyBusiness : ICurrencyBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CurrencyBusiness(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Find all currencies
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<CurrencyResponse>> FindAllAsync()
        {
            var entities = await _unitOfWork.CurrencyRepository.FindAllAsync();

            if (entities == null) return new ListResponse<CurrencyResponse>(
                new ResponseHeader { Message = "No record found." }, null);

            var result = entities.Select(c => new CurrencyResponse
            {
                Id = c.Id,
                Abbrev = c.Abbreviation,
                Currency = c.CurrencyName
            });

            return new ListResponse<CurrencyResponse>(new ResponseHeader(), result);
        }
    }
}
