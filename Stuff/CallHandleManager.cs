﻿using Rage;
using LSPD_First_Response.Mod.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.Linq;
using Rage.Native;

namespace JMCalloutsRemastered.Stuff
{
    // Shoutout to Yobbin for his Open Source Code as a guide. // //https://github.com/YobB1n/YobbinCallouts/blob/master/CallHandler.cs

    class CallHandleManager
    {
        public static Vector3 SpawnPoint; //SpawnPoint that will be returned by GetSpawnPoint functions
        public static bool locationReturned = true; //if a location (house, hospital, store, etc) was returned
        private static int count;
        private static string[] VehicleModels; //array of vehicle models for vehicle spawner
        static Random monke = new Random();
        public static bool arrested;

        public static bool SoundPlayed; //if Sound is Played by a sound-related helper function

        private static string[,] FemaleCopAnim = new string[,] {
            {"amb@world_human_cop_idles@female@base", "base"},
            {"amb@world_human_cop_idles@female@idle_a", "idle_a" },
            {"amb@world_human_cop_idles@female@idle_a", "idle_b" },
            {"amb@world_human_cop_idles@female@idle_a", "idle_c" },
            {"amb@world_human_cop_idles@female@idle_b", "idle_d" },
            {"amb@world_human_cop_idles@female@idle_b", "idle_e" },
        };
        private static string[,] MaleCopAnim = new string[,] {
            {"amb@world_human_cop_idles@male@base", "base"},
            {"amb@world_human_cop_idles@male@idle_a", "idle_a" },
            {"amb@world_human_cop_idles@male@idle_a", "idle_b" },
            {"amb@world_human_cop_idles@male@idle_a", "idle_c" },
            {"amb@world_human_cop_idles@male@idle_b", "idle_d" },
            {"amb@world_human_cop_idles@male@idle_b", "idle_e" },
        };
        private static string[,] FemaleRandoAnim = new string[,] {
            {"amb@world_human_hang_out_street@female_arm_side@idle_a", "idle_a"},
            {"amb@world_human_hang_out_street@female_arm_side@idle_a", "idle_b"},
            {"amb@world_human_hang_out_street@female_arm_side@idle_a", "idle_c"},
            {"amb@world_human_hang_out_street@female_arms_crossed@idle_a", "idle_a"},
            {"amb@world_human_hang_out_street@female_arms_crossed@idle_a", "idle_b"},
            {"amb@world_human_hang_out_street@female_arms_crossed@idle_a", "idle_c"},
        };
        
        private static string[,] MaleRandoAnim = new string[,] {
            {"amb@world_human_hang_out_street@male_a@base", "base"},
            {"amb@world_human_hang_out_street@male_b@base", "base"},
            {"amb@world_human_hang_out_street@male_c@base", "base"},
        };

        //ArrayList of houses all around the map
        public static ArrayList HouseList = new ArrayList() { new Vector3(240.7677f, -1687.701f, 29.6996f), new Vector3(100.6926f, -1914.058f, 21.03957f), new Vector3(288.6435f, -1792.515f, 28.08904f),
        new Vector3(1250.818f, -1734.568f, 52.03207f), new Vector3(1354.907f, -1694.046f, 60.49123f), new Vector3(1362.024f, -1568.026f, 56.34648f), new Vector3(1221.362f, -668.7222f, 63.49313f),
        new Vector3(1010.55f, -418.9665f, 64.95395f), new Vector3(-1101.879f, -1536.912f, 4.579572f), new Vector3(-977.2473f, -1091.995f, 4.222562f), new Vector3(-1064.605f, -1057.521f, 6.411661f),
        new Vector3(-1031.352f, -903.0417f, 3.691091f), new Vector3(-1950.582f, -544.11022f, 14.7255f), new Vector3(-1901.605f, -585.9387f, 11.86937f), new Vector3(-1777.128f, -701.4404f, 10.52536f),
        new Vector3(-817.6935f, 177.9567f, 72.22254f), new Vector3(-896.5508f, -5.058554f, 43.79892f), new Vector3(-1106.531f, 421.4244f, 75.68616f), new Vector3(-933.4481f, 472.059f, 85.12269f),
        new Vector3(-678.85f, 512.1063f, 113.526f), new Vector3(-565.5161f, 525.6989f, 110.2012f), new Vector3(-972.2734f, 752.2137f, 176.3808f), new Vector3(-305.1205f, 431.0618f, 110.4823f),
        new Vector3(260.885f, 22.27959f, 88.12721f), new Vector3(1975.007f, 3816.095f, 33.42553f), new Vector3(1862.327f, 3853.849f, 36.27155f), new Vector3(1808.881f, 3907.963f, 33.73134f),
        new Vector3(1544.591f, 3721.3f, 34.62653f), new Vector3(1725.54f, 4642.25f, 43.87547f), new Vector3(1966.98f, 4634.148f, 41.1016f), new Vector3(-218.4349f, 6453.148f, 31.19829f),
        new Vector3(-365.8479f, 6341.065f, 29.84357f), new Vector3(-374.104f, 6190.625f, 31.72954f)};

