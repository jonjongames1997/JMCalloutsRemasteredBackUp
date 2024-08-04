using CalloutInterfaceAPI;
using System.Threading;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Stolen Military Equipment", CalloutProbability.Medium, "Reports of stolen Military Equipment", "Code 3", "SWAT")]


    public class StolenMilitaryEquipment : Callout
    {
        private static readonly string[] militaryVehicles = new string[] { "APC", "BARRACKS", "BARRACKS2", "BARRACKS3", "BARRAGE", "CHERNOBOG", "CRUSADER", "HALFTRACK", "KHANJALI", "MINITANK", "RHINO", "SCARAB", "SCARAB2", "SCARAB3", "THRUSTER", "VETIR", "TRAILERSMALL2" };
        private static Vehicle militaryVehicle;
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip blip;
        private static LHandle pursuit;
        private static bool pursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_STOLEN_VEHICLE_SPOTTED UNITS_RESPOND_CODE_03_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of stolen military equipment");
            CalloutMessage = "Reports of stolen military equipment. Respond Code 3.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Stolen Military Equipment callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stolen Military Equipment", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);


            militaryVehicle = new Vehicle(militaryVehicles[new Random().Next((int)militaryVehicles.Length)], spawnpoint);
            militaryVehicle.IsPersistent = true;

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Dispatch", "Loading ~g~Information~w~ of the ~o~LSPD Database~w~...");
            LSPD_First_Response.Mod.API.Functions.DisplayVehicleRecord(militaryVehicle, true);
            suspect = new Ped(spawnpoint);
            suspect.WarpIntoVehicle(militaryVehicle, -1);
            suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL_MK2", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;

            blip = suspect.AttachBlip();

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            pursuitCreated = true;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (militaryVehicle) militaryVehicle.Delete();
            if (suspect) suspect.Delete();
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();
            if (suspect) suspect.Dismiss();
            if (militaryVehicle) militaryVehicle.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Stolen Construction Equipment", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURHTER_UNITS_REQUIRED");

            Game.LogTrivial("[JM Callouts Remastered Log]: Stolen Military Equipment is CODE 4!");
        }
    }
}
