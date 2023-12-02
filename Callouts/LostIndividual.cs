using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using System.Windows.Forms;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;


namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lost Individual", CalloutProbability.Medium, "A citizen's report of a lost person", "Code 2", "LSPD")]

    public class LostIndividual : Callout
    {

        // General Variables //
        private Ped victim;
        private Ped suspect;
        private Blip vicBlip;
        private Vector3 spawnPoint;
        private Vector3 suspectSpawnpoint;
        private float suspectHeading;
        private float heading;
        private int counter;
        private string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = new Vector3(-663.64f, -227.5f, 37.39f);
            heading = 70.58f;
            suspectSpawnpoint = new Vector3(-623.54f, -230.25f, 38.06f); // Second Suspect will spawn at this location 
            suspectHeading = 131.09f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Michael DeSanta reported his wife missing. Locate and help her get home safely, Officer.");
            CalloutMessage = "Reports of a missing person";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            victim = new Ped("IG_AMANDATOWNLEY", spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            suspect = new Ped("PLAYER_ZERO", suspectSpawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            suspect.Tasks.StandStill(500);

            vicBlip = victim.AttachBlip();
            vicBlip.Color = System.Drawing.Color.Pink;
            vicBlip.IsRouteEnabled = true;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (victim) victim.Delete();
            if (vicBlip) vicBlip.Delete();
            if (suspect) suspect.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(Game.LocalPlayer.Character.DistanceTo(victim) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~victim~w~.");

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Can you tell me what happened?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: Well, I was doing my grocery shopping for my family, somehow I managed to hit my head on a pole. I was knocked out for a few minutes. I don't remember the rest");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Okay, " + malefemale + ". Do you need any medical attention?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: No, I'll be fine. I'll just take some Tylenol and get some rest. I'll be okay.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("Conversation Ended! Call her an Uber.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~You motherfucker, you! DIE!!!!!");
                        LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("YOUMOTHERFUCKERYOU");
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        suspect.Inventory.GiveNewWeapon("WEAPON_GUSENBERG", 500, true);
                    }

                }

            }

            if (Settings.ActiveAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnPoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.LocalUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnPoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.PrisonerTransport);
            }
            else { Settings.ActiveAIBackup = false; }

            if (victim.IsCuffed || victim.IsDead || Game.LocalPlayer.Character.IsDead || !victim.Exists() && suspect.IsCuffed || suspect.IsDead || Game.LocalPlayer.Character.IsDead || !suspect.Exists())
            {
                End();
            }

            base.Process();
        }

        public override void End()
        {
            if (victim.Exists())
            {
                victim.Dismiss();
            }
            if (vicBlip.Exists())
            {
                vicBlip.Delete();
            }

            if (suspect.Exists())
            {
                suspect.Dismiss();
            }

            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Lost Individual is CODE 4!");
        }

    }
}
