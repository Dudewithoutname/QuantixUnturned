using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Qnx.Unturned.Services;

// ReSharper disable once SuspiciousTypeConversion.Global
public class ServiceCollection : IDisposable
{
    public List<IService> Services { get; }
    
    private readonly GameObject _unityHost;
    
    public ServiceCollection(GameObject unityHost)
    {
        _unityHost = unityHost;
        Services = [];
        
        findAndAddServices();
    }
    
    public void Dispose()
    {
        foreach (var service in Services)
            removeService(service);
    }
    
    private void findAndAddServices()
    {
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes())
        {
            var att = type.GetCustomAttribute<Service>();
            if (att == null) continue;
            if (!typeof(IService).IsAssignableFrom(type))
            {
                Logger.LogError($"Found service attribute but doesn't inherit from service{type}");
                return;
            }
            
            addService(type, att.Type);
        }
    }

    private void removeService(IService service)
    {
        service.Dispose();
        Services.Remove(service);
    }

    private void addService(Type serviceType, EServiceType type)
    {
        IService? service;
        
        if (type == EServiceType.UNITY)
        {
            if (!typeof(MonoBehaviour).IsAssignableFrom(serviceType))
            {
                Logger.LogError($"Trying to host a non-MonoBehaviour service => {serviceType}");
                return;
            }
            
            service = _unityHost.AddComponent(serviceType) as IService;
        }
        else
        {
            service = Activator.CreateInstance(serviceType) as IService;
        }


        if (service == null) return;
        Services.Add(service);
        service.Initialize();
    }
}