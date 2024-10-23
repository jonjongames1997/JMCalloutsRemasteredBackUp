﻿using Rage;
using LSPD_First_Response.Mod.API;
using System;
using System.Collections.Generic;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace JMCalloutsRemastered.Stuff
{
    class Citizen : Ped
    {
        //PED RELATED
        public string FullName { get; private set; }
        public string TimesStopped { get; private set; }

        public WantedInformation WantedInformation { get; private set; }
        public string Forename { get; private set; }
        public bool Wanted { get; private set; }
        public List<string> MedicalProblems { get; private set; }
        public string Gender { get; private set; }
        Random monke = new Random();

        //GENERAL
        private List<string> commonMedicalProblems = new List<string>()
        {
            "Multiple Lacerations all over upper body and face",
            "Gunshot wounds in the thigh, arm, neck",
            "Pneumothorax",
            "Shattered Femur",
            "Grade 3 Concussion",
            "3rd Degree Burns",
            "Broken Nose",
            "Broken Orbital",
            "Stab wounds in the stomach",
            "Bruise marks on wrists and forearms",
            "Celiac Disease", 
            "Scoliosis",
            "HIV",
            "Seizures",
            "Diabetes",
            "Lactose Intolorent",
            "Heart Murmor"
        };

        private List<string> commonMentalHealthProblems = new List<string>()
        {
            "Depression",
            "Generalised anxiety disorder",
            "Panic Disorder",
            "Obsessive-Compulsive Disorder",
            "Post-Traumatic Stress Disorder",
            "Dissociative Identity Disorder",
            "Paranoid Personality Disorder",
            "Schizophrenia",
            "Social Anxiety Disorder",
            "Nosocomephobia(Fear of hospitals)",
            "ADHD",
            "OCD",
            "ADD",
            "Bipolar Disorder",
        };

        private Persona pedPersona;

        /// <summary>
        /// constructors..you know how those work catYes
        /// </summary>
        /// <param name="ped"></param>
        /// 

        public Citizen() : base()
        {
            pedPersona = Functions.GetPersonaForPed(this);
            FullName = pedPersona.FullName;
            Forename = pedPersona.Forename;
            TimesStopped = pedPersona.TimesStopped.ToString();
            Wanted = pedPersona.Wanted;
            WantedInformation = pedPersona.WantedInformation;
            Gender = pedPersona.Gender.ToString();
            MedicalProblems = new List<string>();
        }

        public Citizen(Model modelName, Vector3 spawnPoint) : base(spawnPoint, modelName)
        {
            pedPersona = Functions.GetPersonaForPed(this);
            FullName = pedPersona.FullName;
            Forename = pedPersona.Forename;
            TimesStopped = pedPersona.TimesStopped.ToString();
            Wanted = pedPersona.Wanted;
            WantedInformation = pedPersona.WantedInformation;
            Gender = pedPersona.Gender.ToString();
            MedicalProblems = new List<string>();
        }
        public Citizen(Vector3 spawnPoint, float heading) : base(spawnPoint, heading)
        {
            pedPersona = Functions.GetPersonaForPed(this);
            FullName = pedPersona.FullName;
            Forename = pedPersona.Forename;
            TimesStopped = pedPersona.TimesStopped.ToString();
            Wanted = pedPersona.Wanted;
            WantedInformation = pedPersona.WantedInformation;
            Gender = pedPersona.Gender.ToString();
            MedicalProblems = new List<string>();
        }
        public Citizen(Vector3 spawnPoint, Model modelName, float heading) : base(modelName, spawnPoint, heading)
        {
            pedPersona = Functions.GetPersonaForPed(this);
            FullName = pedPersona.FullName;
            Forename = pedPersona.Forename;
            TimesStopped = pedPersona.TimesStopped.ToString();
            Wanted = pedPersona.Wanted;
            WantedInformation = pedPersona.WantedInformation;
            Gender = pedPersona.Gender.ToString();
            MedicalProblems = new List<string>();
        }
        public Citizen(Vector3 spawnPoint) : base(spawnPoint)
        {
            pedPersona = Functions.GetPersonaForPed(this);
            FullName = pedPersona.FullName;
            Forename = pedPersona.Forename;
            TimesStopped = pedPersona.TimesStopped.ToString();
            Wanted = pedPersona.Wanted;
            WantedInformation = pedPersona.WantedInformation;
            Gender = pedPersona.Gender.ToString();
            MedicalProblems = new List<string>();
        }

        /// <summary>
        /// sets medical problems for escaped suspect using the commonMedicalProblems list of strings
        /// </summary>
        public void setMedicalProblemsForEscapedSuspect()
        {
            MedicalProblems.Clear();
            Wanted = true;
            List<string> CMP = commonMedicalProblems;
            for (int i = 0; i < monke.Next(1, 3); i++)
            {
                int num = monke.Next(0, CMP.Count);
                MedicalProblems.Add(CMP[num]);
                CMP.RemoveRange(num, 1);
            }
        }

        /// <summary>
        ///sets medical problems for mentally ill suspect using the commonMentalHealthProblems list of strings 
        /// </summary>
        public void setMedicalProblemsForMentallyIllSuspect()
        {
            MedicalProblems.Clear();
            Wanted = false;
            List<string> CMHP = commonMentalHealthProblems;
            for (int i = 0; i < monke.Next(1, 3); i++)
            {
                int num = monke.Next(0, CMHP.Count);
                MedicalProblems.Add(CMHP[num]);
                CMHP.RemoveRange(num, 1);
            }
        }

        /// <summary>
        /// Easy way to print out medical problems in toString() method
        /// </summary>
        /// <param name="list"></param>
        /// <returns>Elements of arrays seperated by commas</returns>
        public static string ListToString(List<string> list) => string.Join(", ", list);

        override
        public string ToString()
        {
            return "~w~ Wanted Status: ~y~" + Wanted + "\n" + "~w~ Times Stopped: ~o~ " + TimesStopped + "\n" + "~w~ Medical Flags: ~r~" + "Recent Medical Perscriptions";
        }


    }
}
