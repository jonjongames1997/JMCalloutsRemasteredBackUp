using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] The Taco Dispute", CalloutProbability.Medium, "A taco business requesting police assistance", "CODE 2", "LSPD")]
    public class TheTacoDispute : Callout
    {
        private static Ped suspect;
        private static Blip suspectBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(4.41f, -1604.42f, 29.29f),
                new(651.34f, 2727.58f, 42.00f),
                new(447.91f, -1244.57f, 30.29f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_PUBLIC_DISTURBANCE UNITS_RESPOND_CODE_02_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "An individual causing a scene at a local taco shop.");
            CalloutMessage = "Owner requesting them to be removed from property.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: The Taco Dispute callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~The Taco Dispute", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.Coral;
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
            if (suspect) suspect.Dismiss();
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~suspect~w~. ~y~Approach with caution~w~.", false);

            if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
            {
                counter++;

                if(counter == 1)
                {
                    Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ". What's going on here? What's the problem?");
                }
                if(counter == 2)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: About time! These people are not letting me buy my tacos. It's my right to eat my American made tacos.");
                }
                if(counter == 3)
                {
                    Game.DisplaySubtitle("~b~You~w~: What exactly was said?");
                }
                if(counter == 4)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: Are you for real?");
                }
                if(counter == 5)
                {
                    Game.DisplaySubtitle("~b~You~w~: Yes. I need more information on the situation. I wasn't here, I don't know what happened. Just explain to me what happened so I understand the situation.");
                }
                if(counter == 6)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: Oh, Jesus fucking Christ. Okay fine. I was hungry, I decided to go to the taco stand and get me some tacos, I stepped aside to make sure I have money to pay for my food.");
                }
                if(counter == 7)
                {
                    Game.DisplaySubtitle("~b~You~w~: Okay, go on.");
                }
                if(counter == 8)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: I went up to the window and started ordering but the manager told me that I need to leave and I asked why.");
                }
                if(counter == 9)
                {
                    Game.DisplaySubtitle("~b~You~w~: Did they give you a reason why they told you to leave?");
                }
                if(counter == 10)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: No. I didn't do anything wrong. I have my 'Murican right to eat tacos, I'm hangry, I need food in me.....Pause. I'm just hangry.");
                }
                if(counter == 11)
                {
                    Game.DisplaySubtitle("~b~You~w~: Okay, I'll talk to the manager and see what they say. Sit tight for me.");
                }
                if(counter == 12)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: Okay.");
                }
                if(counter == 13)
                {
                    Game.DisplayHelp("Talk to the manager. *Roleplay it out*");
                }
                if(counter == 14)
                {
                    Game.DisplaySubtitle("~b~You~w~: Okay, " + malefemale + ". I talked to the manager and they said you've made that story up and they said, you were pan handling outside of their store.");
                }
                if(counter == 15)
                {
                    Game.DisplaySubtitle("~y~Suspect~w~: ~o~BULLSHIT~w~! I call bullshit!");
                }
                if(counter == 16)
                {
                    Game.DisplaySubtitle("Conversation Ended!");
                    suspect.Tasks.ReactAndFlee(suspect);
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (suspectBlip) suspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~The Taco Dispute", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("JM Callouts Remastered - The Taco Dispute is Code 4!");
        }
    }
}
