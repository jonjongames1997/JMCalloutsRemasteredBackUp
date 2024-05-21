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


    }
}
