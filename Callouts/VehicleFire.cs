using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using CalloutInterfaceAPI;
using System.Windows.Forms;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Vehicle Fire", CalloutProbability.Medium, "Citizen's report of a vehicle on fire", "Code 3", "SAHP")]

    public class VehicleFire : Callout
    {
        private Vehicle vehicleOnFire;
        private Blip vehicleBlip;
        private Ped driver;

        public override bool OnBeforeCalloutDisplayed()
        {
            Vector3 spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f, 500f));

            vehicleOnFire = new Vehicle("BULLET", spawnpoint);
            vehicleOnFire.IsPersistent = true;
            vehicleOnFire.EngineHealth = -1000f;

            driver = vehicleOnFire.CreateRandomDriver();
            driver.IsPersistent = true;
            driver.Kill();

            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of a vehicle on fire. Respond Code 3!");
            CalloutMessage = "Vehicle on Fire";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
