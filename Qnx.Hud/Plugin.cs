using System;
using Qnx.Core.Models;
using Qnx.Core.Services;
using Qnx.Hud.Components;
using Rocket.Core.Plugins;

namespace Qnx.Hud;

public class Plugin : RocketPlugin
{
    protected override void Load()
    {
        PlayerService.RegisterComponent<PlayerHud>();
    }

    protected override void Unload()
    {
        
    }
}