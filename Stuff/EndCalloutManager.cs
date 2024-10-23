using Rage;

namespace JMCalloutsRemastered
{
    class EndCalloutManager
    {
        public static bool CalloutForcedEnd = false;
        public static void EndCallout()
        {
            if (Settings.LeaveCalloutRunning && !CalloutForcedEnd)
            {
                GameFiber.Wait(2000);
                Game.DisplayHelp("Press ~y~" + Settings.EndCall + " ~w~to ~b~Finish~w~ the Callout.");
                Game.LogTrivial("JMCALLOUTS: ENDCALLOUTHANDLER - Player Will Manually End the Callout");
                while (!Game.IsKeyDown(Settings.EndCall)) GameFiber.Wait(0);
                //End the Callout
            }
            else
            {
                if (!CalloutForcedEnd) GameFiber.Wait(2000);
                else Game.LogTrivial("JMCALLOUTS: Callout Was Ended at Start, May Cause Issues!");
                Game.LogTrivial("JMCALLOUTS: ENDCALLOUTHANDLER - Ending Callout Immediately");
                //End the Callout
            }
            CalloutForcedEnd = false;
        }
    }
}
