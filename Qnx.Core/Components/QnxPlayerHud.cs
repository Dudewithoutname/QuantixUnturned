using System;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Core.Components;

public class QnxPlayerHud : MonoBehaviour, IPlayerComponent
{
    private const ushort ID = 15215;
    private const short KEY = 152;

    private QnxPlayer _qnx;
    
    private int _healthSeq;
    private byte _virusSeq;
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowLifeMeters, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowUseableGunStatus, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowReputationChangeNotification, false);
        _qnx.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowDeathMenu, false);
        
        _qnx.Player.life.onVirusUpdated += onVirus;
    }

    private void OnDestroy()
    {
        _qnx.Player.life.onVirusUpdated -= onVirus;
    }

    public void SetActive(bool active)
    {
        
    }

    public void UpdateHealth()
    {
        uiText("HEALTH_NAME ", _qnx.Life.Health.ToString());

        var newSeq = Mathf.FloorToInt((float)_qnx.Life.Health / _qnx.Life.MaxHealth * 100);
        if (newSeq == _healthSeq)
            return;
        
        uiVisible($"BAR_NAME {newSeq}", true);
        uiVisible($"BAR_NAME {_healthSeq}", false);
        _healthSeq = newSeq;
    }

    private void onVirus(byte virus)
    {
        if (virus == _virusSeq) 
            return;
        
        uiText("VIRUS_NAME ", virus.ToString());
        uiVisible($"BAR_NAME {virus}", true);
        uiVisible($"BAR_NAME {_virusSeq}", false);
        _virusSeq = virus;
    }
    
    private void uiText(string child, string text, bool reliable = true)
        => EffectManager.sendUIEffectText(KEY, _qnx.Player.TC(), reliable, child, text);
    
    private void uiVisible(string child, bool active, bool reliable = true)
        => EffectManager.sendUIEffectVisibility(KEY, _qnx.Player.TC(), reliable, child, active);
    
    private void uiImage(string child, string image, bool reliable = true)
        => EffectManager.sendUIEffectImageURL(KEY, _qnx.Player.TC(), reliable, child, image);
}