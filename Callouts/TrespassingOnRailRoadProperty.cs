using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On Railroad Property", CalloutProbability.High, "A citizen trespassing on Railroad Property", "Code 3", "LSPD")]

    public class TrespassingOnRailRoadProperty : Callout
    {


        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private int counter;
        private float heading;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = new Vector3(452.94f, -1648.89f, 29.97f);
            heading = 225.98f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 1000f);
            CalloutMessage = "A citizen is reporting a suspicious person on railroad tracks.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On Railroad Property callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "Trespassing On Railroad Property", "~b~Dispatch:~w~ Suspect Spotted. Respond ~r~Code 2.");

            Suspect = new Ped(Spawnpoint, heading); // Optional if you want to add a ped for the callout. If you don't want a specific ped for the callout, just put 'Spawnpoint' and 'heading' in the brackets.
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen reporting a suspicious female on the railroad tracks. Possibly high on drugs. Approach with caution.");

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

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press 'E' to interact with Suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~Player~w~: Hello there " + malefemale + ". Can I speak to you for a moment?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ ALIENS! ALIENS! They're here! RUN FOR YOUR LIVES!");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", Calm down for me please.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Oh, I thought you were an alien. Sorry, Officer. What's up? Want to smoke some crack? Smoke some weed?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Did you do any illegal drugs that I should know about? and why are you on railroad property?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Yeah, drugs are the 2nd best medicine cause laughter is the #1 best medicine. I'm trying to record some videos of the trains coming through. Why?");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Well " + malefemale + ", that's fine but you can't be on the tracks cause people have been comitting suicde by train almost every year. As long as you're on the other side of the crossing, that's fine. Can I see some identification from you if it's in your posession?");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ What's a indentification again?");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", you're under arrest.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: You gotta catch me first!");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Conversation ended.");
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

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Railroad Property", "~b~You:~w~ Dispatch, we are ~g~CODE 4.~w~ Show me back 10-8. ");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Trespassing On Railroad Property is Code 4!");
        }

    }
}
