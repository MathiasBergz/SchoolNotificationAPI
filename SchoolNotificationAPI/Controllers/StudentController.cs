using Microsoft.AspNetCore.Mvc;
using SchoolNotificationAPI.Application.Feature.StudentManagement.DTOs;
using SchoolNotificationAPI.Application.Feature.StudentManagement.Interfaces;

namespace SchoolNotificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] RegisterStudentRequest request)
        {
            var studentId = await _service.CreateAsync(request);

            return Ok(studentId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _service.GetAllAsync();

            return Ok(students);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateStudentRequest request)
        {
            await _service.UpdateAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}