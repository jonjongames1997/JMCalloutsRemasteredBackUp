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

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Unauthorized Access - Richard's Majestic", CalloutProbability.Medium, "Reports of a trespasser", "Code 2", "LSPD")]

    public class UnauthorizedAccessMovieStudio : Callout
    {
        private Ped suspect;
        private Blip susBlip;
        private Vector3 spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1050.09f, -512.47f, 36.04f); // Richard's Majestic Movie Studio
            heading = 341.35f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 500f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_OFFICER_IN_NEED_OF_ASSISTANCE_01 IN_OR_ON_POSITION UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "An individual refusing to leave";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Unauthorized Acces Movie Studio callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Individual Trespassing", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 2");

            suspect = new Ped(spawnpoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            CalloutInterfaceAPI.Functions.SendMessage(this, "A security officer reporting an individual trespassing on private property without proper access.");

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Yellow;
            susBlip.IsRouteEnabled = true;

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
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {


            base.Process();
        }
    }
}
