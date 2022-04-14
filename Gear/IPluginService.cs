using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gear;

public interface IPluginService
{
    IEnumerable<Type> GetLoadedPlugins();
    IDictionary<string, string> GetConfigs();
}