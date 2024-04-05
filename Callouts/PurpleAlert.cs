using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("Purple Alert", CalloutProbability.High, "Reports of a missing person with disability", "Code 2", "LSPD")]


    public class PurpleAlert : Callout
    {

        private static readonly string[] pedList = new string[] { "ig_abigail", "ig_amandatownley", "g_m_m_armgoon_01", "ig_money", "ig_barry", "a_f_m_bevhills_01" };
        private static Ped suspect;
        private static Blip blip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("AMBERALERT ATTENTION_ALL_UNITS_01 WE_HAVE_01 CITIZENS_REPORT_01 CRIME_2_42_01 UNITS_RESPOND_CODE_02_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A concerned family member reporting the loved one lost after not responfing to phone calls or texts.");
            CalloutMessage = "An individual not responding to calls or texts, has a disability, has been last seen since Monday and never returned home.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }


    }
}
