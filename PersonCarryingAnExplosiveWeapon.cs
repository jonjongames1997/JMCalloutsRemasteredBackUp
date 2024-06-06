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


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