        //Arraylist of all the hospitals all around the map
        public static ArrayList HospitalList = new ArrayList() { new Vector3(361.0359f, -585.4946f, 28.8267f), new Vector3(356.689f, -597.6279f, 28.78184f), new Vector3(-449.401f, -347.7617f, 34.50174f),
        new Vector3(-447.8303f, -334.3066f, 34.50184f), new Vector3(295.7652f, -1447.524f, 29.966f), new Vector3(341.2158f, -1398.245f, 32.50923f), new Vector3(1838.992f, 3673.217f, 34.27671f),
        new Vector3(1815.018f, 3679.552f, 34.27674f), new Vector3(-247.249f, 6330.457f, 32.42619f), new Vector3(1152.5f, -1526.501f, 34.84344f), new Vector3(1161.176f, -1536.283f, 39.39494f)};

        //Arraylist of all the police stations all around the map
        public static ArrayList PoliceStationList = new ArrayList() { new Vector3(638.5f, 1.75f, 82.8f), new Vector3(826.8f, -1290f, 28.24f), new Vector3(-561.65f, -131.65f, 38.21f),
        new Vector3(-1108.18f, -845.18f, 19.32f),new Vector3(450.0654f, -993.0596f, 30f),new Vector3(360.97f, -1584.70f, 29.29f),new Vector3(1848.73f, 3689.98f, 34.27f),new Vector3(-448.22f, 6008.23f, 31.72f),
        new Vector3(379.31f, 792.06f, 190.41f),new Vector3(-864.61f, -2408.92f, 14.03f),new Vector3(1846.49f, 2585.95f, 45.67f)};

        public static ArrayList StoreList = new ArrayList()
        {
            new Vector3(-47.29313f, -1758.671f, 29.42101f),
            new Vector3(289f, -1267f, 29.44f),
            new Vector3(818f, -1039f, 26.75f),
            new Vector3(289f, -1267f, 29.44f),
            new Vector3(1211.76f, -1390f, 35.37f),
            new Vector3(1164.94f, -324.3139f, 69.22092f),
            new Vector3(-530f, -1220f, 18.45f),
            new Vector3(-711f, -917f, 19.21f),
            new Vector3(-2073f, -327f, 13.32f),
            new Vector3(527f, -151f, 57.46f),
            new Vector3(643f, 264.4f, 103.3f),
            new Vector3(1959.956f, 3740.31f, 32.34f),
            new Vector3(-1442f, -1993f, 13.164f),
            new Vector3(-93f, 6410.87f, 31.65f),
            new Vector3(1696.867f, 4923.803f, 42.06f),
            new Vector3(2557.269f, 380.7113f, 108.6229f),
            new Vector3(-3038f, 483.778f, 7.91f),
            new Vector3(-2545.63f, 2316.986f, 33.21579f),
        };

        public static Color[] colors = new Color[]
        {
            Color.Brown,
            Color.Red,
            Color.DarkRed,
            Color.Green,
            Color.LightGreen,
            Color.DarkGreen,
            Color.Blue,
            Color.LightBlue,
            Color.SkyBlue,
            Color.DarkBlue,
            Color.Silver,
            Color.Yellow,
            Color.Orange,
            Color.DarkOrange,
            Color.Black,
            Color.White,
            Color.Gray,
            Color.LightGray,
            Color.DarkGray,
            Color.DarkCyan,
            Color.LightCyan,
            Color.DarkMagenta,
            Color.DarkGreen,
        };

