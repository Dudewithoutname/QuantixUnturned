using System;
using System.Collections.Generic;
using Qnx.Core.Components;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using Qnx.Items.Enums;
using Qnx.Items.Models;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Items.Components;

public class PlayerBuffItems : MonoBehaviour, IPlayerComponent
{
    public QnxPlayer _qnx;

    public Dictionary<EClothing, ModifiedItem?> ClothingBuffs; 
    
    public ushort[] ClothesArray => 
    [
        _qnx.Player.clothing.hat, _qnx.Player.clothing.glasses, _qnx.Player.clothing.mask,
        _qnx.Player.clothing.shirt, _qnx.Player.clothing.vest,
        _qnx.Player.clothing.pants, _qnx.Player.clothing.backpack
    ];
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        ClothingBuffs = EnumDictionary.Create<EClothing, ModifiedItem?>();
        
        _qnx.Player.clothing.onHatUpdated += updateHat;
        _qnx.Player.clothing.onGlassesUpdated += updateGlasses;
        _qnx.Player.clothing.onMaskUpdated += updateMask;
        _qnx.Player.clothing.onShirtUpdated += updateShirt;
        _qnx.Player.clothing.onVestUpdated += updateVest;
        _qnx.Player.clothing.onPantsUpdated += updatePants;
        _qnx.Player.clothing.onBackpackUpdated += updateBackpack;
    }

    private void OnDestroy()
    {
        _qnx.Player.clothing.onHatUpdated -= updateHat;
        _qnx.Player.clothing.onGlassesUpdated -= updateGlasses;
        _qnx.Player.clothing.onMaskUpdated -= updateMask;
        _qnx.Player.clothing.onShirtUpdated -= updateShirt;
        _qnx.Player.clothing.onVestUpdated -= updateVest;
        _qnx.Player.clothing.onPantsUpdated -= updatePants;
        _qnx.Player.clothing.onBackpackUpdated -= updateBackpack;
    }
    
    private void updateClothing(EClothing type, ushort id)
    {
            
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