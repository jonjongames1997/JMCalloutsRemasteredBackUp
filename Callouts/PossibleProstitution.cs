using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Possible Prostitution", CalloutProbability.High, "A female possibly selling her body for money", "Code 2", "LCSO")]

    public class PossibleProstitution : Callout
    {

        // General Variables //
        private string[] pedList = new string[] { "S_F_Y_HOOKER_01", "S_F_Y_HOOKER_02", "S_F_Y_HOOKER_03" };
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private int counter;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(-535.76f, -849.20f, 29.44f), // Near Lucky Plucker in Little Seoul // 
                new Vector3(-415.10f, 1172.77f, 325.81f), // Galileo Observatory
                new Vector3(-1137.11f, 2664.79f, 18.07f), // Route 68 near Fort Zancudo
                new Vector3(-2326.10f, 374.99f, 174.61f), // University of Los Santos
                new Vector3(-1618.14f, 178.25f, 60.26f), // Kortz Center
            };
            Spawnpoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen reported a young female selling her body for money. Talk to her and see if the claim is true. Approach with caution.");
            CalloutMessage = "Citizens reporting a young female possibly selling her body for money.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Possible Prostitution callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Possible Prostitution", "~b~Dispatch:~w~ Suspect has been spotted. Respond ~r~Code 2.");

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            Game.DisplayNotification("Tip: This callout works better at night time when other prostitutes are on the streets.");

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Cyan;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~You~w~: Good evening " + malefemale + ", Can I ask you some questions?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Sure. What seems to be the problem, Officer?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I have gotten reports of you possibly selling your body for money. Is it true?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Yes. I need the money to pay off my college debt.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: You know that's illegal in the state of San Andreas. Which I can arrest you for that.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ What you gonna do? I'm not going anywhere!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", You can get a job anywhere here in the city. We can help you get a job through a vocational school.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplayNotification("Chief: What the fuck is going on out there, Deputy?!");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Come on, Officer. First time is free.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", You're under arrest for prostitution.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Fuck you then, pigs.");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("Conversation ended.");
                        Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
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

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Possible Prostitution", "~b~You:~w~ Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Possible Prostitution is Code 4!");
        }
    }
}
