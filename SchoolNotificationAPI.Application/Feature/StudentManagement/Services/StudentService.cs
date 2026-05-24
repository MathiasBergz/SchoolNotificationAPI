using SchoolNotificationAPI.Application.Feature.StudentManagement.DTOs;
using SchoolNotificationAPI.Application.Feature.StudentManagement.Interfaces;
using SchoolNotificationAPI.Application.Interfaces.Repositories;
using SchoolNotificationAPI.Domain.Entities;

namespace SchoolNotificationAPI.Application.Feature.StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(
            RegisterStudentRequest request)
        {
            var student = new Student(
                request.Name,
                request.Year,
                request.GroupClass,
                request.Period);

            foreach (var contact in request.Contacts)
            {
                student.AddContact(new StudentContact(
                    student.Id,
                    contact.ParentName,
                    contact.PhoneNumber,
                    contact.IsMainContact));
            }

            return await _repository.CreateAsync(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();

            return students.Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Year = student.Year,
                GroupClass = student.GroupClass,
                Period = student.Period,
                Contacts = student.Contacts.Select(contact =>
                    new StudentContactDto
                    {
                        ParentName = contact.ParentName,
                        PhoneNumber = contact.PhoneNumber,
                        IsMainContact = contact.IsMainContact
                    }).ToList()
            });
        }

        public async Task UpdateAsync(
            Guid id,
            UpdateStudentRequest request)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student is null)
                throw new Exception("Student not found");

            student.Update(
                request.Name,
                request.Year,
                request.GroupClass,
                request.Period);

            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
