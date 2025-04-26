using System;
using Rocket.Core.Logging;
using IService = Qnx.Core.Interfaces.IService;

namespace Qnx.Core.Utils;

public abstract class SingletonService<T> : IService  where T : class
{
    public static T Singleton => _instance;
 
    private static T _instance;

    protected SingletonService()
    {
        if (_instance is not null)
        {
            Logger.LogWarning($"Trying to create a new instance singleton service of {typeof(T)}");
            return;
        }
        _instance = this as T;
    }
    

    public void Dispose()
    {
        OnDispose();
        _instance = null;
    }
    
    
    public virtual void OnDispose() {}
}