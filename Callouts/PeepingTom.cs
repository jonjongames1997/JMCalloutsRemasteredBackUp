using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Peeping Tom - SFW", CalloutProbability.High, "A citizen's report of a peeping tom", "Code 3", "LSPD")]

    public class PeepingTom : Callout
    {

        private static readonly string[] pedList = new string[] { "s_m_m_autoshop_01", "g_f_y_ballas_01", "g_m_y_ballaorig_01", "a_m_m_beach_01", "a_f_m_bodybuild_01", "ig_claypain" };
        private static Ped suspect;
        private static Blip suspectBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(),
                new(),
                new(),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_PEEPING_TOM UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of a peeping Tom. Suspect is armed";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Peeping Tom callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Peeping Tom", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Tasks.AimWeaponAt(MainPlayer, 500);
            suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.Chocolate;
            suspectBlip.IsRouteEnabled = true;

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
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }



    }
}
