using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Trespassing On Construction Property", CalloutProbability.High, "A individual trespassing on construction property", "Code 1", "LSPD")]

    public class TrespassingOnConstructionProperty : Callout
    {
        // General Variables //
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(7.788585f, -394.9392f, 39.41744f), // Construction site next the Freeway
                new(-129.83f, -1041.61f, 27.27f), // Across from Simeon's Dealership
                new(-1140.92f, -1405.55f, 4.44f), // Near Floyd's Apartment
                new(-2349.65f, 3997.07f, 26.90f), // On the G.O.H near Route 68
                new(-452.89f, -541.51f, 25.13f),
                new(-59.43f, -546.32f, 31.59f),
                new(55.14f, -710.83f, 30.84f),
                new(1093.11f, 2238.07f, 45.38f),
                new(-191.15f, 1857.57f, 198.27f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_SUSPICIOUS_ACTIVITY");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen reporting a trespasser on construction property.");
            CalloutMessage = "Reports of an individual trespassing on constrcution property";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remasteed Log]: Trespassing On Construction Property Callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Construction Property", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Beige;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            if (Settings.ActivateAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(Spawnpoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.LocalUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(Spawnpoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.LocalUnit);
            }
            else
            {
                Settings.ActivateAIBackup = false;
            }

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (Suspect) Suspect.Delete();
            if (SuspectBlip) SuspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~~w~ to talk to Suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Can you come talk to me for a minute?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What do you want? I'm filming here.");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You can't be on constrcution property. It's against the law. Are you an employee of the construction company?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Yes. I'm surveying the site to write down important information for the mayor. Is that a problem?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: No. Just need to verify. Do you mind if I run your information real quick? It's a procedure I have to follow.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Oh, shit! They know!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Construction Property", "~b~You~w~: We are ~g~Code 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Trespassing on Construction Property is Code 4!");
        }
    }
}
