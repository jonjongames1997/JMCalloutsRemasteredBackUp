using CalloutInterfaceAPI;


namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("Rival Gang Shootout", CalloutProbability.High, "Reports of a rival gang shootout", "Code 99", "SWAT")]

    public class RivalGangShootout : Callout
    {

        // Vagos Peds 
        private static Ped VagosPed1 => vagosPeds[0];
        private static Ped VagosPed2 => vagosPeds[1];
        private static Ped VagosPed3 => vagosPeds[2];
        private static Ped VagosPed4 => vagosPeds[3];


        // Lost MC Peds
        private static Ped LostMCPeds1 => LostMCPeds[0];
        private static Ped LostMCPeds2 => LostMCPeds[1];
        private static Ped LostMCPeds3 => LostMCPeds[2];
        private static Ped LostMCPeds4 => LostMCPeds[3];

        private static Vector3 spawnpoint;

        // Blips
        private static Blip blip;
        private static Blip blip2;
        private static Blip blip3;
        private static Blip blip4;
        private static Blip blip5;
        private static Blip blip6;
        private static bool hasBegunAttacking;

        // Arrays
        private static Ped[] vagosPeds = { VagosPed1, VagosPed2, VagosPed3, VagosPed4 };
        private static Ped[] LostMCPeds = { LostMCPeds1, LostMCPeds2, LostMCPeds3, LostMCPeds4 };

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 CITIZENS_REPORT_01 CRIME_GANG_SHOOTOUT UNITS_RESPOND_CODE_99_01", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a gang shootout");
            CalloutMessage = "Multiple shots fired in the area";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Rival Gang Shootout callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Rival Gang Shootout", "~b~Dispatch~w~: Multiple suspects spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);



            return base.OnCalloutAccepted();
        }
    }
}
