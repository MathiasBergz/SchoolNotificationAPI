using SchoolNotificationAPI.Application.Feature.Notifications.DTOs;

namespace SchoolNotificationAPI.Application.Feature.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(CreateNotificationRequest request);

        Task<IEnumerable<ParentNotificationDto>>
            GetByPhoneAsync(string phone);

        Task MarkAsReadAsync(Guid recipientId);
    }
}
