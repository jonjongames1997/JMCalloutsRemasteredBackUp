using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using System.Collections.Generic;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Amber Alert", CalloutProbability.High, "A citizen's report of a missing person", "Code 1", "LSPD")]

    public class AmberAlert : Callout
    {
        private string[] pedList = new string[] { "IG_LESTERCREST", "A_F_Y_RURMETH_01", "A_F_M_SALTON_01", "A_F_O_SALTON_01", "A_M_O_SOUCENT_03", "S_M_M_TRUCKER_01", "IG_PRIEST" };
        private Ped suspect;
        private Blip blip;
        private Vector3 spawnpoint;


        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
            };
            spawnpoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A missing person reported. Be On A Lookout.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("AMBERALERT");
            CalloutMessage = "A missing person reported";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }
    }
}
