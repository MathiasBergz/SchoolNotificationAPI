using SchoolNotificationAPI.Domain.Entities;

namespace SchoolNotificationAPI.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Guid> CreateAsync(Student student);

    Task<IEnumerable<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(Guid id);

    Task UpdateAsync(Student student);

    Task DeleteAsync(Guid id);
}