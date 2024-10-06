using CalloutInterfaceAPI;
using System.Windows.Forms;


namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Lost Individual", CalloutProbability.Medium, "A citizen's report of a lost person", "Code 2", "LSPD")]

    public class LostIndividual : Callout
    {

        // General Variables //
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
            spawnPoint = new(-663.64f, -227.5f, 37.39f);
            heading = 70.58f;
            suspectSpawnpoint = new(-623.54f, -230.25f, 38.06f); // Second Suspect will spawn at this location 
            suspectHeading = 131.09f;
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Lost_Individual_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Michael DeSanta reported his wife missing. Locate and help her get home safely, Officer.");
            CalloutMessage = "Reports of a missing person";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Lost Individual callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Lost Individual", "~b~Dispatch: ~w~Victim has been spotted. Respond ~r~Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Lost_Individual_Audo_2");

            // Amanda is a decoy in this situation //
            victim = new Ped("IG_AMANDATOWNLEY", spawnPoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            victim.Tasks.PlayAnimation(new AnimationDictionary("anim_heist@arcade@fortune@male@"), "reaction_confused", -1f, AnimationFlags.Loop);

            suspect.Tasks.PlayAnimation(new AnimationDictionary("weapons@first_person@aim_rng@p_m_zero@shotgun@assault_shotgun@fidgets@c"), "fidget_med_loop", -1f, AnimationFlags.UpperBodyOnly);

            // Michael Spawns and equips a gun //
            suspect = new Ped("PLAYER_ZERO", suspectSpawnpoint, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            suspect.Tasks.StandStill(500);

            vicBlip = victim.AttachBlip();
            vicBlip.Color = System.Drawing.Color.Pink;
            vicBlip.IsRouteEnabled = true;

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.DarkRed;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (victim) victim.Delete();
            if (vicBlip) vicBlip.Delete();
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (MainPlayer.DistanceTo(victim) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~victim~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        victim.Tasks.PlayAnimation(new AnimationDictionary("special_ped@mani@trevor_1@trevor_1d"), "convo_trevor_whahellholaamigo_3", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Can you tell me what happened?");
                    }
                    if (counter == 2)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Victim~w~: Well, I was doing my grocery shopping for my family, somehow I managed to hit my head on a pole. I was knocked out for a few minutes. I don't remember the rest");
                    }
                    if (counter == 3)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("special_ped@mani@trevor_1@trevor_1d"), "convo_trevor_whahellholaamigo_3", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~Player~w~: Okay, " + malefemale + ". Do you need any medical attention?");
                    }
                    if (counter == 4)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@hangout@ped_female@stand_withdrink@01a@idles_convo"), "idle_a", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Victim~w~: No, I'll be fine. I'll just take some Tylenol and get some rest. I'll be okay.");
                    }
                    if (counter == 5)
                    {
                        victim.Tasks.PlayAnimation(new AnimationDictionary("special_ped@mani@trevor_1@trevor_1d"), "convo_trevor_whahellholaamigo_3", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("Conversation Ended! Call her an Uber.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: You motherfucker, you! DIE!!!!!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_GUSENBERG", 500, true);
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
            if (victim) victim.Dismiss();
            if (vicBlip) vicBlip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Lost Individual", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Lost Individual is CODE 4!");
        }

    }
}
