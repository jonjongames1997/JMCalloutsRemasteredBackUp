using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Unknown Trouble", CalloutProbability.High, "Reports of an unknown trouble", "CODE 3", "LSPD")]


    public class UnknownTrouble : Callout
    {

        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip suspectBlip;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_SUSPICIOUS_ACTIVITY UNITS_RESPOND_CODE_03_01", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Multiple reports of an unknown trouble.");
            CalloutMessage = "Reports of an unknown trouble. Respond & approach with caution.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Unknown Trouble callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Unknown Trouble", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~End~w~ at anytime to end the callout.");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.Red;
            suspectBlip.IsRouteEnabled = true;

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
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();


            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~Suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Los Santos Police Department!");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Go away, motherfucker!");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", You ok? What's going on?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I said 'Go Away', motherfucking cocksucker, you!");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: I'm not going anywhere, " + malefemale + ". Tell me what's bothering you or what's the issue.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: If you don't get your motherfucking ass outta here, I will shoot to kill!");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Chief, suspect is threatening to shoot to kill. What do you want us to do?");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~b~Chief~w~: Try to continue to negotiate with the suspect. If they point any guns at you, ~r~LETHAL FORCE IS AUTHORIZED~w~.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You~w~: 10-4, chief.");
                    }
                    if(counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", please talk to me, I don't want to shoot you. Let's talk this out. We'll get you the help you need.");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: FUCK YOU! Take your last breath of fresh air, motherfuckers!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_TACTICALRIFLE", 500, true);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (suspectBlip) suspectBlip.Delete();
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Unknown Trouble", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Unknown Trouble callout is Code 4!");
        }
    }
}
