namespace SchoolNotificationAPI.Domain.Entities
{
    public class StudentContact
    {
        public Guid Id { get; private set; }

        public Guid StudentId { get; private set; }

        public string ParentName { get; private set; }

        public string PhoneNumber { get; private set; }

        public bool IsMainContact { get; private set; }

        private StudentContact()
        {
        }

        public StudentContact(
            Guid studentId,
            string parentName,
            string phoneNumber,
            bool isMainContact)
        {
            Id = Guid.NewGuid();
            StudentId = studentId;
            ParentName = parentName;
            PhoneNumber = phoneNumber;
            IsMainContact = isMainContact;
        }
    }
}
