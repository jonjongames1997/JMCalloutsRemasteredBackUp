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

    [CalloutInterface("Shots Fired", CalloutProbability.High, "A citizen's report of shots fired", "Code 3", "LSPD")]

    public class ShotsFired : Callout
    {
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_PISTOL_MK2", "WEAPON_DOUBLEACTION", "WEAPON_CARBINERIFLE" };
        private Ped suspect1;
        private Ped suspect2;
        private Ped suspect3;
        private Ped suspect4;
        private Vector3 spawnPoint;
        private Vector3 searchArea;
        private Blip blip;
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
