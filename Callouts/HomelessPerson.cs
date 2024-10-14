using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Homeless Person Loitering", CalloutProbability.Medium, "A homeless person loitering and refusing to leave", "CODE 2", "LSPD")]

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
                new(83.87f, -1400.55f, 29.42f),
                new(1190.65f, 2701.18f, 38.16f),
                new(-290.47f, 6266.16f, 31.49f),
                new(-35.51f, -141.44f, 57.15f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A homeless person loitering");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallouts_Homeless_Person_Callout_Audio_1", spawnpoint);
            CalloutMessage = "A homeless person loitering and refusing to leave by owner";
            CalloutPosition = spawnpoint;

            base.OnCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {

            Game.LogTrivial("[JM Callouts Remastered Log]: Homeless Person Loitering callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Homeless Person Loitering", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

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
                        Game.DisplaySubtitle("~b~You~: Dispatch, suspect is on foot. He's fleeing from me. Show me in pursuit of that suspect.");
                        suspect.Tasks.ReactAndFlee(suspect);
                    }
                }
            }

            if (MainPlayer.IsDead)
            {
                End();
            }

            if (Game.IsKeyDown(Settings.EndCall))
            {
                End();
            }
        }

        public override void End()
        {
            base.End();

            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Homeless Person", "~b~You~w~: Dispatch, we are ~g~Code 4~w~! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");


            Game.LogTrivial("JM Callouts Remastered - Homeless Person is Code 4!");
        }
    }
}
