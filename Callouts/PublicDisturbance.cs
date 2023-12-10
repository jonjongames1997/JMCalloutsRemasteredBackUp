using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Public Disturbance", CalloutProbability.Medium, "A individual causing a scene in public", "Code 3", "LSPD")]

    public class PublicDisturbance : Callout
    {

        // General Variables //
        private Ped suspect;
        private Blip SuspectBlip;
        private Vector3 spawnPoint;
        private int counter;
        private string malefemale;
        private float heading;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = new Vector3(-174.17f, -1427.77f, 31.25f);
            heading = 178.40f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's reporting a public disturbance.");
            CalloutMessage = "A citizen's reporting a disturbance with a white male with no shirt. Threatening the victim's life with a deadly weapon.";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Public Disturbance callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Disturbance", "~b~Dispatch:~w~ Suspect has been spotted!. Respond ~r~Code 2.");

            suspect = new Ped(spawnPoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            SuspectBlip = suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.BurlyWood;
            SuspectBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (SuspectBlip) SuspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to talk to suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You (Officier)~w~: Excuse me, " + malefemale + ", Can you come talk to me for a second?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ What do you want now, you donut eaters?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You (Officer):~w~ Can you explain to me on what's to be the problem?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~I have a bipolar disorder which I can't control and it makes me say offensive things.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You (Officer): ~w~Do you take any type of medication for your disorder?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~Yes, I have forget about it. I do apologize for y'all to be called out here.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You (Officer): ~w~Why did threat someone's life for when they didn't do anything to you?");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~I didn't mean anything by it. I do apologize about it.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You (Officer): ~w~Next time, take your medicine when you are supposed to take it, " + malefemale + ".");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~I understand that loud and clear, Officer.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Conversation Ended! Deal with this situation to your satisfaction.");
                        suspect.Tasks.StandStill(500);
                    }
                }

                if (suspect.IsCuffed || suspect.IsDead || Game.LocalPlayer.Character.IsDead || !suspect.Exists())
                {
                    End();
                }
            }
        }

        public override void End()
        {
            base.End();

            if (suspect.Exists())
            {
                suspect.Dismiss();
            }
            if (SuspectBlip.Exists())
            {
                SuspectBlip.Delete();
            }

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Disturbance", "~b~You:~w~ Dispatch, we are ~g~Code 4.~w~ Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Public Disturbance is Code 4!");
        }
    }
}
