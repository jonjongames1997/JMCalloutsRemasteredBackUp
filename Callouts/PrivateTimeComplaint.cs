using CalloutInterfaceAPI;
using LSPD_First_Response.Engine;

namespace JMCalloutsRemastered
{

    [CalloutInterface("[JM Callouts] Private Time Complaint", CalloutProbability.Medium, "Reports of a couple having sexy time in public", "CODE 2", "LEO")]

    internal class PrivateTimeComplaint : Callout
    {
        private static readonly string[] femalePedList = new string[] { "ig_amandatownley", "ig_tracydisanto" };
        private static readonly string[] malePedList = new string[] { "ig_jimmydisanto" };
        private static Ped suspect1;
        private static Ped suspect2;
        private static Blip susBlip1;
        private static Blip susBlip2;
        private static Vector3 spawnpoint;
        private static Vector3 femalespawnpoint;
        private static int counter;
        private static string malefemale;
        private static float heading;
        private static float femaleheading;
        private static string copGender;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new(-477.8965f, -450.5123f, 34.2013f);
            heading = 162;
            femalespawnpoint = new(-478.0538f, -447.9926f, 33.2013f);
            femaleheading = 2;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Neighbors reporting their neighbors having sex in public.");
            CalloutMessage = "Reports of a couple having sexy time in public.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Private Time Complaint callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Private Time Complaint", "~b~Dispatch: ~w~Suspect has been spotted. Respond ~r~Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Lost_Individual_Audo_2");

            suspect2 = new Ped(femalePedList[new Random().Next((int)femalePedList.Length)], femalespawnpoint, 0f);
            suspect2.IsPersistent = true;
            suspect2.BlockPermanentEvents = true;
            suspect2.IsValid();

            suspect2.Tasks.PlayAnimation(new AnimationDictionary("rcmpaparazzo_2"), "shag_loop_poppy", -1f, AnimationFlags.Loop);

            suspect1 = new Ped(malePedList[new Random().Next((int)malePedList.Length)], spawnpoint, 0f);
            suspect1.IsPersistent = true;
            suspect1.BlockPermanentEvents = true;
            suspect1.IsValid();

            suspect1.Tasks.PlayAnimation(new AnimationDictionary("rcmpaparazzo_2"), "shag_action_a", -1f, AnimationFlags.Loop);

            susBlip2 = suspect2.AttachBlip();
            susBlip1 = suspect1.AttachBlip();

            susBlip1.Color = System.Drawing.Color.Red;
            susBlip1.Alpha = 0.75f;
            susBlip1.IsRouteEnabled = true;

            if (suspect1.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            if (MainPlayer.IsMale)
                copGender = "Sir";
            else
                copGender = "Ma'am";

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (suspect1) suspect1.Delete();
            if (suspect2) suspect2.Delete();
            if (susBlip1) susBlip1.Delete();
            if (susBlip2) susBlip2.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {

            if(MainPlayer.DistanceTo(suspect1) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect1.Face(MainPlayer);
                        suspect2.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, May I talk to you for a minute?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~o~Suspect~w~: Oh, shit! The diddy party is over! RUN!!!!!");
                    }
                    if(counter == 3)
                    {
                        Game.DisplayHelp("Capture the suspects!");
                        suspect1.Tasks.ReactAndFlee(suspect1);
                        suspect2.Tasks.ReactAndFlee(suspect2);
                    }
                }

                if (MainPlayer.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }

            base.Process();
        }

        public override void End()
        {
            if (suspect1) suspect1.Dismiss();
            if (suspect2) suspect2.Dismiss();
            if (susBlip1) susBlip1.Delete();
            if (susBlip2) susBlip2.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Private Time Complaint", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");
            Game.LogTrivial("[LOG]: JM Callouts Remastered - Private Time Complaint is Code 4!");

            base.End();
        }
    }
}
