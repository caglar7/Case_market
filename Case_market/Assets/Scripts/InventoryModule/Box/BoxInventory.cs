
using UnityEngine;
using DG.Tweening;

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

                item.transform.DOLocalMove(Vector3.zero, .5f);

                item.transform.DOLocalRotate(Vector3.zero, .5f);
            }
        }
    }
}