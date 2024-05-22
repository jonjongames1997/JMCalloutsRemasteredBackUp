using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Domestic Disturbance - Harmony/Route 68", CalloutProbability.High, "Reports of a domestic disturbance", "Code 3", "BCSO")]

    public class DomesticDisturbance2 : Callout
    {
        private static Ped victim;
        private static Ped suspect;
        private static Blip vicBlip;
        private static Blip susBlip;
        private static Vector3 spawnPoint;
        private static Vector3 suspectSpawnpoint;
        private static float suspectHeading;
        private static float heading;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = new();
            heading = 178.95f;
            suspectSpawnpoint = new();
            suspectHeading = 205.89f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_DOMESTIC_DISTURBANCE UNITS_RESPOND_CODE_03_01", spawnPoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of 2 individuals having an argument.");
            CalloutMessage = "Manager reporting a domestic disturbance. 1 individual may be armed with a weapon.";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Domestic Disturbance - Harmony/Route 68 callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Route 68 Motel", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            victim = new Ped("IG_AMANDATOWNLEY", spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            suspect = new Ped("IG_TRACEYDISSANTO", suspectSpawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;

            vicBlip = victim.AttachBlip();
            vicBlip.Color = System.Drawing.Color.DodgerBlue;
            vicBlip.IsRouteEnabled = true;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();
            if (victim) victim.Delete();
            if (vicBlip) vicBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {


            base.Process();
        }
    }
}
