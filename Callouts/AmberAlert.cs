using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using System.Drawing;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Amber Alert", CalloutProbability.High, "A citizen's report of a missing person", "Code 1", "LSPD")]

    public class AmberAlert : Callout
    {

    }
}
