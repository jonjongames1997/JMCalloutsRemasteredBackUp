using CalloutInterfaceAPI;
using StopThePed;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Person On The Highway", CalloutProbability.Medium, "A citizen's report of an individual on the freeway", "Code 2", "SAHP")]

    public class PersonOnTheHighway : Callout
    {
        private static readonly string[] wepList = new string[] { "WEAPON_KNIFE", "WEAPON_BAT", "WEAPON_DAGGER", "WEAPON_GOLFCLUB", "WEAPON_HAMMER", "WEAPON_HATCHET", "WEAPON_PISTOL", "WEAPON_COMBATPISTOL", "WEAPON_AUTOSHOTGUN", "WEAPON_SWITCHBLADE", "WEAPON_PROXMINE" };
        private static readonly string[] pedList = new string[] { "a_m_m_afriamer_01", "ig_amandatownley", "ig_ashley", "g_f_y_ballas_01", "a_f_m_bodybuild_01", "a_f_y_eastsa_03", "ig_maryann", "ig_money", "s_m_y_baywatch_01", "a_f_y_beach_01", "a_f_m_bevhills_01", "a_f_m_fatbla_01", "ig_tracydisanto", "a_f_y_tourist_02", "ig_tonya", "a_f_y_tennis_01", "u_f_y_poppymich", "ig_michelle" };
        private static Blip blip;
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(2865.42f, 4259.82f, 50.08f), // Route 13 near Maude's House 
                new(1707.10f, 1413.60f, 85.92f), // Route 13 going into Blaine County
                new(2440.64f, 963.54f, 87.11f), // Near the wind farm on Route 15
                new(-2721.99f, 8.95f, 15.55f), // Route 1 going into Chumash
                new(1668.16f, -946.03f, 64.91f), // Polomino Freeway
                new(1311.71f, -2292.97f, 52.39f), // ElysianFields Freeway
                new(1165.92f, 6508.53f, 20.97f), // Great Ocean Highway near Paleto Bay
                new(1500.31f, 6443.57f, 22.48f),
                new(2028.89f, 6091.67f, 48.02f),
                new(2373.62f, 5778.17f, 46.05f),
                new(2578.77f, 5191.19f, 44.72f),
                new(2673.34f, 4825.73f, 44.58f),
                new(2879.91f, 3705.77f, 52.63f),
                new(2397.75f, 2928.08f, 40.24f),
                new(1923.26f, 2495.40f, 54.73f),
                new(1827.58f, 1998.85f, 56.74f),
                new(2514.53f, 196.93f, 104.21f),
                new(2419.41f, -218.85f, 86.01f),
                new(2250.02f, -464.08f, 90.19f),
                new(1363.15f, -1082.76f, 52.49f),
                new(1060.88f, -1184.31f, 55.82f),
                new(535.26f, -1186.54f, 41.97f),
                new(-282.89f, -1193.05f, 37.08f),
                new(-455.82f, -1584.14f, 39.17f),
                new(-906.17f, -1824.35f, 35.34f),
                new(-228.77f, -2455.10f, 55.88f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_ALL_UNITS_02 WE_HAVE_01 SOS_02 UNITS_RESPOND_CODE_02_02");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an individual on the highway");
            CalloutMessage = "A citizen's reporting an individual on the highway. Respond Code 2.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Person on the highway callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Person On The Highway", "~b~Dispatch~w~: The suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@nightclub_island@dancers@club@"), "hi_idle_b_m03", -1, AnimationFlags.Loop);

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Gold;
            blip.IsRouteEnabled = true;

            suspect.KeepTasks = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

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
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~suspect~w~. Approach with ~y~CAUTION~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Hey there, " + malefemale + ". What goin' on? Come talk to me real quick.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Hi, Officer, My Uber driver kicked me out his car for no reason.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Why would your driver do that? What was the reason?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: The driver is very picky on who they pick up. I find that as discrimination.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Ok, you didn't call a taxi? If you are being discriminated, you can contact Uber and file a complaint with them cause that's on the company. If the driver was threatening you or anything criminal, that's when we step in.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: No I didn't call a taxi. I prefer to walk. I know it's against the law to do that but nobody will stop and help me.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: I am more than happy to call you a taxi. I don't want to put you in cuffs. Let me call you a taxi and get you home safe. I am worried for your safety,");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Thank you, officer.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("~y~Suspect~w~: Death to Los Santos, Motherfucka!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.KeepTasks = true;
                        suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                        suspect.Armor = 500;
                    }
                }

                if (MainPlayer.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }
        }

        public override void End()
        {
            base.End();
            if (suspect) suspect.Dismiss();
            if (blip) blip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Person On The Highway", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered [LOG]: Person On The Highway is code 4!");
        }
    }
}
