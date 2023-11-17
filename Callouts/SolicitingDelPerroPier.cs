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

    [CalloutInterface("Soliciting - Del Perro Pier", CalloutProbability.Medium, "Reports of an individual asking people for money", "Code 2", "LSPD")]

    public class SolicitingDelPerroPier : Callout
    {
        private Ped suspect;
        private Blip blip;
        private Vector3 spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1651.26f, -1007.90f, 13.02f);
            heading = 214.98f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_DISTURBING_THE_PEACE_01 IN_OR_ON_POSITION UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "Reports of an individual asking people for money";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {


            return base.OnCalloutAccepted();
        }
    }
}
