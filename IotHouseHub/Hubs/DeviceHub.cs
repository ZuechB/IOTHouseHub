using IotHouseHub.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IotHouseHub.Hubs
{
    public class DeviceHub : Hub
    {
        readonly DatabaseContext context;
        readonly IHttpContextAccessor httpContextAccessor;

        public DeviceHub(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            var qs = HttpUtility.ParseQueryString(httpContextAccessor.HttpContext.Request.QueryString.Value);
            if (!String.IsNullOrWhiteSpace(qs["token"]))
            {
                var id = Convert.ToInt64(qs["token"]);
                var device = await context.Devices.Where(u => u.Id == id).FirstOrDefaultAsync();
                if (device != null)
                {
                    device.ConnectionId = Context.ConnectionId;
                }
                //else
                //{
                //    context.Devices.Add(new Device()
                //    {
                //        ConnectionId = Context.ConnectionId,
                //        Name = "Temp Name"
                //    });
                //}
                await context.SaveChangesAsync();

                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connection = await context.Devices.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefaultAsync();
            if (connection != null)
            {
                connection.ConnectionId = null;
                await context.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
