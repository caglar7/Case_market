

using System;
using System.Collections.Generic;
using DG.Tweening;
using Template;
using UnityEngine;

public class BaseInventory : MonoBehaviour, IModuleInit
{
    public int slotCount = 9;

    public Action<InventorySlot, int> OnInventorySlotUpdated;
    public Action<BaseItem> OnItemAdded;
    public Action OnAnimationDone;

    [HideInInspector] public List<InventorySlot> inventorySlots;

    protected List<BaseItem> _itemList = new List<BaseItem>();

    public List<BaseItem> ItemList 
    {
        get {return _itemList;}
    }
    public BaseItem LastItem => _itemList[_itemList.Count - 1];


    private BaseItem _lastAddedItem;
    public BaseItem LastAddedItem => _lastAddedItem;

    


    public virtual void Init()
    {   
        inventorySlots = new List<InventorySlot>();

        for(int i = 0; i < slotCount; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool TryAddItem(BaseItem item, bool animate = false)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].IsSlotEmpty() == true)
            {
                inventorySlots[i].AddItem(item);

                item.transform.SetParent(transform);

                HandleItemAdded(item, animate);

                _itemList.Add(item);

                OnInventorySlotUpdated?.Invoke(inventorySlots[i], i);

                OnItemAdded?.Invoke(item);

                _lastAddedItem = item;

                return true;
            }
        }
    
        Debug.Log("Couldnt find empty slot");
        return false;
    }

    public bool TryRemoveItem(BaseItem item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].Item == item)
            {
                inventorySlots[i].ClearSlot();

                HandleItemRemoved(item);

                _itemList.Remove(item);

                OnInventorySlotUpdated?.Invoke(inventorySlots[i], i);

                return true;
            }
        }

        Debug.Log("No such item found to remove");
        return false;
    }

    public bool ContainsItem(BaseItemData checkData, out BaseItem item)
    {
        item = null;

        foreach (InventorySlot slot in inventorySlots)
        {
            if(slot.Item != null && slot.Item.itemData == checkData)
            {
                item = slot.Item;
                return true;
            }
        }

        Debug.Log("No such item (" + checkData.itemName + ") found");
        return false;
    }
    
    public BaseItem FindWithType<T>() where T : BaseItem
    {
        foreach (var baseItem in _itemList)
        {
            if (baseItem is T foundItem)
            {
                return foundItem;
            }
        }

        return null;
    }

    public bool IsThereEmptySlot()
    {
        bool isThere = false;   

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].IsSlotEmpty() == true)
            {
                isThere = true;
                break;
            }
        }

        return isThere;
    }
    public bool IsThereAnyItem()
    {
        return _itemList.Count != 0 ? true : false;
    }

    public virtual void HandleItemAdded(BaseItem item, bool animate = false)
    {
        item.gameObject.SetActive(false);
    }
    public virtual void HandleItemRemoved(BaseItem item)
    {
        item.transform.SetParent(null);
    }

    public void Animate(BaseItem item, bool animate, Vector3 targetLocalPos)
    {
        if(animate)
        {
            item.transform.DOKill();

            item.transform.DOLocalMove(targetLocalPos, .5f);

            item.transform.DOLocalRotate(Vector3.zero, .5f)
            .OnComplete(() => {
                OnAnimationDone?.Invoke();
                OnAnimationDone = null;
            });

        }
        else
        {
            item.transform.localPosition = targetLocalPos;
            item.transform.localEulerAngles = Vector3.zero;
        }
    }


}