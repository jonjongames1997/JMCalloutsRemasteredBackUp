namespace JMCalloutsRemastered
{
    internal class StatsDisplay
    {
        public static void DisplayStats()
        {
            Game.DisplayNotification("web_jonjongames", "web_jonjongames", "~w~JM Callouts Remastered", "~w~Patrol Stats", string.Format("~s~Arrest: ~o~{0} ~s~~n~Fights Involed In: ~o~{1} ~n~~s~Pursuts: ~o~{2} ~n~~s~Calls Accepted: ~o~{3} ~n~~s~Shootouts: ~o~{4}", new object[]
            {
                Settings.Arrests.ToString(),
                Settings.FightsInvolved.ToString(),
                Settings.Pursuits.ToString(),
                Settings.CallsAccepted.ToString(),
                Settings.Shootouts.ToString(),
            }));
        }
    }
}
