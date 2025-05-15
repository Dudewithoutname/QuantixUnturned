using System;
using JetBrains.Annotations;
using SDG.Unturned;

namespace Qnx.Core.Attributes;


/// <summary>
/// Automatically loads asset when all assets load
/// </summary>
[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
[AttributeUsage(AttributeTargets.Field)]
internal class LoadAsset(EAssetType type, ushort id) : Attribute
{
    public ushort Id = id;
    public EAssetType Type = type;
}