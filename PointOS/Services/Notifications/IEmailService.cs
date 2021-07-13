using PointOS.Common.DTO.Request;
using System.Threading.Tasks;

namespace PointOS.Services.Notifications
{
    public interface IEmailService
    {
        Task SendEmail(EmailRequest request);
    }
}