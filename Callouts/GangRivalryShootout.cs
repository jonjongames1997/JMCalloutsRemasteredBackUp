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
        private Vector3 spawnPoint;
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
            spawnPoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 70f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 CRIME_GUNFIRE_02 IN_OR_ON_POSITION UNITS_RESPOND_CODE_99_03");
            CalloutMessage = "Reports of a gang shootout";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Gang Rivalry Shootout callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Gang Shootout", "~b~Dispatch: The Gangs has been spotted! Respond ~r~Code 99");
            Game.DisplayNotification("Chief: LETHAL FORCE IS AUTHORIZED! SHOOT TO KILL!");

            Aggressor1 = new Ped("ig_ashley", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 20f), 0f);
            Aggressor2 = new Ped("cs_johnnyklebitz", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 30f), 0f);
            Aggressor3 = new Ped("g_f_y_lost_01", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 22f), 0f);
            Aggressor4 = new Ped("a_f_y_hippie_01", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 24f), 0f);
            Aggressor5 = new Ped("a_m_y_hippy_01", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 26f), 0f);
            Aggressor6 = new Ped("a_f_y_hipster_01", Stuff.Vector3Extension.ExtensionAround(spawnPoint, 28f), 0f);

            Aggressor1.Inventory.GiveNewWeapon("WEAPON_MICROSMG", 5000, true);
            Aggressor2.Inventory.GiveNewWeapon("WEAPON_ASSAULTRIFLE", 5000, true);
            Aggressor3.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 5000, true);
            Aggressor4.Inventory.GiveNewWeapon("WEAPON_TACTICALRIFLE", 5000, true);
            Aggressor5.Inventory.GiveNewWeapon("WEAPON_MG", 5000, true);
            Aggressor6.Inventory.GiveNewWeapon("WEAPON_COMBATMG", 5000, true);

            blip1 = Aggressor1.AttachBlip();
            blip2 = Aggressor2.AttachBlip();
            blip3 = Aggressor3.AttachBlip();
            blip4 = Aggressor4.AttachBlip();
            blip5 = Aggressor5.AttachBlip();
            blip6 = Aggressor6.AttachBlip();



            return base.OnCalloutAccepted();
        }
    }
}
