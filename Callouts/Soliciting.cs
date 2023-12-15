using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Soliciting", CalloutProbability.High, "An individual soliciting on private property", "Code 3", "LSPD")]

    public class Soliciting : Callout
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
            Spawnpoint = new Vector3(154.39f, -987.48f, 30.09f); // Legion Square in Mission Row //
            heading = 165.04f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "An individual is asking people for money and harassing them. Deal with this, Officer.");
            CalloutMessage = "An Individual asking people for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Soliciting Callout accaepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting", "~b~Dispatch:~w~ Suspect has been spotted. Respond ~r~Code 2.");

            Suspect = new Ped(Spawnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Indigo;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to talk to Suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("Player: Excuse me, " + malefemale + ". Can you stop and talk to me please?");
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

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting", "~b~You~w~: We are ~g~Code 4!~w~ Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Soliciting is Code 4!");
        }

    }
}
