

using UnityEngine;
using DG.Tweening;

public class PlayerInventory : BaseInventory
{
    public CameraUnit cameraUnit;
    public Transform holdDirection;

    private Vector3 _localHoldDir;

    public override void Init()
    {
        base.Init();

        _localHoldDir = cameraUnit.transform.InverseTransformDirection
                            ((holdDirection.position - cameraUnit.transform.position).normalized);
    }

    public override void HandleItemAdded(BaseItem item, bool animate = false)
    {
        item.transform.SetParent(cameraUnit.transform);

        item.transform.DOLocalMove(_localHoldDir * item.itemData.holdDistance, .5f);
    }
}
