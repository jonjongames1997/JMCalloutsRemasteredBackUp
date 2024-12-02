﻿using System.Windows.Forms;
using System.Xml;
using Rage;

namespace JMCalloutsRemastered
{
    internal static class Settings
    {
        internal static bool IllegalCampfireOnPublicBeach = true;
        internal static bool IllegalProstitution = true;
        internal static bool IntoxicatedIndividual = true;
        internal static bool _911HangUp = true;
        internal static bool PossibleProstitution = true;
        internal static bool PublicDisturbance = true;
        internal static bool RefuseToPay = true;
        internal static bool RefuseToLeave = true;
        internal static bool Soliciting = true;
        internal static bool TrespassingOnPrivateProperty = true;
        internal static bool TrespassingOnRailRoadProperty = true;
        internal static bool CodeKaren = true;
        internal static bool TrespassingOnConstructionProperty = true;
        internal static bool LostIndividual = true;
        internal static bool UnauthorizedAccessMovieStudio = false;
        internal static bool DeadBody = true;
        internal static bool SolicitingDelPerroPier = true;
        internal static bool WrecklessDriving = true;
        internal static bool StolenEmergencyVehicle = true;
        internal static bool TrespassingOnSchoolGrounds = true;
        internal static bool PersonOnTheHighway = true;
        internal static bool StolenConstructionEquipment = true;
        internal static bool DomesticDisturbance = true;
        internal static bool StolenMilitaryEquipment = true;
        internal static bool MovieHopping = false;
        internal static bool AbandonedVehicleOnRailRoadTracks = true;
        internal static bool Stalking = true;
        internal static bool PurpleAlert = true;
        internal static bool PeepingTom = true;
        internal static bool TheTacoDispute = true;
        internal static bool SuspiciousPerson = true;
        internal static bool FirstAmendmentAuditors = true;
        internal static bool Rocketman = true;
        internal static bool PrankCall = true;
        internal static bool InjuredPerson = true;
        internal static bool PersonCarryingAnExplosiveWeapon = false;
        internal static bool BountyHunterRequiringAssistance = false;
        internal static bool HomelessPerson = true;
        internal static bool DomesticDisturbanceGrapeseed = true;
        internal static bool DomesticDistrubancePaletoBay = true;
        internal static bool DomesticDisturbanceHarmony = true;
        internal static bool DomesticDisturbanceSandyShores = true;
        internal static bool PublicOrdinanceEasterEgg1 = true;
        internal static bool PublicOrdinanceEasterEgg2 = true;
        internal static bool AbandonedCarOnRailroadTracks = true;
        internal static bool BicycleBlockingRoadway = true;
        internal static bool HelpMessages = true;
        internal static bool WarningMessages = true;
        internal static Keys EndCall = Keys.End;
        internal static Keys Dialog = Keys.Y;
        internal static InitializationFile ini;
        internal static string inipath = "Plugins/LSPDFR/JMCalloutsRemastered.ini";

