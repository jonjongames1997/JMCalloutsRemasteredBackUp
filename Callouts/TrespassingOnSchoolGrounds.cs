using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.Callouts;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using Rage;
using Rage.Native;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On School Property", CalloutProbability.Medium, "An individual trespassing on school property", "Code 2", "LSPD")]

    public class TrespassingOnSchoolGrounds : Callout
    {
        private string[] pedList = new string[] { "PLAYER_TWO", "PLAYER_ZERO", "PLAYER_ONE", "IG_AMANDATOWNLEY", "S_F_Y_BARTENDER_01", "IG_BEVERLY", "U_F_Y_BIKERCHIC", "G_M_M_CHEMWORK_01", "MP_F_FREEMODE_01", "HC_HACKER", "A_F_Y_RURMETH_01", "MP_F_COCAINE_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_STUNGUN", "WEAPON_DAGGER", "WEAPON_KNIFE", "WEAPON_WRENCH", "WEAPON_RAYPISTOL", "WEAPON_AUTOSHOTGUN", "WEAPON_ASSAULTRIFLE", "WEAPON_CARBINERIFLE" };
        private Vector3 spawnpoint;
        private Blip blip;
        private Ped suspect;
        private int counter;
        private float heading;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1602.71f, 206.43f, 59.28f);
            heading = 100.56f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            AddMinimumDistanceCheck(100f, spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "An unknown individual trespassing on school property");
            CalloutMessage = "Reports of an unknown person trespassing";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On School Grounds callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On School Property", "~b~Dispatch~w~: Suspect has been spotted! Respond ~r~Code 2~w~.");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Orange;
            blip.IsRouteEnabled = true;
            blip.Alpha = 0.5f;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {


            base.OnCalloutNotAccepted();
        }
    }
}
