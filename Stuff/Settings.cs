using System;
using System.Linq;
using System.Reflection;
using LSPD_First_Response.Mod.API;
using System.Windows.Forms;
using System.Xml;
using Rage;

namespace JMCalloutsRemastered
{
    internal static class Settings
    {

        public static KeysConverter kc = new KeysConverter();
        public static Keys EndCall = Keys.End;
        public static bool _911HangUp = true;
        public static Keys Dialog = Keys.E;
        public static bool AbandonedCarOnRailroadTracks = true;
        public static bool BountyHunterRequiringAssistance = false;
        public static bool CodeKaren = true;
        public static bool DeadBody = true;
        public static bool DomesticDisturbance = true;
        public static bool DomesticDisturbanceGrapeSeed = true;
        public static bool DomesticDisturbanceHarmony = true;
        public static bool DomesticDisturbanceSandyShores = true;
        public static bool DomesticDisturbancePaletoBay = true;
        public static bool FirstAmendmentAuditors = true;
        public static bool HomelessPerson = true;
        public static bool IllegalCampfireOnPublicBeach = true;
        public static bool IllegalProstitution = true;
        public static bool InjuredPerson = true;
        public static bool IntoxicatedIndividual = true;
        public static bool LostIndividual = true;
        public static bool MovieHopping = false;
        public static bool PeepingTom = true;
        public static bool PersonCarryingAnExplosiveWeapon = true;
        public static bool PersonOnTheHighway = true;
        public static bool PossibleProstitution = true;
        public static bool PrankCall = true;
        public static bool PrivateTimeComplaint = true;
        public static bool PublicDisturbance = true;
        public static bool PublicOrdinanceEasterEgg1 = true;
        public static bool PublicOrdinanceEasterEgg2 = true;
        public static bool PurpleAlert = true;
        public static bool RefuseToLeave = true;
        public static bool RefuseToPay = true;
        public static bool Rocketman = true;
        public static bool Soliciting = true;
        public static bool SolicitingDelPerroPier = true;
        public static bool Stalking = true;
        public static bool StolenConstructionEquipment = true;
        public static bool StolenEmergencyVehicle = true;
        public static bool StolenMilitaryEquipment = true;
        public static bool SuspiciousPerson = true;
        public static bool TheTacoDispute = true;
        public static bool TrespassingOnConstructionProperty = true;
        public static bool TrespassingOnPrivateProperty = true;
        public static bool TrespassingOnRailRoadProperty = true;
        public static bool TrespassingOnSchoolGrounds = true;
        public static bool UnauthorizedAccessMovieStudio = true;
        public static bool BicycleBlockingRoadway = true;
        public static bool WarningMessages = true;
        public static bool HelpMessages = true;



        public static InitializationFile initialiseFile()
        {
            InitializationFile initializationFile = new InitializationFile("Plugins/LSPDFR/JMCalloutsRemastered.ini");
            initializationFile.Create();
            return initializationFile;
        }

