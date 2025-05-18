namespace Qnx.Unturned.Utils;

public abstract class Singleton<T> where T : class
{
    public static T? Instance => _instance;
 
    private static T? _instance;

    protected Singleton()
    {
        if (_instance is not null)
        {
            //Logger.LogWarning($"Trying to create a new instance singleton service of {typeof(T)}");
            return;
        }
        _instance = this as T;
    }
    

    protected void RemoveInstance()
    {
        _instance = null;
    }
    
}