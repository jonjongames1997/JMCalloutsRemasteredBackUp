using System;
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

    [CalloutInterface("RocketMan", CalloutProbability.VeryHigh, "Reports of an individual brandishing a explosive weapon", "Code 3", "LSPD")]

    public class RocketMan : Callout
    {
        private string[] pedList = new string[] { "s_m_m_fiboffice_01", "a_f_m_fatwhite_01", "s_f_m_fembarber", "g_m_y_famfor_01", "g_f_y_families_01", "s_f_y_bartender_01", "ig_molly" };
        private string[] wepList = new string[] { "WEAPON_RPG" };
        private Ped suspect;
        private Vector3 spawnpoint;
        private Vector3 searchArea;
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
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_BRANDISHING_WEAPON_02 IN_OR_ON_POSITION UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed individual with an explosive weapon";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Reports of an armed individual with an explosive weapon accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Clown", "~b~Dispatch: The suspect has been spotted with a explosive weapon! Respond ~r~Code 3");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;
            suspect.Tasks.Wander();

            searchArea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(searchArea, 80f);
            blip.Color = Color.Pink;
            blip.EnableRoute(Color.Pink);
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
                    if(scenario > 40)
                    {
                        suspect.KeepTasks = true;
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ROCKETMAN");
                        suspect.Armor = 500;
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

                if (Game.LocalPlayer.Character.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
                if (suspect && suspect.IsDead) End();
                if (suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)) End();
            }, "Reports of an armed individual with an explosive weapon [JM Callouts Remastered]");

            base.Process();
        }

        public override void End()
        {


            base.End();
        }
    }
}
