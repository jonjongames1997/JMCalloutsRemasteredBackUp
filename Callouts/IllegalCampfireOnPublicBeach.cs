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

    [CalloutInterface("Illegal Campfire On Public Beach", CalloutProbability.High, "An individual started a campfire on Vespucci Beach.", "Code 3", "LSPD")]

    public class IllegalCampfireOnPublicBeach : Callout
    {

        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private float heading;
        private int counter;
        private string malefemale;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = new Vector3(-1537.564f, -1214.748f, 1.887064f); // Campfire Spawns at night //
            heading = 133.8988f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 1000f); // Blips the area of the callout //
            CalloutInterfaceAPI.Functions.SendMessage(this, "Vespucci Beach Security reporting an individual starting a campfire on the beach. Suspect refused to put out the fire as requested by security.");
            CalloutMessage = "Individual started an illegal campfire on the beach!"; // Brief description of the call //
            CalloutPosition = Spawnpoint; // Gives the position of where the callout is located at //

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Suspect = new Ped(Spawnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.Chocolate;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press 'E' to speak with the ~r~suspect.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        Game.DisplaySubtitle("Player: Good evening" + malefemale + ", May I speak with you for a moment?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Good evening to you as well officer. What seems to be the problem?");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("Player: Are you aware of the City Wide Ban on campfires on public beaches?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ No, officer. Is there really a ban on campfires on beaches?");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("Player: Yes there is. The city said there is a a heat wave that's in effect until further notice and it's a high risk of causing wild fires.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ RISK OF WILDFI.... That's asinine! This is sand. Are they mentally retarded? They need to go back to science class.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("Player: " + malefemale + ", I'm gonna ask you to put out the fire and leave or I'll place you under arrest for failure to comply with a lawful order.");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect:~w~ Fuck you, Dick Tickler! I'll do my campfires any time anywhere I want. It's my right as a US Citizen.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("Conversation has ended!");
                        Game.DisplayNotification("Arrest the suspect, Officer.");
                        Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        Suspect.Inventory.GiveNewWeapon("WEAPON_MACHETE", 500, true);
                    }
                }
            }

            if (Settings.ActiveAIBackup)
            {
                LSPD_First_Response.Mod.API.Functions.RequestBackup(Spawnpoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.LocalUnit);
                LSPD_First_Response.Mod.API.Functions.RequestBackup(Spawnpoint, LSPD_First_Response.EBackupResponseType.Code2, LSPD_First_Response.EBackupUnitType.PrisonerTransport);
            }
            else { Settings.ActiveAIBackup = false; }

            if (Suspect.IsCuffed || Suspect.IsDead || Game.LocalPlayer.Character.IsDead || !Suspect.Exists())
            {
                End();
            }

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


            Game.LogTrivial("JM Callouts Remastered - Illegal Campfire On Public Beach is Code 4!");

        }
    }
}
