using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<TransactionRequest> Find(string transactionId)
        //{
        //    var tran = await _unitOfWork.SalesRepository.FindByTransactionId(DateTime.UtcNow);

        //}

        /// <summary>
        /// Saves a company record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(CompanyRequest request)
        {
            var entity = new Company
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                AltPhoneNumber = request.AltPhoneNumber,
                EmailAddress = request.EmailAddress
            };

            var branch = new Branch
            {
                CreatedOn = DateTime.UtcNow,
                CreatedUserId = request.CreatedBy,
                GuidId = Guid.NewGuid(),
                Name = request.Branch
            };

            await using var tran = await _unitOfWork.TransactionAsync();
            try
            {
                switch (request.Operation)
                {
                    case CrudOperation.Create:
                        entity.GuidId = Guid.NewGuid();
                        entity.CreatedOn = DateTime.UtcNow;
                        entity.CreatedUserId = request.CreatedBy;
                        await _unitOfWork.CompanyRepository.AddAsync(entity);
                        await _unitOfWork.SaveChangesAsync();

                        branch.CompanyId = entity.Id;
                        await _unitOfWork.BranchRepository.AddAsync(branch);

                        break;
                    case CrudOperation.Update:
                        await _unitOfWork.CompanyRepository.UpdateAsync(entity);
                        break;
                    case CrudOperation.Read:
                        break;
                    case CrudOperation.Delete:
                        break;
                    default:
                        goto case CrudOperation.Create;
                }

                // save all changes to db if no error
                var numOfRows = await _unitOfWork.SaveChangesAsync();

                // commit all db changes
                tran.Commit();

                var result = numOfRows != 0
                    ? request.Operation == CrudOperation.Create
                        ? new ResponseHeader { StatusCode = 201, Message = $"Record created for {request.Name}", Success = true }
                        : new ResponseHeader { StatusCode = 202, Message = $"Record updated for {request.Name}", Success = true }
                    : new ResponseHeader { Message = "Operation failed. Try again later!" };

                return result;
            }
            catch
            {
                // restore db to previous state if any error occurs
                await tran.RollbackAsync();

                return new ResponseHeader { Message = "Operation failed. Try again later!" };
            }


        }
    }
}
