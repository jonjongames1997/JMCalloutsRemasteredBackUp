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


    }
}
