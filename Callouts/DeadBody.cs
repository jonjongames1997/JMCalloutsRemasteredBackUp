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
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Dead Bpdy", CalloutProbability.High, "Reports of a dead body", "Code 3", "LSPD")]

    public class DeadBody : Callout
    {
        private Ped deadBody;
        private Blip deadBlip;

        public override bool OnBeforeCalloutDisplayed()
        {
            Vector3 spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));

            deadBody = new Ped(spawnPoint);
            deadBody.IsPersistent = true;
            deadBody.Kill();

            NativeFunction.Natives.APPLY_PED_DAMAGE_PACK(deadBody, "BigHitByVehicle", 1f, 1f);

            CalloutMessage = "Reports of a dead body";
            CalloutPosition = spawnPoint;

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_HIT_AND_RUN_03 IN_OR_ON_POSITION");
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 50);

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            deadBlip = new Blip(deadBody)
            {
                Color = Color.Red,
                IsRouteEnabled = true,
                Scale = 0.8f,
                Name = "Dead Person",
            };

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {


            base.OnCalloutNotAccepted();
        }
    }
}
