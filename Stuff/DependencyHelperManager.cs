using JMCalloutsRemastered.Engine;
using System.IO;

namespace JMCalloutsRemastered.Stuff
{
    internal static class DependencyHelperManager
    {
        private static string _missingFiles = string.Empty;

        internal static bool VerifyDependencies()
        {

            if (!JMCalloutsRemasteredAPI.IsAssemblyAvailable("SceneManager.dll", "2.3.3")) _missingFiles += "~n~- SceneManager.dll";
            if (!File.Exists("InputManager.dll")) _missingFiles += "~n~ InputManager.dll";
            //if (!File.Exists("irrKlang.NET4.dll")) _missingFiles += "~n~ irrKlang.NET4.dll"

            if (_missingFiles.Length <= 0) return true;
            Normal($"Failed to load because of these required files were not found: {_missingFiles.Replace("~n~", "")}"); // note to astro: replacing ~n~ is important otherwise the log will look weird
            Game.DisplayNotification("commonmenu", "mp_alerttriangle", "RiskierTrafficStops", "~r~Missing files!", $"These files were not found: ~y~{_missingFiles}");
            //Game.UnloadActivePlugin(); // note to astro: prevents FileNotFoundException from being sent or textures not being seen.
            return false; // note to astro: returns the IsUpdateAvailable method to false, make sure this is the first thing in the if-statement otherwise other things will return true, or add '&& missingFiles.Length < 0' to those statements, it's personal preference

        }
    }
}
