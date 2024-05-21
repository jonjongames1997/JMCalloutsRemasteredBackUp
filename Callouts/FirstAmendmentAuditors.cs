using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("1st Amendment Auditor", CalloutProbability.High, "Security reporting an individual refusing to leave government property", "Code 2", "LSPD")]


    public class FirstAmendmentAuditors : Callout
    {
        private static Ped suspect;
        private static Blip suspectBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SECURITY_REQUESTING_ASSISTANCE UNITS_RESPOND_CODE_02_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Security officer requiring assitance.");
            CalloutMessage = "An individual claiming to be 'The Press' refusing to leave government building.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
