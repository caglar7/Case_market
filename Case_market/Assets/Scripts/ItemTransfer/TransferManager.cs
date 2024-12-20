

using Template;
using UnityEngine;

/// <summary>
/// 
/// Depends on Inventory Module
/// 
/// Transfer Management within Inventories
/// 
/// Transferin one item or items from one inventory to another 
/// 
/// and related management
/// 
/// </summary>

public class TransferManager : Singleton<TransferManager> 
{
    public void Transfer(BaseItem item, BaseInventory from, BaseInventory target, bool animate)
    {
        if (IsTransferPossible(item, from, target) == false) 
        {
            Debug.Log("Transfer not possible");

            if(item == null) Debug.Log("item null");
            if(from == null) Debug.Log("from inventory null");
            if(target == null) Debug.Log("target inventory null");

            return;
        }

        from.TryRemoveItem(item);

        target.TryAddItem(item, animate);
    }


    private bool IsTransferPossible(BaseItem itemCheck, BaseInventory from, BaseInventory target)
    {
        return  itemCheck != null
                && from != null
                && target != null
                // && from.ContainsItem(itemCheck.itemData, out BaseItem item) == true 
                && target.IsThereEmptySlot() == true;
    }
}