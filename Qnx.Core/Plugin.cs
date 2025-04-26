using System;
using System.Collections.Generic;
using Qnx.Core.Interfaces;
using Qnx.Core.Services;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;

namespace Qnx.Core;

public class Plugin : RocketPlugin
{
    public List<IService> Services;
    
    public override void LoadPlugin()
    {
        Services =
        [
            new PlayerService()
        ];
    }
}
