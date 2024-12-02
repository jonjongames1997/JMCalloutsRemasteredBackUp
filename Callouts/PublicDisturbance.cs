﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Public Disturbance", CalloutProbability.Medium, "A individual causing a scene in public", "Code 2", "LSPD")]

    public class PublicDisturbance : Callout
    {

        // General Variables //
        private static Ped suspect;
        private static Blip SuspectBlip;
        private static Vector3 spawnPoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-174.17f, -1427.77f, 31.25f), // Across from the auto shop in strawberry
                new(1693.54f, 4822.75f, 42.06f), // Clothing Shop in Grape Seed
                new(1991.82f, 3048.46f, 47.22f), // Yellow Jack
                new(1197.32f, 2695.93f, 37.91f), // Clothing Shop on Route 68
                new(-2962.76f, 482.39f, 15.70f),
                new(-1450.11f, -614.95f, 30.83f),
                new(-817.27f, -621.98f, 29.22f),
                new(-250.46f, -728.36f, 33.32f),
                new(18.70f, -1122.72f, 28.95f),
                new(233.85f, -1512.19f, 28.73f),
                new(175.51f, -1736.56f, 28.89f),
                new(370.55f, -1986.71f, 24.23f),
                new(550.10f, -1772.51f, 33.44f),
                new(488.65f, -1313.33f, 29.26f),
                new(-163.46f, -303.08f, 39.73f),
                new(244.34f, -389.38f, 45.87f),
                new(225.60f, 339.84f, 105.57f),
                new(81.13f, 274.48f, 110.21f),
                new(-369.74f, 264.92f, 84.98f),
                new(-638.03f, -110.30f, 37.99f),
                new(-545.24f, -204.61f, 38.22f),
                new(-1820.98f, 792.17f, 138.12f),
                new(-1466.04f, -326.55f, 44.80f),
                new(-1314.80f, -645.97f, 26.45f),
            };
            spawnPoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Public_Disturbance_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's reporting a public disturbance.");
            CalloutMessage = "A citizen's reporting a person threatening a victim's life with a deadly weapon.";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Public Disturbance callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Disturbance", "~b~Dispatch:~w~ Suspect has been spotted!. Respond ~r~Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);
            Game.DisplayNotification("~r~WARNING~w~: Some Players may find this callout controversial or disturbing. Player Discretion is Advised.");

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            suspect = new Ped(spawnPoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.PlayAnimation(new AnimationDictionary("amb@world_human_prostitute@crackhooker@base"), "base", -1f, AnimationFlags.Loop);

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

            if (MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~ to talk to suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(Settings.Dialog))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ", Can you come talk to me for a second?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: What do you want now, you donut eaters?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Can you explain to me on what's to be the problem?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: I have a bipolar disorder which I can't control and it makes me say offensive things.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Do you take any type of medication for your disorder?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: Yes, I have forget about it. I do apologize for y'all to be called out here.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Why did threat someone's life for when they didn't do anything to you?");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: I didn't mean anything by it. I do apologize about it.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Next time, take your medicine when you are supposed to take it, " + malefemale + ".");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: I understand that loud and clear, Officer.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Conversation Ended!");
                        suspect.Tasks.Wander();
                    }
                }
                if (MainPlayer.IsDead)
                {
                    End();
                }

                if (Game.IsKeyDown(Settings.EndCall)) this.End();
            }
        }

        public override void End()
        {
            base.End();
            if (suspect) suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Disturbance", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            Game.LogTrivial("JM Callouts Remastered - Public Disturbance is Code 4!");
        }
    }
}
