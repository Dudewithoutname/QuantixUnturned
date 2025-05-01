using System;
using System.Collections.Generic;
using System.Linq;
using Qnx.Core.Enums;

namespace Qnx.Core.Utils;

public static class EnumDictionary
{
    public static Dictionary<TEnum, TValue> Create<TEnum,TValue> () where TEnum : Enum
        => Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(v => v, v => default(TValue));
    
}