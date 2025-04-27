using System;
using System.Collections.Generic;
using HarmonyLib;
using Qnx.Core.Interfaces;
using Qnx.Core.Services;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
namespace Qnx.Core;

public class Plugin : RocketPlugin
{
    public List<IService> Services;

    private Harmony _harmony;
    
    public override void LoadPlugin()
    {
        _harmony = new Harmony("dudewithoutname.qnx.core");
        _harmony.PatchAll();
        
        Services =
        [
            new PlayerService(),
            new PlayerHealthService(),
        ];
    }
}
