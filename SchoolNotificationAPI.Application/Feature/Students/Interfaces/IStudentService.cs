using SchoolNotificationAPI.Application.Feature.Students.DTOs;

namespace SchoolNotificationAPI.Application.Feature.Students.Interfaces
{
    public interface IStudentService
    {
        Task<Guid> CreateAsync(RegisterStudentRequest request);

        Task<IEnumerable<StudentDto>> GetAllAsync();

        Task UpdateAsync(Guid id, UpdateStudentRequest request);

        Task DeleteAsync(Guid id);
    }
}
