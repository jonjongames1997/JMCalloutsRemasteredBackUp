using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using System.Collections.Generic;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Amber Alert", CalloutProbability.High, "A citizen's report of a missing person", "Code 1", "LSPD")]

    public class AmberAlert : Callout
    {
        private string[] pedList = new string[] { "IG_LESTERCREST", "A_F_Y_RURMETH_01", "A_F_M_SALTON_01", "A_F_O_SALTON_01", "A_M_O_SOUCENT_03", "S_M_M_TRUCKER_01", "IG_PRIEST" };
        private Ped suspect;
        private Blip blip;
        private Vector3 spawnpoint;
        private string malefemale;
        private int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
            };
            spawnpoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A missing person reported. Be On A Lookout.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("AMBERALERT");
            CalloutMessage = "A missing person reported";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Amber Alert callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Amber Alert", "~b~Dispatch~w~: The missing person has been spotted. Respond ~r~Code 2.");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.AttachBlip();
            blip.IsRouteEnabled = true;

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
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~Victim~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ". May I speak with you for a moment?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Victim~w~: What's going on, Officer? Did I do something illegal?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: No, you didn't do anything wrong. We got a call from one of your loved ones. They were worried about you. They said they haven't seen you for a while.");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Victim~w~: Oh, I forgot to tell them where I was heading. I'll notify them that I am safe. Thanks for your help, officer");
                    }
                    if(counter == 5)
                    {
                        suspect.Tasks.StandStill(500);
                        suspect.KeepTasks = true;
                        Game.DisplaySubtitle("Conversation Ended!");
                    }
                }
            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (suspect && suspect.IsDead) End();
            if (suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)) End();

            if(suspect.IsCuffed || suspect.IsDead || Game.LocalPlayer.Character.IsDead || !suspect.Exists())
            {
                End();
            }

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Amber Alert", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered Log]: Amber Alert is Code 4!");
        }
    }
}
