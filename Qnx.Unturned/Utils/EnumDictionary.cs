using System;
using System.Collections.Generic;
using System.Linq;

namespace Qnx.Unturned.Utils;

public static class EnumDictionary
{
    public static Dictionary<TEnum, TValue?> Create<TEnum,TValue> () where TEnum : Enum
        => Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(v => v, v => default(TValue));
}