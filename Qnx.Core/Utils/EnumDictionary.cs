using System;
using System.Collections.Generic;
using System.Linq;
using Qnx.Core.Enums;

namespace Qnx.Core.Utils;

public static class EnumDictionary
{
    public static Dictionary<EBuff, T> Create<T>()
    {
        return Enum.GetValues(typeof(EBuff)).Cast<EBuff>().ToDictionary(v => v, v => default(T));
    }
}