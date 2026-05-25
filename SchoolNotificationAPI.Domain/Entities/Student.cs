namespace SchoolNotificationAPI.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Year { get; private set; }

        public string GroupClass { get; private set; }

        public string Period { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public List<StudentContact> Contacts { get; private set; } = [];

        private Student()
        {
        }

        public Student(
            string name,
            string year,
            string groupClass,
            string period)
        {
            Id = Guid.NewGuid();
            Name = name;
            Year = year;
            GroupClass = groupClass;
            Period = period;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddContact(StudentContact contact)
        {
            Contacts.Add(contact);
        }

        public void Update(
            string name,
            string year,
            string groupClass,
            string period)
        {
            Name = name;
            Year = year;
            GroupClass = groupClass;
            Period = period;
        }

        public void ClearContacts()
        {
            Contacts.Clear();
        }
    }
}
