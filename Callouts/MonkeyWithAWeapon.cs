using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using System.Threading;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Monkey With A Weapon", CalloutProbability.High, "A citizen's report of a chimpanzee with a weapon", "Code 3", "LSPD")]

    public class MonkeyWithAWeapon : Callout
    {
        private string[] pedList = new string[] { "A_C_CHIMP", "IG_ORLEANS", "A_C_RHESUS" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MG", "WEAPON_COMBATMG", "WEAPON_TACTICALRIFLE", "WEAPON_CARBINERIFLE" };
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
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen's report of a chimpanzee with a weapon. Respond Code 3.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_BRANDISHING_WEAPON_02 UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed chimpanzee";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Monkey With A Weapon callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Monkey With A Weapon", "~b~Dispatch~w~: The primate has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();
            blip = suspect.AttachBlip();

            searcharea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(searcharea, 80f);
            blip.Color = Color.DarkOrange;
            blip.EnableRoute(Color.DarkOrange);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process() => GameFiber.StartNew((ThreadStart)(() =>
        {
            if ((double)((Entity)this.suspect).DistanceTo(((Entity)Game.LocalPlayer.Character).GetOffsetPosition(Vector3.RelativeFront)) < 40.0 && (this.blip))
                this.blip.Delete();
            if ((double)((Entity)this.suspect).DistanceTo(((Entity)Game.LocalPlayer.Character).GetOffsetPosition(Vector3.RelativeFront)) < 70.0 && !this.isArmed)
            {
                this.suspect.Inventory.GiveNewWeapon((this.wepList[new Random().Next(this.wepList.Length)]), (short)500, true);
                this.isArmed = true;
            }

            base.Process();
        }));

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
            }, "Reports of a Monkey With A Weapon [JM Callouts Remastered]");
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Monkey With A Weapon", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered Log]: Monkey With A Weapon is Code 4!");

        }
    }
}
