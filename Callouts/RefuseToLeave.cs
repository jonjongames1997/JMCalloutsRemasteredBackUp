﻿using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Refuse To Leave", CalloutProbability.Medium, "An individual refuses to leave property", "Code 1", "LSPD")]

    public class RefuseToLeave : Callout
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
                new(-1222.80f, -907.12f, 12.33f), // Rob's Liquors near the Nightclub
                new(-1193.68f, -768.45f, 17.32f), // Suburban near Vespucci PD HQ
                new(-330.96f, 6081.46f, 31.45f), // Ammunation Near Paleto PD
                new(-113.23f, 6469.90f, 31.63f), // Paleto Bank
                new(-57.16f, 6522.26f, 31.49f), // Willie's Grocery Store
                new(-3170.89f, 1043.86f, 20.86f), // Suburban in Chumash
                new(-1660.25f, -1043.75f, 13.15f),
                new(-1276.61f, -1417.05f, 4.34f),
                new(26.69f, -1343.42f, 29.50f),
                new(4.41f, -1604.42f, 29.29f),
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Refuse_To_Leave_Callout_Audio_1");
            CalloutInterfaceAPI.Functions.SendMessage(this, "Person refusing to leave");
            CalloutMessage = "Individual refusing to leave property by business owner/employee.";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Refuse To Leave callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Refuse To Leave", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 1~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_1_Response_Audio");

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.IsMeleeProof = true;

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.BlueViolet;
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

                Game.DisplayHelp("Press ~y~" + Settings.Dialog + "~w~ to interact with ~r~suspect~w~. ~y~Approach with caution~w~.", false);

                if (Game.IsKeyDown(Settings.Dialog))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: Hello there " + malefemale + ", Can I talk to you for a second?");
                    }
                    if (counter == 2)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: What now donut pigs?");
                    }
                    if (counter == 3)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: Can you tell me what's going on?");
                    }
                    if (counter == 4)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: That bitch over there told me I can't come in here.");
                    }
                    if (counter == 5)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: Is there a reason why they can't let you come in here?");
                    }
                    if (counter == 6)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: I was outside the door asking people for money. They called the cops and they told me that I was trespassed from the property.");
                    }
                    if (counter == 7)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplayNotification("~y~Tip~w~: If the suspect was trespassed from the property before, that's an arrestable offense.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Ok. Well, you know you can be arrested and/or cited for trespassing, right?");
                    }
                    if (counter == 9)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: WHAT?! Are you f***ing with me?");
                    }
                    if (counter == 10)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: No, I'm not. Don't try anything stupid, you'll make things worse on yourself.");
                    }
                    if (counter == 11)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: F**k you and f**k them! I'm outta here, playa!");
                    }
                    if (counter == 12)
                    {
                        Game.DisplaySubtitle("Conversation ended!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                        Suspect.Inventory.GiveNewWeapon("WEAPON_COMBATPISTOL", 500, true);
                    }
                }
            }
            if (MainPlayer.IsDead)
            {
                End();
            }

            if (Game.IsKeyDown(Settings.EndCall))
            {
                End();
            }
        }

        public override void End()
        {
            base.End();
            if (Suspect) Suspect.Dismiss();
            if (SuspectBlip) SuspectBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Refuse To Leave", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            Game.LogTrivial("JM Callouts Remastered - Refuse to leave is Code 4!");
        }

    }
}
