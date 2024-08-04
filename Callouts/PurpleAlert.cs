using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Purple Alert", CalloutProbability.Medium, "Reports of a missing person with disability", "Code 2", "LSPD")]


    public class PurpleAlert : Callout
    {

        private static Ped suspect;
        private static Blip blip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("AMBERALERT ATTENTION_ALL_UNITS_01 WE_HAVE_01 CITIZENS_REPORT_01 CRIME_2_42_01 UNITS_RESPOND_CODE_02_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A concerned family member reporting the loved one lost after not responfing to phone calls or texts.");
            CalloutMessage = "An individual not responding to calls or texts, has a disability, has been last seen since Monday and never returned home.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Purple Alert callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Purple Alert", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Tasks.Wander();

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.DarkTurquoise;
            blip.IsRouteEnabled = true;

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
            if (blip) blip.Delete();

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
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". What's going on? You ok>");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: Yeah, I'm fine. What's going on?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: We got a call from your loved one saying that you were not responding to their calls or texts. They are worried about you.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: My phone died and I didn't have my charger with me to go somewhere and get it charged. I'm sorry.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Let's get you home to your loved ones, alright.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: Thank you, Officer.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("Conversation Ended. Call a Taxi or Uber");
                    }
                }
            }
            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Purple Alert", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Purple Alert is Code 4!");
        }
    }
}
