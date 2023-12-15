using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Refuse To Leave", CalloutProbability.Medium, "An individual refuses to leave property", "Code 2", "LSPD")]

    public class RefuseToLeave : Callout
    {

        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private float heading;
        private int counter;
        private string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = new Vector3(-821.94f, -1073.82f, 11.33f); // Near Floyd's Apartment //
            heading = 45.67f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Business employee told the individual to leave the property but refuses to. Employee suspects the individual to be under the influence.");
            CalloutMessage = "Individual refusing to leave property by business owner/employee.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Refuse To Leave callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Refuse To Leave", "~b~Dispatch: ~w~Suspect has been spotted. Respond ~r~Code 2");

            Suspect = new Ped(Spawnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.BlueViolet;
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

                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~suspect~w~. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("Player: Hello there " + malefemale + ", Can I talk to you for a second?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ What now donut pigs?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("Player: Can you tell me what's going on?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ That bitch over there told me I can't come in here.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("Player: Is there a reason why she can't let you come in here?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ I was outside the door asking people for money. She called the cops and they told me that I was trespassed from the property.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplayNotification("Tip: ~o~If the suspect was trespassed from the property before, that's an arrestable offense.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("Player: Ok. Well, you know you can be arrested for trespassing, right?");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ WHAT?! Are you fucking with me?");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("Player: No, I'm not. Don't try anything stupid, you'll make things worse on yourself.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Fuck you and fuck her! I'm outta here, playa!");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("Conversation ended!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                    }
                }
            }
            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (Suspect && Suspect.IsDead) End();
            if (Suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Suspect)) End();
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

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Refuse To Leave", "~b~You:~w~ Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Refuse to leave is Code 4!");
        }

    }
}
