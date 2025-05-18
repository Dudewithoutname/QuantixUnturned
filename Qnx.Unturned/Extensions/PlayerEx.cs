using Qnx.Unturned.Players;
using SDG.NetTransport;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Unturned.Extensions;

public static class PlayerEx
{
    public static CSteamID SteamID(this SteamPlayer player) => player.playerID.steamID;
    public static CSteamID SteamID(this Player player) => player.channel.owner.playerID.steamID;
    public static ITransportConnection TC(this Player player) => player.channel.GetOwnerTransportConnection();
    public static QnxPlayer? QnxComponent(this Player player) => PlayerService.Instance?.Players[player.channel.owner.playerID.steamID];
}