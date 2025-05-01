using System.IO;
using Qnx.Core.Interfaces;

namespace Qnx.Items.Services;

public class ItemService 
{
    private string _path;
    
    public ItemService()
    {
        _path = Path.Combine(Rocket.Core.Environment.PluginsDirectory, "Qnx.Items");
        
        foreach (var file in Directory.EnumerateFiles(_path))
        {
            if (!file.EndsWith(".yaml")) continue;
            
        }
    }
    
    public void Dispose()
    {
        
    }
}