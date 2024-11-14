using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Dead Body", CalloutProbability.Medium, "Reports of a dead body", "Code 3", "LSPD")]

    public class DeadBody : Callout
    {
        private static Ped deadBody;
        private static Blip deadBlip;
        private static Vector3 spawnpoint;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a deceased body found.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallotus_DeadBody_Callout_Audio_1", spawnpoint);
            CalloutMessage = "Reports of a deceased body";
            CalloutPosition = spawnpoint;

            deadBody = new Ped(spawnpoint);
            deadBody.IsPersistent = true;
            deadBody.BlockPermanentEvents = true;
            deadBody.Kill();

            NativeFunction.Natives.APPLY_PED_DAMAGE_PACK(deadBody, "BigHitByVehicle", 1f, 1f);

            CalloutMessage = "Reports of a dead body";
            CalloutPosition = spawnpoint;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Dead body callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Dead Body", "~b~Dispatch~w~: The dead body has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout.");

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallotus_DeadBody_Callout_Audio_2");

            LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.LocalUnit);
            LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.LocalUnit);

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
            if (deadBody.DistanceTo(MainPlayer) < 10f)
            {
                Game.DisplayNotification("Call EMS to attempt CPR or Call a Coroner to pick up the deceased body.");
            }

            if (MainPlayer.IsDead)
            {
                End();
            }

            bool flag = Game.IsKeyDown(Settings.EndCall);
            if (flag)
            {
                this.End();
            }

            base.Process();
        }

        public override void End()
        {
            if (deadBody) deadBody.Dismiss();
            if (deadBlip) deadBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Dead Body", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Dead Body is code 4!");
        }
    }
}
