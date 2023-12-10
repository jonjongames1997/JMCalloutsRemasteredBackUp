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
using JMCalloutsRemastered.Utilities;
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
            if (!Settings.ATMRobbery)
            {
                Game.Console.Print("[JM Callouts Remastered Log]: User has disabled ATMRobbery, returning false");
                Game.LogTrivial("[JM Callouts Remastered Log]: To enable this callout, go to GTA V > plugins > LSPDFR > JMCalloutsRemastered.ini to enable from false to true.");
                return false;
            }

            Random rindum = new Random();
            List<Vector3> list = new List<Vector3>();
            Tuple<Vector3, float>[] SpawnLocationList =
            {
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
                Tuple.Create(new Vector3()),
            };
            for (int i = 0; i < SpawnLocationList.Length; i++)
            {
                list.Add(SpawnLocationList[i].Item1);
            }
            int num = CallHandler.nearestLocationIndex(list);

            spawnpoint = SpawnLocationList[num].Item1;

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, SpawnLocationList[num].Item2);

            mainScenario = new Random().Next(0, 3);

            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 20f);
            AddMinimumDistanceCheck(100f, spawnpoint);
            AddMaximumDistanceCheck(1200f, spawnpoint);

            CalloutMessage = "ATM Robbery";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
