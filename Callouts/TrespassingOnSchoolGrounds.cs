using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.Callouts;
using CalloutInterfaceAPI;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using Rage;
using Rage.Native;
using System.Drawing;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On School Property", CalloutProbability.Medium, "An individual trespassing on school property", "Code 2", "LSPD")]

    public class TrespassingOnSchoolGrounds : Callout
    {

    }
}
