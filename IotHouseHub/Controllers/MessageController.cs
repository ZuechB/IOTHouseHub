using System.Threading.Tasks;
using IOTDevice.Models;
using IotHouseHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace IotHouseHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        readonly IDeviceService deviceService;
        public MessageController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Notification notification)
        {
            await deviceService.BroadcastCommand(notification);
            return Ok();
        }
    }
}