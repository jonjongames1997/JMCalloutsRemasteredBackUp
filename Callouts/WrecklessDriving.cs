using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using CalloutInterfaceAPI;
using System.Drawing;
using System.Windows.Forms;
using Rage;
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Reckless Driving", CalloutProbability.VeryHigh, "Reports of a reckless driver", "Code 3", "LSPD")]

    public class WrecklessDriving : Callout
    {
        private Vehicle vehicle;
        private Vector3 spawnpoint;
        private Blip driverBlip;
        private Ped driver;
        private LHandle pursuit;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            vehicle = new Vehicle("DOMINATOR", spawnpoint);
            vehicle.IsPersistent = true;

            driver = vehicle.CreateRandomDriver();
            driver.BlockPermanentEvents = true;
            driver.IsPersistent = true;
            driver.Tasks.CruiseWithVehicle(vehicle.TopSpeed, VehicleDrivingFlags.Emergency);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a reckless driver");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SUSPECT_ON_THE_RUN_02");
            CalloutMessage = "Reckless driving in the area";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
