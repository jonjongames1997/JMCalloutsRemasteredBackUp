using Rage.Attributes;

namespace JMCalloutsRemastered
{
    public static class ConsoleCommand
    {
        [ConsoleCommand]
        public static void Command_JMCReloadConfig()
        {
            Settings.LoadSettings();
        }
    }
}
