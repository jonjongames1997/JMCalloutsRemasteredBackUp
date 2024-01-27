using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Xml;
using LSPD_First_Response.Mod.API;
using Rage;
using RAGENativeUI.Elements;
using Rage.ConsoleCommands;
using System.Runtime.InteropServices;
using Rage.Native;
using Rage.Attributes;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.VersionChecker;

[assembly: Rage.Attributes.Plugin("JMCalloutsRemastered", Description = "LSPDFR Callout Pack", Author = "OfficerMorrison")]
namespace JMCalloutsRemastered
{
    public class Main : Plugin
    {
        public static bool CalloutInterface;
        public static bool STP;

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
                    Game.Console.Print("=============================================== JM Callouts Remastered by OfficerMorrison ================================================");
                    Game.Console.Print();


                    Game.DisplayNotification("web_jonjongames", "web_jonjongames", "JM Callouts Remastered", "~g~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~r~by OfficerMorrison", "~b~successfully loaded!");
                    Game.DisplayNotification("JM Callouts Remastered is looking for Playtesters to test out future versions. If interested, join ~y~OfficerMorrison's~w~ ~o~Official Discord~w~.");

                    VersionChecker.VersionChecker.isUpdateAvailable();
                    GameFiber.Wait(300);
                });
        }
        private static void RegisterCallouts()
        {
            Game.Console.Print();
            if (Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("CalloutInterface")) == true)
            {
                Game.LogTrivial("User has CalloutInterface INSTALLED starting integration with Callout Interface by Opus49");
                CalloutInterface = true;
            }
            else
            {
                Game.LogTrivial("User do NOT have CalloutInterface installed.");
                CalloutInterface = false;
            }
            if (Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("StopThePed")) == true)
            {
                Game.LogTrivial("User has StopThePed INSTALLED starting integration with Stop The Ped by Bejoijo...");
                STP = true;
            }
            else
            {
                Game.LogTrivial("User do NOT have StopThePed installed.....");
                STP = false;
            }
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
            if (Settings._911HangUp) { Functions.RegisterCallout(typeof(_911HangUp)); }
            if (Settings.CodeKaren) { Functions.RegisterCallout(typeof(CodeKaren)); }
            if (Settings.IllegalCampfireOnPublicBeach) { Functions.RegisterCallout(typeof(IllegalCampfireOnPublicBeach)); }
            if (Settings.IllegalProstitution) { Functions.RegisterCallout(typeof(IllegalProstitution)); }
            if (Settings.IntoxicatedIndividual) { Functions.RegisterCallout(typeof(IntoxicatedIndividual)); }
            if (Settings.PersonWithAKnife) { Functions.RegisterCallout(typeof(PersonWithAKnife)); }
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
            if (Settings.DerangedLover) { Functions.RegisterCallout(typeof(DerangedLover)); }
            if (Settings.DeadBody) { Functions.RegisterCallout(typeof(DeadBody)); }
            if (Settings.PersonWithAWeapon) { Functions.RegisterCallout(typeof(PersonWithAWeapon)); }
            if (Settings.GangRivalryShootout) { Functions.RegisterCallout(typeof(GangRivalryShootout)); }
            if (Settings.RocketMan) { Functions.RegisterCallout(typeof(RocketMan)); }
            if (Settings.SolicitingDelPerroPier) { Functions.RegisterCallout(typeof(SolicitingDelPerroPier)); }
            if (Settings.OfficerDown) { Functions.RegisterCallout(typeof(OfficerDown)); }
            if (Settings.VehicleFire) { Functions.RegisterCallout(typeof(VehicleFire)); }
            if (Settings.MonkeyWithAWeapon) { Functions.RegisterCallout(typeof(MonkeyWithAWeapon)); }
            if (Settings.WrecklessDriving) { Functions.RegisterCallout(typeof(WrecklessDriving)); }
            if (Settings.StolenEmergencyVehicle) { Functions.RegisterCallout(typeof(StolenEmergencyVehicle)); }
            if (Settings.TrespassingOnSchoolGrounds) { Functions.RegisterCallout(typeof(TrespassingOnSchoolGrounds)); }
            Game.Console.Print("[LOG]: All callouts of the JMCalloutsRemastered.ini were loaded successfully.");
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
        }
    }
}