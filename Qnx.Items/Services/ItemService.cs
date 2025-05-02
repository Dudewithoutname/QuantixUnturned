using System;
using System.Collections.Generic;
using System.IO;
using Qnx.Core.Interfaces;
using Qnx.Items.Enums;
using Qnx.Items.Models;
using Qnx.Items.Models.Items;
using Rocket.Core.Logging;
using SDG.Unturned;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Qnx.Items.Services;

public class ItemService 
{
    private Dictionary<ushort, ModifiedItem> _items = [];
    
    private Dictionary<string, GunSet> _gunSets = [];
    
    private Dictionary<ushort, Attachment> _gunAttachments = [];

    public ItemService()
    {
        var path = Path.Combine(Rocket.Core.Environment.PluginsDirectory, "Qnx.Items");
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        foreach (var file in Directory.EnumerateFiles(path))
        {
            if (!file.EndsWith(".yaml")) continue;
            var items = deserializer.Deserialize<List<ModifiedBase>>(File.ReadAllText(file));
            foreach (var mod in items)
            {
                mod.Initialize();
                
                if (mod is ModifiedItem item)
                {
                    _items.Add(item.Id, item);
                }
            }
        }
        
        Logger.Log($"Loaded {_items.Count} items");
    }

    public T? GetItem<T>(ushort id) where T : ModifiedBase
        => _items.GetValueOrDefault(id) as T;

    public Attachment? GetAttachment(ushort attachmentId)
        => _gunAttachments.GetValueOrDefault(attachmentId);
    
    public GunSet? GetGunSet(Item item)
    {
        var str = item.id.ToString() + item.state[];
        item.
    }

}