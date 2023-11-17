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

    [CalloutInterface("Gang Rivalry Shootout", CalloutProbability.High, "Reports of a gang shootout", "Code 99", "SWAT")]

    public class GangRivalryShootout : Callout
    {
        private Ped Aggressor1;
        private Ped Aggressor2;
        private Ped Aggressor3;
        private Ped Aggressor4;
        private Ped Aggressor5;
        private Ped Aggressor6;
        private Blip blip1;
        private Blip blip2;
        private Blip blip3;
        private Blip blip4;
        private Blip blip5;
        private Blip blip6;
        private bool hasBegunAttacking = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(-24.46961f, -1457.463f, 30.6445f), // Franklin's Hood
                new Vector3(39.15001f, -1739.961f, 29.30341f), // Mega Mall
                new Vector3(361.2437f, -1967.548f, 24.47634f), // Vagos Territory
                new Vector3(126.7119f, -1472.859f, 29.14161f), // Lucky Plucker in Strawberry
            };


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
