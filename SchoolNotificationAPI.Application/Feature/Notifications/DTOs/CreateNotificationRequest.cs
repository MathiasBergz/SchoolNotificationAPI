using SchoolNotificationAPI.Domain.Enums;

namespace SchoolNotificationAPI.Application.Feature.Notifications.DTOs
{
    public class CreateNotificationRequest
    {
        public NotificationType Type { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        public string Period { get; set; }

        public List<string>? Years { get; set; }

        public List<string>? GroupClasses { get; set; }

        public List<Guid>? StudentIds { get; set; }
    }
}
