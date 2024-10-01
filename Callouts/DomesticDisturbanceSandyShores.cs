using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Domestic Disturbance - Sandy Shores", CalloutProbability.Medium, "A couple having a argument", "Code 2", "BCSO")]


    public class DomesticDisturbanceSandyShores : Callout
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
            spawnPoint = new(1814.89f, 3906.81f, 33.77f);
            heading = 291.56f;
            suspectSpawnpoint = new(1818.70f, 3908.09f, 33.76f);
            suspectHeading = 113.21f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_DOMESTIC_DISTURBANCE UNITS_RESPOND_CODE_02_02", spawnPoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A neighbor's reporting a loud argument next door.");
            CalloutMessage = "Reports of a domestic disturbance";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Domestic Disturbance - Sandy Shores callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Grapeseed", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            victim = new Ped(spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;
            victim.IsValid();

            suspect = new Ped(suspectSpawnpoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;
            suspect.IsValid();

            vicBlip = suspect.AttachBlip();
            vicBlip.Color = System.Drawing.Color.Yellow;
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
            base.Process();


            if (MainPlayer.DistanceTo(victim) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~Victim~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        victim.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Blaine County Sheriff's Office. Hello, there " + malefemale + ". How are you? and what's seems to be the problem? I have gotten a call from your neighbor saying that you and another person were arguing.");
                    }
                    if (counter == 2)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: Hello, Officer. I am doing fine.... well, kinda. We are having an argument over my buddy's bullshit lies about bringing home strangers at 3'o clock in the morning.");
                    }
                    if (counter == 3)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I see. Anything else you can tell me?");
                    }
                    if (counter == 4)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: No, officer.");
                    }
                    if (counter == 5)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("Conversation Ended. Talk to the ~r~Suspect~w~. Roleplay it out.");
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

        }

        public override void End()
        {
            if (victim) victim.Dismiss();
            if (suspect) suspect.Dismiss();
            if (vicBlip) vicBlip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Sandy Shores", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Domestic Disturbance - Grapeseed is Code 4!");
        }
    }
}
