using eViSeM.Common.DTO.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class UserAccountBusiness : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>, IUserAccountBusiness
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserAccountBusiness(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Add/Create a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> AddUser(UserRegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.EmailAddress,
                Email = request.EmailAddress,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.Surname,
                Gender = (int)request.Gender,
                PhoneNumber = request.PhoneNumber,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new ResponseHeader
                {
                    ReferenceNumber = user.Id,
                    Message = token,
                    Success = true,
                    StatusCode = (int)Status.Created
                };
            }

            var errors = result.Errors.Select(identityError => identityError.Description).ToList();

            return new ResponseHeader { Message = string.Join('|', errors) };
        }

        /// <summary>
        /// Account Email confirmation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return new ResponseHeader { StatusCode = 404, Message = "Invalid account confirmation token" };

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new ResponseHeader { StatusCode = 111, Message = "Invalid account confirmation token" };

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ? new ResponseHeader { Message = "Email confirmed successfully. ", Success = true }
            : new ResponseHeader { Message = "Email Cannot be Confirmed" };
        }
        /// <summary>
        /// Gets all registered users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<UserResponse>> GetAllUsers(CancellationToken cancellationToken)
        {
            var entities = await _userManager.Users.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);

            var users = new List<UserResponse>();
            Parallel.ForEach(entities, entity =>
            {
                var user = new UserResponse
                {
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    Surname = entity.LastName,
                    //Gender = (Gender)entity.Gender,
                    Email = entity.Email,
                    UserName = entity.UserName
                };
                users.Add(user);
            });

            return users;
        }

        /// <summary>
        /// Gets all users for a particular role by roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<UserResponse>> GetRoleUsers(string roleId, CancellationToken cancellationToken)
        {
            var entities = await _userManager.GetUsersInRoleAsync(roleId);

            var userResponse = entities.Select(entity => new UserResponse
            {
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                Surname = entity.LastName,
                Email = entity.Email,
                Gender = (Gender)entity.Gender,
                PhoneNumber = entity.PhoneNumber,
                RoleName = roleId,
                //PhotoUrl = entity.PhotoUrl,
                UserName = entity.UserName,
                //Department = entity.Department
            }).ToList();

            return userResponse;
        }

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> AuthenticationAsync(AuthenticationRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,
                request.RememberMe, true);

            if (result.Succeeded)
            {
                //// check whether a company exist for the user
                //var sessionData = await _unitOfWork.DashboardRepository.WelcomeDataAsync(request.UserName);

                //if (sessionData == null) return new ResponseHeader {StatusCode = 700};

                //var userSession = new UserSession
                //{
                //    CompanyId = sessionData.CompanyId,
                //    Company = sessionData.CompanyName,
                //    EmployeeId = sessionData.EmployeeId,
                //    BranchId = sessionData.BranchId,
                //    Branch = sessionData.BranchName,
                //    FullName = sessionData.FullName,
                //    PhotoPath = sessionData.PhotoPath,
                //    LogoPath = sessionData.LogoPath
                //};

                //return new ResponseHeader { Success = true, StatusCode = 200, Data = userSession };
            }

            if (result.IsLockedOut) return new ResponseHeader
            {
                Message = "Access Denied. Account Blocked",
                StatusCode = 900
            };

            if (result.IsNotAllowed) return new ResponseHeader
            {
                Message = "Access Denied. Authentication not allowed.",
                StatusCode = 901
            };

            return new ResponseHeader { StatusCode = 902, Message = "Invalid Login Attempt!!!" };
        }

        /// <summary>
        /// User Account Sign Out
        /// </summary>
        /// <returns></returns>
        public async Task SignOutAsync() => await _signInManager.SignOutAsync();

        /// <summary>
        /// Change user account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded) return new ResponseHeader { Message = "", Success = true, StatusCode = 205 };

            var errors = result.Errors.Select(identityError => identityError.Description).ToList();

            return new ResponseHeader { Message = string.Join('|', errors), StatusCode = 903 };
        }

        /// <summary>
        /// Request forgot password handler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                return new ResponseHeader { Message = "Email is not Confirmed", StatusCode = 111 };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new ResponseHeader { Success = true, Message = token };
        }

        /// <summary>
        /// Reset Account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (user == null) return new ResponseHeader { StatusCode = 404 };

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded) return new ResponseHeader { Success = true, Message = "" };

            foreach (var error in result.Errors)
                return new ResponseHeader { Message = string.Join("|", error.Description) };

            return new ResponseHeader();
        }
        /// <summary>
        /// Gets a user by user name or user Id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserResponse> FindUserAsync(string userName, string userId)
        {
            var entity = string.IsNullOrWhiteSpace(userName) ? await _userManager.FindByIdAsync(userId)
                : await _userManager.FindByNameAsync(userName);

            var userResponse = new UserResponse
            {
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                Surname = entity.LastName,
                Email = entity.Email,
                //Gender = (Gender)entity.Gender,
                PhoneNumber = entity.PhoneNumber,
                //RoleName = entity.RoleName,
                //PhotoUrl = entity.PhotoUrl,
                UserName = entity.UserName,
                //Department = entity.Department
            };

            return userResponse;
        }

        /// <summary>
        /// Add/Create User Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> AddUserRoleAsync(UserRoleRequest request)
        {
            var identityRole = new IdentityRole { Name = request.RoleName };
            var result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded) return new ResponseHeader
            {
                Message = string.Format(Status.Created.GetAttributeStringValue(), $"Role, {request.RoleName}"),
                Success = true,
                StatusCode = (int)Status.Created
            };

            var errors = result.Errors.Select(identityError => identityError.Description).ToList();

            return new ResponseHeader { Message = string.Join('|', errors) };
        }

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<UserRoleResponse>> GetRolesAsync(CancellationToken cancellationToken)
        {
            //var entities = await _unitOfWork.CustomUserRepository
            //    .GetRoleUserLists("f9589be3-9946-4927-9f00-1134b7fe198f", cancellationToken);

            var entities = await _roleManager.Roles.ToListAsync(cancellationToken);

            var result = entities.Select(x => new UserRoleResponse
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToList();

            return result;
        }

        /// <summary>
        /// Gets all roles and indicate the roles of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<UserRoleResponse>> GetUserRoles(string userId, CancellationToken cancellationToken)
        {
            var userRoles = new List<UserRoleResponse>();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return userRoles;

            foreach (var role in await _roleManager.Roles.ToListAsync(cancellationToken))
            {
                var userRole = new UserRoleResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                    NormalizedName = role.NormalizedName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }

                userRoles.Add(userRole);
            }

            return userRoles;
        }

        /// <summary>
        /// Assign and/or Un assign roles to a user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> CreateUserRoleAssignmentAsync(List<UserRoleAssignmentRequest> request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new ResponseHeader { Message = "", StatusCode = 404, Success = false };

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
                return new ResponseHeader { Message = $"Cannot remove existing user, {user.UserName}'s roles", Success = false, StatusCode = 400 };

            result = await _userManager.AddToRolesAsync(user, request.Where(x => x.IsSelected)
                .Select(y => y.RoleName));

            return result.Succeeded ? new ResponseHeader { Message = "", Success = true, StatusCode = 2001 } : new ResponseHeader();
        }

        /// <summary>
        /// Registers custom/generic claims
        /// </summary>
        /// <param name="claimValues"></param>
        /// <param name="claimName"></param>
        public void GenericClaims(string[] claimValues, string claimName)
        {
            var userClaims = new GenericIdentity("Welcome");
            userClaims.AddClaim(new Claim("CompanyName", claimValues[0]));
        }
    }
}
