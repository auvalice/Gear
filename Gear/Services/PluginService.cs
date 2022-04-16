using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrackeysBot.API.Plugins;

namespace Gear;

public class PluginService : IPluginService
{
    private IEnumerable<Assembly> LoadedAssemblies =>
        AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic);

    public IEnumerable<Type> GetLoadedPlugins()
    {
        return LoadedAssemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(MonoPlugin).IsAssignableFrom(type) && type != typeof(MonoPlugin))
            .DistinctBy(type => type.FullName);
    }

    public IDictionary<string, string> GetConfigs()
    {
        var result = new Dictionary<string, string>();

        foreach (var type in GetLoadedPlugins())
        {
            result.Add(type.Name, type.GetField("ConfigSchema")?.GetValue(null) as string
                             ?? "");
        }

        return result;
    }
}