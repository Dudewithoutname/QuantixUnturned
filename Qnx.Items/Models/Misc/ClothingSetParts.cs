using System;
using System.Collections.Generic;

namespace Qnx.Items.Models.Misc;

public class ClothingSetParts
{
    public ushort Hat;
    public ushort Glasses;
    public ushort Mask;
    public ushort Shirt;
    public ushort Vest;
    public ushort Pants;
    public ushort Backpack;

    private ushort[] _array => [Shirt, Pants, Hat, Vest, Glasses, Mask, Backpack];
    
    public bool Compare(ushort[] clothes)
    {
        for (var i = 0; i < 7; i++)
        {
            if (_array[i] == 0)
                continue;

            if (_array[i] != clothes[i])
                return false;
        }

        return true;
    }
}