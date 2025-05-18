using System;
using Cysharp.Threading.Tasks;

namespace Qnx.Unturned.Services;

public interface IService
{
    public void Initialize() { }

    public void Dispose();
}