namespace SchoolNotificationAPI.Application.Feature.Students.DTOs
{
    public class StudentContactDto
    {
        public string ParentName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsMainContact { get; set; }
    }
}
