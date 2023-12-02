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

    [CalloutInterface("Suspicious Person", CalloutProbability.Medium, "Reports of an suspicious individual", "Code 2", "LSPD")]

    public class SuspiciousPerson : Callout
    {
        private Ped suspect;
        private Vector3 spawnPoint;
        private Blip susBlip;
        private LHandle pursuit;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of a suspicious person");
            CalloutMessage = "Reports of a suspicious person";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Deranged Lover callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Suspicious Person", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 3");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("DONTBESUSPICIOUS");

            Game.DisplayHelp("Search the area for any suspicious citizens");

            suspect = new Ped(spawnPoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();
            Persona persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(suspect);
            persona.Wanted = true;
            LSPD_First_Response.Mod.API.Functions.SetPersonaForPed(suspect, persona);

            susBlip = suspect.AttachBlip();
            susBlip.Color = Color.Orange;
            susBlip.IsRouteEnabled = true;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(Game.LocalPlayer.Character.DistanceTo(suspect) < 20f && pursuit == null)
            {
                suspect.Tasks.Flee(Game.LocalPlayer.Character, 9999f, -1);
                suspect.KeepTasks = true;
                pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
                LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                if (susBlip) susBlip.Delete();
            }

            if(pursuit != null)
            {
                if (LSPD_First_Response.Mod.API.Functions.IsPursuitStillRunning(pursuit))
                {
                    Game.DisplaySubtitle("Catch the ~r~wanted~w~ suspect.");
                }else if (!IsEnding)
                {
                    End();
                }
            }

            if(suspect.IsDead || suspect.IsCuffed)
            {
                End();
            }

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Suspicious Person", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Suspicious Person is code 4!");
        }
    }
}
