


using System;
using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;

// creating and filling the boxes with the given order

public class OrderArea : BaseMono, IModuleInit, IEvents
{
    public BaseInventory inventory;
    public float boxSpawnPeriod = .5f;

    public void Init()
    {
        inventory.Init();
        RegisterToEvents();

    }
    private void OnDisable() 
    {
        UnRegisterToEvents();    
    }


    public void RegisterToEvents()
    {
        OrderEvents.OnOrderCalculated += HandleOrderCalculated;
    }

    public void UnRegisterToEvents()
    {
        OrderEvents.OnOrderCalculated -= HandleOrderCalculated;
    }


    private void HandleOrderCalculated(int boxCount, List<ProductData> productList)
    {
        StartCoroutine(HandleOrderCo(boxCount, productList));
    }

    IEnumerator HandleOrderCo(int boxCount, List<ProductData> productList)
    {

        for (int i = 0; i < boxCount; i++)
        {
            Box box = ObjectCreator.instance.CreateBox();
            box.Init();

            for (int j = 0; j < 4; j++)
            {
                if(productList.Count == 0) break;

                ProductData productData = productList[0];
                
                productList.RemoveAt(0);

                box.inventory.TryAddItem(ObjectCreator.instance.CreateProduct(productData));

                StockEvents.OnAdded?.Invoke(productData, 1);
            }

            inventory.TryAddItem(box);

            yield return new WaitForSeconds(boxSpawnPeriod);
        }
    }
}