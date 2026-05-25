namespace SchoolNotificationAPI.Domain.Entities
{
    public class NotificationRecipient
    {
        public Guid Id { get; private set; }

        public Guid NotificationId { get; private set; }

        public Guid StudentId { get; private set; }

        public string ContactPhone { get; private set; }

        public DateTime? ReadAt { get; private set; }

        private NotificationRecipient()
        {
        }

        public NotificationRecipient(
            Guid notificationId,
            Guid studentId,
            string contactPhone)
        {
            Id = Guid.NewGuid();
            NotificationId = notificationId;
            StudentId = studentId;
            ContactPhone = contactPhone;
        }

        public void MarkAsRead()
        {
            ReadAt = DateTime.UtcNow;
        }
    }
}
