using SchoolNotificationAPI.Domain.Enums;

namespace SchoolNotificationAPI.Application.Feature.Notifications.DTOs
{
    public class ParentNotificationDto
    {
        public Guid Id { get; set; }

        public NotificationType Type { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ReadAt { get; set; }
    }
}
