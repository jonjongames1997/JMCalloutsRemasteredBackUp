﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace JMCalloutsRemastered.Stuff
{
    class EndCalloutManager
    {
        public static bool CalloutForcedEnd = false;
        public static void EndCallout()
        {
            if (Settings.LeaveCalloutsRunning && !CalloutForcedEnd)
            {
                GameFiber.Wait(2000);
                Game.DisplayHelp("Press ~o~" + Settings.EndCall + " ~w~to ~g~Finish~w~ the Callout.");
                Game.LogTrivial("YOBBINCALLOUTS: ENDCALLOUTHANDLER - Player Will Manually End the Callout");
                while (!Game.IsKeyDown(Settings.EndCall)) GameFiber.Wait(0);
                //End the Callout
            }
            else
            {
                if (!CalloutForcedEnd) GameFiber.Wait(2000);
                else Game.LogTrivial("JMCallotusRemastered [LOG]: Callout Terminated at Start, May Cause Issues!");
                Game.LogTrivial("JMCallotusRemastered [LOG]: ENDCALLOUTMANAGER - Terminating Callout");
                //End the Callout
            }
            CalloutForcedEnd = false;
        }
    }
}