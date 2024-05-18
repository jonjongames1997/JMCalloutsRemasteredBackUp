using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Stalking", CalloutProbability.Medium, "Reports of stalking by an unknown suspect", "Code 2", "LSPD")]

    public class Stalking : Callout
    {

        // General Variables
        private static Ped suspect;
        private static Blip susBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-1106.97f, -1359.91f, 5.04f),
                new(-1227.50f, -1446.35f, 4.19f),
                new(2013.07f, 3779.35f, 32.18f),
                new(1575.31f, 3597.65f, 35.37f),
                new(1193.34f, 2715.13f, 38.23f),
                new(-43.31f, 6505.73f, 31.52f),
                new(-294.99f, 6258.98f, 31.49f),
                new(),
                new(),
                new(),
                new(),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an unknown individual harassing another citizen");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_02 CITIZENS_REPORT_01 CRIME_DISTURBING_THE_PEACE_01 UNITS_RESPOND_CODE_02_02", spawnpoint);
            CalloutMessage = "Reports of stalking.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }


        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Stalking callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stalking", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout.");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.DarkOrange;
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

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~Suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Police! Stop right there. Don't reach for anything. If you reach for anything, you'll be shot.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What you want, you motherfucker, you!");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: We've gotten reports of you stalking someone. Why you stalking? That's just plain creepy.");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Why you even give a fuck?");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I am concerned for the victim's safety. You can't stalk anybody, it's against the law which you can be arrested for.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fuck you, motherfucker, you! *spits on you*");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("Convo ended.");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(System.Windows.Forms.Keys.End)) End();
        }

        public override void End()
        {
            base.End();
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stalking", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Stalking is Code 4!");
        }
    }
}
