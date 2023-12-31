using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.Callouts;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using Rage;
using Rage.Native;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On School Property", CalloutProbability.Medium, "An individual trespassing on school property", "Code 2", "LSPD")]

    public class TrespassingOnSchoolGrounds : Callout
    {
        private string[] pedList = new string[] { "PLAYER_TWO", "PLAYER_ZERO", "PLAYER_ONE", "IG_AMANDATOWNLEY", "S_F_Y_BARTENDER_01", "IG_BEVERLY", "U_F_Y_BIKERCHIC", "G_M_M_CHEMWORK_01", "MP_F_FREEMODE_01", "HC_HACKER", "A_F_Y_RURMETH_01", "MP_F_COCAINE_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_STUNGUN", "WEAPON_DAGGER", "WEAPON_KNIFE", "WEAPON_WRENCH", "WEAPON_RAYPISTOL", "WEAPON_AUTOSHOTGUN", "WEAPON_ASSAULTRIFLE", "WEAPON_CARBINERIFLE" };
        private Vector3 spawnpoint;
        private Vector3 searchArea;
        private Blip blip;
        private Ped suspect;
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
