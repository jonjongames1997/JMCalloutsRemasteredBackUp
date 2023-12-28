using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CalloutInterfaceAPI;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lockpicking - ULS", CalloutProbability.Medium, "A school security guard reporting a student locker break in", "Code 2", "LSPD")]

    public class LockPickingSchool : Callout
    {

        private Ped suspect;
        private Ped victim;
        private Vector3 spawnpoint;
        private Vector3 susSpawnPoint;
        private Blip suspectBlip;
        private Blip victimBlip;
        private string malefemale;
        private int counter;
        private float heading;
        private float susHeading;


        public override bool OnBeforeCalloutDisplayed()
        {
            spawnpoint = new Vector3(); // University of Los Santos //
            heading = 1509.25f;
            susSpawnPoint = new Vector3();
            susHeading = 1789.69f;
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "A unknown individual reported breaking into a student's locker");
            CalloutMessage = "A student locker break in";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("[JM Callouts Remastered Log]: Lockpicking - ULS callout has been accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Lockpicking - ULS", "~b~Dispatch~w~: Suspect has been spotted by another student! Respond ~r~Code 2~w~!");

            victim = new Ped("A_F_Y_RURMETH_01", spawnpoint, heading);
            victim.IsPersistent = true;
            victim.BlockPermanentEvents = true;

            suspect = new Ped(susSpawnPoint, susHeading);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;
            suspect.KeepTasks = true;
            suspect.Tasks.StandStill(500);
            suspect.AttachBlip();

            suspectBlip.Color = System.Drawing.Color.Red;

            victim.AttachBlip();
            victimBlip.Color = System.Drawing.Color.Aqua;
            victimBlip.IsRouteEnabled = true;

            if (victim.IsMale)
                malefemale = "Sir";
            else
                malefemale = "Ma'am";

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
            if (victim) victim.Delete();
            if (victimBlip) victimBlip.Delete();
            if (suspectBlip) suspectBlip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            if(Game.LocalPlayer.Character.DistanceTo(victim) <= 10f)
            {
                Game.DisplayHelp("Press ~y~E~w~ to interact with the victim", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        victim.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~Player~w~: Hello there, " + malefemale + ". Can you explain to me what is going on?");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: Officer, there was this suspicious person trying to break into my locker for no reason. (1/3)");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: I went to go get the security guard and he busted him then chased him out, the guy pulled out a gun then fatally shot the poor security guard. (2/3)");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: The security guard was loved by our beloved staff and other students. He was our best security guard we ever had.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Do you know this person? What kind of gun did they have? Did you see they had a weapon on them?");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: I never met this person in my life. They had a handgun pistol and yes I did see them with the weapon after I got the security guard.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Do you need any medical assistance? Do you mind sit tight for me while I'll talk to them?");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~y~Victim~w~: No, I'm fine, officer and sure thing, officer.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("Conversation with victim has ended! Talk to the ~r~Suspect~w~.");
                    }
                    if(counter == 10)
                    {
                        Game.DisplaySubtitle("~b~You~w~: Excuse me, " + malefemale + ". Stop right there! Don't reach for anything!");
                    }
                    if(counter == 11)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Die, you motherfucker, die! I will not be taken in alive, copper!");
                        suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 500, true);
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    }
                }
            }

            if (Game.LocalPlayer.IsDead) End();
            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (suspect && suspect.IsDead) End();
            if (suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)) End();

            base.Process();
        }

        public override void End()
        {


            base.End();
        }

    }
}
