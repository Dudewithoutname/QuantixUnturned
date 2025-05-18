using System.Reflection;
using SDG.Unturned;

namespace Qnx.Unturned.Utils.Assets;

public static class AssetUtil
{
    internal static void LoadAssets()
    {
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var att = field.GetCustomAttribute<LoadAsset>();
                
                if (att == null) continue;
                if (!typeof(Asset).IsAssignableFrom(field.FieldType)) continue;
                
                var asset = SDG.Unturned.Assets.find(att.Type, att.Id);
                
                if (asset == null)
                {
                    Logger.LogError($"Missing static asset => {type.Name}.{field.Name} with id {att.Id}");
                    field.SetValue(null, null);
                    return;
                }

                if (!field.FieldType.IsInstanceOfType(asset))
                {
                    Logger.LogError($"Invalid static asset type => {type.Name}.{field.Name} with id {att.Id}");
                    field.SetValue(null, null);
                    return;
                }
                field.SetValue(null, asset);
            }
        }    
    }
}