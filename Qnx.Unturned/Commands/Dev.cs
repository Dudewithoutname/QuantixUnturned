using Rocket.API;
using Rocket.Core.Commands;

namespace Qnx.Unturned.Commands;

public class Dev
{
    [RocketCommand("refresh", "", "", AllowedCaller.Player)]
    public void x(IRocketPlayer caller, string[] command)
    {
        
    }
}