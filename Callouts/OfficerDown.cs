using CalloutInterfaceAPI;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Officer Down", CalloutProbability.High, "An officer reported an officer down", "Code 3", "LSPD")]

    public class OfficerDown : Callout
    {
        // General Variables //
        private string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_PISTOL_MK2", "WEAPON_DOUBLEACTION", "WEAPON_CARBINERIFLE" };
        private Ped suspect1;
        private Ped cop;
        private Ped cop2;
        private Ped suspect2;
        private Ped suspect3;
        private Ped suspect4;
        private Vector3 spawnPoint;
        private Vector3 searchArea;
        private Blip blip;
        private Blip susBlip;
        private int scenario = 0;
        private bool hasBegunAttacking = false;
        private bool isArmed = false;
        private bool hasPursuitBegun = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(94.63f, -217.37f, 54.49f), // Shopping Center in Vinewood
                new Vector3(-55.11012f, -1110.357f, 26.0027f), // Sandy Shores
                new Vector3(1378.079f, 3599.861f, 34.43561f), // Trevor's Meth Lab/Office
                new Vector3(371.4488f, 2637.318f, 44.49557f), // Motel on Route 68 //
                new Vector3(30.33209f, 2793.319f, 57.52761f), // Gas Station Near Church on 68
                new Vector3(1625.555f, 3567.901f, 35.1463f), // Sandy Hotel
            };
            spawnPoint = LocationChooser.chooseNearestLocation(list);
            scenario = new Random().Next(0, 100);
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an officer down by a fellow officer");
            CalloutMessage = "Multiple reports of an officer down. Respond Code 3";
            CalloutPosition = spawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SHOTS_FIRED_AT_AN_OFFICER_03");

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Officer Down callout accepted");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Officer Down", "~b~Dispatch: The suspects has been spotted! Respond ~r~Code 3");

            suspect1 = new Ped(spawnPoint);
            suspect1.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);
            suspect1.BlockPermanentEvents = true;
            suspect1.IsPersistent = true;
            suspect1.Tasks.Wander();
            susBlip = suspect1.AttachBlip();

            cop = new Ped("S_M_Y_HWAYCOP_01", spawnPoint, scenario);
            cop.BlockPermanentEvents = true;
            cop.IsPersistent = true;
            cop.Kill();
            NativeFunction.Natives.APPLY_PED_DAMAGE_PACK(cop, "ShotByFireArm", 1f, 1f);

            cop2 = new Ped("S_M_Y_COP_01", spawnPoint, scenario);
            cop2.BlockPermanentEvents = true;
            cop2.IsPersistent = true;
            cop2.Tasks.StandStill(500);

            suspect2 = new Ped(spawnPoint);
            suspect3 = new Ped(spawnPoint);
            suspect4 = new Ped(spawnPoint);
            suspect2.IsPersistent = true;
            suspect3.IsPersistent = true;
            suspect4.IsPersistent = true;
            suspect2.Tasks.FightAgainst(Game.LocalPlayer.Character);
            suspect3.Tasks.FightAgainst(Game.LocalPlayer.Character);
            suspect4.Tasks.FightAgainst(Game.LocalPlayer.Character);

            searchArea = spawnPoint.Around2D(1f, 2f);
            blip = new Blip(searchArea, 80f);
            blip.Color = Color.OrangeRed;
            blip.EnableRoute(Color.OrangeRed);
            blip.Alpha = 0.5f;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect1) suspect1.Delete();
            if (suspect2) suspect2.Delete();
            if (suspect3) suspect3.Delete();
            if (suspect4) suspect4.Delete();
            if (cop) cop.Delete();
            if (cop2) cop2.Delete();
            if (blip) blip.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            GameFiber.StartNew(delegate
            {
                if (suspect1.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 40f)
                {
                    if (blip) blip.Delete();
                }
                if (suspect1.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 70f && !isArmed)
                {
                    suspect1.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                    isArmed = true;
                }
                if (suspect1 && suspect1.DistanceTo(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeFront)) < 40f && !hasBegunAttacking)
                {
                    if (scenario > 40f)
                    {
                        new RelationshipGroup("VICTIM");
                        new RelationshipGroup("AGGRESSOR");
                        cop.RelationshipGroup = "VICTIM";
                        cop2.RelationshipGroup = "VICTIM";
                        suspect1.RelationshipGroup = "AGGRESSOR";
                        suspect2.RelationshipGroup = "AGGRESSOR";
                        suspect3.RelationshipGroup = "AGGRESSOR";
                        suspect4.RelationshipGroup = "AGGRESSOR";
                        suspect1.KeepTasks = true;
                        Game.SetRelationshipBetweenRelationshipGroups("VICTIM", "AGGRESSOR", Relationship.Hate);
                        suspect1.Tasks.FightAgainstClosestHatedTarget(1000f);
                        GameFiber.Wait(2000);
                        suspect1.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        hasBegunAttacking = true;
                        GameFiber.Wait(600);
                    }
                    else
                    {
                        if (!hasPursuitBegun)
                        {
                            suspect1.Face(Game.LocalPlayer.Character);
                            suspect1.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
                            Game.DisplayNotification("Suspect is surrendering. Take him/her into custody.");
                            hasPursuitBegun = true;
                        }
                    }
                }
                if (Game.LocalPlayer.Character.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }, "Reports of a officer down [JM Callouts Remastered]");

            base.Process();
        }

        public override void End()
        {
            if (suspect1) suspect1.Dismiss();
            if (suspect2) suspect2.Dismiss();
            if (suspect3) suspect3.Dismiss();
            if (suspect4) suspect4.Dismiss();
            if (cop) cop.Dismiss();
            if (cop2) cop2.Dismiss();
            if (blip) blip.Delete();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Officer Down", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("[JM Callouts Remastered]: Officer Down is code 4!");
        }
    }
}
