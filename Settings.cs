using System.Windows.Forms;
using Rage;
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
        internal static bool ClownWithADeadlyWeapon = true;
        internal static bool UnauthorizedAccessMovieStudio = true;
        internal static bool DerangedLover = true;
        internal static bool DeadBody = true;
        internal static bool SuspiciousPerson = true;
        internal static bool PersonWithAWeapon = true;
        internal static bool GangRivalryShootout = true;
        internal static bool RocketMan = true;
        internal static bool ActiveAIBackup = true;
        internal static bool HelpMessages = true;
        internal static Keys EndCall = Keys.End;
        internal static Keys Dialog = Keys.Y;

        internal static void LoadSettings()
        {
            Game.LogTrivial("[LOG]: Loading config file from JM Callouts Remastered.");
            var path = "Plugins/LSPDFR/JMCalloutsRemastered.ini";
            var ini = new InitializationFile(path);
            ini.Create();
            Game.LogTrivial("Initializing Config for JMCalloutsRemastered");
            Settings.CodeKaren = ini.ReadBoolean("Callouts", "CodeKaren", true);
            Settings.IllegalCampfireOnPublicBeach = ini.ReadBoolean("Callouts", "IllegalCampfireOnPublicBeach", true);
            Settings.IllegalProstitution = ini.ReadBoolean("Callouts", "IllegalProstitution", true);
            Settings.IntoxicatedIndividual = ini.ReadBoolean("Callouts", "IntoxicatedIndividual", true);
            Settings.PersonWithAKnife = ini.ReadBoolean("Callouts", "PersonWithAKnife", true);
            Settings.PossibleProstitution = ini.ReadBoolean("Callouts", "PossibleProstitution", true);
            Settings.PublicDisturbance = ini.ReadBoolean("Callouts", "PublicDisturbance", false);
            Settings.RefuseToLeave = ini.ReadBoolean("Callouts", "RefuseToLeave", true);
            Settings.Soliciting = ini.ReadBoolean("Callouts", "Soliciting", true);
            Settings.TrespassingOnPrivateProperty = ini.ReadBoolean("Callouts", "TrespassingOnPrivateProperty", true);
            Settings.TrespassingOnRailRoadProperty = ini.ReadBoolean("Callouts", "TrespassingOnRailRoadProperty", true);
            Settings._911HangUp = ini.ReadBoolean("Callouts", "_911HangUp", true);
            Settings.TrespassingOnConstructionProperty = ini.ReadBoolean("Callouts", "TrespassingOnConstructionProperty", true);
            Settings.LostIndividual = ini.ReadBoolean("Callouts", "LostIndividual", true);
            Settings.ClownWithADeadlyWeapon = ini.ReadBoolean("Callouts", "ClownWithADeadlyWeapon", true);
            Settings.UnauthorizedAccessMovieStudio = ini.ReadBoolean("Callouts", "UnauthorizedAccessMovieStudio", true);
            Settings.DerangedLover = ini.ReadBoolean("Callouts", "DerangedLover", true);
            Settings.DeadBody = ini.ReadBoolean("Callouts", "Dead Body", true);
            Settings.SuspiciousPerson = ini.ReadBoolean("Callouts", "Suspicious Person", true);
            Settings.PersonWithAWeapon = ini.ReadBoolean("Callouts", "Person With A Weapon", true);
            Settings.GangRivalryShootout = ini.ReadBoolean("Callouts", "Gang Rivalry Shootout", true);
            Settings.RocketMan = ini.ReadBoolean("Callots", "RocketMan", true);
            Settings.ActiveAIBackup = ini.ReadBoolean("General", "ActiveAIBackup", true);
            Settings.HelpMessages = ini.ReadBoolean("HelpMessages", "Help Messages", true);
            EndCall = ini.ReadEnum("Keys", "EndCall", Keys.End);
            Dialog = ini.ReadEnum("Keys", "Dialog", Keys.Y);
        }
        public static readonly string PluginVersion = "3.5.6";
    }
}
