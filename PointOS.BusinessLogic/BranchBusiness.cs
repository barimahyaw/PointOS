using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class BranchBusiness : IBranchBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Saves a company branch's record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> SaveAsync(BranchRequest request)
        {
            var entity = new Branch
            {
                Name = request.Name,
                CompanyId = request.CompanyId
            };

            switch (request.Operation)
            {
                case CrudOperation.Create:
                    entity.GuidId = Guid.NewGuid();
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedUserId = request.CreatedBy;
                    await _unitOfWork.BranchRepository.AddAsync(entity);
                    break;
                case CrudOperation.Update:
                    await _unitOfWork.BranchRepository.UpdateAsync(entity);
                    break;
                case CrudOperation.Read:
                    break;
                case CrudOperation.Delete:
                    break;
                default:
                    goto case CrudOperation.Create;
            }

            var numOfRows = await _unitOfWork.SaveChangesAsync();

            var result = numOfRows != 0
                ? request.Operation == CrudOperation.Create
                    ? new ResponseHeader { StatusCode = 201, Message = $"Record created for {request.Name}", Success = true }
                    : new ResponseHeader { StatusCode = 202, Message = $"Record updated for {request.Name}", Success = true }
                : new ResponseHeader { Message = "Operation failed. please try again later!" };

            return result;
        }

        /// <summary>
        /// Gets Branches filtering by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<ListResponse<BranchResponse>> FindByCompanyIdAsync(int companyId, int skip, int take, string orderBy)
        {
            var result = await _unitOfWork.BranchRepository.FindByCompanyIdAsync(companyId, skip, take, orderBy);

            if (result.Count == 0) return new ListResponse<BranchResponse> { ResponseHeader = new ResponseHeader { Message = "No Record/Result found" } };

            return new ListResponse<BranchResponse>
            {
                ResponseBodyList = result.Select(x => new BranchResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedBy = $"{x.CreatedUser.FirstName} {x.CreatedUser.MiddleName} {x.CreatedUser.LastName}",
                    CreatedOn = x.CreatedOn
                }),
                ResponseHeader = new ResponseHeader
                {
                    Success = true,
                    ReferenceNumber = _unitOfWork.BranchRepository.TotalBranchesNumber(companyId).ToString()
                }
            };
        }
    }
}