        internal static void LoadSettings()
        {
            try
            {
                Game.LogTrivial("[JMCallouts LOG]: Loading JMCalloutsRemastered.ini settings......");
                Settings.Dialog = (Keys)Settings.kc.ConvertFromString(Settings.initialiseFile().ReadString("Keys", "Dialog", "E"));
                Settings.EndCall = (Keys)Settings.kc.ConvertFromString(Settings.initialiseFile().ReadString("Keys", "EndCall", "End"));
                Settings._911HangUp = Settings.initialiseFile().ReadBoolean("Callouts", "_911HangUp", true);
                Settings.AbandonedCarOnRailroadTracks = Settings.initialiseFile().ReadBoolean("Callouts", "AbandonedCarOnRailroadTracks", true);
                Settings.BountyHunterRequiringAssistance = Settings.initialiseFile().ReadBoolean("Callouts", "BountyHunterRequiringAssistance", true);
                Settings.CodeKaren = Settings.initialiseFile().ReadBoolean("Callouts", "CodeKaren", true);
                Settings.DeadBody = Settings.initialiseFile().ReadBoolean("Callouts", "DeadBody", true);
                Settings.DomesticDisturbance = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbance", true);
                Settings.DomesticDisturbanceGrapeSeed = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbanceGrapeSeed", true);
                Settings.DomesticDisturbanceHarmony = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbanceHarmony", true);
                Settings.DomesticDisturbancePaletoBay = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbancePaletoBay", true);
                Settings.DomesticDisturbanceSandyShores = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbanceSandyShores", true);
                Settings.DomesticDisturbanceSandyShores = Settings.initialiseFile().ReadBoolean("Callouts", "DomesticDisturbanceSandyShores", true);
                Settings.FirstAmendmentAuditors = Settings.initialiseFile().ReadBoolean("Callouts", "FirstAmendmentAuditor", true);
                Settings.HomelessPerson = Settings.initialiseFile().ReadBoolean("Callouts", "HomelessPerson", true);
                Settings.IllegalCampfireOnPublicBeach = Settings.initialiseFile().ReadBoolean("Callouts", "IllegalCampfireOnPublicBeach", true);
                Settings.IllegalProstitution = Settings.initialiseFile().ReadBoolean("Callouts", "IllegalProstitution", true);
                Settings.InjuredPerson = Settings.initialiseFile().ReadBoolean("Callouts", "InjuredPerson", true);
                Settings.IntoxicatedIndividual = Settings.initialiseFile().ReadBoolean("Callouts", "IntoxicatedIndividual", true);
                Settings.LostIndividual = Settings.initialiseFile().ReadBoolean("Callouts", "LostIndividual", true);
                Settings.MovieHopping = Settings.initialiseFile().ReadBoolean("Callouts", "MovieHopping", true);
                Settings.PeepingTom = Settings.initialiseFile().ReadBoolean("Callouts", "PeepingTom", true);
                Settings.PersonCarryingAnExplosiveWeapon = Settings.initialiseFile().ReadBoolean("Callouts", "PersonCarryingAnExplosiveWeapon", true);
                Settings.PersonOnTheHighway = Settings.initialiseFile().ReadBoolean("Callouts", "PersonOnTheHighway", true);
                Settings.PossibleProstitution = Settings.initialiseFile().ReadBoolean("Callouts", "PossibleProstitution", true);
                Settings.PrankCall = Settings.initialiseFile().ReadBoolean("Callouts", "PrankCall", true);
                Settings.PublicDisturbance = Settings.initialiseFile().ReadBoolean("Callouts", "PublicDisturbance", true);
                Settings.PublicOrdinanceEasterEgg1 = Settings.initialiseFile().ReadBoolean("Callouts", "PublicOrdinanceEasterEgg1", true);
                Settings.PublicOrdinanceEasterEgg2 = Settings.initialiseFile().ReadBoolean("Callouts", "PublicOrdinanceEasterEgg2", true);
                Settings.PurpleAlert = Settings.initialiseFile().ReadBoolean("Callouts", "PurpleAlert", true);
                Settings.RefuseToLeave = Settings.initialiseFile().ReadBoolean("Callouts", "RefuseToLeave", true);
                Settings.RefuseToPay = Settings.initialiseFile().ReadBoolean("Callouts", "RefuseToPay", true);
                Settings.Rocketman = Settings.initialiseFile().ReadBoolean("Callouts", "Rocketman", true);
                Settings.Soliciting = Settings.initialiseFile().ReadBoolean("Callouts", "Soliciting", true);
                Settings.SolicitingDelPerroPier = Settings.initialiseFile().ReadBoolean("Callouts", "SolicitingDelPerroPier", true);
                Settings.Stalking = Settings.initialiseFile().ReadBoolean("Callouts", "Stalking", true);
                Settings.StolenConstructionEquipment = Settings.initialiseFile().ReadBoolean("Callouts", "StolenConstructionEquipment", true);
                Settings.StolenEmergencyVehicle = Settings.initialiseFile().ReadBoolean("Callouts", "StolenEmergencyVehicle", true);
                Settings.StolenMilitaryEquipment = Settings.initialiseFile().ReadBoolean("Callouts", "StolenMilitaryEquipment", true);
                Settings.SuspiciousPerson = Settings.initialiseFile().ReadBoolean("Callouts", "SuspiciousPerson", true);
                Settings.TheTacoDispute = Settings.initialiseFile().ReadBoolean("Callouts", "TheTacoDispute", true);
                Settings.TrespassingOnConstructionProperty = Settings.initialiseFile().ReadBoolean("Callouts", "TrespassingOnConstructionProperty", true);
                Settings.TrespassingOnPrivateProperty = Settings.initialiseFile().ReadBoolean("Callouts", "TrespassingOnPrivateProperty", true);
                Settings.TrespassingOnRailRoadProperty = Settings.initialiseFile().ReadBoolean("Callouts", "TrespassingOnRailRoadProperty", true);
                Settings.TrespassingOnSchoolGrounds = Settings.initialiseFile().ReadBoolean("Callouts", "TrespassingOnSchoolGrounds", true);
                Settings.UnauthorizedAccessMovieStudio = Settings.initialiseFile().ReadBoolean("Callouts", "UnauthorizedAccessMovieStudio", false);
                Settings.BicycleBlockingRoadway = Settings.initialiseFile().ReadBoolean("Callouts", "BicycleBlockingRoadway", true);
                Settings.PrivateTimeComplaint = Settings.initialiseFile().ReadBoolean("Callouts", "PrivateTimeComplaint", true);
                Settings.WarningMessages = Settings.initialiseFile().ReadBoolean("Misc", "WarningMessages", true);
                Settings.HelpMessages = Settings.initialiseFile().ReadBoolean("Help Messages", "HelpMessages", true);
            }
            catch (Exception ex)
            {
                Settings.Dialog = Keys.E;
                Settings.EndCall = Keys.End;
                Settings._911HangUp = true;
                Settings.AbandonedCarOnRailroadTracks = true;
                Settings.CodeKaren = true;
                Settings.DeadBody = true;
                Settings.DomesticDisturbance = true;
                Settings.DomesticDisturbanceGrapeSeed = true;
                Settings.DomesticDisturbanceHarmony = true;
                Settings.DomesticDisturbancePaletoBay = true;
                Settings.DomesticDisturbanceSandyShores = true;
                Settings.FirstAmendmentAuditors = true;
                Settings.HomelessPerson = true;
                Settings.BountyHunterRequiringAssistance = false;
                Settings.IllegalCampfireOnPublicBeach = true;
                Settings.IllegalProstitution = true;
                Settings.InjuredPerson = true;
                Settings.IntoxicatedIndividual = true;
                Settings.LostIndividual = true;
                Settings.MovieHopping = false;
                Settings.PeepingTom = true;
                Settings.PersonCarryingAnExplosiveWeapon = true;
                Settings.PersonOnTheHighway = true;
                Settings.PossibleProstitution = true;
                Settings.PrankCall = true;
                Settings.PublicDisturbance = true;
                Settings.PublicOrdinanceEasterEgg1 = true;
                Settings.PublicOrdinanceEasterEgg2 = true;
                Settings.PurpleAlert = true;
                Settings.RefuseToLeave = true;
                Settings.RefuseToPay = true;
                Settings.Rocketman = true;
                Settings.Soliciting = true;
                Settings.SolicitingDelPerroPier = true;
                Settings.Stalking = true;
                Settings.StolenConstructionEquipment = true;
                Settings.StolenEmergencyVehicle = true;
                Settings.StolenMilitaryEquipment = true;
                Settings.SuspiciousPerson = true;
                Settings.TheTacoDispute = true;
                Settings.TrespassingOnConstructionProperty = true;
                Settings.TrespassingOnPrivateProperty = true;
                Settings.TrespassingOnRailRoadProperty = true;
                Settings.TrespassingOnSchoolGrounds = true;
                Settings.UnauthorizedAccessMovieStudio = false;
                Settings.BicycleBlockingRoadway = true;
                Settings.PrivateTimeComplaint = true;
                Settings.WarningMessages = true;
                Settings.HelpMessages = true;
                Game.LogTrivial(ex.ToString());
                Game.LogTrivial("[JMCallouts LOG]: Could not load settings. Default values set.");
            }
        }
        public static readonly string PluginVersion = "5.0.9.2";
    }
}
