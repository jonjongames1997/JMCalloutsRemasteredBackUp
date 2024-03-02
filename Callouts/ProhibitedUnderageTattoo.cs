using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Prohibited Underage Tattoo", CalloutProbability.Medium, "Reports of a underage teen getting a tattoo.", "Code 2", "LSPD")]

    public class ProhibitedUnderageTattoo : Callout
    {

        // General Variables //
        public static Ped suspect;
        public static Blip susBlip;
        public static Vector3 spawnpoint;
        public static int counter;
        public static string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            List<Vector3> list = new()
            {
                new(-291.41f, 6198.41f, 31.49f),
                new(1862.62f, 3750.56f, 33.03f),
                new(321.39f, 180.68f, 103.59f),
            };
            spawnpoint = LocationChooser.ChooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an underage teen getting a tattoo.");
            CalloutMessage = "A underage teen getting a tattoo";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[LOG]: Prohibited Underage Tattoo callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Prohibited Underage Tattoo", "~b~Dispatch~w~: Suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~End~w~ at anytime to end the callout.", false);

            suspect = new Ped(spawnpoint);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.IsMeleeProof = true;
            suspect.IsValid();

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Coral;
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

            if(MainPlayer.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with ~r~Suspect~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(MainPlayer);
                        Game.DisplaySubtitle("~b~Player~w~: Excuse me, " + malefemale + ". Come talk to me real quick.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: What did I do wrong?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You know it's unlawful for person's under 18 to get a tattoo, right?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Yeah, so what????");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: The tattoo artist is prohibited from performing a tattoo on people under 18. Where is your parents?");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Bullshit. I'm not leaving until I get that tattoo I want. My parents are at work and they knew I was getting a tattoo.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Player~w~: You really don't want to make things worse. Either leave the property or you're walking out in handcuffs. Your choice.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: MAKE ME, BITCH!");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("Conversation ended.");
                        suspect.Tasks.FightAgainst(MainPlayer);
                        suspect.Inventory.GiveNewWeapon("WEAPON_BOTTLE", 500, true);
                    }
                }
            }

            if (MainPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
        }

        public override void End()
        {
            base.End();

            if (suspect.Exists())
            {
                suspect.Dismiss();
            }
            if (susBlip.Exists())
            {
                susBlip.Delete();
            }
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Prohibited Underage Tattoo", "~b~You:~w~ Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("[LOG]: Prohibited Underage Tattoo callout is code 4!");
        }
    }
}
