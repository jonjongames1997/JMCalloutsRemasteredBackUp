using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Suspicious Person", CalloutProbability.Medium, "Reports of a suspicious person", "Code 2", "LSPD")]

    public class SuspiciousPerson : Callout
    {
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;
        private static Blip susBlip;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Suspicious_Person_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Multiple reports of a suspicous person in the area.");
            CalloutMessage = "Callers report the suspect may be armed.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Suspicious Person callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remasterd", "~w~Suspicious Person", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.IsValid();
            suspect.BlockPermanentEvents = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.CornflowerBlue;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to talk to Suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Police! Stop right there, " + malefemale + ". Better not reach for anything.");
                    }
                    if(counter == 2)
                    {
                        suspect.Face(MainPlayer);
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: Oh, Shit. What's going on here, officer?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: We have gotten reports of suspicious activity. What's going on here?");
                    }
                    if(counter == 4)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("oddjobs@assassinate@vice@hooker"), "argue_b", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: Fucking your mother, what does it look like I'm doing?");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: What's your name, " + malefemale + "?");
                    }
                    if(counter == 6)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: None of your fucking business, bitch!");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Identify yourself or you're gonna go to jail. This is your only opportunity to identify yourself.");
                    }
                    if(counter == 8)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@mp_player_intupperfinger"), "idle_a_fp", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: KISS MY MIGHTY BUNGHOLE!");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You're under arrest, " + malefemale + ", for failing to indentify yourself to a law enforcement officer.");
                    }
                    if(counter == 10)
                    {
                        suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@mp_player_intcelebrationmale@finger"), "finger", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~y~Suspect~w~: I'm not going back to that hell hole. KIFFLOM!!!!!");
                    }
                    if(counter == 11)
                    {
                        Game.DisplaySubtitle("END OF CONVERSATION!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
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
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Suspicious Person", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            base.End();

            Game.LogTrivial("[LOG]: JM Callouts Remastered - Suspicious Person is Code 4!");
        }
    }
}
