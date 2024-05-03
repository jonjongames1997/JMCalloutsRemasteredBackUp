using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Suspicious Person", CalloutProbability.Medium, "Reports of a suspicious person", "Code 2", "LSPD")]

    public class SuspiciousPerson : Callout
    {
        private static readonly string[] pedList = new string[] { "ig_abigail", "csb_anita", "ig_shley", "g_m_y_ballaeast_01", "a_m_y_beach_01" };
        private static readonly string[] wepList = new string[] { "WEAPON_BAT", "WEAPON_SWITCHBLADE", "WEAPON_PISTOL", "WEAPON_PISTOL_MK2", "WEPAON_COMBATPISTOL", "WEAPON_CARBINERIFLE", "WEAPON_TACTICALRIFLE" };
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;
        private static Blip susBlip;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_SUSPICIOUS_ACTIVITY UNITS_RESPOND_CODE_02_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Multiple reports of a suspicous person in the area.");
            CalloutMessage = "Callers report the suspect may be armed.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Suspicious Person callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remasterd", "~w~Suspicious Person", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.CornflowerBlue;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();


        }
    }
}
