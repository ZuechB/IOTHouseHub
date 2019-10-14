namespace IOTDevice.Models
{
    public class Notification
    {
        public long? DeviceId { get; set; }
        public string Message { get; set; }
        public bool ShouldOpen { get; set; }
    }
}
