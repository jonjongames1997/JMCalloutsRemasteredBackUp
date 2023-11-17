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

    [CalloutInterface("Deranged Lover", CalloutProbability.High, "Reports of a deranged person", "Code 3", "LSPD")]

    public class DerangedLover : Callout
    {
        private string[] pedList = new string[] { "ig_amanadatownley", "ig_ashley", "s_m_m_autoshop_01", "g_m_y_azteca_01", "g_f_y_ballas_01", "s_f_y_bartender_01", "s_m_y_baywatch_01", "a_m_y_beach_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MICROSMG", "WEAPON_SMG", "WEAPON_MINISMG", "WEAPON_MOLOTOV" };
        private Ped suspect;
        private Vector3 spawnPoint;
        private Vector3 searchArea;
        private Blip blip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;


    }
}
