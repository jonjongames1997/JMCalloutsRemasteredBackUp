using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using System.Windows.Forms;
using JMCalloutsRemastered.Stuff;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Dead Bpdy", CalloutProbability.High, "Reports of a dead body", "Code 3", "LSPD")]

    public class DeadBody : Callout
    {

    }
}
