

using UnityEngine;
using DG.Tweening;

public class PlayerInventory : BaseInventory
{
    public Transform camTransform;
    public Transform holdDirection;

    private Vector3 _localHoldDir;

    public override void Init()
    {
        base.Init();

        _localHoldDir = camTransform.InverseTransformDirection((holdDirection.position - camTransform.position).normalized);
    }

    public override void HandleItemAdded(BaseItem item, bool animate = false)
    {
        item.transform.SetParent(camTransform);

        item.transform.DOLocalMove(_localHoldDir * item.itemData.holdDistance, .5f);
    }
}
