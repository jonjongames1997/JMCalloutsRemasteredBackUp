using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System.Drawing;

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
            vehicleBlip = new Blip(vehicleOnFire)
            {
                Color = Color.Red,
                Scale = 0.8f,
                IsRouteEnabled = true,
                Name = "Vehicle On Fire",
            };

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (vehicleBlip) vehicleBlip.Delete();
            if (vehicleOnFire) vehicleOnFire.Delete();
            if (driver) driver.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (!vehicleOnFire)
            {
                End();
                return;
            }

            if (vehicleOnFire.EngineHealth >= 0 || !vehicleOnFire.IsOnFire)
            {
                End();
                return;
            }

            if (Game.LocalPlayer.Character.DistanceTo(vehicleOnFire) < 30f && !IsEnding && vehicleOnFire.EngineHealth > 0)
            {
                vehicleOnFire.EngineHealth = -1000f;
                Game.DisplayHelp("Put that fire out!");
            }

            base.Process();
        }

        public override void End()
        {
            if (vehicleOnFire) vehicleOnFire.Dismiss();
            if (vehicleBlip) vehicleBlip.Delete();
            if (driver) driver.Dismiss();

            Game.LogTrivial("[JM Callouts Remastered Log]: Vehicle Fire is code 4!");

            base.End();
        }
    }
}
