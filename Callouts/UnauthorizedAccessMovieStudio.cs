﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Unauthorized Access - Richard's Majestic", CalloutProbability.Medium, "Reports of a trespasser", "Code 2", "LSPD")]

    public class UnauthorizedAccessMovieStudio : Callout
    {
        private static readonly string[] wepList = new string[] { "WEAPON_PISTOL_MK2", "WEPAON_SPECIALCARBINE", "WEPAON_ASSAULTRIFLE", "WEAPON_PISTOL", "WEAPON_COMBATPISTOL", "WEAPON_BAT", "WEAPON_GOLFCLUB" };
        private static Ped suspect;
        private static Blip susBlip;
        private static Vector3 spawnpoint;
        private static string malefemale;
        private static int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                // Richard's Majestic Movie Studio //

                new(-1050.09f, -512.47f, 36.04f), // Near Solomon's Office
                new(-1116.63f, -502.75f, 35.81f), // Dressing Room Trailer near the movie set
                new(-1135.64f, -458.73f, 35.42f), // Electricty Trailer near the movie set
                new(-1157.02f, -563.55f, 35.78f), // Ac Units near the movie set
                new(-1189.71f, -537.50f, 28.75f),
                new(-1187.39f, -496.85f, 35.57f),
                new(-1187.11f, -457.27f, 33.92f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A security officer reporting an individual trespassing on private property without proper access.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_SECURITY_REQUESTING_ASSISTANCE UNITS_RESPOND_CODE_02_02");
            CalloutMessage = "An individual refusing to leave";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Unauthorized Acces Movie Studio callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Unauthorized Access Movie Studio", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);


            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Yellow;
            susBlip.IsRouteEnabled = true;

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect) suspect.Delete();
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ". Talk to me real quick.");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Well, hello, Officer, what seems to be the problem?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I have received a call from the security officer that you were trespassing without proper authorization. Explain to me about that.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I have the right to be here. It's public property. I am here to film my new movie called '~y~Deranged Bank Robbers of San Andreas~w~' I don't need proper authorization.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Well, the secuirty officer said by the owner's policy that you are required to have some type of authorization to be here. So, you are looking at a trespassing citation/charge.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: F**k this! Death to Los Santos!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: ~r~KIFFLOM MOTHERF**KAS~w~!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
                        UltimateBackup.API.Functions.callPanicButtonBackup(MainPlayer);
                    }
                }
            }
            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Unauthorized Access Movie Studio", "~b~You:~w~ Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            UltimateBackup.API.Functions.dismissAllBackupUnits();
            
            base.End();

            Game.LogTrivial("JM Callouts Remastered Log: Unauthorized Access Movie Studio is code 4");
        }
    }
}
