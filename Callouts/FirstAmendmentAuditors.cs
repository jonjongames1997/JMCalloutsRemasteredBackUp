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
        private static string pronoun;
        private static string pronoun2;

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

            if (suspect.IsMale)
                pronoun = "he";
            else
                pronoun = "she";

            if (suspect.IsMale)
                pronoun2 = "him";
            else
                pronoun2 = "her";

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
                if (Settings.HelpMessages)
                {
                    Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~to talk to Suspect. ~y~Approach with caution.", false);
                }
                else
                {
                    Settings.HelpMessages = false;
                }

                if (Game.IsKeyDown(Settings.Dialog))
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
                if (Game.IsKeyDown(Settings.Dialog))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Hello, what seemd to be the issue here, " + malefemale + ".");
                    }
                    if(counter == 2)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: Hello, Officer, The person you were talking to came in and started filming with their electronic device. (1/6)");
                    }
                    if (counter == 3)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: I told " + pronoun + " that this is a federal government building and " + pronoun +  " is not allowed to film. (2/6)");
                    }
                    if (counter == 4)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: Then " + pronoun + " proceeds to mention something about 'Freedom Of The Press'. I asked for some sort of identification (3/6)");
                    }
                    if (counter == 5)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: or paperwork to identofy that their are from the press. " + pronoun + " said they don't need one. (4/6)");
                    }
                    if (counter == 6)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: and I said, 'Yes, you do.'. " + pronoun + " continued to argue about it. Then I asked " + pronoun2 + " to leave the premises. (5/6)");
                    }
                    if (counter == 7)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: " + pronoun2 + " refused to leave, so I called you guys down here for assistance. We want them removed from the property and trespassed as possible, officer.(6/6)");
                    }
                    if(counter == 8)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Ok, I will talk to them. Thank you for your cooperation and we'll take it from here.");
                    }
                    if(counter == 9)
                    {
                        securityOfficer.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Security~w~: Thank you, Officer. Stay safe out there. Enjoy your day.");
                    }
                    if(counter == 10)
                    {
                        securityOfficer.Tasks.Wander();
                        Game.DisplaySubtitle("Convo ended. Finish talking to the auditor and deal with the situation.");
                    }
                }
            }


            if (MainPlayer.IsDead)
            {
                End();
            }

            if (Game.IsKeyDown(Settings.EndCall))
            {
                End();
            }

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
