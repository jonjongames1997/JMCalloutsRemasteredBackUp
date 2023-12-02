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
        private Blip suspectBlip;
        private LHandle pursuit;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            scenario = new Random().Next(0, 100);
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Citizen's reporting an individual with a candy cane");
            CalloutMessage = "An individual threating to whack another person with a candy cane";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: The Candy Cane Whacker Callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Candy Cane Whacker", "~b~Dispatch~w~: The suspect has been spotted with a Candy Cane! ~r~Respond Code 2~w~!");

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.Wander();
            suspect.AttachBlip();

            searcharea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(searcharea, 80f);
            blip.Color = Color.OrangeRed;
            blip.EnableRoute(Color.OrangeRed);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (blip) blip.Delete();
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {


            base.Process();
        }
    }
}
