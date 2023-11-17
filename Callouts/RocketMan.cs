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
    }
}
