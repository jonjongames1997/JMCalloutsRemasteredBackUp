using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Public Ordinance - Streamer Easter Egg 2", CalloutProbability.Medium, "A Twitch streamer yelling and screaming 'You're Banned'", "CODE 2", "LSPD")]

    internal class PublicOrdinanceEasterEgg2 : Callout
    {
        private static readonly string[] pedList = new string[] { "a_m_m_afriamer_01", "a_m_y_downtown_01", "a_m_m_farmer_01", "a_m_m_fatlatin_01", "a_f_m_fatcult_01", "a_f_m_fatwhite_01", "a_m_m_genfat_01" };
        private static Vector3 spawnpoint;
        private static Blip susBlip;
        private static Ped suspect;
        private static int counter;
        private static string malefemale;
        private static string copMaleFemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-802.29f, 172.75f, 72.84f), // Michael's House
                new(-7.00f, 515.00f, 174.64f), // Franklin's House in Vinewood
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Public_Ordinance_Easter_Egg_2_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Twitch streamer being loud as fuck");
            CalloutMessage = "A loud twitch streamer yelling and screaming.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }


        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts LOG]: Public Ordinance Easter Egg 2 callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Ordinance Easter Egg 2", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");

            if (Settings.HelpMessages)
            {
                Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);
            }
            else
            {
                Settings.HelpMessages = false;
            }

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.IsValid();

            suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@nightclub@lazlow@hi_dancefloor@"), "dancecrowd_li_11_hu_shimmy_laz", -1f, AnimationFlags.Loop);

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Yellow;
            susBlip.Alpha = 0.75f;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            if (MainPlayer.IsMale)
                copMaleFemale = "Sir";
            else
                copMaleFemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if(susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(Settings.Dialog))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Police department. What's going on here, " + malefemale + "?");
                    }
                    if(counter == 2)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Suspect~w~: Nothing. I'm just an average Twitch streamer just streaming, doing my thang. Having a good time, talking to my chat. Why? Did I do anything wrong?");
                    }
                    if(counter == 3)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I have gotten reports that you were being too loud. Your neighbors were very concerned about the noise.");
                    }
                    if(counter == 4)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Suspect~w~: My apologies, " + copMaleFemale + ". My neighbors are always karens. I ignore them while I eat and order DoorDash. My chat roasts me alot.");
                    }
                    if(counter == 5)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: You built like Cleveland Brown Jr. from the Cleveland Show.");
                    }
                    if(counter == 6)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Suspect~w~: yeah, YOU'RE BANNED BUDDY...... FROM MY PROPERTY!!!!!!");
                    }
                    if(counter == 7)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Calm down, " + malefemale + ". I'm only joking. Let me see your identification and we'll go from here alright. I just want to have some fun with you.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("Conversation Ended. CaseOh went to the fridge to get some food.");
                        suspect.Tasks.ReactAndFlee(suspect);
                    }
                }

                if (MainPlayer.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            } 

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if(susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Ordinance Easter Egg 2", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Public Ordinance Easter Egg 2 is Code 4!");

            base.End();
        }
    }
}
