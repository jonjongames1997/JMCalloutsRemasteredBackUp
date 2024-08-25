namespace JMCalloutsRemastered.API
{
    // Credit to Rohit for the code. github.com/Rohit685/MysteriousCallouts/blob/master/HelperSystems/Logger.cs

    internal static class Logger
    {
        internal static void Error(Exception ex, string location) => Game.LogTrivial($"[ERROR] JM Callouts Remastered: {ex}");

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
