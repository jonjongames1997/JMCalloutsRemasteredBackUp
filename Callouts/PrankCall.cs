﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Prank Call", CalloutProbability.Medium, "A person called 9-1-1 to prank dispatch", "Code 2", "LSPD")]

    public class PrankCall : Callout
    {
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip susBlip;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CIV_ASSIST_01 UNITS_RESPOND_CODE_02_02", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A civilian called 9-1-1 to prank call dispatch.");
            CalloutMessage = "Suspect details is unknown. Respond Code 2.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Prank Call callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Prank Call", "~b~Dispatch~w~: Suspect spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~E~w~ at anytime to end the callout");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();
            suspect.KeepTasks = true;

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            if (Settings.ActivateAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.LocalUnit);
            }
            else
            {
                Settings.ActivateAIBackup = false;
            }


            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();


            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to talk to the suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ". Did you call 9-1-1?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Oh, shit. I thought you guys weren't actually gonna respond.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You thought wrong, " + malefemale + ". We take 9-1-1 calls very seriously. My dispatch told me you called and tried to prank call her. Why did you do that?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: It was only a joke. I didn't mean no harm, Officer.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Whether or not it was no harm but that's considered abuse of the 9-1-1 system and that's a felony charge.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: *Crying and begging* PLEASE! DON'T ARREST ME! I HAVE 2 KIDS AT HOME I NEED TO FEED. I can't afford to lose my job.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Let me see your identification and we'll go from there.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: *attempts to stop crying* Ok.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("Conversation Ended. Deal with the situation as you see fit.");
                        suspect.Tasks.Cower(500);
                        suspect.KeepTasks = true;
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Prank Call", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Prank Call callout is Code 4!");

        }

    }
}