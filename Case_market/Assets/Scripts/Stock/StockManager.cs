


using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;

public class StockManager : Singleton<StockManager>, IEvents, IModuleInit
{
    public List<ProductData> items = new List<ProductData>();

    public List<StockData> stock;


    public void Init()
    {
        stock = new List<StockData>();
        foreach (ProductData productData in items)
        {
            stock.Add(new StockData(productData));
        }

        RegisterToEvents();
    }
    private void OnDisable() 
    {
        UnRegisterToEvents();
    }



    public void RegisterToEvents()
    {
        StockEvents.OnAdded += AddToStockData;
        StockEvents.OnRemoved += RemoveFromStockData;
    }

    public void UnRegisterToEvents()
    {
        StockEvents.OnAdded -= AddToStockData;
        StockEvents.OnRemoved -= RemoveFromStockData;
    }

    private void AddToStockData(BaseItemData itemType, int count) => StartCoroutine(AddCo(itemType, count));
    IEnumerator AddCo(BaseItemData itemType, int count)
    {
        foreach (StockData stock in stock)
        {
            if(stock.stockType == itemType)
            {
                stock.stockCount += count;
                break;
            }
        }

        yield return 0;
    }
    

    private void RemoveFromStockData(BaseItemData itemType, int count) => StartCoroutine(RemoveCo(itemType, count));
    IEnumerator RemoveCo(BaseItemData itemType, int count)
    {
        foreach (StockData stock in stock)
        {
            if(stock.stockType == itemType)
            {
                stock.stockCount -= count;
                break;
            }
        }

        yield return 0;
    }

    public int GetStockCount(BaseItemData itemType)  
    {
        foreach (StockData stock in stock)
        {
            if(stock.stockType == itemType)
            {
                return stock.stockCount;
            }
        }
        return 0;
    }
}

[System.Serializable]
public class StockData
{
    public BaseItemData stockType;
    public int stockCount;

    public StockData(ProductData t)
    {
        stockType = t;
        stockCount = 0;
    }
}