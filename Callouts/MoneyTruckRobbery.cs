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
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Money Truck Robbery", CalloutProbability.VeryHigh, "Reports of a robbery on a money truck", "Code 3", "LSPD")]

    public class MoneyTruckRobbery : Callout
    {
        private Vehicle stockade;
        private Ped Aggressor1;
        private Ped Aggressor2;
        private Ped Aggressor3;
        private Ped Aggressor4;
        private Vector3 spawnpoint;
        private Vector3 vehicleSpawnpoint;
        private Blip blip1;
        private Blip blip2;
        private Blip blip3;
        private Blip blip4;
        private LHandle pursuit;
        private bool isPursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            vehicleSpawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            CalloutMessage = "Reports of an armed money truck robbery";
            CalloutPosition = vehicleSpawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Money Truck Robbery callout accepted");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Money Truck Robbery", "~b~Dispatch: The suspects has been spotted with firearms! Respond ~r~Code 3");

            stockade = new Vehicle("STOCKADE", vehicleSpawnpoint);
            Aggressor1 = new Ped("g_m_m_chicold_01", spawnpoint, 0f);
            Aggressor2 = new Ped("a_c_chimp", spawnpoint, 0f);
            Aggressor3 = new Ped("ig_claypain", spawnpoint, 0f);
            Aggressor4 = new Ped("g_f_y_vagos_01", spawnpoint, 0f);

            Aggressor1.WarpIntoVehicle(stockade, -1);
            Aggressor2.WarpIntoVehicle(stockade, -2);
            Aggressor3.WarpIntoVehicle(stockade, 1);
            Aggressor4.WarpIntoVehicle(stockade, 2);

            Aggressor1.Inventory.GiveNewWeapon("weapon_gusenberg", 5000, true);
            Aggressor2.Inventory.GiveNewWeapon("weapon_tacticalrifle", 5000, true);
            Aggressor3.Inventory.GiveNewWeapon("weapon_mg", 5000, true);
            Aggressor4.Inventory.GiveNewWeapon("weapon_specialcarbine", 5000, true);

            Aggressor1.Armor = 500;
            Aggressor2.Armor = 500;
            Aggressor3.Armor = 500;
            Aggressor4.Armor = 500;

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor1);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor2);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor3);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor4);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            isPursuitCreated = true;

            blip1 = Aggressor1.AttachBlip();
            blip1 = Aggressor2.AttachBlip();
            blip1 = Aggressor3.AttachBlip();
            blip1 = Aggressor4.AttachBlip();

            NativeFunction.CallByName<uint>("TASK_COMBAT_PED", Aggressor1, Game.LocalPlayer.Character, 0, 1);
            NativeFunction.CallByName<uint>("TASK_COMBAT_PED", Aggressor2, Game.LocalPlayer.Character, 0, 1);
            NativeFunction.CallByName<uint>("TASK_COMBAT_PED", Aggressor3, Game.LocalPlayer.Character, 0, 1);
            NativeFunction.CallByName<uint>("TASK_COMBAT_PED", Aggressor4, Game.LocalPlayer.Character, 0, 1);

            if (Settings.ActiveAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(vehicleSpawnpoint, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.LocalUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(vehicleSpawnpoint, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.AirUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(vehicleSpawnpoint, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.StateUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(vehicleSpawnpoint, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.StateUnit);
            }
            else { Settings.ActiveAIBackup = false; }

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (Aggressor1) Aggressor1.Delete();
            if (Aggressor2) Aggressor2.Delete();
            if (Aggressor3) Aggressor3.Delete();
            if (Aggressor4) Aggressor4.Delete();
            if (stockade) stockade.Delete();
            if (blip1) blip1.Delete();
            if (blip2) blip2.Delete();
            if (blip3) blip3.Delete();
            if (blip4) blip4.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            GameFiber.StartNew(delegate
            {
                if(Game.LocalPlayer.Character.DistanceTo(stockade) < 15f)
                {
                    Aggressor1.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    Aggressor2.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    Aggressor3.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    Aggressor4.Tasks.FightAgainst(Game.LocalPlayer.Character);
                }
                if(Aggressor1.IsDead || LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor1))
                {
                    if (blip1) blip1.Delete();
                }
                if(Aggressor2.IsDead || LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor2))
                {
                    if (blip2) blip2.Delete();
                }
                if(Aggressor3.IsDead || LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor3))
                {
                    if (blip3) blip3.Delete();
                }
                if(Aggressor4.IsDead || LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor4))
                {
                    if (blip4) blip4.Delete();
                }
                if (Game.LocalPlayer.Character.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
                if (Aggressor1 && Aggressor1.IsDead && Aggressor2 && Aggressor2.IsDead && Aggressor3 && Aggressor3.IsDead && Aggressor4 && Aggressor4.IsDead) End();
                if (Aggressor1 && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor1) && Aggressor2 && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor2) && Aggressor3 && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor3) && Aggressor4 && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Aggressor4)) End();
            }, "Money Truck Robbery [JM Callouts Remastered]");

            base.Process();
        }

        public override void End()
        {
            if (blip1) blip1.Delete();
            if (blip2) blip2.Delete();
            if (blip3) blip3.Delete();
            if (blip4) blip4.Delete();
            if (Aggressor1) Aggressor1.Dismiss();
            if (Aggressor2) Aggressor2.Dismiss();
            if (Aggressor3) Aggressor3.Dismiss();
            if (Aggressor4) Aggressor4.Dismiss();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Money Truck Robbery", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("JM Callouts Remastered: Money Truck Robbery is code 4");
        }
    }
}