using System;
using System.Collections.Generic;
using Qnx.Core;
using Qnx.Core.Interfaces;
using Qnx.Core.Services;
using Qnx.Items.Components;
using Qnx.Items.Services;
using Rocket.Core.Plugins;
using SDG.Unturned;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Items;

public class Plugin : RocketPlugin
{
    public List<IService>  _services;
    protected override void Load()
    {
        _services =
        [
            new ItemService()
        ];
        
        Level.onPrePreLevelLoaded += onPreLoad;
    }

    protected override void Unload()
    {
        foreach (var service in _services)
            service.Dispose();
        
        Level.onPrePreLevelLoaded -= onPreLoad;
    }

    private void onPreLoad(int _)
    {
        PlayerService.Singleton.RegisterComponent<PlayerBuffItems>();
    }
}
