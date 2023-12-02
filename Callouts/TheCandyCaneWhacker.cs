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

    [CalloutInterface("The Candy Cane Whacker", CalloutProbability.Medium, "A citizen's report of an individual with a candy cane", "Code 2", "LSPD")]

    public class TheCandyCaneWhacker : Callout
    {
        private string[] wepList = new string[] { "WEAPON_CANDYCANE" };
        private string[] pedList = new string[] { "ig_amandatownley", "a_m_m_afriamer_01", "ig_ashley", "u_m_y_babyd", "a_f_y_eastsa_03", "ig_jimmydisanto" };
        private Ped suspect;
        private Vector3 spawnpoint;
        private Vector3 searcharea;
        private Blip blip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {


            return base.OnBeforeCalloutDisplayed();
        }
    }
}
