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

    [CalloutInterface("Special Thank You from Developer", CalloutProbability.VeryHigh, "A citizen requesting an officer", "Code 2", "LSPD")]

    public class SpecialThankYouFromDeveloper : Callout
    {
        private string[] pedList = new string[] { "a_f_m_bevhills_01", "a_m_m_beach01" };
        private Ped citizen;
        private Vector3 spawnpoint;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }

    }
}
