using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrackeysBot.API.Plugins;

namespace Gear.Services;

public class PluginService
{
    public IEnumerable<string> GetLoadedPlugins()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var monoPluginDerivedTypes = assemblies.SelectMany(FindDerivedTypes<MonoPlugin>);
        return monoPluginDerivedTypes.Select(type => type.FullName).OfType<string>();
    }

    private static IEnumerable<Type> FindDerivedTypes<T>(Assembly assembly)
    {
        return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t));
    }
}