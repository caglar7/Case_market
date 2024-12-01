

using UnityEngine;
using DG.Tweening;

public class PlayerInventory : BaseInventory
{
    public Transform itemHolder;

    public override void HandleItemAdded(BaseItem item, bool animate = false)
    {
        item.transform.SetParent(itemHolder);

        item.transform.DOLocalMove(Vector3.zero, .5f);

        item.transform.DOLocalRotate(Vector3.zero, .5f);
    }
}
