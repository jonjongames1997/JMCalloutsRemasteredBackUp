using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Soliciting", CalloutProbability.Medium, "An individual soliciting on private property", "Code 2", "LSPD")]

    public class Soliciting : Callout
    {

        // General Variables //
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(154.39f, -987.48f, 30.09f), // Legion Square in Mission Row //
                new(-330.96f, 6081.46f, 31.45f), // Ammunation Near Paleto PD
                new(-113.23f, 6469.90f, 31.63f), // Paleto Bank
                new(-57.16f, 6522.26f, 31.49f), // Willie's Grocery Store
                new(-1251.52f, -1453.30f, 4.35f),
                new(-1014.72f, -1366.83f, 5.55f),
                new(-1181.91f, -884.12f, 13.78f),
                new(-32.71f, -145.04f, 57.09f),
                new(243.80f, -42.57f, 69.90f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_CITIZEN_REQUESTING_REMOVAL_OF_BEGGARS UNITS_RESPOND_CODE_02_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "An individual is asking people for money and harassing them.");
            CalloutMessage = "An Individual asking people for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Soliciting Callout accaepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Indigo;
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
                Game.DisplayHelp("Press ~y~E~w~ to talk to Suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Can you stop and talk to me please?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Oh, Shit. The one time!");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("Conversation ended!");
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting", "~b~You~w~: We are ~g~Code 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Soliciting is Code 4!");
        }

    }
}
