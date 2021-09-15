using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CustomerBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Saves a customer record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(CustomerRequest request)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                NationalIdCardNumber = request.NationalIdCardNumber,
                Address = request.Address
            };

            switch (request.CrudOperation)
            {
                case CrudOperation.Create:
                    customer.CreatedUerId = request.CreatedBy;
                    customer.CreatedOn = DateTime.UtcNow;
                    customer.GuidId = Guid.NewGuid();
                    await _unitOfWork.CustomerRepository.Add(customer);
                    break;
                case CrudOperation.Read:
                    break;
                case CrudOperation.Update:
                    customer.ModifiedUerId = request.CreatedBy;
                    customer.ModifiedOn = DateTime.UtcNow;
                    await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                    break;
                case CrudOperation.Delete:
                    break;
                default:
                    goto case CrudOperation.Create;
            }

            var numRows = await _unitOfWork.SaveChangesAsync();

            var operation = request.CrudOperation == CrudOperation.Create ? "created" : "updated";

            return numRows != 0 ? new ResponseHeader { StatusCode = 201, Message = $"Record {operation} for customer, {request.FirstName}", Success = true } : new ResponseHeader { Message = "Sorry, operation failed. Try again later!" };
        }

        /// <summary>
        /// Gets customer's id and names by phone number to populate customer drop down
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<SingleResponse<CustomerResponse>> FindAsync(string phoneNumber)
        {
            var customer = await _unitOfWork.CustomerRepository.FindAsync(phoneNumber);

            if (customer == null) return new SingleResponse<CustomerResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Customer)),
                ReferenceNumber = 1.ToString()
            }, null);

            var response = new SingleResponse<CustomerResponse>
            {
                ResponseBody = CustomerResponseEntity(customer),
                ResponseHeader = new ResponseHeader { Success = true }
            };

            return response;
        }

        /// <summary>
        /// Gets customers by their PhoneNumber like phoneNumber to populate customer auto complete
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<ListResponse<CustomerResponse>> FindAllAsync(string phoneNumber)
        {
            var customers = await _unitOfWork.CustomerRepository.FindAllContainPhoneNumberAsync(phoneNumber);


            if (customers == null) return new ListResponse<CustomerResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Customer))
            }, null);

            return new ListResponse<CustomerResponse>
            {
                ResponseHeader = new ResponseHeader { Success = true },
                ResponseBodyList = customers.Select(CustomerResponseEntity)
            };
        }

        /// <summary>
        /// Gets Customer details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<ListResponse<CustomerResponse>> FindAllAsync(int companyId, int skip, int take)
        {
            var customers = await _unitOfWork.CustomerRepository.FindAllAsync(companyId, skip, take);

            if (customers == null) return new ListResponse<CustomerResponse>(new ResponseHeader
            {
                Message = string.Format(Status.NotFound.GetAttributeStringValue(), nameof(Customer))
            }, null);

            return new ListResponse<CustomerResponse>
            {
                ResponseHeader = new ResponseHeader
                {
                    Success = true,
                    ReferenceNumber = customers.Count.ToString()
                },
                ResponseBodyList = customers.Select(CustomerResponseEntity)
            };
        }

        /// <summary>
        /// Prepares a single customer record
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private static CustomerResponse CustomerResponseEntity(Customer customer)
        {
            var response = new CustomerResponse
            {
                Id = customer.Id,
                GuidId = customer.GuidId,
                NationalIdCardNumber = customer.NationalIdCardNumber,
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                EmailAddress = customer.EmailAddress,
                Address = customer.Address,
                CreatedBy = customer.CreatedUerId,
                CreatedOn = customer.CreatedOn
            };
            return response;
        }
    }
}
