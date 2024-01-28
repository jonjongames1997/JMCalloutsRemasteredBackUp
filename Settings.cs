using System.Windows.Forms;
using System.Collections.Generic;
using Rage;
using System.Xml;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Callouts;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace JMCalloutsRemastered
{
    internal static class Settings
    {
        internal static bool IllegalCampfireOnPublicBeach = true;
        internal static bool IllegalProstitution = true;
        internal static bool IntoxicatedIndividual = true;
        internal static bool PersonWithAKnife = true;
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
        internal static bool UnauthorizedAccessMovieStudio = true;
        internal static bool DerangedLover = true;
        internal static bool DeadBody = true;
        internal static bool PersonWithAWeapon = false;
        internal static bool GangRivalryShootout = true;
        internal static bool RocketMan = true;
        internal static bool SolicitingDelPerroPier = true;
        internal static bool OfficerDown = true;
        internal static bool VehicleFire = true;
        internal static bool MonkeyWithAWeapon = true;
        internal static bool WrecklessDriving = true;
        internal static bool StolenEmergencyVehicle = true;
        internal static bool TrespassingOnSchoolGrounds = true;
        internal static bool PersonOnTheHighway = true;
        internal static bool HelpMessages = true;
        internal static Keys EndCall = Keys.End;
        internal static Keys Dialog = Keys.E;
        internal static string CallSign;
        internal static string OfficerName = "Jonathan Morrison";
        internal static Keys InteractionKey1 = Keys.None;
        internal static Keys InteractionKey2 = Keys.None;
        internal static Keys StopThePedKey = Keys.E;
        internal static Keys StopThePedKey1 = Keys.None;
        internal static bool IsSTPKeyModifierSet;
        internal static InitializationFile ini;
        internal static string inipath = "Plugins/LSPDFR/JMCalloutsRemastered.ini";

        internal static void LoadSettings()
        {
            Game.Console.Print("[LOG]: Loading config file from JM Callouts Remastered.");
            ini = new InitializationFile(inipath);
            ini.Create();
            Game.LogTrivial("Initializing Config for JMCalloutsRemastered....");
            Settings.CodeKaren = ini.ReadBoolean("Callouts", "CodeKaren", true);
            Settings.IllegalCampfireOnPublicBeach = ini.ReadBoolean("Callouts", "IllegalCampfireOnPublicBeach", true);
            Settings.IllegalProstitution = ini.ReadBoolean("Callouts", "IllegalProstitution", true);
            Settings.IntoxicatedIndividual = ini.ReadBoolean("Callouts", "IntoxicatedIndividual", true);
            Settings.PersonWithAKnife = ini.ReadBoolean("Callouts", "PersonWithAKnife", true);
            Settings.PossibleProstitution = ini.ReadBoolean("Callouts", "PossibleProstitution", true);
            Settings.PublicDisturbance = ini.ReadBoolean("Callouts", "PublicDisturbance", true);
            Settings.RefuseToLeave = ini.ReadBoolean("Callouts", "RefuseToLeave", true);
            Settings.Soliciting = ini.ReadBoolean("Callouts", "Soliciting", true);
            Settings.TrespassingOnPrivateProperty = ini.ReadBoolean("Callouts", "TrespassingOnPrivateProperty", true);
            Settings.TrespassingOnRailRoadProperty = ini.ReadBoolean("Callouts", "TrespassingOnRailRoadProperty", true);
            Settings._911HangUp = ini.ReadBoolean("Callouts", "_911HangUp", true);
            Settings.TrespassingOnConstructionProperty = ini.ReadBoolean("Callouts", "TrespassingOnConstructionProperty", true);
            Settings.LostIndividual = ini.ReadBoolean("Callouts", "LostIndividual", true);
            Settings.UnauthorizedAccessMovieStudio = ini.ReadBoolean("Callouts", "UnauthorizedAccessMovieStudio", true);
            Settings.DerangedLover = ini.ReadBoolean("Callouts", "DerangedLover", true);
            Settings.DeadBody = ini.ReadBoolean("Callouts", "Dead Body", true);
            Settings.PersonWithAWeapon = ini.ReadBoolean("Callouts", "Person With A Weapon", true);
            Settings.GangRivalryShootout = ini.ReadBoolean("Callouts", "Gang Rivalry Shootout", true);
            Settings.RocketMan = ini.ReadBoolean("Callots", "RocketMan", true);
            Settings.SolicitingDelPerroPier = ini.ReadBoolean("Callouts", "SolicitingDelPerro", true);
            Settings.OfficerDown = ini.ReadBoolean("Callouts", "OfficerDown", true);
            Settings.VehicleFire = ini.ReadBoolean("Callouts", "VehicleFire", true);
            Settings.MonkeyWithAWeapon = ini.ReadBoolean("Callouts", "MonkeyWithAWeapon", true);
            Settings.WrecklessDriving = ini.ReadBoolean("Callouts", "RecklessDriving", true);
            Settings.StolenEmergencyVehicle = ini.ReadBoolean("Callouts", "StolenEmergencyVehicle", true);
            Settings.TrespassingOnSchoolGrounds = ini.ReadBoolean("Callouts", "TrespassingOnSchoolGrounds", true);
            Settings.PersonOnTheHighway = ini.ReadBoolean("Callouts", "PersonOnTheHighway", true);
            Settings.HelpMessages = ini.ReadBoolean("HelpMessages", "Help Messages", true);
            EndCall = ini.ReadEnum("Keys", "EndCall", Keys.End);
            Dialog = ini.ReadEnum("Keys", "Dialog", Keys.E);
            InteractionKey1 = ini.ReadEnum("Keys", "InteractionKey1", Keys.None);
            InteractionKey2 = ini.ReadEnum("Keys", "InteractionKey2", Keys.None);
            CallSign = ini.ReadString("Officer Settings", "Callsign", "1-Lincoln-5");
            OfficerName = ini.ReadString("Officer Settings", "OfficerName", "Jonathan Morrison");

            if (Main.STP)
            {
                var stpini = new InitializationFile("Plugins/LSPDFR/StopThePed.ini");
                StopThePedKey = stpini.ReadEnum("Keys", "StopPedKey", Keys.E);
                if (!stpini.ReadEnum("Keys", "StopPedModifierKey", Keys.None).Equals(Keys.None))
                {
                    StopThePedKey1 = stpini.ReadEnum("Keys", "StopPedModifierKey", Keys.None);
                    IsSTPKeyModifierSet = true;
                }
                else { IsSTPKeyModifierSet = false; }
            }
        }
        public static readonly string PluginVersion = "3.10.0.4";
    }
}
