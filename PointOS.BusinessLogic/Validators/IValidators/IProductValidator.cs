using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Validation;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Validators.IValidators
{
    public interface IProductValidator
    {
        /// <summary>
        /// Validate Product request object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ValidationResult> Validate(ProductRequest request);
    }
}
