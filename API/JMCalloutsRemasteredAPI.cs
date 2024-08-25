using JMCalloutsRemastered.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMCalloutsRemastered.Engine
{
    internal static class JMCalloutsRemasteredAPI
    {
        private static List<Dependency> _depends = new List<Dependency>();

        internal static void AddDepend(string name, string version)
        {
            var newDepend = new Dependency()
            {
                PluginName = Assembly.GetExecutingAssembly().FullName.Split('.').First(),
                DependName = name,
                DependVersion = version
            };
            if (!_depends.Contains(newDepend)) _depends.Add(newDepend);
        }

        internal static bool CheckDepends()
        {
            var PluginName = Assembly.GetExecutingAssembly().FullName.Split('.').First();
            var missingDepend = string.Empty;
            var OutdatedDependency = string.Empty;
            var pluginDepends = _depends.Where(depend => depend.PluginName == PluginName).ToList();

            foreach (var depend in pluginDepends)
                if (!File.Exists(depend.PluginName)) missingDepend += $"{depend.DependName}~n~";
            if(missingDepend.Length > 0)
            {
                Logger.Debug($"You are missing a dependency that JM Callouts Remastered relies on!\r\n{missingDepend.Replace("~n~", "\r\n")}{PluginName} could not load!");
                Game.DisplayNotification("new_editor", "warningtriangle", $"~r~{PluginName}", "~r~Not Loaded!", "Plugin is installed incorrectly! Please see the RagePluginHook.log! Get support here: https://discord.gg/N9KgZx4KUn");
                return false;
            }

            foreach(var depend in pluginDepends)
            {
                var dependVersion = new Version(FileVersionInfo.GetVersionInfo(depend.DependName).FileVersion);
                if (dependVersion < new Version(depend.DependVersion)) OutdatedDependency += $"{depend.DependName}~n~";
            }

            if(OutdatedDependency.Length > 0)
            {
                Logger.Debug($"There are dependencies which are outdated! Please update as soon as possible.\r\n{OutdatedDependency.Replace("~n~", "\r\n")}{PluginName} could not load!");
                Game.DisplayNotification("new_editor", "warningtriangle", $"~r~{PluginName}", "~r~Not Loaded!", "Plugin is installed incorrectly!Please see the RagePluginHook.log! Get support here: https://discord.gg/N9KgZx4KUn");
                return false;
            }
            return true;
        }
    }
}
