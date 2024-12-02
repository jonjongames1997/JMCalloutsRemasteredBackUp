using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Domestic Disturbance - Harmony", CalloutProbability.Medium, "A couple of people arguing", "Code 3", "BCSO")]

    public class DomesticDisturbanceHarmony : Callout
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
            spawnPoint = new(975.77f, 2649.64f, 40.06f);
            heading = 286.12f;
            suspectSpawnpoint = new(982.19f, 2648.61f, 40.16f);
            suspectHeading = 73.00f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallouts_DomesticDisturbance_Harmony_Callout_Audio_1", spawnPoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A neighbor's reporting a loud argument next door.");
            CalloutMessage = "Reports of a domestic disturbance";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Domestic Disturbance - Harmony callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Grapeseed", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_3_Audio");

            victim = new Ped(spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;
            victim.IsValid();

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


            if (MainPlayer.DistanceTo(victim) <= 10f)
            {
                Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~ to interact with the ~r~Victim~w~.", false);

                if (Game.IsKeyDown(Settings.Dialog))
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
                        Game.DisplaySubtitle("~o~Victim~w~: Hello, Officer. I am doing fine.... well, kinda. We are having an argument about the loud music that my buddy was playing. I told them to turn it down. They refuse and I told them to leave my house now.");
                    }
                    if (counter == 3)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I see. Anything else I should know about?");
                    }
                    if (counter == 4)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: No, officer.");
                    }
                    if(counter == 5)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Did you threatened anybody?");
                    }
                    if(counter == 6)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: No. Why would ask such a thing?");
                    }
                    if(counter == 7)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I have gotten a tip that you and your buddy might have a weapon on your person.");
                    }
                    if(counter == 8)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: The fuck? Who called and said that?");
                    }
                    if(counter == 9)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I don't know, " + malefemale + ". All I know is a couple of people arguing and threatening each other with weapons.");
                    }
                    if(counter == 10)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: I bet it was my fucking Karen ass neighbor again. I told that motherfucker to stop eavesdropping on my business.");
                    }
                    if(counter == 11)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", calm down. We'll take care of it. I need you to calm down.");
                    }
                    if(counter == 12)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~o~Victim~w~: I'm done talking to you.");
                    }
                    if(counter == 13)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", 1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("Conversation ended!");
                        suspect.Tasks.FightAgainst(victim);
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                        victim.Tasks.FightAgainst(suspect);
                        victim.Inventory.GiveNewWeapon("WEAPON_COMBATMG", 500, true);
                        victim.Armor = 500;
                        suspect.Armor = 500;
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

        }

        public override void End()
        {
            if (victim) victim.Dismiss();
            if (suspect) suspect.Dismiss();
            if (vicBlip) vicBlip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Domestic Disturbance - Harmony", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Domestic Disturbance - Grapeseed is Code 4!");
        }

    }
}
