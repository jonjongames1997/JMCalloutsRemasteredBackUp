using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("9-1-1 Hang Up - Mirror Park", CalloutProbability.High, "An individual hung up on 911.", "Code 1", "LSPD")]

    public class _911HangUp : Callout
    {

        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private string malefemale;
        private int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(1082.087f, -346.2961f, 67.1872f), // Mirror Park near Horny's //
                new Vector3(-1294.99f, -1316.46f, 4.69f), // OCRP Postal 305/Vespucci Beach //
                new Vector3(-1281.43f, -1139.24f, 6.47f), // Bean Machine in Vespucci Beach
                new Vector3(-1335.80f, -929.51f, 11.75f), // Motel Near Rob's Liquors
                new Vector3(-232.05f, -2055.64f, 27.62f), // Maze Bank Arena Parking 
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A civilian called 9-1-1 then immediately hung up. Deal with this, Officer.");
            CalloutMessage = "A citizen called 911 then hung up on dispatch"; // Brief description of callout //
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: 9-1-1 Hang Up callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~ 9-1-1 Hang Up", "~b~Dispatch: Suspect has been spotted. Respond ~r~Code 1.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.BlueViolet;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to speak with the ~r~suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
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
                        Game.DisplaySubtitle("~r~Suspect~w~: Snitch! I'm gonna give you the ass whooping of your life, Officer, that your parents couldn't give you as a child.");
                        Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        Suspect.Inventory.GiveNewWeapon("WEAPON_CANDYCANE", 500, true);
                    }
                }
            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
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

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~9-1-1 Hang Up", "~b~You: ~w~Dispatch, we are ~g~Code 4! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - 911 Hang Up is Code 4!");
        }

    }
}
