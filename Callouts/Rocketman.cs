using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("Rocketman", CalloutProbability.High, "Reports of a civilian carrying an explosive weapon", "Code 3", "SWAT")]

    public class Rocketman : Callout
    {
        private static readonly string[] wepList = new string[] { "WEAPON_RPG", "WEAPON_HOMINGLAUNCHER", "WEAPON_FIREWORK" };
        private static Ped suspect;
        private static Blip susBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_BRANDISHING_WEAPON_01 UNITS_RESPOND_CODE_03_01", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Citizen's reporting a individual carrying an explosive weapon.");
            CalloutMessage = "An individual is threatening citizens with an explosive weapon.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Rocketman callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Rocketman", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Tasks.Wander();

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;
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
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {

                    }

                }

            }

        }
    }
}