using SchoolNotificationAPI.Application.Feature.Notifications.DTOs;
using SchoolNotificationAPI.Application.Feature.Notifications.Interfaces;
using SchoolNotificationAPI.Application.Feature.Students.Interfaces;
using SchoolNotificationAPI.Domain.Entities;

namespace SchoolNotificationAPI.Application.Feature.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        private readonly IStudentRepository _studentRepository;

        public NotificationService(
            INotificationRepository repository,
            IStudentRepository studentRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
        }

        public async Task CreateAsync(
            CreateNotificationRequest request)
        {
            var notification = new Notification(
                request.Type,
                request.Title,
                request.Content,
                request.ImageUrl);

            var students = await _studentRepository
                .GetTargetsAsync(
                    request.Period,
                    request.Years,
                    request.GroupClasses,
                    request.StudentIds);

            var recipients = new List<NotificationRecipient>();

            foreach (var student in students)
            {
                foreach (var contact in student.Contacts)
                {
                    recipients.Add(
                        new NotificationRecipient(
                            notification.Id,
                            student.Id,
                            contact.PhoneNumber));
                }
            }

            await _repository.CreateAsync(notification);

            await _repository.CreateRecipientsAsync(recipients);
        }

        public async Task<IEnumerable<ParentNotificationDto>>
            GetByPhoneAsync(string phone)
        {
            return await _repository.GetByPhoneAsync(phone);
        }

        public async Task MarkAsReadAsync(Guid recipientId)
        {
            await _repository.MarkAsReadAsync(recipientId);
        }
    }
}
