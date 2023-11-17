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



            return base.OnCalloutAccepted();
        }
    }
}
