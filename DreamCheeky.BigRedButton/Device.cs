using System;
using System.Linq;
using HidLibrary;

namespace DreamCheeky
{
    class Device : IDisposable
    {
        private readonly byte[] statusCommand = { 0, 0, 0, 0, 0, 0, 0, 0, 2 };

        private readonly int vendorId = 0x1D34;
        private readonly int productId = 0x000D;
        private readonly HidDevice device;

        public Device()
        {
            device = HidDevices.Enumerate(vendorId, productId).FirstOrDefault();

            if (device == null)
                throw new InvalidOperationException("Device not found");
        }

        public void Open()
        {
            device.OpenDevice();
        }

        public void Close()
        {
            device.CloseDevice();
        }

        public DeviceStatus GetStatus()
        {
            if (!device.Write(statusCommand, 100))
            {
                return DeviceStatus.Errored;
            }

            HidDeviceData data = device.Read();
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return DeviceStatus.Errored;
            }

            return (DeviceStatus)data.Data[1];
        }

        public void Dispose()
        {
            Close();
        }
    }
}
