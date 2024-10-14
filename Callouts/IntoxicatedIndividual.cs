using CalloutInterfaceAPI;
using StopThePed.API;

namespace JMCalloutsRemastered.Callouts
{
    [CalloutInterface("[JM Callouts] Intoxicated Individual", CalloutProbability.Medium, "An individual causing a scene possibly drunk or high", "Code 2", "LSPD")]


    public class IntoxicatedIndividual : Callout
    {

        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(94.63f, -217.37f, 54.49f), // Shopping Center in Vinewood //
                new(-1682.72f,-296.65f, 51.81f), // Vinewood Cemetery
                new(-1392.72f, -607.95f, 30.32f), // Bahama Mamas. Requires either OpenInteriors or Enable All Interiors
                new(-47.78f, -1097.19f, 26.42f), // Simeon's Dealership
                new(128.20f, -1285.29f, 29.28f), // Vanilla Unicorn
                new(-195.23f, -1125.62f, 23.02f), // Construction site near Simeon's Dealership
                new(-37.90f, -1808.93f, 26.49f), // Grove Street
                new(907.54f, -1733.49f, 30.59f),
                new(809.17f, -1109.94f, 29.08f),
                new(787.99f, -758.52f, 26.77f),
                new(-1425.23f, -1126.91f, 3.45f),
                new(-1746.91f, -1114.01f, 13.02f),
                new(-1879.93f, -438.88f, 46.00f),
                new(851.34f, -113.42f, 79.39f),
                new(1009.09f, -563.53f, 60.20f),
            };
            Spawnnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Intoxicated_Individual_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A business owner reported an individual being drunk on business property.");
            CalloutMessage = "Suspect refused to leave property. Owner said that suspect is possibly be drunk or under the influence of narcotics. Approach with caustion.";
            CalloutPosition = Spawnnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Intoxicated Individual callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Intoxicated Individual", "~b~Dispatch~w~: Suspect located. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            Suspect = new Ped(Spawnnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            StopThePed.API.Functions.setPedAlcoholOverLimit(Suspect, true);

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@drunk_driver_1"), "drunk_driver_stand_loop_dd2", 1f, AnimationFlags.Loop);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.CadetBlue;
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

                Game.DisplayHelp("Press ~y~E ~w~to talk to Suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Good Afternoon " + malefemale + ", How are you today?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I'm fine, Officer. What's the problem?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: We've gotten reports that you are intoxicated. Did you have anything to drink today?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I'm not **hiccup* drunk. I'm fine.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Let me give you a sobriety test to make sure you're not under the influence of alcohol or drugs.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: whatever, *burp* bro.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("Conversation has ended!");
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
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Intoxicated Individual", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            Game.LogTrivial("JM Callouts Remastered - Intoxicated Individual is Code 4!");
        }
    }
}
