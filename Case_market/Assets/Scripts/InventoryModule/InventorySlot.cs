using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  currently, lets just assume there is only one stack of every item
/// </summary>

[Serializable]
public class InventorySlot
{
    private BaseItem _baseItem;
    public BaseItem Item => _baseItem;
    

    public InventorySlot()
    {

    }    
    
    public InventorySlot(BaseItem item)
    {
        AddItem(item);
    }


    public void AddItem(BaseItem item)
    {
        _baseItem = item;
    }

    public bool IsSlotEmpty()
    {
        return _baseItem == null;
    }

    public void ClearSlot()
    {
        _baseItem = null;
    }
}
