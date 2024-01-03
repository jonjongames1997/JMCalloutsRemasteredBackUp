using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.Callouts;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using Rage;
using Rage.Native;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On School Property", CalloutProbability.Medium, "An individual trespassing on school property", "Code 2", "LSPD")]

    public class TrespassingOnSchoolGrounds : Callout
    {
        private string[] pedList = new string[] { "PLAYER_TWO", "PLAYER_ZERO", "PLAYER_ONE", "IG_AMANDATOWNLEY", "S_F_Y_BARTENDER_01", "IG_BEVERLY", "U_F_Y_BIKERCHIC", "G_M_M_CHEMWORK_01", "MP_F_FREEMODE_01", "HC_HACKER", "A_F_Y_RURMETH_01", "MP_F_COCAINE_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_STUNGUN", "WEAPON_DAGGER", "WEAPON_KNIFE", "WEAPON_WRENCH", "WEAPON_RAYPISTOL", "WEAPON_AUTOSHOTGUN", "WEAPON_ASSAULTRIFLE", "WEAPON_CARBINERIFLE" };
        private Vector3 spawnpoint;
        private Blip blip;
        private Ped suspect;
        private int counter;
        private float heading;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1602.71f, 206.43f, 59.28f);
            heading = 100.56f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "An unknown individual trespassing on school property");
            CalloutMessage = "Reports of an unknown person trespassing";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On School Grounds callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On School Property", "~b~Dispatch~w~: Suspect has been spotted! Respond ~r~Code 2~w~.");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Orange;
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
            if(Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~Suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~You~w~: Police Departmant. Stop where I can see you " + malefemale + ". I want to talk to you.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What now?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I have gotten reports of you trespassing on the school grounds without a vistor's pass. Why you refusing a vistor's pass from the school staff?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I am a student here at ULSA. Why you come up with that marlarkey?");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Are you sure? I will review the CCTV Footage, you better tell me the truth and be honest.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fuck me, they know I am not a student here. Take your last breath of fresh air, Motherfucker!");
                        suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    }
                }
            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (suspect && suspect.IsDead) End();
            if (suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)) End();

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On School Property", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing on School Property is code 4!");
        }
    }
}
