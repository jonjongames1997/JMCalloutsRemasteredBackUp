﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Homeless Man Loitering", CalloutProbability.Medium, "A homeless person loitering and refusing to leave", "CODE 2", "LSPD")]

    public class HomelessPerson : Callout
    {

        private static Ped suspect;
        private static Blip susBlip;
        private static Vector3 spawnpoint;
        private static string malefemale;
        private static int counter;

        public override void OnCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(130.16f, -1737.21f, 30.11f),
                new(94.63f, -217.37f, 54.49f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A homeless person loitering");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_CITIZEN_REQUESTING_REMOVAL_OF_BEGGARS UNITS_RESPOND_CODE_02_01", spawnpoint);
            CalloutMessage = "A homeless person loitering and refusing to leave by owner";
            CalloutPosition = spawnpoint;

            base.OnCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {

            Game.LogTrivial("[JM Callouts Remastered Log]: Homeless Person Loitering callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Homeless Person Loitering", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            susBlip = suspect.AttachBlip();
            suspect.Tasks.PutHandsUp(500, MainPlayer);
            susBlip.Color = System.Drawing.Color.Yellow;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if(susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();




        }


    }
}