using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("[JM Callouts] Code Karen", CalloutProbability.Medium, "An individual causing a scene", "Code 2", "LSPD")]


    public class CodeKaren : Callout
    {

        // General Variables //
        private static Ped Suspect;
        private static Blip SuspectBlip;
        private static Vector3 Spawnpoint;
        private static string malefemale;
        private static int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-624.7086f, -231.8441f, 38.05705f), // Vangelico Jewellery Store // 
                new(4.01f, 6512.48f, 31.88f), // Discount Store in Paleto Bay
                new(843.45f, -1031.66f, 28.19f), // Ammunation in Vespucci Blvd near Garment Factory
                new(1197.06f, 2709.98f, 38.22f), // Clothing Shop on Route 68
                new(2675.74f, 3283.97f, 55.24f), // 24/7 on Senora Freeway near Sandy
                new(547.30f, 2667.02f, 42.16f), // 24/7 on Route 68
                new(1730.48f, 6417.37f, 35.04f), // 24/7 on Senora Freewat near Paleto Bay
                new(-3170.14f, 1044.83f, 20.86f), // Suburban on Great Ocean Highway
                new(-2968.63f, 390.61f, 15.04f), // 24/7 on Great Ocean Highway next to Fleeca Bank
                new(-2962.58f, 482.68f, 15.70f), // Fleeca Bank on Great Ocean Highway
                new(-162.73f, -303.62f, 39.73f), // Ponsynby's in Vinewood
                new(148.06f, -1040.05f, 29.38f), // Fleeca Bank in Los Santos
                new(76.46f, -1391.67f, 29.38f),  // Clothing Shop on Innocence Blvd near Strip Club
                new(1699.80f, 4924.14f, 42.06f), // Grapeseed Gas Station //
                new(1693.83f, 4821.21f, 42.06f), // Grapeseed clothing shop
                new(1791.17f, 4590.48f, 37.68f), // OCRP Postal 2018 
                new(1962.04f, 3740.62f, 32.34f), // 24/7 in Sandy Shores
                new(-3242.32f, 1002.13f, 12.83f), // 24/7 in Chumash
                new(-1337.89f, -1273.98f, 4.90f), // Mask Shop in Vespucci
                new(-1272.96f, -1419.59f, 4.34f), // OCRP Postal 306
                new(-1120.70f, -1340.96f, 5.07f), // OCRP Postal 327
                new(-838.83f, -610.09f, 29.03f), // Bean Machine in Downtown KoreaTown
                new(-519.88f, -677.55f, 33.67f), // Pizza Parlor in West Koreatown
                new(1167.31f, 2708.48f, 38.16f), // Route 68
                new(1959.95f, 3743.31f, 32.34f), // Sandy Shores
                new(1706.72f, 4928.24f, 42.06f), // GrapeSeed
            };
            Spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A business employee reporting an individual causing a scene. Respond Code 2. Possibly a Karen.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_CodeKaren_Callout_Audio_1");
            CalloutMessage = "A business employee requesting an officer to escort a individual causing a scene";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Code Karen callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Code Karan", "~b~Dispatch~w~: Suspect spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_CodeKaren_Callout_Audio_2");

            Suspect = new Ped(Spawnpoint);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            Suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

            Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_01", -1f, AnimationFlags.Loop);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Coral;
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

                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". I'm gonna have to ask you to leave the premises cause the employee doesn't want you here.");
                    }
                    if (counter == 2)
                    {
                        Suspect.Face(MainPlayer);
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: Fuck no! I can come into this business anytime I want. It's public property!");
                    }
                    if (counter == 3)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: No it's not, " + malefemale + ". It's private property and they can trespass you anytime they want. Come talk to me real quick.");
                    }
                    if (counter == 4)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: I'm not talking you until I receive my fucking merchandise that I paid for! I want to speak with the Manager!!!");
                    }
                    if (counter == 5)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("random@shop_tattoo"), "_idle_a", -1f, AnimationFlags.UpperBodyOnly);
                        Game.DisplaySubtitle("~b~Player~w~: " + malefemale + ", I need you to calm down and please don't cuss, there's children in the store.");
                    }
                    if (counter == 6)
                    {
                        Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@casino@brawl@fights@argue@"), "arguement_loop_mp_m_brawler_02", -1f, AnimationFlags.Loop);
                        Game.DisplaySubtitle("~y~Suspect~w~: FUCK YOU AND FUCK THIS STORE! I'll be back with 'my little friend' I'll show y'all.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplayNotification("Arrest the suspect!");
                        Suspect.Tasks.ReactAndFlee(Suspect);
                        UltimateBackup.API.Functions.callPursuitBackup(Suspect);
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Code Karen", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("JMCallouts_Code_4_Audio");

            Game.LogTrivial("JM Callouts Remastered - Code Karen is Code 4!");
        }
    }
}
