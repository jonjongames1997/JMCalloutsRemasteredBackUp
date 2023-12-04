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
        private Vector3 spawnPoint;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f, 500f));

            vehicleOnFire = new Vehicle("BULLET", spawnPoint);
            vehicleOnFire.IsPersistent = true;
            vehicleOnFire.EngineHealth = -1000f;

            driver = vehicleOnFire.CreateRandomDriver();
            driver.IsPersistent = true;
            driver.Kill();

            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of a vehicle on fire. Respond Code 3!");
            CalloutMessage = "Vehicle on Fire";
            CalloutPosition = spawnPoint;

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

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (!vehicleOnFire)
            {
                End();
                return;
            }

            if(vehicleOnFire.EngineHealth >= 0 || !vehicleOnFire.IsOnFire)
            {
                End();
                return;
            }

            if(Game.LocalPlayer.Character.DistanceTo(vehicleOnFire) < 30f && !IsEnding && vehicleOnFire.EngineHealth > 0)
            {
                vehicleOnFire.EngineHealth = -1000f;
                Game.DisplayHelp("Put that fire out!");
            }

            if (Settings.ActiveAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnPoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.Firetruck);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnPoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.Ambulance);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnPoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.LocalUnit);
            }
            else { Settings.ActiveAIBackup = false; }

            base.Process();
        }

        public override void End()
        {


            base.End();
        }
    }
}
