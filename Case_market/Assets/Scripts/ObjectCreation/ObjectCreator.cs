

using System.Collections.Generic;
using Template;
using UnityEngine;

public class ObjectCreator : Singleton<ObjectCreator> 
{
    public PoolObject boxPool;
    public PoolObject customerPool;
    public List<PoolObject> productPools = new List<PoolObject>();

    private PoolingPattern _poolCached;


    public void Remove(BaseItem item)
    {
        _poolCached = item.itemData.poolObject.poolingPattern;
        _poolCached.AddObjToPool(item.gameObject);
    }


    public Product CreateProduct(ProductData productData)
    {
        _poolCached = productData.poolObject.poolingPattern;
        return _poolCached.PullObjFromPool<Product>();
    }

    public Product CreateRandomProduct()
    {
        _poolCached = productPools[Random.Range(0, productPools.Count)].poolingPattern;
        return _poolCached.PullObjFromPool<Product>();
    }


    public Box CreateBox()
    {
       return boxPool.poolingPattern.PullObjFromPool<Box>(); 
    }
    public void Remove(Box boxToRemove)
    {
        boxPool.poolingPattern.AddObjToPool(boxToRemove.gameObject);
    }


    public CustomerAI CreateCustomer()
    {
        return customerPool.poolingPattern.PullObjFromPool<CustomerAI>(); 
    }
    public void Remove(CustomerAI customerToRemove)
    {
        customerToRemove.inventory.ClearItems();
        
        customerPool.poolingPattern.AddObjToPool(customerToRemove.gameObject);
    }
}