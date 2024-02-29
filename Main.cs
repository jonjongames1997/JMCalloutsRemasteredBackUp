using System.Linq;
using System.Reflection;
using JMCalloutsRemastered.Callouts;

[assembly: Rage.Attributes.Plugin("JMCalloutsRemastered", Description = "LSPDFR Callout Pack", Author = "OfficerMorrison")]
namespace JMCalloutsRemastered
{
    public class Main : Plugin
    {
        public static bool CalloutInterface;
        public static bool StopThePed;
        public static bool UltimateBackup;

        public override void Finally() { }

        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;
            JMCalloutsRemastered.Settings.LoadSettings();
        }
        static void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
                GameFiber.StartNew(delegate
                {
                    RegisterCallouts();
                    Game.Console.Print();
                    Game.Console.Print("=============================================== JM Callouts Remastered by OfficerMorrison ================================================");
                    Game.Console.Print();
                    Game.Console.Print("[LOG]: Callouts and settings were loaded successfully.");
                    Game.Console.Print("[LOG]: The config file was loaded successfully.");
                    Game.Console.Print("[VERSION]: Detected Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    Game.Console.Print("[LOG]: Checking for a new JM Callouts Remastered version...");
                    Game.Console.Print();
                    Game.Console.Print("[LOG]: Checking to see if user has Callout Interface 1.4.1, Stop The Ped, and Ultimate Backup Installed...");
                    Game.Console.Print("=============================================== JM Callouts Remastered by OfficerMorrison ================================================");
                    Game.Console.Print();


                    Game.DisplayNotification("web_jonjongames", "web_jonjongames", "JM Callouts Remastered", "~g~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~r~by OfficerMorrison", "~b~successfully loaded!");
                    Game.DisplayNotification("I am ~y~NO LONGER SUPPORTING Old versions of JM Callouts Remasterd~w~. Delete any old versions you have and update to ~g~Latest~w~ build. Thank you! - JJG.");

                    VersionChecker.VersionChecker.IsUpdateAvailable();
                    GameFiber.Wait(300);
                });
        }
        private static void RegisterCallouts()
        {
            Game.Console.Print();
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
            if(Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("CalloutInterface")) == true)
            {
                Game.LogTrivial("User does have Callout Interface 1.4.1 installed. Starting integration.....");
                CalloutInterface = true;
            }
            else
            {
                Game.LogTrivial("User does not have Callout Interface 1.4.1 Installed. Stopping integration....");
                CalloutInterface = false;
            }
            if(Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("StopThePed")) == true)
            {
                Game.LogTrivial("User does have Stop The Ped installed. Starting integration......");
                StopThePed = true;
            }
            else
            {
                Game.LogTrivial("User does not have Stop The Ped installed. Stopping integration.......");
                StopThePed = false;
            }
            if(Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("UltimateBackup")) == true)
            {
                Game.LogTrivial("User does have Ultimate Backup installed. Starting integration......");
                UltimateBackup = true;
            }
            else
            {
                Game.LogTrivial("User does not have Ultimate Backup installed. Stopping integration......");
                UltimateBackup = false;
            }
            if (Settings._911HangUp) { Functions.RegisterCallout(typeof(_911HangUp)); }
            if (Settings.CodeKaren) { Functions.RegisterCallout(typeof(CodeKaren)); }
            if (Settings.IllegalCampfireOnPublicBeach) { Functions.RegisterCallout(typeof(IllegalCampfireOnPublicBeach)); }
            if (Settings.IllegalProstitution) { Functions.RegisterCallout(typeof(IllegalProstitution)); }
            if (Settings.IntoxicatedIndividual) { Functions.RegisterCallout(typeof(IntoxicatedIndividual)); }
            if (Settings.PossibleProstitution) { Functions.RegisterCallout(typeof(PossibleProstitution)); }
            if (Settings.PublicDisturbance) { Functions.RegisterCallout(typeof(PublicDisturbance)); }
            if (Settings.RefuseToLeave) { Functions.RegisterCallout(typeof(RefuseToLeave)); }
            if (Settings.RefuseToPay) { Functions.RegisterCallout(typeof(RefuseToPay)); }
            if (Settings.Soliciting) { Functions.RegisterCallout(typeof(Soliciting)); }
            if (Settings.TrespassingOnPrivateProperty) { Functions.RegisterCallout(typeof(TrespassingOnPrivateProperty)); }
            if (Settings.TrespassingOnRailRoadProperty) { Functions.RegisterCallout(typeof(TrespassingOnRailRoadProperty)); }
            if (Settings.TrespassingOnConstructionProperty) { Functions.RegisterCallout(typeof(TrespassingOnConstructionProperty)); }
            if (Settings.LostIndividual) { Functions.RegisterCallout(typeof(LostIndividual)); }
            if (Settings.UnauthorizedAccessMovieStudio) { Functions.RegisterCallout(typeof(UnauthorizedAccessMovieStudio)); }
            if (Settings.DeadBody) { Functions.RegisterCallout(typeof(DeadBody)); }
            if (Settings.SolicitingDelPerroPier) { Functions.RegisterCallout(typeof(SolicitingDelPerroPier)); }
            if (Settings.WrecklessDriving) { Functions.RegisterCallout(typeof(WrecklessDriving)); }
            if (Settings.StolenEmergencyVehicle) { Functions.RegisterCallout(typeof(StolenEmergencyVehicle)); }
            if (Settings.TrespassingOnSchoolGrounds) { Functions.RegisterCallout(typeof(TrespassingOnSchoolGrounds)); }
            if (Settings.PersonOnTheHighway) { Functions.RegisterCallout(typeof(PersonOnTheHighway)); }
            if (Settings.StolenConstructionEquipment) { Functions.RegisterCallout(typeof(StolenConstructionEquipment)); }
            if (Settings.DomesticDisturbance) { Functions.RegisterCallout(typeof(DomesticDisturbance)); }
            if (Settings.StolenMilitaryEquipment) { Functions.RegisterCallout(typeof(StolenMilitaryEquipment)); }
            if (Settings.MovieHopping) { Functions.RegisterCallout(typeof(MovieHopping)); }
            Game.Console.Print("[LOG]: All callouts of the JMCalloutsRemastered.ini were loaded successfully.");
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
        }
    }
}