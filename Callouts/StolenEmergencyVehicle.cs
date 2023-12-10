using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalloutInterfaceAPI;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using Rage;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Stolen Emergency Vehicle", CalloutProbability.Medium, "Reports of a stolen emergency vehicle", "CODE 3", "LSPD")]

    public class StolenEmergencyVehicle : Callout
    {

    }
}
