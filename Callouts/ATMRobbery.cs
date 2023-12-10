using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using CAPI = CalloutInterfaceAPI;
using Rage;
using Rage.Native;
using System.Drawing;
using LSPD_First_Response.Engine;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;

namespace JMCalloutsRemastered.Callouts
{

    [CAPI.CalloutInterface("ATM Robbery", CalloutProbability.VeryHigh, "A citizen reported that the suspect might have a weapon", "Code 3", "LSPD")]
    public class ATMRobbery : Callout
    {

    }
}
