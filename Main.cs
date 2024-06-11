using System.Reflection;
using JMCalloutsRemastered.Callouts;
using StopThePed;
using UltimateBackup;

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
                    Game.Console.Print("=============================================== JM Callouts Remastered by OfficerMorrison ================================================");
                    Game.Console.Print();


                    Game.DisplayNotification("web_jonjongames", "web_jonjongames", "JM Callouts Remastered", "~g~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~r~by OfficerMorrison", "~b~successfully loaded!");

                    GameFiber.Wait(300);

                    bool helpMessages = Settings.HelpMessages;
                    if (helpMessages)
                    {
                        Game.DisplayHelp("You can disable the help messages in JMCalloutsRemastered.ini at any time.");
                    }
                    StatsDisplay.DisplayStats();
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
            bool _911HangUp = Settings._911HangUp;
            if (Settings._911HangUp) { Functions.RegisterCallout(typeof(_911HangUp)); }
            bool codeKaren = Settings.CodeKaren;
            if (Settings.CodeKaren) { Functions.RegisterCallout(typeof(CodeKaren)); }
            bool illegalCampfireOnPublicBeach = Settings.IllegalCampfireOnPublicBeach;
            if (Settings.IllegalCampfireOnPublicBeach) { Functions.RegisterCallout(typeof(IllegalCampfireOnPublicBeach)); }
            bool illegalProstitution = Settings.IllegalProstitution;
            if (Settings.IllegalProstitution) { Functions.RegisterCallout(typeof(IllegalProstitution)); }
            bool intoxicatedIndividual = Settings.IntoxicatedIndividual;
            if (Settings.IntoxicatedIndividual) { Functions.RegisterCallout(typeof(IntoxicatedIndividual)); }
            bool possibleProstitution = Settings.PossibleProstitution;
            if (Settings.PossibleProstitution) { Functions.RegisterCallout(typeof(PossibleProstitution)); }
            bool publicDisturbance = Settings.PublicDisturbance;
            if (Settings.PublicDisturbance) { Functions.RegisterCallout(typeof(PublicDisturbance)); }
            bool refuseToLeave = Settings.RefuseToLeave;
            if (Settings.RefuseToLeave) { Functions.RegisterCallout(typeof(RefuseToLeave)); }
            bool refuseToPay = Settings.RefuseToPay;
            if (Settings.RefuseToPay) { Functions.RegisterCallout(typeof(RefuseToPay)); }
            bool soliciting = Settings.Soliciting;
            if (Settings.Soliciting) { Functions.RegisterCallout(typeof(Soliciting)); }
            bool trespassingOnPrivateProperty = Settings.TrespassingOnPrivateProperty;
            if (Settings.TrespassingOnPrivateProperty) { Functions.RegisterCallout(typeof(TrespassingOnPrivateProperty)); }
            bool trespassingOnRailRoadProperty = Settings.TrespassingOnRailRoadProperty;
            if (Settings.TrespassingOnRailRoadProperty) { Functions.RegisterCallout(typeof(TrespassingOnRailRoadProperty)); }
            bool trespassingOnConstructionProperty = Settings.TrespassingOnConstructionProperty;
            if (Settings.TrespassingOnConstructionProperty) { Functions.RegisterCallout(typeof(TrespassingOnConstructionProperty)); }
            bool lostIndividual = Settings.LostIndividual;
            if (Settings.LostIndividual) { Functions.RegisterCallout(typeof(LostIndividual)); }
            bool unauthorizedAccessMovieStudio = Settings.UnauthorizedAccessMovieStudio;
            if (Settings.UnauthorizedAccessMovieStudio) { Functions.RegisterCallout(typeof(UnauthorizedAccessMovieStudio)); }
            bool deadBody = Settings.DeadBody;
            if (Settings.DeadBody) { Functions.RegisterCallout(typeof(DeadBody)); }
            bool solicitingDelPerroPier = Settings.SolicitingDelPerroPier;
            if (Settings.SolicitingDelPerroPier) { Functions.RegisterCallout(typeof(SolicitingDelPerroPier)); }
            bool stolenEmergencyVehicle = Settings.StolenEmergencyVehicle;
            if (Settings.StolenEmergencyVehicle) { Functions.RegisterCallout(typeof(StolenEmergencyVehicle)); }
            bool trespassingOnSchoolGrounds = Settings.TrespassingOnSchoolGrounds;
            if (Settings.TrespassingOnSchoolGrounds) { Functions.RegisterCallout(typeof(TrespassingOnSchoolGrounds)); }
            bool personOnTheHighway = Settings.PersonOnTheHighway;
            if (Settings.PersonOnTheHighway) { Functions.RegisterCallout(typeof(PersonOnTheHighway)); }
            bool stolenConstructionEquipment = Settings.StolenConstructionEquipment;
            if (Settings.StolenConstructionEquipment) { Functions.RegisterCallout(typeof(StolenConstructionEquipment)); }
            bool domesticDisturbance = Settings.DomesticDisturbance;
            if (Settings.DomesticDisturbance) { Functions.RegisterCallout(typeof(DomesticDisturbance)); }
            bool abandonedVehicleOnRailRoadTracks = Settings.AbandonedVehicleOnRailRoadTracks;
            if (Settings.AbandonedVehicleOnRailRoadTracks) { Functions.RegisterCallout(typeof(AbandonedCarOnRailroadTracks)); }
            bool stalking = Settings.Stalking;
            if (Settings.Stalking) { Functions.RegisterCallout(typeof(Stalking)); }
            bool purpleAlert = Settings.PurpleAlert;
            if (Settings.PurpleAlert) { Functions.RegisterCallout(typeof(PurpleAlert)); }
            bool peepingTom = Settings.PeepingTom;
            if (Settings.PeepingTom) { Functions.RegisterCallout(typeof(PeepingTom)); }
            bool theTacoDispute = Settings.TheTacoDispute;
            if (Settings.TheTacoDispute) { Functions.RegisterCallout(typeof(TheTacoDispute)); }
            bool suspiciousPerson = Settings.SuspiciousPerson;
            if (Settings.SuspiciousPerson) { Functions.RegisterCallout(typeof(SuspiciousPerson)); }
            bool firstAmendmentAuditors = Settings.FirstAmendmentAuditors;
            if (Settings.FirstAmendmentAuditors) { Functions.RegisterCallout(typeof(FirstAmendmentAuditors)); }
            bool rocketman = Settings.Rocketman;
            if (Settings.Rocketman) { Functions.RegisterCallout(typeof(Rocketman)); }
            bool prankCall = Settings.PrankCall;
            if (Settings.PrankCall) { Functions.RegisterCallout(typeof(PrankCall)); }
            bool injuredPerson = Settings.InjuredPerson;
            if (Settings.InjuredPerson) { Functions.RegisterCallout(typeof(InjuredPerson)); }
            bool unknownTrouble = Settings.UnknownTrouble;
            if (Settings.UnknownTrouble) { Functions.RegisterCallout(typeof(UnknownTrouble)); }
            bool personCarryingAnExplosiveWeapon = Settings.PersonCarryingAnExplosiveWeapon;
            if (Settings.PersonCarryingAnExplosiveWeapon) { Functions.RegisterCallout(typeof(PersonCarryingAnExplosiveWeapon)); }
            bool sexyTimeInPublicNSFW = Settings.SexyTimeInPublicNSFW;
            if (Settings.SexyTimeInPublicNSFW) { Functions.RegisterCallout(typeof(SexyTimeInPublicNSFW)); }
            Game.Console.Print("[LOG]: All callouts of the JMCalloutsRemastered.ini were loaded successfully.");
            Game.Console.Print();
            Game.Console.Print("================================================== JM Callouts Remastered ===================================================");
            Game.Console.Print();
        }
    }
}