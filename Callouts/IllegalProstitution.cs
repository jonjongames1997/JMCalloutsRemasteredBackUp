using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Illegal Prostitution", CalloutProbability.Medium, "A female selling her body for money", "Code 2", "SAHP")]

    public class IllegalProstitution : Callout
    {

        // General Variables //
        private static readonly string[] pedList = new string[] { "IG_AMANDATOWNLEY", "CSB_ANITA", "S_F_Y_BARTENDER_01", "S_F_Y_BAYWATCH_01", "A_F_M_BEACH_01", "A_F_Y_BEACH01", "U_F_Y_BIKERCHIC", "S_F_Y_HOOKER_01", "S_F_Y_HOOKER_02", "S_F_Y_HOOKER_03", "IG_MOLLY", "A_F_Y_TOPLESS_01", "IG_TRACEYDISANTO", "MP_F_COCAINE_01", "a_f_m_bodybuild_01", "cs_debra", "a_f_y_eastsa_03", "a_f_y_eastsa_02", "g_f_y_families_01" };
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Illegal_Prostitution_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A female civillian is selling her body for money");
            CalloutMessage = "Citizens reporting a young female selling her body for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Illegal Prostitution callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Prostitution", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsValid();

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("switch@michael@prostitute"), "base_hooker", -1f, AnimationFlags.Loop);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.DeepPink;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (Suspect) Suspect.Delete();
            if (SuspectBlip) SuspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E ~w~to talk to Suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Can you talk to me for a minute?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Sure. What seems to be the problem, Officer?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: We've gotten reports of you selling your body for money. Can you explain why for me please? I want to remind you that you're under oath.");
                    }
                    if (counter == 4)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("oddjobs@taxi@argument"), "hooker_loop_a_hooker_b", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: Well, cutie.....");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Please don't call me 'cutie', " + malefemale + ". I am happily married and I got kids.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Sorry, Officer. I need money for food. You know, with inflation being out of control. I can't afford like a box of Cap'n Crunch which is $4.95 a box. Nobody is hiring and this is my only option.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You know you can apply for Food Stamps and get benefits to help you get food. Selling your body is not the way to go. My job as an officer of the law is to prevent you from doing this. (1/2)");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Because I'm concerned for your safety. There are people out here that can rape, kidnap, or kill you. You know how many women were victims because of prostituting? (2/2)");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I see where you're coming from.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplayNotification("Chief: Good Job, Officer. You're getting a promotion.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", I'm trying to do my job and prevent you from doing this. I'm giving you the opportunity to walk home or I'll have to place you in cuffs and charge you with prostitution.");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Yes, Officer. I promise I won't be back here again.");
                    }
                    if (counter == 13)
                    {
                        Game.DisplaySubtitle("Conversation ended. Deal with the situation you may see fit.");
                        Suspect.Tasks.StandStill(500);
                    }
                }
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Prostitution", "~b~You~w~: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Illegal Prostitution is Code 4!");
        }

    }
}
