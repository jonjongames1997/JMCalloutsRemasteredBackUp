﻿namespace JMCalloutsRemastered.VersionChecker
{
    public class PluginCheck
    {

        public static bool isUpdateAvailable()
        {
            string curVersion = Settings.PluginVersion;
            Uri latestVersionUri = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=43616&textOnly=1");
            WebClient webClient = new WebClient();
            string recieveData = string.Empty;
            try
            {
                recieveData = webClient.DownloadString(latestVersionUri).Trim();
            }
            catch (WebException)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~JM Callouts Remastered Warning", "~r~Failed to check for an update", "Please make sure you're ~y~connected~w~ to your WiFi Network or try to reload the plugin");
                Game.Console.Print();
                Game.Console.Print("===================================================== JM Callouts Remastered ===========================================");
                Game.Console.Print();
                Game.Console.Print("[WARNING!]: Failed to check for an update!");
                Game.Console.Print("[LOG]: Please make sure you are connected to the internet or try to reload the plugin.");
                Game.Console.Print();
                Game.Console.Print("==================================================== JM Callouts Remastered ============================================");
                Game.Console.Print();
                return false;
            }
            if(recieveData != Settings.PluginVersion)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~JM Callouts Remastered Warning", "~y~A new update is available!", "Current Version: ~r~" + curVersion + "~w~<br>New Version: ~y~" + recieveData + "<br>~r~Please Update to the latest build for new callouts and improvments! :-)");
                Game.Console.Print();
                Game.Console.Print("===================================================== JM Callouts Remastered ===========================================");
                Game.Console.Print();
                Game.Console.Print("[WARNING!]: A new version of JM Callouts Remastered is NOW AVAILABLE to download! Update to latest build! v4.5.3 or lower is ~r~NOT SUPPORTED~w~.");
                Game.Console.Print("[LOG]: Current Version:" + curVersion);
                Game.Console.Print("[LOG]: New Version:" + recieveData);
                Game.Console.Print();
                Game.Console.Print("===================================================== JM Callouts Remastered ===========================================");
                Game.Console.Print();
                return true;
            }
            else
            {
                Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "", "Detected the ~g~latest~w~ build of ~o~JM Callouts Remastered! Thank you for downloading! :-)");
                return false;
            }
        }

    }
}