        /// <summary>
        /// plays a dialgoue in a List<string> format. Optionally, specify a ped and animation to use while the dialogue is playing.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="animped"></param>
        /// <param name="animdict"></param>
        /// <param name="animname"></param>
        /// <param name="animspeed"></param>
        /// <param name="animflag"></param>
        public static void Dialogue(List<string> dialogue, Ped animped = null, String animdict = "missfbi3_party_d", String animname = "stand_talk_loop_a_male1", float animspeed = -1, AnimationFlags animflag = AnimationFlags.Loop)
        {
            //test (in this if)
            if (animped != null && animped.Exists())
            {
                animped.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180).WaitForCompletion(500);
                //if ((int)animped.Heading - 180.CompareTo((int)Game.LocalPlayer.Character.Heading) > 15)
                //{
                //    animped.Tasks.AchieveHeading(Game.LocalPlayer.Character.Heading - 180).WaitForCompletion(100);
                //}
                //else
                //{
                //    animped.Heading = Game.LocalPlayer.Character.Heading - 180;
                //}
                //GameFiber.Wait(69);
            }

            count = 0;
            while (count < dialogue.Count)
            {
                GameFiber.Yield();
                if (Game.IsKeyDown(Settings.Dialog))
                {
                    if (animped != null && animped.Exists())
                    {
                        try
                        {
                            animped.Tasks.PlayAnimation(animdict, animname, animspeed, animflag);
                        }
                        catch (Exception) { }
                    }
                    Game.DisplaySubtitle(dialogue[count]);
                    count++;
                }
            }
        }

        /// <summary>
        /// Plays an idle animation, depending on if the Ped is a cop or not.
        /// </summary>
        /// <param name="ped"></param>
        /// <param name="iscop"></param>
        public static void IdleAction(Ped ped, bool iscop)
        {
            if (ped != null && ped.Exists())
            {
                if (iscop)
                {
                    if (ped.IsFemale)
                    {
                        int animation = monke.Next(0, FemaleCopAnim.Length / 2);
                        ped.Tasks.PlayAnimation(FemaleCopAnim[animation, 0], FemaleCopAnim[animation, 1], -1, AnimationFlags.Loop);
                    }
                    else
                    {
                        int animation = monke.Next(0, MaleCopAnim.Length / 2);
                        //Game.LogTrivial("YOBBINCALLOUTS: There are "+MaleCopAnim.Length+"animations");
                        //Game.LogTrivial(MaleCopAnim[animation, 0]);
                        //Game.LogTrivial(MaleCopAnim[animation, 1]);
                        ped.Tasks.PlayAnimation(MaleCopAnim[animation, 0], MaleCopAnim[animation, 1], -1, AnimationFlags.Loop);
                    }
                }
                else
                {
                    if (ped.IsFemale)
                    {
                        int animation = monke.Next(0, FemaleRandoAnim.Length / 2);
                        ped.Tasks.PlayAnimation(FemaleRandoAnim[animation, 0], FemaleRandoAnim[animation, 1], -1, AnimationFlags.Loop);
                    }
                    else
                    {
                        int animation = monke.Next(0, MaleRandoAnim.Length / 2);
                        ped.Tasks.PlayAnimation(MaleRandoAnim[animation, 0], MaleRandoAnim[animation, 1], -1, AnimationFlags.Loop);
                    }
                }
            }
        }

        /// <summary>
        /// Spawns a vehicle at the position and heading.
        /// </summary>
        /// <param name="SpawnPoint"></param>
        /// <param name="Heading"></param>
        /// <param name="persistent"></param>
        /// <returns></returns>
        public static Vehicle SpawnVehicle(Vector3 SpawnPoint, float Heading, bool persistent = true)
        {
            VehicleModels = new string[63] {"asbo", "blista", "dilettante", "panto", "prairie", "cogcabrio", "exemplar", "f620", "felon", "felon2", "jackal", "oracle", "oracle2", "sentinel", "sentinel2",
            "zion", "zion2", "baller", "baller2", "baller3", "cavalcade", "fq2", "granger", "gresley", "habanero", "huntley", "mesa", "radi", "rebla", "rocoto", "seminole", "serrano", "xls", "asea", "asterope",
            "emporor", "fugitive", "ingot", "intruder", "premier", "primo", "primo2", "regina", "stanier", "stratum", "surge", "tailgater", "washington", "bestiagts", "blista2", "buffalo", "schafter2", "euros",
            "sadler", "bison", "bison2", "bison3", "burrito", "burrito2", "minivan", "minivan2", "paradise", "pony"};
            int model = monke.Next(0, VehicleModels.Length);
            Game.LogTrivial("JMCALLOUTS: VEHICLESPAWNER: Vehicle Model is " + VehicleModels[model]);
            var veh = new Vehicle(VehicleModels[model], SpawnPoint, Heading);
            if (persistent) veh.IsPersistent = true; //vehicle is marked as persistent by default
            return veh;
        }

