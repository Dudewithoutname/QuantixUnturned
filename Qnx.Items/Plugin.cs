using System;
using Qnx.Core;
using Qnx.Core.Services;
using Qnx.Items.Components;
using Rocket.Core.Plugins;
using SDG.Unturned;

namespace Qnx.Items;

public class Plugin : RocketPlugin
{
    protected override void Load()
    {
        Level.onPrePreLevelLoaded += onPreLoad;
    }

    protected override void Unload()
    {
        Level.onPrePreLevelLoaded -= onPreLoad;
    }

    private void onPreLoad(int _)
    {
        PlayerService.Singleton.RegisterComponent<PlayerBuffItems>();
    }
}
