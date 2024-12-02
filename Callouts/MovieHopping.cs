﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Movie Hopping", CalloutProbability.Medium, "Reports of a movie goer sneaking into the movies without a ticket", "Code 1", "LSPD")]


    public class MovieHopping : Callout
    {
        public static Ped suspect;
        public static Blip suspectBlip;
        public static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(301.34f, 202.71f, 104.38f), // Oriental Theater on Vinewood Blvd
                new(395.79f, -711.73f, 29.28f), // Movie theater near Mission Row PD
                new(-1420.45f, -213.17f, 46.50f), // Movie Theater in Morningwood
                new(-720.09f, -681.83f, 30.31f), // Valedaz Movie Theater
                new(-1370.69f, -171.10f, 47.48f) // Movie theater next to Tennis court near Morningwood
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Movie_Hopping_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a movie goer without a movie ticket");
            CalloutMessage = "Movie goer failing to show a movie ticket";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Movie Hopping callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Movie Hopping", "~b~Dispatch~w~: Suspect located. Respond ~r~Code 1~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_1_Response_Audio");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.IsValid();

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.Blue;
            suspectBlip.IsRouteEnabled = true;

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
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Come here and talk to me.");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I didn't do anything wrong, Officer. I wanted to see a movie so bad.");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Where is your movie ticket? May I see it please?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I don't have one, I'm broke. Tickets are f***ing almost $15 per person.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You know you can be charged with a misdemeanor, right?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: WHAT?!!!! HOW? That's bullsh*t! I'm not leaving until I see that film");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ". You either leave now or be put in cuffs. It's your choice.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: F**K YOU!");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You are under arrest. Put your hands behind your back.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Nope, I'm not going to jail. I can't survive prison, they'll kill me. You'll never take me alive, copper.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Conversation Ended!");
                        suspect.Tasks.ReactAndFlee(suspect);
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
            if (suspect) suspect.Dismiss();
            if (suspectBlip) suspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Movie Hopping", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            Game.LogTrivial("JM Callouts Remastered - Movie Hopping is Code 4!");
        }
    }
}
