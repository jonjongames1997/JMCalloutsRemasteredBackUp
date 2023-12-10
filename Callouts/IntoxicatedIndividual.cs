using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{
    [CalloutInterface("Intoxicated Individual", CalloutProbability.Medium, "An individual causing a scene possibly drunk or high", "Code 2", "LSPD")]


    public class IntoxicatedIndividual : Callout
    {

        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnnpoint;
        private float heading;
        private int counter;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnnpoint = new Vector3(94.63f, -217.37f, 54.49f); // Shopping Center in Vinewood //
            heading = 53.08f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A business owner reported an individual being drunk on business property.");
            CalloutMessage = "Suspect refused to leave property. Owner said that suspect is possibly be drunk or under the influence of narcotics. Approach with caustion.";
            CalloutPosition = Spawnnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Intoxicated Individual callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Intoxicated Individual", "~b~Dispatch:~w~ Suspect located. Respond ~r~Code 2.");

            Suspect = new Ped(Spawnnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.CadetBlue;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

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

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E ~w~to talk to Suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("Player: Good Afternoon " + malefemale + ", How are you today?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ I'm fine, Officer. What's the problem?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("Player: We've gotten reports from this business behind you that you were intoxicated. Did you have anything to drink today?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ I'm not **hiccup* drunk. I'm fine.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("Player: Let me give you a sobriety test to make sure you're not under the influence of alcohol or drugs.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ I DO NOT CONSENT TO THIS TYPE OF INTERROGATION!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("Conversation has ended!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                    }
                }
            }

            if (Suspect.IsCuffed || Suspect.IsDead || Game.LocalPlayer.Character.IsDead || !Suspect.Exists())
            {
                End();
            }
        }

        public override void End()
        {
            base.End();

            if (Suspect.Exists())
            {
                Suspect.Dismiss();
            }
            if (SuspectBlip.Exists())
            {
                SuspectBlip.Delete();
            }

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Intoxicated Individual", "~b~You:~w~ Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Intoxicated Individual is Code 4!");
        }
    }
}