        /// <summary>
        /// knock on the door of a house and wait for response (with doorbell audio)
        /// </summary>
        /// <param name="doorlocation"></param>
        /// <param name="resident"></param>
        /// <param name="residentmodel"></param>
        public static void OpenDoor(Vector3 doorlocation, Ped resident = null, String residentmodel = "")
        {
            Game.DisplayHelp("Press ~y~" + Settings.MainInteractionKey + "~w~ to ~b~Ring~w~ the Doorbell.");
            while (!Game.IsKeyDown(Settings.MainInteractionKey)) GameFiber.Wait(0);
            Doorbell();
            GameFiber.Wait(2500);
            Game.LocalPlayer.HasControl = false;
            Game.FadeScreenOut(1500, true);
            if (resident != null) //if you want to spawn a resident
            {
                if (residentmodel != "") resident = new Ped(residentmodel, doorlocation, Game.LocalPlayer.Character.Heading - 180);
                else resident = new Ped(doorlocation, Game.LocalPlayer.Character.Heading - 180);
                resident.Heading = Game.LocalPlayer.Character.Heading - 180; //might not be needed
                IdleAction(resident, false);
            }
            GameFiber.Wait(1500);
            Game.FadeScreenIn(1500, true);
            Game.LocalPlayer.HasControl = true;
        }

        /// <summary>
        /// plays a sound at any specific file location.
        /// </summary>
        /// <param name="SoundLocation"></param>
        public static void PlaySound(string SoundLocation)
        {
            try
            {
                Game.LogTrivial("JMCALLOUTS: PLAYSOUNDHANDLER:" + SoundLocation + " - SOUND PLAY");
                System.Media.SoundPlayer sound = new System.Media.SoundPlayer();
                sound.SoundLocation = SoundLocation;
                GameFiber.StartNew(delegate
                {
                    try
                    {
                        sound.Load();
                        sound.Play();
                        GameFiber.Wait(4500);
                        sound.Stop();
                        SoundPlayed = true;
                    }
                    catch (System.IO.FileNotFoundException) //most common error due to user error not installing sound file properly
                    {
                        Game.DisplayNotification("The ~b~Audio File~w~ for ~o~JMCallouts~w~ is ~r~not Installed Properly.~w~ Please ~b~Reinstall~w~ the Plugin Properly.");
                        Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT ==========");
                        Game.LogTrivial("AUDIO FILE FOR JMCALLOUTS NOT INSTALLED. PLEASE REINSTALL THE PLUGIN PROPERLY.");
                        Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT ==========");
                        SoundPlayed = false;
                    }
                });
            }
            catch (System.Threading.ThreadAbortException) { } //this error doesn't really matter in my experience so I don't log it
            catch (System.IO.FileNotFoundException)
            {
                Game.DisplayNotification("The ~b~Audio File~w~ for ~o~JMCallouts~w~ is ~r~not Installed Properly.~w~ Please ~b~Reinstall~w~ the Plugin Properly.");
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT ==========");
                Game.LogTrivial("AUDIO FILE FOR JMCALLOUTS NOT INSTALLED. PLEASE REINSTALL THE PLUGIN PROPERLY.");
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT ==========");
                SoundPlayed = false;
            }
            catch (Exception e) //any other error
            {
                Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
                string error = e.ToString();
                Game.LogTrivial("ERROR: " + error);
                Game.LogTrivial("IN - JMCALLOUTS SOUND PLAYER");
                Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~o~JMCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
                //Game.DisplayNotification("Error: ~r~" + error);
                Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT ==========");
                SoundPlayed = false;
            }
        }
        //just for my callouts, plays a doorbell sound.
        public static void Doorbell()
        {
            int model = monke.Next(0, 3);
            if (model == 0) PlaySound(@"lspdfr\audio\scanner\JMCallouts Audio\RING_DOORBELL_CHIME_01.wav");
            else if (model == 2) PlaySound(@"lspdfr\audio\scanner\JMCallouts Audio\RING_DOORBELL_CHIME_02.wav");
            else PlaySound(@"lspdfr\audio\scannerJMCallouts Audio\RING_DOORBELL_CHIME_01.wav");
        }
        /// <summary>
        /// wait the active GameFiber until the suspect no longer exists, is killed, or is arrested.
        /// </summary>
        /// <param name="Suspect"></param>
        public static void SuspectWait(Ped Suspect) //test this
        {
            Game.LogTrivial("YOBBINCALLOUTS: Waiting the active GameFiber until the suspect is killed or arrested.");
            while (Suspect.Exists())
            {
                GameFiber.Yield();
                if (!Suspect.Exists() || Suspect.IsDead || Functions.IsPedArrested(Suspect)) break;
            }
            if (Suspect.IsAlive) //test all this (STP )
            {
                Game.LogTrivial("JMCALLOUTS: Suspect is alive and therefore under arrest.");
                Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest.");
            }
            else if (Suspect.Exists() && Functions.IsPedArrested(Suspect))
            {
                Game.LogTrivial("JMCALLOUTS: Suspect is alive and therefore under arrest.");
                Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest.");
            }
            else
            {
                Game.LogTrivial("JMCALLOUTS: Suspect is dead.");
                GameFiber.Wait(1000); Game.DisplayNotification("Dispatch, Suspect is ~r~Dead.");
            }
            GameFiber.Wait(2000);
            Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
            GameFiber.Wait(2000);
        }

