﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using System.Windows.Forms;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Monkey With A Weapon", CalloutProbability.Medium, "Reports of an armed Monkey", "Code 3", "LSPD")]

    public class MonkeyWithAWeapon : Callout
    {
        private string[] pedList = new string[] { "A_C_CHIMP" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_ASSAULTRIFLE", "WEAPON_SPECIALCARBINE", "WEAPON_CARBINERIFLE" };
        private Ped suspect;
        private Vector3 spawnpoint;
        private Vector3 searcharea;
        private Blip blip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            scenario = new Random().Next(0, 100);
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_BRANDISHING_WEAPON_02 UNITS_RESPOND_CODE_03_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of an armed monkey with a firearm.");
            CalloutMessage = "Monkey with a firearm";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Person With A Weapon callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Monkey", "~b~Dispatch: The suspect has been spotted with a firearm! Respond ~r~Code 3");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();

            searcharea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(spawnpoint, 80f);
            blip.Color = Color.Black;
            blip.EnableRoute(Color.Black);
            blip.Alpha = 0.5f;

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
            GameFiber.StartNew(delegate
            {
                if(suspect.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 25f && !isArmed)
                {
                    suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                    isArmed = true;
                }
                if(suspect && suspect.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 25f && !hasBegunAttacking)
                {
                    if(scenario > 40f)
                    {
                        suspect.KeepTasks = true;
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        hasBegunAttacking = true;
                        GameFiber.Wait(2000);
                    }
                    else
                    {
                        if (!hasPursuitBegun)
                        {
                            if (blip) blip.Delete();
                            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
                            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                            hasPursuitBegun = true;
                        }
                    }
                }

                if (Settings.ActiveAIBackup)
                {
                    LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.LocalUnit);
                    LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.PrisonerTransport);
                    LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.StateUnit);
                    LSPD_First_Response.Mod.API.Functions.RequestBackup(spawnpoint, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.AirUnit);
                }
                else { Settings.ActiveAIBackup = false; }

                if (Game.LocalPlayer.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
                if (suspect && suspect.IsDead) End();
                if (suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)) End();
            }, "JM Callouts Remastered: Monkey With A Weapon");

            base.Process();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Monkey", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Monkey With A Weapon is Code 4!");
        }
    }
}