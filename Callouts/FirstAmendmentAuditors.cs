using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("1st Amendment Auditor", CalloutProbability.High, "Security reporting an individual refusing to leave government property", "Code 2", "LSPD")]


    public class FirstAmendmentAuditors : Callout
    {
        private static Ped suspect;
        private static Ped securityOfficer;
        private static readonly string[] pedList = new string[] { "mp_s_m_armoured_01", "s_m_m_armoured_01", "s_m_m_armoured_02", "ig_casey", "s_m_m_chemsec_01", "csb_mweather", "s_m_m_security_01", "mp_m_securoguard_01" };
        private static Blip suspectBlip;
        private static Blip securityBlip;
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
            Game.LogTrivial("JM Callouts Remastered Log: First Amendment Auditor callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~First Amendment Auditor", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.AliceBlue;
            suspectBlip.IsRouteEnabled = true;

            securityOfficer = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            securityOfficer.IsPersistent = true;
            securityOfficer.BlockPermanentEvents = true;

            securityBlip = securityOfficer.AttachBlip();
            securityBlip.Color = System.Drawing.Color.Cornsilk;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;


            return base.OnCalloutAccepted();
        }


        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (securityOfficer) securityOfficer.Delete();
            if (suspectBlip) suspectBlip.Delete();
            if (securityBlip) securityBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {




            base.Process();
        }

    }
}
