using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Bicycle Blocking Roadway", CalloutProbability.Medium, "Abandoned Bicycle Left On Street", "Code 1", "LSPD")]

    public class BicycleBlockingRoadway : Callout
    {
        private static readonly string[] bikeList = new string[] { "bmx", "scorcher", "cruiser", "fixter", "tribike", "tribike2", "tribike3" };
        private static Vehicle thebike;
        private static Blip blip;
        private static Vector3 spawnpoint;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-1146.43f, -1496.17f, 4.37f),
                new(898.76f, -2458.87f, 28.61f),
                new(969.30f, -1436.41f, 31.41f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Bicycle_Blocking_Roadway_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Bicycle left on the street.");
            CalloutMessage = "Reports of a bicycle blocking the road.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[Adam69 Callouts LOG]: Bicycle Blocking Roadway callout accepted!");
            Game.DisplayNotification("web_adam69callouts", "web_adam69callouts", "~w~Adam69 Callouts", "~w~Bicycle Blocking Roadway", "~b~Dispatch~w~: The vehicle has been spotted! Respond ~r~Code 2~w~.");

            if (Settings.HelpMessages)
            {
                Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);
            }
            else
            {
                Settings.HelpMessages = false;
            }

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_1_Response_Audio");

            thebike = new Vehicle(bikeList[new Random().Next((int)bikeList.Length)], spawnpoint, 0f);
            thebike.IsValid();
            thebike.IsPersistent = true;

            thebike.IsStolen = false;

            blip = thebike.AttachBlip();
            blip.Color = System.Drawing.Color.Yellow;
            blip.Alpha = 0.75f;
            blip.IsRouteEnabled = true;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (thebike) thebike.Delete();
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (MainPlayer.DistanceTo(thebike) <= 10f)
            {
                Game.DisplayHelp("Deal with the situation as you see fit.");
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            if (thebike) thebike.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~9-1-1 Hang Up", "~b~You~w~: Dispatch, we are ~g~Code 4~w~! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            base.End();

            Game.LogTrivial("[JM Callouts LOG]: Bicycle Blocking Roadway is Code 4!");
        }
    }
}
