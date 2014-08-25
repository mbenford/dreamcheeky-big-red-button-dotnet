using System;
using DreamCheeky.BigRedButton;

namespace Test.Application
{
    class Program
    {
        static void Main()
        {
            using (var bigRedButton = new DreamCheekyBigRedButton())
            {
                bigRedButton.LidClosed += (sender, args) => Console.WriteLine("Lid closed");
                bigRedButton.LidOpen += (sender, args) => Console.WriteLine("Lid open");
                bigRedButton.ButtonPressed += (sender, args) => Console.WriteLine("Button pressed");

                bigRedButton.Start();

                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
            }
        }
    }
}
