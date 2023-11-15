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
    public class ClownWithADeadlyWeapon : Callout
    {
        private string[] pedList = new string[] { "s_m_y_clown_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_COMBATPISTOL", "WEAPON_SMG", "WEAPON_ASSAULTSHOTGUN" };
        private Ped suspect;
        private Vector3 spawnPoint;
        private Vector3 searchArea;
        private Blip blip;
        private LHandle Pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
