using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Sexy Time In Public - NSFW", CalloutProbability.High, "Reports of a couple having 'sexy time' in public", "Code 2", "LSPD")]

    public class SexyTimeInPublicNSFW : Callout
    {
        private static readonly string[] malePedList = new string[] { "g_m_y_ballaorig_01", "s_m_y_baywatch_01", "player_two", "player_one", "player_zero" };
        private static readonly string[] femalePedList = new string[] { "ig_abigail", "ig_amandatownley", "csb_anita", "ig_ashley", "s_f_y_bartender_01", "u_f_y_corpse_02", "u_f_m_corpse_01", "s_f_y_scrubs_01", "a_f_y_topless_01", "ig_traceydisanto", "mp_f_cocaine_01", "a_f_y_beach_02" };
        private static Ped maleSuspect;
        private static Ped femaleSuspect;
        private static Vehicle motorVehicle;
        private static Vector3 spawnpoint;
        private static Vector3 maleSpawnpoint;
        private static Vector3 femaleSpawnpoint;
        private static Blip maleBlip;
        private static Blip femaleBlip;
        private static Blip vehBlip;
        private static int counter;
        private static string malefemale;
        private static float maleHeading;
        private static float femaleHeading;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new(-770.19f, 1856.03f, 161.41f);
            maleSpawnpoint = new(-801.87f, 1909.88f, 168.91f);
            maleHeading = 207.90f;
            femaleSpawnpoint = new(-801.30f, 1909.04f, 168.88f);
            femaleHeading = 207.69f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Citizen's report of a couple having sexy time.");
            CalloutMessage = "A couple fucking in the forest";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Sexy Time In Public - NSFW callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Sexy Time In Public", "~b~Dispatch~w~: Suspects has been spotted. Respond Code 2.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            maleSuspect = new(malePedList[new Random().Next((int)malePedList.Length)], spawnpoint, 0f);
            maleSuspect.IsPersistent = true;
            maleSuspect.BlockPermanentEvents = true;

            motorVehicle = new(spawnpoint);

            femaleSuspect = new(femalePedList[new Random().Next((int)femalePedList.Length)], spawnpoint, 0f);
            femaleSuspect.IsPersistent = true;
            femaleSuspect.BlockPermanentEvents = true;

            maleSuspect.Tasks.PlayAnimation(new AnimationDictionary("rcmpaparazzo_2"), "shag_action_a", -1f, AnimationFlags.Loop);
            femaleSuspect.Tasks.PlayAnimation(new AnimationDictionary("rcmpaparazzo_2"), "shag_action_poppy", -1f, AnimationFlags.Loop);

            if (maleSuspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if (maleSuspect) maleSuspect.Delete();
            if (femaleSuspect) femaleSuspect.Delete();
            if (motorVehicle) motorVehicle.Delete();
            if (maleBlip) maleBlip.Delete();
            if (femaleBlip) femaleBlip.Delete();
            if (vehBlip) vehBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(MainPlayer.DistanceTo(maleSuspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the male suspect", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Police! Stop what y'all doing. You can't be doing this in public.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Hold on, I'm close to cumming inside of her. Let me finish.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: No, now. You will be tased, if you don't stop.");
                    }
                    if(counter == 5)
                    {
                        maleSuspect.Tasks.FightAgainst(MainPlayer);
                        maleSuspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
                        femaleSuspect.Inventory.GiveNewWeapon("WEAPON_ASSAULTSHOTGUN", 500, true);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();

        }

        public override void End()
        {
            if (maleSuspect) maleSuspect.Dismiss();
            if (femaleSuspect) femaleSuspect.Dismiss();
            if (maleBlip) maleBlip.Delete();
            if (femaleBlip) femaleBlip.Delete();
            if (motorVehicle) motorVehicle.Delete();
            if (vehBlip) vehBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Sexy Time In Public", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURHTER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Sexy Time In Public is Code 4!");
        }

    }
}
