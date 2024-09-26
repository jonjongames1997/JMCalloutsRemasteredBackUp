using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Domestic Disturbance - Grapeseed", CalloutProbability.Medium, "A couple of people arguing", "Code 2", "BCSO")]

    public  class DomesticDisturbanceGrapeSeed : Callout
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
            spawnPoint = new(1668.33f, 4750.65f, 41.88f);
            heading = 276.64f;
            suspectSpawnpoint = new(1651.86f, 4744.29f, 42.02f);
            suspectHeading = 208.55f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallouts_DomesticDisturbance_Grapeseed_Callout_Audio_1", spawnPoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A neighbor's reporting a loud argument next door.");
            CalloutMessage = "Reports of a domestic disturbance";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Domestic Disturbance - Grapeseed callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Grapeseed", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_DomesticDisturbance_Grapeseed_Callout_Audio_2");

            victim = new Ped(spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;
            victim.IsValid();

            victim.Tasks.PlayAnimation(new AnimationDictionary("friends@frj@ig_1"), "wave_a", -1f, AnimationFlags.Loop);
            suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);

            suspect = new Ped(suspectSpawnpoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            suspect.IsValid();
            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;

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


            if(MainPlayer.DistanceTo(victim) <= 10f)
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
                        Game.DisplaySubtitle("~o~Victim~w~: Hello, Officer. I am doing fine.... well, kinda. We are having an argument over headphones and earbuds. My buddy claims that over the head headphones is different from headphones. I told my buddy is stil headphones. I do apologize for you being called out.");
                    }
                    if (counter == 3)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Is that why I was being called out here over headphones and earbuds?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I'll solve this argument right now. Headphones is headphones. The over the ear headphones is considered headphones. Earbuds is earbuds. I swear y'all Gen Z's are being idiotic.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: But I will talk to your buddy and tell them what I said.");
                    }
                    if (counter == 6)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: I'm not a Gen Z, officer, my buddy is. But ok officer.");
                    }
                    if (counter == 7)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("Conversation Ended. Talk to the ~r~Suspect~w~. Roleplay it out.");
                    }
                }
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();

        }

        public override void End()
        {
            if (victim) victim.Dismiss();
            if (suspect) suspect.Dismiss();
            if (vicBlip) vicBlip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Grapeseed", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Domestic Disturbance - Grapeseed is Code 4!");
        }
    }
}
