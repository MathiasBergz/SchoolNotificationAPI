using SchoolNotificationAPI.Application.Feature.Notifications.DTOs;
using SchoolNotificationAPI.Domain.Entities;

namespace SchoolNotificationAPI.Application.Feature.Notifications.Interfaces
{
    public interface INotificationRepository
    {
        Task CreateAsync(Notification notification);

        Task CreateRecipientsAsync(
            IEnumerable<NotificationRecipient> recipients);

        Task<IEnumerable<ParentNotificationDto>>
            GetByPhoneAsync(string phone);

        Task MarkAsReadAsync(Guid recipientId);
    }
}
