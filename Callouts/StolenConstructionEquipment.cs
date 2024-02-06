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
using JMCalloutsRemastered.Callouts;
using LSPD_First_Response.Mod.API;
using System.Threading;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Stolen Construction Equipment", CalloutProbability.Medium, "Reports of stolen construction equipment.", "Code 3", "LSPD")]


    public class StolenConstructionEquipment : Callout
    {

    }
}
