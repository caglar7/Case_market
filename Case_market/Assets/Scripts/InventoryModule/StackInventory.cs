


using UnityEngine;

public class StackInventory : BaseInventory 
{
    public Transform stackHolder;
    public float offsetY = .5f;

    public override void HandleItemAdded(BaseItem item, bool animate = false)
    {
        Vector3 targetLocalPos = Vector3.up * stackHolder.childCount;

        item.transform.SetParent(stackHolder);

        Animate(item, animate, targetLocalPos);
    }
}