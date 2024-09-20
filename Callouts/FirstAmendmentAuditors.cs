using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] 1st Amendment Auditor - Union Depository", CalloutProbability.Medium, "Security reporting an individual refusing to leave government property", "Code 2", "LSPD")]


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
            spawnpoint = new(15.28f, -673.21f, 47.73f);
            suspectHeading = 166.38f;
            securitySpawnpoint = new(2.71f, -708.50f, 45.97f);
            securityHeading = 203.73f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_First_Amendment_Auditor_Callout_Audio_1");
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

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_First_Amendment_Auditor_Callout_Audio_2");

            suspect = new Ped(spawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspectBlip = suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.AliceBlue;
            suspectBlip.IsRouteEnabled = true;

            securityOfficer = new Ped("s_m_m_security_01", securitySpawnpoint, securityHeading);
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
                        Game.DisplaySubtitle("~y~Suspect~w~: This security officer is not letting me film. I have the right of ~y~'Freedom of the Press'~w~ to film anywhere I want.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You know you are on federal government property, right?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Yeah, so what? I have the right to film. It's called 'Freedom Of The Press', you dumb fuck.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, " + malefemale + ", I'm not gonna argue with you on this subject. Sit tight here and I'll talk to security and get there side.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Fine!!!");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("Go talk to ~g~Security~w~ then talk to the suspect again.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Okay, " + malefemale + ", The security officer wants you to leave the premises now.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Are you fucking serious????");
                    }
                    if(counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Leave now or you'll be in cuffs and charged with refusing to leave. Your choice, " + malefemale + ".");
                    }
                    if(counter == 11)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I'm not leaving.");
                    }
                    if(counter == 12)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You're under arrest " + malefemale + ".");
                    }
                    if(counter == 13)
                    {
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                        suspect.Tasks.FightAgainst(MainPlayer);
                        Game.DisplaySubtitle("Conversation Ended! Arrest the suspect, Officer.");
                    }
                }
            }

            if(MainPlayer.DistanceTo(securityOfficer) <= 10f)
            {

                Game.DisplayHelp("YOU ARE ~r~NOT~w~ AUTHORIZED TO DETAIN A SECURITY OFFICER FOR ANY REASON.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Hey there, " + malefemale + ". Can you exlain what's going on?");
                    }
                    if(counter == 2)
                    {
                        LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("SecurityOfficerDialogue");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, we'll take care of it from here.");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: Thank you for your time.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("Convo ended. Go back and finish talking to the suspect.");
                        securityOfficer.Tasks.Wander();
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
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");


            Game.LogTrivial("[LOG]: JM Callouts Remastered - First Amendment Auditor is Code 4!");

            base.End();
        }

    }
}
