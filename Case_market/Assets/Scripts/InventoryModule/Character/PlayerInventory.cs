using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    public Transform itemHolder;

    public override void HandleItemAdded(BaseItem item)
    {
        item.transform.SetParent(itemHolder);
        item.transform.localPosition = Vector3.zero;
    }
}
