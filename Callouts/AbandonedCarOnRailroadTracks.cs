using CalloutInterfaceAPI;
using StopThePed.API;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Abandoned Vehicle On RailRoad Tracks", CalloutProbability.Medium, "Reports of an abandoned vehicle on tracks", "Code 2", "LSPD")]

    public class AbandonedCarOnRailroadTracks : Callout
    {
        private static Vehicle vehicle;
        private static Blip vehicleBlip;
        private static Vector3 spawnpoint;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(222.25f, -2401.46f, 8.10f), // Place Place
                new(456.48f, -1648.28f, 28.56f), // Innocence Blvd
                new(547.26f, -1423.92f, 28.65f), // Capital Blvd
                new(2719.48f, 3900.61f, 43.06f), // Near Maude's House
                new(2839.44f, 4436.06f, 48.93f),
                new(3012.24f, 4154.55f, 58.57f),
                new(1937.51f, 1971.20f, 66.44f),
                new(2662.52f, 284.24f, 94.59f),
                new(-231.55f, -1431.56f, 31.37f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an abandoned vehicle on train tracks.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("WE_HAVE CRIME_CAR_STUCK_ON_RAILROAD_TRACKS UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "Abandoned Vehicle";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On Railroad Property callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "Abandoned Vehicle On Railroad Tracks", "~b~Dispatch~w~: Vehicle Spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            vehicle = new Vehicle(spawnpoint);
            vehicle.IsPersistent = true;
            vehicle.IsValid();

            vehicleBlip = vehicle.AttachBlip();
            vehicleBlip.Color = System.Drawing.Color.Chocolate;
            vehicleBlip.IsRouteEnabled = true;

            StopThePed.API.Functions.requestDispatchVehiclePlateCheck(true);

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (vehicle) vehicle.Delete();
            if (vehicleBlip) vehicleBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(MainPlayer.DistanceTo(vehicle) <= 5f)
            {
                Game.DisplayHelp("Inspect the vehicle then call a tow.");
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            base.End();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Abandoned Vehicle On Railroad Tracks", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Abandoned Vehicle On Railroad Tracks is Code 4!");
        }

    }
}
