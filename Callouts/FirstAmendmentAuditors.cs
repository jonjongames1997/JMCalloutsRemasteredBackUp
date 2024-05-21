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
                        Game.DisplayHelp("Go talk to ~r~Security~w~.");
                    }
                }
            }

            if(MainPlayer.DistanceTo(securityOfficer) <= 10f)
            {
                counter++;

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w: Hello, " + malefemale + ". Can I have your side of the story?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: The person you were talking to came in filiming with their electronic device (1/?)");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~g~Security~w: I told he or she that this is a federal government building, you are not allowed to film. (2/?)");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: then he/she started bringing up about 'Freedom Of The Press', and I asked for some sort of identification or paperwork (3/?)");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: to identify that you're the press. They said that they don't need one and I said yes you do then they continue to argue then I asked them to leave the premises (4/?)");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: They refused to leave the property so I called y'all for assistance. We want them removed from the property and trespassed, if possible, Officer.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Okay, I'll see what I can do. Thanks for your cooperation. We appreciate it.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~g~Security~w~: No problem, Officer. I am just doing my job like how you're doing yours.");
                    }
                    if(counter == 9)
                    {
                        securityOfficer.Dismiss();
                        Game.DisplayHelp("Go talk to the ~r~Suspect~w~ and deal with the situation that you may see fit");
                    }
                }
            }

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                counter++;

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Okay, " + malefemale + ", The security officer wants you to leave the premises now.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Are you fucking serious????");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Leave now or you'll be in cuffs and charged with refusing to leave. Your choice, " + malefemale + ".");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You're under arrest " + malefemale + ".");
                    }
                    if(counter == 5)
                    {
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                        suspect.Tasks.FightAgainst(MainPlayer);
                        Game.DisplaySubtitle("Conversation Ended! Arrest the suspect, Officer.");
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
