using RealEstateRefactored.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RealEstateRefactored
{
    public class AppRunner : IAppRunner
    {
        public void StartApp()
        {
            Console.WriteLine("Hello");
            string command;
            while ((command = Console.ReadLine()) != null)
            {
                if (command.ToLowerInvariant() == "exit")
                    break;
            }
        }
    }
}
