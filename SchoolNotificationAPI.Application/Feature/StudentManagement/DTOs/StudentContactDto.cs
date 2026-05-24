namespace SchoolNotificationAPI.Application.Feature.StudentManagement.DTOs
{
    public class StudentContactDto
    {
        public string ParentName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsMainContact { get; set; }
    }
}
