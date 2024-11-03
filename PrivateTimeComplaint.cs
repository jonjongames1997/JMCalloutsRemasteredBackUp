using CalloutInterfaceAPI;

namespace JMCalloutsRemastered
{

    [CalloutInterface("[JM Callouts] Private Time Complaint", CalloutProbability.Medium, "Reports of a couple having sexy time in their backyard", "CODE 2", "LEO")]

    internal class PrivateTimeComplaint : Callout
    {
        private static readonly string[] femalePedList = new string[] { "ig_abigail", "ig_amandatownley", "csb_anita", "s_f_y_bartender_01", "a_f_m_beach_01", "a_f_y_beach_01", "ig_janet" };
        private static readonly string[] malePedList = new string[] { "a_m_y_jetski_01", "ig_jimmydisanto", "u_m_y_justin", "ig_lestercrest" };
        private static Ped suspect1;
        private static Ped suspect2;
        private static Blip susBlip1;
        private static Blip susBlip2;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
