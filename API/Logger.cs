using System.Runtime.CompilerServices;

namespace JMCalloutsRemastered.API
{
    // Credit to Rohit for the code. github.com/Rohit685/MysteriousCallouts/blob/master/HelperSystems/Logger.cs

    internal static class Logger
    {
        // Thanks Khori
        internal static void Error(Exception ex, [CallerFilePath] string p = "", [CallerMemberName] string m = "", [CallerLineNumber] int l = 0) =>
            Game.LogTrivial($"[ERROR] JM Callouts Remastered: An error occured at '{p} {m} line {l}' - {ex}");

        internal static void Debug(string msg)
        {
            if (DebugMode)
            {
                Game.LogTrivial($"[DEBUG] JM Callouts Remastered: {msg}");
            }
        }

        internal static void Normal(string msg) => Game.LogTrivial($"[NORMAL] JM Callouts Remastered: {msg}");
    }
}
