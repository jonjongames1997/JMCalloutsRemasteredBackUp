using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("1st Amendment Auditor - FIB Headquarters", CalloutProbability.High, "Security reporting an individual refusing to leave government property", "Code 2", "LSPD")]


    public class FirstAmendmentAuditors : Callout
    {
        private static Ped suspect;
        private static Ped securityOfficer;
        private static Blip suspectBlip;
        private static Blip securityBlip;
        private static Vector3 spawnpoint;
        private static Vector3 securitySpawnpoint;
        private static float suspectHeading;
        private static float securityHeading;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new(115.31f, -748.42f, 45.75f);
            suspectHeading = 288.71f;
            securitySpawnpoint = new(104.70f, -739.23f, 45.76f);
            securityHeading = 170.97f;
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

            suspect = new Ped(spawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.AliceBlue;
            suspectBlip.IsRouteEnabled = true;

            securityOfficer = new Ped("s_m_m_security_01", spawnpoint, securityHeading);
            securityOfficer.KeepTasks = true;
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

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E ~w~to talk to Suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Hello, there " + malefemale + ". What seems to be the issue?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: This security officer is not letting me film. I have the right of ~y~'Freedom of the Press'~w~ to film anywhere I want.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You know you are in a federal government building, right?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Yeah, so what? I have the right to film. It's called 'Freedom Of The Press', you dumb fuck.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, " + malefemale + ", I'm not gonna argue with you on this subject. Sit tight here and I'll talk to security and get there side.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fine!!!");
                    }
                    if(counter == 7)
                    {
                        Game.DisplayHelp("Go talk to ~r~Security~w~. Roleplay it out.");
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (securityOfficer) securityOfficer.Dismiss();
            if (suspectBlip) suspectBlip.Delete();
            if (securityBlip) securityBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~First Amendment Auditor", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");


            Game.LogTrivial("[LOG]: JM Callouts Remastered - First Amendment Auditor is Code 4!");

            base.End();
        }

    }
}
