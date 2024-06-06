using CalloutInterfaceAPI;
using Rage;
using Rage.Native;

namespace JMCalloutsRemastered.Callouts
{


    [CalloutInterface("[JM Callouts] Rocketman", CalloutProbability.High, "Reports of a civilian carrying an explosive weapon", "Code 3", "SWAT")]

    public class Rocketman : Callout
    {
        private static readonly string[] wepList = new string[] { "WEAPON_RPG", "WEAPON_HOMINGLAUNCHER", "WEAPON_FIREWORK" };
        private static Ped suspect;
        private static Blip susBlip;
        private static Vector3 spawnpoint;
        private static int counter;
        private static string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = World.GetNextPositionOnStreet(MainPlayer.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS_01 WE_HAVE_01 CRIME_BRANDISHING_WEAPON_01 UNITS_RESPOND_CODE_03_01", spawnpoint);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Citizen's reporting a individual carrying an explosive weapon.");
            CalloutMessage = "An individual is threatening citizens with an explosive weapon.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Rocketman callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Rocketman", "~b~Dispatch~w~: The suspect has been spotted! Respond ~r~Code 3~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("Here Comes Rocketman");

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            suspect.Inventory.GiveNewWeapon("WEAPON_RPG", 500, true);

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Red;
            susBlip.IsRouteEnabled = true;

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
            if (susBlip) susBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~You~w~: POLICE! Drop the weapon or you'll be fucking shot!");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What goin' on?");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You~w~: DROP THE WEAPON, NOW " + malefemale + "!");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Not until you tell me what's goin' on.");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: We've gotten reports of you threatening people with that explosive weapon. Drop it now, I'm not gonna say it again, " + malefemale + ".");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Go fuck yourself, PO PO!");
                    }
                    if (counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Chief, suspect is not complying with commands and not dropping the weapon.");
                    }
                    if (counter == 8)
                    {
                        Game.DisplaySubtitle("~b~Chief~w~: If he/she points the weapon at you, ~r~LETHAL FORCE~w~ is authorized.");
                    }
                    if (counter == 9)
                    {
                        Game.DisplaySubtitle("~b~You~w~: 10-4, Chief.");
                    }
                    if (counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: " + malefemale + ", Just drop the weapon, we don't want to shoot you. It's not worth it. Do you have kids?");
                    }
                    if (counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Fuck you! Take your last breath of fresh air, motherfuckers!");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                        suspect.KeepTasks = true;
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
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Rocketman", "~b~You~w~: Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            base.End();

            Game.LogTrivial("JM Callouts Remastered [LOG]: Rocketman callout is Code 4!");
        }

    }
}