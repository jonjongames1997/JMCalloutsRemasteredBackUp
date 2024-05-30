using System.Windows.Forms;

namespace JMCalloutsRemastered
{

    internal static class JMCalloutsMenu
    {
        private static MenuPool pool;
        private static UIMenu jmcalloutsnMenu;

        private static readonly Keys KeyBinding = Keys.F5;

        private static void Main()
        {
            pool = new MenuPool();

            jmcalloutsnMenu = new UIMenu("JM Callouts Menu", "BETA");

            {
                var cb = new UIMenuCheckboxItem("Checkbox", false, "Spawn A Vehicle");
                cb.CheckboxEvent += (item, isChecked) => Game.DisplaySubtitle($"The checkbox is {(isChecked ? "" : "~r~not~s~ ")}checked");

                var spawnCar = new UIMenuItem("Spawn A Car", "Spawns a random car");
                spawnCar.Activated += (menu, item) =>
                {
                    spawnCar.Enabled = false;

                    GameFiber.StartNew(() =>
                    {
                        for (int sec = 5; sec > 0; sec--)
                        {
                            jmcalloutsnMenu.DescriptionOverride = $"The car will spawn in ~o~{sec}~s~ second(s).";

                            GameFiber.Sleep(1000);
                        }
                    });

                    jmcalloutsnMenu.DescriptionOverride = null;

                    new Vehicle(m => m.IsCar, Game.LocalPlayer.Character.GetOffsetPositionFront(5.0f)).Dismiss();

                    GameFiber.Sleep(500);
                    spawnCar.Enabled = true;
                };

                var cash = new UIMenuNumericScrollerItem<int>("Cash", "Give or Remove Cash", -10_000, 10_000, 50);
                cash.Formatter = n => cash.Selected ? $"{n:+$#;-$#;$0}" : $"{n:~g~+$#;~r~-$#;$0}";
                jmcalloutsnMenu.OnIndexChange += (menu, itemIndex) => cash.Reformat();
                cash.Activated += (menu, item) =>
                {
                    if(cash.Value == 0)
                    {
                        return;
                    }

                    uint stat = 
                };
            }
        }
    }
}
