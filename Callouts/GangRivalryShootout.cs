using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;
using System.Threading;

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
                new Vector3(39.15001f, -1739.961f, 29.30341f), // Mega Mall
                new Vector3(126.7119f, -1472.859f, 29.14161f), // Lucky Plucker in Strawberry
                new Vector3(-1176.59f, -1517.95f, 4.39f), // Floyd's House 
                new Vector3(-435.25f, -1703.43f, 18.96f), // Recycling Plant near Maze Bank Arena
                new Vector3(86.61f, 3688.23f, 39.73f), // Stab City
                new Vector3(1958.62f, 4639.84f, 40.68f), // Near MacKenzie Field Airstrip
            };
            spawnPoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Rival Gang Shootout in public. Respond Code 99!");
            CalloutMessage = "Reports of a gang shootout";
            CalloutPosition = spawnPoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Gang Rivalry Shootout callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Gang Shootout", "~b~Dispatch: The Gangs has been spotted! Respond ~r~Code 99");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

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

            Aggressor1.Armor = 500;
            Aggressor2.Armor = 500;
            Aggressor3.Armor = 500;
            Aggressor4.Armor = 500;
            Aggressor5.Armor = 500;
            Aggressor6.Armor = 500;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (Aggressor1) Aggressor1.Delete();
            if (Aggressor2) Aggressor2.Delete();
            if (Aggressor3) Aggressor3.Delete();
            if (Aggressor4) Aggressor4.Delete();
            if (Aggressor5) Aggressor5.Delete();
            if (Aggressor6) Aggressor6.Delete();
            if (blip1) blip1.Delete();
            if (blip2) blip2.Delete();
            if (blip3) blip3.Delete();
            if (blip4) blip4.Delete();
            if (blip5) blip5.Delete();
            if (blip6) blip6.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            GameFiber.StartNew(delegate
            {
                GameFiber.Yield();
                if (Game.LocalPlayer.Character.DistanceTo(spawnPoint) < 100f && !hasBegunAttacking)
                {
                    new RelationshipGroup("LOST MC");
                    new RelationshipGroup("HIPSTERS");
                    Aggressor1.RelationshipGroup = "LOST MC";
                    Aggressor2.RelationshipGroup = "LOST MC";
                    Aggressor3.RelationshipGroup = "LOST MC";
                    Aggressor4.RelationshipGroup = "HIPSTERS";
                    Aggressor5.RelationshipGroup = "HIPSTERS";
                    Aggressor6.RelationshipGroup = "HIPSTERS";
                    Game.SetRelationshipBetweenRelationshipGroups("LOST MC", "HIPSTERS", Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups("HIPSTERS", "LOST MC", Relationship.Hate);
                    Aggressor1.Armor = 700;
                    Aggressor2.Armor = 700;
                    Aggressor3.Armor = 700;
                    Aggressor4.Armor = 700;
                    Aggressor5.Armor = 700;
                    Aggressor6.Armor = 700;
                    Aggressor1.Tasks.FightAgainstClosestHatedTarget(1000f);
                    Aggressor2.Tasks.FightAgainstClosestHatedTarget(1000f);
                    Aggressor3.Tasks.FightAgainstClosestHatedTarget(1000f);
                    Aggressor4.Tasks.FightAgainstClosestHatedTarget(1000f);
                    Aggressor5.Tasks.FightAgainstClosestHatedTarget(1000f);
                    Aggressor6.Tasks.FightAgainstClosestHatedTarget(1000f);
                    hasBegunAttacking = true;
                }
                if (Game.LocalPlayer.Character.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }, "JM Callouts Remastered: Gang Rivalry Shootout");

            base.Process();
        }

        public override void End()
        {
            if (Aggressor1) Aggressor1.Dismiss();
            if (Aggressor2) Aggressor2.Dismiss();
            if (Aggressor3) Aggressor3.Dismiss();
            if (Aggressor4) Aggressor4.Delete();
            if (Aggressor5) Aggressor5.Dismiss();
            if (Aggressor6) Aggressor6.Dismiss();
            if (blip1) blip1.Delete();
            if (blip2) blip2.Delete();
            if (blip3) blip3.Delete();
            if (blip4) blip4.Delete();
            if (blip5) blip5.Delete();
            if (blip6) blip6.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Gang Shootout", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Gang Rivalry Shootout is code 4!");
        }
    }
}
