using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Qnx.Core.Attributes;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using Rocket.Core.Logging;
using SDG.Unturned;

namespace Qnx.Core.Utils;

internal static class AttributeLoader
{
    internal static Dictionary<EBuff, IBuff> GetBuffProviders()
    {
        var result = new Dictionary<EBuff, IBuff>();

        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<Buff>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<Buff>();
            if (attr == null) continue;
            
            result[attr.Origin] = Activator.CreateInstance(type) as IBuff;
        }

        return result;
    }

    internal static void LoadAssets()
    {
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var att = field.GetCustomAttribute<LoadAsset>();
                
                if (att == null) continue;
                if (!typeof(Asset).IsAssignableFrom(field.FieldType)) continue;
                
                var asset = Assets.find(att.Type, att.Id);
                
                if (asset == null)
                {
                    Logger.LogError($"Missing static asset => {type.Name}.{field.Name} with id {att.Id}");
                    return;
                }

                if (!field.FieldType.IsAssignableFrom(asset.GetType()))
                {
                    Logger.LogError($"Invalid static asset type => {type.Name}.{field.Name} with id {att.Id}");
                    return;
                }
                field.SetValue(null, asset);
            }
        }    
    }
}