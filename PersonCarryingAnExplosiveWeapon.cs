using CalloutInterfaceAPI;

namespace JMCalloutsRemastered
{


    [CalloutInterface("[JM Callouts] Person Carrying An Explosive Weapon", CalloutProbability.High, "Citizen's report of an individual carrying an explosive weapon", "CODE 3", "SWAT")]

    public class PersonCarryingAnExplosiveWeapon : Callout
    {

        private static readonly string[] wepList = new string[] { "WEAPON_RPG", "WEAPON_GRENADELAUNCHER", "WEAPON_GRENADELAUNCHER_SMOKE", "WEAPON_FIREWORK", "WEAPON_HOMINGLAUNCHER", "WEAPON_COMPACTLAUNCHER", "WEAPON_GRENADE", "WEAPON_MOLOTOV", "WEAPON_STICKYBOMB", "WEAPON_PROXMINE", "WEAPON_PIPEBOMB" };
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip suspectBlip;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_SWAT_UNITS_01 WE_HAVE_01 CRIME_ASSAULT_WITH_A_DEADLY_WEAPON UNITS_RESPOND_CODE_03_02", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Multiple reports of an explosive device");
            CalloutMessage = "An idividual with a deadly weapon reported by civilian";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Person Carrying An Explosive Weapon has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Person Carrying An Explosive Weapon", "~b~Dispatch~w~: Suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout");



            return base.OnCalloutAccepted();
        }
    }
}
