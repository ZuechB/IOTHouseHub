using System.Device.Gpio;

namespace IOTDeviceListener
{
    public class GPIOService
    {
        private const int pin = 17; // GPIO 17
        private GpioController controller;
        public GPIOService()
        {
            controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
        }

        public void Open()
        {
            controller.Write(pin, PinValue.High);
        }
        
        public void Close()
        {
            controller.Write(pin, PinValue.Low);
        }
    }
}
