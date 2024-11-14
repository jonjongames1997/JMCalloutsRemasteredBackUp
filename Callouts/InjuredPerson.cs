using CalloutInterfaceAPI;
using SceneManager.API;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Injured Person - BETA", CalloutProbability.Medium, "Reports of an injured civilian", "CODE 3", "LSPD")]

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
        private static Vector3 vehicleSpawn;
        private static Vehicle suspectVehicle;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new(98.48f, -1331.91f, 29.31f);
            vicHeading = 93.56f;
            suspectSpawn = new(113.21f, -1343.09f, 29.25f);
            suspectHeading = 126.79f;
            vehicleSpawn = new(105.19f, -1345.49f, 29.31f);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("JMCallouts_Injured_Person_Audio_1", spawnpoint);
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

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Injured_Person_Audio_2");

            victim = new Ped(spawnpoint, vicHeading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;
            victim.IsValid();

            vicBlip = new Blip(victim);
            vicBlip = victim.AttachBlip();
            vicBlip.Color = System.Drawing.Color.Red;
            vicBlip.IsRouteEnabled = true;

            suspect = new Ped(suspectSpawn, suspectHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.IsValid();

            NativeFunction.Natives.IS_PED_INJURED(victim, true);
            NativeFunction.Natives.IS_PED_HURT(victim, true);

            suspect.Tasks.PlayAnimation(new AnimationDictionary("random@drunk_driver_1"), "drunk_driver_stand_loop_dd2", -1f, AnimationFlags.Loop);
            StopThePed.API.Functions.setPedAlcoholOverLimit(suspect, true);

            suspectBlip = new Blip(suspect);
            suspect.AttachBlip();
            suspectBlip.Color = System.Drawing.Color.DarkRed;

            suspectVehicle = new Vehicle(vehicleSpawn);
            suspectVehicle.IsPersistent = true;

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

            SceneManager.API.Functions.DeleteLoadedPaths();

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
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", tell me what happened and how did this occurred.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: I was driving to Jon Boy's Towing Services to apply for a job. (1/2)");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: The light was green in my favor then this jackass decided to cross the street. They even have the right of way. I have witnesses. (2/2)");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Ok, I will talk to the witnesses and we'll go from there. Sit tight for me.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Ok.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("Conversation Ended. Talk to the victim *RP it out*");
                    }
                }

                if (Game.IsKeyDown(Settings.EndCall))
                {
                    End();
                }
            }
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (victim) victim.Dismiss();
            if (suspectVehicle) suspectVehicle.Delete();
            if (suspectBlip) suspectBlip.Delete();
            if (vicBlip) vicBlip.Delete();
            SceneManager.API.Functions.DeleteLoadedPaths();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Injured Person", "~b~you~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Injured Person callout is code 4!");
        }
    }
}
