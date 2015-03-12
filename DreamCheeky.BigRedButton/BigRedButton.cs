using System;
using System.Threading;

namespace DreamCheeky
{
    public class BigRedButton : IDisposable
    {
        private readonly Device device;

        private volatile bool terminated;
        private Thread thread;

        public TimeSpan PollingInterval { get; set; }

        public BigRedButton()
        {
            PollingInterval = TimeSpan.FromMilliseconds(100);
            device = new Device();
        }

        public void Start()
        {
            device.Open();
            thread = new Thread(ThreadCallback);
            thread.Start();
        }

        private void ThreadCallback()
        {
            var lastStatus = DeviceStatus.Unknown;

            while (!terminated)
            {
                DeviceStatus status = device.GetStatus();
                if (status != DeviceStatus.Errored)
                {
                    if (status == DeviceStatus.LidClosed && lastStatus == DeviceStatus.LidOpen)
                    {
                        OnLidClosed();
                    }
                    else if (status == DeviceStatus.ButtonPressed && lastStatus != DeviceStatus.ButtonPressed)
                    {
                        OnButtonPressed();
                    }
                    else if (status == DeviceStatus.LidOpen && lastStatus == DeviceStatus.LidClosed)
                    {
                        OnLidOpen();
                    }
                    
                    lastStatus = status;
                }
                Thread.Sleep(PollingInterval);
            }
        }

        public void Stop()
        {
            terminated = true;
            thread.Join(TimeSpan.FromSeconds(10));
            device.Close();
        }

        public void Dispose()
        {
            Stop();
        }

        private void OnLidOpen()
        {
            if (LidOpen != null)
            {
                LidOpen(this, EventArgs.Empty);
            }
        }

        private void OnLidClosed()
        {
            if (LidClosed != null)
            {
                LidClosed(this, EventArgs.Empty);
            }
        }

        private void OnButtonPressed()
        {
            if (ButtonPressed != null)
            {
                ButtonPressed(this, EventArgs.Empty);
            }
        }

        public EventHandler LidOpen;
        public EventHandler LidClosed;
        public EventHandler ButtonPressed;
    }
}
