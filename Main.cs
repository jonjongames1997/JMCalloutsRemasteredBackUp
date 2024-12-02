﻿using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.VersionChecker;
using LSPD_First_Response.Mod.Utils;

[assembly: Rage.Attributes.Plugin("JMCalloutsRemastered", Description = "LSPDFR Callout Pack", Author = "OfficerMorrison")]
namespace JMCalloutsRemastered
{

    public class Main : Plugin
    {
        public static bool CalloutInterface;
        public static bool StopThePed;
        public static bool UltimateBackup;

        public override void Initialize()
        {

            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;
            Settings.LoadSettings();
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
                    Game.Console.Print();
                    Game.Console.Print("For support, Join the official Jon Jon Games Discord: https://discord.gg/N9KgZx4KUn");
                    Game.Console.Print();
                    Game.Console.Print();
                    Game.Console.Print();
                    Game.Console.Print("=============================================== JM Callouts Remastered by OfficerMorrison ================================================");
                    Game.Console.Print();

                    Game.DisplayNotification("web_jonjongames", "web_jonjongames", "JM Callouts Remastered", "~g~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~r~by ~o~OfficerMorrison", "~b~successfully loaded!");
                
                #region Help Messages
                if (Settings.HelpMessages)
                {
                    Game.DisplayHelp("You can disable the help messages in JMCalloutsRemastered.ini at any time.");
                }
                else
                {
                    Settings.HelpMessages = false;
                }
                #endregion

                if (Settings.WarningMessages)
                {
                    Game.DisplayHelp("~y~JM Callouts Remasterd Warning: If this callout pack is redistributed on any other sites that aren'tapproved by JM Modifications, You'll be sent a DMCA Takedown Notice.");
                }
                else
                {
                    Settings.WarningMessages = false;
                }

                VersionChecker.PluginCheck.IsUpdateAvailable();

                GameFiber.Wait(300);
            });
        }
        private static void RegisterCallouts()
        {
            if (Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("CalloutInterface")) == true)
            {
                Game.LogTrivial("User has Callout Interface 1.4.1 by Opus INSTALLED. starting integration.......");
                CalloutInterface = true;
            }
            else
            {
                Game.LogTrivial("User do NOT have CalloutInterface installed. Stopping integration....");
                CalloutInterface = false;
            }
            if (Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("StopThePed")) == true)
            {
                Game.LogTrivial("User has StopThePed by Bejoijo Plugins installed. Starting integration.....");
                StopThePed = true;
            }
            else
            {
                Game.LogTrivial("User doe not have Stop The Ped by Bejoijo Plugins installed. Stopping integration....");
                StopThePed = false;
            }
            if (Functions.GetAllUserPlugins().ToList().Any(a => a != null && a.FullName.Contains("UltimateBackup")) == true)
            {
                Game.LogTrivial("User has Ultimate Backup by Bejoijo Plugins installed. Starting integration....");
                UltimateBackup = true;
            }
            else
            {
                Game.LogTrivial("User does not have Ultimate Backup by Bejoijo Plugins installed. Stopping integration....");
                UltimateBackup = false;
            }
            Game.Console.Print();
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
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
            if (Settings.DeadBody) { Functions.RegisterCallout(typeof(DeadBody)); }
            if (Settings.SolicitingDelPerroPier) { Functions.RegisterCallout(typeof(SolicitingDelPerroPier)); }
            if (Settings.StolenEmergencyVehicle) { Functions.RegisterCallout(typeof(StolenEmergencyVehicle)); }
            if (Settings.TrespassingOnSchoolGrounds) { Functions.RegisterCallout(typeof(TrespassingOnSchoolGrounds)); }
            if (Settings.PersonOnTheHighway) { Functions.RegisterCallout(typeof(PersonOnTheHighway)); }
            if (Settings.StolenConstructionEquipment) { Functions.RegisterCallout(typeof(StolenConstructionEquipment)); }
            if (Settings.DomesticDisturbance) { Functions.RegisterCallout(typeof(DomesticDisturbance)); }
            if (Settings.AbandonedCarOnRailroadTracks) { Functions.RegisterCallout(typeof(AbandonedCarOnRailroadTracks)); }
            if (Settings.Stalking) { Functions.RegisterCallout(typeof(Stalking)); }
            if (Settings.PurpleAlert) { Functions.RegisterCallout(typeof(PurpleAlert)); }
            if (Settings.PeepingTom) { Functions.RegisterCallout(typeof(PeepingTom)); }
            if (Settings.TheTacoDispute) { Functions.RegisterCallout(typeof(TheTacoDispute)); }
            if (Settings.SuspiciousPerson) { Functions.RegisterCallout(typeof(SuspiciousPerson)); }
            if (Settings.FirstAmendmentAuditors) { Functions.RegisterCallout(typeof(FirstAmendmentAuditors)); }
            if (Settings.Rocketman) { Functions.RegisterCallout(typeof(Rocketman)); }
            if (Settings.PrankCall) { Functions.RegisterCallout(typeof(PrankCall)); }
            if (Settings.InjuredPerson) { Functions.RegisterCallout(typeof(InjuredPerson)); }
            if (Settings.PersonCarryingAnExplosiveWeapon) { Functions.RegisterCallout(typeof(PersonCarryingAnExplosiveWeapon)); }
            if (Settings.BountyHunterRequiringAssistance) { Functions.RegisterCallout(typeof(BountyHunterRequiringAssistance)); }
            if (Settings.HomelessPerson) { Functions.RegisterCallout(typeof(HomelessPerson)); }
            if (Settings.DomesticDisturbanceGrapeseed) { Functions.RegisterCallout(typeof(DomesticDisturbanceGrapeSeed)); }
            if (Settings.DomesticDistrubancePaletoBay) { Functions.RegisterCallout(typeof(DomesticDisturbancePaletoBay)); }
            if (Settings.DomesticDisturbanceSandyShores) { Functions.RegisterCallout(typeof(DomesticDisturbanceSandyShores)); }
            if (Settings.DomesticDisturbanceHarmony) { Functions.RegisterCallout(typeof(DomesticDisturbanceHarmony)); }
            if (Settings.PublicOrdinanceEasterEgg1) { Functions.RegisterCallout(typeof(PublicOrdinanceEasterEgg1)); }
            if (Settings.PublicOrdinanceEasterEgg2) { Functions.RegisterCallout(typeof(PublicOrdinanceEasterEgg2)); }
            if (Settings.BicycleBlockingRoadway) { Functions.RegisterCallout(typeof(BicycleBlockingRoadway)); }
            Game.Console.Print("[LOG]: All callouts of the JMCalloutsRemastered.ini were loaded successfully.");
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
        }

        public override void Finally()
        {
            Game.LogTrivial("[JMCallouts LOG]: JM Callouts Remastered has successfully unloaded! Unregistering callouts.");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "JM Callouts Remastered", "By ~o~OfficerMorrison", "Successfully unloaded. I'm shocked you made it alive.");
        }
    }

}