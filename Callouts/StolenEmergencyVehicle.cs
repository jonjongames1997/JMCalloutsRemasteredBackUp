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


            return base.OnBeforeCalloutDisplayed();
        }

    }
}
