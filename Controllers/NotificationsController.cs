using BotashVMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotashVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly BotashPortalContext _portalContext;

        public NotificationsController(BotashPortalContext context)
        {
            _portalContext = context;
        }

        [HttpGet("{vendorID}")]
        public IActionResult Notifications(int vendorID)
        {
            try { 
            if (vendorID <= 0) return BadRequest("Invalid ID");

            var result = _portalContext
                .Notifications
                .ToList()
                .Where(n => n.VendorId == vendorID);

            return Ok(result); 
            
            }catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Send")]
        public IActionResult SendNotification([FromBody] Notification notification)
        {
            try { 
            
            var newNotification = _portalContext.Add(notification);
            _portalContext.SaveChanges();

                var notificationId = _portalContext
                        .Notifications
                        .OrderBy(v => v.Id)
                        .Last()
                        .Id;

                return Created("NotificationID", notificationId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
                //return StatusCode(500, "Internal Server Error occurred. Try again later or contact Site Administrator if error persists");
            }
        }

        [HttpPut("MarkAsRead/{notificationId}")]
        public IActionResult MarkAsRead(int notificationId)
        {
            if (notificationId <= 0) return BadRequest("Invalid ID");

            var notification = _portalContext
                .Notifications
                .SingleOrDefault(n => n.Id  == notificationId);

            if (notification == null) return BadRequest($"Notification with ID: {notificationId} does not exist");

            notification.MarkedAsRead = 1;
            _portalContext.SaveChanges();

            return NoContent();
        }
    }
}
