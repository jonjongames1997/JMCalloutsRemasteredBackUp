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
using JMCalloutsRemastered.Callouts;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Soliciting - Del Perro Pier", CalloutProbability.Medium, "Reports of an individual asking people for money", "Code 2", "LSPD")]

    public class SolicitingDelPerroPier : Callout
    {
        private Ped suspect;
        private Blip blip;
        private Vector3 spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1651.26f, -1007.90f, 13.02f); // Del Perro Pier
            heading = 214.98f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an individual asking people for money");
            CalloutMessage = "Reports of an individual asking people for money";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Reports of an individual soliciting callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting - Del Perro", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 2");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(spawnpoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Aqua;
            blip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if (Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~Suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~You: Excuse me, " + malefemale + ". Can you come talk to me real quick?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ What now, you motherfucker you, cops?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You: Why are you asking people for money? Panhandling is ~r~ILLEGAL~w~ in the state.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Fuck this, I'm outta here.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("Conversation Ended!");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Die, you motherfucka!");
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                    }
                }

            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Soliciting - Del Perro", "~b~You:~w~ Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Solicitng Del Perro Pier is code 4!");
        }
    }
}
