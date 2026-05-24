namespace SchoolNotificationAPI.Application.Feature.StudentManagement.DTOs
{
    public class RegisterStudentRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Year { get; set; } = string.Empty;

        public string GroupClass { get; set; } = string.Empty;

        public string Period { get; set; } = string.Empty;

        public List<StudentContactDto> Contacts { get; set; } = [];
    }
}
