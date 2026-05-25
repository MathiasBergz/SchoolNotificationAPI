using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNotificationAPI.Application.Feature.Notifications.Interfaces;

namespace SchoolNotificationAPI.Controllers.Parent
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentNotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public ParentNotificationsController(
            INotificationService service)
        {
            _service = service;
        }

        [HttpGet("{phone}")]
        public async Task<IActionResult> GetByPhone(
            string phone)
        {
            var notifications =
                await _service.GetByPhoneAsync(phone);

            return Ok(notifications);
        }

        [HttpPut("{recipientId}/read")]
        public async Task<IActionResult> MarkAsRead(
            Guid recipientId)
        {
            await _service.MarkAsReadAsync(recipientId);

            return NoContent();
        }
    }
}
