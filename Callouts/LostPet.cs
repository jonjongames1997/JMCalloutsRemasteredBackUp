using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;
using JMCalloutsRemastered.Stuff;
using CalloutInterfaceAPI;
using System.Drawing;
using Rage;
using System.Windows.Forms;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lost Pet - BETA", CalloutProbability.Medium, "A citizen's report of a lost animal", "Code 2", "LSPD")]

    public class LostPet : Callout
    {
        private string[] pedList = new string[] { "A_C_WESTY", "A_C_SHEPERD", "A_C_ROTTWEILER", "A_C_RETRIEVER", "A_C_RABBIT_01", "A_C_PUG", "A_C_POODLE", "A_C_HUSKY", "A_C_CHOP", "A_C_CAT_01" };
        private Ped lostPet;
        private Blip blip;
        private Vector3 spawnpoint;
        private Vector3 searchArea;
        private string malefemale;
        private int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of a lost pet");
            CalloutMessage = "Lost animal";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Lost Pet callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Lost Pet", "~b~Dispatch~w~: The animal has been spotted! Respond ~r~Code 2~w~.");

            lostPet = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            lostPet.IsPersistent = true;
            lostPet.BlockPermanentEvents = true;
            lostPet.Tasks.Wander();

            searchArea = spawnpoint.Around2D(1f, 2f);
            blip = new Blip(searchArea, 80f);
            blip.Color = Color.DarkOliveGreen;
            blip.EnableRoute(Color.DarkOliveGreen);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {

            base.OnCalloutNotAccepted();
        }
    }
}
