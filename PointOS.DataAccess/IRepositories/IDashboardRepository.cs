using System.Threading.Tasks;
using PointOS.Common.DTO.Response;

namespace PointOS.DataAccess.IRepositories
{
    public interface IDashboardRepository
    {
        /// <summary>
        /// select all data needed to populate the welcome dashboard
        /// </summary>
        /// <returns></returns>
        Task<WelcomeResponse> WelcomeDataAsync(string userName, bool newUser);
    }
}