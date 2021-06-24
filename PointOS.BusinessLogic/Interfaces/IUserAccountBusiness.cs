using eViSeM.Common.DTO.Response;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface IUserAccountBusiness
    {
        /// <summary>
        /// Add/Create a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> AddUser(UserRegistrationRequest request);

        /// <summary>
        /// Account Email confirmation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseHeader> ConfirmEmail(string userId, string token);

        /// <summary>
        /// Gets all registered users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<UserResponse>> GetAllUsers(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all users for a particular role by roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<UserResponse>> GetRoleUsers(string roleId, CancellationToken cancellationToken);

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        Task<ResponseHeader> AuthenticationAsync(AuthenticationRequest request, bool isLogin = false);

        /// <summary>
        /// User Account Sign Out
        /// </summary>
        /// <returns></returns>
        Task SignOutAsync();

        /// <summary>
        /// Change user account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> ChangePasswordAsync(ChangePasswordRequest request);

        /// <summary>
        /// Request forgot password handler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> ForgotPasswordAsync(ForgotPasswordRequest request);

        /// <summary>
        /// Reset Account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> ResetPasswordAsync(ResetPasswordRequest request);

        /// <summary>
        /// Gets a user by user name or user Id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserResponse> FindUserAsync(string userName, string userId);

        /// <summary>
        /// Add/Create User Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> AddUserRoleAsync(UserRoleRequest request);

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<UserRoleResponse>> GetRolesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all roles and indicate the roles of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<UserRoleResponse>> GetUserRoles(string userId, CancellationToken cancellationToken);

        /// <summary>
        /// Assign and/or Un assign roles to a user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseHeader> CreateUserRoleAssignmentAsync(List<UserRoleAssignmentRequest> request, string userId);

        /// <summary>
        /// Registers custom claims
        /// </summary>
        /// <param name="claimValues"></param>
        /// <param name="claimName"></param>
        void GenericClaims(string[] claimValues, string claimName);
    }
}