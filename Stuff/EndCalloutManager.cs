using Rage;

namespace JMCalloutsRemastered.Stuff
{
    // Shoutout to Yobbin for the code. https://github.com/YobB1n/YobbinCallouts/blob/master/EndCalloutHandler.cs
    class EndCalloutManager
    {
        public static bool CalloutForceEnd = false;

        public static void EndCallout()
        {
            if(Settings.LeaveCalloutsRunning && !CalloutForceEnd)
            {
                GameFiber.Wait(2000);
                Game.DisplayHelp("Press ~o~" + Settings.EndCall + "~w~ to end the callout at anytime.");
                Game.LogTrivial("JMCallouts [LOG]: ENDCALLOUTMANAGER - Player will end the callout manually...");
                while (!Game.IsKeyDown(Settings.EndCall)) GameFiber.Wait(0);
            }
            else
            {
                if (!CalloutForceEnd) GameFiber.Wait(2000);
                else Game.LogTrivial("JMCALLOUTS [LOG]: Callout ended at the start, may cause issues.");
                Game.LogTrivial("JMCALLOUTS [LOG]: ENDCALLOUTMANAGER - Callout ending immediately");
            }
            CalloutForceEnd = false;
        }
    }
}
