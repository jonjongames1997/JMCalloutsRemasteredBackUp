using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Illegal Prostitution", CalloutProbability.High, "A female selling her body for money", "Code 2", "SAHP")]

    public class IllegalProstitution : Callout
    {

        // General Variables //
        private string[] pedList = new string[] { "IG_AMANDATOWNLEY", "CSB_ANITA", "S_F_Y_BARTENDER_01", "S_F_Y_BAYWATCH_01", "A_F_M_BEACH_01", "A_F_Y_BEACH01", "U_F_Y_BIKERCHIC", "S_F_Y_HOOKER_01", "S_F_Y_HOOKER_02", "S_F_Y_HOOKER_03", "IG_MOLLY", "A_F_Y_TOPLESS_01", "IG_TRACEYDISANTO", "MP_F_COCAINE_01" };
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private int counter;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A female civillian is selling her body for money");
            CalloutMessage = "Citizens reporting a young female selling her body for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Illegal Prostitution callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Prostitution", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 2");

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            Game.DisplayNotification("Tip: This callout works best at night time.");

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

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E ~w~to talk to Suspect. ~y~Approach with caution.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("Player: Excuse me, " + malefemale + ". Can you talk to me for a minute?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Sure. What seems to be the problem, Officer?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("Player: We've gotten reports of you selling your body for money. Can you explain why for me please? I want to remind you that you're under oath.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Well, cutie.....");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("Player: Please don't call me 'cutie', " + malefemale + ". I am happily married and I got kids.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Sorry, Officer. I need money for food. You know, with inflation being out of control. I can't afford like a box of Cap'n Crunch which is $4.95 a box. Nobody is hiring and this is my only option.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("Player: You know you can apply for Food Stamps and get benefits to help you get food. Selling your body is not the way to go. My job as an officer of the law is to prevent you from doing this. (1/2)");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("Player: Because I'm concerned for your safety. There are people out here that can rape, kidnap, or kill you. You know how many women were victims because of prostituting? (2/2)");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ I see where you're coming from.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplayNotification("Chief: Good Job, Officer. You're getting a promotion.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Player: " + malefemale + ", I'm trying to do my job and prevent you from doing this. I'm giving you the opportunity to walk home or I'll have to place you in cuffs and charge you with prostitution.");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Yes, Officer. I promise I won't be back here again.");
                    }
                    if (counter == 13)
                    {
                        Game.DisplaySubtitle("Conversation ended. Arrest the suspect.");
                        Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character); // What the suspect will do after the conversation ends //
                        Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                    }
                }
            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (Suspect && Suspect.IsDead) End();
            if (Suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Suspect)) End();
        }

        public override void End()
        {
            base.End();

            if (Suspect.Exists())
            {
                Suspect.Dismiss();
            }
            if (SuspectBlip.Exists())
            {
                SuspectBlip.Delete();
            }

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Prostitution", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");


            Game.LogTrivial("JM Callouts Remastered - Illegal Prostitution is Code 4!");
        }

    }
}
