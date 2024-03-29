﻿using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace PointOS.DataAccess.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _dbContext;
        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public BranchRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        ///  Gets Branches as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Branch> GetQueryable()
            => _dbContext.Branches.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Add/Attach a new Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        public async Task AddAsync(Branch branch)
            => await _dbContext.Branches.AddAsync(branch);

        /// <summary>
        /// Attach changes made to a Branch's record into repository
        /// </summary>
        /// <param name="branch"></param>
        public Task UpdateAsync(Branch branch)
        {
            _dbContext.Branches.Update(branch);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Gets Branches filtering by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<List<Branch>> FindByCompanyIdAsync(int companyId, int skip, int take, string orderBy) =>
            await GetQueryable()
                .Where(b => b.CompanyId == companyId)
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(take)
                .Select(b=> new Branch
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedOn = b.CreatedOn,
                    CreatedUser = new ApplicationUser
                    {
                        FirstName = b.CreatedUser.FirstName,
                        MiddleName = b.CreatedUser.MiddleName,
                        LastName = b.CreatedUser.LastName
                    }
                })
                .ToListAsync();

        /// <summary>
        /// Gets the total number of branches by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int TotalBranchesNumber(int companyId) 
            => GetQueryable().Count(b => b.CompanyId == companyId);

        ///// <summary>
        ///// Gets Branches filtering by company Id
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //public async Task<List<BranchResponse>> FindByCompanyIdAsync(int companyId) =>
        //    await GetQueryable().Where(b => b.CompanyId == companyId).Select(r => new BranchResponse
        //    {
        //        Id = r.Id,
        //        Name = r.Name,
        //        CreatedBy = r.CreatedUser.FirstName,
        //        CreatedOn = r.CreatedOn
        //    }).ToListAsync();

    }
}
