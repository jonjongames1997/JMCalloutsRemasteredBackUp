using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("The Taco Dispute", CalloutProbability.Medium, "A taco business requesting police assistance", "Code 2", "LSPD")]
    public class TheTacoDispute : Callout
    {
        private static Ped suspect;
        private static Blip suspectBlip;
        private static Vector3 spawnnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(4.41f, -1604.42f, 29.29f),
                new(651.34f, 2727.58f, 42.00f),
            };
            spawnnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_PUBLIC_DISTURBANCE UNITS_RESPOND_CODE_02_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "An individual causing a scene at a local taco shop.");
            CalloutMessage = "Owner requesting them to be removed from property.";
            CalloutPosition = spawnnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: The Taco Dispute callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~The Taco Dispute", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnnpoint);
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
                    suspect.Face(MainPlayer);
                    Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". What's going on here? What's the problem?");
                }
                if(counter == 2)
                {
                    Game.DisplaySubtitle("~r~Suspect~w~: You fucking seriously called the cops? Fuck this, I'm outta here, you cocksucking cock head!");
                }
                if(counter == 3)
                {
                    Game.DisplaySubtitle("Conversation ended!");
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
