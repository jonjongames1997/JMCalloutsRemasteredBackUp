using CalloutInterfaceAPI;
using Rage;
using Rage.Native;
namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Illegal Campfire On Public Beach", CalloutProbability.Medium, "An individual started a campfire on Vespucci Beach.", "Code 2", "LSPD")]

    public class IllegalCampfireOnPublicBeach : Callout
    {

        // General Variables //
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                // Campfire Spawns at night //
                new(-1537.564f, -1214.748f, 1.887064f), /// Vespucci Beach ///
                new(-1420.77f, -1536.43f, 2.12f),
                new(-1463.18f, -1357.66f, 2.89f),
                new(-1490.16f, -1283.89f, 2.43f),
                new(-1571.04f, -1131.01f, 3.20f),
                new(-3078.07f, 532.86f, 2.36f),
                new(-2951.33f, 3.74f, 7.43f),
                new(-2868.12f, -28.59f, 5.34f),
                new(-2065.61f, -548.95f, 5.52f),
                new(-1955.56f, -658.09f, 6.51f),
                new(-1888.42f, -731.95f, 6.71f),
                new(-1871.27f, -771.81f, 6.07f),
                new(-1817.46f, -905.95f, 2.76f),
                new(-1329.29f, -1694.91f, 2.20f),
                new(-1241.34f, -1785.47f, 2.50f),
                new(1299.17f, 6610.68f, 2.13f), // Procorpio Beach/Blaine County
                new(-2609.83f, 3563.55f, 4.01f),
                new(-2320.80f, 4373.31f, 8.01f),
                new(-1731.23f, 4941.31f, 4.15f),
                new(-837.07f, 5903.42f, 5.11f),
                new(-345.28f, 6502.31f, 2.91f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 1000f); // Blips the area of the callout //
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_PUBLIC_DISTURBANCE UNITS_RESPOND_CODE_02_01");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Vespucci Beach Security reporting an individual starting a campfire on the beach. Suspect refused to put out the fire as requested by security.");
            CalloutMessage = "Individual started an illegal campfire on the beach!"; // Brief description of the call //
            CalloutPosition = Spawnpoint; // Gives the position of where the callout is located at //

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Illegal Campfire On Public Beach callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Campfire On Public Beach", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Chocolate;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

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

                Game.DisplayHelp("Press 'E' to speak with the ~r~suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Good evening " + malefemale + ", May I speak with you for a moment?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Good evening to you as well officer. What seems to be the problem?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Are you aware of the City Wide Ban on campfires on public beaches?");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: No, officer. Is there really a ban on campfires on beaches?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Yes there is. The city said there is a a heat wave that's in effect until further notice and it's a high risk of causing wild fires.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: RISK OF WILDFI.... That's asinine! This is sand. Are they mentally retarded? They need to go back to science class.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", I'm gonna ask you to put out the fire and leave or I'll place you under arrest for failure to comply with a lawful order.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fuck you, Dick Tickler! I'll do my campfires any time anywhere I want. It's my right as a US Citizen.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("Conversation has ended!");
                        Game.DisplayNotification("Arrest the suspect, Officer.");
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Illegal Campfire On Public Beach", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Illegal Campfire On Public Beach is Code 4!");
        }
    }
}
