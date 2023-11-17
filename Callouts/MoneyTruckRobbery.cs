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
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Money Truck Robbery", CalloutProbability.VeryHigh, "Reports of a robbery on a money truck", "Code 3", "LSPD")]

    public class MoneyTruckRobbery : Callout
    {
        private Vehicle stockade;
        private Ped Aggressor1;
        private Ped Aggressor2;
        private Ped Aggressor3;
        private Ped Aggressor4;
        private Vector3 spawnpoint;
        private Vector3 vehicleSpawnpoint;
        private Blip blip1;
        private Blip blip2;
        private Blip blip3;
        private Blip blip4;
        private LHandle pursuit;
        private bool isPursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            vehicleSpawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SHOTS_FIRED_AT_AN_OFFICER_03 IN_OR_ON_POSITION UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed money truck robbery";
            CalloutPosition = vehicleSpawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Money Truck Robbery callout accepted");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Money Truck Robbery", "~b~Dispatch: The suspects has been spotted with firearms! Respond ~r~Code 3");

            stockade = new Vehicle("STOCKADE", vehicleSpawnpoint);
            Aggressor1 = new Ped("g_m_m_chicold_01", spawnpoint, 0f);
            Aggressor2 = new Ped("a_c_chimp", spawnpoint, 0f);
            Aggressor3 = new Ped("ig_claypain", spawnpoint, 0f);
            Aggressor4 = new Ped("g_f_y_vagos_01", spawnpoint, 0f);

            Aggressor1.WarpIntoVehicle(stockade, -1);
            Aggressor2.WarpIntoVehicle(stockade, -2);
            Aggressor3.WarpIntoVehicle(stockade, 1);
            Aggressor4.WarpIntoVehicle(stockade, 2);

            Aggressor1.Inventory.GiveNewWeapon("weapon_gusenberg", 5000, true);
            Aggressor2.Inventory.GiveNewWeapon("weapon_tacticalrifle", 5000, true);
            Aggressor3.Inventory.GiveNewWeapon("weapon_mg", 5000, true);
            Aggressor4.Inventory.GiveNewWeapon("weapon_specialcarbine", 5000, true);

            Aggressor1.Armor = 200;
            Aggressor2.Armor = 200;
            Aggressor3.Armor = 200;
            Aggressor4.Armor = 200;

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor1);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor2);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor3);
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, Aggressor4);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            isPursuitCreated = true;



            return base.OnCalloutAccepted();
        }
    }
}
