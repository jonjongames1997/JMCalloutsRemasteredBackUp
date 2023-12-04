using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using CalloutInterfaceAPI;
using System.Windows.Forms;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Vehicle Fire", CalloutProbability.Medium, "Citizen's report of a vehicle on fire", "Code 3", "SAHP")]

    public class VehicleFire : Callout
    {

    }
}
