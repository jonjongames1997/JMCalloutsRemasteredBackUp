using System;
using Rage;
using System.Reflection;

namespace JMCalloutsRemastered.API
{
    internal static class Logger
    {
        public static void Error(string message) 
        {
            var asmName = Assembly.GetExecutingAssembly().FullName.Split('.').First();
            Game.Console.Print($"{asmName}: An error has occured while using JM Callouts Remastered. See log for details.");
            Game.Console.Print("====================== ERROR! ======================");
            Game.Console.Print();
            Game.Console.Print(message);
            Game.Console.Print();
            Game.Console.Print("====================== ERROR! ======================");
        }

        public static void Warning(string message)
        {
            var asmName = Assembly.GetExecutingAssembly().FullName.Split('.').First();
            Game.Console.Print($"{asmName}: A minor issue has occured while using JM Callouts Remastered. See log for details.");
            Game.Console.Print("====================== WARNING! ======================");
            Game.Console.Print();
            Game.Console.Print(message);
            Game.Console.Print();
            Game.Console.Print("====================== WARNING! ======================");
        }

        public static void Info(string message)
        {
            var asmName = Assembly.GetCallingAssembly().FullName.Split(',').First();
            Game.Console.Print($"{asmName}: {message}");
        }
    }
}
