﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Peeping Tom - SFW", CalloutProbability.Medium, "A citizen's report of a peeping tom", "Code 3", "LSPD")]

    public class PeepingTom : Callout
    {

        private static Ped suspect;
        private static Blip suspectBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(468.63f, -1779.71f, 28.69f),
                new(1691.34f, 3857.23f, 34.91f),
                new(-310.01f, 6327.21f, 32.49f),
                new(-1623.53f, -362.89f, 46.40f),
                new(-1532.85f, 27.17f, 56.82f),
                new(-1713.22f, 386.81f, 89.73f),
                new(312.86f, 468.63f, 151.27f),
                new(-162.05f, 889.77f, 233.47f),
                new(-792.48f, 181.51f, 72.84f), // Michael's House
                new(1975.41f, 3815.74f, 33.42f), // Trevor's Trailer
                new(-401.75f, 6321.55f, 28.94f), // Paleto Bay
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Peeping_Tom_Audio_1");
            CalloutMessage = "Reports of a peeping Tom. Suspect is armed";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Peeping Tom callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Peeping Tom", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");

            if (Settings.HelpMessages)
            {
                Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);
            }
            else
            {
                Settings.HelpMessages = false;
            }


            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallouts_Respond_Code_3_Audio", spawnpoint);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Tasks.PlayAnimation(new AnimationDictionary("missarmenian2"), "standing_idle_loop_drunk", -1f, AnimationFlags.Loop);
            StopThePed.API.Functions.setPedAlcoholOverLimit(suspect, true);

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.Chocolate;
            suspectBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

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

            if(MainPlayer.DistanceTo(suspect)   <= 10f)
            {

                Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(Settings.Dialog))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: LSPD! Stay right where you are, motherfucker!");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Oh, shit!");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("Conversation Ended!");
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
            if (suspect) suspect.Dismiss();
            if (suspectBlip) suspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Peeping Tom", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Peeping Tom is Code 4!");
        }

    }
}
