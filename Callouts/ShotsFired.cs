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
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(60.597f, 18.319f, 68.829f), // Alta Parking Lot near FIB Building //
                new Vector3(226.897f, -35.595f, 69.286f), // Near GoPostal and Vinewood Ammunation
            };
            spawnPoint = LocationChooser.chooseNearestLocation(list);
            scenario = new Random().Next(0, 100);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A report of shots fired by civilian");
            CalloutMessage = "Shots fired reported";
            CalloutPosition = spawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SHOTS_FIRED_AT_AN_OFFICER_03");

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
