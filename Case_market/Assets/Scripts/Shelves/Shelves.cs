


using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shelves : BaseMono, IModuleInit
{

    public List<BaseInventory> inventoryList = new List<BaseInventory>();
    public Transform agentWayPoint;

    public bool occupied = false;

    public void Init()
    {
        foreach (BaseInventory inv in inventoryList)
        {
            inv.Init();

            StartCoroutine(AddSomeItems(inv));
        }

        ShelvesManager.instance.shelvesList.Add(this);
    }

    IEnumerator AddSomeItems(BaseInventory inv)
    {
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            Product created =  ObjectCreator.instance.CreateRandomProduct();
            
            inv.TryAddItem(created);

            StockEvents.OnAdded?.Invoke(created.itemData, 1);

            yield return 0;
        }
    }

    public bool IsThereProduct()
    {
        foreach (BaseInventory inv in inventoryList)
        {   
            if(inv.IsThereAnyItem() == true)
            {
                return true;
            }
        }   
        return false;
    }

    public int GetItemCount()
    {
        int count = 0;

        foreach (BaseInventory inv in inventoryList)
        {   
            count += inv.ItemList.Count;
        } 
        return count;  
    }

}