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
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Money Truck Robbery", CalloutProbability.VeryHigh, "Reports of a robbery on a money truck", "Code 3", "LSPD")]

    public class MoneyTruckRobbery : Callout
    {
        private Vehicle stockade;
        private Ped Aggressor1;
        private Ped Aggressor2;
        private Ped Aggressor3;
        private Ped Aggressor4;
        private Vector3 spawnpoint;
        private Vector3 vehicleSpawnpoint;
        private Blip blip1;
        private Blip blip2;
        private Blip blip3;
        private Blip blip4;
        private LHandle pursuit;
        private bool isPursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            vehicleSpawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SHOTS_FIRED_AT_AN_OFFICER_03 IN_OR_ON_POSITION UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed money truck robbery";
            CalloutPosition = vehicleSpawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
