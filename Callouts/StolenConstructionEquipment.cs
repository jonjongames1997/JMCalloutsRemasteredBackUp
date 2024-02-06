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
        private string[] constructionVehicle = new string[] { "BULLDOZER", "CUTTER", "DUMP", "MIXER", "MIXER2", "HANDLER", "RUBBLE", "TIPTRUCK", "TIPTRUCK2" };
        private Vehicle consVehicle;
        private Ped suspect;
        private Vector3 spawnpoint;
        private Blip blip;
        private LHandle pursuit;
        private bool hasPursuitBegun = false;

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
    }
}
