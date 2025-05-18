using UnityEngine;

namespace Qnx.Unturned.Utils;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T? Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance ??= (T)this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}