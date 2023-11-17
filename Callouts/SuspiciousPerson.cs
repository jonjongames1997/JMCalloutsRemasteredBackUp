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
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_DISTURBING_THE_PEACE_02 IN_OR_ON_POSITION UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "Reports of a suspicious person";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Deranged Lover callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Suspicious Person", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 3");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("DONTBESUSPICIOUS");

            Game.DisplayHelp("Search the are for any suspicious citizens");

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


            base.Process();
        }
    }
}
