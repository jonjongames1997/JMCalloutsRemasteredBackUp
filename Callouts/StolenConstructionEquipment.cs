using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Stolen Construction Equipment", CalloutProbability.Medium, "Reports of stolen construction equipment.", "Code 3", "LSPD")]


    public class StolenConstructionEquipment : Callout
    {
        private static readonly string[] constructionVehicles = new string[] { "BULLDOZER", "CUTTER", "DUMP", "MIXER", "MIXER2", "HANDLER", "RUBBLE", "TIPTRUCK", "TIPTRUCK2", "BISON3", "SCRAP" };
        private static Vehicle constructionVehicle;
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip blip;
        private static LHandle pursuit;
        private static bool pursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Stolen_Construction_Equipment_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Mulitple reports of a stolen construction equipment");
            CalloutMessage = "Reports of stolen construction equipment";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Stolen Construction Equipment callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stolen Construction Equipment", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_3_Audio");

            constructionVehicle = new Vehicle(constructionVehicles[new Random().Next((int)constructionVehicles.Length)], spawnpoint);
            constructionVehicle.IsPersistent = true;

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Dispatch", "Loading ~g~Information~w~ of the ~o~LSPD Database~w~...");
            LSPD_First_Response.Mod.API.Functions.DisplayVehicleRecord(constructionVehicle, true);
            suspect = new Ped(spawnpoint);
            suspect.WarpIntoVehicle(constructionVehicle, -1);
            suspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;

            blip = suspect.AttachBlip();

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            pursuitCreated = true;
            UltimateBackup.API.Functions.callPursuitBackup();

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (blip) blip.Delete();
            if (constructionVehicle) constructionVehicle.Delete();
            if (suspect) suspect.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (constructionVehicle) constructionVehicle.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stolen Construction Equipment", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            UltimateBackup.API.Functions.dismissAllBackupUnits();
            base.End();

            Game.LogTrivial("[JM Callouts Remastered Log]: Stolen Police Vehicle is Code 4!");
        }
    }
}
