using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Validation;
using PointOS.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.BusinessLogic.Validators.IValidators;
using static System.String;

namespace PointOS.BusinessLogic.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Validate Product request object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ValidationResult> Validate(ProductRequest request)
        {
            var valResult = new ValidationResult();
            var errorResult = new List<string>();

            var product = await _unitOfWork.ProductRepository.FindByNameAndCategoryIdAsync(request.Name, request.ProductCategoryId);

            if (product != null)
                errorResult.Add("Product already exist.");

            //errorResult.RemoveAll(x => x.IsEmpty());

            if (errorResult.Count <= 0) return valResult;

            valResult.IsError = true;
            valResult.Message = Join('|', errorResult);
            return valResult;
        }
    }
}
