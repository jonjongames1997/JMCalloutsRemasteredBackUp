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
using JMCalloutsRemastered.Callouts;
using LSPD_First_Response.Mod.API;
using System.Threading;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Stolen Construction Equipment", CalloutProbability.Medium, "Reports of stolen construction equipment.", "Code 3", "LSPD")]


    public class StolenConstructionEquipment : Callout
    {
        private string[] constructionVehicles = new string[] { "BULLDOZER", "CUTTER", "DUMP", "MIXER", "MIXER2", "HANDLER", "RUBBLE", "TIPTRUCK", "TIPTRUCK2" };
        private Vehicle constructionVehicle;
        private Ped suspect;
        private Vector3 spawnpoint;
        private Blip blip;
        private LHandle pursuit;
        private bool pursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Mulitple reports of a stolen construction equipment");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SUSPECT_ON_THE_RUN_01 UNITS_RESPOND_CODE_03_02");
            CalloutMessage = "Reports of stolen construction equipment";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Stolen Construction Equipment callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stolen Construction Equipment", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            constructionVehicle = new Vehicle(constructionVehicles[new Random().Next((int)constructionVehicles.Length)], spawnpoint);

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Dispatch", "Loading ~g~Information~w~ of the ~o~LSPD Database~w~...");
            LSPD_First_Response.Mod.API.Functions.DisplayVehicleRecord(constructionVehicle, true);
            suspect = new Ped(spawnpoint);
            suspect.WarpIntoVehicle(constructionVehicle, -1);
            suspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;

            blip = suspect.AttachBlip();

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            pursuitCreated = true;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (blip) blip.Delete();
            if (constructionVehicle) constructionVehicle.Delete();
            if (suspect) suspect.Delete();

            base.OnCalloutNotAccepted();
        }


    }
}
