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

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Unauthorized Access - Richard's Majestic", CalloutProbability.Medium, "Reports of a trespasser", "Code 2", "LSPD")]

    public class UnauthorizedAccessMovieStudio : Callout
    {
        private Ped suspect;
        private Blip susBlip;
        private Vector3 spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
