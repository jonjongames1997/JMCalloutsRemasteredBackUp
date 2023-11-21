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
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(-73.264f, -28.95624f, 65.75121f),
                new Vector3(-1123.261f, 483.8483f, 82.16084f),
                new Vector3(967.7412f, -546.0015f, 59.36506f),
                new Vector3(-109.5984f, -10.19665f, 70.51959f),
                new Vector3(-10.93565f, -1434.329f, 31.11683f),
                new Vector3(-1.83876f, 523.2645f, 174.6274f),
                new Vector3(-801.5516f, 178.7447f, 72.83471f),
                new Vector3(-812.7239f, 178.7438f, 76.74079f),
                new Vector3(3.542758f, 526.8926f, 170.6218f),
                new Vector3(-1155.698f, -1519.297f, 10.63272f),
            };

            spawnPoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 30f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "The LSPD has issued a warrant for arrest on a suspect that is connected to a recent crime");
            switch(new Random().Next(1, 3))
            {
                case 1:
                    attack = true;
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            switch(new Random().Next(1, 3))
            {

            }

            return base.OnBeforeCalloutDisplayed();
        }
    }
}
