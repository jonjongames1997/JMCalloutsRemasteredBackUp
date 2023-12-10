using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using Rage.Native;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Dead Body", CalloutProbability.High, "Reports of a dead body", "Code 3", "LSPD")]

    public class DeadBody : Callout
    {
        private Ped deadBody;
        private Blip deadBlip;
        private Vector3 spawnpoint;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));

            deadBody = new Ped(spawnpoint);
            deadBody.IsPersistent = true;
            deadBody.Kill();

            NativeFunction.Natives.APPLY_PED_DAMAGE_PACK(deadBody, "BigHitByVehicle", 1f, 1f);

            CalloutMessage = "Reports of a dead body";
            CalloutPosition = spawnpoint;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 50);

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Dead body callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Dead Body", "~b~Dispatch: The dead body has been spotted! Respond ~r~Code 3");

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
            if (deadBody) deadBody.Delete();
            if (deadBlip) deadBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (deadBody.DistanceTo(Game.LocalPlayer.Character) < 2f)
            {
                End();
                Game.DisplayNotification("Call EMS to attempt CPR or Call a Coroner to pick up the deceased body.");
            }

            base.Process();
        }

        public override void End()
        {
            if (deadBody) deadBody.Dismiss();
            if (deadBlip) deadBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Dead Body", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Dead Body is code 4!");
        }
    }
}
