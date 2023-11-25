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


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
