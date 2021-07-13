using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Services.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly IRestUtility _restUtility;

        public EmailService(IRestUtility restUtility)
        {
            _restUtility = restUtility;
        }

        public async Task SendEmail(EmailRequest request) => await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "Notification/SendEmail",
                null, request, null, Verb.Post);
    }
}
