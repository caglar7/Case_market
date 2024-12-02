
using UnityEngine;
using DG.Tweening;

public class StaticInventory : BaseInventory 
{
    public Transform[] itemHolders;

    public override void HandleItemAdded(BaseItem item, bool animate = false)
    {
        for (int i = 0; i < itemHolders.Length; i++)
        {
            if(itemHolders[i].childCount == 0)
            {
                item.transform.SetParent(itemHolders[i]);

                Animate(item, animate, Vector3.zero);
            }
        }
    }
}