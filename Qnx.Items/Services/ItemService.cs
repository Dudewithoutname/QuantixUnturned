using System.Collections.Generic;
using System.IO;
using Qnx.Core.Interfaces;
using Qnx.Items.Enums;
using Qnx.Items.Models;
using SDG.Unturned;

namespace Qnx.Items.Services;

public class ItemService 
{
    // let's save 50 nanoseconds
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _hats = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _glasses = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _masks = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _shirts = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _vests = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _pants = [];
    private Dictionary<ushort, ModifiedItem<ItemClothingAsset>> _backpack = [];


    
    private string _path;
    
    public ItemService()
    {
        _path = Path.Combine(Rocket.Core.Environment.PluginsDirectory, "Qnx.Items");
        
        foreach (var file in Directory.EnumerateFiles(_path))
        {
            if (!file.EndsWith(".yaml")) continue;
            
        }
    }

    public ModifiedItem<ItemClothingAsset>? GetClothingModifier(EClothing type, ushort id)
    {
        return type switch
        {
            EClothing.HAT => _hats.GetValueOrDefault(id),
            EClothing.GLASSES => _glasses.GetValueOrDefault(id),
            EClothing.MASK => _masks.GetValueOrDefault(id),
            EClothing.SHIRT => _shirts.GetValueOrDefault(id),
            EClothing.VEST => _vests.GetValueOrDefault(id),
            EClothing.PANTS => _pants.GetValueOrDefault(id),
            EClothing.BACKPACK => _backpack.GetValueOrDefault(id),
            _ => null
        };
    }
    
    public void Dispose()
    {
        
    }
}