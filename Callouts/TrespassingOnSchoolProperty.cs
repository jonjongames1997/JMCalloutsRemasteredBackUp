using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Engine.Scripting;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using JMCalloutsRemastered.Stuff;
using JMCalloutsRemastered.Callouts;
using System.Drawing;
using System.Windows.Forms;
using CalloutInterfaceAPI;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Trespassing On School Property", CalloutProbability.Medium, "Reports of an individual refusing to leave", "CODE 2", "LSPD")]

    public class TrespassingOnSchoolProperty : Callout
    {

    }
}
