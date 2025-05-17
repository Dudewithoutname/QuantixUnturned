using System;
using System.Collections;
using System.Collections.Generic;
using Qnx.Core.Components;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using Qnx.Items.Enums;
using Qnx.Items.Models;
using Qnx.Items.Models.Items;
using Qnx.Items.Services;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Items.Components;

public class PlayerBuffItems : MonoBehaviour, IPlayerComponent
{
    public QnxPlayer Qnx;

    public Dictionary<EClothing, ModifiedItem?> ClothingBuffs; 
    public List<ClothingSet> ClothingSets; 
    
    public ModifiedItem? Useable;
    public GunSet? GunSet;
    public List<ModifiedItem> Attachments;
    
    private bool _scheduledSetUpdate;
    
    private ushort[] _clothesArray => 
    [
        Qnx.Player.clothing.shirt, Qnx.Player.clothing.pants, Qnx.Player.clothing.hat,
        Qnx.Player.clothing.vest, Qnx.Player.clothing.glasses, Qnx.Player.clothing.mask,
        Qnx.Player.clothing.backpack
    ];
    
    public void Initialize(QnxPlayer player)
    {
        Qnx = player;
        
        Logger.Log("Init");
        ClothingBuffs = EnumDictionary.Create<EClothing, ModifiedItem?>();
        ClothingSets = [];
        Attachments = [];
        
        PlayerEquipment.OnUseableChanged_Global += onUseableChanged;
        Qnx.Player.clothing.onHatUpdated += updateHat;
        Qnx.Player.clothing.onGlassesUpdated += updateGlasses;
        Qnx.Player.clothing.onMaskUpdated += updateMask;
        Qnx.Player.clothing.onShirtUpdated += updateShirt;
        Qnx.Player.clothing.onVestUpdated += updateVest;
        Qnx.Player.clothing.onPantsUpdated += updatePants;
        Qnx.Player.clothing.onBackpackUpdated += updateBackpack;
    }

    private void OnDestroy()
    {
        PlayerEquipment.OnUseableChanged_Global -= onUseableChanged;
        Qnx.Player.clothing.onHatUpdated -= updateHat;
        Qnx.Player.clothing.onGlassesUpdated -= updateGlasses;
        Qnx.Player.clothing.onMaskUpdated -= updateMask;
        Qnx.Player.clothing.onShirtUpdated -= updateShirt;
        Qnx.Player.clothing.onVestUpdated -= updateVest;
        Qnx.Player.clothing.onPantsUpdated -= updatePants;
        Qnx.Player.clothing.onBackpackUpdated -= updateBackpack;
    }

    private void onUseableChanged(PlayerEquipment equipment)
    {
        if (equipment.player.SteamID() != Qnx.Player.SteamID())
            return;
        
        
        var item = equipment.itemID == 0 ? null : ItemService.Singleton.GetItem(equipment.itemID);
        Logger.Log($"USEALBE ITEM: {item?.Id ?? 0}");
        
        if (Useable is not null)
        { 
            Useable?.RemoveFromPlayer(this);
            Useable = null;
            
            GunSet?.RemoveFromPlayer(this);
            GunSet = null;

            if (Attachments.Count <= 0) 
                return;
            
            foreach (var attachment in Attachments)
                attachment.RemoveFromPlayer(this);
            
            Attachments.Clear();
            return;
        }

        if (item is null)
            return;
        
        Useable = item;
        Useable?.AddToPlayer(this);

        if (equipment.asset is not ItemGunAsset)
            return;
        
        GunSet = ItemService.Singleton.GetGunSet(equipment.itemID, equipment.state);
        if (GunSet is not null)
        {
            GunSet.AddToPlayer(this);
            return;
        }
        
        ItemService.Singleton.GetAttachmentBuffs(Attachments, equipment.state);
        if (Attachments.Count == 0)
            return;
        
        foreach (var attachment in Attachments)
            attachment.AddToPlayer(this);
    }

    
    private void updateClothing(EClothing type, ushort id)
    {
        var cloth = ClothingBuffs[type];
        
        if (cloth?.Id == id)
        {
            return;
        }

        cloth?.RemoveFromPlayer(this);
        cloth = ClothingBuffs[type] = id != 0 ? ItemService.Singleton.GetItem(id) : null;
        cloth?.AddToPlayer(this);
        
        scheduleClothingSetUpdate();
    }

    private void scheduleClothingSetUpdate()
    {
        if (_scheduledSetUpdate)
        {
            return;
        }

        _scheduledSetUpdate = true;

        foreach (var set in ClothingSets)
            set.RemoveFromPlayer(this);

        ClothingSets.Clear();
        
        StartCoroutine(clothingSetUpdate());
    }

    private IEnumerator clothingSetUpdate()
    {
        yield return new WaitForSeconds(1.1f);
        
        ItemService.Singleton.GetClothingSets(ClothingSets, _clothesArray);
    
        foreach (var set in ClothingSets)
            set.AddToPlayer(this);

        _scheduledSetUpdate = false;
    }

    #region unturned garbage
    private void updateHat(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.HAT, id);

    private void updateGlasses(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.GLASSES, id);

    private void updateMask(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.MASK, id);

    private void updateShirt(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.SHIRT, id);

    private void updateVest(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.VEST, id);

    private void updatePants(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.PANTS, id);

    private void updateBackpack(ushort id, byte _, byte[] __)
        => updateClothing(EClothing.BACKPACK, id);
    #endregion
}