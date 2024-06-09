using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Injured Person", CalloutProbability.High, "Reports of an injured civilian", "CODE 2", "LSPD")]

    public class InjuredPerson : Callout
    {

        private static Ped victim;
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Vector3 suspectSpawn;
        private static Blip vicBlip;
        private static Blip suspectBlip;
        private static string malefemale;
        private static int counter;
        private static float vicHeading;
        private static float suspectHeading;
        private static readonly string[] vehicleList = new string[] { "BULLET", "SANDKING", "BODHI2", "INFERNUS" };
        private static Vector3 vehicleSpawn;
        private static Vehicle suspectVehicle;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new();
            vicHeading = 175.69f;
            suspectSpawn = new();
            suspectHeading = 359.79f;
            vehicleSpawn = new();
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A civilian injured by a attacker.");
            CalloutMessage = "Civilian injured requiring assistance";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Injured Person callout is accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Injured Person", "~b~Dispatch~w~: Victim and Suspect has been spotted. Respond Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout.");

            victim = new Ped(spawnpoint, vicHeading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;
            victim.IsInjured.ToString();

            NativeFunction.Natives.APPLY_PED_DAMAGE_PACK(victim, "LightHitByVehicle", 1f, 1f);

            vicBlip = new Blip(victim)
            {
                Color = Color.Red,
                IsRouteEnabled = true,
                Scale = 0.8f,
                Name = "Injured Ped",
            };

            suspect = new Ped(suspectSpawn, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            victim.Tasks.PlayAnimation(new AnimationDictionary("misschinese2_crystalmaze"), "2int_loop_a_taocheng", -1f, AnimationFlags.Loop);

            suspect.Tasks.PlayAnimation(new AnimationDictionary("random@drunk_driver_1"), "drunk_driver_stand_loop_dd2", -1f, AnimationFlags.Loop);
            StopThePed.API.Functions.setPedAlcoholOverLimit(suspect, true);

            suspectBlip = new Blip(suspect);
            suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.DarkRed;

            suspectVehicle = new Vehicle(vehicleList[new Random().Next((int)vehicleList.Length)], vehicleSpawn, 0f);
            suspectVehicle.EngineHealth = 0;
            suspectVehicle.IsPersistent = true;
            suspectVehicle.IsDriveable = false;
            suspectVehicle.IsStolen = true;

            LSPD_First_Response.Mod.API.Functions.DisplayVehicleRecord(suspectVehicle, true);

            if (suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (victim) victim.Delete();
            if (vicBlip) vicBlip.Delete();
            if (suspect) suspect.Delete();
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();


            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", tell me what happened and how did this occurred.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I was driving to Jon Boy's Towing Services to apply for a job. (1/2)");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: The light was green in my favor then this jackass decided to cross the street. They even have the right of way. I have witnesses. (2/2)");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, I will talk to the witnesses and we'll go from there. Sit tight for me.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Ok.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("Conversation Ended. Talk to the victim");
                    }
                }

            }

            if(MainPlayer.DistanceTo(victim) <= 10f)
            {
                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", you alright? Need medical attention?");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~g~Victime~w~: Yes, I got distracted by my phone when crossing the road.");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, I will call an ambulance to have them check you out. I need to ask you some questions.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~g~Victime~w~: Ok, officer.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: What were you doing before the accident?");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: I was on my phone texting then I started crossing the street. I thought I had the right of way.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: So you weren't paying attention?");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: No I wasn't, officer.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Well, I'm gonna have to issue you a citation for jaywalking.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: WHAT THE SIGMA?! ARE YOU CEREAL?");
                    }
                    if(counter == 11)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Yes, " + malefemale + ". You are at fault of an accident. That's why the push buttons are there for a reason.");
                    }
                    if(counter == 12)
                    {
                        Game.DisplaySubtitle("~g~Victim~w~: Well, I am fucked.");
                    }
                    if(counter == 13)
                    {
                        Game.DisplaySubtitle("Conversation Ended. Deal the situation you may see fit.");
                    }
                }

                if (MainPlayer.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (victim) victim.Dismiss();
            if (suspectVehicle) suspectVehicle.Delete();
            if (suspectBlip) suspectBlip.Delete();
            if (vicBlip) vicBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Injured Person", "~b~you~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURHTER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Injured Person callout is code 4!");
        }
    }
}
