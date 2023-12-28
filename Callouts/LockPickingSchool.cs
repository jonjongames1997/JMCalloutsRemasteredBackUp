using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CalloutInterfaceAPI;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lock Picking - ULS", CalloutProbability.Medium, "A school security guard reporting a student locker break in", "Code 2", "LSPD")]

    public class LockPickingSchool : Callout
    {

        private Ped suspect;
        private Ped victim;
        private Vector3 spawnpoint;
        private Vector3 susSpawnPoint;
        private Blip suspectBlip;
        private Blip victimBlip;
        private string malefemale;
        private int counter;
        private float heading;
        private float susHeading;


        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }

    }
}
