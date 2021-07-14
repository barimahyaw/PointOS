using Microsoft.EntityFrameworkCore;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _dbContext;
        /// <summary>
        /// constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public DashboardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        ///// <summary>
        ///// select all data needed to populate the welcome dashboard
        ///// </summary>
        ///// <returns></returns>
        //public async Task<WelcomeResponse> WelcomeDataAsync(string userName)
        //{
        //    var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        //    var entity = await _dbContext.Employees.Where(x => x.CreatedUser.Id == user.CreatedUserId
        //                                                       || x.CreatedUser.Id == user.Id || x.CreatedUser.CreatedUser.Id == user.Id)
        //        .Select(x => new WelcomeResponse
        //        {
        //            CompanyId = x.Branch.CompanyId,
        //            BranchId = x.BranchId,
        //            CompanyAbbrev = x.Branch.Company.NameAbbrev,
        //            CompanyName = x.Branch.Company.CompanyName,
        //            BranchName = x.Branch.BranchName,
        //            FirstName = x.FirstName,
        //            FullName = $"{x.FirstName} {x.MiddleName} {x.Surname}",
        //            LogoPath = x.Branch.Company.LogoPath,
        //            PhotoPath = x.PhotoPath,
        //            EmployeeId = x.Id,
        //            EmpCount = x.Branch.Employees.Count
        //        })
        //        .FirstOrDefaultAsync();

        //    return entity;
        //}

        /// <summary>
        /// select all data needed to populate the welcome dashboard
        /// </summary>
        /// <returns></returns>
        public async Task<WelcomeResponse> WelcomeDataAsync(string userName, bool newUser)
        {
            var data = _dbContext.Branches.Where(c => c.CreatedUser.UserName == userName);

            if (data.ToList().Count == 0) return new WelcomeResponse();

            var response = await data.Select(x => new WelcomeResponse
            {
                UserId = x.CreatedUserId,
                CompanyId = x.Company.Id,
                BranchId = x.Id,
                //CompanyAbbrev = x.Company.NameAbbrev,
                CompanyName = x.Company.Name,
                BranchName = x.Name,
                FirstName = x.CreatedUser.FirstName,
                FullName = $"{x.CreatedUser.FirstName} {x.CreatedUser.MiddleName} {x.CreatedUser.LastName}",
                //LogoPath = x.Company.LogoPath,
                EmpCount = 0
            }).FirstOrDefaultAsync();

            return response;
        }
    }
}
