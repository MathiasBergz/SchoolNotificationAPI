using Microsoft.AspNetCore.Mvc;
using SchoolNotificationAPI.Application.Feature.Notifications.DTOs;
using SchoolNotificationAPI.Application.Feature.Notifications.Interfaces;

namespace SchoolNotificationAPI.Controllers.Manager
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(
            INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateNotificationRequest request)
        {
            await _service.CreateAsync(request);

            return Ok();
        }
    }
}
