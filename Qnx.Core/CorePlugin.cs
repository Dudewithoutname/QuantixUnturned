using System.Collections.Generic;
using HarmonyLib;
using Qnx.Core.Interfaces;
using Qnx.Core.Services;
using Rocket.Core.Plugins;
using SDG.Unturned;

namespace Qnx.Core;

public class CorePlugin : RocketPlugin
{
    public List<IService> Services;

    private Harmony _harmony;
    
    protected override void Load()
    {
        _harmony = new Harmony("dudewithoutname.qnx.core");
        _harmony.PatchAll();
        
        Services =
        [
            new PlayerService(),
            new PlayerHealthService(),
        ];
    }

    protected override void Unload()
    {
    }
}
