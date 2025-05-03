using System;
using System.Collections.Generic;
using System.IO;
using Qnx.Core.Utils;
using Qnx.Items.Models;
using Qnx.Items.Models.Items;
using Rocket.Core.Logging;
using SDG.Unturned;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Qnx.Items.Services;

public class ItemService : SingletonService<ItemService>
{
    private Dictionary<ushort, ModifiedItem> _items = [];
    private Dictionary<ushort, List<GunSet>> _gunSets = [];
    
    public ItemService()
    {
        var path = Path.Combine(Rocket.Core.Environment.PluginsDirectory, "Qnx.Items");
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        foreach (var file in Directory.EnumerateFiles(path))
        {
            if (!file.EndsWith(".yaml")) 
                continue;
           
            var items = deserializer.Deserialize<List<ModifiedBase>>(File.ReadAllText(file));
            
            foreach (var mod in items)
            {
                switch (mod)
                {
                    case ModifiedItem item:
                        if (_items.TryAdd(item.Id, item))
                        {
                            Logger.LogWarning($"duplicate of item with id {item.Id} from {file}");
                            continue;
                        }
                        _items.Add(item.Id, item);
                        continue;
                    
                    case GunSet set:
                        if (_gunSets.TryGetValue(set.Id, out var list))
                        {
                            list.Add(set);
                            continue;
                        }
                        
                        _gunSets.Add(set.Id, [set]);
                        continue;
                }
            }
        }
        
        Logger.Log($"Loaded {_items.Count} items");
    }

    public ModifiedItem? GetItem(ushort id)
        => _items.GetValueOrDefault(id);

    public void GetAttachmentBuffs(List<ModifiedItem> source, Item item)
    {
        for (var i = 0; i <= 6; i += 2)
        {
            var att  = BitConverter.ToUInt16(item.state, i);
            
            if (att == 0 || !_items.TryGetValue(att, out var value))
                continue;
            
            source.Add(value);
        }
    }
    
    public GunSet? GetGunSet(Item item)
    {
        var gunSets = _gunSets.GetValueOrDefault(item.id);
        var sight = BitConverter.ToUInt16(item.state, 0);
        var tactical = BitConverter.ToUInt16(item.state, 2);
        var grip = BitConverter.ToUInt16(item.state, 4);
        var barrel = BitConverter.ToUInt16(item.state, 6);

        foreach (var set in gunSets)
        {
            if (set.Attachments == null) continue;

            if (set.Attachments.Barrel != 0 || set.Attachments.Barrel != barrel)
                continue;

            if (set.Attachments.Tactical != 0 || set.Attachments.Tactical != tactical)
                continue;

            if (set.Attachments.Grip != 0 || set.Attachments.Grip != grip)
                continue;

            if (set.Attachments.Sight != 0 || set.Attachments.Sight != sight)
                continue;

            return set;
        }
        
        return null;
    }

}