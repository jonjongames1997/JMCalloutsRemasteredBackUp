using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CalloutInterfaceAPI;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lockpicking - ULS", CalloutProbability.Medium, "A school security guard reporting a student locker break in", "Code 2", "LSPD")]

    public class LockPickingSchool : Callout
    {

        private Ped suspect;
        private Ped victim;
        private Vector3 spawnpoint;
        private Vector3 susSpawnPoint;
        private Blip suspectBlip;
        private Blip victimBlip;
        private string malefemale;
        private int counter;
        private float heading;
        private float susHeading;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(); // University of Los Santos //
            heading = 1509.25f;
            susSpawnPoint = new Vector3();
            susHeading = 1789.69f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A unknown individual reported breaking into a student's locker");
            CalloutMessage = "A student locker break in";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Lockpicking - ULS callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Lockpicking - ULS", "~b~Dispatch~w~: Suspect has been spotted by another student! Respond ~r~Code 2~w~!");

            victim = new Ped("A_F_Y_RURMETH_01", spawnpoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            suspect = new Ped(susSpawnPoint, susHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            suspect.Tasks.StandStill(500);
            suspect.AttachBlip();

            suspectBlip.Color = System.Drawing.Color.Red;

            victim.AttachBlip();
            victimBlip.Color = System.Drawing.Color.Aqua;
            victimBlip.IsRouteEnabled = true;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

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
            if (victim) victim.Delete();
            if (victimBlip) victimBlip.Delete();
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

    }
}
