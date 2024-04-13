using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On Railroad Property", CalloutProbability.Medium, "A citizen trespassing on Railroad Property", "Code 1", "LSPD")]

    public class TrespassingOnRailRoadProperty : Callout
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
                new(452.94f, -1648.89f, 29.97f), // Next to Davis Sheriff
                new(1743.83f, 3464.93f, 38.50f), // Railroad tracks on Panarama Dr/Sandy Shores Welcome sign
                new(2609.09f, 1699.36f, 26.83f), // Near power station near Sandy Shores/Davis Quartz
                new(2744.25f, 3888.97f, 43.94f), // See screenshot for location 
                new(153.24f, -2070.53f, 17.99f),
                new(222.25f, -2401.46f, 8.10f),
                new(221.80f, -2542.75f, 6.20f),
                new(),
                new(),
                new(),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 1000f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 CRIME_SUSPICIOUS_ACTIVITY");
            CalloutInterfaceAPI.Functions.SendMessage(this, "A citizen reporting a suspicious person on the railroad tracks.");
            CalloutMessage = "A suspicious person on railroad tracks.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Trespassing On Railroad Property callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Railroad Property", "~b~Dispatch~w~: Suspect Spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.CadetBlue;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

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

                Game.DisplayHelp("Press ~y~E~w~ to interact with Suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Hello there " + malefemale + ". Can I speak to you for a moment?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: ALIENS! ALIENS! They're here! RUN FOR YOUR LIVES!");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", Calm down for me please.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Oh, I thought you were an alien. Sorry, Officer. What's up? Want to smoke some crack? Smoke some weed?");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Did you do any illegal drugs that I should know about? and why are you on railroad property?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Yeah, drugs are the 2nd best medicine cause laughter is the #1 best medicine. I'm trying to record some videos of the trains coming through. Why?");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Well " + malefemale + ", that's fine but you can't be on the tracks cause people have been comitting suicde by train almost every year. As long as you're on the other side of the crossing, that's fine. Can I see some identification from you if it's in your posession?");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What's a indentification again?");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", you're under arrest.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I'm not going back to prison to drop the soap!");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("Conversation ended.");
                        Suspect.Tasks.FightAgainst(MainPlayer);
                        Suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                        Suspect.Armor = 500;
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Trespassing On Railroad Property", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            Game.LogTrivial("JM Callouts Remastered - Trespassing On Railroad Property is Code 4!");
        }

    }
}
