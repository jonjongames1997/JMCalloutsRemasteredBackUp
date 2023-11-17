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

    [CalloutInterface("Deranged Lover", CalloutProbability.High, "Reports of a deranged person", "Code 3", "LSPD")]

    public class DerangedLover : Callout
    {
        private string[] pedList = new string[] { "ig_amanadatownley", "ig_ashley", "s_m_m_autoshop_01", "g_m_y_azteca_01", "g_f_y_ballas_01", "s_f_y_bartender_01", "s_m_y_baywatch_01", "a_m_y_beach_01" };
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
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000));
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_DISTURBING_THE_PEACE_02 IN_OR_ON_POSITION UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of a deranged lover";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Deranged Lover callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Deranged Lover", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 3");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnPoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.BlockPermanentEvents = true;
            suspect.IsPersistent = true;
            suspect.Tasks.Wander();

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

            });

            base.Process();
        }
    }
}
