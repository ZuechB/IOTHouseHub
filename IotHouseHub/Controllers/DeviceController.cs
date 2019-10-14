using System.Threading.Tasks;
using IOTDevice.Models;
using IotHouseHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace IotHouseHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        readonly IDeviceService deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Device device)
        {
            return Ok(await deviceService.RegisterDevice(device.Name));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to IOT House Hub!");
        }
    }
}