        /// <summary>
        /// assign a blip that will be attached to an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="blipcolor"></param>
        /// <param name="scale"></param>
        /// <param name="name"></param>
        /// <param name="route"></param>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public static Blip AssignBlip(Entity entity, Color blipcolor, float scale = 1f, string name = "", bool route = false, float intensity = 1f)
        {
            try
            {
                if (!entity.Exists()) return null;
                Blip blip = entity.AttachBlip();

                if (blipcolor == Color.Blue) blip.IsFriendly = true;
                else if (blipcolor == Color.Red) blip.IsFriendly = false;
                else blip.Color = blipcolor;
                blip.Scale = scale;
                if (name != "") blip.Name = name;
                else if (blipcolor == Color.Blue) blip.Name = "Citizen";
                else if (blipcolor == Color.Red) blip.Name = "Suspect";
                blip.IsRouteEnabled = route;
                blip.Alpha = intensity;
                return blip;
            }
            catch (Exception e)
            {
                Game.LogTrivial("JMCALLOUTS: Error assigning blip. Error: " + e);
                return null;
            }
        }
        /// <summary>
        /// Chooses a random location in list between mindistance and maxdistance.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="maxdistance"></param>
        /// <param name="mindistance"></param>
        public static void locationChooser(ArrayList list, float maxdistance = 600f, float mindistance = 25f)
        {
            ArrayList closeLocations = new ArrayList();
            for (int i = 1; i < list.Count; i++)
            {
                float distance = Vector3.Distance(Game.LocalPlayer.Character.Position, (Vector3)list[i]);
                if (distance <= maxdistance && distance >= mindistance)
                {
                    closeLocations.Add(list[i]); //kind of like bs racket recursion lmao
                }
            }
            if (closeLocations.Count == 0)
            {
                Game.LogTrivial("YOBBINCALLOUTS: Spawn Point not found.");
                locationReturned = false;
            }
            else
            {
                SpawnPoint = (Vector3)closeLocations[monke.Next(0, closeLocations.Count)];
                locationReturned = true;
                Game.LogTrivial("JMCALLOUTS: Spawn Point found successfully.");
                Game.LogTrivial("JMCALLOUTS: Spawn Point found at " + SpawnPoint + " in " + Functions.GetZoneAtPosition(SpawnPoint).RealAreaName);
            }
        }

        public static bool FiftyFifty()
        {
            int num = monke.Next(0, 2);
            if (num == 0) { return false; } else { return true; }
        }

        /// <summary>
        /// Returns a random number out of options, or a random number between min and max if specified. Specifiy 0 in options to use RNG for min and max.
        /// </summary>
        public static int RNG(int options = 0, int min = 0, int max = 0)
        {
            if (min == 0 && max == 0) max = options;
            int num = monke.Next(min, max);
            Game.LogTrivial("JMCALLOUTS: RNG value is " + num);
            return num;
        }

