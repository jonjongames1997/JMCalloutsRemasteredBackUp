using CalloutInterfaceAPI;

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
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(100f));
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


            if(MainPlayer.DistanceTo(suspect) <= 5f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to speak with the ~r~suspect~w~.", false);

                if(Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", Come talk to me for a minute.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Why? I didn't do anything illegal.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: We have gotten reports of you loitering. You know it's against the law to loiter, right?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Not that I'm aware of, Officer.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Well, it is the law and can lead to a citation and/or an arrest.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I call bullshit on that. *spits on you*");
                    }
                    if(counter == 7)
                    {
                        suspect.Tasks.ReactAndFlee(suspect);
                        Game.DisplaySubtitle("~b~You~: Dispatch, suspect is on foot. He's fleeing from me. Show me in pursuit of that suspect.");
                        UltimateBackup.API.Functions.callPursuitBackup(suspect);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();

            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Homeless Person", "~b~You~w~: Dispatch, we are ~g~Code 4~w~! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");


            Game.LogTrivial("JM Callouts Remastered - Homeless Person is Code 4!");
        }
    }
}
