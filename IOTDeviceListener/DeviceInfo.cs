using IOTDevice.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace IOTDeviceListener
{
    public class DeviceInfo
    {
        private const string FileName = "device.ihh";
        public async Task WriteDeviceFile(Device device)
        {
            var initfile = new StreamWriter(FileName);
            await initfile.WriteAsync(JsonConvert.SerializeObject(device));
            initfile.Close();
        }

        public bool DoesFileExists()
        {
            return File.Exists(FileName);
        }

        public async Task<Device> ReadDeviceFile()
        {
            var initfile = new StreamReader(FileName);
            var data = await initfile.ReadToEndAsync();
            return JsonConvert.DeserializeObject<Device>(data);
        }
    }
}
