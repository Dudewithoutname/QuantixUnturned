using System.IO;
using SDG.Unturned;

namespace Qnx.Unturned.Utils;

public static class FileUtil
{
    public static string Path => $"{ReadWrite.PATH}/{ServerSavedata.directory}/{Provider.serverID}/Quantix";

    public static void Init()
    {
        if (Directory.Exists(Path)) return;
        Directory.CreateDirectory(Path);
    }

    public static string PrepareDirectory(string directory)
    {
        var path = System.IO.Path.Combine(Path, directory);
        if (!Directory.Exists(path)) 
            Directory.CreateDirectory(path);

        return path;
    }
}