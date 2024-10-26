using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Public Ordinance - Streamer Easter Egg 1", CalloutProbability.Medium, "Twitch streamer rage disturbance", "CODE 2", "LEO")]

    public class PublicOrdinanceEasterEgg1 : Callout
    {


        // General Variables //
        private static readonly string[] pedList = new string[] { "a_m_m_afriamer_01", "u_m_y_antonb", "g_m_y_ballaeast_01", "g_m_y_ballaorig_01", "ig_ballasog", "g_m_y_ballasout_01", "a_m_m_beach_01", "s_m_m_bouncer_01" };
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Ped _Caller;
        private static Vector3 _CallerBlip;
        private static Vector3 Spawnpoint;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(),
                new(),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A person being very loud.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_PublicOrdinanceEasterEgg1_Callout_Audio");
            CalloutMessage = "A person being very loud, breaking controllers, and busting his house walls.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Public Ordinance Easter Egg 1 callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Ordinance Easter Egg 1", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;



            return base.OnCalloutAccepted();
        }

    }
}
