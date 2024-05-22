using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Domestic Disturbance - Harmony/Route 68", CalloutProbability.High, "Reports of a domestic disturbance", "Code 3", "BCSO")]

    public class DomesticDisturbance2 : Callout
    {
        private static Ped victim;
        private static Ped suspect;
        private static Blip vicBlip;
        private static Blip susBlip;
        private static Vector3 spawnPoint;
        private static Vector3 suspectSpawnpoint;
        private static float suspectHeading;
        private static float heading;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = new(1112.87f, 2651.00f, 38.00f);
            heading = 161.52f;
            suspectSpawnpoint = new(152.36f, -1003.18f, -99.00f);
            suspectHeading = 168.14f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_DOMESTIC_DISTURBANCE UNITS_RESPOND_CODE_03_01", spawnPoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of 2 individuals having an argument.");
            CalloutMessage = "Manager reporting a domestic disturbance. 1 individual may be armed with a weapon.";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Domestic Disturbance - Route 68 callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Route 68 Motel", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            victim = new Ped("IG_AMANDATOWNLEY", spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            suspect = new Ped("IG_TRACEYDISSANTO", suspectSpawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;

            vicBlip = victim.AttachBlip();
            vicBlip.Color = System.Drawing.Color.DodgerBlue;
            vicBlip.IsRouteEnabled = true;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();
            if (victim) victim.Delete();
            if (vicBlip) vicBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(MainPlayer.DistanceTo(victim) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~Victim~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Blaine County Sheriff's Office. Hello, there " + malefemale + ". How are you? and what's seems to be the problem? We've gotten a call from the motel manager saying that you and your daughter were arguing about something.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: Yes, officer. My daughter is trying to sell her body to get on tv by Lazlow 'The Pervert' Fame or Shame host. I told her, you're not doing that to get on tv. You have to audition like everyone else. (1/2)");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: I followed her to the motel, I took her car key away from her and I told her that 'Until you move out of my house, you're not doing that shit.' (2/2)");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Okay, I will talk to her, what's your daughter's name?");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: Tracey and she is 21.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Okay, stay here, I will try to talk to her.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: Thank you, Officer.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("Go talk to ~y~Tracey~w~. Roleplay it out.");
                    }
                    if(counter == 9)
                    {
                        suspect.Inventory.GiveNewWeapon("WEAPON_TACTICALRIFLE", 500, true);
                        suspect.Tasks.FightAgainst(victim);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            if (victim) victim.Dismiss();
            if (suspect) suspect.Dismiss();
            if (vicBlip) vicBlip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Route 68 Motel", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Domestic Disturbance - Route 68 Motel is Code 4!");
        }
    }
}
