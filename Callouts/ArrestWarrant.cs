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


    [CalloutInterface("Arrest Warrant", CalloutProbability.Medium, "Arrest warrant has been issued to a suspect by LSPD", "Code 2", "LSPD")]

    public class ArrestWarrant : Callout
    {
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_KNIFE", "WEAPON_MG", "WEAPON_GUSENBERG" };
        private Ped suspect;
        private Vector3 spawnPoint;
        private Ped searchArea;
        private Blip blip;
        private int storyLine = 1;
        private int calloutMessage = 0;
        private bool attack = false;
        private bool hasWeapon = false;
        private bool wasClose = false;
        private bool alreadySubtitleIntrod = false;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
