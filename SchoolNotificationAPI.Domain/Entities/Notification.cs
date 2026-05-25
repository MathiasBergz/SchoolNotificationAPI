using SchoolNotificationAPI.Domain.Enums;

namespace SchoolNotificationAPI.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; private set; }

        public NotificationType Type { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public string? ImageUrl { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private Notification()
        {
        }

        public Notification(
            NotificationType type,
            string title,
            string content,
            string? imageUrl)
        {
            Id = Guid.NewGuid();
            Type = type;
            Title = title;
            Content = content;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
