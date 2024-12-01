
using UnityEngine;

public class BoxInventory : BaseInventory 
{
    public Transform[] itemHolders;

    public override void HandleItemAdded(BaseItem item)
    {
        for (int i = 0; i < itemHolders.Length; i++)
        {
            if(itemHolders[i].childCount == 0)
            {
                item.transform.SetParent(itemHolders[i]);
                item.transform.localPosition = Vector3.zero;
            }
        }
    }
}