using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using Rage.Native;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using System.Threading;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Deranged Lover", CalloutProbability.High, "Reports of a deranged person", "Code 3", "LSPD")]

    public class DerangedLover : Callout
    {
        private string[] pedList = new string[] { "ig_amanadatownley", "ig_ashley", "s_m_m_autoshop_01", "g_m_y_azteca_01", "g_f_y_ballas_01", "s_f_y_bartender_01", "s_m_y_baywatch_01", "a_m_y_beach_01", "a_m_y_beach_01", "ig_beverly" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MICROSMG", "WEAPON_SMG", "WEAPON_MINISMG", "WEAPON_MOLOTOV" };
        private Ped suspect;
        private Vector3 spawnPoint;
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
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of an deranged lover");
            CalloutMessage = "Reports of a deranged lover";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Deranged Lover callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Deranged Lover", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 3");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnPoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;
            suspect.Tasks.Wander();

            blip = suspect.AttachBlip();
            searchArea = spawnPoint.Around2D(1f, 2f);
            blip = new Blip(searchArea, 80f);
            blip.Color = Color.Orange;
            blip.EnableRoute(Color.Orange);
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
            GameFiber.StartNew((ThreadStart)(() =>
            {
                if (suspect.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 25f && !isArmed)
                {
                    suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                    isArmed = true;
                }
            }));

            base.Process();
        }

        public void BeginFighting()
        {
            GameFiber.StartNew(delegate
            {
                GameFiber.Yield();
                if (suspect && suspect.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 25f && !hasBegunAttacking)
                {
                    if (scenario > 40)
                    {
                        suspect.KeepTasks = true;
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        hasBegunAttacking = true;
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
            }, "Reports of a deranged lover [JM Callouts Remastered]");
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Deranged Lover", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Deranged Lover is code 4!");
        }
    }
}
