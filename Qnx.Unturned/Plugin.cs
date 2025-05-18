using System;
using HarmonyLib;
using Qnx.Unturned.Services;
using Qnx.Unturned.Utils.Assets;
using Rocket.Core.Plugins;
using SDG.Unturned;

namespace Qnx.Unturned;

public class Plugin : RocketPlugin
{
    public ServiceCollection? ServiceCollection;
    private Harmony? _harmony;

    protected override void Load()
    {
        ServiceCollection = new ServiceCollection(gameObject);
        
        _harmony = new Harmony("dudewithoutname.qnx.unturned");
        _harmony.PatchAll();
        
        Level.onPostLevelLoaded += onPostLoad;
    }

    protected override void Unload()
    {
        _harmony?.UnpatchAll();
        _harmony = null;
        
        ServiceCollection?.Dispose();
        ServiceCollection = null;
        
        Level.onPostLevelLoaded -= onPostLoad;
    }

    private void onPostLoad(int _)
    {
        AssetUtil.LoadAssets();
    }
}