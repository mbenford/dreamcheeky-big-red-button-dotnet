# Dream Cheeky's Big Red Button class for .NET

## Installing

To install DreamCheeky.BigRedButton package, run the following command in the Package Manager Console inside Visual Studio:

    Install-Package DreamCheeky.BigRedButton
    
The package currently depends on [hidlibrary](https://www.nuget.org/packages/hidlibrary/) (≥ 3.2.28).

## Usage

```csharp
using DreamCheeky;

class Program
{
    static void Main()
    {
        using (var bigRedButton = new BigRedButton())
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
```
