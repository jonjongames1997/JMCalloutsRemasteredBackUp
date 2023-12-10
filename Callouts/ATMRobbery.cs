using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using CAPI = CalloutInterfaceAPI;
using Rage;
using Rage.Native;
using System.Drawing;
using LSPD_First_Response.Engine;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CAPI.CalloutInterface("ATM Robbery", CalloutProbability.VeryHigh, "A citizen reported that the suspect might have a weapon", "Code 3", "LSPD")]
    public class ATMRobbery : Callout
    {
        private string[] wepList = new string[] { "WEAPON_CROWBAR", "WEAPON_KNIFE", "WEAPON_CANDYCANE", "WEAPON_PISTOL", "WEAPON_TACTICALRIFLE" };
        private string[] pedList = new string[] { "A_F_Y_HIPPIE_01", "HC_DRIVER", "HC_GUNMAN", "IG_ORTEGA", "IG_OLD_MAN2", "IG_MOLLY", "S_F_Y_MIGRANT_01", "S_F_M_MAID_01", "IG_TENNISCOACH", "IG_TONYA", "MP_F_COCAINE_01" };
        private Vector3 spawnpoint;
        private Vector3 searcharea;
        public Ped suspect;
        private Ped player => Game.LocalPlayer.Character;
        public Blip suspectBlip;
        public Blip searchAreaBlip;

        private bool hasBegunAttacking = false;
        private bool hasPursuitBegun = false;
        private bool hasPursuitBeenCreated = false;
        private bool calloutRunning = false;
        private bool suspectBlipCreated = false;
        private bool hasSuspectSurrendered = false;

        private LHandle pursuit;

        private int mainScenario;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
