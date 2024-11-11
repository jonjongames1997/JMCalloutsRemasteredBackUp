using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Public Ordinance - Streamer Easter Egg 1", CalloutProbability.Medium, "Twitch streamer rage disturbance", "CODE 2", "LEO")]

    public class PublicOrdinanceEasterEgg1 : Callout
    {


        // General Variables //
        private static readonly string[] pedList = new string[] { "a_m_m_afriamer_01", "u_m_y_antonb", "g_m_y_ballaeast_01", "g_m_y_ballaorig_01", "ig_ballasog", "g_m_y_ballasout_01", "a_m_m_beach_01", "s_m_m_bouncer_01" };
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static string malefemale;
        private static string copgender;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-1156.54f, -1517.99f, 10.63f), // Debra's Apartment
                new(1977.52f, 3819.61f, 33.45f), // Trevor's Trailer
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            AddMinimumDistanceCheck(100f, Spawnpoint);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A person being very loud.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Public_Ordinance_Easter_Egg_1_Audio_1");
            CalloutMessage = "A person being very loud, breaking controllers, and busting his house walls.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Public Ordinance Easter Egg 1 callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Ordinance Easter Egg 1", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");

            if (Settings.HelpMessages)
            {
                Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);
            }
            else
            {
                Settings.HelpMessages = false;
            }


            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Respond_Code_2_Audio");

            Suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], Spawnpoint, 0f);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsValid();

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("oddjobs@assassinate@vice@hooker"), "argue_a", -1f, AnimationFlags.Loop);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Red;
            SuspectBlip.Alpha = 0.75f;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

            if (MainPlayer.IsMale)
                copgender = "Sir";
            else
                copgender = "Ma'am";

            counter = 0;


            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            if(Suspect) Suspect.Delete();
            if(SuspectBlip) SuspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();


            if(MainPlayer.DistanceTo(Suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Suspect.Face(MainPlayer);
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Police Department. What's going on, " + malefemale + "?. We've gotten a call from your neighbor that you were being too loud and you were breaking your game controllers.");
                    }
                    if(counter == 2)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~r~Suspect~w~: Bro, I was only playing my video game on my Playbox Console online.");
                    }
                    if(counter == 3)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: Ok. You just need to calm down. It's only a game. Wait a minute, Are you FlightReacts?");
                    }
                    if(counter == 4)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~r~Suspect~w~: Yes, " + copgender + ". I'm also known as DemonReacts, LReacts, I scored 6 like a touchdown, bitch Reacts, IQReacts, etc. You know what I'm saying.");
                    }
                    if(counter == 5)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~b~You~w~: I need you to calm down and relax. Try not to piss off your neighbors, Ok.");
                    }
                    if(counter == 6)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~r~Suspect~w~: Yes, " + copgender + ". I do apologize about this. Gamer rage gets the best of me.");
                    }
                    if(counter == 7)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("rcmjosh1"), "idle", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("Ended! Deal with the situation as you see fit.");
                        Suspect.KeepTasks = true;
                    }
                }
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Public Ordinance Easter Egg 1", "~b~You~w~: Dispatch, we are ~g~Code 4~w~! Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            base.End();

            Game.LogTrivial("[JM Callouts Remastered Log]: Public Ordinance Easter Egg 1 is Code 4!");
        }
    }
}
