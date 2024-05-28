using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("9-1-1 Hang Up", CalloutProbability.Medium, "An individual hung up on 911.", "Code 2", "LSPD")]

    public class _911HangUp : Callout
    {

        // General Variables //
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(1082.087f, -346.2961f, 67.1872f), // Mirror Park near Horny's //
                new(-1294.99f, -1316.46f, 4.69f), // OCRP Postal 305/Vespucci Beach //
                new(-1281.43f, -1139.24f, 6.47f), // Bean Machine in Vespucci Beach
                new(-1335.80f, -929.51f, 11.75f), // Motel Near Rob's Liquors
                new(-232.05f, -2055.64f, 27.62f), // Maze Bank Arena Parking 
                new(139.11f, -1635.78f, 29.30f), // Near Ron Station on Davis Ave
                new(130.16F, -1737.21f, 30.11f), // Train Station next to Davis Mall
                new(-1324.74f, -397.55f, 35.83f), // Ammunation in Morningwood 
                new(332.11f, 180.63f, 103.11f), // Oriental Theatre on Vinewood Blvd.
                new(344.79f, -191.55f, 57.23f), // OCRP Postal 582
                new(757.17f, -229.23f, 66.11f), // The park near Cab Depot
                new(1316.90f, -1666.68f, 51.24f), // OCRP Postal 184
                new(947.70f, -2112.82f, 30.56f), // Meat Factory Near Mirror Park
                new(1072.90f, -3066.06f, 5.58f), // The docks 
                new(501.06f, -3015.24f, 5.72f), // OCRP Postal 28
                new(181.76f, -2996.86f, 5.42f), // OCRP postal 23
                new(-169.38f, -2615.76f, 5.68f), // The Ports
                new(-993.00f, -2130.90f, 10.20f), // Near LSIA
                new(-1049.26f, -1622.02f, 3.96f), // Vespucci Beach/OCRP Postal 321
                new(-1038.28f, -1133.90f, 1.83f), // Vespucci Canals
                new(-1367.36f, -658.04f, 27.53f), // OCRP Postal 628
                new(-3224.25f, 1080.55f, 10.52f), // Chumash/G.O.H.
                new(-448.69f, 118.38f, 64.22f), // OCRP Postal 527
                new(-9.04f, -285.22f, 46.86f), // OCRP Postal 540
                new(470.14f, 270.12f, 102.73f), // Clinton Ave Hotel
                new(),
                new(),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A civilian called 9-1-1 then immediately hung up. Deal with this, Officer.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("WE_HAVE SOS_CALL UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "A citizen called 911 then hung up on dispatch"; // Brief description of callout //
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: 9-1-1 Hang Up callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~9-1-1 Hang Up", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            Suspect.Tasks.PutHandsUp(-1, MainPlayer);
            SuspectBlip.Color = System.Drawing.Color.BlueViolet;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (Suspect) Suspect.Delete();
            if (SuspectBlip) SuspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to speak with the ~r~suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me " + malefemale + ", Can I speak to you for a moment?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Sure, Officer. What seems to be the problem?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: We received a call from your cell phone ping. Did you call 9-1-1?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Oh, shit. I think Siri misheard what I've said. Oh, my lord, I do apologize about thi... Am I getting arrested?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Ok, let me see some identification from you and we'll go from there. Do you have any warrants that I should know about?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Sure, here's my ID and no, officer, no warrants. I never been arrested before.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Ok, let me run your information real quick and we'll go from there.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Snitch! I'm outta here!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~9-1-1 Hang Up", "~b~You~w~: Dispatch, we are ~g~Code 4~w~! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - 911 Hang Up is Code 4!");
        }

    }
}
