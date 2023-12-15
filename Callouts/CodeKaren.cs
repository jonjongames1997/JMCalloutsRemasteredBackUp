using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Code Karen", CalloutProbability.Medium, "An individual causing a scene", "Code 2", "LSPD")]


    public class CodeKaren : Callout
    {

        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;


        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = new Vector3(-624.7086f, -231.8441f, 38.05705f); // Vangelico Jewellery Store // 
            heading = 315.2649f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A business employee reporting an individual causing a scene. Respond Code 2. Possibly a Karen.");
            CalloutMessage = "A business employee requesting an officer to escort a individual causing a scene";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Code Karen callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Code Karan", "~b~Dispatch: ~w~Suspect spotted. Respond ~Code 2.");

            Suspect = new Ped(Spawnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

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

        public override void Process()
        {
            base.Process();

            if (Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~'E'~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if (counter == 1)
                    {
                        Suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("Player: Excuse me, " + malefemale + ". I'm gonna have to ask you to leave the premises cause the employee doesn't want you here.");
                    }
                    if (counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect: Fuck no! I can come into this business anytime I want. It's public property!");
                    }
                    if (counter == 3)
                    {
                        Game.DisplaySubtitle("Player: No it's not, " + malefemale + ". It's private property and they can trespass you anytime they want. Come talk to me real quick.");
                    }
                    if (counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect: I'm not talking you until I receive my fucking merchandise that I paid for! I want to speak with the Manager!!!");
                    }
                    if (counter == 5)
                    {
                        Game.DisplaySubtitle("Player: " + malefemale + ", I need you to calm down and please don't cuss, there's children in the store.");
                    }
                    if (counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect: FUCK YOU AND FUCK THIS STORE! I'll be back with 'my little friend' I'll show y'all.");
                    }
                    if (counter == 7)
                    {
                        Game.DisplayNotification("Arrest the suspect!");
                        Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        Suspect.Inventory.GiveNewWeapon("WEAPON_CANDYCANE", 500, true);
                    }
                }
            }

            if (Game.LocalPlayer.Character.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (Suspect && Suspect.IsDead) End();
            if (Suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(Suspect)) End();
        }

        public override void End()
        {
            base.End();

            if (Suspect.Exists())
            {
                Suspect.Dismiss();
            }
            if (SuspectBlip.Exists())
            {
                SuspectBlip.Delete();
            }

            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Code Karen", "~b~You:~w~ Dispatch, we are ~g~Code 4~w~. Show me back 10-8.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");

            Game.LogTrivial("JM Callouts Remastered - Code Karen is Code 4!");
        }
    }
}
