using System;

namespace IOTDeviceListener
{
    class Program
    {
        static void Main(string[] args)
        {
            // start the app code
            App app = new App();
            app.Start();

            Console.WriteLine("Application started");

            // look out for escape key to kill the app
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.Escape);

            app.Stop();
            Console.WriteLine("Application ended");
        }
    }
}
