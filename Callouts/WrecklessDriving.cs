﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using JMCalloutsRemastered.Callouts;
using JMCalloutsRemastered.Stuff;
using CalloutInterfaceAPI;
using System.Drawing;
using System.Windows.Forms;

namespace JMCalloutsRemastered.Callouts
{

    [CalloutInterface("Reckless Driving", CalloutProbability.VeryHigh, "Reports of a reckless driver", "Code 3", "LSPD")]

    public class WrecklessDriving : Callout
    {

    }
}