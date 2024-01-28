using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using JMCalloutsRemastered;
using JMCalloutsRemastered.Stuff;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Person On The Highway", CalloutProbability.Medium, "A citizen's report of an individual on the freeway", "Code 2", "SAHP")]

    public class PersonOnTheHighway : Callout
    {
        private string[] wepList = new string[] { "WEAPON_KNIFE", "WEAPON_BAT", "WEAPON_DAGGER", "WEAPON_GOLFCLUB", "WEAPON_HAMMER", "WEAPON_HATCHET" };
        private string[] pedList = new string[] { "a_m_m_afriamer_01", "ig_abigail", "ig_amandatownley", "csb_anita", "ig_ashley", "g_f_y_ballas_01", "a_f_m_bodybuild_01", "a_f_y_eastsa_03", "ig_maryann" };
        private Blip blip;
        private Ped suspect;
        private Vector3 spawnpoint;
        private int counter;
        private string malefemale;


        public override bool OnBeforeCalloutDisplayed()
        {
            Random random = new Random();
            List<Vector3> list = new List<Vector3>
            {
                new Vector3(),
                new Vector3(),
                new Vector3(),
                new Vector3(),
            };
            spawnpoint = LocationChooser.chooseNearestLocation(list);
            ShowCalloutAreaBlipBeforeAccepting(spawnpoint, 100f);
            CalloutInterfaceAPI.Functions.SendMessage(this, "Reports of an individual on the highway");
            CalloutMessage = "A citizen's reporting an individual on the highway. Respond Code 2.";
            CalloutPosition = spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Game.LogTrivial("JM Callouts Remastered [LOG]: Person on the highway callout accepted!");
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~y~Person On The Highway", "~b~Dispatch~w~: The suspect has been spotted. Respond ~r~Code 2~w~.");
            Game.DisplayHelp("Press ~y~END~w~ at anytime to end the callout", false);

            suspect = new Ped(pedList[new Random().Next((int)pedList.Length)], spawnpoint, 0f);
            suspect.IsPersistent = true;
            suspect.BlockPermanentEvents = true;

            blip = suspect.AttachBlip();
            blip.Color = System.Drawing.Color.Gold;
            blip.IsRouteEnabled = true;

            suspect.Tasks.Wander();
            suspect.KeepTasks = true;
            suspect.Inventory.GiveNewWeapon("WEAPON_UNARMED", 500, true);

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
            if (blip) blip.Delete();

            base.OnCalloutNotAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(Game.LocalPlayer.Character.DistanceTo(suspect) <= 10f)
            {

                Game.DisplayHelp("Press ~y~E~w~ to interact with the ~r~suspect~w~. Approach with ~y~CAUTION~w~.", false);

                if (Game.IsKeyDown(System.Windows.Forms.Keys.E))
                {
                    counter++;

                    if(counter == 1)
                    {
                        suspect.Face(Game.LocalPlayer.Character);
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Hey there, " + malefemale + ". What goin' on? Come talk to me real quick.");
                    }
                    if(counter == 2)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Hi, Officer, My Uber driver kicked me out his car for no reason.");
                    }
                    if(counter == 3)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Why would your driver do that? What was the reason?");
                    }
                    if(counter == 4)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: The driver is very picky on who they pick up. I find that as discrimination.");
                    }
                    if(counter == 5)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: Ok, you didn't call a taxi? If you are being discriminated, you can contact Uber and file a complaint with them cause that's on the company. If the driver was threatening you or anything criminal, that's when we step in.");
                    }
                    if(counter == 6)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: No I didn't call a taxi. I prefer to walk. I know it's against the law to do that but nobody will stop and help me.");
                    }
                    if(counter == 7)
                    {
                        Game.DisplaySubtitle("~b~Officer (You)~w~: I am more than happy to call you a taxi. I don't want to put you in cuffs. Let me call you a taxi and get you home safe. I am worried for your safety,");
                    }
                    if(counter == 8)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Thank you, officer.");
                    }
                    if(counter == 9)
                    {
                        Game.DisplaySubtitle("~r~Suspect~w~: Death to Los Santos, Motherfucka!");
                        suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
                        suspect.KeepTasks = true;
                        suspect.Inventory.GiveNewWeapon(wepList[new Random().Next((int)wepList.Length)], 500, true);
                        suspect.Armor = 500;
                    }
                }

                if (Game.LocalPlayer.Character.IsDead) End();
                if (Game.IsKeyDown(Settings.EndCall)) End();
            }
        }

        public override void End()
        {
            if (suspect.Exists())
            {
                suspect.Dismiss();
            }
            if (blip.Exists())
            {
                blip.Delete();
            }

            base.End();


        }
    }
}
