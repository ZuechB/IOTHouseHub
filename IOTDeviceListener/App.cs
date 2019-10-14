using Authsome;
using IOTDevice.Models;
using System;
using System.Threading.Tasks;

namespace IOTDeviceListener
{
    public class App
    {
        private const string baseUrl = "http://[yourserver]:84";

        private HubConnector hubConnector;
        private IAuthsomeService authsomeService;
        private DeviceInfo deviceInfo;
        private Device device;
        private GPIOService GPIOService;

        public async Task Start()
        {
            deviceInfo = new DeviceInfo();
            authsomeService = new AuthsomeService();
            GPIOService = new GPIOService();

            if (!deviceInfo.DoesFileExists())
            {
                var newDevice = await authsomeService.PostAsync<Device>(baseUrl + "/api/Device", new Device()
                {
                    Name = "MyIOTDevice"
                });

                if (newDevice.httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    device = newDevice.Content;

                    // stores the device file
                    await deviceInfo.WriteDeviceFile(device);

                    // informs the user the device has been registered
                    Console.WriteLine("device registered: " + device.Id);
                }
            }
            else
            {
                // the device has already been registered, read the information
                device = await deviceInfo.ReadDeviceFile();

                Console.WriteLine("device file found: " + device.Id);
            }

            hubConnector = new HubConnector();
            hubConnector.OnReceive += HubConnector_OnReceive;
            hubConnector.Connect(baseUrl, device);
        }

        private void HubConnector_OnReceive(Notification notification)
        {
            if (notification.ShouldOpen)
            {
                Console.WriteLine(notification.Message);
                GPIOService.Open();
            }
            else
            {
                Console.WriteLine(notification.Message);
                GPIOService.Close();
            }
        }

        public void Stop()
        {
            hubConnector.Disconnect();
        }
    }
}
