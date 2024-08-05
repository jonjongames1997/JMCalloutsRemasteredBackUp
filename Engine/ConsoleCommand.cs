using Rage.Attributes;

namespace JMCalloutsRemastered.Engine
{
    
    public static class ConsoleCommand
    {
        [Rage.Attributes.ConsoleCommand]
        public static void Command_JMCalloutsReloadConfig()
        {
            Settings.LoadSettings();
        }
    }
}
