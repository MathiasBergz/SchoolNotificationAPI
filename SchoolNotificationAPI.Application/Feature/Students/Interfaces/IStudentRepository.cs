using SchoolNotificationAPI.Domain.Entities;

namespace SchoolNotificationAPI.Application.Feature.Students.Interfaces;

public interface IStudentRepository
{
    Task<Guid> CreateAsync(Student student);

    Task<IEnumerable<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(Guid id);

    Task UpdateAsync(Student student);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Student>> GetTargetsAsync(
    string period,
    List<string>? years,
    List<string>? groupClasses,
    List<Guid>? studentIds);
}