        /// <summary>
        /// Displays a notification with relevant vehicle information.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="ped"></param>
        /// <param name="pedstatus"></param>
        //test this later
        public static void VehicleInfo(Vehicle vehicle, Citizen ped)
        {
            if (vehicle.Exists() && ped.Exists())
            {
                Functions.SetVehicleOwnerName(vehicle, ped.FullName);
                var personaarray = new string[4];
                personaarray[0] = "~n~~w~Registered to: ~y~" + ped.Forename;
                personaarray[1] = "~n~~w~Ped Info: ~y~" + ped.WantedInformation;
                personaarray[2] = "~n~~w~Plate: ~y~" + vehicle.LicensePlate;
                personaarray[3] = "~n~~w~Color: ~y~" + vehicle.PrimaryColor.Name;
                var persona = string.Concat(personaarray);
                //test icon later
                Game.DisplayNotification("mpcarhud", "leaderboard_car_colour_icon", "~g~Vehicle Description", "~b~" + vehicle.Model.Name, persona);
            }
        }

        /// <summary>
        /// Creates a pursuit with Suspects.
        /// </summary>
        public static void CreatePursuit(LHandle pursuit, bool wait = true, bool audio = true, bool backup = false, params Ped[] suspects)
        {
            try
            {
                //none of these three lines should be problematic anymore
                Functions.ForceEndCurrentPullover();
                pursuit = Functions.CreatePursuit();
                Functions.SetPursuitIsActiveForPlayer(pursuit, true);

                foreach (Ped Suspect in suspects)
                {
                    if (Suspect.Exists()) Functions.AddPedToPursuit(pursuit, Suspect);
                }
                Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: Started Pursuit with " + suspects.Length + " Suspects.");

                if (audio)
                {
                    GameFiber.Wait(1500);
                    try { Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01"); }
                    catch (System.Threading.ThreadAbortException) { Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: UNABLE TO PLAY SOUND - THREADBORTEXCEPTION CAUGHT."); }
                    if (backup)
                    {
                        try { Functions.RequestBackup(Game.LocalPlayer.Character.Position, LSPD_First_Response.EBackupResponseType.Code3, LSPD_First_Response.EBackupUnitType.LocalUnit); }
                        catch { }
                    }
                }

                if (wait && suspects.Length == 1)
                {
                    Ped suspect = suspects[0];
                    while (Functions.IsPursuitStillRunning(pursuit)) { GameFiber.Wait(0); }
                    while (suspect.Exists())
                    {
                        GameFiber.Yield();
                        if (!suspect.Exists() || suspect.IsDead || Functions.IsPedArrested(suspect)) break;
                    }
                    if (suspect.IsAlive) //test all this (STP )
                    {
                        Game.DisplayNotification("Dispatch, the Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
                        Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: Suspect is under arrest.");
                        arrested = true;
                    }
                    else
                    {
                        GameFiber.Wait(1000); Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
                        Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: Suspect is killed.");
                        arrested = false;
                    }
                    GameFiber.Wait(2000);
                    Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
                    GameFiber.Wait(1500);
                }
                else
                {
                    while (Functions.IsPursuitStillRunning(pursuit)) { GameFiber.Wait(0); }
                }
                Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: Pursuit over.");
                Game.LogTrivial("JMCALLOUTS: PURSUITHANDLER: Suspect is arrested: " + arrested);
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT - PURSUITHANDLER THREADABORT ==========");
                string error = e.ToString();
                Game.LogTrivial("ERROR: " + error);
                Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT - PURSUITHANDLER THREADABORT ==========");
            }
            catch (Exception e)
            {
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT - PURSUITHANDLER ==========");
                string error = e.ToString();
                Game.LogTrivial("ERROR: " + error);
                Game.LogTrivial("========== JMCALLOUTS: ERROR CAUGHT - PURSUITHANDLER ==========");
            }
        }
        //this function is stupid
        /// <summary>
        /// Gets a random vehicle colour, and sets the specified vehicle to that color. Very stupid function, make sure the vehicle is not within eyesight of the player lmao
        /// </summary>
        public static string GetSetVehicleColor(Vehicle vehicle)
        {
            Game.LogTrivial("YOBBINCALLOUTS: GETTING NAME OF COLOR.");
            int colorno = monke.Next(0, colors.Length);
            Game.LogTrivial("YOBBINCALLOUTS: VEHICLESPAWNER: Color is " + colors[colorno]);
            if (vehicle.Exists()) vehicle.PrimaryColor = colors[colorno];
            string VehicleColor = colors[colorno].Name.ToString();

            return VehicleColor;
        }
    }
}
