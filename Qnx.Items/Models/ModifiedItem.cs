using Qnx.Core.Components;
using SDG.Unturned;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

public record ModifiedItem<T> : ModifiedBase where T : ItemAsset
{
    [YamlIgnore] public T? Asset;
    
    [YamlMember(Order = 0)]
    public ushort Id;

    public override void Initialize()
    {
        Asset = Assets.find(EAssetType.ITEM, Id) as T;
    }

    public virtual bool Match(QnxPlayer player)
    {
        return false;
    }
}