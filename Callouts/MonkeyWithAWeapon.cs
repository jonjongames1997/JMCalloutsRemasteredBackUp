using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Monkey With A Weapon", CalloutProbability.High, "A citizen's report of a chimpanzee with a weapon", "Code 3", "LSPD")]

    public class MonkeyWithAWeapon : Callout
    {
        private string[] pedList = new string[] { "A_C_CHIMP", "IG_ORLEANS", "A_C_RHESUS" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MG", "WEAPON_COMBATMG", "WEAPON_TACTICALRIFLE", "WEAPON_CARBINERIFLE" };
        private Ped suspect;
        private Vector3 spawnpoint;
        private Vector3 searcharea;
        private Blip blip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            scenario = new Random().Next(0, 100);
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of a chimpanzee with a weapon. Respond Code 3.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_BRANDISHING_WEAPON_02 UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed chimpanzee";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Monkey With A Weapon callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Monkey With A Weapon", "~b~Dispatch~w~: The primate has been spotted! Respond ~r~Code 3~w~.");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();

            searcharea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(searcharea, 80f);
            blip.Color = Color.DarkOrange;
            blip.EnableRoute(Color.DarkOrange);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }
    }
}
