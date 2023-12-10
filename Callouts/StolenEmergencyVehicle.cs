using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Mod.API;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Stolen Emergency Vehicle", CalloutProbability.Medium, "Reports of a stolen emergency vehicle", "CODE 3", "LSPD")]

    public class StolenEmergencyVehicle : Callout
    {
        private string[] emergencyVehicles = new string[] { "POLICE", "POLICE2", "POLICE3", "SHERIFF", "SHERIFF2", "POLICE4", "FBI", "FBI2", "AMBULANCE", "FIRETRUK", "POLICEB" };
        private Vehicle emergencyVehicle;
        private Ped suspect;
        private Vector3 spawnpoint;
        private Blip blip;
        private LHandle pursuit;
        private bool pursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a reckless driver in the area");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SUSPECT_ON_THE_RUN_01 UNITS_RESPOND_CODE_03_02");
            CalloutMessage = "Multiple reports of a reckless driver";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

    }
}
