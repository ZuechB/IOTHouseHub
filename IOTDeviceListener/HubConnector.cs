using IOTDevice.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IOTDeviceListener
{
    public delegate void OnReceiveDelegate(Notification notification);

    public class HubConnector
    {
        public ConnectionState connectionState { get; set; }

        private HubConnection connection { get; set; }
        public event OnReceiveDelegate OnReceive;

        public async void Connect(string baseUrl, Device device)
        {
            connection = new HubConnectionBuilder()
                .WithUrl(baseUrl + "/deviceHub?token=" + device.Id)
                .Build();

            connection.Closed += Connection_Closed;

            connection.On<Notification>("sendToAll", (notification) =>
            {
                if (OnReceive != null)
                {
                    OnReceive(notification);
                }
            });

            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                return;
            }

            connectionState = ConnectionState.Connected;
        }

        public void Disconnect()
        {
            connectionState = ConnectionState.Disconnected;
        }

        private Task Connection_Closed(Exception arg)
        {
            return Task.Run(() => {
                connectionState = ConnectionState.Disconnected;
            });
        }
    }

    public enum ConnectionState
    {
        Disconnected,
        Connected
    }
}
