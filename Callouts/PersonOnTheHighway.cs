using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Person On The Highway", CalloutProbability.Medium, "A citizen's report of an individual on the freeway", "Code 2", "SAHP")]

    public class PersonOnTheHighway : Callout
    {
        private string[] wepList = new string[] { "WEAPON_KNIFE", "WEAPON_BAT", "WEAPON_DAGGER", "WEAPON_GOLFCLUB", "WEAPON_HAMMER", "WEAPON_HATCHET" };
        private string[] pedList = new string[] { "a_m_m_afriamer_01", "ig_abigail", "ig_amandatownley", "csb_anita", "ig_ashley", "g_f_y_ballas_01", "a_f_m_bodybuild_01", "a_f_y_eastsa_03", "ig_maryann" };
        private Blip blip;
        private Ped suspect;
        private Vector3 spawnpoint;
        private int counter;
        private string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
            };
            spawnpoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an individual on the highway");
            CalloutMessage = "A citizen's reporting an individual on the highway. Respond Code 2.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Person on the highway callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Person On The Highway", "~b~Dispatch~w~: The suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Gold;
            blip.IsRouteEnabled = true;

            suspect.Tasks.Wander();
            suspect.KeepTasks = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

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


    }
}
