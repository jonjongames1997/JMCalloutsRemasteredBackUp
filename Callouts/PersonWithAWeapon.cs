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

    [CalloutInterface("Person With A Weapon", CalloutProbability.High, "Reports of a individual with a weapon", "Code 3", "LSPD")]

    public class PersonWithAWeapon : Callout
    {
        private string[] pedList = new string[] { "ig_amandatownley", "a_f_m_beach_01", "u_f_y_bikerchic", "a_f_m_bodybuild_01", "ig_chef", "g_m_m_chemwork_01" };
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MG", "WEAPON_COMBATMG", "WEAPON_TACTICALRIFLE", "WEAPON_CARBINERIFLE" };
        private Ped suspect;
        private Vector3 spawnPoint;
        private Vector3 searchArea;
        private Blip blip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            scenario = new Random().Next(0, 100);
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000));
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CITIZENS_REPORT_04 CRIME_BRANDISHING_WEAPON_02 IN_OR_ON_POSITION UNITS_RESPOND_CODE_03_01");
            CalloutMessage = "Reports of an armed individual";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Person With A Weapon callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Armed Individual", "~b~Dispatch: The suspect has been spotted with a firearm! Respond ~r~Code 3");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnPoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();

            searchArea = spawnPoint.Around2D(1f, 2f);
            blip = new Blip(searchArea, 80f);
            blip.Color = Color.DarkOrange;
            blip.EnableRoute(Color.DarkOrange);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            GameFiber.StartNew(delegate
            {

            });

            base.Process();
        }
    }
}
