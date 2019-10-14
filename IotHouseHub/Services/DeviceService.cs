using IOTDevice.Models;
using IotHouseHub.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IotHouseHub.Services
{
    public interface IDeviceService
    {
        Task BroadcastCommand(Notification notification);
        Task<Device> RegisterDevice(string name);
    }

    public class DeviceService : IDeviceService
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly DatabaseContext context;

        public DeviceService(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public async Task BroadcastCommand(Notification notification)
        {
            if (notification.DeviceId != null)
            {
                var device = await context.Devices.Where(d => d.Id == notification.DeviceId.Value).FirstOrDefaultAsync();
                if (device != null)
                {
                    var chatService = (IHubContext<Hubs.DeviceHub>)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IHubContext<Hubs.DeviceHub>));
                    await chatService.Clients.Client(device.ConnectionId).SendAsync("sendToAll", notification);
                }
            }
            else
            {
                var chatService = (IHubContext<Hubs.DeviceHub>)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IHubContext<Hubs.DeviceHub>));
                await chatService.Clients.All.SendAsync("sendToAll", notification);
            }
        }

        public async Task<Device> RegisterDevice(string name)
        {
            var device = new Device()
            {
                Name = name
            };
            context.Devices.Add(device);
            await context.SaveChangesAsync();

            return device;
        }
    }
}
