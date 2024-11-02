using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Public Ordinance Easter Egg 2", CalloutProbability.Medium, "A Twitch streamer yelling and screaming 'You're Banned'", "CODE 2", "LSPD")]

    internal class PublicOrdinanceEasterEgg2 : Callout
    {
        private static readonly string[] pedList = new string[] { "a_m_m_afriamer_01", "a_m_y_downtown_01", "a_m_m_farmer_01", "a_m_m_fatlatin_01", "a_f_m_fatcult_01", "a_f_m_fatwhite_01", "a_m_m_genfat_01" };
        private static Vector3 spawnpoint;
        private static Blip susBlip;
        private static Ped suspect;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(),
                new(),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Public_Ordinance_Easter_Egg_2_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Twitch streamer being loud as fuck");
            CalloutMessage = "A loud twitch streamer yelling and screaming.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }


        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }

    }
}
