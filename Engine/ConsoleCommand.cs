using Rage.Attributes;

namespace JMCalloutsRemastered.Engine
{
    public static class ConsoleCommand
    {
        [ConsoleCommand]
        public static void Command_JMCalloutsReloadConfig()
        {
            Settings.LoadSettings();
        }
    }
}
