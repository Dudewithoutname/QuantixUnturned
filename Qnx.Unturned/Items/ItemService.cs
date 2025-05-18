using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Qnx.Unturned.Items.Models;
using Qnx.Unturned.Items.Models.Types;
using Qnx.Unturned.Services;
using Qnx.Unturned.Utils;
using SDG.Unturned;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Qnx.Unturned.Items;

[Service(EServiceType.NORMAL)]
public class ItemService : Singleton<ItemService>, IService
{
    private static string _directory = "Items";
    
    private Dictionary<ushort, ModifiedItem> _items;
    private Dictionary<ushort, List<GunSet>> _gunSets;
    private List<ClothingSet> _clothingSets;

    public ItemService()
    {
        _items = [];
        _gunSets = [];
        _clothingSets = [];
        getItemsFromFiles();
        Logger.Log($"Loaded {_items.Count} items, {_clothingSets.Count} clothing sets, {_gunSets.Count} gun sets");
    }

    public void Dispose()
    {
        RemoveInstance();
    }
    
    private void getItemsFromFiles()
    {
        var path = FileUtil.PrepareDirectory(_directory);
        var deserializer = createDeserializer();
        
        foreach (var file in Directory.EnumerateFiles(path))
        {
            if (!file.EndsWith(".yaml")) 
                continue;
           
            var items = deserializer.Deserialize<List<ModifiedBase>>(File.ReadAllText(file));
            registerItems(items, file);
        }
    }
    
    private IDeserializer createDeserializer()
    {
        var b = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance);
        
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => 
                t is { IsAbstract: false, IsClass: true } 
                && (typeof(ModifiedBase).IsAssignableFrom(t) || 
                    typeof(ItemBuff).IsAssignableFrom(t)));
        
        foreach (var type in types)
        {
            var tag = "!" + type.Name;
            b = b.WithTagMapping(tag, type);
        }
        
        return b.Build();
    }
    
    private void registerItems(List<ModifiedBase> items, string fileName = "unknown")
    {
        foreach (var mod in items)
        {
            switch (mod)
            {
                case ModifiedItem item:
                    if (_items.TryAdd(item.Id, item))
                    {
                        continue;
                    }
                    Logger.LogWarning($"duplicate of item with id {item.Id} from {fileName}");
                    continue;
                    
                case GunSet set:
                    if (_gunSets.TryGetValue(set.Id, out var list))
                    {
                        list.Add(set);
                        continue;
                    }

                    _gunSets.Add(set.Id, [set]);
                    continue;
                case ClothingSet set:
                    if (set.Parts is null)
                    {
                        Logger.LogWarning($"Ignoring clothing set {set.Name ?? "NULL_NAME"}, doesn't have any parts from {fileName}");
                        continue;
                    }
                    _clothingSets.Add(set);
                    continue;
            }
        }
    }

    public ModifiedItem? GetItem(ushort id)
         => _items.GetValueOrDefault(id);
    

    public void GetClothingSets(List<ClothingSet> source, ushort[] clothes) 
        => source.AddRange(_clothingSets.Where(set => set.Parts!.Compare(clothes)));
    
    public void GetAttachmentBuffs(List<ModifiedItem> source, byte[] state)
    {
        for (var i = 0; i <= 6; i += 2)
        {
            var att  = BitConverter.ToUInt16(state, i);
            
            if (att == 0 || !_items.TryGetValue(att, out var value))
                continue;
            
            source.Add(value);
        }
    }
    
    public GunSet? GetGunSet(ushort id, byte[] state)
    {
        var gunSets = _gunSets.GetValueOrDefault(id);
        
        if (gunSets is null)
            return null;
        
        var sight = BitConverter.ToUInt16(state, 0);
        var tactical = BitConverter.ToUInt16(state, 2);
        var grip = BitConverter.ToUInt16(state, 4);
        var barrel = BitConverter.ToUInt16(state, 6);

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