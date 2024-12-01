
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

                if(animate)
                {
                    item.transform.DOLocalMove(Vector3.zero, .5f);
                    item.transform.DOLocalRotate(Vector3.zero, .5f);
                }
                else
                {
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localEulerAngles = Vector3.zero;
                }
            }
        }
    }
}