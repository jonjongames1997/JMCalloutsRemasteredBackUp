using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting;
using JMCalloutsRemastered.Stuff;
using CalloutInterfaceAPI;
using System.Drawing;
using Rage;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Lost Pet", CalloutProbability.Medium, "A citizen's report of a lost animal", "Code 2", "LSPD")]

    public class LostPet : Callout
    {

    }
}
