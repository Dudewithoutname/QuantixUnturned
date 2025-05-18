using Qnx.Unturned.Extensions;
using Qnx.Unturned.Utils.Assets;
using SDG.Unturned;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Unturned.Players.Components;

public class PlayerHud : MonoBehaviour, IPlayerComponent
{
    private const ushort ID = 31189;
    private const short KEY = 152;

    [LoadAsset(EAssetType.EFFECT, ID)] 
    private static EffectAsset _assetUI = null!;

    private QnxPlayer _qnx;
    
    private int _healthSeq;
    private byte _virusSeq;
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        
        EffectManager.SendUIEffect(_assetUI, KEY, player.Player.TC(), true);
        
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowLifeMeters, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowUseableGunStatus, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowReputationChangeNotification, false);
        //_qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowDeathMenu, false);
        

        UpdateHealth();
        UpdateVirus(_qnx.Player.life.virus);
        
        uiText("money v", "100 <color=#3FD200>$</color>");
        uiText("kills v", "0 / 10 Kills");

        PlayerEquipment.OnInspectingUseable_Global += onInspect;

        _qnx.Player.equipment.onEquipRequested += onEquip;
        _qnx.Player.equipment.onDequipRequested += onDequip;
        
        if (player.Player.equipment.itemID != 0)
        {
            var garbage = false;
            onEquip
            (
                _qnx.Player.equipment,
                _qnx.Player.inventory.getItem(
                    _qnx.Player.equipment.equippedPage,
                    _qnx.Player.inventory.getIndex(_qnx.Player.equipment.equippedPage, _qnx.Player.equipment.equipped_x, _qnx.Player.equipment.equipped_y)
                ),
                _qnx.Player.equipment.asset, 
                ref garbage
            );
            return;
        }
        
        uiVisible("weapons", false);
    }

    private void OnDestroy()
    {
        _qnx.Player.equipment.onEquipRequested -= onEquip;
        PlayerEquipment.OnInspectingUseable_Global -= onInspect;
        _qnx.Player.equipment.onDequipRequested -= onDequip;

    }

    public void SetActive(bool active)
    {
    }

    private void onInspect(PlayerEquipment equipment)
    {
        if (equipment.player.SteamID() != _qnx.Player.SteamID())
            return;
     
        uiVisible("w up", false);
        uiVisible("w up", true);
    }
    private void onEquip(PlayerEquipment equipment, ItemJar jar, ItemAsset asset, ref bool _)
    {
        switch (asset)
        {
            case ItemMeleeAsset melee:
            {
                uiText("w mode", "MELEE");
                uiText("w n", "1");
                uiText("w a", "1");
                uiVisible("w up", true);
                break;
            }
            case ItemGunAsset gun:
            {
                // todo ammo management
                uiVisible("w up", true);
                break;
            }
            case ItemMedicalAsset { hasAid: true } medical:
            {
                uiText("w mode", "HEAL");
                uiText("w n", "HP");
                uiText("w a", medical.health.ToString());
                uiVisible("w up", false);
                break;
            }
            
            default:
            {
                uiVisible("weapons", false);
                return;
            }        
        }
        uiText("w name", asset.FriendlyName);
        uiVisible("weapons", false);
        uiVisible("weapons", true);
    }

    private void onDequip(PlayerEquipment equipment, ref bool _)
     => uiVisible("weapons", false);

    public void UpdateHealth()
    {
        uiText("health v", _qnx.Life.Health.ToString());

        var newSeq = Mathf.FloorToInt((float)_qnx.Life.Health / _qnx.Life.MaxHealth * 100);
        if (newSeq == _healthSeq)
            return;
        
        uiVisible($"hv {newSeq}", true);
        uiVisible($"hv {_healthSeq}", false);
        _healthSeq = newSeq;
    }

    public void UpdateVirus(byte virus)
    {
        if (virus == _virusSeq) 
            return;
        
        uiVisible($"vb {virus}", true);
        uiVisible($"vb {_virusSeq}", false);
        _virusSeq = virus;
    }
    
    private void uiText(string child, string text, bool reliable = true)
        => EffectManager.sendUIEffectText(KEY, _qnx.Player.TC(), reliable, child, text);
    
    private void uiVisible(string child, bool active, bool reliable = true)
        => EffectManager.sendUIEffectVisibility(KEY, _qnx.Player.TC(), reliable, child, active);
    
    private void uiImage(string child, string image, bool reliable = true)
        => EffectManager.sendUIEffectImageURL(KEY, _qnx.Player.TC(), reliable, child, image);

    private static string getFiremodeName(EFiremode firemode) => firemode switch
    {
        EFiremode.AUTO => "AUTO",
        EFiremode.SEMI => "SEMI",
        EFiremode.SAFETY => "LOCK",
        EFiremode.BURST => "BURST",
        _ => "ERR"
    };
}