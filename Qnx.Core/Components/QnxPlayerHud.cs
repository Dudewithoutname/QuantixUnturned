using System;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Core.Components;

public class QnxPlayerHud : MonoBehaviour, IPlayerComponent
{
    private const ushort ID = 31189;
    private const short KEY = 152;

    private QnxPlayer _qnx;
    
    private int _healthSeq;
    private byte _virusSeq;
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        
        #pragma warning disable CS0618 // Type or member is obsolete
        EffectManager.sendUIEffect(ID, KEY, player.Player.TC(), true);
        #pragma warning restore CS0618 // Type or member is obsolete
        
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowLifeMeters, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowUseableGunStatus, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowReputationChangeNotification, false);
        //_qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowDeathMenu, false);
        
        UnturnedPlayerEvents.OnPlayerUpdateVirus += onVirus;

        UpdateHealth();
        onVirus(_qnx.Player.life.virus);
        
        uiText("money v", "100 <color=#3FD200>$</color>");
        uiText("kills v", "0 / 10 Kills");
    }

    private void OnDestroy()
    {
        UnturnedPlayerEvents.OnPlayerUpdateVirus -= onVirus;
    }

    public void SetActive(bool active)
    {
        
    }

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

    private void onVirus(byte virus)
    {
        if (virus == _virusSeq) 
            return;
        
        uiVisible($"vb {virus}", true);
        uiVisible($"vb {_virusSeq}", false);
        _virusSeq = virus;
    }

    private void onVirus(UnturnedPlayer player, byte virus)
    {
        if (player.Player != _qnx.Player) 
            return;
        
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
}