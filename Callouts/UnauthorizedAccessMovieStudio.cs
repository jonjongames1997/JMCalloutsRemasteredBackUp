using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using System.Windows.Forms;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Unauthorized Access - Richard's Majestic", CalloutProbability.Medium, "Reports of a trespasser", "Code 2", "LSPD")]

    public class UnauthorizedAccessMovieStudio : Callout
    {
        private Ped suspect;
        private Blip susBlip;
        private Vector3 spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(-1050.09f, -512.47f, 36.04f); // Richard's Majestic Movie Studio
            heading = 341.35f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 500f);
            CalloutMessage = "An individual refusing to leave";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered Log: Unauthorized Acces Movie Studio callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of a Individual Trespassing", "~b~Dispatch: The suspect has been spotted! Respond ~r~Code 2");

            suspect = new Ped(spawnpoint, heading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            CalloutInterfaceAPI.Functions.SendMessage(this, "A security officer reporting an individual trespassing on private property without proper access.");

            susBlip = suspect.AttachBlip();
            susBlip.Color = System.Drawing.Color.Yellow;
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

            if(Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;
                    suspect.Face(Game.LocalPlayer.Character);

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("~b~You: ~w~Excuse me, " + malefemale + ". Talk to me real quick.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect: ~w~Well, hello, Officer, what seems to be the problem?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~You: ~w~I have received a call from the security officer that you were trespassing without proper authorization. Explain to me about that.");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~ w~I have the right to be here. It's public property. I am with the ~y~'Cougars Gone Wild'~w~ cast. No, not that kind of Cougars Gone Wild, I'm talking about the animal. I don't need proper authorization.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You: ~w~Well, the secuirty officer said by the owner's policy that you are required to have some type of authorization to be here. So, you are looking at a trespassing citation/charge.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Fuck this, I'm gonna kill everybody! Fuck my life.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("Comversation Ended. Attempt to arrest the suspect. Save everyone's lives.");
                    }
                    if(counter == 8)
                    {
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        suspect.Inventory.GiveNewWeapon("WEAPON_GUSENBERG", 500, true);
                    }
                }
            }
            if (suspect.IsCuffed || suspect.IsDead || Game.LocalPlayer.Character.IsDead || !suspect.Exists())
            {
                End();
            }
        }

        public override void End()
        {
            if (suspect) suspect.Dismiss();
            if (susBlip) susBlip.Delete();
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Reports of an Individual Trespassing", "~b~You: Dispatch, We are ~g~CODE 4~w~! Show me back 10-8!");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();

            Game.LogTrivial("JM Callouts Remastered Log: Unauthorized Access Movie Studio is code 4");
        }
    }
}
