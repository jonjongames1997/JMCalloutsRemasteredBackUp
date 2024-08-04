using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Trespassing On Private Property", CalloutProbability.Medium, "An individual spotted on private property", "Code 1", "LSPD")]

    public class TrespassingOnPrivateProperty : Callout
    {
        // General Variables //

        private static readonly string[] wepList = new string[] { "WEAPON_PISTOL", "WEAPON_MG", "WEAPON_BAT", "WEAPON_GOLFCLUB", "WEAPON_KNIFE", "WEAPON_HATCHET", "WEAPON_SWITCHBLADE", "WEAPON_COMBATPISTOL" };
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static int counter;
        private static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(131.11f, -1301.31f, 29.23f), // Vanilla Unicorn //
                new(-895.89f, -4.68f, 43.80f), // OCRP Postal 679
                new(980.39f, -627.39f, 59.24f), // OCRP Postal 430
                new(-350.56f, 513.67f, 120.64f), // OCRP Postal 549
                new(-1943.11f, 449.60f, 102.93f), // OCRP Postal 832
                new(-817.84f, 178.33f, 72.22f),
                new(-1039.47f, 314.27f, 66.88f),
                new(-802.71f, 423.11f, 91.58f),
                new(1643.42f, 4772.87f, 42.01f),
                new(-366.61f, 6207.90f, 31.57f),
                new(-368.03f, 6341.05f, 29.84f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_SUSPICIOUS_ACTIVITY");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A business owner reported an individual trespassing on their property.");
            CalloutMessage = "An individual trespassing on private property";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On Private Property Callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Private Property", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);


            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Black;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

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

                Game.DisplayHelp("Press ~y~E~w~ to talk to Suspect. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;


                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Hello there " + malefemale + ", Can I talk to you for a moment?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: What do you want now pigs?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Can you tell me what's going on here? We gotten a call about you being on this property when you are trespassed.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Trying to work things out with my ex. Is that a problem?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: No but you're ex doesn't want you here. I need you to leave the property cause your ex is requesting a restraining order against you.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: That mothafucka!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Leave now, " + malefemale + "! Refusing to leave the property will have you in handcuffs. You will be charged with criminal mischief and disobeying a lawful order.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Fine! That dick tickler is making a big mistake.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplayNotification("Conversation has ended.");
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Private Property", "~b~You~w~: Dispatch, We are ~g~Code 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            Game.LogTrivial("JM Callouts Remastered - Trespasing on Private Property is Code 4!");
        }

    }
}
