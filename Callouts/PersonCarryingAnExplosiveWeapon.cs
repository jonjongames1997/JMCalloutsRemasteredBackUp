using CalloutInterfaceAPI;

namespace JMCalloutsRemastered
{


    [CalloutInterface("[JM Callouts] Person Carrying An Explosive Weapon", CalloutProbability.High, "Citizen's report of an individual carrying an explosive weapon", "CODE 3", "SWAT")]

    public class PersonCarryingAnExplosiveWeapon : Callout
    {

        private static readonly string[] wepList = new string[] { "WEAPON_RPG", "WEAPON_GRENADELAUNCHER", "WEAPON_GRENADELAUNCHER_SMOKE", "WEAPON_FIREWORK", "WEAPON_HOMINGLAUNCHER", "WEAPON_COMPACTLAUNCHER", "WEAPON_GRENADE", "WEAPON_MOLOTOV", "WEAPON_STICKYBOMB", "WEAPON_PROXMINE", "WEAPON_PIPEBOMB" };
        private static Ped suspect;
        private static Vector3 spawnpoint;
        private static Blip suspectBlip;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_SWAT_UNITS_01 WE_HAVE_01 CRIME_ASSAULT_WITH_A_DEADLY_WEAPON UNITS_RESPOND_CODE_03_02", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Multiple reports of an explosive device");
            CalloutMessage = "An idividual with a deadly weapon reported by civilian";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Person Carrying An Explosive Weapon has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Person Carrying An Explosive Weapon", "~b~Dispatch~w~: Suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);

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
                Game.DisplayHelp("Press ~y~E~w~ to interact with the suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: Police! Drop that weapon right now!");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: No. Back off! I will use this!");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: If you aim that weapon at us, we will shoot you graveyard dead.");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: BET!");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", Don't do it!");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: I said, 'BET!'. What part of that don't you understand? You speak English?");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Armor = 500;
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
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Person Carrying An Explosive Weapon", "~b~You~w~: Dispatch, we are ~g~CODE 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Person Carrying An Explosive Weapon callout is Code 4!");
        }
    }
}
