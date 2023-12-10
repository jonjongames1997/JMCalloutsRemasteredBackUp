using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;

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


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
