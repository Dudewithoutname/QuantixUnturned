using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Qnx.Core.Attributes;
using Qnx.Core.Enums;

namespace Qnx.Core.Utils;

public static class AttributeLoader
{
    public static Dictionary<EBuff, Buff> LoadBuffProviders()
    {
        var result = new Dictionary<EBuff, Buff>();

        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<Buff>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<Buff>();
            if (attr == null) continue;
            
            result[attr.Origin] = Activator.CreateInstance(type) as Buff;
        }

        return result;
    }
}