        internal static void LoadSettings()
        {
            Game.Console.Print("[LOG]: Loading config file from JM Callouts Remastered.");
            InitializationFile ini = new InitializationFile(inipath);
            ini.Create();
            Game.LogTrivial("Initializing Config for JMCalloutsRemastered....");
            Settings.CodeKaren = ini.ReadBoolean("Callouts", "CodeKaren", true);
            Settings.IllegalCampfireOnPublicBeach = ini.ReadBoolean("Callouts", "IllegalCampfireOnPublicBeach", true);
            Settings.IllegalProstitution = ini.ReadBoolean("Callouts", "IllegalProstitution", true);
            Settings.IntoxicatedIndividual = ini.ReadBoolean("Callouts", "IntoxicatedIndividual", true);
            Settings.PossibleProstitution = ini.ReadBoolean("Callouts", "PossibleProstitution", true);
            Settings.PublicDisturbance = ini.ReadBoolean("Callouts", "PublicDisturbance", true);
            Settings.RefuseToLeave = ini.ReadBoolean("Callouts", "RefuseToLeave", true);
            Settings.Soliciting = ini.ReadBoolean("Callouts", "Soliciting", true);
            Settings.TrespassingOnPrivateProperty = ini.ReadBoolean("Callouts", "TrespassingOnPrivateProperty", true);
            Settings.TrespassingOnRailRoadProperty = ini.ReadBoolean("Callouts", "TrespassingOnRailRoadProperty", true);
            Settings._911HangUp = ini.ReadBoolean("Callouts", "_911HangUp", true);
            Settings.TrespassingOnConstructionProperty = ini.ReadBoolean("Callouts", "TrespassingOnConstructionProperty", true);
            Settings.LostIndividual = ini.ReadBoolean("Callouts", "LostIndividual", true);
            Settings.UnauthorizedAccessMovieStudio = ini.ReadBoolean("Callouts", "UnauthorizedAccessMovieStudio", false);
            Settings.DeadBody = ini.ReadBoolean("Callouts", "DeadBody", true);
            Settings.SolicitingDelPerroPier = ini.ReadBoolean("Callouts", "SolicitingDelPerro", true);
            Settings.StolenEmergencyVehicle = ini.ReadBoolean("Callouts", "StolenEmergencyVehicle", true);
            Settings.TrespassingOnSchoolGrounds = ini.ReadBoolean("Callouts", "TrespassingOnSchoolGrounds", true);
            Settings.PersonOnTheHighway = ini.ReadBoolean("Callouts", "PersonOnTheHighway", true);
            Settings.StolenConstructionEquipment = ini.ReadBoolean("Callouts", "StolenConstructionEquipment", true);
            Settings.DomesticDisturbance = ini.ReadBoolean("Callouts", "DomesticDisturbance", true);
            Settings.StolenMilitaryEquipment = ini.ReadBoolean("Callouts", "StolenMilitaryEquipment", true);
            Settings.MovieHopping = ini.ReadBoolean("Callouts", "MovieHopping", false);
            Settings.Stalking = ini.ReadBoolean("Callouts", "Stalking", true);
            Settings.PurpleAlert = ini.ReadBoolean("Callouts", "PurpleAlert", true);
            Settings.PeepingTom = ini.ReadBoolean("Callouts", "PeepingTom", true);
            Settings.TheTacoDispute = ini.ReadBoolean("Callouts", "TheTacoDispute", true);
            Settings.SuspiciousPerson = ini.ReadBoolean("Callouts", "SuspiciousPerson", true);
            Settings.Rocketman = ini.ReadBoolean("Callouts", "Rocketman", true);;
            Settings.PrankCall = ini.ReadBoolean("Callouts", "PrankCall", true);
            Settings.InjuredPerson = ini.ReadBoolean("Callouts", "InjuredPerson", true);
            Settings.PersonCarryingAnExplosiveWeapon = ini.ReadBoolean("Callouts", "PersonCarryingAnExplosiveWeapon", false);
            Settings.BountyHunterRequiringAssistance = ini.ReadBoolean("Callouts", "BountyHunterRequiringAssistance", false);
            Settings.HomelessPerson = ini.ReadBoolean("Callouts", "HomelessPerson", true);
            Settings.DomesticDisturbanceGrapeseed = ini.ReadBoolean("Callouts", "DomesticDisturbanceGrapeseed", true);
            Settings.DomesticDistrubancePaletoBay = ini.ReadBoolean("Callouts", "DomesticDistrubancePaletoBay", true);
            Settings.DomesticDisturbanceHarmony = ini.ReadBoolean("Callouts", "DomesticDisturbanceHarmony", true);
            Settings.DomesticDistrubancePaletoBay = ini.ReadBoolean("Callouts", "DomesticDistrubancePaletoBay", true);
            Settings.PublicOrdinanceEasterEgg1 = ini.ReadBoolean("Callouts", "PublicOrdinanceEasterEgg1", true);
            Settings.PublicOrdinanceEasterEgg2 = ini.ReadBoolean("Callouts", "PublicOrdinanceEasterEgg2", true);
            Settings.AbandonedCarOnRailroadTracks = ini.ReadBoolean("Callouts", "AbandonedCarOnRailroadTracks", true);
            Settings.BicycleBlockingRoadway = ini.ReadBoolean("Callouts", "BicycleBlockingRoadway", true);
            Settings.HelpMessages = ini.ReadBoolean("Help Messages", "HelpMessages", true);
            Settings.WarningMessages = ini.ReadBoolean("Misc", "WarningMessages", true);
            Settings.EndCall = ini.ReadEnum<Keys>("Keys", "EndCall", Keys.End);
            Settings.Dialog = ini.ReadEnum<Keys>("Keys", "Dialog", Keys.Y);
        }
        public static readonly string PluginVersion = "5.0.10.5";
    }
}
