using SchoolNotificationAPI.Application.Feature.StudentManagement.DTOs;

namespace SchoolNotificationAPI.Application.Feature.StudentManagement.Interfaces
{
    public interface IStudentService
    {
        Task<Guid> CreateAsync(RegisterStudentRequest request);

        Task<IEnumerable<StudentDto>> GetAllAsync();

        Task UpdateAsync(Guid id, UpdateStudentRequest request);

        Task DeleteAsync(Guid id);
    }
}
