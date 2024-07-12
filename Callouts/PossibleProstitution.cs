using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Possible Prostitution", CalloutProbability.Medium, "A female possibly selling her body for money", "Code 2", "LCSO")]

    public class PossibleProstitution : Callout
    {

        // General Variables //
        private static readonly string[] pedList = new string[] { "S_F_Y_HOOKER_01", "S_F_Y_HOOKER_02", "S_F_Y_HOOKER_03", "IG_ABIGAIL", "IG_AMANDATOWNLEY", "G_F_Y_BALLAS_01", "S_F_Y_BARTENDER_01", "A_F_M_BEACH_01", "IG_MARYANN", "IG_MOLLY", "A_F_Y_RURMETH_01", "A_F_Y_EASTSA_03", "MP_F_MISTY_01" };
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-535.76f, -849.20f, 29.44f), // Near Lucky Plucker in Little Seoul // 
                new(-415.10f, 1172.77f, 325.81f), // Galileo Observatory
                new(-1137.11f, 2664.79f, 18.07f), // Route 68 near Fort Zancudo
                new(-2326.10f, 374.99f, 174.61f), // University of Los Santos
                new(-1618.14f, 178.25f, 60.26f), // Kortz Center
                new(203.46f, -2952.93f, 6.02f),
                new(241.05f, -2631.20f, 6.02f),
                new(277.89f, -2539.12f, 5.76f),
                new(880.85f, -2449.57f, 28.57f),
                new(912.89f, -2196.08f, 30.47f),
                new(1046.61f, -2179.10f, 31.47f),
                new(1443.83f, -1856.55f, 71.50f),
                new(1317.76f, -1615.19f, 52.35f),
                new(-3144.28f, 1056.61f, 20.54f),
                new(-2937.53f, 481.45f, 15.24f),
                new(-2074.46f, -326.96f, 13.32f),
                new(-1544.78f, -329.74f, 46.98f),
                new(-1327.82f, -202.22f, 43.58f),
                new(-1182.08f, -136.73f, 40.13f),
                new(-935.60f, -125.23f, 37.76f),
                new(-601.90f, 120.41f, 59.81f),
                new(-528.28f, 308.22f, 83.02f),
                new(-374.88f, 188.17f, 80.59f),
                new(-229.39f, 108.89f, 69.70f),
                new(20.42f, -90.22f, 58.74f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_CITIZENS_REQUESTING_REMOVAL_OF_PROSTITUTES UNITS_RESPOND_CODE_02_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen reported a young female selling her body for money. Talk to her and see if the claim is true. Approach with caution.");
            CalloutMessage = "Citizens reporting a young female possibly selling her body for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Possible Prostitution callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Possible Prostitution", "~b~Dispatch:~w~ Suspect has been spotted. Respond ~r~Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            Game.DisplayNotification("Tip: This callout works better at night time when other prostitutes are on the streets.");

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Cyan;
            SuspectBlip.IsRouteEnabled = true;

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("switch@michael@prostitute"), "base_hooker", -1f, AnimationFlags.Loop);

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

                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Hello " + malefemale + ", Can I ask you some questions?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Sure. What seems to be the problem, Officer?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I have gotten reports of you possibly selling your body for money. Is it true?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Yes. I need the money to pay off my college debt.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You know that's illegal in the state of San Andreas. Which I can arrest you for that.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What you gonna do? I'm not going anywhere!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", You can get a job anywhere here in the city. We can help you get a job through a vocational school.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplayNotification("Chief: What the fuck is going on out there, Deputy?!");
                    }
                    if (counter == 9)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("mini@strip_club@idles@stripper"), "stripper_idle_06", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~r~Suspect~w~: Come on, Officer. First time is free.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", You're under arrest for prostitution.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fuck you then, pigs.");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("Conversation ended.");
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Possible Prostitution", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Possible Prostitution is Code 4!");
        }
    }
}
